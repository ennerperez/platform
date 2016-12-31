using System;
using System.Drawing;

namespace Platform.Support.Drawing
{

    public static partial class ColorHelper
    {

        public static Color ToColor(CMYK value)
        {
            if (value.Cyan == 0 && value.Magenta == 0 && value.Yellow == 0 && value.Key == 1)
            {
                return Color.FromArgb(value.Alpha, 0, 0, 0);
            }

            double c = value.Cyan * (1 - value.Key) + value.Key;
            double m = value.Magenta * (1 - value.Key) + value.Key;
            double y = value.Yellow * (1 - value.Key) + value.Key;

            int r = (int)Math.Round((1 - c) * 255);
            int g = (int)Math.Round((1 - m) * 255);
            int b = (int)Math.Round((1 - y) * 255);

            return Color.FromArgb(value.Alpha, r, g, b);
        }

        public static CMYK ToCMYK(Color color)
        {
            if (color.R == 0 && color.G == 0 && color.B == 0)
            {
                return new CMYK(0, 0, 0, 1, color.A);
            }

            double c = 1 - (color.R / 255d);
            double m = 1 - (color.G / 255d);
            double y = 1 - (color.B / 255d);
            double k = Math.Min(c, Math.Min(m, y));

            c = (c - k) / (1 - k);
            m = (m - k) / (1 - k);
            y = (y - k) / (1 - k);

            return new CMYK(c, m, y, k, color.A);
        }

    }

    public static partial class ColorExtensions
    {

        public static void FromCMYK(this Color @this, CMYK value)
        {
            @this = ColorHelper.ToColor(value);
        }

        public static CMYK ToCMYK(this Color @this)
        {
            return ColorHelper.ToCMYK(@this);
        }

    }

    public struct CMYK
    {
        private double cyan;
        private double magenta;
        private double yellow;
        private double key;
        private int alpha;

        public double Cyan
        {
            get
            {
                return cyan;
            }
            set
            {
                cyan = ColorHelper.ValidColor(value);
            }
        }

        public double Cyan100
        {
            get
            {
                return cyan * 100;
            }
            set
            {
                cyan = ColorHelper.ValidColor(value / 100);
            }
        }

        public double Magenta
        {
            get
            {
                return magenta;
            }
            set
            {
                magenta = ColorHelper.ValidColor(value);
            }
        }

        public double Magenta100
        {
            get
            {
                return magenta * 100;
            }
            set
            {
                magenta = ColorHelper.ValidColor(value / 100);
            }
        }

        public double Yellow
        {
            get
            {
                return yellow;
            }
            set
            {
                yellow = ColorHelper.ValidColor(value);
            }
        }

        public double Yellow100
        {
            get
            {
                return yellow * 100;
            }
            set
            {
                yellow = ColorHelper.ValidColor(value / 100);
            }
        }

        public double Key
        {
            get
            {
                return key;
            }
            set
            {
                key = ColorHelper.ValidColor(value);
            }
        }

        public double Key100
        {
            get
            {
                return key * 100;
            }
            set
            {
                key = ColorHelper.ValidColor(value / 100);
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

        public CMYK(double cyan, double magenta, double yellow, double key, int alpha = 255)
            : this()
        {
            Cyan = cyan;
            Magenta = magenta;
            Yellow = yellow;
            Key = key;
            Alpha = alpha;
        }

        public CMYK(int cyan, int magenta, int yellow, int key, int alpha = 255)
            : this()
        {
            Cyan100 = cyan;
            Magenta100 = magenta;
            Yellow100 = yellow;
            Key100 = key;
            Alpha = alpha;
        }

        public CMYK(Color color)
        {
            this = ColorHelper.ToCMYK(color);
        }

        public static implicit operator CMYK(Color color)
        {
            return ColorHelper.ToCMYK(color);
        }

        public static implicit operator Color(CMYK color)
        {
            return color.ToColor();
        }

        public static implicit operator HSB(CMYK color)
        {
            return color.ToColor();
        }

        public static bool operator ==(CMYK left, CMYK right)
        {
            return (left.Cyan == right.Cyan) && (left.Magenta == right.Magenta) && (left.Yellow == right.Yellow) && (left.Key == right.Key);
        }

        public static bool operator !=(CMYK left, CMYK right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            //return String.Format(Resources.CMYK_ToString_Cyan___0_0_0____Magenta___1_0_0____Yellow___2_0_0____Key___3_0_0__, Cyan100, Magenta100, Yellow100, Key100);
            return string.Format("Cyan: {0:0.0}%, Magenta: {1:0.0}%, Yellow: {2:0.0}%, Key: {3:0.0}%", Cyan100, Magenta100, Yellow100, Key100);
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
