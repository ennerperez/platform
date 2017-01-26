//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Text;


//internal static class ExternDll
//{
//    internal const string Gdi32 = "gdi32.dll";
//    internal const string User32 = "user32.dll";
//}

//internal static partial class NativeMethods
//{

//    internal enum GraphicsMode : int
//    {
//        Compatible = 1,
//        Advanced = 2
//    }

//    internal enum Win32Messages : uint
//    {
//        WM_NULL = 0x0,
//        WM_CREATE = 0x1,
//        WM_DESTROY = 0x2,
//        WM_MOVE = 0x3,
//        WM_SIZE = 0x5,
//        WM_ACTIVATE = 0x6,
//        WM_SETFOCUS = 0x7,
//        WM_KILLFOCUS = 0x8,
//        WM_ENABLE = 0xa,
//        WM_SETREDRAW = 0xb,
//        WM_SETTEXT = 0xc,
//        WM_GETTEXT = 0xd,
//        WM_GETTEXTLENGTH = 0xe,
//        WM_PAINT = 0xf,
//        WM_CLOSE = 0x10,
//        WM_QUERYENDSESSION = 0x11,
//        WM_QUERYOPEN = 0x13,
//        WM_ENDSESSION = 0x16,
//        WM_QUIT = 0x12,
//        WM_ERASEBKGND = 0x14,
//        WM_SYSCOLORCHANGE = 0x15,
//        WM_SHOWWINDOW = 0x18,
//        WM_WININICHANGE = 0x1a,
//        WM_SETTINGCHANGE = WM_WININICHANGE,
//        WM_DEVMODECHANGE = 0x1b,
//        WM_ACTIVATEAPP = 0x1c,
//        WM_FONTCHANGE = 0x1d,
//        WM_TIMECHANGE = 0x1e,
//        WM_CANCELMODE = 0x1f,
//        WM_SETCURSOR = 0x20,
//        WM_MOUSEACTIVATE = 0x21,
//        WM_CHILDACTIVATE = 0x22,
//        WM_QUEUESYNC = 0x23,
//        WM_GETMINMAXINFO = 0x24,
//        WM_PAINTICON = 0x26,
//        WM_ICONERASEBKGND = 0x27,
//        WM_NEXTDLGCTL = 0x28,
//        WM_SPOOLERSTATUS = 0x2a,
//        WM_DRAWITEM = 0x2b,
//        WM_MEASUREITEM = 0x2c,
//        WM_DELETEITEM = 0x2d,
//        WM_VKEYTOITEM = 0x2e,
//        WM_CHARTOITEM = 0x2f,
//        WM_SETFONT = 0x30,
//        WM_GETFONT = 0x31,
//        WM_SETHOTKEY = 0x32,
//        WM_GETHOTKEY = 0x33,
//        WM_QUERYDRAGICON = 0x37,
//        WM_COMPAREITEM = 0x39,
//        WM_GETOBJECT = 0x3d,
//        WM_COMPACTING = 0x41,
//        WM_COMMNOTIFY = 0x44,
//        WM_WINDOWPOSCHANGING = 0x46,
//        WM_WINDOWPOSCHANGED = 0x47,
//        WM_POWER = 0x48,
//        WM_COPYDATA = 0x4a,
//        WM_CANCELJOURNAL = 0x4b,
//        WM_NOTIFY = 0x4e,
//        WM_INPUTLANGCHANGEREQUEST = 0x50,
//        WM_INPUTLANGCHANGE = 0x51,
//        WM_TCARD = 0x52,
//        WM_HELP = 0x53,
//        WM_USERCHANGED = 0x54,
//        WM_NOTIFYFORMAT = 0x55,
//        WM_CONTEXTMENU = 0x7b,
//        WM_STYLECHANGING = 0x7c,
//        WM_STYLECHANGED = 0x7d,
//        WM_DISPLAYCHANGE = 0x7e,
//        WM_GETICON = 0x7f,
//        WM_SETICON = 0x80,
//        WM_NCCREATE = 0x81,
//        WM_NCDESTROY = 0x82,
//        WM_NCCALCSIZE = 0x83,
//        WM_NCHITTEST = 0x84,
//        WM_NCPAINT = 0x85,
//        WM_NCACTIVATE = 0x86,
//        WM_GETDLGCODE = 0x87,
//        WM_SYNCPAINT = 0x88,
//        WM_NCMOUSEMOVE = 0xa0,
//        WM_NCLBUTTONDOWN = 0xa1,
//        WM_NCLBUTTONUP = 0xa2,
//        WM_NCLBUTTONDBLCLK = 0xa3,
//        WM_NCRBUTTONDOWN = 0xa4,
//        WM_NCRBUTTONUP = 0xa5,
//        WM_NCRBUTTONDBLCLK = 0xa6,
//        WM_NCMBUTTONDOWN = 0xa7,
//        WM_NCMBUTTONUP = 0xa8,
//        WM_NCMBUTTONDBLCLK = 0xa9,
//        WM_NCXBUTTONDOWN = 0xab,
//        WM_NCXBUTTONUP = 0xac,
//        WM_NCXBUTTONDBLCLK = 0xad,
//        WM_INPUT = 0xff,
//        WM_KEYFIRST = 0x100,
//        WM_KEYDOWN = 0x100,
//        WM_KEYUP = 0x101,
//        WM_CHAR = 0x102,
//        WM_DEADCHAR = 0x103,
//        WM_SYSKEYDOWN = 0x104,
//        WM_SYSKEYUP = 0x105,
//        WM_SYSCHAR = 0x106,
//        WM_SYSDEADCHAR = 0x107,
//        WM_UNICHAR = 0x109,
//        WM_KEYLAST = 0x108,
//        WM_IME_STARTCOMPOSITION = 0x10d,
//        WM_IME_ENDCOMPOSITION = 0x10e,
//        WM_IME_COMPOSITION = 0x10f,
//        WM_IME_KEYLAST = 0x10f,
//        WM_INITDIALOG = 0x110,
//        WM_COMMAND = 0x111,
//        WM_SYSCOMMAND = 0x112,
//        WM_TIMER = 0x113,
//        WM_HSCROLL = 0x114,
//        WM_VSCROLL = 0x115,
//        WM_INITMENU = 0x116,
//        WM_INITMENUPOPUP = 0x117,
//        WM_MENUSELECT = 0x11f,
//        WM_MENUCHAR = 0x120,
//        WM_ENTERIDLE = 0x121,
//        WM_MENURBUTTONUP = 0x122,
//        WM_MENUDRAG = 0x123,
//        WM_MENUGETOBJECT = 0x124,
//        WM_UNINITMENUPOPUP = 0x125,
//        WM_MENUCOMMAND = 0x126,
//        WM_CHANGEUISTATE = 0x127,
//        WM_UPDATEUISTATE = 0x128,
//        WM_QUERYUISTATE = 0x129,
//        WM_CTLCOLOR = 0x19,
//        WM_CTLCOLORMSGBOX = 0x132,
//        WM_CTLCOLOREDIT = 0x133,
//        WM_CTLCOLORLISTBOX = 0x134,
//        WM_CTLCOLORBTN = 0x135,
//        WM_CTLCOLORDLG = 0x136,
//        WM_CTLCOLORSCROLLBAR = 0x137,
//        WM_CTLCOLORSTATIC = 0x138,
//        WM_MOUSEFIRST = 0x200,
//        WM_MOUSEMOVE = 0x200,
//        WM_LBUTTONDOWN = 0x201,
//        WM_LBUTTONUP = 0x202,
//        WM_LBUTTONDBLCLK = 0x203,
//        WM_RBUTTONDOWN = 0x204,
//        WM_RBUTTONUP = 0x205,
//        WM_RBUTTONDBLCLK = 0x206,
//        WM_MBUTTONDOWN = 0x207,
//        WM_MBUTTONUP = 0x208,
//        WM_MBUTTONDBLCLK = 0x209,
//        WM_MOUSEWHEEL = 0x20a,
//        WM_XBUTTONDOWN = 0x20b,
//        WM_XBUTTONUP = 0x20c,
//        WM_XBUTTONDBLCLK = 0x20d,
//        WM_MOUSELAST = 0x20d,
//        WM_PARENTNOTIFY = 0x210,
//        WM_ENTERMENULOOP = 0x211,
//        WM_EXITMENULOOP = 0x212,
//        WM_NEXTMENU = 0x213,
//        WM_SIZING = 0x214,
//        WM_CAPTURECHANGED = 0x215,
//        WM_MOVING = 0x216,
//        WM_POWERBROADCAST = 0x218,
//        WM_DEVICECHANGE = 0x219,
//        WM_MDICREATE = 0x220,
//        WM_MDIDESTROY = 0x221,
//        WM_MDIACTIVATE = 0x222,
//        WM_MDIRESTORE = 0x223,
//        WM_MDINEXT = 0x224,
//        WM_MDIMAXIMIZE = 0x225,
//        WM_MDITILE = 0x226,
//        WM_MDICASCADE = 0x227,
//        WM_MDIICONARRANGE = 0x228,
//        WM_MDIGETACTIVE = 0x229,
//        WM_MDISETMENU = 0x230,
//        WM_ENTERSIZEMOVE = 0x231,
//        WM_EXITSIZEMOVE = 0x232,
//        WM_DROPFILES = 0x233,
//        WM_MDIREFRESHMENU = 0x234,
//        WM_IME_SETCONTEXT = 0x281,
//        WM_IME_NOTIFY = 0x282,
//        WM_IME_CONTROL = 0x283,
//        WM_IME_COMPOSITIONFULL = 0x284,
//        WM_IME_SELECT = 0x285,
//        WM_IME_CHAR = 0x286,
//        WM_IME_REQUEST = 0x288,
//        WM_IME_KEYDOWN = 0x290,
//        WM_IME_KEYUP = 0x291,
//        WM_MOUSEHOVER = 0x2a1,
//        WM_MOUSELEAVE = 0x2a3,
//        WM_NCMOUSELEAVE = 0x2a2,
//        WM_WTSSESSION_CHANGE = 0x2b1,
//        WM_TABLET_FIRST = 0x2c0,
//        WM_TABLET_LAST = 0x2df,
//        WM_CUT = 0x300,
//        WM_COPY = 0x301,
//        WM_PASTE = 0x302,
//        WM_CLEAR = 0x303,
//        WM_UNDO = 0x304,
//        WM_RENDERFORMAT = 0x305,
//        WM_RENDERALLFORMATS = 0x306,
//        WM_DESTROYCLIPBOARD = 0x307,
//        WM_DRAWCLIPBOARD = 0x308,
//        WM_PAINTCLIPBOARD = 0x309,
//        WM_VSCROLLCLIPBOARD = 0x30a,
//        WM_SIZECLIPBOARD = 0x30b,
//        WM_ASKCBFORMATNAME = 0x30c,
//        WM_CHANGECBCHAIN = 0x30d,
//        WM_HSCROLLCLIPBOARD = 0x30e,
//        WM_QUERYNEWPALETTE = 0x30f,
//        WM_PALETTEISCHANGING = 0x310,
//        WM_PALETTECHANGED = 0x311,
//        WM_HOTKEY = 0x312,
//        WM_PRINT = 0x317,
//        WM_PRINTCLIENT = 0x318,
//        WM_APPCOMMAND = 0x319,
//        WM_THEMECHANGED = 0x31a,
//        WM_HANDHELDFIRST = 0x358,
//        WM_HANDHELDLAST = 0x35f,
//        WM_AFXFIRST = 0x360,
//        WM_AFXLAST = 0x37f,
//        WM_PENWINFIRST = 0x380,
//        WM_PENWINLAST = 0x38f,
//        WM_USER = 0x400,
//        WM_REFLECT = 0x2000,
//        WM_APP = 0x8000
//    }

