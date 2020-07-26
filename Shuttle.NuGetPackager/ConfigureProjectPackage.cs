using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace Shuttle.NuGetPackager.VSIX
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideService(typeof(ConfigureProjectPackage), IsAsyncQueryable = true)]
    [ProvideAutoLoad(PackageGuidString, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class ConfigureProjectPackage : AsyncPackage
    {
        public const string PackageGuidString = "64130770-3ABC-491E-B331-CB2A3D283AD0";

        protected override Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            ConfigureProject.Initialize(this);

            return base.InitializeAsync(cancellationToken, progress);
        }
    }
}