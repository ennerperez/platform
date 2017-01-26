// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Runtime.InteropServices;

namespace Platform.Support.Windows
{
    /// <summary>
    /// Summary description for GdiPlus.
    /// </summary>
    public class GdiPlus
    {
        [DllImport(ExternDll.GdiPlus, CharSet = CharSet.Auto)]
        public static extern int GdipCreateBitmapFromGdiDib(IntPtr bminfo,
                                                                IntPtr pixdat,
                                                                ref IntPtr image);

        [DllImport(ExternDll.GdiPlus, CharSet = CharSet.Auto)]
        public static extern int GdipSaveImageToFile(IntPtr image,
                                                        string filename,
                                                        [In] ref Guid clsid,
                                                        IntPtr encparams);

        [DllImport(ExternDll.GdiPlus, CharSet = CharSet.Auto)]
        public static extern int GdipDisposeImage(IntPtr image);
    }
}
