// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Platform.Support.Windows
{
    /// <summary>
    /// Imports from UrlMon.dll
    /// </summary>
#if !INTEROP
    internal class UrlMon
#else
public class UrlMon
#endif

    {
        [DllImport(ExternDll.UrlMon)]
        public static extern Int32 IsValidURL(
            [In] IntPtr pBC,
            [In] string szURL,
            [In] Int32 dxReserved);

        [DllImport(ExternDll.UrlMon)]
        public static extern int CreateURLMoniker(
            [In] IMoniker pmkContext,
            [In, MarshalAs(UnmanagedType.LPWStr)]  string szURL,
            [Out] out IMoniker ppmk);

        [DllImport(ExternDll.UrlMon, CharSet = CharSet.Auto)]
        public static extern int URLDownloadToFile(
            [In] IntPtr pCaller,
            [In, MarshalAs(UnmanagedType.LPTStr)] string szURL,
            [In, MarshalAs(UnmanagedType.LPTStr)] string szFileName,
            uint dwReserved,
            [In] IBindStatusCallback lpfnCB
            );
        [DllImport(ExternDll.UrlMon, CharSet = CharSet.Auto)]
        public static extern int URLOpenBlockingStream([In] IntPtr pCaller,
        [In, MarshalAs(UnmanagedType.LPTStr)] string szURL,
        out IStream ppStream,
        uint dwReserved,
        IBindStatusCallback lpfnCB);

        [DllImport(ExternDll.UrlMon)]
        public static extern int CoInternetSetFeatureEnabled(
            [In] int FEATURE,
            [In] int dwFlags,
            [In] bool fEnable);
    }
#if !INTEROP
    internal struct FEATURE
#else
public struct FEATURE
#endif

    {
        public const int OBJECT_CACHING = 0;
        public const int ZONE_ELEVATION = 1;
        public const int MIME_HANDLING = 2;
        public const int MIME_SNIFFING = 3;
        public const int WINDOW_RESTRICTIONS = 4;
        public const int WEBOC_POPUPMANAGEMENT = 5;
        public const int BEHAVIORS = 6;
        public const int DISABLE_MK_PROTOCOL = 7;
        public const int LOCALMACHINE_LOCKDOWN = 8;
        public const int SECURITYBAND = 9;
        public const int RESTRICT_ACTIVEXINSTALL = 10;
        public const int VALIDATE_NAVIGATE_URL = 11;
        public const int RESTRICT_FILEDOWNLOAD = 12;
        public const int ADDON_MANAGEMENT = 13;
        public const int PROTOCOL_LOCKDOWN = 14;
        public const int HTTP_USERNAME_PASSWORD_DISABLE = 15;
        public const int SAFE_BINDTOOBJECT = 16;
        public const int UNC_SAVEDFILECHECK = 17;
        public const int GET_URL_DOM_FILEPATH_UNENCODED = 18;
        public const int TABBED_BROWSING = 19;
        public const int SSLUX = 20;
        public const int DISABLE_NAVIGATION_SOUNDS = 21;
        public const int DISABLE_LEGACY_COMPRESSION = 22;
        public const int FORCE_ADDR_AND_STATUS = 23;
        public const int XMLHTTP = 24;
        public const int DISABLE_TELNET_PROTOCOL = 25;
        public const int FEEDS = 26;
        public const int BLOCK_INPUT_PROMPTS = 27;
        public const int ENTRY_COUNT = 28;

    }
#if !INTEROP
    internal struct INTERNETSETFEATURE
#else
public struct INTERNETSETFEATURE
#endif

    {
        public const int ON_THREAD = 0x00000001;
        public const int ON_PROCESS = 0x00000002;
        public const int IN_REGISTRY = 0x00000004;
        public const int ON_THREAD_LOCALMACHINE = 0x00000008;
        public const int ON_THREAD_INTRANET = 0x00000010;
        public const int ON_THREAD_TRUSTED = 0x00000020;
        public const int ON_THREAD_INTERNET = 0x00000040;
        public const int ON_THREAD_RESTRICTED = 0x00000080;
    }

#if !INTEROP
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("79eac9c1-baf9-11ce-8c82-00aa004ba90b")]
    internal interface IBindStatusCallback