//    /// <summary>
//    /// The SetGraphicsMode function sets the graphics mode for the specified device context.
//    /// </summary>
//    /// <param name="hdc">A handle to the device context.</param>
//    /// <param name="iMode">The graphics mode.</param>
//    /// <remarks>https://msdn.microsoft.com/en-us/library/dd162977(v=vs.85).aspx</remarks>
//    /// <returns>
//    /// If the function succeeds, the return value is the old graphics mode.
//    /// If the function fails, the return value is zero.
//    /// </returns>
//    [DllImport(ExternDll.Gdi32)]
//    internal static extern GraphicsMode SetGraphicsMode(IntPtr hdc, GraphicsMode iMode);

//    /// <summary>
//    /// The GetWorldTransform function retrieves the current world-space to page-space transformation.
//    /// </summary>
//    /// <param name="hdc">A handle to the device context.</param>
//    /// <param name="lpXform">A pointer to an XFORM structure that receives the current world-space to page-space transformation.</param>
//    /// <remarks>https://msdn.microsoft.com/en-us/library/dd144953(v=vs.85).aspx</remarks>
//    /// <returns>
//    /// If the function succeeds, the return value is nonzero.
//    /// If the function fails, the return value is zero.
//    /// </returns>
//    [DllImport(ExternDll.Gdi32, SetLastError = true)]
//    internal static extern bool GetWorldTransform(IntPtr hdc, [Out] out XFORM lpXform);

