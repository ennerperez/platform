using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Net.Download.Bits
{
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("F1BD1079-9F01-4BDC-8036-F09B70095066")]
    [CLSCompliant(false)]
    [ComImport]
    internal interface IBackgroundCopyJobHttpOptions
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        void SetClientCertificateByID([In] BG_CERT_STORE_LOCATION storeLocation, [MarshalAs(UnmanagedType.LPWStr)] [In] string storeName, [In] ref byte pCertHashBlob);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void SetClientCertificateByName([In] BG_CERT_STORE_LOCATION storeLocation, [MarshalAs(UnmanagedType.LPWStr)] [In] string storeName, [MarshalAs(UnmanagedType.LPWStr)] [In] string subjectName);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void RemoveClientCertificate();

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetClientCertificate(out BG_CERT_STORE_LOCATION pStoreLocation, [MarshalAs(UnmanagedType.LPWStr)] out string pStoreName, [Out] IntPtr ppCertHashBlob, [MarshalAs(UnmanagedType.LPWStr)] out string pSubjectName);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void SetCustomHeaders([MarshalAs(UnmanagedType.LPWStr)] [In] string requestHeaders);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetCustomHeaders([MarshalAs(UnmanagedType.LPWStr)] out string pRequestHeaders);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void SetSecurityFlags([In] BG_HTTP_SECURITY_FLAGS flags);

        [MethodImpl(MethodImplOptions.InternalCall)]
        void GetSecurityFlags(out BG_HTTP_SECURITY_FLAGS pFlags);
    }
}