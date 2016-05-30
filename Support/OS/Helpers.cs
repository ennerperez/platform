using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Platform.Support.OS
{
    public static class Helpers
    {

        /// <summary>
        /// returns 32 for 32 bit systems 64 for 64 bit systems
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough()]
        public static int ArchitectureLegacy()
        {
            string pa = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            return (string.IsNullOrEmpty(pa) | string.Compare(pa, 0, "x86", 0, 3, true) == 0 ? 32 : 64);
        }

        /// <summary>
        ///  returns 32 if windows runs in 32 bit mode and 64 for 64 bit mode
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough()]
        public static int RunningMode()
        {
            if (IntPtr.Size == 8)
            {
                return 64;
            }
            else
            {
                return 32;
            }
        }

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
            {
                return 32;
            }
            else
            {
                return 64;
            }
        }

#endif

        /// <summary>
        /// Returns if 
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows()
        {
            int p = (int)Environment.OSVersion.Platform;
            return (p != 4) && (p != 6) && (p != 128);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsLinux()
        {
            int p = (int)Environment.OSVersion.Platform;
            return (p == 4) || (p == 6) || (p == 128);
        }

    }
}