//    internal struct XFORM
//    {
//        internal float eM11;
//        internal float eM12;
//        internal float eM21;
//        internal float eM22;
//        internal float eDx;
//        internal float eDy;
//    }

//    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
//    internal class MENUITEMINFO_T_RW
//    {
//        internal int cbSize = Marshal.SizeOf(typeof(MENUITEMINFO_T_RW));
//        internal int fMask = 0x00000080; //MIIM_BITMAP = 0x00000080
//        internal int fType;
//        internal int fState;
//        internal int wID;
//        internal IntPtr hSubMenu = IntPtr.Zero;
//        internal IntPtr hbmpChecked = IntPtr.Zero;
//        internal IntPtr hbmpUnchecked = IntPtr.Zero;
//        internal IntPtr dwItemData = IntPtr.Zero;
//        internal IntPtr dwTypeData = IntPtr.Zero;
//        internal int cch;
//        internal IntPtr hbmpItem = IntPtr.Zero;
//    }

//    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
//    internal class MENUINFO
//    {
//        internal int cbSize = Marshal.SizeOf(typeof(MENUINFO));
//        internal int fMask = 0x00000010; //MIM_STYLE;
//        internal int dwStyle = 0x04000000; //MNS_CHECKORBMP;
//        internal uint cyMax;
//        internal IntPtr hbrBack = IntPtr.Zero;
//        internal int dwContextHelpID;
//        internal IntPtr dwMenuData = IntPtr.Zero;
//    }

