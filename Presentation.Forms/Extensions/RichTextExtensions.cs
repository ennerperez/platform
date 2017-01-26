using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Platform.Presentation.Forms
{
    public static class RichTextExtensions
    {
        public static void ClearAllFormatting(this RichTextBox te, Font font)
        {
            CHARFORMAT2 fmt = new CHARFORMAT2();

            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.dwMask = User32.CFM_ALL2;
            fmt.dwEffects = User32.CFE_AUTOCOLOR | User32.CFE_AUTOBACKCOLOR;
            fmt.szFaceName = font.FontFamily.Name;

            double size = font.Size;
            size /= 72;//logical dpi (pixels per inch)
            size *= 1440.0;//twips per inch

            fmt.yHeight = (int)size;//165
            fmt.yOffset = 0;
            fmt.crTextColor = 0;
            fmt.bCharSet = 1;// DEFAULT_CHARSET;
            fmt.bPitchAndFamily = 0;// DEFAULT_PITCH;
            fmt.wWeight = 400;// FW_NORMAL;
            fmt.sSpacing = 0;
            fmt.crBackColor = 0;
            //fmt.lcid = ???
            fmt.dwMask &= ~User32.CFM_LCID;//don't know how to get this...
            fmt.dwReserved = 0;
            fmt.sStyle = 0;
            fmt.wKerning = 0;
            fmt.bUnderlineType = 0;
            fmt.bAnimation = 0;
            fmt.bRevAuthor = 0;
            fmt.bReserved1 = 0;

            User32.SendMessage(te.Handle, User32.EM_SETCHARFORMAT, User32.SCF_ALL, ref fmt);
        }
        
    }
}

//internal static partial class NativeMethods
//{
//    internal const UInt32 WM_USER = 0x0400;
//    internal const UInt32 EM_GETCHARFORMAT = (WM_USER + 58);
//    internal const UInt32 EM_SETCHARFORMAT = (WM_USER + 68);
//    internal const UInt32 SCF_ALL = 0x0004;
//    internal const UInt32 SCF_SELECTION = 0x0001;

//    [DllImport(ExternDll.User32, CharSet = CharSet.Auto, SetLastError = false)]
//    internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, UInt32 wParam, ref CHARFORMAT2 lParam);

//    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
//    internal struct CHARFORMAT2
//    {
//        public int cbSize;
//        public uint dwMask;
//        public uint dwEffects;
//        public int yHeight;
//        public int yOffset;
//        public int crTextColor;
//        public byte bCharSet;
//        public byte bPitchAndFamily;
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
//        public string szFaceName;
//        public short wWeight;
//        public short sSpacing;
//        public int crBackColor;
//        public int lcid;
//        public int dwReserved;
//        public short sStyle;
//        public short wKerning;
//        public byte bUnderlineType;
//        public byte bAnimation;
//        public byte bRevAuthor;
//        public byte bReserved1;
//    }

//    #region CFE_
//    // CHARFORMAT effects 
//    internal const UInt32 CFE_BOLD = 0x0001;
//    internal const UInt32 CFE_ITALIC = 0x0002;
//    internal const UInt32 CFE_UNDERLINE = 0x0004;
//    internal const UInt32 CFE_STRIKEOUT = 0x0008;
//    internal const UInt32 CFE_PROTECTED = 0x0010;
//    internal const UInt32 CFE_LINK = 0x0020;
//    internal const UInt32 CFE_AUTOCOLOR = 0x40000000;            // NOTE: this corresponds to 
//                                                        // CFM_COLOR, which controls it 
//                                                        // Masks and effects defined for CHARFORMAT2 -- an (*) indicates
//                                                        // that the data is stored by RichEdit 2.0/3.0, but not displayed
//    internal const UInt32 CFE_SMALLCAPS = CFM_SMALLCAPS;
//    internal const UInt32 CFE_ALLCAPS = CFM_ALLCAPS;
//    internal const UInt32 CFE_HIDDEN = CFM_HIDDEN;
//    internal const UInt32 CFE_OUTLINE = CFM_OUTLINE;
//    internal const UInt32 CFE_SHADOW = CFM_SHADOW;
//    internal const UInt32 CFE_EMBOSS = CFM_EMBOSS;
//    internal const UInt32 CFE_IMPRINT = CFM_IMPRINT;
//    internal const UInt32 CFE_DISABLED = CFM_DISABLED;
//    internal const UInt32 CFE_REVISED = CFM_REVISED;

