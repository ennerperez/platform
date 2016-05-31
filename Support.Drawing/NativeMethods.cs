using System;
using System.Runtime.InteropServices;

internal static class ExternDll
{
    internal const string Kernel32 = "kernel32.dll";
}

internal static class NativeMethods
{

    [DllImport(ExternDll.Kernel32, CharSet = CharSet.Unicode, EntryPoint = "CopyMemory")]
    internal extern static void CopyMemory(IntPtr dest, IntPtr src, uint length);
}
