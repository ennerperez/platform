using System;
using System.Runtime.InteropServices;

namespace Platform.Support.Drawing
{
    internal static class NativeMethods
    {

        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "CopyMemory")]
        internal extern static void CopyMemory(IntPtr dest, IntPtr src, uint length);
    }
}
