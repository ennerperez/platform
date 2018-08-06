// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Runtime.InteropServices;

namespace Platform.Support.Windows
{
#if !INTEROP

    internal static class OleAut32
#else

    public static class OleAut32
#endif
    {
        [DllImport(ExternDll.OleAut32, PreserveSig = true)] // psa is actually returned, not hresult
        public extern static IntPtr SafeArrayCreateVector(ushort vt, int lowerBound, uint cElems);

        [DllImport(ExternDll.OleAut32, PreserveSig = false)] // returns hresult
        public extern static IntPtr SafeArrayAccessData(IntPtr psa);

        [DllImport(ExternDll.OleAut32, PreserveSig = false)] // returns hresult
        public extern static void SafeArrayUnaccessData(IntPtr psa);

        [DllImport(ExternDll.OleAut32, PreserveSig = true)] // retuns uint32
        public extern static uint SafeArrayGetDim(IntPtr psa);

        [DllImport(ExternDll.OleAut32, PreserveSig = false)] // returns hresult
        public extern static int SafeArrayGetLBound(IntPtr psa, uint nDim);

        [DllImport(ExternDll.OleAut32, PreserveSig = false)] // returns hresult
        public extern static int SafeArrayGetUBound(IntPtr psa, uint nDim);

        // This decl for SafeArrayGetElement is only valid for cDims==1!
        [DllImport(ExternDll.OleAut32, PreserveSig = false)] // returns hresult
        [return: MarshalAs(UnmanagedType.IUnknown)]
        public extern static object SafeArrayGetElement(IntPtr psa, ref int rgIndices);
    }
}