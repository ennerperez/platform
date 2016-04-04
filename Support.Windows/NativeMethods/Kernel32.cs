using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Windows.Kernel32
{
    public static partial class NativeMethods
    {

        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Unicode, EntryPoint = "CopyMemory")]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint length);

        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Unicode)]
        public static extern int WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    }
}