#else
[ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("79eac9c1-baf9-11ce-8c82-00aa004ba90b")]
    public interface IBindStatusCallback
#endif

    {
        void OnStartBinding(
            [In] uint dwReserved,
            [In] IntPtr pib);

        void GetPriority(
            [Out] out int pnPriority);

        void OnLowResource(
            [In] uint reserved);

        [PreserveSig]
        int OnProgress(
            [In] uint ulProgress,
            [In] uint ulProgressMax,
            [In] BINDSTATUS ulStatusCode,
            [In, MarshalAs(UnmanagedType.LPWStr)] string szStatusText);

        void OnStopBinding(
            [In] int hresult,
            [In, MarshalAs(UnmanagedType.LPWStr)] string szError);

        void GetBindInfo(
            [In, Out] ref uint grfBINDF,
            [In, Out] ref BINDINFO pbindinfo);

        void OnDataAvailable(
            [In] BSCF grfBSCF,
            [In] uint dwSize,
            [In] ref FORMATETC pformatetc,
            [In] ref STGMEDIUM pstgmed);

        void OnObjectAvailable(
            [In] ref Guid riid,
            [In] IntPtr punk);
    }

    /// <summary>
    /// Contains values that are passed to the client application's implementation of the
    /// ReportProgress method to indicate the progress of the bind operation.
    /// </summary>
#if !INTEROP
    internal enum BINDSTATUS : uint
