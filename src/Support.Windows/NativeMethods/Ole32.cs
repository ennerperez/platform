// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Platform.Support.Windows
{
#if !INTEROP

    internal class Ole32
#else

    public class Ole32
#endif

    {
        [DllImport(ExternDll.Ole32)]
        public static extern int OleInitialize(IntPtr pvReserved);

        /// <summary>
        /// Frees the specified storage medium (automatically calls
        /// pUnkForRelease if required).
        /// </summary>
        /// <param name="pmedium">Storage medium to be freed</param>
        [DllImport(ExternDll.Ole32)]
        public static extern void ReleaseStgMedium(ref STGMEDIUM pmedium);

        [DllImport(ExternDll.Ole32)]
        public static extern int CoCreateInstance(
            [In] ref Guid rclsid,
            [In] IntPtr pUnkOuter,
            [In] CLSCTX dwClsContext,
            [In] ref Guid riid,
            [Out] out IntPtr pUnknown);

        [DllImport(ExternDll.Ole32)]
        public static extern int CoDisconnectObject(
            [In, MarshalAs(UnmanagedType.IUnknown)] object pUnknown,
            uint dwReserved);

        [DllImport(ExternDll.Ole32)]
        public static extern int OleRun(
            [MarshalAs(UnmanagedType.IUnknown)] object pUnknown);

        [DllImport(ExternDll.Ole32)]
        public static extern int OleLockRunning(
            [MarshalAs(UnmanagedType.IUnknown)] object pUnknown,
            [MarshalAs(UnmanagedType.Bool)] bool fLock,
            [MarshalAs(UnmanagedType.Bool)] bool fLastUnlockCloses);

        /// <summary>
        /// Gets the CLSID for an application based upon its progId
        /// </summary>
        [DllImport(ExternDll.Ole32)]
        public static extern int CLSIDFromProgID(
            [MarshalAs(UnmanagedType.LPWStr)] string lpszProgID,
            out Guid pclsid
            );

        [DllImport(ExternDll.Ole32)]
        public static extern IntPtr CoTaskMemAlloc(uint cb);

        [DllImport(ExternDll.Ole32)]
        public static extern int CreateBindCtx(
            [In] uint reserved,
            [Out] out IBindCtx ppbc);

        [DllImport(ExternDll.Ole32)]
        public static extern int CreateItemMoniker(
            [MarshalAs(UnmanagedType.LPWStr)] string lpszDelim,
            [MarshalAs(UnmanagedType.LPWStr)] string lpszItem,
            out IMoniker ppmk
            );

        [DllImport(ExternDll.Ole32)]
        public static extern int GetRunningObjectTable(
            int reserved,
            out IRunningObjectTable pprot
            );

        /// <summary>
        /// Initiate a drag and drop operation
        /// </summary>
        [DllImport(ExternDll.Ole32)]
        public static extern int DoDragDrop(
            IOleDataObject pDataObject,  // Pointer to the data object
            IDropSource pDropSource,	  // Pointer to the source
            DROPEFFECT dwOKEffect,       // Effects allowed by the source
            ref DROPEFFECT pdwEffect    // Pointer to effects on the source
            );

        [DllImport(ExternDll.Ole32)]
        public static extern int RegisterDragDrop(
            IntPtr hwnd,  //Handle to a window that can accept drops
            IDropTarget pDropTarget
            //Pointer to object that is to be target of drop
            );

        [DllImport(ExternDll.Ole32)]
        public static extern int RevokeDragDrop(
            IntPtr hwnd  //Handle to a window that can accept drops
            );

        [DllImport(ExternDll.Ole32)]
        public static extern IntPtr OleGetIconOfFile(
            [MarshalAs(UnmanagedType.LPWStr)] string lpszPath,
            [MarshalAs(UnmanagedType.Bool)] bool fUseFileAsLabel);

        [DllImport(ExternDll.Ole32, PreserveSig = false)] // returns hresult
        public extern static void PropVariantCopy([Out] out PropVariant pDst, [In] ref PropVariant pSrc);

        [DllImport(ExternDll.Ole32, PreserveSig = false)] // returns hresult
        internal extern static void PropVariantClear([In, Out] ref PropVariant pvar);
    }

#if !INTEROP

    [Flags]
    internal enum CLSCTX : uint
#else

    [Flags]
    public enum CLSCTX : uint
#endif

    {
        INPROC_SERVER = 0x1,
        INPROC_HANDLER = 0x2,
        LOCAL_SERVER = 0x4,
        INPROC_SERVER16 = 0x8,
        REMOTE_SERVER = 0x10,
        INPROC_HANDLER16 = 0x20,
        RESERVED1 = 0x40,
        RESERVED2 = 0x80,
        RESERVED3 = 0x100,
        RESERVED4 = 0x200,
        NO_CODE_DOWNLOAD = 0x400,
        RESERVED5 = 0x800,
        NO_CUSTOM_MARSHAL = 0x1000,
        ENABLE_CODE_DOWNLOAD = 0x2000,
        NO_FAILURE_LOG = 0x4000,
        DISABLE_AAA = 0x8000,
        ENABLE_AAA = 0x10000,
        FROM_DEFAULT_CONTEXT = 0x20000
    };

    /// <summary>
    /// Version of IOleDataObject that preserves all of the raw signatures so that
    /// implementors can return the appropriate values
    /// </summary>
#if !INTEROP

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000010e-0000-0000-C000-000000000046")]
    internal interface IOleDataObject
#else

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("0000010e-0000-0000-C000-000000000046")]
    public interface IOleDataObject
