using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace Shuttle.NuGetPackager
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class ConfigureProjectPackage : AsyncPackage
    {
        public const string PackageGuidString = "106458ed-d4dc-4300-88d3-21a569a60995";

        protected override async Task InitializeAsync(CancellationToken cancellationToken,
            IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await ConfigureProjectCommand.InitializeAsync(this);
        }
    }
}