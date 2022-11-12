using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Moq;
using NUnit.Framework;
using Shuttle.NuGetPackager.MSBuild.NuGet;

namespace Shuttle.NuGetPackager.Tests
{
    [TestFixture]
    public class SetNuGetPackageVersionsFixture
    {
        [Test]
        public void Should_be_able_to_set_nuget_dependency_versions_using_custom_values()
        {
            var task = new SetNuGetPackageVersions
            {
                BuildEngine = new Mock<IBuildEngine>().Object,
                OpenTag = "%",
                CloseTag = "%",
                ProjectFile = new TaskItem(FilePathExtensions.BasePath(@".\files\packages.csproj")),
                Files = new List<ITaskItem>
                {
                    new TaskItem(FilePathExtensions.BasePath(@".\files\set-nuget-dependency-versions-custom-test.txt"))
                }.ToArray()
            };

            File.Copy(FilePathExtensions.BasePath(@".\files\set-nuget-dependency-versions-custom.txt"),
                FilePathExtensions.BasePath(@".\files\set-nuget-dependency-versions-custom-test.txt"), true);

            Assert.True(task.Execute());

            var contents = File.ReadAllText(FilePathExtensions.BasePath(@".\files\set-nuget-dependency-versions-custom-test.txt"));

            Assert.IsTrue(contents.Contains("<dependency id=\"another.package\" version=\"[3.2.1]\" />"));
            Assert.IsTrue(contents.Contains("<dependency id=\"some-package\" version=\"[1.2.3]\" />"));
            Assert.IsTrue(contents.Contains("<dependency id=\"prerelease-package-a\" version=\"[4.5.6-rc1]\" />"));
            Assert.IsTrue(contents.Contains("<dependency id=\"prerelease-package-b\" version=\"[4.5.6-rc1.15]\" />"));
            Assert.IsTrue(contents.Contains("<dependency id=\"full-version.package\" version=\"[4.5.6.7000]\" />"));

            File.Delete(FilePathExtensions.BasePath(@".\files\set-nuget-dependency-versions-custom-test.txt"));
        }

        [Test]
        public void Should_be_able_to_set_nuget_dependency_versions_using_defaults()
        {
            var task = new SetNuGetPackageVersions
            {
                BuildEngine = new Mock<IBuildEngine>().Object,
                ProjectFile = new TaskItem(FilePathExtensions.BasePath(@".\files\packages.csproj")),
                Files = new List<ITaskItem>
                {
                    new TaskItem(FilePathExtensions.BasePath(@".\files\set-nuget-dependency-versions-default-test.txt"))
                }.ToArray()
            };

            File.Copy(FilePathExtensions.BasePath(@".\files\set-nuget-dependency-versions-default.txt"),
                FilePathExtensions.BasePath(@".\files\set-nuget-dependency-versions-default-test.txt"), true);

            Assert.True(task.Execute());

            var contents = File.ReadAllText(FilePathExtensions.BasePath(@".\files\set-nuget-dependency-versions-default-test.txt"));

            Assert.IsTrue(contents.Contains("<dependency id=\"another.package\" version=\"[3.2.1]\" />"));
            Assert.IsTrue(contents.Contains("<dependency id=\"some-package\" version=\"[1.2.3]\" />"));
            Assert.IsTrue(contents.Contains("<dependency id=\"prerelease-package-a\" version=\"[4.5.6-rc1]\" />"));
            Assert.IsTrue(contents.Contains("<dependency id=\"prerelease-package-b\" version=\"[4.5.6-rc1.15]\" />"));
            Assert.IsTrue(contents.Contains("<dependency id=\"full-version.package\" version=\"[4.5.6.7000]\" />"));

            File.Delete(FilePathExtensions.BasePath(@".\files\set-nuget-dependency-versions-default-test.txt"));
        }
    }
}