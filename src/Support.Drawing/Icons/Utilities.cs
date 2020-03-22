using Platform.Support.Windows;
using System;
using System.Drawing;

namespace Platform.Support.Drawing.Icons
{
    public static partial class Utilities
    {
        public static Icon ExtractIcon(string file, int number, bool largeIcon)
        {
#if NETFX_40 || NETFX_45
            IntPtr large;
            IntPtr small;
            Shell32.ExtractIconExW(file, number, out large, out small, 1);
#else
            Shell32.ExtractIconExW(file, number, out IntPtr large, out IntPtr small, 1);
#endif
            try
            {
                return Icon.FromHandle(largeIcon ? large : small);
            }
            catch (Exception ex)
            {
                ex.DebugThis();
                return null;
            }
        }
    }
}