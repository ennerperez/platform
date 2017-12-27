// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Windows
{
    /// <summary>
    /// Imports from Kernel32.dll
    /// </summary>
#if !INTEROP

    internal class Kernel32
#else

    public class Kernel32
#endif
    {
        /// <summary>
        /// Infinite timeout value
        /// </summary>
        public static readonly uint INFINITE = 0xFFFFFFFF;

        /// <summary>
        /// Invalid handle value
        /// </summary>
        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern bool SetDllDirectory(string pathName);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern uint SetErrorMode(uint uMode);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern void ExitProcess(
            UInt32 uExitCode
            );

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern bool TerminateProcess(
            IntPtr hProcess,
            UInt32 uExitCode
            );

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern IntPtr GetCurrentProcess();

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern uint GetCurrentProcessId();

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(
            IntPtr lpModuleName
            );

        [DllImport(ExternDll.Kernel32, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint GetModuleFileName(
            IntPtr hModule,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpFilename,
            uint nSize
            );

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern bool SetProcessWorkingSetSize(
            IntPtr hProcess,
            int dwMinimumWorkingSetSize,
            int dwMaximumWorkingSetSize
            );

        [DllImport(ExternDll.Kernel32)]
        public static extern bool Beep(int frequency, int duration);

        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Unicode)]
        private static extern int GetLongPathName(
            [In] string lpFileName,
            [Out] StringBuilder lpBuffer,
            [In] int nBufferLength);

        /// <summary>
        /// Converts a potentially short filename to a long filename.
        /// If the file does not exist, <c>null</c> is returned.
        /// </summary>
        public static string GetLongPathName(string fileName)
        {
            return GetLongPathName(fileName, MAX_PATH);
        }

        protected static string GetLongPathName(string fileName, int bufferLen)
        {
            StringBuilder buffer = new StringBuilder(bufferLen);
            int requiredBuffer = GetLongPathName(fileName, buffer, bufferLen);
            if (requiredBuffer == 0)
            {
                // error... probably file does not exist
                return null;
            }
            if (requiredBuffer > bufferLen)
            {
                // The buffer we used was not long enough... try again
                return GetLongPathName(fileName, requiredBuffer);
            }
            else
            {
                return buffer.ToString();
            }
        }

        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Unicode)]
        private static extern int GetShortPathName(
            [In] string lpFileName,
            [Out] StringBuilder lpBuffer,
            [In] int nBufferLength);

        /// <summary>
        /// Converts a potentially long filename to a short filename.
        /// If the file does not exist, <c>null</c> is returned.
        /// </summary>
        public static string GetShortPathName(string fileName)
        {
            return GetShortPathName(fileName, MAX_PATH);
        }

        protected static string GetShortPathName(string fileName, int bufferLen)
        {
            StringBuilder buffer = new StringBuilder(bufferLen);
            int requiredBuffer = GetShortPathName(fileName, buffer, bufferLen);
            if (requiredBuffer == 0)
            {
                // error... probably file does not exist
                return null;
            }
            if (requiredBuffer > bufferLen)
            {
                // The buffer we used was not long enough... try again
                return GetShortPathName(fileName, requiredBuffer);
            }
            else
            {
                return buffer.ToString();
            }
        }

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern bool SetThreadLocale(int lcid);

        // used to get supported code pages
        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool EnumSystemCodePages(CodePageDelegate lpCodePageEnumProc,
            uint dwFlags);

        public const uint CP_INSTALLED = 0x00000001;  // installed code page ids
        public const uint CP_SUPPORTED = 0x00000002;  // supported code page ids

        /// <summary>
        /// Delegate used for EnumSystemCodePages
        /// </summary>
        public delegate bool CodePageDelegate([MarshalAs(UnmanagedType.LPTStr)] string codePageName);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern IntPtr LocalFree(
            IntPtr hMem
            );

        /// <summary>
        /// Get the Terminal Services/Fast User Switching session ID
        /// for a process ID.
        /// </summary>
        [DllImport(ExternDll.Kernel32)]
        public static extern bool ProcessIdToSessionId(
            [In] uint dwProcessId,
            [Out] out uint pSessionId);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern IntPtr CreateFileMapping(
            IntPtr hFile,
            IntPtr lpAttributes,
            uint flProtect,
            uint dwMaximumSizeHigh,
            uint dwMaximumSizeLow,
            [In, MarshalAs(UnmanagedType.LPTStr)] string lpName);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern IntPtr MapViewOfFile(
            IntPtr hFileMappingObject,
            uint dwDesiredAccess,
            uint dwFileOffsetHigh,
            uint dwFileOffsetLow,
            uint dwNumberOfBytesToMap);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern void ZeroMemory(
            IntPtr Destination,
            uint Length);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern int UnmapViewOfFile(
            IntPtr lpBaseAddress);

        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ReplaceFile(
            [MarshalAs(UnmanagedType.LPTStr)] string lpReplacedFileName,
            [MarshalAs(UnmanagedType.LPTStr)] string lpReplacementFileName,
            [MarshalAs(UnmanagedType.LPTStr)] string lpBackupFileName,
            uint dwReplaceFlags,
            IntPtr lpExclude,
            IntPtr lpReserved);

        /// <summary>
        /// Get an error code for the last error on this thread.
        /// IntPtr can be converted to a 32 bit Int.
        /// </summary>
        [DllImport(ExternDll.Kernel32)]
        [Obsolete("Use Marshal.GetLastWin32Error() with [DllImport(SetLastError = true)] instead!", true)]
        public static extern uint GetLastError();

        /// <summary>
        /// Locks a global memory object and returns a pointer to the first byte
        /// of the object's memory block.
        /// </summary>
        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern IntPtr GlobalLock(IntPtr hMem);

        /// <summary>
        /// Decrements the lock count associated with the HGLOBAL memory block
        /// </summary>
        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern UIntPtr GlobalSize(IntPtr hMem);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

        [DllImport(ExternDll.Kernel32, EntryPoint = "RtlMoveMemory", SetLastError = true)]
        public static extern void CopyMemory(IntPtr Destination, IntPtr Source, UIntPtr Length);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern IntPtr CreateMutex(
            IntPtr lpMutexAttributes,
            int bInitialOwner,
            [MarshalAs(UnmanagedType.LPTStr)] string lpName
            );

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern int ReleaseMutex(
            IntPtr hMutex
            );

        [DllImport(ExternDll.Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateEvent(
            IntPtr lpEventAttributes,
            int bManualReset,
            int bInitialState,
            [MarshalAs(UnmanagedType.LPTStr)] string lpName);

        [DllImport(ExternDll.Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr OpenEvent(
            uint dwDesiredAccess,
            int bInheritHandle,
            [MarshalAs(UnmanagedType.LPTStr)] string lpName);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern int SetEvent(IntPtr hEvent);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern int ResetEvent(IntPtr hEvent);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern uint WaitForSingleObject(
            IntPtr hHandle,
            uint dwMilliseconds
            );

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern int CloseHandle(
            IntPtr hObject
            );

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern uint WaitForMultipleObjects(
            uint nCount,
            IntPtr[] pHandles,
            bool bWaitAll,
            uint dwMilliseconds
            );

        /// <summary>
        /// Get the drive type for a particular drive.
        /// </summary>
        [DllImport(ExternDll.Kernel32)]
        public static extern long GetDriveType(string driveLetter);

        /// <summary>
        /// Read a string from an INI file
        /// </summary>
        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpdefault,
            StringBuilder sbout,
            int nsize,
            string lpFileName);

        /// <summary>
        /// The drive types returned by GetDriveType
        /// </summary>
        public struct DRIVE
        {
            public const int UNKNOWN = 0;
            public const int NO_ROOT_DIR = 1;
            public const int REMOVABLE = 2;
            public const int FIXED = 3;
            public const int REMOTE = 4;
            public const int CDROM = 5;
            public const int RAMDISK = 6;
        }

        /// <summary>
        /// Get the unique ID of the currently executing thread
        /// </summary>
        [DllImport(ExternDll.Kernel32)]
        public static extern uint GetCurrentThreadId();

        /// <summary>
        /// Gets a temp file using a path and a prefix string.  Note that stringbuilder
        /// should be at least kernel32.MAX_PATH size. uUnique should be 0 to make unique
        /// temp file names, non zero to not necessarily create unique temp file names.
        /// </summary>
        [DllImport(ExternDll.Kernel32)]
        public static extern int GetTempFileName(
            string lpPathName,
            string lpPrefixString,
            Int32 uUnique,
            StringBuilder lpTempFileName);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern bool WriteFile(
            SafeFileHandle hFile, IntPtr lpBuffer,
            uint nNumberOfBytesToWrite, out uint lpNumberOfBytesWritten,
            IntPtr lpOverlapped);

        // The Max Path constant
        public const int MAX_PATH = 260;

        /// <summary>
        ///    The QueryPerformanceCounter function retrieves the current value of the high-resolution
        ///    performance counter.
        ///    </summary>
        [DllImport(ExternDll.Kernel32)]
        public static extern bool QueryPerformanceCounter(ref long performanceCount);

        /// <summary>
        ///    The QueryPerformanceFrequency function retrieves the frequency of the high-resolution
        ///    performance counter, if one exists. The frequency cannot change while the system is running.
        ///    </summary>
        [DllImport(ExternDll.Kernel32)]
        public static extern bool QueryPerformanceFrequency(ref long frequency);

        /// <summary>
        /// The GetTickCount function retrieves the number of milliseconds that have elapsed since the
        /// system was started. It is limited to the resolution of the system timer. To obtain the system
        /// timer resolution, use the GetSystemTimeAdjustment function.
        /// </summary>
        /// <remarks>
        /// The elapsed time is stored as a DWORD value. Therefore, the time will wrap around to zero if
        /// the system is run continuously for 49.7 days.
        /// </remarks>
        [DllImport(ExternDll.Kernel32)]
        public static extern uint GetTickCount();

        [DllImport(ExternDll.Kernel32)]
        public static extern bool FileTimeToLocalFileTime([In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime,
            out System.Runtime.InteropServices.ComTypes.FILETIME lpLocalFileTime);

        [DllImport(ExternDll.Kernel32)]
        public static extern bool LocalFileTimeToFileTime([In] ref System.Runtime.InteropServices.ComTypes.FILETIME lpLocalFileTime,
            out System.Runtime.InteropServices.ComTypes.FILETIME lpFileTime);

        [DllImport(ExternDll.Kernel32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport(ExternDll.Kernel32, SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport(ExternDll.Kernel32)]
        public static extern UIntPtr GetStdHandle(STDHandle stdHandle);

        [DllImport(ExternDll.Kernel32)]
        public static extern FileType GetFileType(UIntPtr hFile);

        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Auto)]
        public static extern int WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport(ExternDll.Kernel32, CharSet = CharSet.Auto, EntryPoint = "CopyMemory")]
        public extern static void CopyMemory(IntPtr dest, IntPtr src, uint length);

        [DllImport(ExternDll.Kernel32)]
        public extern static uint WTSGetActiveConsoleSessionId();
    }

#if !INTEROP

    internal enum STDHandle : uint
#else

    public enum STDHandle : uint
#endif
    {
        STD_INPUT_HANDLE = unchecked((uint)-10),
        STD_OUTPUT_HANDLE = unchecked((uint)-11),
        STD_ERROR_HANDLE = unchecked((uint)-12),
    }

#if !INTEROP

    internal enum FileType : uint
#else

    public enum FileType : uint
#endif
    {
        FILE_TYPE_UNKNOWN = 0x0000,
        FILE_TYPE_DISK = 0x0001,
        FILE_TYPE_CHAR = 0x0002,
        FILE_TYPE_PIPE = 0x0003,
        FILE_TYPE_REMOTE = 0x8000,
    }

#if !INTEROP

    internal class GlobalMemoryStatus
#else

    public class GlobalMemoryStatus
#endif
    {
        public GlobalMemoryStatus()
        {
            _memoryStatus.Length = Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            if (!GlobalMemoryStatusEx(ref _memoryStatus))
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        public int MemoryLoad { get { return _memoryStatus.MemoryLoad; } }
        public long TotalPhysical { get { return _memoryStatus.TotalPhysical; } }
        public long AvailablePhysical { get { return _memoryStatus.AvailablePhysical; } }
        public long TotalPageFile { get { return _memoryStatus.TotalPageFile; } }
        public long AvailablePageFile { get { return _memoryStatus.AvailablePageFile; } }
        public long TotalVirtual { get { return _memoryStatus.TotalVirtual; } }
        public long AvailableVirtual { get { return _memoryStatus.AvailableVirtual; } }
        public long AvailableExtendedVirtual { get { return _memoryStatus.AvailableExtendedVirtual; } }

        private struct MEMORYSTATUSEX
        {
            public int Length;
            public int MemoryLoad;
            public long TotalPhysical;
            public long AvailablePhysical;
            public long TotalPageFile;
            public long AvailablePageFile;
            public long TotalVirtual;
            public long AvailableVirtual;
            public long AvailableExtendedVirtual;
        }

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

        private MEMORYSTATUSEX _memoryStatus = new MEMORYSTATUSEX();
    }

    /// <summary>
    /// Disposable wrapper class for getting access to the contents of an HGLOBAL
    /// </summary>
#if !INTEROP

    internal class HGlobalLock : IDisposable
#else

    public class HGlobalLock : IDisposable
#endif
    {
        /// <summary>
        /// Initialize by locking the HGLOBAL
        /// </summary>
        /// <param name="hGlobal">HGLOBAL to lock and then access</param>
        public HGlobalLock(IntPtr hGlobal)
        {
            this.hGlobal = hGlobal;
            Lock();
        }

        /// <summary>
        /// Unlock on dispose
        /// </summary>
        public void Dispose()
        {
            Unlock();
        }

        /// <summary>
        /// Underlying memory pointed to by the hGlobal
        /// </summary>
        public IntPtr Memory
        {
            get
            {
                Debug.Assert(pData != IntPtr.Zero);
                return pData;
            }
        }

        /// <summary>
        /// Get the size of of the locked global memory handle
        /// </summary>
        public UIntPtr Size
        {
            get
            {
                UIntPtr size = Kernel32.GlobalSize(hGlobal);
                if (size == UIntPtr.Zero)
                {
                    throw new Win32Exception(
                        Marshal.GetLastWin32Error(), "Unexpected error calling GlobalSize");
                }
                return size;
            }
        }

        /// <summary>
        /// Lock the HGLOBAL so as to access the underlying memory block
        /// </summary>
        public void Lock()
        {
            Debug.Assert(pData == IntPtr.Zero);
            pData = Kernel32.GlobalLock(hGlobal);
            if (pData == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(),
                    "Error occurred while trying to lock global memory");
            }
        }

        /// <summary>
        /// Unlock the HGLOBAL
        /// </summary>
        public void Unlock()
        {
            if (pData != IntPtr.Zero)
            {
                bool success = Kernel32.GlobalUnlock(hGlobal);
                int lastError = Marshal.GetLastWin32Error();
                if (!success && lastError != ERROR.SUCCESS)
                {
                    throw new Win32Exception(lastError,
                         "Unexpected error occurred calling GlobalUnlock");
                }
                pData = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Clone the underlying memory and return a new HGLOBAL that references
        /// the cloned memory (this HGLOBAL will need to be freed using GlobalFree)
        /// </summary>
        /// <returns>cloned memory block</returns>
        public IntPtr Clone()
        {
            // allocate output memory
            IntPtr hglobOut = Kernel32.GlobalAlloc(GMEM.FIXED, Size);
            if (hglobOut == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(),
                    "Unexpected error occurred calling GlobalAlloc");
            }

            // got the memory, copy into it and return it
            Kernel32.CopyMemory(hglobOut, Memory, Size);
            return hglobOut;
        }

        /// <summary>
        /// Underlying HGLOBAL
        /// </summary>
        private IntPtr hGlobal = IntPtr.Zero;

        /// <summary>
        /// Pointer to data (acquired by locking the HGLOBAL)
        /// </summary>
        private IntPtr pData = IntPtr.Zero;
    }

    /// <summary>
    /// SetErrorMode flags
    /// </summary>
#if !INTEROP

    internal struct SEM
#else

    public struct SEM
#endif
    {
        public const uint FAILCRITICALERRORS = 0x0001;
        public const uint NOGPFAULTERRORBOX = 0x0002;
        public const uint NOALIGNMENTFAULTEXCEPT = 0x0004;
        public const uint NOOPENFILEERRORBOX = 0x8000;
    }

    /// <summary>
    /// ReplaceFile flags
    /// </summary>
#if !INTEROP

    internal struct REPLACEFILE
#else

    public struct REPLACEFILE
#endif
    {
        public const uint WRITE_THROUGH = 0x00000001;
        public const uint IGNORE_MERGE_ERRORS = 0x00000002;
    }

    /// <summary>
    /// Thread priority constants
    /// </summary>
#if !INTEROP

    internal struct THREAD_PRIORITY
#else

    public struct THREAD_PRIORITY
#endif
    {
        public const int NORMAL = 0;
    }

#if !INTEROP

    internal struct GMEM
#else

    public struct GMEM
#endif
    {
        public const uint FIXED = 0x0000;
        public const uint MOVEABLE = 0x0002;
        public const uint ZEROINIT = 0x0040;
        public const uint SHARE = 0x2000;
    }

#if !INTEROP

    internal struct PAGE
#else

    public struct PAGE
#endif
    {
        public const uint READONLY = 0x02;
        public const uint READWRITE = 0x04;
        public const uint WRITECOPY = 0x08;
    }

#if !INTEROP

    internal struct SEC
#else

    public struct SEC
#endif
    {
        public const uint IMAGE = 0x1000000;
        public const uint RESERVE = 0x4000000;
        public const uint COMMIT = 0x8000000;
        public const uint NOCACHE = 0x10000000;
    }

#if !INTEROP

    internal struct FILE_MAP
#else

    public struct FILE_MAP
#endif
    {
        public const uint COPY = 0x0001;
        public const uint WRITE = 0x0002;
        public const uint READ = 0x0004;
    }

#if !INTEROP

    internal struct FILE_ATTRIBUTE
#else

    public struct FILE_ATTRIBUTE
#endif
    {
        public const uint READONLY = 0x00000001;
        public const uint HIDDEN = 0x00000002;
        public const uint SYSTEM = 0x00000004;
        public const uint DIRECTORY = 0x00000010;
        public const uint ARCHIVE = 0x00000020;
        public const uint DEVICE = 0x00000040;
        public const uint NORMAL = 0x00000080;
        public const uint TEMPORARY = 0x00000100;
    }

    /// <summary>
    /// The SECURITY_ATTRIBUTES structure contains the security descriptor for
    /// an object and specifies whether the handle retrieved by specifying this
    /// structure is inheritable
    /// </summary>
#if !INTEROP

    internal struct SECURITY_ATTRIBUTES
#else

    public struct SECURITY_ATTRIBUTES
#endif
    {
#pragma warning disable CS0649

        /// <summary>
        /// Specifies the size, in bytes, of this structure. Set this value to
        /// the size of the SECURITY_ATTRIBUTES structure.
        /// </summary>
        public uint nLength;

        /// <summary>
        /// Pointer to a security descriptor for the object that controls the
        /// sharing of it. If NULL is specified for this member, the object
        /// is assigned the default security descriptor of the calling process.
        /// </summary>
        public IntPtr lpSecurityDescriptor;

        /// <summary>
        /// Specifies whether the returned handle is inherited when a new
        /// process is created. If this member is TRUE, the new process inherits
        /// the handle.
        /// </summary>
        [MarshalAs(UnmanagedType.Bool)]
        public bool bInheritHandle;

#pragma warning restore CS0649
    }

    /// <summary>
    /// Wait constants used in synchronization functions
    /// </summary>
#if !INTEROP

    internal struct WAIT
#else

    public struct WAIT
#endif
    {
        public const uint FAILED = 0xFFFFFFFF;
        public const uint OBJECT_0 = 0x00000000;
        public const uint ABANDONED = 0x00000080;
        public const uint TIMEOUT = 258;
    }

    /// <summary>
    /// Constants used for getting access to synchronization events
    /// </summary>
#if !INTEROP

    internal struct EVENT
#else

    public struct EVENT
#endif
    {
        public const uint MODIFY_STATE = 0x0002;
    }
}