#else
public enum BINDSTATUS : uint
#endif

    {
        /// <summary>
        /// Notifies the client application that the bind operation is finding the
        /// resource that holds the object or storage being bound to. The szStatusText
        /// parameter to the IBindStatusCallback::OnProgress method provides the
        /// display name of the resource being searched for (for example, "www.foo.com").
        /// </summary>
        FINDINGRESOURCE = 1,

        /// <summary>
        /// Notifies the client application that the bind operation is connecting to
        /// the resource that holds the object or storage being bound to. The
        /// szStatusText parameter to the IBindStatusCallback::OnProgress method
        /// provides the display name of the resource being connected to (for example,
        /// an IP address).
        /// </summary>
        CONNECTING,

        /// <summary>
        /// Notifies the client application that the bind operation has been
        /// redirected to a different data location. The szStatusText parameter
        /// to the IBindStatusCallback::OnProgress method provides the display
        /// name of the new data location.
        /// </summary>
        REDIRECTING,

        /// <summary>
        /// Notifies the client application that the bind operation has begun
        /// receiving the object or storage being bound to. The szStatusText
        /// parameter to the IBindStatusCallback::OnProgress method provides the
        /// display name of the data location.
        /// </summary>
        BEGINDOWNLOADDATA,

        /// <summary>
        /// Notifies the client application that the bind operation has begun
        /// receiving the object or storage being bound to. The szStatusText
        /// parameter to the IBindStatusCallback::OnProgress method provides
        /// the display name of the data location.
        /// </summary>
        DOWNLOADINGDATA,

        /// <summary>
        /// Notifies the client application that the bind operation has finished
        /// receiving the object or storage being bound to. The szStatusText
        /// parameter to the IBindStatusCallback::OnProgress method provides
        /// the display name of the data location.
        /// </summary>
        ENDDOWNLOADDATA,

        /// <summary>
        /// Notifies the client application that the bind operation is beginning
        /// to download the component.
        /// </summary>
        BEGINDOWNLOADCOMPONENTS,

        /// <summary>
        /// Notifies the client application that the bind operation is installing
        /// the component.
        /// </summary>
        INSTALLINGCOMPONENTS,

        /// <summary>
        /// Notifies the client application that the bind operation has finished
        /// downloading the component.
        /// </summary>
        ENDDOWNLOADCOMPONENTS,

        /// <summary>
        /// Notifies the client application that the bind operation is retrieving
        /// the requested object or storage from a cached copy. The szStatusText
        /// parameter to the IBindStatusCallback::OnProgress method is NULL.
        /// </summary>
        USINGCACHEDCOPY,

        /// <summary>
        /// Notifies the client application that the bind operation is requesting
        /// the object or storage being bound to. The szStatusText parameter to the
        /// IBindStatusCallback::OnProgress method provides the display name of the
        /// object (for example, a file name).
        /// </summary>
        SENDINGREQUEST,

        /// <summary>
        /// Notifies the client application that the CLSID of the resource is available.
        /// </summary>
        CLASSIDAVAILABLE,

        /// <summary>
        /// Notifies the client application that the MIME type of the resource is
        /// available.
        /// </summary>
        MIMETYPEAVAILABLE,

        /// <summary>
        /// Notifies the client application that the temporary or cache file name
        /// of the resource is available. The temporary file name might be returned
        /// if BINDF_NOWRITECACHE is called. The temporary file will be deleted once
        /// the storage is released.
        /// </summary>
        CACHEFILENAMEAVAILABLE,

        /// <summary>
        /// Notifies the client application that a synchronous operation has started.
        /// </summary>
        BEGINSYNCOPERATION,

        /// <summary>
        /// Notifies the client application that the synchronous operation has completed.
        /// </summary>
        ENDSYNCOPERATION,

        /// <summary>
        /// Notifies the client application that the file upload has started.
        /// </summary>
        BEGINUPLOADDATA,

        /// <summary>
        /// Notifies the client application that the file upload is in progress.
        /// </summary>
        UPLOADINGDATA,

        /// <summary>
        /// Notifies the client application that the file upload has completed.
        /// </summary>
        ENDUPLOADDATA,

        /// <summary>
        /// Notifies the client application that the CLSID of the protocol
        /// handler being used is available.
        /// </summary>
        PROTOCOLCLASSID,

        /// <summary>
        /// Notifies the client application that the Urlmon.dll is encoding data.
        /// </summary>
        ENCODING,

        /// <summary>
        /// Notifies the client application that the verified MIME type is available.
        /// </summary>
        VERIFIEDMIMETYPEAVAILABLE,

        /// <summary>
        /// Notifies the client application that the class install location is available.
        /// </summary>
        CLASSINSTALLLOCATION,

        /// <summary>
        /// Notifies the client application that the bind operation is decoding data.
        /// </summary>
        DECODING,

        /// <summary>
        /// Notifies the client application that a pluggable MIME handler is being loaded.
        /// This value was added for Microsoft® Internet Explorer 5.
        /// </summary>
        LOADINGMIMEHANDLER,

        /// <summary>
        /// Notifies the client application that this resource contained a
        /// Content-Disposition header that indicates that this resource is an
        /// attachment. The content of this resource should not be automatically
        /// displayed. Client applications should request permission from the user.
        /// This value was added for Internet Explorer 5.
        /// </summary>
        CONTENTDISPOSITIONATTACH,

        /// <summary>
        /// Notifies the client application of the new MIME type of the resource.
        /// This is used by a pluggable MIME filter to report a change in the MIME
        /// type after it has processed the resource. This value was added for
        /// Internet Explorer 5.
        /// </summary>
        FILTERREPORTMIMETYPE,

        /// <summary>
        /// Notifies the Urlmon.dll that this CLSID is for the class the Urlmon.dll
        /// should return to the client on a call to IMoniker::BindToObject. This
        /// value was added for Internet Explorer 5.
        /// </summary>
        CLSIDCANINSTANTIATE,

        /// <summary>
        /// Reports that the IUnknown interface has been released. This value was
        /// added for Internet Explorer 5.
        /// </summary>
        IUNKNOWNAVAILABLE,

        /// <summary>
        /// Reports whether or not the client application is connected directly to
        /// the pluggable protocol handler. This value was added for Internet
        /// Explorer 5.
        /// </summary>
        DIRECTBIND,

        /// <summary>
        /// Reports the MIME type of the resource, before any code sniffing is done.
        /// This value was added for Internet Explorer 5.
        /// </summary>
        RAWMIMETYPE,

        /// <summary>
        /// Reports that a proxy server has been detected. This value was added
        /// for Internet Explorer 5.
        /// </summary>
        PROXYDETECTING,

        /// <summary>
        /// Reports the valid types of range requests for a resource. This value
        /// was added for Internet Explorer 5.
        /// </summary>
        ACCEPTRANGES,

        /// <summary>
        ///
        /// </summary>
        COOKIE_SENT,

        /// <summary>
        ///
        /// </summary>
        COMPACT_POLICY_RECEIVED,

        /// <summary>
        ///
        /// </summary>
        COOKIE_SUPPRESSED,

        /// <summary>
        ///
        /// </summary>
        COOKIE_STATE_UNKNOWN,

        /// <summary>
        ///
        /// </summary>
        COOKIE_STATE_ACCEPT,

        /// <summary>
        ///
        /// </summary>
        COOKIE_STATE_REJECT,

        /// <summary>
        ///
        /// </summary>
        COOKIE_STATE_PROMPT,

        /// <summary>
        ///
        /// </summary>
        COOKIE_STATE_LEASH,

        /// <summary>
        ///
        /// </summary>
        COOKIE_STATE_DOWNGRADE,

        /// <summary>
        ///
        /// </summary>
        POLICY_HREF,

        /// <summary>
        ///
        /// </summary>
        P3P_HEADER,

        /// <summary>
        ///
        /// </summary>
        SESSION_COOKIE_RECEIVED,

        /// <summary>
        ///
        /// </summary>
        PERSISTENT_COOKIE_RECEIVED,

        /// <summary>
        ///
        /// </summary>
        SESSION_COOKIES_ALLOWED
    }

    /// <summary>
    /// Contains additional information on the requested binding operation. The
    /// meaning of this structure is specific to the type of asynchronous moniker.
    /// </summary>
