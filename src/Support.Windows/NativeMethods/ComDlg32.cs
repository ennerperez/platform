using System;
using System.Runtime.InteropServices;

namespace Platform.Support.Windows
{
#if !INTEROP
    internal class ComDlg32
#else

    public class ComDlg32
#endif
    {
        [DllImport(ExternDll.ComDlg32, CharSet = CharSet.Unicode)]
        public static extern bool GetOpenFileName(ref OPENFILENAME ofn);

        [DllImport(ExternDll.ComDlg32, CharSet = CharSet.Unicode)]
        public static extern Int32 CommDlgExtendedError();
    }

    #region Structures

    //for commong dialogs
    /// <summary>
    /// See the documentation for OPENFILENAME
    /// </summary>
#if !INTEROP
    internal struct OPENFILENAME
#else

    public struct OPENFILENAME
#endif
    {
        public Int32 lStructSize;
        public IntPtr hwndOwner;
        public IntPtr hInstance;
        public IntPtr lpstrFilter;
        public IntPtr lpstrCustomFilter;
        public Int32 nMaxCustFilter;
        public Int32 nFilterIndex;
        public IntPtr lpstrFile;
        public Int32 nMaxFile;
        public IntPtr lpstrFileTitle;
        public Int32 nMaxFileTitle;
        public IntPtr lpstrInitialDir;
        public IntPtr lpstrTitle;
        public Int32 Flags;
        public Int16 nFileOffset;
        public Int16 nFileExtension;
        public IntPtr lpstrDefExt;
        public Int32 lCustData;
        public OfnHookProc lpfnHook;
        public IntPtr lpTemplateName;
        public IntPtr pvReserved;
        public Int32 dwReserved;
        public Int32 FlagsEx;
    };

    #endregion Structures
}