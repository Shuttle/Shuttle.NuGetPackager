namespace Shuttle.NuGetPackager.MSBuild.NuGet
{
    public class NugGetPackage
    {
        public NugGetPackage(string name, string version)
        {
            Name = name;
            Version = version;
        }

        public string Name { get; }
        public string Version { get; }
    }
}