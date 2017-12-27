using Microsoft.Win32;
using System;
using System.Diagnostics;

namespace Platform.Support.Windows
{
    [DebuggerStepThrough()]
    public static class OSHelper
    {
        internal static Version OSVer = System.Environment.OSVersion.Version;

        /// <summary>
        ///  returns 32 if windows has installed in 32 bit mode and 64 for 64 bit mode
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static int WindowsArchitecture()
        {
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Hardware\\Description\\System\\CentralProcessor\\0");
            if (rk.GetValue("Identifier", "x86").ToString().Contains("x86"))
                return 32;
            else
                return 64;
        }

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

        public static bool IsWindows81()
        {
            return OSVer.Major == 6 && OSVer.Minor == 3;
        }

        public static bool IsWindows80()
        {
            return OSVer.Major == 6 && OSVer.Minor == 2;
        }

        public static bool IsWindows7SP1OrGreater()
        {
            return OSVer.Major > 6 || (OSVer.Major == 6 && OSVer.Minor >= 2) || (OSVer.Major == 6 && OSVer.Minor == 1 && System.Environment.OSVersion.ServicePack != string.Empty);
        }

        public static bool IsWindows7SP1()
        {
            return OSVer.Major == 6 && OSVer.Minor == 1 && System.Environment.OSVersion.ServicePack != string.Empty;
        }

        //TODO: Any Framework detection as param
        public static bool NetFx45orGreater()
        {
            bool result;
            try
            {
                using (RegistryKey registryKey = RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, string.Empty).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\"))
                {
                    RegistryKey registryKey2 = registryKey.OpenSubKey("v4\\full");
                    if (registryKey2 == null)
                        result = false;
                    else
                    {
                        object value = registryKey2.GetValue("release", null);
                        if (value == null)
                            result = false;
                        else if (int.Parse(value.ToString()) < 378389)
                            result = false;
                        else
                            result = true;
                    }
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        //TODO: Any KB detection as param
        public static bool DetectKB2919355()
        {
            int num = 112;
            bool result;
            try
            {
                using (RegistryKey registryKey = RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, string.Empty).OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Component Based Servicing\\Packages\\"))
                {
                    RegistryKey registryKey2 = registryKey.OpenSubKey("Package_for_KB2919355~31bf3856ad364e35~amd64~~6.3.1.14");
                    if (registryKey2 == null)
                    {
                        registryKey2 = registryKey.OpenSubKey("Package_for_KB2919355~31bf3856ad364e35~x86~~6.3.1.14");
                        if (registryKey2 == null)
                            return false;
                    }
                    object value = registryKey2.GetValue("currentstate", null);
                    if (value == null)
                        result = false;
                    else if (int.Parse(value.ToString()) != num)
                        result = false;
                    else
                        result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool HasPendingRebot()
        {
            using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager"))
                if (registryKey != null && registryKey.GetValue("PendingFileRenameOperations") != null)
                    return true;
            bool result;
            using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\WindowsUpdate\\Auto Update\\RebootRequired"))
                result = (registryKey2 != null && registryKey2.GetValueNames().Length != 0);
            return result;
        }

        public static void DeleteFileOnReboot(string filePath)
        {
            Kernel32.MoveFileEx(filePath, null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
        }
    }
}