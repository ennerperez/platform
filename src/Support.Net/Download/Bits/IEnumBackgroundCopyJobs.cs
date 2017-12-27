using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Net.Download.Bits
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("1AF4F612-3B71-466F-8F58-7B6F73AC57AD")]
    [CLSCompliant(false)]
    [ComImport]
    internal interface IEnumBackgroundCopyJobs
    {
        void Next(uint celt, [MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyJob rgelt, out uint pceltFetched);

        void Skip(uint celt);

        void Reset();

        void Clone([MarshalAs(UnmanagedType.Interface)] out IEnumBackgroundCopyJobs ppenum);

        void GetCount(out uint puCount);
    }
}