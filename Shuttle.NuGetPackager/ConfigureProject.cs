using System;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Shuttle.NuGetPackager.VSIX
{
    internal sealed class ConfigureProject
    {
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("aa40f306-40eb-446d-a5bb-1b89031eeba0");
        private static string _extensionPath;
        private readonly Package _package;

        private ConfigureProject(Package package)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));

            if (!(ServiceProvider.GetService(typeof(IMenuCommandService)) is OleMenuCommandService commandService))
            {
                return;
            }

            commandService.AddCommand(new MenuCommand(MenuItemCallback, new CommandID(CommandSet, CommandId)));
        }

        public static ConfigureProject Instance { get; private set; }
        private IServiceProvider ServiceProvider => _package;

        public static void Initialize(Package package)
        {
            Instance = new ConfigureProject(package);

            var codebase = typeof(ConfigureProject).Assembly.CodeBase;
            var uri = new Uri(codebase, UriKind.Absolute);

            _extensionPath = Path.GetDirectoryName(uri.LocalPath);
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            const string title = "Configure NuGet Project";
            var invoked = false;

            var dte = (DTE) ServiceProvider.GetService(typeof(DTE));
            var activeSolutionProjects = dte.ActiveSolutionProjects as object[];

            if (activeSolutionProjects != null)
            {
                foreach (var activeSolutionProject in activeSolutionProjects)
                {
                    var project = activeSolutionProject as Project;

                    if (project != null)
                    {
                        invoked = true;

                        ConfigureBuildFolder(project);
                    }
                }
            }

            if (!invoked)
            {
                VsShellUtilities.ShowMessageBox(
                    ServiceProvider,
                    "This command may only be executed on a project.",
                    title,
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

            var view = new ConfigureView();

            try
            {
                if (view.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                var packageFolderProjectItem = FindFolder(project, ".package");
                var packageFolder = Path.Combine(projectFolder, ".package");

                if (packageFolderProjectItem == null)
                {
                    packageFolderProjectItem = project.ProjectItems.AddFromDirectory(Path.Combine(projectFolder, ".package"));
                }

                CopyBuildRelatedFile(packageFolder, "Shuttle.NuGetPackager.MSBuild.dll");
                CopyBuildRelatedFile(packageFolder, "Shuttle.NuGetPackager.targets");
                ProcessBuildRelatedFile(project, packageFolder, "package.msbuild.template", "package.msbuild");
                ProcessBuildRelatedFile(project, packageFolder, "AssemblyInfo.cs.template", "AssemblyInfo.cs.template");

                File.WriteAllText(Path.Combine(packageFolder, "package.nuspec.template"), GetNuspecTemplate(view, project));
                File.WriteAllText(Path.Combine(packageFolder, "package.nuspec"), @"<!-- WILL BE POPULATED ON RELEASE -->");

                packageFolderProjectItem.ProjectItems.AddFromFile(Path.Combine(packageFolder, "Shuttle.NuGetPackager.MSBuild.dll"));
                packageFolderProjectItem.ProjectItems.AddFromFile(Path.Combine(packageFolder, "Shuttle.NuGetPackager.targets"));
                packageFolderProjectItem.ProjectItems.AddFromFile(Path.Combine(packageFolder, "package.msbuild"));
                packageFolderProjectItem.ProjectItems.AddFromFile(Path.Combine(packageFolder, "package.nuspec.template"));
                packageFolderProjectItem.ProjectItems.AddFromFile(Path.Combine(packageFolder, "AssemblyInfo.cs.template"));
                packageFolderProjectItem.ProjectItems.AddFromFile(Path.Combine(packageFolder, "package.nuspec"));

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
            result.AppendLine($"\t\t<id>{(view.ExplicitPackageName.Checked ? view.PackageName.Text : project.Name)}</id>");
            result.AppendLine("\t\t<version>#{SemanticVersion}#</version>");
            result.AppendLine($"\t\t<authors>{view.Authors.Text}</authors>");
            result.AppendLine($"\t\t<owners>{view.Owners.Text}</owners>");

            switch (view.LicenseType.SelectedText)
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
                result.AppendLine($"\t\t<requireLicenseAcceptance>{view.RequireLicenseAcceptance}</requireLicenseAcceptance>");
            }

            if (view.HasIcon.Checked)
            {
                result.AppendLine($"<icon>images\\{Path.GetFileName(view.IconPath.Text)}</icon>");
            }

            if (!string.IsNullOrWhiteSpace(view.RepositoryUrl.Text))
            {
                result.AppendLine($"\t\t<repository type=\"git\" url=\"{view.RepositoryUrl.Text}\"");
            }

            if (!string.IsNullOrWhiteSpace(view.ProjectUrl.Text))
            {
                result.AppendLine($"\t\t<projectUrl>{view.ProjectUrl.Text}</projectUrl>");
            }

            result.AppendLine($"\t\t<description>{view.Description.Text}</description>");
            result.AppendLine($"\t\t<copyright>Copyright (c) #{{Year}}#, {(string.IsNullOrWhiteSpace(view.Owners.Text) ? view.Authors.Text : view.Owners.Text)}</copyright>");
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

            if (view.LicenseType.SelectedText.Equals("File", StringComparison.InvariantCultureIgnoreCase))
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
                                "    <TargetFramework>netstandard2.0</TargetFramework>");
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
                Console.WriteLine($@"[CopyBuildRelatedFile] : could not copy '{sourceFileName}' tp '{targetFileName}' / exception = {ex.Message}");
            }
        }

        private static void ProcessBuildRelatedFile(Project project, string packageFolder, string sourceFileName, string targetFileName)
        {
            var targetPath = Path.Combine(packageFolder, targetFileName);

            if (File.Exists(targetPath))
            {
                return;
            }

            File.Copy(Path.Combine(_extensionPath, ".package", sourceFileName), targetPath);

            var packageName = project.Name;

            File.WriteAllText(targetPath, File.ReadAllText(targetPath).Replace("#{PackageName}#", packageName));
        }
    }
}