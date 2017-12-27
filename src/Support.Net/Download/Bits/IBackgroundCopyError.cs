using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Net.Download.Bits
{
    [Guid("19C613A0-FCB8-4F28-81AE-897C3D078F81")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [CLSCompliant(false)]
    [ComImport]
    internal interface IBackgroundCopyError
    {
        void GetError(out BG_ERROR_CONTEXT pContext, [MarshalAs(UnmanagedType.Error)] out int pCode);

        void GetFile([MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyFile pVal);

        void GetErrorDescription(uint languageId, [MarshalAs(UnmanagedType.LPWStr)] out string pErrorDescription);

        void GetErrorContextDescription(uint languageId, [MarshalAs(UnmanagedType.LPWStr)] out string pContextDescription);

        void GetProtocol([MarshalAs(UnmanagedType.LPWStr)] out string pProtocol);
    }
}