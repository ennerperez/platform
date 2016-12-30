using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing
{

    public static partial class ColorHelper
    {
        public static string ToHEX(Color source)
        {
            return ToHEX(source.R, source.G, source.B);
        }
        public static string ToHEX(int r, int g, int b)
        {
            return "#" + System.Drawing.ColorTranslator.FromHtml(string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b)).Name.Remove(0, 2);
        }

        public static Color ToColor(string hex, int alpha = 255)
        {
            System.Drawing.Color _return;

            try
            {
                _return = System.Drawing.ColorTranslator.FromHtml(hex);
            }
            catch
            {
                throw new Exception("Hexadecimal string is not a valid color format");
            }

            return _return;
        }

        //public static Color HEX(string val)
        //{
        //    return ToColor(val);
        //}

    }

    public static partial class ColorExtensions
    {

        public static void FromHEX(this Color @this, string hex, int alpha = 255)
        {
            @this = ColorHelper.ToColor(hex, alpha);
        }

        public static string ToHEX(this Color @this)
        {
            return ColorHelper.ToHEX(@this);
        }

    }
}
