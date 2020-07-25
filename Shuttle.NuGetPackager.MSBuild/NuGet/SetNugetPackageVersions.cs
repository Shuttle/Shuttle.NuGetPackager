using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Shuttle.NuGetPackager.MSBuild.NuGet
{
    public class SetNuGetPackageVersions : Task
    {
        [Required] public ITaskItem[] Files { get; set; }

        [Required] public ITaskItem ProjectFile { get; set; }

        public string OpenTag { get; set; }
        public string CloseTag { get; set; }

        public override bool Execute()
        {
            var openTag = string.IsNullOrEmpty(OpenTag) ? "#{" : OpenTag;
            var closeTag = string.IsNullOrEmpty(CloseTag) ? "}#" : CloseTag;

            var projectFilePath = ProjectFile.ItemSpec;

            if (!Path.IsPathRooted(projectFilePath))
            {
                projectFilePath = Path.GetFullPath(projectFilePath);
            }

            if (!File.Exists(projectFilePath))
            {
                Log.LogError("ProjectFile '{0}' does not exist.", projectFilePath);

                return false;
            }

            var files = new List<FileContent>();

            foreach (var file in Files)
            {
                if (File.Exists(file.ItemSpec))
                {
                    files.Add(new FileContent(file.ItemSpec, File.ReadAllText(file.ItemSpec)));
                }
                else
                {
                    Log.LogWarning("File '{0}' does not exist.", file.ItemSpec);
                }
            }

            var projectFile = new ProjectFile(projectFilePath);
            var dependencies = new StringBuilder();
            var packageCount = projectFile.Packages.Count();
            var index = 1;

            foreach (var package in projectFile.Packages)
            {
                dependencies.Append(
                    $"      <dependency id=\"{package.Name}\" version=\"{package.Version}\" />{(index < packageCount ? Environment.NewLine : string.Empty)}");

                foreach (var file in files)
                {
                    file.Replace($"{openTag}{package.Name}-version{closeTag}", package.Version);
                }

                index++;
            }

            foreach (var file in files)
            {
                file.Replace($"{openTag}Dependencies{closeTag}", dependencies.ToString());

                File.WriteAllText(file.Path, file.Content);
            }

            return true;
        }

        private class FileContent
        {
            public FileContent(string path, string content)
            {
                Path = path;
                Content = content;
            }

            public string Path { get; }
            public string Content { get; private set; }

            public void Replace(string s, string r)
            {
                Content = Content.Replace(s, r);
            }
        }
    }
}