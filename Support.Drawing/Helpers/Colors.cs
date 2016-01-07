using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing
{
    public static partial class Helpers
    {
        
        public static double ValidColor(double number)
        {
            return number.Between(0, 1);
        }

        public static int ValidColor(int number)
        {
            return number.Between(0, 255);
        }

        public static byte ValidColor(byte number)
        {
            return number.Between(0, 255);
        }

        public static Color ToColor(string source)
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

        public static Color RandomColor()
        {
            return Color.FromArgb(Maths.Helpers.Random(255), Maths.Helpers.Random(255), Maths.Helpers.Random(255));
        }

        public static Color ParseColor(string color)
        {
            if (color.StartsWith("#"))
            {
                return ToColor(color);
            }

            if (color.Contains(','))
            {
                int[] colors = color.Split(',').Select(x => int.Parse(x.Trim())).ToArray();

                if (colors.Length == 3)
                {
                    return Color.FromArgb(colors[0], colors[1], colors[2]);
                }

                if (colors.Length == 4)
                {
                    return Color.FromArgb(colors[0], colors[1], colors[2], colors[3]);
                }
            }

            return Color.FromName(color);
        }

        public static int HexToDecimal(string hex)
        {
            return Convert.ToInt32(hex, 16);
        }

        public static Color Mix(List<Color> colors)
        {
            int a = 0;
            int r = 0;
            int g = 0;
            int b = 0;
            int count = 0;

            foreach (Color color in colors)
            {
                if (!color.Equals(Color.Empty))
                {
                    a += color.A;
                    r += color.R;
                    g += color.G;
                    b += color.B;
                    count++;
                }
            }

            if (count == 0)
            {
                return Color.Empty;
            }

            return Color.FromArgb(a / count, r / count, g / count, b / count);
        }

        public static int PerceivedBrightness(Color c)
        {
            return (int)Math.Sqrt(
                c.R * c.R * .299 +
                c.G * c.G * .587 +
                c.B * c.B * .114);
        }

        public static Color VisibleTextColor(Color c)
        {
            return PerceivedBrightness(c) > 130 ? Color.Black : Color.White;
        }

        public static Color Lerp(Color from, Color to, float amount)
        {
            return Color.FromArgb((int)Maths.Helpers.Lerp(from.R, to.R, amount),
                (int)Maths.Helpers.Lerp(from.G, to.G, amount),
                (int)Maths.Helpers.Lerp(from.B, to.B, amount));
        }
        
        public static Color RGB(int r, int g, int b)
        {
            return Color.FromArgb(r, g, b);
        }

        public static Color GetDarkColor(Color c, byte d)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            if ((c.R > d))
                r = (byte)(c.R - d);
            if ((c.G > d))
                g = (byte)(c.G - d);
            if ((c.B > d))
                b = (byte)(c.B - d);

            Color c1 = Color.FromArgb(r, g, b);
            return c1;
        }

        public static Color GetLightColor(Color c, byte d)
        {
            byte r = 255;
            byte g = 255;
            byte b = 255;

            if (((int)c.R + (int)d <= 255))
                r = (byte)(c.R + d);
            if (((int)c.G + (int)d <= 255))
                g = (byte)(c.G + d);
            if (((int)c.B + (int)d <= 255))
                b = (byte)(c.B + d);

            Color c2 = Color.FromArgb(r, g, b);
            return c2;
        }

        public static int GetLuminosity(Color color)
        {
            int num = System.Math.Max(System.Math.Max(color.R, color.G), color.B) + System.Math.Min(System.Math.Min(color.R, color.G), color.B);
            return ((num * 240) + 0xff) / 510;
        }

        public static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        public static Color LightenBy(Color color, int percent)
        {
            return ChangeColorBrightness(color, (float)(percent / 100.0));
        }

        public static Color DarkenBy(Color color, int percent)
        {
            return ChangeColorBrightness(color, (float)(-1 * percent / 100.0));
        }


    }
}
