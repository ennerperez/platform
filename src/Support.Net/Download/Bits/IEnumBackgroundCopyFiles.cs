using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Net.Download.Bits
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("CA51E165-C365-424C-8D41-24AAA4FF3C40")]
    [CLSCompliant(false)]
    [ComImport]
    internal interface IEnumBackgroundCopyFiles
    {
        void Next(uint celt, [MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyFile rgelt, out uint pceltFetched);

        void Skip(uint celt);

        void Reset();

        void Clone([MarshalAs(UnmanagedType.Interface)] out IEnumBackgroundCopyFiles ppenum);

        void GetCount(out uint puCount);
    }
}