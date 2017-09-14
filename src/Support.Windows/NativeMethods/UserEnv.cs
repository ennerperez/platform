using System;
using System.Runtime.InteropServices;

namespace Platform.Support.Windows
{
    public class UserEnv
    {

        [DllImport(ExternDll.UserEnv, SetLastError = true)]
        public static extern bool CreateEnvironmentBlock(ref IntPtr lpEnvironment, IntPtr hToken, bool bInherit);

        [DllImport(ExternDll.UserEnv, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyEnvironmentBlock(IntPtr lpEnvironment);

    }
}
