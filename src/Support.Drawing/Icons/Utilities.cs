using Platform.Support.Windows;
using System;
using System.Drawing;

namespace Platform.Support.Drawing.Icons
{
    public static partial class Utilities
    {
        public static Icon ExtractIcon(string file, int number, bool largeIcon)
        {
            Shell32.ExtractIconExW(file, number, out IntPtr large, out IntPtr small, 1);
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