#if !INTEROP
    internal struct BINDINFO
#else
public struct BINDINFO
#endif

    {
        /// <summary>
        /// Size of the structure, in bytes.
        /// </summary>
        public uint cbSize;

        /// <summary>
        /// Behavior of this field is moniker-specific. For URL monikers, this
        /// string is appended to the URL when the bind operation is started.
        /// Like other OLE strings, this value is a Unicode string that the
        /// client should allocate using CoTaskMemAlloc. The URL moniker frees
        /// the memory later.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szExtraInfo;

        /// <summary>
        /// Data to be used in a PUT or POST operation specified by the
        /// dwBindVerb member.
        /// </summary>
        public STGMEDIUM stgmedData;

        /// <summary>
        /// Flag from the BINDINFOF enumeration that determines the use of URL
        /// encoding during the binding operation. This member is specific to
        /// URL monikers.
        /// </summary>
        public BINDINFOF grfBindInfoF;

        /// <summary>
        /// Value from the BINDVERB enumeration specifying an action to be
        /// performed during the bind operation.
        /// </summary>
        public BINDVERB dwBindVerb;

        /// <summary>
        /// BSTR specifying a protocol-specific custom action to be performed
        /// during the bind operation (only if dwBindVerb is set to
        /// BINDVERB_CUSTOM).
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string szCustomVerb;

        /// <summary>
        /// Size of the data provided in the stgmedData member.
        /// </summary>
        public uint cbstgmedData;

        /// <summary>
        /// Reserved. Must be set to 0.
        /// </summary>
        public uint dwOptions;

        /// <summary>
        /// Reserved. Must be set to 0.
        /// </summary>
        public uint dwOptionsFlags;

        /// <summary>
        /// Unsigned long integer value that contains the code page used to
        /// perform the conversion. This can be one of the following values.
        /// CP_ACP
        ///		ANSI code page.
        /// CP_MACCP
        ///		Macintosh code page.
        /// CP_OEMCP
        ///		OEM code page.
        /// CP_SYMBOL
        ///		Symbol code page (42).
        /// CP_THREAD_ACP
        ///		Current thread's ANSI code page.
        /// CP_UTF7
        ///		Translate using UTF-7.
        /// CP_UTF8
        /// 	Translate using UTF-8.
        /// </summary>
        public uint dwCodePage;

        /// <summary>
        /// SECURITY_ATTRIBUTES structure that contains the descriptor for the
        /// object being bound to and indicates whether the handle retrieved by
        /// specifying this structure is inheritable.
        /// </summary>
        public SECURITY_ATTRIBUTES securityAttributes;

        /// <summary>
        /// Interface identifier of the IUnknown interface referred to by pUnk.
        /// </summary>
        public Guid iid;

        /// <summary>
        /// Pointer to the IUnknown interface.
        /// </summary>
        [MarshalAs(UnmanagedType.IUnknown)]
        public object punk;

        /// <summary>
        /// Reserved. Must be set to 0.
        /// </summary>
        public uint dwReserved;
    }

    /// <summary>
    /// Values from the BSCF enumeration are passed to ReportData to indicate the type
    /// of data that is available.
    /// </summary>