#endif

    {
        /// <summary>
        /// Renders the data described in a FORMATETC structure and transfers it
        /// through the STGMEDIUM structure.
        /// </summary>
        /// <param name="pFormatEtc">Pointer to the FORMATETC structure that defines
        /// the format, medium, and target device to use when passing the data. It is
        /// possible to specify more than one medium by using the Boolean OR operator,
        /// allowing the method to choose the best medium among those specified</param>
        /// <param name="pMedium">Pointer to the STGMEDIUM structure that indicates
        /// the storage medium containing the returned data through its tymed member,
        /// and the responsibility for releasing the medium through the value of its
        /// pUnkForRelease member. If pUnkForRelease is NULL, the receiver of the medium
        /// is responsible for releasing it; otherwise, pUnkForRelease points to the
        /// IUnknown on the appropriate object so its Release method can be called.
        /// The medium must be allocated and filled in by IDataObject::GetData</param>
        [PreserveSig]
        int GetData(ref FORMATETC pFormatEtc, ref STGMEDIUM pMedium);

        /// <summary>
        /// Renders the data described in a FORMATETC structure and transfers it
        /// through the STGMEDIUM structure allocated by the caller.
        /// </summary>
        /// <param name="pFormatEtc">Pointer to the FORMATETC structure that defines
        /// the format, medium, and target device to use when passing the data. It is
        /// possible to specify more than one medium by using the Boolean OR operator,
        /// allowing the method to choose the best medium among those specified</param>
        /// <param name="pMedium">Pointer to the STGMEDIUM structure that defines the
        /// storage medium containing the data being transferred. The medium must be
        /// allocated by the caller and filled in by IDataObject::GetDataHere. The
        /// caller must also free the medium. The implementation of this method must
        /// always supply a value of NULL for the punkForRelease member of the
        /// STGMEDIUM structure to which this parameter points</param>
        [PreserveSig]
        int GetDataHere(ref FORMATETC pFormatEtc, ref STGMEDIUM pMedium);

        /// <summary>
        /// Determines whether the data object is capable of rendering the data
        /// described in the FORMATETC structure.
        /// </summary>
        /// <param name="pFormatEtc">Pointer to the FORMATETC structure defining
        /// the format, medium, and target device to use for the query</param>
        [PreserveSig]
        int QueryGetData(ref FORMATETC pFormatEtc);

        /// <summary>
        /// Provides a standard FORMATETC structure that is logically equivalent
        /// to one that is more complex. You use this method to determine whether
        /// two different FORMATETC structures would return the same data, removing
        /// the need for duplicate rendering
        /// </summary>
        /// <param name="pFormatEtcIn">Pointer to the FORMATETC structure that
        /// defines the format, medium, and target device that the caller would
        /// like to use to retrieve data in a subsequent call such as
        /// IDataObject::GetData. The TYMED member is not significant in this case
        /// and should be ignored</param>
        /// <param name="pFormatEtcOut">Pointer to a FORMATETC structure that contains
        /// the most general information possible for a specific rendering, making it
        /// canonically equivalent to pFormatetcIn. The caller must allocate this
        /// structure and the GetCanonicalFormatEtc method must fill in the data.
        /// To retrieve data in a subsequent call like IDataObject::GetData, the
        /// caller uses the supplied value of pFormatetcOut, unless the value supplied
        /// is NULL. This value is NULL if the method returns DATA_S_SAMEFORMATETC.
        /// The TYMED member is not significant in this case and should be ignored</param>
        /// <returns>S_OK if the logically equivilant structure was provided,
        /// otherwise returns DATA_S_SAMEFORMATETC indicating the structures
        /// are the same (in this case pFormatEtcOut is NULL)</returns>
        [PreserveSig]
        int GetCanonicalFormatEtc(ref FORMATETC pFormatEtcIn, ref FORMATETC pFormatEtcOut);

        /// <summary>
        /// Provides the source data object with data described by a FORMATETC
        /// structure and an STGMEDIUM structure
        /// </summary>
        /// <param name="pFormatEtc">Pointer to the FORMATETC structure defining the
        /// format used by the data object when interpreting the data contained in the
        /// storage medium</param>
        /// <param name="pMedium">Pointer to the STGMEDIUM structure defining the storage
        /// medium in which the data is being passed</param>
        /// <param name="fRelease">If TRUE, the data object called, which implements
        /// IDataObject::SetData, owns the storage medium after the call returns. This
        /// means it must free the medium after it has been used by calling the
        /// ReleaseStgMedium function. If FALSE, the caller retains ownership of the
        /// storage medium and the data object called uses the storage medium for the
        /// duration of the call only</param>
        [PreserveSig]
        int SetData(ref FORMATETC pFormatEtc, ref STGMEDIUM pMedium,
            [MarshalAs(UnmanagedType.Bool)] bool fRelease);

        /// <summary>
        /// Creates and returns a pointer to an object to enumerate the FORMATETC
        /// supported by the data object
        /// </summary>
        /// <param name="dwDirection">Direction of the data through a value from
        /// the enumeration DATADIR</param>
        /// <param name="ppEnumFormatEtc">Address of IEnumFORMATETC* pointer variable
        /// that receives the interface pointer to the new enumerator object</param>
        [PreserveSig]
        int EnumFormatEtc(DATADIR dwDirection, out IEnumFORMATETC ppEnumFormatEtc);

        /// <summary>
        /// Creates a connection between a data object and an advise sink so the
        /// advise sink can receive notifications of changes in the data object
        /// </summary>
        /// <param name="pFormatEtc">Pointer to a FORMATETC structure that defines the
        /// format, target device, aspect, and medium that will be used for future
        /// notifications. For example, one sink may want to know only when the bitmap
        /// representation of the data in the data object changes. Another sink may be
        /// interested in only the metafile format of the same object. Each advise sink
        /// is notified when the data of interest changes. This data is passed back to
        /// the advise sink when notification occurs</param>
        /// <param name="advf">DWORD that specifies a group of flags for controlling
        /// the advisory connection. Valid values are from the enumeration ADVF.
        /// However, only some of the possible ADVF values are relevant for this
        /// method (see MSDN documentation for more details).</param>
        /// <param name="pAdvSink">Pointer to the IAdviseSink interface on the advisory
        /// sink that will receive the change notification</param>
        /// <param name="pdwConnection">Pointer to a DWORD token that identifies this
        /// connection. You can use this token later to delete the advisory connection
        /// (by passing it to IDataObject::DUnadvise). If this value is zero, the
        /// connection was not established</param>
        [PreserveSig]
        int DAdvise(ref FORMATETC pFormatEtc, uint advf, IntPtr pAdvSink, ref uint pdwConnection);

        /// <summary>
        /// Destroys a notification previously set up with the DAdvise method
        /// </summary>
        /// <param name="dwConnection">DWORD token that specifies the connection to remove.
        /// Use the value returned by IDataObject::DAdvise when the connection was originally
        /// established</param>
        [PreserveSig]
        int DUnadvise(uint dwConnection);

        /// <summary>
        /// Creates and returns a pointer to an object to enumerate the current
        /// advisory connections
        /// </summary>
        /// <param name="ppEnumAdvise">Address of IEnumSTATDATA* pointer variable that
        /// receives the interface pointer to the new enumerator object. If the
        /// implementation sets *ppenumAdvise to NULL, there are no connections to
        /// advise sinks at this time</param>
        [PreserveSig]
        int EnumDAdvise(ref IntPtr ppEnumAdvise);
    }

