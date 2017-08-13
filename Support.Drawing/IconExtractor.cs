using Platform.Support.Windows;
using System;
using System.Drawing;

namespace Platform.Support.Drawing
{
    public class IconExtractor
    {
        public static Icon Extract(string file, int number, bool largeIcon)
        {
            IntPtr large;
            IntPtr small;
            Shell32.ExtractIconExW(file, number, out large, out small, 1);
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