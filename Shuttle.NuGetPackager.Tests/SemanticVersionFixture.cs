using NUnit.Framework;
using Shuttle.NuGetPackager.MSBuild.NuGet;

namespace Shuttle.NuGetPackager.Tests
{
    [TestFixture]
    public class SemanticVersionFixture
    {
        [Test]
        [TestCase("1.2.3", 1, 2, 3, "1.2.3", "", "")]
        [TestCase("1.2.3+4", 1, 2, 3, "1.2.3", "", "4")]
        [TestCase("1.2.3-alpha1", 1, 2, 3, "1.2.3", "alpha1", "")]
        [TestCase("1.2.3-alpha1+4", 1, 2, 3, "1.2.3", "alpha1", "4")]
        [TestCase("1.2.3-alpha1.4", 1, 2, 3, "1.2.3", "alpha1.4", "")]
        [TestCase("1.2.3-alpha1.4+5", 1, 2, 3, "1.2.3", "alpha1.4", "5")]
        [TestCase("1.2.3-beta.23+abc", 1, 2, 3, "1.2.3", "beta.23", "abc")]
        public void Should_be_able_to_parse(string value, int major, int minor, int patch, string versionCore, string prerelease, string buildMetadata)
        {
            SemanticVersion.Parser parser = null;

            Assert.That(() => parser = new SemanticVersion.Parser(value), Throws.Nothing);

            Assert.That(parser.Major, Is.EqualTo(major));
            Assert.That(parser.Minor, Is.EqualTo(minor));
            Assert.That(parser.Patch, Is.EqualTo(patch));
            Assert.That(parser.BuildMetadata, Is.EqualTo(buildMetadata));
            Assert.That(parser.VersionCore, Is.EqualTo(versionCore));
            Assert.That(parser.Prerelease, Is.EqualTo(prerelease));
            Assert.That(parser.Version, Is.EqualTo(value));

            var semanticVersion = new SemanticVersion { Value = value };

            Assert.That(() => semanticVersion.Execute(), Throws.Nothing);

            Assert.That(semanticVersion.BuildMetadata, Is.EqualTo(buildMetadata));
            Assert.That(semanticVersion.VersionCore, Is.EqualTo(versionCore));
            Assert.That(semanticVersion.Prerelease, Is.EqualTo(prerelease));
            Assert.That(semanticVersion.Version, Is.EqualTo(value));
        }
    }
}