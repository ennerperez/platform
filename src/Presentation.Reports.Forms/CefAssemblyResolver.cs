using CefSharp;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Platform.Presentation.Reports
{
    public static class CefAssemblyResolve
    {
        private static bool resolved;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Resolve()
        {
            if (resolved)
                return;

            AppDomain.CurrentDomain.AssemblyLoad += Loader;
            AppDomain.CurrentDomain.AssemblyResolve += Resolver;

            resolved = true;
        }

        internal static void Loader(object sender, AssemblyLoadEventArgs args)
        {
            if (args.LoadedAssembly.GetName().Name.StartsWith("CefSharp"))
            {
                //Perform dependency check to make sure all relevant resources are in our output directory.
                var settings = new CefSettings
                {
                    BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                   Environment.Is64BitProcess ? "x64" : "x86",
                                                   "CefSharp.BrowserSubprocess.exe")
                };

                Cef.Initialize(settings, true, false);//, browserProcessHandler: null);
            }
        }

        // Will attempt to load missing assembly from either x86 or x64 subdir
        internal static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("CefSharp"))
            {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                       Environment.Is64BitProcess ? "x64" : "x86",
                                                       assemblyName);

                return File.Exists(archSpecificPath)
                           ? Assembly.LoadFile(archSpecificPath)
                           : null;
            }

            return null;
        }
    }
}