#if !INTEROP

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000121-0000-0000-C000-000000000046")]
    internal interface IDropSource
#else

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000121-0000-0000-C000-000000000046")]
    public interface IDropSource
#endif

    {
        /// <summary>
        /// Determines whether a drag-and-drop operation should be continued, canceled, or completed. You do not call this method directly. The OLE DoDragDrop function calls this method during a drag-and-drop operation
        /// </summary>
        /// <param name="fEscapePressed">Specifies whether the Esc key has been pressed since the previous call to IDropSource::QueryContinueDrag or to DoDragDrop if this is the first call to QueryContinueDrag. A TRUE value indicates the end user has pressed the escape key; a FALSE value indicates it has not been pressed</param>
        /// <param name="grfKeyState">Current state of the keyboard modifier keys on the keyboard</param>
        /// <returns>S_OK The drag operation should continue. This result occurs if no errors are detected, the mouse button starting the drag-and-drop operation has not been released, and the Esc key has not been detected.
        ///			 DRAGDROP_S.DROP The drop operation should occur completing the drag operation. This result occurs if grfKeyState indicates that the key that started the drag-and-drop operation has been released.
        ///			 DRAGDROP_S.CANCEL   The drag operation should be canceled with no drop operation occurring. This result occurs if fEscapePressed is TRUE, indicating the Esc key has been pressed</returns>
        [PreserveSig]
        int QueryContinueDrag(
            [In] bool fEscapePressed,
            [In] uint grfKeyState);

        /// <summary>
        /// Enables a source application to give visual feedback to the end user during a drag-and-drop operation by providing the DoDragDrop function with an enumeration value specifying the visual effect
        /// </summary>
        /// <param name="dwEffect">The DROPEFFECT value returned by the most recent call to IDropTarget::DragEnter, IDropTarget::DragOver, or IDropTarget::DragLeave</param>
        /// <returns>S_OK The method completed its task successfully, using the cursor set by the source application.
        ///   	     DRAGDROP_S.USEDEFAULTCURSORS Indicates successful completion of the method, and requests OLE to update the cursor using the OLE-provided default cursors</returns>
        [PreserveSig]
        int GiveFeedback(
            [In] DROPEFFECT dwEffect);
    }

    /// <summary>
    /// The DoDragDrop function and many of the methods in the IDropSource and
    /// IDropTarget interfaces pass information about the effects of a drag-and-drop
    /// operation in a DROPEFFECT enumeration. Valid drop-effect values are the
    /// result of applying the OR operation to the values contained in the DROPEFFECT
    /// enumeration
    /// </summary>
