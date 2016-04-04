using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Windows.ComDlg32
{
    public static partial class NativeMethods
    {

        [DllImport(ExternDll.Comdlg32, CharSet = CharSet.Unicode)]
        public static extern bool GetOpenFileName(ref OPENFILENAME ofn);

        [DllImport(ExternDll.Comdlg32, CharSet = CharSet.Unicode)]
        public static extern Int32 CommDlgExtendedError();

    }

    #region Delegates

    public delegate IntPtr OfnHookProc(IntPtr hWnd, UInt32 msg, Int32 wParam, Int32 lParam);

    #endregion

    #region Structures

    //for commong dialogs
    /// <summary>
    /// See the documentation for OPENFILENAME
    /// </summary>
    public struct OPENFILENAME
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

    #endregion

}