//    internal struct GETTEXTLENGTHEX
//    {
//        internal Int32 flags;
//        internal Int32 codepage;
//    }

//    /// <summary>
//    /// Window field offsets for GetWindowLong().
//    /// </summary>
//    internal struct GWL
//    {
//        internal const int WNDPROC = -4;
//        internal const int HINSTANCE = -6;
//        internal const int HWNDPARENT = -8;
//        internal const int STYLE = -16;
//        internal const int EXSTYLE = -20;
//        internal const int USERDATA = -21;
//        internal const int ID = -12;
//    }

//    /// <summary>
//    /// Constants for window styles
//    /// </summary>
//    internal struct WS
//    {
//        internal const UInt32 CHILD = 0x40000000;
//        internal const UInt32 VISIBLE = 0x10000000;
//        internal const UInt32 CLIPCHILDREN = 0x02000000;
//        internal const UInt32 CLIPSIBLINGS = 0x04000000;
//        internal const UInt32 HSCROLL = 0x00100000;
//        internal const UInt32 POPUP = 0x80000000;
//        internal const UInt32 TABSTOP = 0x00010000;
//        internal const UInt32 VSCROLL = 0x00200000;
//        internal const UInt32 EX_TOOLWINDOW = 0x00000080;
//        internal const UInt32 EX_APPWINDOW = 0x00040000;
//        internal const UInt32 EX_TOPMOST = 0x00000008;
        
