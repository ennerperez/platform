using System;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

    public static partial class Extensions
    {
        public static int RandomAdd(this int num, int min, int max)
        {
            return num + Math.Random(min, max);
        }

        public static int Min(this int num, int min)
        {
            return (num < min ? min : num);
        }

        public static int Max(this int num, int max)
        {
            return (num > max ? max : num);
        }

        public static int Between(this int num, int min, int max)
        {
            if (num <= min) return min;
            if (num >= max) return max;
            return num;
        }

        public static float Between(this float num, float min, float max)
        {
            if (num <= min) return min;
            if (num >= max) return max;
            return num;
        }

        public static double Between(this double num, double min, double max)
        {
            if (num <= min) return min;
            if (num >= max) return max;
            return num;
        }

        public static byte Between(this byte num, byte min, byte max)
        {
            if (num <= min) return min;
            if (num >= max) return max;
            return num;
        }

        public static bool IsBetween(this int num, int min, int max)
        {
            return num >= min && num <= max;
        }

        public static bool IsBetween(this byte num, int min, int max)
        {
            return num >= min && num <= max;
        }

        public static int BetweenOrDefault(this int num, int min, int max, int defaultValue = 0)
        {
            return (num.IsBetween(min, max) ? num : defaultValue);
        }

        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        private static readonly string[] suffixDecimal = new[] { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
        private static readonly string[] suffixBinary = new[] { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB" };

        public static string ToSizeString(this long size, bool binary = false, int decimalPlaces = 2)
        {
            if (size < 1024) return System.Math.Max(size, 0) + " B";
            int place = (int)System.Math.Floor(System.Math.Log(size, 1024));
            double num = size / System.Math.Pow(1024, place);
            string suffix = binary ? suffixBinary[place] : suffixDecimal[place];
            return string.Format("{0} {1}", num.ToDecimalString(decimalPlaces.Between(0, 3)), suffix);
        }

        public static string ToDecimalString(this double number, int decimalPlaces)
        {
            string format = "0";
            if (decimalPlaces > 0) format += "." + new string('0', decimalPlaces);
            return number.ToString(format);
        }

        public static string ToBase(this int value, int radix, string digits)
        {
            if (string.IsNullOrEmpty(digits))
            {
                throw new ArgumentNullException("digits", string.Format("Digits must contain character value representations"));
            }

            radix = System.Math.Abs(radix);
            if (radix > digits.Length || radix < 2)
            {
#if (!PORTABLE)
                throw new ArgumentOutOfRangeException("radix", radix, string.Format("Radix has to be > 2 and < {0}", digits.Length));
#else
                    throw new ArgumentOutOfRangeException("radix", radix.ToString());
#endif
            }

            string result = string.Empty;
            int quotient = System.Math.Abs(value);
            while (0 < quotient)
            {
                int temp = quotient % radix;
                result = digits[temp] + result;
                quotient /= radix;
            }
            return result;
        }
    }

#if PORTABLE
    }

#endif
}