#if !INTEROP
    [Flags]
    internal enum BSCF : uint
#else
[Flags]
    public enum BSCF : uint
#endif

    {
        /// <summary>
        /// Identify the first call to ReportData for a given bind operation.
        /// </summary>
        FIRSTDATANOTIFICATION = 0x00000001,

        /// <summary>
        /// Identify an intermediate call to ReportData for a bind operation.
        /// </summary>
        INTERMEDIATEDATANOTIFICATION = 0x00000002,

        /// <summary>
        /// Identify the last call to ReportData for a bind operation.
        /// </summary>
        LASTDATANOTIFICATION = 0x00000004,

        /// <summary>
        /// All of the requested data is available.
        /// </summary>
        DATAFULLYAVAILABLE = 0x00000008,

        /// <summary>
        /// Size of the data available is unknown.
        /// </summary>
        AVAILABLEDATASIZEUNKNOWN = 0x00000010
    }

    /// <summary>
    /// Contains values that determine the use of URL encoding during the binding
    /// operation.
    /// </summary>
#if !INTEROP
    internal enum BINDINFOF : uint
#else
public enum BINDINFOF : uint
#endif

    {
        /// <summary>
        /// Use URL encoding to pass in the data provided in the stgmedData member
        /// of the BINDINFO structure for PUT and POST operations.
        /// </summary>
        URLENCODESTGMEDDATA = 0x00000001,

        /// <summary>
        /// Use URL encoding to pass in the data provided in the szExtraInfo
        /// member of the BINDINFO structure.
        /// </summary>
        URLENCODEDEXTRAINFO = 0x00000002
    };

    /// <summary>
    /// Contains values that specify an action, such as an HTTP request, to be
    /// performed during the binding operation.
    /// </summary>
#if !INTEROP
    internal enum BINDVERB : uint
#else
 public enum BINDVERB : uint
#endif

    {
        /// <summary>
        /// Perform an HTTP GET operation, the default operation. The stgmedData
        /// member of the BINDINFO structure should be set to TYMED_NULL for the
        /// GET operation.
        /// </summary>
        GET = 0x00000000,

        /// <summary>
        /// Perform an HTTP POST operation. The data to be posted should be
        /// specified in the stgmedData member of the BINDINFO structure.
        /// </summary>
        POST = 0x00000001,

        /// <summary>
        /// Perform an HTTP PUT operation. The data to put should be specified
        /// in the stgmedData member of the BINDINFO structure.
        /// </summary>
        PUT = 0x00000002,

        /// <summary>
        /// Perform a custom operation that is protocol-specific. See the
        /// szCustomVerb member of the BINDINFO structure. The data to be used
        /// in the custom operation should be specified in the stgmedData
        /// structure.
        /// </summary>
        CUSTOM = 0x00000003
    }

}
