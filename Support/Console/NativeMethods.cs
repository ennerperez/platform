using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{

    using System.Runtime.InteropServices;

#pragma warning disable 0436
    internal interface IProgress<T>
    {
        void Report(T value);
    }

    public enum FileType : uint
    {
        FILE_TYPE_UNKNOWN = 0x0000,
        FILE_TYPE_DISK = 0x0001,
        FILE_TYPE_CHAR = 0x0002,
        FILE_TYPE_PIPE = 0x0003,
        FILE_TYPE_REMOTE = 0x8000,
    }

    public enum STDHandle : uint
    {
        STD_INPUT_HANDLE = unchecked((uint)-10),
        STD_OUTPUT_HANDLE = unchecked((uint)-11),
        STD_ERROR_HANDLE = unchecked((uint)-12),
    }
}

namespace Platform.Support.ConsoleEx
{
    internal static class NativeMethods
    {
        [DllImport("Kernel32.dll")]
        static public extern UIntPtr GetStdHandle(STDHandle stdHandle);
        [DllImport("Kernel32.dll")]
        static public extern FileType GetFileType(UIntPtr hFile);

    }

}
