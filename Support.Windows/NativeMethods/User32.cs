// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Windows
{
#pragma warning disable CS0649

    /// <summary>
    /// Imports from User32.dll
    /// </summary>
#if !INTEROP

    internal class User32
#else

    public class User32
#endif
    {
        [DllImport(ExternDll.User32, ExactSpelling = true)]
        public static extern int GetKeyboardLayout(int dwLayout);

        [DllImport(ExternDll.User32)]
        public static extern bool SetMenu(IntPtr hWnd, IntPtr hMenu);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport(ExternDll.User32)]
        public static extern int GetClassName(IntPtr hWnd, [Out] StringBuilder lpClassName, int nMaxCount);

        [DllImport(ExternDll.User32)]
        public static extern int GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            public bool fIcon;
            public Int32 xHotspot;
            public Int32 yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [DllImport(ExternDll.User32)]
        public static extern bool ReleaseCapture();

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetCapture();

        [DllImport(ExternDll.User32)]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport(ExternDll.User32)]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadCursor(
            IntPtr hInstance,
            IntPtr lpCursorName
            );

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern int LoadCursor(int hInstance, int lpCursorName);

        [DllImport(ExternDll.User32)]
        public static extern bool OpenClipboard(IntPtr hWnd);

        [DllImport(ExternDll.User32)]
        public static extern int CountClipboardFormats();

        [DllImport(ExternDll.User32)]
        public static extern bool EmptyClipboard();

        [DllImport(ExternDll.User32)]
        public static extern bool CloseClipboard();

        [DllImport(ExternDll.User32)]
        public static extern bool SetSystemCursor(IntPtr hcur, uint id);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr SetCursor(IntPtr hcur);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern int SetCursor(int hCursor);

        [DllImport(ExternDll.User32)]
        public static extern bool EnableWindow(IntPtr hcur, bool bEnable);

        [DllImport(ExternDll.User32)]
        public static extern uint MsgWaitForMultipleObjects(
            uint nCount,
            IntPtr[] pHandles,
            bool bWaitAll,
            uint dwMilliseconds,
            QS dwWakeMask
        );

        [DllImport(ExternDll.User32)]
        public static extern bool InvalidateRect(
            IntPtr hWnd,           // handle to window
            ref RECT lpRect,  // rectangle coordinates
                              //IntPtr lpRect,
            bool bErase          // erase state
            );

        [DllImport(ExternDll.User32, EntryPoint = "InvalidateRect")]
        public static extern bool InvalidateWindow(
            IntPtr hWnd,           // handle to window
            IntPtr lpRect,       // pass IntPtr.Zero to invalidate entire window
            bool bErase          // erase state
            );

        [DllImport(ExternDll.User32)]
        public static extern bool UpdateWindow(
            IntPtr hWnd   // handle to window
            );

        [DllImport(ExternDll.User32)]
        public static extern bool UpdateLayeredWindow(
            IntPtr hwnd,
            IntPtr hdcDst,
            ref POINT pptDst,
            ref SIZE psize,
            IntPtr hdcSrc,
            ref POINT pptSrc,
            uint crKey,
            ref BLENDFUNCTION pblend,
            uint dwFlags);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [DllImport(ExternDll.User32)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(
            IntPtr hWnd,
            [MarshalAs(UnmanagedType.LPTStr)] string lpString
            );

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern uint RegisterWindowMessage(
            [MarshalAs(UnmanagedType.LPTStr)] string lpString
            );

        [DllImport(ExternDll.User32)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(
            IntPtr hwndParent, IntPtr hwndChildAfter,
            [In, MarshalAs(UnmanagedType.LPTStr)] string lpszClass,
            [In, MarshalAs(UnmanagedType.LPTStr)] string lpszWindow
            );

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(
            [In, MarshalAs(UnmanagedType.LPTStr)] string lpszClass,
            [In, MarshalAs(UnmanagedType.LPTStr)] string lpszWindow
            );

        /// <summary>
        /// Set's the input focus to the specified window.
        /// </summary>
        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        /// <summary>
        /// Changes the parent window of the specified child window.
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        /// <summary>
        /// Returns the active window of the calling thread
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetActiveWindow();

        [DllImport(ExternDll.User32)]
        public static extern Boolean SetForegroundWindow(IntPtr hWnd);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport(ExternDll.User32)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport(ExternDll.User32)]
        public static extern Boolean GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport(ExternDll.User32)]
        public static extern Boolean SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        [DllImport(ExternDll.User32)]
        public static extern Boolean ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport(ExternDll.User32)]
        public static extern bool MoveWindow(
            IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport(ExternDll.User32)]
        public static extern bool ScreenToClient(
            IntPtr hWnd,        // handle to window
            ref POINT lpPoint   // screen coordinates
            );

        //If the function succeeds, the low-order word of the return value is the number of pixels added
        //to the horizontal coordinate of each source point in order to compute the horizontal
        //coordinate of each destination point; the high-order word is the number of pixels added
        //to the vertical coordinate of each source point in order to compute the vertical coordinate
        //of each destination point.
        //If the function fails, the return value is zero. Call SetLastError prior to calling this method
        //to differentiate an error return value from a legitimate "0" return value.

        //If hWndFrom or hWndTo (or both) are mirrored windows (that is, have WS_EX_LAYOUTRTL extended style),
        //MapWindowPoints will automatically adjust mirrored coordinates
        //if you pass two or less points in lpPoints.
        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern int MapWindowPoints(
            IntPtr hwndFrom,  //handle to window mapping from
            IntPtr hwndTo,   //handle to window mapping to
            ref POINT lpPoints, //array of points to map
            [MarshalAs(UnmanagedType.U4)] int cPoints //size of the array
            );

        public static readonly int GWL_EXSTYLE = (-20);
        public const uint WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetAncestor(IntPtr hWnd, GA gaFlags);

        [DllImport(ExternDll.User32)]
        public static extern Boolean AnimateWindow(IntPtr hWnd, uint dwTime, AW dwFlags);

        /// <summary>
        /// The IsWindow function determines whether the specified
        /// window handle identifies an existing window.
        /// </summary>
        /// <param name="hWnd">
        /// [in] Handle to the window to test.
        /// </param>
        /// <returns>
        /// If the window handle identifies an existing window, the return value is nonzero.
        /// If the window handle does not identify an existing window, the return value is zero.
        /// </returns>
        [DllImport(ExternDll.User32)]
        public static extern Boolean IsWindow(IntPtr hWnd);

        [DllImport(ExternDll.User32)]
        public static extern UInt32 WaitForInputIdle(IntPtr hProcess, UInt32 dwMilliseconds);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        /// <summary>
        /// The GetDCEx function retrieves a handle to a display device context (DC) for the client
        /// area of a specified window or for the entire screen. You can use the returned handle in
        /// subsequent GDI functions to draw in the DC.
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr region, UInt32 dw);

        /// <summary>
        /// The ReleaseDC function releases a device context (DC), freeing it for use by other
        /// applications. The effect of the ReleaseDC function depends on the type of DC. It frees
        /// only common and window DCs. It has no effect on class or private DCs.
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr dc);

        /// <summary>
        /// The GetWindowLong function retrieves information about the specified window. The
        /// function also retrieves the 32-bit (long) value at the specified offset into the extra
        /// window memory.
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// The SetWindowLong function changes an attribute of the specified window. The function
        /// also sets the 32-bit (long) value at the specified offset into the extra window memory.
        /// </summary>
        [DllImport(ExternDll.User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);

        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport(ExternDll.User32, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

        [DllImport(ExternDll.User32, CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);

        [DllImport(ExternDll.User32, CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, UInt32 wParam, ref CHARFORMAT2 lParam);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        public static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern int SendMessage(HandleRef hWnd, UInt32 Msg, ref int wParam, StringBuilder lParam);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern int SendMessage(HandleRef hWnd, UInt32 Msg, IntPtr wParam, string lParam);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, string lParam);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern int SendMessage(HandleRef hWnd, UInt32 Msg, IntPtr wParam, bool lParam);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern int SendMessage(HandleRef hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern long SendMessage(IntPtr hWnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport(ExternDll.User32)]
        public static extern bool PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        /// <summary>
        /// Translates virtual-key messages into character messages. The character
        /// messages are posted to the calling thread's message queue, to be read
        /// the next time the thread calls the GetMessage or PeekMessage function.
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern bool TranslateMessage([In] ref MSG msg);

        /// <summary>
        /// Dispatches a message to a window procedure.
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern IntPtr DispatchMessage([In] ref MSG msg);

        /// <summary>
        /// Delegate used for SetWindowsHookEx
        /// </summary>
        public delegate IntPtr HookDelegate(int nCode, UIntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Installs a windows hook procedure (used for low level filtering of
        /// keyboard, mouse, events, etc.)
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern IntPtr SetWindowsHookEx(
            WH hookType, HookDelegate lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// Uninstalls a windows hook procedure
        /// </summary>
        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport(ExternDll.User32)]
        public static extern int GetSystemMetrics(int nIndex);

        /// <summary>
        /// Calls the next hook in a hook chain
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern IntPtr CallNextHookEx(
            IntPtr hhk, int nCode, UIntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Retrieves the identifier of the thread that created the specified window
        /// and, optionally, the identifier of the process that created the window
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern uint GetWindowThreadProcessId(
            IntPtr hWnd, IntPtr lpdwProcessId);

        /// <summary>
        /// Retrieves the status of the specified virtual key. The status specifies
        /// whether the key is up, down, or toggled (on, off—alternating each time
        /// the key is pressed).
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern short GetKeyState(int nVirtKey);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport(ExternDll.User32)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport(ExternDll.User32)]
        public static extern bool PostMessage(
            IntPtr hWnd,
            uint Msg,
            UIntPtr wParam,
            IntPtr lParam
            );

        [DllImport(ExternDll.User32)]
        public static extern uint GetMenuItemID(IntPtr hMenu, int nPos);

        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT lpEventTrack);

        public const uint TME_NONCLIENT = 0x00000010;
        public const uint TME_HOVER = 0x00000001;
        public const uint TME_QUERY = 0x40000000;
        public const uint TME_CANCEL = 0x80000000;
        public const uint TME_LEAVE = 0x00000010;

        public const UInt32 WM_USER = 0x0400;
        public const UInt32 EM_GETCHARFORMAT = (WM_USER + 58);
        public const UInt32 EM_SETCHARFORMAT = (WM_USER + 68);
        public const UInt32 SCF_ALL = 0x0004;
        public const UInt32 SCF_SELECTION = 0x0001;

        #region CFE_

        // CHARFORMAT effects
        public const UInt32 CFE_BOLD = 0x0001;

        public const UInt32 CFE_ITALIC = 0x0002;
        public const UInt32 CFE_UNDERLINE = 0x0004;
        public const UInt32 CFE_STRIKEOUT = 0x0008;
        public const UInt32 CFE_PROTECTED = 0x0010;
        public const UInt32 CFE_LINK = 0x0020;
        public const UInt32 CFE_AUTOCOLOR = 0x40000000;            // NOTE: this corresponds to

        // CFM_COLOR, which controls it
        // Masks and effects defined for CHARFORMAT2 -- an (*) indicates
        // that the data is stored by RichEdit 2.0/3.0, but not displayed
        public const UInt32 CFE_SMALLCAPS = CFM_SMALLCAPS;

        public const UInt32 CFE_ALLCAPS = CFM_ALLCAPS;
        public const UInt32 CFE_HIDDEN = CFM_HIDDEN;
        public const UInt32 CFE_OUTLINE = CFM_OUTLINE;
        public const UInt32 CFE_SHADOW = CFM_SHADOW;
        public const UInt32 CFE_EMBOSS = CFM_EMBOSS;
        public const UInt32 CFE_IMPRINT = CFM_IMPRINT;
        public const UInt32 CFE_DISABLED = CFM_DISABLED;
        public const UInt32 CFE_REVISED = CFM_REVISED;

        // CFE_AUTOCOLOR and CFE_AUTOBACKCOLOR correspond to CFM_COLOR and
        // CFM_BACKCOLOR, respectively, which control them
        public const UInt32 CFE_AUTOBACKCOLOR = CFM_BACKCOLOR;

        #endregion CFE_

        #region CFM_

        // CHARFORMAT masks
        public const UInt32 CFM_BOLD = 0x00000001;

        public const UInt32 CFM_ITALIC = 0x00000002;
        public const UInt32 CFM_UNDERLINE = 0x00000004;
        public const UInt32 CFM_STRIKEOUT = 0x00000008;
        public const UInt32 CFM_PROTECTED = 0x00000010;
        public const UInt32 CFM_LINK = 0x00000020;         // Exchange hyperlink extension
        public const UInt32 CFM_SIZE = 0x80000000;
        public const UInt32 CFM_COLOR = 0x40000000;
        public const UInt32 CFM_FACE = 0x20000000;
        public const UInt32 CFM_OFFSET = 0x10000000;
        public const UInt32 CFM_CHARSET = 0x08000000;

        public const UInt32 CFM_SMALLCAPS = 0x0040;            // (*)
        public const UInt32 CFM_ALLCAPS = 0x0080;          // Displayed by 3.0
        public const UInt32 CFM_HIDDEN = 0x0100;           // Hidden by 3.0
        public const UInt32 CFM_OUTLINE = 0x0200;          // (*)
        public const UInt32 CFM_SHADOW = 0x0400;           // (*)
        public const UInt32 CFM_EMBOSS = 0x0800;           // (*)
        public const UInt32 CFM_IMPRINT = 0x1000;          // (*)
        public const UInt32 CFM_DISABLED = 0x2000;
        public const UInt32 CFM_REVISED = 0x4000;

        public const UInt32 CFM_BACKCOLOR = 0x04000000;
        public const UInt32 CFM_LCID = 0x02000000;
        public const UInt32 CFM_UNDERLINETYPE = 0x00800000;        // Many displayed by 3.0
        public const UInt32 CFM_WEIGHT = 0x00400000;
        public const UInt32 CFM_SPACING = 0x00200000;      // Displayed by 3.0
        public const UInt32 CFM_KERNING = 0x00100000;      // (*)
        public const UInt32 CFM_STYLE = 0x00080000;        // (*)
        public const UInt32 CFM_ANIMATION = 0x00040000;        // (*)
        public const UInt32 CFM_REVAUTHOR = 0x00008000;

        public const UInt32 CFE_SUBSCRIPT = 0x00010000;        // Superscript and subscript are
        public const UInt32 CFE_SUPERSCRIPT = 0x00020000;      //  mutually exclusive

        public const UInt32 CFM_SUBSCRIPT = (CFE_SUBSCRIPT | CFE_SUPERSCRIPT);
        public const UInt32 CFM_SUPERSCRIPT = CFM_SUBSCRIPT;

        // CHARFORMAT "ALL" masks
        public const UInt32 CFM_EFFECTS = (CFM_BOLD | CFM_ITALIC | CFM_UNDERLINE | CFM_COLOR |
                             CFM_STRIKEOUT | CFE_PROTECTED | CFM_LINK);

        public const UInt32 CFM_ALL = (CFM_EFFECTS | CFM_SIZE | CFM_FACE | CFM_OFFSET | CFM_CHARSET);

        public const UInt32 CFM_EFFECTS2 = (CFM_EFFECTS | CFM_DISABLED | CFM_SMALLCAPS | CFM_ALLCAPS
                            | CFM_HIDDEN | CFM_OUTLINE | CFM_SHADOW | CFM_EMBOSS
                            | CFM_IMPRINT | CFM_DISABLED | CFM_REVISED
                            | CFM_SUBSCRIPT | CFM_SUPERSCRIPT | CFM_BACKCOLOR);

        public const UInt32 CFM_ALL2 = (CFM_ALL | CFM_EFFECTS2 | CFM_BACKCOLOR | CFM_LCID
                            | CFM_UNDERLINETYPE | CFM_WEIGHT | CFM_REVAUTHOR
                            | CFM_SPACING | CFM_KERNING | CFM_STYLE | CFM_ANIMATION);

        #endregion CFM_

        public const int WM_SYSCOMMAND = 0x112;
        public const int MOUSE_MOVE = 0xF012;

        public const int BS_COMMANDLINK = 0x0000000E;
        public const uint BCM_SETNOTE = 0x00001609;
        public const uint BCM_GETNOTE = 0x0000160A;
        public const uint BCM_GETNOTELENGTH = 0x0000160B;
        public const uint BCM_SETSHIELD = 0x0000160C;
        public const int BM_SETIMAGE = 0x00F7;

        public const int EM_SETCUEBANNER = 0x1501;

        public const uint BCM_FIRST = 0x1600;
        //public const uint BCM_SETSHIELD = (BCM_FIRST + 0xc);

        public const int IDC_HAND = 32649;

        [DllImport(ExternDll.User32)]
        public static extern int TrackPopupMenu(
            IntPtr hMenu,
            uint uFlags,
            int x,
            int y,
            int nReserved,
            IntPtr hWnd,
            IntPtr prcRect);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern bool SetMenuItemInfo(HandleRef hMenu, int uItem, bool fByPosition, MENUITEMINFO_T_RW lpmii);

        [DllImport(ExternDll.User32)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport(ExternDll.User32)]
        public static extern bool DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        [DllImport(ExternDll.User32)]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        /// <summary>
        /// Registers a new clipboard format. This format can then be used as a valid
        /// clipboard format. Return value is an integer id representing the format.
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern uint RegisterClipboardFormat(string lpszFormat);

        [DllImport(ExternDll.User32, EntryPoint = "SetWindowLong", CharSet = CharSet.Unicode)]
        public static extern int SetWindowProc(IntPtr hWnd, int nIndex, WndProcDelegate lpWndProc);

        // SetWindowLong - Conventional declaration with integer value parameter
        [DllImport(ExternDll.User32, EntryPoint = "SetWindowLong", CharSet = CharSet.Unicode)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int nValue);

        // CallWindowProc
        [DllImport(ExternDll.User32, CharSet = CharSet.Unicode)]
        public static extern IntPtr CallWindowProc(IntPtr lpWndProc, IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

        //get a title bar structure for a given hWnd
        [DllImport(ExternDll.User32)]
        public static extern bool GetTitleBarInfo(IntPtr hWnd, ref TITLEBARINFO pti);

        //get window info, such as border width, for a given hWnd
        [DllImport(ExternDll.User32)]
        public static extern bool GetWindowInfo(IntPtr hWnd, ref WINDOWINFO pwi);

        // Gets the text from a text box
        [DllImport(ExternDll.User32, EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern int GetText(IntPtr hWnd, int Msg, ref GETTEXTEX wParam, StringBuilder lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct GETTEXTEX
        {
            public Int32 cb;
            public Int32 flags;
            public Int32 codepage;
            public IntPtr lpDefaultChar;
            public IntPtr lpUsedDefChar;
        }

        public const int EM_GETTEXTEX = 0x0400 + 94;

        [DllImport(ExternDll.User32)]
        public static extern bool FlashWindow(IntPtr hwnd, Boolean bInvert);

        [DllImport(ExternDll.User32)]
        public static extern bool FlashWindowEx(ref FLASHWINFO pfwi);

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            public FLASHWINFO(IntPtr hwnd, Int32 dwFlags, Int32 uCount, Int32 dwTimeout)
            {
                this.hwnd = hwnd;
                this.dwFlags = dwFlags;
                this.uCount = uCount;
                this.dwTimeout = dwTimeout;

                // The size of the IntPtr + 4 Int32s
                cbSize = IntPtr.Size + 16;
            }

            public Int32 cbSize;
            public IntPtr hwnd;
            public Int32 dwFlags;
            public Int32 uCount;
            public Int32 dwTimeout;
        }

        public struct TRACKMOUSEEVENT
        {
            public int cbSize;
            public uint dwFlags;
            public IntPtr hwndTrack;
            public uint dwHoverTime;
        }

        public struct FlashStatus
        {
            public static Int32 FLASHW_STOP = 0;
            public static Int32 FLASHW_CAPTION = 1;
            public static Int32 FLASHW_TRAY = 2;
            public static Int32 FLASHW_ALL = (FlashStatus.FLASHW_CAPTION | FlashStatus.FLASHW_TRAY);
            public static Int32 FLASHW_TIMER = 4;
            public static Int32 FLASHW_TIMERNOFG = 12;
        };

        // declaration for EnumWindows
        [DllImport(ExternDll.User32)]
        public static extern bool EnumWindows(EnumWindowsDelegate lpEnumFunc, GCHandle lParam);

        [DllImport(ExternDll.User32)]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsDelegate lpEnumFunc, GCHandle lParam);

        // Gets the text length from a text box
        [DllImport(ExternDll.User32, EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern int GetTextLength(IntPtr hWnd, int Msg, ref GETTEXTLENGTHEX wParam, StringBuilder lParam);

        public struct GETTEXTLENGTHEX
        {
            public Int32 flags;
            public Int32 codepage;
        }

        public const int EM_GETTEXTLENGTHEX = 0x0400 + 95;

        public const int GTL_DEFAULT = 0;	// Do default (return # of chars)
        public const int GTL_USECRLF = 1;	// Compute answer using CRLFs for paragraphs
        public const int GTL_PRECISE = 2;	// Compute a precise answer
        public const int GTL_CLOSE = 4;	// Fast computation of a "close" answer
        public const int GTL_NUMCHARS = 8;	// Return number of characters
        public const int GTL_NUMBYTES = 16;	// Return number of _bytes_

        [DllImport(ExternDll.User32, CharSet = CharSet.Unicode)]
        public static extern int DrawTextEx(IntPtr hdc, StringBuilder lpchText, int cchText,
            ref RECT lprc, DT dwDTFormat, ref DRAWTEXTPARAMS lpDTParams);

        [StructLayout(LayoutKind.Sequential)]
        public struct DRAWTEXTPARAMS
        {
            public uint cbSize;
            public int iTabLength;
            public int iLeftMargin;
            public int iRightMargin;
            public uint uiLengthDrawn;
        }

        public enum DT : uint
        {
            TOP = 0x00000000,
            LEFT = 0x00000000,
            CENTER = 0x00000001,
            RIGHT = 0x00000002,
            VCENTER = 0x00000004,
            BOTTOM = 0x00000008,
            WORDBREAK = 0x00000010,
            SINGLELINE = 0x00000020,
            EXPANDTABS = 0x00000040,
            TABSTOP = 0x00000080,
            NOCLIP = 0x00000100,
            EXTERNALLEADING = 0x00000200,
            CALCRECT = 0x00000400,
            NOPREFIX = 0x00000800,
            INTERNAL = 0x00001000,
            EDITCONTROL = 0x00002000,
            PATH_ELLIPSIS = 0x00004000,
            END_ELLIPSIS = 0x00008000,
            MODIFYSTRING = 0x00010000,
            RTLREADING = 0x00020000,
            WORD_ELLIPSIS = 0x00040000,
            NOFULLWIDTHCHARBREAK = 0x00080000,
            HIDEPREFIX = 0x00100000,
            PREFIXONLY = 0x00200000
        }

        /////////////////////////////////////////////////////////////////////////////
        /// Active Accessiblity API -- Available in Win98, Win2K, and WinXP
        /// (available as a redistributable component for Win95 and NT4/SP6)
        ///

        /// <summary>
        /// Application-defined callback (or hook) function that the system calls
        /// in response to events generated by an accessible object.
        /// </summary>
        public delegate void WinEventProc(IntPtr hWinEventHook,
            EVENT_SYSTEM evt, IntPtr hwnd,
            Int32 idObject, Int32 idChild,
            uint dwEventThread, uint dwmsEventTime);

        /// <summary>
        /// Sets an event hook function for a range of events.
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern IntPtr SetWinEventHook(
            EVENT_SYSTEM eventMin, EVENT_SYSTEM eventMax,
            IntPtr hmodWinEventProc, WinEventProc lpfnWinEventProc,
            uint idProcess, uint idThread,
            WINEVENT dwFlags);

        /// <summary>
        /// Removes an event hook function created by a call to SetWinEventHook.
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        /// <summary>
        /// Retrieves the time of the last input event.
        /// </summary>
        [DllImport(ExternDll.User32)]
        public static extern bool GetLastInputInfo(out LASTINPUTINFO plii);

        //common dialogs

        [DllImport("ComDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetOpenFileName(ref OpenFileName ofn);

        [DllImport("ComDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 CommDlgExtendedError();

        [DllImport(ExternDll.User32, CharSet = CharSet.Unicode)]
        public static extern IntPtr GetDlgItem(IntPtr hWndDlg, Int32 Id);

        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern IntPtr GetDlgCtrlID(IntPtr hwndCtl);

        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern bool AllowSetForegroundWindow(int procId);

        // We can overload this definition, since that's in effect what the unmanaged
        // API does anyway.
        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(int uAction,
            int uParam, ref NONCLIENTMETRICS lpvParam, int fuWinIni);

        [DllImport(ExternDll.User32)]
        public static extern int DrawMenuBar(IntPtr hwnd);

        [DllImport(ExternDll.User32)]
        public static extern int SetMenuInfo(IntPtr hmenu, ref MENUINFO mi);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern bool SetMenuInfo(HandleRef hMenu, MENUINFO lpcmi);
    }

#if !INTEROP

    internal struct SM
#else

    public struct SM
#endif

    {
        public const int CXSIZEFRAME = 32;
        public const int CYSIZEFRAME = 33;
    }

    /// <summary>
    /// The MENUINFO structure contains information about a menu.
    /// </summary>
#if !INTEROP

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal class MENUINFO
#else

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class MENUINFO
#endif

    {
        public int cbSize = Marshal.SizeOf(typeof(MENUINFO));
        public int fMask = 0x00000010; //MIM_STYLE;
        public int dwStyle = 0x04000000; //MNS_CHECKORBMP;
        public uint cyMax;
        public IntPtr hbrBack = IntPtr.Zero;
        public int dwContextHelpID;
        public IntPtr dwMenuData = IntPtr.Zero;

        //public int cbSize;
        //public int fMask;
        //public int dwStyle;
        //public int cyMax;
        //public IntPtr hbrBack;
        //public int dwContextHelpID;
        //public int dwMenuData;
    }

#if !INTEROP

    internal struct MIM
#else

    public struct MIM
#endif

    {
        public const int BACKGROUND = 0x2;
    }

#if !INTEROP

    internal struct SPI
#else

    public struct SPI
#endif

    {
        public const int GETNONCLIENTMETRICS = 41;
    }

    // A "logical font" used by old-school windows
#if !INTEROP

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct LOGFONT
#else

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct LOGFONT
#endif

    {
        public int lfHeight;
        public int lfWidth;
        public int lfEscapement;
        public int lfOrientation;
        public int lfWeight;
        public byte lfItalic;
        public byte lfUnderline;
        public byte lfStrikeOut;
        public byte lfCharSet;
        public byte lfOutPrecision;
        public byte lfClipPrecision;
        public byte lfQuality;
        public byte lfPitchAndFamily;

        /// <summary>
        /// <see cref="UnmanagedType.ByValTStr"/> means that the string
        /// should be marshalled as an array of TCHAR embedded in the
        /// structure.  This implies that the font names can be no larger
        /// than <see cref="LF_FACESIZE"/> including the terminating '\0'.
        /// That works out to 31 characters.
        /// </summary>
        private const int LF_FACESIZE = 32;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FACESIZE)]
        public string lfFaceName;

        // to shut it up about the warnings
        public LOGFONT(string lfFaceName)
        {
            this.lfFaceName = lfFaceName;
            lfHeight = lfWidth = lfEscapement = lfOrientation = lfWeight = 0;
            lfItalic = lfUnderline = lfStrikeOut = lfCharSet = lfOutPrecision
                = lfClipPrecision = lfQuality = lfPitchAndFamily = 0;
        }
    }

#if !INTEROP

    internal struct NONCLIENTMETRICS
#else

    public struct NONCLIENTMETRICS
#endif

    {
        public int cbSize;
        public int iBorderWidth;
        public int iScrollWidth;
        public int iScrollHeight;
        public int iCaptionWidth;
        public int iCaptionHeight;

        /// <summary>
        /// Since <see cref="LOGFONT"/> is a struct instead of a class,
        /// we don't have to do any special marshalling here.  Much
        /// simpler this way.
        /// </summary>
        public LOGFONT lfCaptionFont;

        public int iSMCaptionWidth;
        public int iSMCaptionHeight;
        public LOGFONT lfSMCaptionFont;
        public int iMenuWidth;
        public int iMenuHeight;
        public LOGFONT lfMenuFont;
        public LOGFONT lfStatusFont;
        public LOGFONT lfMessageFont;
    }

    /// <summary>
    /// Delegate to which messages for subclassed control will be redirected.
    /// redirected. The delegate should invoke CallBaseWindowProc when it
    /// wishes to forward a message on to the underlying window.
    /// </summary>
#if !INTEROP

    internal delegate IntPtr WndProcDelegate(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

#else

    public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);

#endif

#if !INTEROP

    internal struct WINDOWPLACEMENT
#else

    public struct WINDOWPLACEMENT
#endif

    {
        public uint length;
        public uint flags;
        public uint showCmd;
        public POINT ptMinPosition;
        public POINT ptMaxPosition;
        public RECT rcNormalPosition;
    };

#if !INTEROP

    [Flags]
    internal enum QS : uint
#else

    [Flags]
    public enum QS : uint
#endif

    {
        KEY = 0x0001,
        MOUSEMOVE = 0x0002,
        MOUSEBUTTON = 0x0004,
        POSTMESSAGE = 0x0008,
        TIMER = 0x0010,
        PAINT = 0x0020,
        SENDMESSAGE = 0x0040,
        HOTKEY = 0x0080,
        ALLPOSTMESSAGE = 0x0100,
        RAWINPUT = 0x0400,
        MOUSE = MOUSEMOVE | MOUSEBUTTON,
        INPUT = MOUSE | KEY | RAWINPUT,
        ALLEVENTS = INPUT | POSTMESSAGE | TIMER | PAINT | HOTKEY,
        ALLINPUT = INPUT | POSTMESSAGE | TIMER | PAINT | HOTKEY | SENDMESSAGE
    }

#if !INTEROP

    internal struct MF
#else

    public struct MF
#endif

    {
        public const uint BYCOMMAND = 0x00000000;
        public const uint BYPOSITION = 0x00000400;
        public const uint ENABLED = 0x00000000;
        public const uint DISABLED = 0x00000002;
    }

#if !INTEROP

    internal struct PM
#else

    public struct PM
#endif

    {
        public const uint NOREMOVE = 0x0000;
        public const uint REMOVE = 0x0001;
    }

    // cursor values
#if !INTEROP

    internal class IDC
#else

    public class IDC
#endif

    {
        public static readonly IntPtr APPSTARTING = new IntPtr(32650);
        public static readonly IntPtr ARROW = new IntPtr(32512);
    }

#if !INTEROP

    internal struct OCR
#else

    public struct OCR
#endif

    {
        public const uint NORMAL = 32512;
    }

    /// <summary>
    /// A structure representing a windows message
    /// </summary>
#if !INTEROP

    internal struct MSG
#else

    public struct MSG
#endif

    {
        /// <summary>
        /// The handle of the window receiving the message
        /// </summary>
        public IntPtr hwnd;

        /// <summary>
        /// The message number
        /// </summary>
        public UInt32 message;

        /// <summary>
        /// First message parameter
        /// </summary>
        public UInt32 wParam;

        /// <summary>
        /// Second message parameter
        /// </summary>
        public Int32 lParam;

        /// <summary>
        /// The time the message was posted
        /// </summary>
        public UInt32 time;

        /// <summary>
        /// A POINT structure containing the cursor position in screen coordinates
        /// at the time the message was posted
        /// </summary>
        public POINT pt;
    }

    /// <summary>
    /// POINT used in MSG structure
    /// </summary>

#if !INTEROP

    internal struct POINT
#else

    public struct POINT
#endif

    {
        /// <summary>
        /// x-coordinate
        /// </summary>
        public Int32 x;

        /// <summary>
        /// y-coordinate
        /// </summary>
        public Int32 y;
    }

    /// <summary>
    /// Windows RECT structure
    /// </summary>
#if !INTEROP

    internal struct RECT
#else

    public struct RECT
#endif

    {
        public Int32 left;
        public Int32 top;
        public Int32 right;
        public Int32 bottom;

        public static implicit operator Rectangle(RECT rect)
        {
            return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
        }

        public static implicit operator RECT(Rectangle rectangle)
        {
            RECT rect = new RECT();
            rect.left = rectangle.Left;
            rect.top = rectangle.Top;
            rect.right = rectangle.Right;
            rect.bottom = rectangle.Bottom;
            return rect;
        }

        public int Width { get { return right - left; } }
        public int Height { get { return bottom - top; } }
    }

    /// <summary>
    /// Windows SIZE structure
    /// </summary>
#if !INTEROP

    internal struct SIZE
#else

    public struct SIZE
#endif

    {
        /// <summary>
        /// Width
        /// </summary>
        public Int32 cx;

        /// <summary>
        /// Height
        /// </summary>
        public Int32 cy;
    }

    /// <summary>
    /// Structure used with COPYDATASTRUCT
    /// </summary>
#if !INTEROP

    internal struct COPYDATASTRUCT
#else

    public struct COPYDATASTRUCT
#endif

    {
        public uint dwData;
        public uint cbData;
        public IntPtr lpData;
    }

    // delegate signagure for EnumWindows callback
#if !INTEROP

    internal delegate bool EnumWindowsDelegate(IntPtr hwnd, GCHandle lParam);

#else

    public delegate bool EnumWindowsDelegate(IntPtr hwnd, GCHandle lParam);

#endif

    /// <summary>
    /// Constants for window styles
    /// </summary>
#if !INTEROP

    internal struct WS
#else

    public struct WS
#endif

    {
        public const UInt32 CHILD = 0x40000000;
        public const UInt32 VISIBLE = 0x10000000;
        public const UInt32 CLIPCHILDREN = 0x02000000;
        public const UInt32 CLIPSIBLINGS = 0x04000000;
        public const UInt32 HSCROLL = 0x00100000;
        public const UInt32 POPUP = 0x80000000;
        public const UInt32 TABSTOP = 0x00010000;
        public const UInt32 VSCROLL = 0x00200000;
        public const UInt32 EX_TOOLWINDOW = 0x00000080;
        public const UInt32 EX_APPWINDOW = 0x00040000;
        public const UInt32 EX_TOPMOST = 0x00000008;

        public const UInt32 BORDER = 0x00800000;
        public const UInt32 THICKFRAME = 0x00040000;
        public const UInt32 SYSMENU = 0x00080000;
        public const UInt32 MINIMIZEBOX = 0x00020000;
        public const UInt32 MAXIMIZEBOX = 0x00010000;
    }

#if !INTEROP

    internal struct MOD
#else

    public struct MOD
#endif

    {
        public const uint ALT = 0x0001;
        public const uint CONTROL = 0x0002;
        public const uint SHIFT = 0x0004;
        public const uint WIN = 0x0008;
    }

    /// <summary>
    /// Constants for ShowWindow
    /// </summary>
#if !INTEROP

    internal struct SW
#else

    public struct SW
#endif

    {
        public const int HIDE = 0;
        public const int SHOWNORMAL = 1;
        public const int NORMAL = 1;
        public const int SHOWMINIMIZED = 2;
        public const int SHOWMAXIMIZED = 3;
        public const int MAXIMIZE = 3;
        public const int SHOWNOACTIVATE = 4;
        public const int SHOW = 5;
        public const int MINIMIZE = 6;
        public const int SHOWMINNOACTIVE = 7;
        public const int SHOWNA = 8;
        public const int RESTORE = 9;
        public const int SHOWDEFAULT = 10;
        public const int FORCEMINIMIZE = 11;
        public const int MAX = 11;
    }

    /// <summary>
    /// MessageBox return codes
    /// </summary>
#if !INTEROP

    internal struct MSGBOX_ID
#else

    public struct MSGBOX_ID
#endif

    {
        public const int OK = 1;
        public const int CANCEL = 2;
        public const int ABORT = 3;
        public const int RETRY = 4;
        public const int IGNORE = 5;
        public const int YES = 6;
        public const int NO = 7;
        public const int CLOSE = 8;
        public const int HELP = 9;
    }

#if !INTEROP

    internal struct WPF
#else

    public struct WPF
#endif

    {
        public const int RESTORETOMAXIMIZED = 0x0002;
    }

#if !INTEROP

    [Flags]
    internal enum AW : uint
#else

    [Flags]
    public enum AW : uint
#endif

    {
        HOR_POSITIVE = 0x00000001,
        HOR_NEGATIVE = 0x00000002,
        VER_POSITIVE = 0x00000004,
        VER_NEGATIVE = 0x00000008,
        CENTER = 0x00000010,
        HIDE = 0x00010000,
        ACTIVATE = 0x00020000,
        SLIDE = 0x00040000,
        BLEND = 0x00080000
    }

#if !INTEROP

    internal enum GA
#else

    public enum GA
#endif

    {
        PARENT = 1,
        ROOT = 2,
        ROOTOWNER = 3
    }

    /// <summary>
    /// Constants for SetWindowPos
    /// </summary>
#if !INTEROP

    internal struct SWP
#else

    public struct SWP
#endif

    {
        public const uint NOSIZE = 0x0001;
        public const uint NOMOVE = 0x0002;
        public const uint NOZORDER = 0x0004;

        public const uint SHOWWINDOW = 0x0040;
        public const uint NOACTIVATE = 0x0010;
        public const uint HIDEWINDOW = 0x0080;

        public const uint NOOWNERZORDER = 0x0200;
    }

    /// <summary>
    /// TrackPopupMenu flags
    /// </summary>
#if !INTEROP

    internal struct TPM
#else

    public struct TPM
#endif

    {
        public const uint LEFTBUTTON = 0x0000;
        public const uint RIGHTBUTTON = 0x0002;
        public const uint LEFTALIGN = 0x0000;
        public const uint CENTERALIGN = 0x0004;
        public const uint RIGHTALIGN = 0x0008;
        public const uint NONOTIFY = 0x0080;
        public const uint RETURNCMD = 0x0100;
        public const uint VERNEGANIMATION = 0x2000;
        public const uint LAYOUTRTL = 0x8000;
    }

    /// <summary>
    /// Window field offsets for GetWindowLong().
    /// </summary>
#if !INTEROP

    internal struct GWL
#else

    public struct GWL
#endif

    {
        public const int WNDPROC = -4;
        public const int HINSTANCE = -6;
        public const int HWNDPARENT = -8;
        public const int STYLE = -16;
        public const int EXSTYLE = -20;
        public const int USERDATA = -21;
        public const int ID = -12;
    }

    /// <summary>
    /// GetDCEx() flags.
    /// </summary>
#if !INTEROP

    internal struct DCX
#else

    public struct DCX
#endif

    {
        public const UInt32 WINDOW = 0x00000001;
        public const UInt32 CACHE = 0x00000002;
        public const UInt32 NORESETATTRS = 0x00000004;
        public const UInt32 CLIPCHILDREN = 0x00000008;
        public const UInt32 CLIPSIBLINGS = 0x00000010;
        public const UInt32 PARENTCLIP = 0x00000020;
        public const UInt32 EXCLUDERGN = 0x00000040;
        public const UInt32 INTERSECTRGN = 0x00000080;
        public const UInt32 EXCLUDEUPDATE = 0x00000100;
        public const UInt32 INTERSECTUPDATE = 0x00000200;
        public const UInt32 LOCKWINDOWUPDATE = 0x00000400;
        public const UInt32 VALIDATE = 0x00200000;
    }

    /// <summary>
    /// Constants for button messages
    /// </summary>
#if !INTEROP

    internal struct BM
#else

    public struct BM
#endif

    {
        public const UInt32 SETSTYLE = 0x000000F4;
    }

#if !INTEROP

    internal struct ButtonStyles
#else

    public struct ButtonStyles
#endif

    {
        public const long BS_PUSHBUTTON = 0x00000000L;
        public const long BS_DEFPUSHBUTTON = 0x00000001L;
    }

    /// <summary>
    /// Constants for combo box notifications
    /// </summary>
#if !INTEROP

    internal struct CBN
#else

    public struct CBN
#endif

    {
        public const int DROPDOWN = 7;
    }

    /// <summary>
    /// Constants for window messages
    /// </summary>
#if !INTEROP

    internal struct WM
#else

    public struct WM
#endif

    {
        public const UInt32 COMMAND = 0x0111;
        public const UInt32 DESTROY = 0x0002;
        public const UInt32 SIZE = 0x0005;
        public const UInt32 CLOSE = 0x0010;

        public const UInt32 QUERYENDSESSION = 0x0011;
        public const UInt32 ENDSESSION = 0x0016;

        public const UInt32 VSCROLL = 0x0115;
        public const UInt32 SETCURSOR = 0x0020;
        public const UInt32 MOUSEACTIVATE = 0x0021;
        public const UInt32 NOTIFY = 0x004E;
        public const UInt32 INITDIALOG = 0x0110;
        public const UInt32 GETTEXT = 0x000D;
        public const UInt32 GETTEXTLENGTH = 0x000E;
        public const UInt32 USER = 0x0400;
        public const UInt32 NULL = 0x0000;
        public const UInt32 GETMINMAXINFO = 0x0024;
        public const UInt32 HOTKEY = 0x0312;
        public const UInt32 SETFOCUS = 0x0007;
        public const UInt32 KEYDOWN = 0x100;
        public const UInt32 KEYUP = 0x0101;
        public const UInt32 CHAR = 0x0102;
        public const UInt32 SYSKEYDOWN = 0x104;
        public const UInt32 SYSKEYUP = 0x105;
        public const UInt32 SYSCHAR = 0x106;
        public const UInt32 NCDESTROY = 0x0082;
        public const UInt32 NCPAINT = 0x0085;
        public const UInt32 NCACTIVATE = 0x0086;
        public const UInt32 ERASEBKGND = 0x0014;
        public const UInt32 NCHITTEST = 0x0084;
        public const UInt32 SYSCOMMAND = 0x0112;
        public const UInt32 MOUSEMOVE = 0x0200;
        public const UInt32 LBUTTONDOWN = 0x0201;
        public const UInt32 LBUTTONUP = 0x0202;
        public const UInt32 LBUTTONDBLCLK = 0x0203;
        public const UInt32 RBUTTONDOWN = 0x0204;
        public const UInt32 RBUTTONUP = 0x0205;
        public const UInt32 CHANGEUISTATE = 0x0127;
        public const UInt32 UPDATEUISTATE = 0x0128;
        public const UInt32 ENTERMENULOOP = 0x0211;
        public const UInt32 EXITMENULOOP = 0x0212;
        public const UInt32 CUT = 0x0300;
        public const UInt32 COPY = 0x0301;
        public const UInt32 PASTE = 0x0302;
        public const UInt32 CLEAR = 0x0303;
        public const UInt32 ACTIVATE = 0x0006;
        public const UInt32 COPYDATA = 0x004A;
        public const UInt32 CONTEXTMENU = 0x007B;
        public const UInt32 WININICHANGE = 0x001A;
        public const UInt32 ENTERSIZEMOVE = 0x0231;
        public const UInt32 EXITSIZEMOVE = 0x0232;
        public const UInt32 NCMOUSEMOVE = 0x00A0;
        public const UInt32 NCLBUTTONDOWN = 0x00A1;
        public const UInt32 NCLBUTTONUP = 0x00A2;
        public const UInt32 NCLBUTTONDBLCLK = 0x00A3;
        public const UInt32 NCRBUTTONDOWN = 0x00A4;
        public const UInt32 NCRBUTTONUP = 0x00A5;
        public const UInt32 NCRBUTTONDBLCLK = 0x00A6;
        public const UInt32 NCMBUTTONDOWN = 0x00A7;
        public const UInt32 NCMBUTTONUP = 0x00A8;
        public const UInt32 NCMBUTTONDBLCLK = 0x00A9;
        public const UInt32 NCMOUSEHOVER = 0x02A0;
        public const UInt32 NCMOUSELEAVE = 0x02A2;
        public const UInt32 DM_SETDEFID = WM.USER + 1;
        public const UInt32 EM_REPLACESEL = 0x00C2;
        public const UInt32 EM_POSFROMCHAR = 0x00D6;
        public const UInt32 EM_SETMARGINS = 0x00D3;
        public const UInt32 EM_GETMARGINS = 0x00D4;

        public const UInt32 REFLECT = 0x2000;
    }

#if !INTEROP

    internal struct EC
#else

    public struct EC
#endif

    {
        public const uint RIGHTMARGIN = 2;
        public const uint LEFTMARGIN = 1;
    }

#if !INTEROP

    internal struct UIS
#else

    public struct UIS
#endif

    {
        public const int SET = 1;
        public const int CLEAR = 2;
        public const int INITIALIZE = 3;
    }

#if !INTEROP

    internal struct UISF
#else

    public struct UISF
#endif

    {
        public const int HIDEFOCUS = 0x1;
        public const int HIDEACCEL = 0x2;
        public const int ACTIVE = 0x4;
    }

    /// <summary>
    /// Constants for return value from NCHITTEST
    /// </summary>
#if !INTEROP

    internal struct HT
#else

    public struct HT
#endif

    {
        public const int ERROR = (-2);
        public const int TRANSPARENT = (-1);
        public const int NOWHERE = 0;
        public const int CLIENT = 1;
        public const int CAPTION = 2;
        public const int SYSMENU = 3;
        public const int GROWBOX = 4;
        public const int SIZE = GROWBOX;
        public const int MENU = 5;
        public const int HSCROLL = 6;
        public const int VSCROLL = 7;
        public const int MINBUTTON = 8;
        public const int MAXBUTTON = 9;
        public const int LEFT = 10;
        public const int RIGHT = 11;
        public const int TOP = 12;
        public const int TOPLEFT = 13;
        public const int TOPRIGHT = 14;
        public const int BOTTOM = 15;
        public const int BOTTOMLEFT = 16;
        public const int BOTTOMRIGHT = 17;
        public const int BORDER = 18;
        public const int REDUCE = MINBUTTON;
        public const int ZOOM = MAXBUTTON;
        public const int SIZEFIRST = LEFT;
        public const int SIZELAST = BOTTOMRIGHT;
        public const int OBJECT = 19;
        public const int CLOSE = 20;
        public const int HELP = 21;
    }

    /// <summary>
    /// Constants for WM_SYSCOMMAND
    /// </summary>
#if !INTEROP

    internal struct SC
#else

    public struct SC
#endif

    {
        public static readonly UIntPtr SIZE = new UIntPtr(0xF000);
        public static readonly UIntPtr MOVE = new UIntPtr(0xF010);
        public static readonly UIntPtr MINIMIZE = new UIntPtr(0xF020);
        public static readonly UIntPtr MAXIMIZE = new UIntPtr(0xF030);
        public static readonly UIntPtr NEXTWINDOW = new UIntPtr(0xF040);
        public static readonly UIntPtr PREVWINDOW = new UIntPtr(0xF050);
        public static readonly UIntPtr CLOSE = new UIntPtr(0xF060);
        public static readonly UIntPtr VSCROLL = new UIntPtr(0xF070);
        public static readonly UIntPtr HSCROLL = new UIntPtr(0xF080);
        public static readonly UIntPtr MOUSEMENU = new UIntPtr(0xF090);
        public static readonly UIntPtr KEYMENU = new UIntPtr(0xF100);
        public static readonly UIntPtr ARRANGE = new UIntPtr(0xF110);
        public static readonly UIntPtr RESTORE = new UIntPtr(0xF120);
        public static readonly UIntPtr TASKLIST = new UIntPtr(0xF130);
        public static readonly UIntPtr SCREENSAVE = new UIntPtr(0xF140);
        public static readonly UIntPtr HOTKEY = new UIntPtr(0xF150);
        public static readonly UIntPtr DEFAULT = new UIntPtr(0xF160);
        public static readonly UIntPtr MONITORPOWER = new UIntPtr(0xF170);
        public static readonly UIntPtr CONTEXTHELP = new UIntPtr(0xF180);
        public static readonly UIntPtr SEPARATOR = new UIntPtr(0xF00F);
    }

    /// <summary>
    /// Constants for virtual key codes
    /// </summary>
#if !INTEROP

    internal struct VK
#else

    public struct VK
#endif

    {
        public const int RETURN = 0x0D;
        public const int BACK = 0x08;
        public const int TAB = 0x09;
        public const int SHIFT = 0x10;
        public const int CONTROL = 0x11;
        public const int MENU = 0x12;
        public const int LMENU = 0xA4;
        public const int RMENU = 0xA5;
        public const int END = 0x23;
        public const int HOME = 0x24;
        public const int LEFT = 0x25;
        public const int UP = 0x26;
        public const int RIGHT = 0x27;
        public const int DOWN = 0x28;
        public const int INSERT = 0x2D;
        public const int DELETE = 0x2E;
    }

    /// <summary>
    /// Constants used for testing values returned from GetKeyState
    /// </summary>
#if !INTEROP

    internal struct VK_STATE
#else

    public struct VK_STATE
#endif

    {
        public const short PRESSED = 0xF0;
    }

    /// <summary>
    /// Modifiers for mouse-events
    /// </summary>

#if !INTEROP

    [Flags]
    internal enum MK : uint
#else

    [Flags]
    public enum MK : uint
#endif

    {
        LBUTTON = 0x0001,
        RBUTTON = 0x0002,
        SHIFT = 0x0004,
        CONTROL = 0x0008,
        MBUTTON = 0x0010,
        XBUTTON1 = 0x0020,
        XBUTTON2 = 0x0040
    }

    /// <summary>
    /// Enumeration for windows hook types
    /// </summary>
#if !INTEROP

    internal enum WH : int
#else

    public enum WH : int
#endif

    {
        MSGFILTER = -1,
        JOURNALRECORD = 0,
        JOURNALPLAYBACK = 1,
        KEYBOARD = 2,
        GETMESSAGE = 3,
        CALLWNDPROC = 4,
        CBT = 5,
        SYSMSGFILTER = 6,
        MOUSE = 7,
        HARDWARE = 8,
        DEBUG = 9,
        SHELL = 10,
        FOREGROUNDIDLE = 11,
        CALLWNDPROCRET = 12,
        KEYBOARD_LL = 13,
        MOUSE_LL = 14
    }

#if !INTEROP

    internal struct HWND
#else

    public struct HWND
#endif

    {
        public static readonly IntPtr DESKTOP = new IntPtr(0);
        public static readonly IntPtr TOP = new IntPtr(0);
        public static readonly IntPtr BOTTOM = new IntPtr(1);
        public static readonly IntPtr TOPMOST = new IntPtr(-1);
        public static readonly IntPtr NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr MESSAGE = new IntPtr(-3);
    }

    /// <summary>
    /// Hook codes passed to HookDelegate
    /// </summary>
#if !INTEROP

    internal struct HC
#else

    public struct HC
#endif

    {
        public const int ACTION = 0;
        public const int GETNEXT = 1;
        public const int SKIP = 2;
        public const int NOREMOVE = 3;
        public const int SYSMODALON = 4;
        public const int SYSMODALOFF = 5;
    }

#if !INTEROP

    internal struct ENDSESSION
#else

    public struct ENDSESSION
#endif
    {
        public const UInt32 ENDSESSION_CLOSEAPP = 0x00000001;
        public const UInt32 ENDSESSION_CRITICAL = 0x40000000;
        public const UInt32 ENDSESSION_LOGOFF = 0x80000000;
    }

    /// <summary>
    /// Key flags used to extract extended key information from lParam
    /// </summary>
#if !INTEROP

    internal struct KF
#else

    public struct KF
#endif

    {
        public static readonly IntPtr EXTENDED = new IntPtr(0x0100);
        public static readonly IntPtr DLGMODE = new IntPtr(0x0800);
        public static readonly IntPtr MENUMODE = new IntPtr(0x1000);
        public static readonly IntPtr ALTDOWN = new IntPtr(0x2000);
        public static readonly IntPtr REPEAT = new IntPtr(0x4000);
        public static readonly IntPtr UP = new IntPtr(0x8000);
    }

    /// <summary>
    /// Active Accessibilty event constants (note: only the constants we are
    /// currently using are defined -- there are many more available in Winuser.h)
    /// </summary>
#if !INTEROP

    internal enum EVENT_SYSTEM : uint
#else

    public enum EVENT_SYSTEM : uint
#endif

    {
        /*
         * EVENT_SYSTEM_CAPTURESTART
         * EVENT_SYSTEM_CAPTUREEND
         * Sent when a window takes the capture and releases the capture.
         */
        CAPTURESTART = 0x0008,
        CAPTUREEND = 0x0009,

        /*
        * Drag & Drop
        * EVENT_SYSTEM_DRAGDROPSTART
        * EVENT_SYSTEM_DRAGDROPEND
        * Send the START notification just before going into drag&drop loop.  Send
        * the END notification just after canceling out.
        * Note that it is up to apps and OLE to generate this, since the system
        * doesn't know.  Like EVENT_SYSTEM_SOUND, it will be a while before this
        * is prevalent.
        */
        DRAGDROPSTART = 0x000E,
        DRAGDROPEND = 0x000F
    }

#if !INTEROP

    [Flags]
    internal enum WINEVENT : uint
#else

    [Flags]
    public enum WINEVENT : uint
#endif

    {
        OUTOFCONTEXT = 0x0000,  // Events are ASYNC
        SKIPOWNTHREAD = 0x0001,  // Don't call back for events on installer's thread
        SKIPOWNPROCESS = 0x0002,  // Don't call back for events on installer's process
        INCONTEXT = 0x0004   // Events are SYNC (invalid in .NET because it requires
        // the client dll to be injected into every process)
    }

#if !INTEROP

    internal struct TTM
#else

    public struct TTM
#endif

    {
        public const uint ADDTOOL = WM.USER + 50;
        public const uint TRACKACTIVATE = WM.USER + 17;
        public const uint TRACKPOSITION = WM.USER + 18;
    }

#if !INTEROP

    internal struct TTF
#else

    public struct TTF
#endif

    {
        public const uint TRACK = 0x0020;
        public const uint ABSOLUTE = 0x0080;
    }

#if !INTEROP

    internal struct TTS
#else

    public struct TTS
#endif

    {
        public const uint ALWAYSTIP = 0x01;
        public const uint NOPREFIX = 0x02;
    }

#if !INTEROP

    internal struct TOOLINFO
#else

    public struct TOOLINFO
#endif

    {
        public uint cbSize;
        public uint uFlags;
        public IntPtr hwnd;
        public UIntPtr uId;
        public RECT rect;
        public IntPtr hinst;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpszText;

        public IntPtr lParam;
    }

#if !INTEROP

    internal struct WINDOW_CLASS
#else

    public struct WINDOW_CLASS
#endif

    {
        public const string TOOLTIPS = "tooltips_class32";
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential)]
    internal struct LASTINPUTINFO
#else

    [StructLayout(LayoutKind.Sequential)]
    public struct LASTINPUTINFO
#endif

    {
        public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

        [MarshalAs(UnmanagedType.U4)]
        public int cbSize;

        [MarshalAs(UnmanagedType.U4)]
        public int dwTime;
    }

    //title bar structure with size info
#if !INTEROP

    [StructLayout(LayoutKind.Sequential)]
    internal struct TITLEBARINFO
#else

    [StructLayout(LayoutKind.Sequential)]
    public struct TITLEBARINFO
#endif

    {
        public uint cbSize;
        public RECT rcTitleBar;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5 + 1)]
        public uint[] rgstate;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWINFO
#else

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
#endif

    {
        public uint cbSize;
        public RECT rcWindow;
        public RECT rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public uint cxWindowBorders;
        public uint cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;
    }

    //for commong dialogs
    /// <summary>
    /// See the documentation for OPENFILENAME
    /// </summary>
#if !INTEROP

    internal struct OpenFileName
#else

    public struct OpenFileName
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

#if !INTEROP

    internal delegate IntPtr OfnHookProc(IntPtr hWnd, UInt32 msg, Int32 wParam, Int32 lParam);

#else

    public delegate IntPtr OfnHookProc(IntPtr hWnd, UInt32 msg, Int32 wParam, Int32 lParam);

#endif

    /// <summary>
    /// Values that can be placed in the OPENFILENAME structure, we don't use all of them
    /// </summary>
#if !INTEROP

    internal class OpenFileNameFlags
#else

    public class OpenFileNameFlags
#endif

    {
        public const Int32 ReadOnly = 0x00000001;
        public const Int32 OverWritePrompt = 0x00000002;
        public const Int32 HideReadOnly = 0x00000004;
        public const Int32 NoChangeDir = 0x00000008;
        public const Int32 ShowHelp = 0x00000010;
        public const Int32 EnableHook = 0x00000020;
        public const Int32 EnableTemplate = 0x00000040;
        public const Int32 EnableTemplateHandle = 0x00000080;
        public const Int32 NoValidate = 0x00000100;
        public const Int32 AllowMultiSelect = 0x00000200;
        public const Int32 ExtensionDifferent = 0x00000400;
        public const Int32 PathMustExist = 0x00000800;
        public const Int32 FileMustExist = 0x00001000;
        public const Int32 CreatePrompt = 0x00002000;
        public const Int32 ShareAware = 0x00004000;
        public const Int32 NoReadOnlyReturn = 0x00008000;
        public const Int32 NoTestFileCreate = 0x00010000;
        public const Int32 NoNetworkButton = 0x00020000;
        public const Int32 NoLongNames = 0x00040000;
        public const Int32 Explorer = 0x00080000;
        public const Int32 NoDereferenceLinks = 0x00100000;
        public const Int32 LongNames = 0x00200000;
        public const Int32 EnableIncludeNotify = 0x00400000;
        public const Int32 EnableSizing = 0x00800000;
        public const Int32 DontAddToRecent = 0x02000000;
        public const Int32 ForceShowHidden = 0x10000000;
    };

    /// <summary>
    /// Values that can be placed in the FlagsEx field of the OPENFILENAME structure
    /// </summary>
#if !INTEROP

    internal class OpenFileNameFlagsEx
#else

    public class OpenFileNameFlagsEx
#endif

    {
        public const Int32 NoPlacesBar = 0x00000001;
    };

    /// <summary>
    /// Win32 window style constants
    /// We use them to set up our child window
    /// </summary>
#if !INTEROP

    internal class DlgStyle
#else

    public class DlgStyle
#endif
    {
        public const Int32 DsSetFont = 0x00000040;
        public const Int32 Ds3dLook = 0x00000004;
        public const Int32 DsControl = 0x00000400;
        public const Int32 WsChild = 0x40000000;
        public const Int32 WsClipSiblings = 0x04000000;
        public const Int32 WsVisible = 0x10000000;
        public const Int32 WsGroup = 0x00020000;
        public const Int32 SsNotify = 0x00000100;
    };

    /// <summary>
    /// Win32 "extended" window style constants
    /// </summary>
#if !INTEROP

    internal class ExStyle
#else

    public class ExStyle
#endif
    {
        public const Int32 WsExNoParentNotify = 0x00000004;
        public const Int32 WsExControlParent = 0x00010000;
    };

    /// <summary>
    /// An in-memory Win32 dialog template
    /// Note: this has a very specific structure with a single static "label" control
    /// See documentation for DLGTEMPLATE and DLGITEMTEMPLATE
    /// </summary>
#if !INTEROP

    [StructLayout(LayoutKind.Sequential)]
    internal class DlgTemplate
#else

    [StructLayout(LayoutKind.Sequential)]
    public class DlgTemplate
#endif

    {
        // The dialog template - see documentation for DLGTEMPLATE
        public Int32 style = DlgStyle.Ds3dLook | DlgStyle.DsControl | DlgStyle.WsChild | DlgStyle.WsClipSiblings | DlgStyle.SsNotify;

        public Int32 extendedStyle = ExStyle.WsExControlParent;
        public Int16 numItems = 1;
        public Int16 x = 0;
        public Int16 y = 0;
        public Int16 cx = 0;
        public Int16 cy = 0;
        public Int16 reservedMenu = 0;
        public Int16 reservedClass = 0;
        public Int16 reservedTitle = 0;

        // Single dlg item, must be dword-aligned - see documentation for DLGITEMTEMPLATE
        public Int32 itemStyle = DlgStyle.WsChild;

        public Int32 itemExtendedStyle = ExStyle.WsExNoParentNotify;
        public Int16 itemX = 0;
        public Int16 itemY = 0;
        public Int16 itemCx = 0;
        public Int16 itemCy = 0;
        public Int16 itemId = 0;
        public UInt16 itemClassHdr = 0xffff;	// we supply a constant to indicate the class of this control
        public Int16 itemClass = 0x0082;	// static label control
        public Int16 itemText = 0x0000;	// no text for this control
        public Int16 itemData = 0x0000;	// no creation data for this control
    };

    /// <summary>
    /// The possible notification messages that can be generated by the OpenFileDialog
    /// We only look for CDN_SELCHANGE
    /// </summary>
#if !INTEROP

    internal class CommonDlgNotification
#else

    public class CommonDlgNotification
#endif

    {
        //this was original definition but it errors in corext due asmmeta storing it as negative value
        //private const UInt16 First =			unchecked((UInt16)((UInt16)0 - (UInt16)601));
        //this is the same value
        private const Int16 First = -601;

        public const Int16 InitDone = (First - 0x0000);
        public const Int16 SelChange = (First - 0x0001);
        public const Int16 FolderChange = (First - 0x0002);
        public const Int16 ShareViolation = (First - 0x0003);
        public const Int16 Help = (First - 0x0004);
        public const Int16 FileOk = (First - 0x0005);
        public const Int16 TypeChange = (First - 0x0006);
        public const Int16 IncludeItem = (First - 0x0007);
    }

    /// <summary>
    /// Messages that can be send to the common dialogs
    /// We only use CDM_GETFILEPATH
    /// </summary>
#if !INTEROP

    internal class CommonDlgMessage
#else

    public class CommonDlgMessage
#endif
    {
        private const UInt16 User = 0x0400;
        private const UInt16 First = User + 100;

        public const UInt16 GetFilePath = First + 0x0001;
        public const UInt16 GetFolderPath = First + 0x0002;
    };

    /// <summary>
    /// Part of the notification messages sent by the common dialogs
    /// </summary>
#if !INTEROP

    [StructLayout(LayoutKind.Explicit)]
    internal struct NMHDR
#else

    [StructLayout(LayoutKind.Explicit)]
    public struct NMHDR
#endif
    {
        [FieldOffset(0)]
        public IntPtr hWndFrom;

        [FieldOffset(4)]
        public UInt16 idFrom;

        [FieldOffset(8)]
        public UInt16 code;
    };

    /// <summary>
    /// Part of the notification messages sent by the common dialogs
    /// </summary>
#if !INTEROP

    [StructLayout(LayoutKind.Explicit)]
    internal struct OfNotify
#else

    [StructLayout(LayoutKind.Explicit)]
    public struct OfNotify
#endif
    {
        [FieldOffset(0)]
        public NMHDR hdr;

        [FieldOffset(12)]
        public IntPtr ipOfn;

        [FieldOffset(16)]
        public IntPtr ipFile;
    };

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    internal struct CHARFORMAT2
#else

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    public struct CHARFORMAT2
#endif
    {
        public int cbSize;
        public uint dwMask;
        public uint dwEffects;
        public int yHeight;
        public int yOffset;
        public int crTextColor;
        public byte bCharSet;
        public byte bPitchAndFamily;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szFaceName;

        public short wWeight;
        public short sSpacing;
        public int crBackColor;
        public int lcid;
        public int dwReserved;
        public short sStyle;
        public short wKerning;
        public byte bUnderlineType;
        public byte bAnimation;
        public byte bRevAuthor;
        public byte bReserved1;
    }

#if !INTEROP

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal class MENUITEMINFO_T_RW
#else

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class MENUITEMINFO_T_RW
#endif
    {
        public int cbSize = Marshal.SizeOf(typeof(MENUITEMINFO_T_RW));
        public int fMask = 0x00000080; //MIIM_BITMAP = 0x00000080
        public int fType;
        public int fState;
        public int wID;
        public IntPtr hSubMenu = IntPtr.Zero;
        public IntPtr hbmpChecked = IntPtr.Zero;
        public IntPtr hbmpUnchecked = IntPtr.Zero;
        public IntPtr dwItemData = IntPtr.Zero;
        public IntPtr dwTypeData = IntPtr.Zero;
        public int cch;
        public IntPtr hbmpItem = IntPtr.Zero;
    }

#pragma warning restore
}