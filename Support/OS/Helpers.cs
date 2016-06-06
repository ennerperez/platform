using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Platform.Support.OS
{
    public static partial class Helpers
    {

        /// <summary>
        /// returns 32 for 32 bit systems 64 for 64 bit systems
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough()]
        public static int EnvironmentArchitecture()
        {
            string pa = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            return (string.IsNullOrEmpty(pa) | string.Compare(pa, 0, "x86", 0, 3, true) == 0 ? 32 : 64);
        }

        /// <summary>
        ///  returns 32 if runs in 32 bit mode and 64 for 64 bit mode
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        [DebuggerStepThrough()]
        public static int Architecture()
        {
            return (IntPtr.Size == 8 ? 64 : 32);
        }

        /// <summary>
        /// Returns if running OS is Windows
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough()]
        public static bool IsWindows()
        {
            int p = (int)Environment.OSVersion.Platform;
            return (p != 4) && (p != 6) && (p != 128);
        }

        /// <summary>
        /// Returns if running OS is Linux
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough()]
        public static bool IsLinux()
        {
            int p = (int)Environment.OSVersion.Platform;
            return (p == 4) || (p == 6) || (p == 128);
        }

    }
}
