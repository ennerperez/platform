using Platform.Support.Windows;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Platform.Presentation.Forms
{
    public static partial class Extensions
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