using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Net.Download.Bits
{
    [Guid("01B7BD23-FB88-4A77-8490-5891D3E4653A")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [CLSCompliant(false)]
    [ComImport]
    internal interface IBackgroundCopyFile
    {
        void GetRemoteName([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

        void GetLocalName([MarshalAs(UnmanagedType.LPWStr)] out string pVal);

        void GetProgress(out _BG_FILE_PROGRESS pVal);
    }
}