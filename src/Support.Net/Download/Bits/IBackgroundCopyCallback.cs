using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Net.Download.Bits
{
    [Guid("97EA99C7-0186-4AD4-8DF9-C5B4E0ED6B22")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [CLSCompliant(false)]
    [ComImport]
    internal interface IBackgroundCopyCallback
    {
        void JobTransferred([MarshalAs(UnmanagedType.Interface)] [In] IBackgroundCopyJob pJob);

        void JobError([MarshalAs(UnmanagedType.Interface)] [In] IBackgroundCopyJob pJob, [MarshalAs(UnmanagedType.Interface)] [In] IBackgroundCopyError pError);

        void JobModification([MarshalAs(UnmanagedType.Interface)] [In] IBackgroundCopyJob pJob, [In] uint dwReserved);
    }
}