//        internal const UInt32 BORDER = 0x00800000;
//        internal const UInt32 THICKFRAME = 0x00040000;
//        internal const UInt32 SYSMENU = 0x00080000;
//        internal const UInt32 MINIMIZEBOX = 0x00020000;
//        internal const UInt32 MAXIMIZEBOX = 0x00010000;
//    }

//    /// <summary>
//    /// The SetWorldTransform function sets a two-dimensional linear transformation between world space and page space for the specified device context. This transformation can be used to scale, rotate, shear, or translate graphics output.
//    /// </summary>
//    /// <param name="hdc">A handle to the device context.</param>
//    /// <param name="lpXform">A pointer to an XFORM structure that contains the transformation data.</param>
//    /// <remarks>https://msdn.microsoft.com/en-us/library/dd145104(v=vs.85).aspx</remarks>
//    /// <returns>
//    /// If the function succeeds, the return value is nonzero.
//    /// If the function fails, the return value is zero.
//    /// </returns>
//    [DllImport(ExternDll.Gdi32, SetLastError = true)]
//    internal static extern bool SetWorldTransform(IntPtr hdc, [In] ref XFORM lpXform);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern long SendMessage(IntPtr hWnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern IntPtr SendMessage(IntPtr hWnd, Win32Messages messagetype, IntPtr wParam, IntPtr lParam);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern bool SetMenuItemInfo(HandleRef hMenu, int uItem, bool fByPosition, MENUITEMINFO_T_RW lpmii);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern bool SetMenuInfo(HandleRef hMenu, MENUINFO lpcmi);

//    [DllImport(ExternDll.Gdi32, CharSet = CharSet.Auto)]
//    internal static extern bool DeleteObject(IntPtr hObject);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern int SendMessage(HandleRef hWnd, UInt32 Msg, ref int wParam, StringBuilder lParam);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern int SendMessage(HandleRef hWnd, UInt32 Msg, IntPtr wParam, string lParam);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern int SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, string lParam);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern int SendMessage(HandleRef hWnd, UInt32 Msg, IntPtr wParam, bool lParam);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern int SendMessage(HandleRef hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

//    //[DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    //internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

//    // declaration for EnumWindows
//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern bool EnumWindows(EnumWindowsDelegate lpEnumFunc, GCHandle lParam);

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsDelegate lpEnumFunc, GCHandle lParam);

//    /// <summary>
//    /// The GetWindowLong function retrieves information about the specified window. The
//    /// function also retrieves the 32-bit (long) value at the specified offset into the extra
//    /// window memory.
//    /// </summary>
//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
//    internal static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

//    // Gets the text length from a text box
//    [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
//    internal static extern int GetTextLength(IntPtr hWnd, int Msg, ref GETTEXTLENGTHEX wParam, StringBuilder lParam);

//    internal const int BS_COMMANDLINK = 0x0000000E;
//    internal const uint BCM_SETNOTE = 0x00001609;
//    internal const uint BCM_GETNOTE = 0x0000160A;
//    internal const uint BCM_GETNOTELENGTH = 0x0000160B;
//    internal const uint BCM_SETSHIELD = 0x0000160C;
//    internal const int BM_SETIMAGE = 0x00F7;

//    // delegate signagure for EnumWindows callback
//    internal delegate bool EnumWindowsDelegate(IntPtr hwnd, GCHandle lParam);

//}