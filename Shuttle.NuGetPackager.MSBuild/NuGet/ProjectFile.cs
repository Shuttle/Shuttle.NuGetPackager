using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;

namespace Shuttle.NuGetPackager.MSBuild.NuGet
{
	public class ProjectFile
	{
		private static readonly Regex PackageReferenceExpression =
			new Regex(@"PackageReference\s*Include=""(?<package>.*?)""\s*Version=""(?<version>(?<major>\d+)\.(?<minor>\d+)\.(?<patch>\d+)(?<revision>\.\d+)?)[-\.]?(?<prerelease>.*)""",
			          RegexOptions.IgnoreCase);

		private readonly List<NugGetPackage> _packages = new List<NugGetPackage>();

		public ProjectFile(string path)
		{
		    if (!File.Exists(path))
		    {
                throw new InvalidOperationException($"File '{path}' does not exist.");
		    }

		    foreach (Match match in PackageReferenceExpression.Matches(File.ReadAllText(path)))
		    {
		        var packageName = match.Groups["package"];
		        var packageVersion = match.Groups["version"];

		        if (!packageName.Success || !packageVersion.Success)
		        {
		            continue;
		        }

		        AddPackage(new NugGetPackage(packageName.Value, packageVersion.Value));
		    }
		}

		public void AddPackage(NugGetPackage package)
		{
			var existing = _packages.Find(candidate => candidate.Name.Equals(package.Name, StringComparison.OrdinalIgnoreCase));

			if (existing == null)
			{
				_packages.Add(package);
			}
			else
			{
				if (StringComparer.OrdinalIgnoreCase.Compare(package.Version, existing.Version) > 0)
				{
					_packages.Remove(existing);
					_packages.Add(package);
				}
			}
		}

		public IEnumerable<NugGetPackage> Packages => new ReadOnlyCollection<NugGetPackage>(_packages);
	}
}