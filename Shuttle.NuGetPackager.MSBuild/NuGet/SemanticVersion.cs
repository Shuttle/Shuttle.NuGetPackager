using System;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Shuttle.NuGetPackager.MSBuild.NuGet
{
    public class SemanticVersion : Task
    {
        public class Parser
        {
            private readonly Regex _expression =
                new Regex(@"^(?<major>0|[1-9]\d*)\.(?<minor>0|[1-9]\d*)\.(?<patch>0|[1-9]\d*)(?:-(?<prerelease>(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+(?<buildmetadata>[0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$");

            public int Major { get; }
            public int Minor { get; }
            public int Patch { get; }
            public string VersionCore => $"{Major}.{Minor}.{Patch}";
            public string Prerelease { get; } = string.Empty;
            public string BuildMetadata { get; } = string.Empty;
            public string Version => $"{VersionCore}{(!string.IsNullOrWhiteSpace(Prerelease) ? $"-{Prerelease}" : string.Empty)}{(!string.IsNullOrWhiteSpace(BuildMetadata) ? $"+{BuildMetadata}" : string.Empty)}";

            public Parser(string value)
            {
                var exception = new ArgumentException($"Argument '{nameof(value)}' is not a valid semantic version.");

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw exception;
                }

                var match = _expression.Match(value);

                if (!match.Success)
                {
                    throw exception;
                }

                Major = int.Parse(match.Groups["major"].Value);
                Minor = int.Parse(match.Groups["minor"].Value);
                Patch = int.Parse(match.Groups["patch"].Value);

                if (match.Groups["buildmetadata"].Success)
                {
                    BuildMetadata = match.Groups["buildmetadata"].Value;
                }

                if (match.Groups["prerelease"].Success)
                {
                    Prerelease = match.Groups["prerelease"].Value;
                }
            }
        }

        [Required]
        public string Value { get; set; }

        [Output]
        public string VersionCore { get; private set; }

        [Output]
        public string Version { get; private set; }

        [Output]
        public string Prerelease { get; private set; }

        [Output]
        public string BuildMetadata { get; private set; }

        public override bool Execute()
        {
            var parser = new Parser(Value);

            Version = parser.Version;
            VersionCore = parser.VersionCore;
            Prerelease = parser.Prerelease;
            BuildMetadata = parser.BuildMetadata;

            return true;
        }
    }
}