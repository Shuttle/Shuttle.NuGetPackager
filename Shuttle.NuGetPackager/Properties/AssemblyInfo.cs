﻿using System.Reflection;
using System.Runtime.InteropServices;

#if NETFRAMEWORK
[assembly: AssemblyTitle(".NET Framework")]
#endif

#if NETCOREAPP
[assembly: AssemblyTitle(".NET Core")]
#endif

#if NETSTANDARD
[assembly: AssemblyTitle(".NET Standard")]
#endif

[assembly: AssemblyVersion("1.2.0.0")]
[assembly: AssemblyCopyright("Copyright © Shuttle 2020")]
[assembly: AssemblyProduct("Shuttle.NuGetPackager")]
[assembly: AssemblyCompany("Shuttle")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyInformationalVersion("1.0.0")]
[assembly: ComVisible(false)]
