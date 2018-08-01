using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Platform.Support.Drawing
{
    public static partial class Extensions
    {
        public static Color FromHex(this Color color, string value)
        {
            value = (!value.StartsWith("#") ? "#" : "") + value;
            if (!string.IsNullOrEmpty(value) && Regex.IsMatch(value, "^#[A-Fa-f0-9]{2,6}", RegexOptions.IgnoreCase))
            {
                try
                {
                    color = ColorTranslator.FromHtml(value);
                }
                catch (Exception)
                {
                }
            }
            else
                color = Color.Empty;
            return color;
        }

        public static Color FromHsb(this Color color, float hue, float saturation, float brightness)
        {
            color = FromHsb(color, 255, hue, saturation, brightness);
            return color;
        }

        public static Color FromHsb(this Color color, int alpha, float hue, float saturation, float brightness)
        {
            int d = 1530;
            int mid;
            int max = (int)System.Math.Round(brightness * 255);
            int min = (int)System.Math.Round((1.0 - saturation) * (brightness / 1.0) * 255);
            double q = (double)(max - min) / 255;

            if (hue >= 0 && hue <= (double)1 / 6)
            {
                mid = (int)System.Math.Round(((hue - 0) * q) * d + min);
                color = Color.FromArgb(alpha, max, mid, min);
            }

            if (hue <= (double)1 / 3)
            {
                mid = (int)System.Math.Round(-((hue - (double)1 / 6) * q) * d + max);
                color = Color.FromArgb(alpha, mid, max, min);
            }

            if (hue <= 0.5)
            {
                mid = (int)System.Math.Round(((hue - (double)1 / 3) * q) * d + min);
                color = Color.FromArgb(alpha, min, max, mid);
            }

            if (hue <= (double)2 / 3)
            {
                mid = (int)System.Math.Round(-((hue - 0.5) * q) * d + max);
                color = Color.FromArgb(alpha, min, mid, max);
            }

            if (hue <= (double)5 / 6)
            {
                mid = (int)System.Math.Round(((hue - (double)2 / 3) * q) * d + min);
                return Color.FromArgb(alpha, mid, min, max);
            }

            if (hue <= 1.0)
            {
                mid = (int)System.Math.Round(-((hue - (double)5 / 6) * q) * d + max);
                color = Color.FromArgb(alpha, max, min, mid);
            }

            color = Color.Empty;
            return color;
        }

        public static Color FromCmyk(this Color color, float cyan, float magenta, float yellow, float key)
        {
            color = FromCmyk(color, 255, cyan, magenta, yellow, key);
            return color;
        }

        public static Color FromCmyk(this Color color, int alpha, float cyan, float magenta, float yellow, float key)
        {
            if (cyan == 0 && magenta == 0 && yellow == 0 && key == 1)
            {
                return Color.FromArgb(alpha, 0, 0, 0);
            }

            double c = cyan * (1 - key) + key;
            double m = magenta * (1 - key) + key;
            double y = yellow * (1 - key) + key;

            int r = (int)System.Math.Round((1 - c) * 255);
            int g = (int)System.Math.Round((1 - m) * 255);
            int b = (int)System.Math.Round((1 - y) * 255);

            return Color.FromArgb(alpha, r, g, b);
        }

        public static string GetRgb(this Color color)
        {
            return $"{color.R},{color.G},{color.B}";
        }

        public static string GetHex(this Color color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }

        public static string GetHsb(this Color color, bool relative = true)
        {
            if (relative)
                return $"{(int)color.GetHue()},{(int)color.GetRelativeSaturation()},{(int)color.GetRelativeBrightness()}";
            else
                return $"{color.GetHue()},{color.GetSaturation()},{color.GetBrightness()}";
        }

        public static string GetHsbRelative(this Color color)
        {
            return $"{color.GetHue()},{color.GetRelativeSaturation()},{color.GetRelativeBrightness()}";
        }

        public static float GetRelativeSaturation(this Color color)
        {
            return color.GetSaturation() * 100;
        }

        public static float GetRelativeBrightness(this Color color)
        {
            return color.GetBrightness() * 100;
        }

        public static float GetRelativeHue(this Color color)
        {
            return color.GetHue() * 360;
        }

        public static string GetCmyk(this Color color)
        {
            if (color.R == 0 && color.G == 0 && color.B == 0)
                return $"{0},{0},{0},{0}";

            double c = 1 - (color.R / 255d);
            double m = 1 - (color.G / 255d);
            double y = 1 - (color.B / 255d);
            double k = System.Math.Min(c, System.Math.Min(m, y));

            c = (c - k) / (1 - k);
            m = (m - k) / (1 - k);
            y = (y - k) / (1 - k);

            return $"{(int)(c * 100)},{(int)(m * 100)},{(int)(y * 100)},{(int)(k * 100)}";
        }

        public static double GetCyan(this Color color)
        {
            if (color.R == 0 && color.G == 0 && color.B == 0)
                return 0;

            double c = 1 - (color.R / 255d);
            double m = 1 - (color.G / 255d);
            double y = 1 - (color.B / 255d);
            double k = System.Math.Min(c, System.Math.Min(m, y));

            c = (c - k) / (1 - k);
            m = (m - k) / (1 - k);
            y = (y - k) / (1 - k);

            return c * 100;
        }

        public static double GetYellow(this Color color)
        {
            if (color.R == 0 && color.G == 0 && color.B == 0)
                return 0;

            double c = 1 - (color.R / 255d);
            double m = 1 - (color.G / 255d);
            double y = 1 - (color.B / 255d);
            double k = System.Math.Min(c, System.Math.Min(m, y));

            c = (c - k) / (1 - k);
            m = (m - k) / (1 - k);
            y = (y - k) / (1 - k);

            return y * 100;
        }

        public static double GetMagenta(this Color color)
        {
            if (color.R == 0 && color.G == 0 && color.B == 0)
                return 0;

            double c = 1 - (color.R / 255d);
            double m = 1 - (color.G / 255d);
            double y = 1 - (color.B / 255d);
            double k = System.Math.Min(c, System.Math.Min(m, y));

            c = (c - k) / (1 - k);
            m = (m - k) / (1 - k);
            y = (y - k) / (1 - k);

            return m * 100;
        }

        public static double GetKey(this Color color)
        {
            if (color.R == 0 && color.G == 0 && color.B == 0)
                return 0;

            double c = 1 - (color.R / 255d);
            double m = 1 - (color.G / 255d);
            double y = 1 - (color.B / 255d);
            double k = System.Math.Min(c, System.Math.Min(m, y));

            c = (c - k) / (1 - k);
            m = (m - k) / (1 - k);
            y = (y - k) / (1 - k);

            return k * 100;
        }

        public static string ToHexString(this Color color)
        {
            return color.GetHex();
        }

        public static string ToRgbString(this Color color)
        {
            return string.Format("Red: {0:0}, Green: {1:0}, Blue: {2:0}", color.R, color.G, color.B);
        }

        public static string ToHsbString(this Color color)
        {
            return string.Format("Hue: {0:0.0}°, Saturation: {1:0.0}%, Brightness: {2:0.0}%", color.GetHue(), color.GetRelativeSaturation(), color.GetRelativeBrightness());
        }

        public static string ToCmykString(this Color color)
        {
            return string.Format("Cyan: {0:0.0}%, Magenta: {1:0.0}%, Yellow: {2:0.0}%, Key: {3:0.0}%", color.GetCyan(), color.GetMagenta(), color.GetYellow(), color.GetKey());
        }
    }
}