// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Windows
{
    /// <summary>
    /// Imports from Psapi.dll
    /// </summary>
    public class Psapi
    {
        /// <summary>
        /// Do not use this method; it does not work on Windows 2000
        /// </summary>
        [Obsolete("Only works with Windows XP and above", true)]
        [DllImport(ExternDll.Psapi, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint GetProcessImageFileName(
            IntPtr hProcess,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpImageFileName,
            uint nSize
            );

        [DllImport(ExternDll.Psapi, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool EnumProcessModules(
            IntPtr hProcess,
            IntPtr[] lphModule,
            uint cb,
            ref uint lpcbNeeded);

        [DllImport(ExternDll.Psapi, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint GetModuleFileNameEx(
            IntPtr hProcess,
            IntPtr hModule,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpFilename,
            uint nSize
            );
    }
}