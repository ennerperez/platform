//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;

//internal static class ExternDll
//{
//    internal const string Kernel32 = "kernel32.dll";
//}

//internal static class NativeMethods
//{

//#pragma warning disable 0436
//    internal interface IProgress<T>
//    {
//        void Report(T value);
//    }

//    internal enum FileType : uint
//    {
//        FILE_TYPE_UNKNOWN = 0x0000,
//        FILE_TYPE_DISK = 0x0001,
//        FILE_TYPE_CHAR = 0x0002,
//        FILE_TYPE_PIPE = 0x0003,
//        FILE_TYPE_REMOTE = 0x8000,
//    }

//    internal enum STDHandle : uint
//    {
//        STD_INPUT_HANDLE = unchecked((uint)-10),
//        STD_OUTPUT_HANDLE = unchecked((uint)-11),
//        STD_ERROR_HANDLE = unchecked((uint)-12),
//    }

//    [DllImport(ExternDll.Kernel32)]
//    internal static extern UIntPtr GetStdHandle(STDHandle stdHandle);
//    [DllImport(ExternDll.Kernel32)]
//    internal static extern FileType GetFileType(UIntPtr hFile);

//    [DllImport(ExternDll.Kernel32, CharSet = CharSet.Unicode)]
//    internal static extern int WritePrivateProfileString(string section, string key, string val, string filePath);
//    [DllImport(ExternDll.Kernel32, CharSet = CharSet.Unicode)]
//    internal static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
//}
