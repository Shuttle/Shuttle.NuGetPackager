using System;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Shuttle.NuGetPackager
{
    internal sealed class ConfigureProjectCommand
    {
        private const string Title = "Configure NuGet Project";
        public const int CommandId = 0x0100;
        private static string _extensionPath;
        public static readonly Guid CommandSet = new Guid("a3b3285d-a7f8-4c25-93ab-84f31e31a612");
        private readonly AsyncPackage package;

        private ConfigureProjectCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static ConfigureProjectCommand Instance { get; private set; }
        private IAsyncServiceProvider ServiceProvider => package;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new ConfigureProjectCommand(package, commandService);

            var codebase = typeof(ConfigureProjectCommand).Assembly.CodeBase;
            var uri = new Uri(codebase, UriKind.Absolute);

            _extensionPath = Path.GetDirectoryName(uri.LocalPath);
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var invoked = false;

            var dte = (DTE)ServiceProvider.GetServiceAsync(typeof(DTE)).Result;

            if (dte.ActiveSolutionProjects is object[] activeSolutionProjects)
            {
                foreach (var activeSolutionProject in activeSolutionProjects)
                {
                    if (activeSolutionProject is Project project)
                    {
                        invoked = true;

                        ConfigureBuildFolder(project);
                    }
                }
            }

            if (!invoked)
            {
                VsShellUtilities.ShowMessageBox(
                    package,
                    "This command may only be executed on a project.",
                    Title,
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }
        }

        private void ConfigureBuildFolder(Project project)
        {
            var projectFolder = Path.GetDirectoryName(project.Properties.Item("FullPath").Value.ToString());

            if (string.IsNullOrEmpty(projectFolder))
            {
                throw new ApplicationException("Could not determine project path.");
            }

            var packageFolderProjectItem = FindFolder(project, ".package");
            var packageFolder = Path.Combine(projectFolder, ".package");

            if (Directory.Exists(packageFolder))
            {
                CopyBuildRelatedFile(packageFolder, "Shuttle.NuGetPackager.MSBuild.dll");
                CopyBuildRelatedFile(packageFolder, "Shuttle.NuGetPackager.targets");

                VsShellUtilities.ShowMessageBox(
                    package,
                    $"Package folder '{packageFolder}' already exists.  Files `Shuttle.NuGetPackager.MSBuild.dll` and `Shuttle.NuGetPackager.targets` have been copied.",
                    Title,
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

                return;
            }

            var view = new ConfigureView();

            try
            {
                if (view.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (packageFolderProjectItem == null)
                {
                    packageFolderProjectItem =
                        project.ProjectItems.AddFromDirectory(Path.Combine(projectFolder, ".package"));
                }

                CopyBuildRelatedFile(packageFolder, "Shuttle.NuGetPackager.MSBuild.dll");
                CopyBuildRelatedFile(packageFolder, "Shuttle.NuGetPackager.targets");
                ProcessBuildRelatedFile(project, view, packageFolder, "package.msbuild.template", "package.msbuild");
                ProcessBuildRelatedFile(project, view, packageFolder, "AssemblyInfo.cs.template",
                    "AssemblyInfo.cs.template");

                File.WriteAllText(Path.Combine(packageFolder, "package.nuspec.template"),
                    GetNuspecTemplate(view, project));

                packageFolderProjectItem.ProjectItems.AddFromFile(Path.Combine(packageFolder,
                    "Shuttle.NuGetPackager.MSBuild.dll"));
                packageFolderProjectItem.ProjectItems.AddFromFile(Path.Combine(packageFolder,
                    "Shuttle.NuGetPackager.targets"));
                packageFolderProjectItem.ProjectItems.AddFromFile(Path.Combine(packageFolder, "package.msbuild"));
                packageFolderProjectItem.ProjectItems.AddFromFile(
                    Path.Combine(packageFolder, "package.nuspec.template"));
                packageFolderProjectItem.ProjectItems.AddFromFile(Path.Combine(packageFolder,
                    "AssemblyInfo.cs.template"));

                project.Save();

                ConfigureProjectFile(project, projectFolder);
            }
            finally
            {
                view.Dispose();
            }
        }

        private string GetNuspecTemplate(ConfigureView view, Project project)
        {
            var result = new StringBuilder();

            result.AppendLine("<?xml version=\"1.0\"?>");
            result.AppendLine();
            result.AppendLine("<package>");
            result.AppendLine("\t<metadata>");
            result.AppendLine(
                $"\t\t<id>{(view.ExplicitPackageName.Checked ? view.PackageName.Text : project.Name)}</id>");
            result.AppendLine("\t\t<version>#{SemanticVersion}#</version>");
            result.AppendLine($"\t\t<authors>{view.Authors.Text}</authors>");
            result.AppendLine($"\t\t<owners>{view.Owners.Text}</owners>");

            switch ((string)view.LicenseType.SelectedItem ?? string.Empty)
            {
                case "Expression":
                {
                    result.AppendLine($"\t\t<license type=\"expression\">{view.License.Text}</license>");
                    break;
                }
                case "File":
                {
                    result.AppendLine($"\t\t<license type=\"file\">{Path.GetFileName(view.License.Text)}</license>");
                    break;
                }
            }

            if (view.LicenseType.SelectedIndex > 0)
            {
                result.AppendLine(
                    $"\t\t<requireLicenseAcceptance>{view.RequireLicenseAcceptance.Checked.ToString().ToLower()}</requireLicenseAcceptance>");
            }

            if (view.HasIcon.Checked)
            {
                result.AppendLine($"\t\t<icon>images\\{Path.GetFileName(view.IconPath.Text)}</icon>");
            }

            if (view.HasReadme.Checked)
            {
                result.AppendLine($"\t\t<readme>docs\\{Path.GetFileName(view.ReadmePath.Text)}</readme>");
            }

            if (!string.IsNullOrWhiteSpace(view.RepositoryUrl.Text))
            {
                result.AppendLine($"\t\t<repository type=\"git\" url=\"{view.RepositoryUrl.Text}\" />");
            }

            if (!string.IsNullOrWhiteSpace(view.ProjectUrl.Text))
            {
                result.AppendLine($"\t\t<projectUrl>{view.ProjectUrl.Text}</projectUrl>");
            }

            result.AppendLine($"\t\t<description>{view.Description.Text}</description>");
            result.AppendLine(
                $"\t\t<copyright>Copyright (c) #{{Year}}#, {(string.IsNullOrWhiteSpace(view.Owners.Text) ? view.Authors.Text : view.Owners.Text)}</copyright>");
            result.AppendLine($"\t\t<tags>{view.Tags.Text}</tags>");

            result.AppendLine("\t\t<dependencies>");
            result.AppendLine("#{Dependencies}#");
            result.AppendLine("\t\t</dependencies>");
            result.AppendLine("\t</metadata>");
            result.AppendLine("\t<files>");

            if (view.HasIcon.Checked)
            {
                result.AppendLine($"\t\t<file src=\"{view.IconPath.Text}\" target=\"images\" />");
            }

            if (view.HasReadme.Checked)
            {
                result.AppendLine($"\t\t<file src=\"{view.ReadmePath.Text}\" target=\"docs\" />");
            }

            if (((string)view.LicenseType.SelectedItem ?? string.Empty).Equals("File",
                    StringComparison.InvariantCultureIgnoreCase))
            {
                result.AppendLine($"\t\t<file src=\"{view.License.Text}\" target=\"\" />");
            }

            result.AppendLine("\t\t<file src=\"lib\\**\\*.*\" target=\"lib\" />");
            result.AppendLine("\t</files>");
            result.AppendLine("</package>");

            return result.ToString();
        }

        private ProjectItem FindFolder(Project project, string name)
        {
            ProjectItem result = null;

            foreach (ProjectItem projectItem in project.ProjectItems)
            {
                if (!projectItem.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                result = projectItem;
                break;
            }

            return result;
        }

        private static void ConfigureProjectFile(Project project, string projectFolder)
        {
            var projectFilePath = Path.Combine(projectFolder, project.FileName);

            if (!File.Exists(projectFilePath))
            {
                return;
            }

            try
            {
                var result = new StringBuilder();

                using (var sr = new StreamReader(projectFilePath))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("<GenerateAssemblyInfo>"))
                        {
                            continue;
                        }

                        if (line.Contains("<TargetFrameworks>") || line.Contains("<TargetFramework>"))
                        {
                            result.AppendLine(
                                "    <TargetFrameworks>netstandard2.0;netstandard2.1</TargetFrameworks>");
                            result.AppendLine(
                                "    <GenerateAssemblyInfo>false</GenerateAssemblyInfo>");
                        }
                        else
                        {
                            result.AppendLine(line);
                        }
                    }
                }

                File.WriteAllText(projectFilePath, result.ToString());
            }
            catch
            {
                // ignore
            }
        }

        public void CopyBuildRelatedFile(string packageFolder, string fileName)
        {
            var sourceFileName = Path.Combine(_extensionPath, ".package", fileName);
            var targetFileName = Path.Combine(packageFolder, fileName);

            try
            {
                File.Copy(sourceFileName, targetFileName, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $@"[CopyBuildRelatedFile] : could not copy '{sourceFileName}' tp '{targetFileName}' / exception = {ex.Message}");
            }
        }

        private static void ProcessBuildRelatedFile(Project project, ConfigureView view, string packageFolder,
            string sourceFileName, string targetFileName)
        {
            var targetPath = Path.Combine(packageFolder, targetFileName);

            if (File.Exists(targetPath))
            {
                return;
            }

            File.Copy(Path.Combine(_extensionPath, ".package", sourceFileName), targetPath);

            var packageName = project.Name;

            var contents = File.ReadAllText(targetPath)
                .Replace("#{PackageName}#", packageName)
                .Replace("#{Owners}#",
                    string.IsNullOrWhiteSpace(view.Owners.Text) ? view.Authors.Text : view.Owners.Text);

            File.WriteAllText(targetPath, contents);
        }
    }
}