#if !INTEROP

    [Flags]
    internal enum DROPEFFECT : uint
#else

    [Flags]
    public enum DROPEFFECT : uint
#endif

    {
        /// <summary>
        /// Drop target cannot accept the data.
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Drop results in a copy. The original data is untouched by the drag source.
        /// </summary>
        COPY = 1,

        /// <summary>
        /// Drag source should remove the data.
        /// </summary>
        MOVE = 2,

        /// <summary>
        /// Drag source should create a link to the original data.
        /// </summary>
        LINK = 4,

        /// <summary>
        /// Scrolling is about to start or is currently occurring in the target.
        /// This value is used in addition to the other values.
        /// </summary>
        SCROLL = 0x80000000
    };

#if !INTEROP

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000122-0000-0000-C000-000000000046")]
    internal interface IDropTarget
#else

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00000122-0000-0000-C000-000000000046")]
    public interface IDropTarget
#endif

    {
        void DragEnter(
            [In] IOleDataObject pDataObj,
            [In] MK grfKeyState,
            [In] POINT pt,
            [Out, In] ref DROPEFFECT pdwEffect);

        void DragOver(
            [In] MK grfKeyState,
            [In] POINT pt,
            [Out, In] ref DROPEFFECT pdwEffect);

        void DragLeave();

        void Drop(
            [In] IOleDataObject pDataObj,
            [In] MK grfKeyState,
            [In] POINT pt,
            [Out, In] ref DROPEFFECT pdwEffect);
    }
}