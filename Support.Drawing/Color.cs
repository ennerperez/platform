using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Drawing
{
    public static class Color
    {

        public static System.Drawing.Color FromHex(string hex, int alpha = 255)
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
        public static System.Drawing.Color FromString(string source)
        {
            System.Drawing.Color _return;

            try
            {
                int r;
                int g;
                int b;
                int a;
                string[] _source = source.Split(',');

                r = int.Parse(_source[0]);
                g = int.Parse(_source[1]);
                b = int.Parse(_source[2]);

                if (_source.Length < 4)
                {
                    _return = System.Drawing.Color.FromArgb(r, g, b);
                }
                else
                {
                    a = int.Parse(_source[3]);
                    _return = System.Drawing.Color.FromArgb(a, r, g, b);
                }
            }
            catch
            {
                throw new Exception("String is not a valid color format");
            }

            return _return;
        }

        public static string ToHex(System.Drawing.Color source)
        {
            return ToHex(source.R, source.G, source.B);
        }
        public static string ToHex(int r, int g, int b)
        {
            return "#" + System.Drawing.ColorTranslator.FromHtml(string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b)).Name.Remove(0, 2);
        }
        public static System.Drawing.Color Hex(string val)
        {
            return FromHex(val);
        }

        public static System.Drawing.Color rgb(int r, int g, int b)
        {
            return System.Drawing.Color.FromArgb(r, g, b);
        }

    }
}
