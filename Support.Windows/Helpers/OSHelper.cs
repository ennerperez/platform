using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Platform.Support.Windows
{
    public static class OSHelper
    {

#if (!PORTABLE)

        /// <summary>
        ///  returns 32 if windows has installed in 32 bit mode and 64 for 64 bit mode
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough()]
        public static int WindowsArchitecture()
        {
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Hardware\\Description\\System\\CentralProcessor\\0");
            if (rk.GetValue("Identifier", "x86").ToString().Contains("x86"))
                return 32;
            else
                return 64;
        }

#endif

        /// <summary>
        /// Gets whether the running operating system is Windows Vista or a more recent version.
        /// </summary>
        public static bool IsVistaOrBetter
        {
            get { return (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6); }
        }

        /// <summary>
        /// Gets whether the running operating system is Windows Seven or a more recent version.
        /// </summary>
        public static bool IsSevenOrBetter
        {
            get
            {
                if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                    return false;

                if (Environment.OSVersion.Version.Major < 6)
                    return false;
                else if (Environment.OSVersion.Version.Major == 6)
                    return (Environment.OSVersion.Version.Minor >= 1);
                else
                    return true;
            }
        }

    }
}
