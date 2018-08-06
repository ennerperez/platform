using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Net.Download.Bits
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("5CE34C0D-0DC9-4C1F-897C-DAA1B78CEE7C")]
    [CLSCompliant(false)]
    [ComImport]
    internal interface IBackgroundCopyManager
    {
        void CreateJob([MarshalAs(UnmanagedType.LPWStr)] string displayName, BG_JOB_TYPE type, out Guid pJobId, [MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyJob ppJob);

        void GetJob(ref Guid jobID, [MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyJob ppJob);

        void EnumJobs(uint dwFlags, [MarshalAs(UnmanagedType.Interface)] out IEnumBackgroundCopyJobs ppenum);

        void GetErrorDescription([MarshalAs(UnmanagedType.Error)] int hResult, uint languageId, [MarshalAs(UnmanagedType.LPWStr)] out string pErrorDescription);
    }
}