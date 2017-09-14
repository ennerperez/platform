// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace Platform.Support.Windows
{

    public class Advapi32
    {
        [DllImport(ExternDll.Advapi32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int RegOpenKeyEx(
            UIntPtr hKey,
            [MarshalAs(UnmanagedType.LPTStr)] string lpSubKey,
            uint ulOptions,
            uint samDesired,
            out UIntPtr phkResult
            );

        [DllImport(ExternDll.Advapi32, SetLastError = true)]
        public static extern int RegCloseKey(
            UIntPtr hKey
            );

        [DllImport(ExternDll.Advapi32, SetLastError = true)]
        public static extern int RegNotifyChangeKeyValue(
            UIntPtr hKey,
            bool bWatchSubtree,
            uint dwNotifyFilter,
            SafeWaitHandle hEvent,
            bool fAsynchronous
            );

        [DllImport(ExternDll.Advapi32, EntryPoint = "CreateProcessAsUser", SetLastError = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CreateProcessAsUser(
            IntPtr hToken,
            String lpApplicationName,
            String lpCommandLine,
            IntPtr lpProcessAttributes,
            IntPtr lpThreadAttributes,
            bool bInheritHandle,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            String lpCurrentDirectory,
            ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation
            );

        [DllImport(ExternDll.Advapi32, EntryPoint = "DuplicateTokenEx")]
        public static extern bool DuplicateTokenEx(
            IntPtr ExistingTokenHandle,
            uint dwDesiredAccess,
            IntPtr lpThreadAttributes,
            int TokenType,
            int ImpersonationLevel,
            ref IntPtr DuplicateTokenHandle);


        #region Win32 Constants

        private const int CREATE_UNICODE_ENVIRONMENT = 0x00000400;
        private const int CREATE_NO_WINDOW = 0x08000000;

        private const int CREATE_NEW_CONSOLE = 0x00000010;

        private const uint INVALID_SESSION_ID = 0xFFFFFFFF;
        private static readonly IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

        #endregion Win32 Constants

        /// <summary>
        /// Gets the user token from the currently active session
        /// </summary>
        /// <param name="phUserToken"></param>
        /// <returns></returns>
        public static bool GetSessionUserToken(ref IntPtr phUserToken)
        {
            var bResult = false;
            var hImpersonationToken = IntPtr.Zero;
            var activeSessionId = INVALID_SESSION_ID;
            var pSessionInfo = IntPtr.Zero;
            var sessionCount = 0;

            // Get a handle to the user access token for the current active session.
            if (Wtsapi32.WTSEnumerateSessions(WTS_CURRENT_SERVER_HANDLE, 0, 1, ref pSessionInfo, ref sessionCount) != 0)
            {
                var arrayElementSize = Marshal.SizeOf(typeof(WTS_SESSION_INFO));
                var current = pSessionInfo;

                for (var i = 0; i < sessionCount; i++)
                {
                    var si = (WTS_SESSION_INFO)Marshal.PtrToStructure((IntPtr)current, typeof(WTS_SESSION_INFO));
                    current += arrayElementSize;

                    if (si.State == WTS_CONNECTSTATE_CLASS.WTSActive)
                    {
                        activeSessionId = si.SessionID;
                    }
                }
            }

            // If enumerating did not work, fall back to the old method
            if (activeSessionId == INVALID_SESSION_ID)
            {
                activeSessionId = Kernel32.WTSGetActiveConsoleSessionId();
            }

            if (Wtsapi32.WTSQueryUserToken(activeSessionId, ref hImpersonationToken) != 0)
            {
                // Convert the impersonation token to a primary token
                bResult = Advapi32.DuplicateTokenEx(hImpersonationToken, 0, IntPtr.Zero,
                    (int)SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, (int)TOKEN_TYPE.TokenPrimary,
                    ref phUserToken);

                Kernel32.CloseHandle(hImpersonationToken);
            }

            return bResult;
        }

        public static bool StartProcessAsCurrentUser(string appPath, out uint pID, string cmdLine = null, string workDir = null, bool visible = true)
        {
            var hUserToken = IntPtr.Zero;
            var startInfo = new STARTUPINFO();
            var procInfo = new PROCESS_INFORMATION();
            var pEnv = IntPtr.Zero;
            int iResultOfCreateProcessAsUser;

            startInfo.cb = Marshal.SizeOf(typeof(STARTUPINFO));

            try
            {
                if (!GetSessionUserToken(ref hUserToken))
                {
                    throw new Exception("StartProcessAsCurrentUser: GetSessionUserToken failed.");
                }

                uint dwCreationFlags = CREATE_UNICODE_ENVIRONMENT | (uint)(visible ? CREATE_NEW_CONSOLE : CREATE_NO_WINDOW);
                startInfo.wShowWindow = (short)(visible ? SW.SHOW : SW.HIDE);
                startInfo.lpDesktop = "winsta0\\default";

                if (!UserEnv.CreateEnvironmentBlock(ref pEnv, hUserToken, false))
                {
                    throw new Exception("StartProcessAsCurrentUser: CreateEnvironmentBlock failed.");
                }

                if (!Advapi32.CreateProcessAsUser(hUserToken,
                    appPath, // Application Name
                    cmdLine, // Command Line
                    IntPtr.Zero,
                    IntPtr.Zero,
                    false,
                    dwCreationFlags,
                    pEnv,
                    workDir, // Working directory
                    ref startInfo,
                    out procInfo))
                {
                    iResultOfCreateProcessAsUser = Marshal.GetLastWin32Error();
                    throw new Exception("StartProcessAsCurrentUser: CreateProcessAsUser failed.  Error Code -" + iResultOfCreateProcessAsUser);
                }

                pID = procInfo.dwProcessId;

                iResultOfCreateProcessAsUser = Marshal.GetLastWin32Error();
            }
            finally
            {
                Kernel32.CloseHandle(hUserToken);
                if (pEnv != IntPtr.Zero)
                {
                    UserEnv.DestroyEnvironmentBlock(pEnv);
                }
                Kernel32.CloseHandle(procInfo.hThread);
                Kernel32.CloseHandle(procInfo.hProcess);
            }

            return true;
        }

    }

    public struct STANDARD_RIGHTS
    {
        public const uint READ = 0x00020000;
        public const uint SYNCHRONIZE = 0x00100000;
    }

    public struct KEY
    {
        public const uint QUERY_VALUE = 0x0001;
        public const uint ENUMERATE_SUB_KEYS = 0x0008;
        public const uint NOTIFY = 0x0010;
        public const uint READ = (STANDARD_RIGHTS.READ | KEY.QUERY_VALUE | KEY.ENUMERATE_SUB_KEYS | KEY.NOTIFY) & ~(STANDARD_RIGHTS.SYNCHRONIZE);
    }

    //from WinReg.h
    public struct HKEY
    {
        public static readonly UIntPtr CLASSES_ROOT = new UIntPtr(0x80000000);
        public static readonly UIntPtr CURRENT_USER = new UIntPtr(0x80000001);
        public static readonly UIntPtr LOCAL_MACHINE = new UIntPtr(0x80000002);
        public static readonly UIntPtr USERS = new UIntPtr(0x80000003);
        public static readonly UIntPtr PERFORMANCE_DATA = new UIntPtr(0x80000004);
        public static readonly UIntPtr CURRENT_CONFIG = new UIntPtr(0x80000005);
        public static readonly UIntPtr DYN_DATA = new UIntPtr(0x80000006);
    }

    public struct REG_NOTIFY_CHANGE
    {
        public const uint NAME = 0x00000001;
        public const uint ATTRIBUTES = 0x00000002;
        public const uint LAST_SET = 0x00000004;
        public const uint SECURITY = 0x00000008;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STARTUPINFO
    {
        public int cb;
        public String lpReserved;
        public String lpDesktop;
        public String lpTitle;
        public uint dwX;
        public uint dwY;
        public uint dwXSize;
        public uint dwYSize;
        public uint dwXCountChars;
        public uint dwYCountChars;
        public uint dwFillAttribute;
        public uint dwFlags;
        public short wShowWindow;
        public short cbReserved2;
        public IntPtr lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_INFORMATION
    {
        public IntPtr hProcess;
        public IntPtr hThread;
        public uint dwProcessId;
        public uint dwThreadId;
    }

    public enum SECURITY_IMPERSONATION_LEVEL
    {
        SecurityAnonymous = 0,
        SecurityIdentification = 1,
        SecurityImpersonation = 2,
        SecurityDelegation = 3,
    }

    public enum TOKEN_TYPE
    {
        TokenPrimary = 1,
        TokenImpersonation = 2
    }
    
}