using System;
using System.Drawing;

namespace Platform.Support.Drawing
{
    public static partial class ColorHelper
    {
        public static HSB ToHSB(Color color)
        {
            HSB hsb = new HSB();

            int Max, Min;

            if (color.R > color.G)
            {
                Max = color.R;
                Min = color.G;
            }
            else
            {
                Max = color.G;
                Min = color.R;
            }

            if (color.B > Max) Max = color.B;
            else if (color.B < Min) Min = color.B;

            int Diff = Max - Min;

            hsb.Brightness = (double)Max / 255;

            if (Max == 0) hsb.Saturation = 0;
            else hsb.Saturation = (double)Diff / Max;

            double q;
            if (Diff == 0) q = 0;
            else q = (double)60 / Diff;

            if (Max == color.R)
            {
                if (color.G < color.B) hsb.Hue = (360 + q * (color.G - color.B)) / 360;
                else hsb.Hue = q * (color.G - color.B) / 360;
            }
            else if (Max == color.G) hsb.Hue = (120 + q * (color.B - color.R)) / 360;
            else if (Max == color.B) hsb.Hue = (240 + q * (color.R - color.G)) / 360;
            else hsb.Hue = 0.0;

            hsb.Alpha = color.A;

            return hsb;
        }

        public static Color ToColor(HSB value)
        {
            int Mid;
            int Max = (int)System.Math.Round(value.Brightness * 255);
            int Min = (int)System.Math.Round((1.0 - value.Saturation) * (value.Brightness / 1.0) * 255);
            double q = (double)(Max - Min) / 255;

            if (value.Hue >= 0 && value.Hue <= (double)1 / 6)
            {
                Mid = (int)System.Math.Round(((value.Hue - 0) * q) * 1530 + Min);
                return Color.FromArgb(value.Alpha, Max, Mid, Min);
            }

            if (value.Hue <= (double)1 / 3)
            {
                Mid = (int)System.Math.Round(-((value.Hue - (double)1 / 6) * q) * 1530 + Max);
                return Color.FromArgb(value.Alpha, Mid, Max, Min);
            }

            if (value.Hue <= 0.5)
            {
                Mid = (int)System.Math.Round(((value.Hue - (double)1 / 3) * q) * 1530 + Min);
                return Color.FromArgb(value.Alpha, Min, Max, Mid);
            }

            if (value.Hue <= (double)2 / 3)
            {
                Mid = (int)System.Math.Round(-((value.Hue - 0.5) * q) * 1530 + Max);
                return Color.FromArgb(value.Alpha, Min, Mid, Max);
            }

            if (value.Hue <= (double)5 / 6)
            {
                Mid = (int)System.Math.Round(((value.Hue - (double)2 / 3) * q) * 1530 + Min);
                return Color.FromArgb(value.Alpha, Mid, Min, Max);
            }

            if (value.Hue <= 1.0)
            {
                Mid = (int)System.Math.Round(-((value.Hue - (double)5 / 6) * q) * 1530 + Max);
                return Color.FromArgb(value.Alpha, Max, Min, Mid);
            }

            return Color.FromArgb(value.Alpha, 0, 0, 0);
        }

        public static string HSB(HSB source, bool relative = true)
        {
            if (relative)
                return string.Join(",", new string[] { System.Math.Round(source.Hue360, 0).ToString(), System.Math.Round(source.Saturation100, 0).ToString(), System.Math.Round(source.Brightness100, 0).ToString() });
            else
                return string.Join(",", new string[] { source.Hue.ToString(), source.Saturation.ToString(), source.Brightness.ToString() });
        }
    }

    public static partial class ColorExtensions
    {
        public static void FromHSB(this Color @this, HSB value)
        {
            @this = ColorHelper.ToColor(value);
        }

        public static HSB ToHSB(this Color @this)
        {
            return ColorHelper.ToHSB(@this);
        }
    }

    public struct HSB
    {
        private double hue;
        private double saturation;
        private double brightness;
        private int alpha;

        public double Hue
        {
            get
            {
                return hue;
            }
            set
            {
                hue = ColorHelper.ValidColor(value);
            }
        }

        public double Hue360
        {
            get
            {
                return hue * 360;
            }
            set
            {
                hue = ColorHelper.ValidColor(value / 360);
            }
        }

        public double Saturation
        {
            get
            {
                return saturation;
            }
            set
            {
                saturation = ColorHelper.ValidColor(value);
            }
        }

        public double Saturation100
        {
            get
            {
                return saturation * 100;
            }
            set
            {
                saturation = ColorHelper.ValidColor(value / 100);
            }
        }

        public double Brightness
        {
            get
            {
                return brightness;
            }
            set
            {
                brightness = ColorHelper.ValidColor(value);
            }
        }

        public double Brightness100
        {
            get
            {
                return brightness * 100;
            }
            set
            {
                brightness = ColorHelper.ValidColor(value / 100);
            }
        }

        public int Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = ColorHelper.ValidColor(value);
            }
        }

        public HSB(double hue, double saturation, double brightness, int alpha = 255)
            : this()
        {
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
            Alpha = alpha;
        }

        public HSB(int hue, int saturation, int brightness, int alpha = 255)
            : this()
        {
            Hue360 = hue;
            Saturation100 = saturation;
            Brightness100 = brightness;
            Alpha = alpha;
        }

        public HSB(Color color)
        {
            this = ColorHelper.ToHSB(color);
        }

        public static implicit operator HSB(Color color)
        {
            return ColorHelper.ToHSB(color);
        }

        public static implicit operator Color(HSB color)
        {
            return color.ToColor();
        }

        public static implicit operator CMYK(HSB color)
        {
            return color.ToColor();
        }

        public static bool operator ==(HSB left, HSB right)
        {
            return (left.Hue == right.Hue) && (left.Saturation == right.Saturation) && (left.Brightness == right.Brightness);
        }

        public static bool operator !=(HSB left, HSB right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            //return String.Format(Resources.HSB_ToString_, Hue360, Saturation100, Brightness100);
            return string.Format("Hue: {0:0.0}°, Saturation: {1:0.0}%, Brightness: {2:0.0}%", Hue360, Saturation100, Brightness100);
        }

        public Color ToColor()
        {
            return ColorHelper.ToColor(this);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}