//    // CFE_AUTOCOLOR and CFE_AUTOBACKCOLOR correspond to CFM_COLOR and
//    // CFM_BACKCOLOR, respectively, which control them
//    internal const UInt32 CFE_AUTOBACKCOLOR = CFM_BACKCOLOR;
//    #endregion
//    #region CFM_
//    // CHARFORMAT masks 
//    internal const UInt32 CFM_BOLD = 0x00000001;
//    internal const UInt32 CFM_ITALIC = 0x00000002;
//    internal const UInt32 CFM_UNDERLINE = 0x00000004;
//    internal const UInt32 CFM_STRIKEOUT = 0x00000008;
//    internal const UInt32 CFM_PROTECTED = 0x00000010;
//    internal const UInt32 CFM_LINK = 0x00000020;         // Exchange hyperlink extension 
//    internal const UInt32 CFM_SIZE = 0x80000000;
//    internal const UInt32 CFM_COLOR = 0x40000000;
//    internal const UInt32 CFM_FACE = 0x20000000;
//    internal const UInt32 CFM_OFFSET = 0x10000000;
//    internal const UInt32 CFM_CHARSET = 0x08000000;

//    internal const UInt32 CFM_SMALLCAPS = 0x0040;            // (*)	
//    internal const UInt32 CFM_ALLCAPS = 0x0080;          // Displayed by 3.0	
//    internal const UInt32 CFM_HIDDEN = 0x0100;           // Hidden by 3.0 
//    internal const UInt32 CFM_OUTLINE = 0x0200;          // (*)	
//    internal const UInt32 CFM_SHADOW = 0x0400;           // (*)	
//    internal const UInt32 CFM_EMBOSS = 0x0800;           // (*)	
//    internal const UInt32 CFM_IMPRINT = 0x1000;          // (*)	
//    internal const UInt32 CFM_DISABLED = 0x2000;
//    internal const UInt32 CFM_REVISED = 0x4000;

//    internal const UInt32 CFM_BACKCOLOR = 0x04000000;
//    internal const UInt32 CFM_LCID = 0x02000000;
//    internal const UInt32 CFM_UNDERLINETYPE = 0x00800000;        // Many displayed by 3.0 
//    internal const UInt32 CFM_WEIGHT = 0x00400000;
//    internal const UInt32 CFM_SPACING = 0x00200000;      // Displayed by 3.0	
//    internal const UInt32 CFM_KERNING = 0x00100000;      // (*)	
//    internal const UInt32 CFM_STYLE = 0x00080000;        // (*)	
//    internal const UInt32 CFM_ANIMATION = 0x00040000;        // (*)	
//    internal const UInt32 CFM_REVAUTHOR = 0x00008000;

//    internal const UInt32 CFE_SUBSCRIPT = 0x00010000;        // Superscript and subscript are 
//    internal const UInt32 CFE_SUPERSCRIPT = 0x00020000;      //  mutually exclusive			 

//    internal const UInt32 CFM_SUBSCRIPT = (CFE_SUBSCRIPT | CFE_SUPERSCRIPT);
//    internal const UInt32 CFM_SUPERSCRIPT = CFM_SUBSCRIPT;

//    // CHARFORMAT "ALL" masks
//    internal const UInt32 CFM_EFFECTS = (CFM_BOLD | CFM_ITALIC | CFM_UNDERLINE | CFM_COLOR |
//                         CFM_STRIKEOUT | CFE_PROTECTED | CFM_LINK);
//    internal const UInt32 CFM_ALL = (CFM_EFFECTS | CFM_SIZE | CFM_FACE | CFM_OFFSET | CFM_CHARSET);

//    internal const UInt32 CFM_EFFECTS2 = (CFM_EFFECTS | CFM_DISABLED | CFM_SMALLCAPS | CFM_ALLCAPS
//                        | CFM_HIDDEN | CFM_OUTLINE | CFM_SHADOW | CFM_EMBOSS
//                        | CFM_IMPRINT | CFM_DISABLED | CFM_REVISED
//                        | CFM_SUBSCRIPT | CFM_SUPERSCRIPT | CFM_BACKCOLOR);

//    internal const UInt32 CFM_ALL2 = (CFM_ALL | CFM_EFFECTS2 | CFM_BACKCOLOR | CFM_LCID
//                        | CFM_UNDERLINETYPE | CFM_WEIGHT | CFM_REVAUTHOR
//                        | CFM_SPACING | CFM_KERNING | CFM_STYLE | CFM_ANIMATION);
//    #endregion
//}