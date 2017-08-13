// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Platform.Support.Windows
{
#pragma warning disable CS0649

    /// <summary>
    /// Imports from Shell32.dll
    /// </summary>
#if !INTEROP

    internal class Shell32
#else

    public class Shell32
#endif
    {
        [DllImport(ExternDll.Shell32, CharSet = CharSet.Unicode)]
        public static extern bool PathYetAnotherMakeUniqueName(
            StringBuilder pszUniqueName,
            string pszPath,
            string pszShort,
            string pszFileSpec);

        [DllImport(ExternDll.Shell32)]
        public static extern bool PathIsSlow(string pszFile, int attr);

        [DllImport(ExternDll.Shell32)]
        public static extern IntPtr FindExecutable(string lpFile, string lpDirectory,
            [Out] StringBuilder lpResult);

        [DllImport(ExternDll.Shell32)]
        public static extern int SHGetInstanceExplorer(
            out IntPtr ppunk);

        [DllImport(ExternDll.Shell32)]
        public static extern int SHGetSpecialFolderLocation(
            IntPtr hwndOwner, int nFolder, out IntPtr ppidl);

        [DllImport(ExternDll.Shell32)]
        public static extern Int32 SHGetFolderLocation(
            IntPtr hwndOwner, Int32 nFolder, IntPtr hToken, UInt32 dwReserved, out IntPtr ppidl);

        [DllImport(ExternDll.Shell32)]
        public static extern Int32 SHGetKnownFolderIDList(
            ref Guid rfid, UInt32 dwFlags, IntPtr hToken, out IntPtr ppidl);

        [DllImport(ExternDll.Shell32)]
        public static extern void ILFree(IntPtr pidl);

        [DllImport(ExternDll.Shell32, CharSet = CharSet.Unicode)]
        public static extern void SHAddToRecentDocs(uint uFlags, [MarshalAs(UnmanagedType.LPTStr)]string pv);

        [DllImport(ExternDll.Shell32, CharSet = CharSet.Unicode)]
        public static extern bool SHGetPathFromIDList(IntPtr pidl,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszPath);

        [DllImport(ExternDll.Shell32)]
        public static extern int SHGetMalloc(out IntPtr ppMalloc);

        [DllImport(ExternDll.Shell32)]
        public static extern int SHCreateDirectory(
            IntPtr hwnd,
            [MarshalAs(UnmanagedType.LPWStr)] string pszPath);

        [DllImport(ExternDll.Shell32, CharSet = CharSet.Unicode,
            SetLastError = true)]
        public static extern int SHCreateItemFromParsingName(
            [MarshalAs(UnmanagedType.LPWStr)] string path,
            // The following parameter is not used - binding context.
            IntPtr pbc,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out IShellItem2 shellItem);

        /// <summary>
        /// Gets the HIcon for the icon associated with a file
        /// </summary>
        [DllImport(ExternDll.Shell32)]
        public static extern IntPtr ExtractAssociatedIcon(
            IntPtr hInst,
            string lpIconPath,
            ref short lpiIcon
            );

        /// <summary>
        /// Retrieves an icon from an icon, exe, or dll file by index
        /// </summary>
        [DllImport(ExternDll.Shell32)]
        public static extern IntPtr ExtractIcon(
            IntPtr hInst,
            string lpszExeFileName,
            Int32 nIconIndex
            );

        [DllImport(ExternDll.Shell32, SetLastError = true)]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [DllImport(ExternDll.Shell32)]
        public static extern void SetCurrentProcessExplicitAppUserModelID(
            [MarshalAs(UnmanagedType.LPWStr)] string AppID);

        [DllImport(ExternDll.Shell32)]
        public static extern void GetCurrentProcessExplicitAppUserModelID(
            [Out(), MarshalAs(UnmanagedType.LPWStr)] out string AppID);

        [DllImport(ExternDll.Shell32)]
        public static extern int SHGetPropertyStoreForWindow(
            IntPtr hwnd,
            ref Guid iid /*IID_IPropertyStore*/,
            [Out(), MarshalAs(UnmanagedType.Interface)]
                out IPropertyStore propertyStore);

        /// <summary>
        /// Indicate flags that modify the property store object retrieved by methods
        /// that create a property store, such as IShellItem2::GetPropertyStore or
        /// IPropertyStoreFactory::GetPropertyStore.
        /// </summary>
        [Flags]
        public enum GETPROPERTYSTOREFLAGS : uint
        {
            /// <summary>
            /// Meaning to a calling process: Return a read-only property store that contains all
            /// properties. Slow items (offline files) are not opened.
            /// Combination with other flags: Can be overridden by other flags.
            /// </summary>
            GPS_DEFAULT = 0,

            /// <summary>
            /// Meaning to a calling process: Include only properties directly from the property
            /// handler, which opens the file on the disk, network, or device. Meaning to a file
            /// folder: Only include properties directly from the handler.
            ///
            /// Meaning to other folders: When delegating to a file folder, pass this flag on
            /// to the file folder; do not do any multiplexing (MUX). When not delegating to a
            /// file folder, ignore this flag instead of returning a failure code.
            ///
            /// Combination with other flags: Cannot be combined with GPS_TEMPORARY,
            /// GPS_FASTPROPERTIESONLY, or GPS_BESTEFFORT.
            /// </summary>
            GPS_HANDLERPROPERTIESONLY = 0x1,

            /// <summary>
            /// Meaning to a calling process: Can write properties to the item.
            /// Note: The store may contain fewer properties than a read-only store.
            ///
            /// Meaning to a file folder: ReadWrite.
            ///
            /// Meaning to other folders: ReadWrite. Note: When using default MUX,
            /// return a single unmultiplexed store because the default MUX does not support ReadWrite.
            ///
            /// Combination with other flags: Cannot be combined with GPS_TEMPORARY, GPS_FASTPROPERTIESONLY,
            /// GPS_BESTEFFORT, or GPS_DELAYCREATION. Implies GPS_HANDLERPROPERTIESONLY.
            /// </summary>
            GPS_READWRITE = 0x2,

            /// <summary>
            /// Meaning to a calling process: Provides a writable store, with no initial properties,
            /// that exists for the lifetime of the Shell item instance; basically, a property bag
            /// attached to the item instance.
            ///
            /// Meaning to a file folder: Not applicable. Handled by the Shell item.
            ///
            /// Meaning to other folders: Not applicable. Handled by the Shell item.
            ///
            /// Combination with other flags: Cannot be combined with any other flag. Implies GPS_READWRITE
            /// </summary>
            GPS_TEMPORARY = 0x4,

            /// <summary>
            /// Meaning to a calling process: Provides a store that does not involve reading from the
            /// disk or network. Note: Some values may be different, or missing, compared to a store
            /// without this flag.
            ///
            /// Meaning to a file folder: Include the "innate" and "fallback" stores only. Do not load the handler.
            ///
            /// Meaning to other folders: Include only properties that are available in memory or can
            /// be computed very quickly (no properties from disk, network, or peripheral IO devices).
            /// This is normally only data sources from the IDLIST. When delegating to other folders, pass this flag on to them.
            ///
            /// Combination with other flags: Cannot be combined with GPS_TEMPORARY, GPS_READWRITE,
            /// GPS_HANDLERPROPERTIESONLY, or GPS_DELAYCREATION.
            /// </summary>
            GPS_FASTPROPERTIESONLY = 0x8,

            /// <summary>
            /// Meaning to a calling process: Open a slow item (offline file) if necessary.
            /// Meaning to a file folder: Retrieve a file from offline storage, if necessary.
            /// Note: Without this flag, the handler is not created for offline files.
            ///
            /// Meaning to other folders: Do not return any properties that are very slow.
            ///
            /// Combination with other flags: Cannot be combined with GPS_TEMPORARY or GPS_FASTPROPERTIESONLY.
            /// </summary>
            GPS_OPENSLOWITEM = 0x10,

            /// <summary>
            /// Meaning to a calling process: Delay memory-intensive operations, such as file access, until
            /// a property is requested that requires such access.
            ///
            /// Meaning to a file folder: Do not create the handler until needed; for example, either
            /// GetCount/GetAt or GetValue, where the innate store does not satisfy the request.
            /// Note: GetValue might fail due to file access problems.
            ///
            /// Meaning to other folders: If the folder has memory-intensive properties, such as
            /// delegating to a file folder or network access, it can optimize performance by
            /// supporting IDelayedPropertyStoreFactory and splitting up its properties into a
            /// fast and a slow store. It can then use delayed MUX to recombine them.
            ///
            /// Combination with other flags: Cannot be combined with GPS_TEMPORARY or
            /// GPS_READWRITE
            /// </summary>
            GPS_DELAYCREATION = 0x20,

            /// <summary>
            /// Meaning to a calling process: Succeed at getting the store, even if some
            /// properties are not returned. Note: Some values may be different, or missing,
            /// compared to a store without this flag.
            ///
            /// Meaning to a file folder: Succeed and return a store, even if the handler or
            /// innate store has an error during creation. Only fail if substores fail.
            ///
            /// Meaning to other folders: Succeed on getting the store, even if some properties
            /// are not returned.
            ///
            /// Combination with other flags: Cannot be combined with GPS_TEMPORARY,
            /// GPS_READWRITE, or GPS_HANDLERPROPERTIESONLY.
            /// </summary>
            GPS_BESTEFFORT = 0x40,

            /// <summary>
            /// Mask for valid GETPROPERTYSTOREFLAGS values.
            /// </summary>
            GPS_MASK_VALID = 0xff,
        }

        [Flags]
        public enum SFGAO : uint
        {
            /// <summary>
            /// The specified items can be copied.
            /// </summary>
            SFGAO_CANCOPY = 0x00000001,

            /// <summary>
            /// The specified items can be moved.
            /// </summary>
            SFGAO_CANMOVE = 0x00000002,

            /// <summary>
            /// Shortcuts can be created for the specified items. This flag has the same value as DROPEFFECT.
            /// The normal use of this flag is to add a Create Shortcut item to the shortcut menu that is displayed
            /// during drag-and-drop operations. However, SFGAO_CANLINK also adds a Create Shortcut item to the Microsoft
            /// Windows Explorer's File menu and to normal shortcut menus.
            /// If this item is selected, your application's IContextMenu::InvokeCommand is invoked with the lpVerb
            /// member of the CMINVOKECOMMANDINFO structure set to "link." Your application is responsible for creating the link.
            /// </summary>
            SFGAO_CANLINK = 0x00000004,

            /// <summary>
            /// The specified items can be bound to an IStorage interface through IShellFolder::BindToObject.
            /// </summary>
            SFGAO_STORAGE = 0x00000008,

            /// <summary>
            /// The specified items can be renamed.
            /// </summary>
            SFGAO_CANRENAME = 0x00000010,

            /// <summary>
            /// The specified items can be deleted.
            /// </summary>
            SFGAO_CANDELETE = 0x00000020,

            /// <summary>
            /// The specified items have property sheets.
            /// </summary>
            SFGAO_HASPROPSHEET = 0x00000040,

            /// <summary>
            /// The specified items are drop targets.
            /// </summary>
            SFGAO_DROPTARGET = 0x00000100,

            /// <summary>
            /// This flag is a mask for the capability flags.
            /// </summary>
            SFGAO_CAPABILITYMASK = 0x00000177,

            /// <summary>
            /// Windows 7 and later. The specified items are system items.
            /// </summary>
            SFGAO_SYSTEM = 0x00001000,

            /// <summary>
            /// The specified items are encrypted.
            /// </summary>
            SFGAO_ENCRYPTED = 0x00002000,

            /// <summary>
            /// Indicates that accessing the object = through IStream or other storage interfaces,
            /// is a slow operation.
            /// Applications should avoid accessing items flagged with SFGAO_ISSLOW.
            /// </summary>
            SFGAO_ISSLOW = 0x00004000,

            /// <summary>
            /// The specified items are ghosted icons.
            /// </summary>
            SFGAO_GHOSTED = 0x00008000,

            /// <summary>
            /// The specified items are shortcuts.
            /// </summary>
            SFGAO_LINK = 0x00010000,

            /// <summary>
            /// The specified folder objects are shared.
            /// </summary>
            SFGAO_SHARE = 0x00020000,

            /// <summary>
            /// The specified items are read-only. In the case of folders, this means
            /// that new items cannot be created in those folders.
            /// </summary>
            SFGAO_READONLY = 0x00040000,

            /// <summary>
            /// The item is hidden and should not be displayed unless the
            /// Show hidden files and folders option is enabled in Folder Settings.
            /// </summary>
            SFGAO_HIDDEN = 0x00080000,

            /// <summary>
            /// This flag is a mask for the display attributes.
            /// </summary>
            SFGAO_DISPLAYATTRMASK = 0x000FC000,

            /// <summary>
            /// The specified folders contain one or more file system folders.
            /// </summary>
            SFGAO_FILESYSANCESTOR = 0x10000000,

            /// <summary>
            /// The specified items are folders.
            /// </summary>
            SFGAO_FOLDER = 0x20000000,

            /// <summary>
            /// The specified folders or file objects are part of the file system
            /// that is, they are files, directories, or root directories).
            /// </summary>
            SFGAO_FILESYSTEM = 0x40000000,

            /// <summary>
            /// The specified folders have subfolders = and are, therefore,
            /// expandable in the left pane of Windows Explorer).
            /// </summary>
            SFGAO_HASSUBFOLDER = 0x80000000,

            /// <summary>
            /// This flag is a mask for the contents attributes.
            /// </summary>
            SFGAO_CONTENTSMASK = 0x80000000,

            /// <summary>
            /// When specified as input, SFGAO_VALIDATE instructs the folder to validate that the items
            /// pointed to by the contents of apidl exist. If one or more of those items do not exist,
            /// IShellFolder::GetAttributesOf returns a failure code.
            /// When used with the file system folder, SFGAO_VALIDATE instructs the folder to discard cached
            /// properties retrieved by clients of IShellFolder2::GetDetailsEx that may
            /// have accumulated for the specified items.
            /// </summary>
            SFGAO_VALIDATE = 0x01000000,

            /// <summary>
            /// The specified items are on removable media or are themselves removable devices.
            /// </summary>
            SFGAO_REMOVABLE = 0x02000000,

            /// <summary>
            /// The specified items are compressed.
            /// </summary>
            SFGAO_COMPRESSED = 0x04000000,

            /// <summary>
            /// The specified items can be browsed in place.
            /// </summary>
            SFGAO_BROWSABLE = 0x08000000,

            /// <summary>
            /// The items are nonenumerated items.
            /// </summary>
            SFGAO_NONENUMERATED = 0x00100000,

            /// <summary>
            /// The objects contain new content.
            /// </summary>
            SFGAO_NEWCONTENT = 0x00200000,

            /// <summary>
            /// It is possible to create monikers for the specified file objects or folders.
            /// </summary>
            SFGAO_CANMONIKER = 0x00400000,

            /// <summary>
            /// Not supported.
            /// </summary>
            SFGAO_HASSTORAGE = 0x00400000,

            /// <summary>
            /// Indicates that the item has a stream associated with it that can be accessed
            /// by a call to IShellFolder::BindToObject with IID_IStream in the riid parameter.
            /// </summary>
            SFGAO_STREAM = 0x00400000,

            /// <summary>
            /// Children of this item are accessible through IStream or IStorage.
            /// Those children are flagged with SFGAO_STORAGE or SFGAO_STREAM.
            /// </summary>
            SFGAO_STORAGEANCESTOR = 0x00800000,

            /// <summary>
            /// This flag is a mask for the storage capability attributes.
            /// </summary>
            SFGAO_STORAGECAPMASK = 0x70C50008,

            /// <summary>
            /// Mask used by PKEY_SFGAOFlags to remove certain values that are considered
            /// to cause slow calculations or lack context.
            /// Equal to SFGAO_VALIDATE | SFGAO_ISSLOW | SFGAO_HASSUBFOLDER.
            /// </summary>
            SFGAO_PKEYSFGAOMASK = 0x81044000,
        }

        public enum KNOWNDESTCATEGORY
        {
            KDC_FREQUENT = 1,
            KDC_RECENT
        }

        public enum SIGDN : uint
        {
            SIGDN_NORMALDISPLAY = 0x00000000,           // SHGDN_NORMAL
            SIGDN_PARENTRELATIVEPARSING = 0x80018001,   // SHGDN_INFOLDER | SHGDN_FORPARSING
            SIGDN_DESKTOPABSOLUTEPARSING = 0x80028000,  // SHGDN_FORPARSING
            SIGDN_PARENTRELATIVEEDITING = 0x80031001,   // SHGDN_INFOLDER | SHGDN_FOREDITING
            SIGDN_DESKTOPABSOLUTEEDITING = 0x8004c000,  // SHGDN_FORPARSING | SHGDN_FORADDRESSBAR
            SIGDN_FILESYSPATH = 0x80058000,             // SHGDN_FORPARSING
            SIGDN_URL = 0x80068000,                     // SHGDN_FORPARSING
            SIGDN_PARENTRELATIVEFORADDRESSBAR = 0x8007c001,     // SHGDN_INFOLDER | SHGDN_FORPARSING | SHGDN_FORADDRESSBAR
            SIGDN_PARENTRELATIVE = 0x80080001           // SHGDN_INFOLDER
        }

        public enum SICHINTF : uint
        {
            SICHINT_DISPLAY = 0x00000000,
            SICHINT_CANONICAL = 0x10000000,
            SICHINT_TEST_FILESYSPATH_IF_NOT_EQUAL = 0x20000000,
            SICHINT_ALLFIELDS = 0x80000000
        }

        // IID GUID strings for relevant Shell COM interfaces.
        public const string IShellItem = "43826D1E-E718-42EE-BC55-A1E261C37BFE";

        public const string IShellItem2 = "7E9FB0D3-919F-4307-AB2E-9B1860310C93";
        internal const string IShellFolder = "000214E6-0000-0000-C000-000000000046";
        internal const string IShellFolder2 = "93F2F68C-1D1B-11D3-A30E-00C04F79ABD1";
        internal const string IShellLinkW = "000214F9-0000-0000-C000-000000000046";
        internal const string CShellLink = "00021401-0000-0000-C000-000000000046";
        public const string IPropertyStore = "886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99";

        public static Guid IObjectArray = new Guid("92CA9DCD-5622-4BBA-A805-5E9F541BD8C9");

        [DllImport(ExternDll.Shell32, EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public extern static int ExtractIconExW(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);
    }

#if !INTEROP

    internal struct SHARD
#else

    public struct SHARD
#endif
    {
        public const uint PIDL = 0x00000001;
        public const uint PATHA = 0x00000002;
        public const uint PATHW = 0x00000003;
    }

#if !INTEROP

    internal struct CSIDL
#else

    public struct CSIDL
#endif
    {
        public const int SENDTO = 0x0009;
    }

#if !INTEROP

    internal struct FILEDESCRIPTOR_HEADER
#else

    public struct FILEDESCRIPTOR_HEADER
#endif
    {
        public int dwFlags;
        public Guid clsid;
        public SIZEL sizel;
        public POINTL pointl;
        public int dwFileAttributes;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
        public int nFileSizeHigh;
        public int nFileSizeLow;
    }

#if !INTEROP

    internal struct SHGFI
#else

    public struct SHGFI
#endif
    {
        public const uint ICON = 0x100;
        public const uint LARGEICON = 0x0; // 'Large icon
        public const uint SMALLICON = 0x1; // 'Small icon
        public const uint USEFILEATTRIBUTES = 0x000000010;
        public const uint ADDOVERLAYS = 0x000000020;
        public const uint LINKOVERLAY = 0x000008000; // Show shortcut overlay
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential)]
    internal struct SHFILEINFO
#else

    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
#endif
    {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    };

#if !INTEROP

    internal struct DROPFILES
#else

    public struct DROPFILES
#endif
    {
        public uint pFiles;
        public POINT pt;

        [MarshalAs(UnmanagedType.Bool)]
        public bool fNC;

        [MarshalAs(UnmanagedType.Bool)]
        public bool fWide;
    };

    // ShellExecute and FindExecutable error codes
#if !INTEROP

    internal struct SE_ERR
#else

    public struct SE_ERR
#endif
    {
        public const int FNF = 2;      // file not found
        public const int NOASSOC = 31; // no association available
        public const int OOM = 8;      // out of memory
    }

    /// <summary>
    /// Flags controlling the appearance of a window
    /// </summary>
#if !INTEROP

    internal enum WindowShowCommand : uint
#else

    public enum WindowShowCommand : uint
#endif
    {
        /// <summary>
        /// Hides the window and activates another window.
        /// </summary>
        Hide = 0,

        /// <summary>
        /// Activates and displays the window (including restoring
        /// it to its original size and position).
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Minimizes the window.
        /// </summary>
        Minimized = 2,

        /// <summary>
        /// Maximizes the window.
        /// </summary>
        Maximized = 3,

        /// <summary>
        /// Similar to <see cref="Normal"/>, except that the window
        /// is not activated.
        /// </summary>
        ShowNoActivate = 4,

        /// <summary>
        /// Activates the window and displays it in its current size
        /// and position.
        /// </summary>
        Show = 5,

        /// <summary>
        /// Minimizes the window and activates the next top-level window.
        /// </summary>
        Minimize = 6,

        /// <summary>
        /// Minimizes the window and does not activate it.
        /// </summary>
        ShowMinimizedNoActivate = 7,

        /// <summary>
        /// Similar to <see cref="Normal"/>, except that the window is not
        /// activated.
        /// </summary>
        ShowNA = 8,

        /// <summary>
        /// Activates and displays the window, restoring it to its original
        /// size and position.
        /// </summary>
        Restore = 9,

        /// <summary>
        /// Sets the show state based on the initial value specified when
        /// the process was created.
        /// </summary>
        Default = 10,

        /// <summary>
        /// Minimizes a window, even if the thread owning the window is not
        /// responding.  Use this only to minimize windows from a different
        /// thread.
        /// </summary>
        ForceMinimize = 11
    }

#if !INTEROP

    [ComImport,
    Guid(Shell32.IShellItem2),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellItem2 : IShellItem
#else

    [ComImport,
    Guid(Shell32.IShellItem2),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IShellItem2 : IShellItem
#endif
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Compare(
            [In, MarshalAs(UnmanagedType.Interface)] IShellItem psi,
            [In] uint hint,
            out int piOrder);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        int GetPropertyStore(
            [In] Shell32.GETPROPERTYSTOREFLAGS Flags,
            [In] ref Guid riid,
            [Out, MarshalAs(UnmanagedType.Interface)] out IPropertyStore ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetPropertyStoreWithCreateObject([In] Shell32.GETPROPERTYSTOREFLAGS Flags, [In, MarshalAs(UnmanagedType.IUnknown)] object punkCreateObject, [In] ref Guid riid, out IntPtr ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetPropertyStoreForKeys([In] ref PropertyKey rgKeys, [In] uint cKeys, [In] Shell32.GETPROPERTYSTOREFLAGS Flags, [In] ref Guid riid, [Out, MarshalAs(UnmanagedType.IUnknown)] out IPropertyStore ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetPropertyDescriptionList([In] ref PropertyKey keyType, [In] ref Guid riid, out IntPtr ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Update([In, MarshalAs(UnmanagedType.Interface)] IBindCtx pbc);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetProperty([In] ref PropertyKey key, out PropVariant ppropvar);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetCLSID([In] ref PropertyKey key, out Guid pclsid);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetFileTime([In] ref PropertyKey key, out System.Runtime.InteropServices.ComTypes.FILETIME pft);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetInt32([In] ref PropertyKey key, out int pi);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int GetString([In] ref PropertyKey key, [MarshalAs(UnmanagedType.LPWStr)] out string ppsz);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetUInt32([In] ref PropertyKey key, out uint pui);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetUInt64([In] ref PropertyKey key, out ulong pull);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetBool([In] ref PropertyKey key, out int pf);
    }

#if !INTEROP

    [ComImport,
    Guid(Shell32.IShellItem),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IShellItem
#else

    [ComImport,
    Guid(Shell32.IShellItem),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IShellItem
#endif
    {
        // Not supported: IBindCtx.
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int BindToHandler(
            [In] IntPtr pbc,
            [In] ref Guid bhid,
            [In] ref Guid riid,
            [Out, MarshalAs(UnmanagedType.Interface)] out IShellFolder ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetParent([MarshalAs(UnmanagedType.Interface)] out IShellItem ppsi);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int GetDisplayName(
            [In] Shell32.SIGDN sigdnName,
            out IntPtr ppszName);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetAttributes([In] Shell32.SFGAO sfgaoMask, out Shell32.SFGAO psfgaoAttribs);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int Compare(
            [In, MarshalAs(UnmanagedType.Interface)] IShellItem psi,
            [In] Shell32.SICHINTF hint,
            out int piOrder);
    }

#if !INTEROP

    [ComImport,
  Guid(Shell32.IPropertyStore),
  InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyStore
#else

    [ComImport,
       Guid(Shell32.IPropertyStore),
       InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPropertyStore
#endif
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetCount([Out] out uint cProps);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetAt([In] uint iProp, out PropertyKey pkey);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetValue([In] ref PropertyKey key, out PropVariant pv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
        [return: MarshalAs(UnmanagedType.I4)]
        int SetValue([In] ref PropertyKey key, [In] ref PropVariant pv);

        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int Commit();
    }

    /// <summary>
    /// Defines a unique key for a Shell Property
    /// </summary>
#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct PropertyKey : IEquatable<PropertyKey>
#else

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PropertyKey : IEquatable<PropertyKey>
#endif
    {
        #region Private Fields

        private Guid formatId;
        private Int32 propertyId;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// A unique GUID for the property
        /// </summary>
        public Guid FormatId
        {
            get
            {
                return formatId;
            }
        }

        /// <summary>
        ///  Property identifier (PID)
        /// </summary>
        public Int32 PropertyId
        {
            get
            {
                return propertyId;
            }
        }

        #endregion Public Properties

        #region Public Construction

        /// <summary>
        /// PropertyKey Constructor
        /// </summary>
        /// <param name="formatId">A unique GUID for the property</param>
        /// <param name="propertyId">Property identifier (PID)</param>
        public PropertyKey(Guid formatId, Int32 propertyId)
        {
            this.formatId = formatId;
            this.propertyId = propertyId;
        }

        /// <summary>
        /// PropertyKey Constructor
        /// </summary>
        /// <param name="formatId">A string represenstion of a GUID for the property</param>
        /// <param name="propertyId">Property identifier (PID)</param>
        public PropertyKey(string formatId, Int32 propertyId)
        {
            this.formatId = new Guid(formatId);
            this.propertyId = propertyId;
        }

        // Convenience ctor to initialize Windows Ribbon framework property key.
        public PropertyKey(Int32 index, VarEnum id)
        {
            this.formatId = new Guid(index, 0x7363, 0x696e, new byte[] { 0x84, 0x41, 0x79, 0x8a, 0xcf, 0x5a, 0xeb, 0xb7 });
            this.propertyId = Convert.ToInt32(id);
        }

        #endregion Public Construction

        #region IEquatable<PropertyKey> Members

        /// <summary>
        /// Returns whether this object is equal to another. This is vital for performance of value types.
        /// </summary>
        /// <param name="other">The object to compare against.</param>
        /// <returns>Equality result.</returns>
        public bool Equals(PropertyKey other)
        {
            return other.Equals((object)this);
        }

        #endregion IEquatable<PropertyKey> Members

        #region equality and hashing

        /// <summary>
        /// Returns the hash code of the object. This is vital for performance of value types.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return formatId.GetHashCode() ^ propertyId;
        }

        /// <summary>
        /// Returns whether this object is equal to another. This is vital for performance of value types.
        /// </summary>
        /// <param name="obj">The object to compare against.</param>
        /// <returns>Equality result.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is PropertyKey))
                return false;

            PropertyKey other = (PropertyKey)obj;
            return other.formatId.Equals(formatId) && (other.propertyId == propertyId);
        }

        /// <summary>
        /// Implements the == (equality) operator.
        /// </summary>
        /// <param name="a">Object a.</param>
        /// <param name="b">Object b.</param>
        /// <returns>true if object a equals object b. false otherwise.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        public static bool operator ==(PropertyKey a, PropertyKey b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Implements the != (inequality) operator.
        /// </summary>
        /// <param name="a">Object a.</param>
        /// <param name="b">Object b.</param>
        /// <returns>true if object a does not equal object b. false otherwise.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "b"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "a")]
        public static bool operator !=(PropertyKey a, PropertyKey b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Override ToString() to provide a user friendly string representation
        /// </summary>
        /// <returns>String representing the property key</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
        public override string ToString()
        {
            return String.Format("{0}, {1}", formatId.ToString("B"), propertyId);
        }

        #endregion equality and hashing

        private static System.Collections.Generic.Dictionary<PropertyKey, GCHandle> s_pinnedCache =
            new System.Collections.Generic.Dictionary<PropertyKey, GCHandle>(16);

        public IntPtr ToPointer()
        {
            if (!s_pinnedCache.ContainsKey(this))
            {
                s_pinnedCache.Add(this, GCHandle.Alloc(this, GCHandleType.Pinned));
            }

            return s_pinnedCache[this].AddrOfPinnedObject();
        }
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential)]
    internal class PropertyKeyRef
#else

    [StructLayout(LayoutKind.Sequential)]
    public class PropertyKeyRef
#endif
    {
        public PropertyKey PropertyKey;

        public static PropertyKeyRef From(PropertyKey value)
        {
            PropertyKeyRef obj = new PropertyKeyRef();
            obj.PropertyKey = value;
            return obj;
        }
    }

#if !INTEROP

    [ComImport,
    Guid(Shell32.IShellFolder),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    ComConversionLoss]
    internal interface IShellFolder
#else

    [ComImport,
    Guid(Shell32.IShellFolder),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    ComConversionLoss]
    public interface IShellFolder
#endif
    {
        [PreserveSig]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        int BindToObject([In] IntPtr pidl, /*[In, MarshalAs(UnmanagedType.Interface)] IBindCtx*/ IntPtr pbc, [In] ref Guid riid, [Out, MarshalAs(UnmanagedType.Interface)] out IShellFolder ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void BindToStorage([In] ref IntPtr pidl, [In, MarshalAs(UnmanagedType.Interface)] IBindCtx pbc, [In] ref Guid riid, out IntPtr ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CompareIDs([In] IntPtr lParam, [In] ref IntPtr pidl1, [In] ref IntPtr pidl2);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void CreateViewObject([In] IntPtr hwndOwner, [In] ref Guid riid, out IntPtr ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetAttributesOf([In] uint cidl, [In] IntPtr apidl, [In, Out] ref uint rgfInOut);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetUIObjectOf([In] IntPtr hwndOwner, [In] uint cidl, [In] IntPtr apidl, [In] ref Guid riid, [In, Out] ref uint rgfReserved, out IntPtr ppv);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetDisplayNameOf([In] ref IntPtr pidl, [In] uint uFlags, out IntPtr pName);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SetNameOf([In] IntPtr hwnd, [In] ref IntPtr pidl, [In, MarshalAs(UnmanagedType.LPWStr)] string pszName, [In] uint uFlags, [Out] IntPtr ppidlOut);
    }

#pragma warning restore
}