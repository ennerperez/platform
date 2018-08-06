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
        public static Color Lerp(this Color from, Color to, float amount)
        {
            return Color.FromArgb((int)Math.Lerp(from.R, to.R, amount),
                (int)Math.Lerp(from.G, to.G, amount),
                (int)Math.Lerp(from.B, to.B, amount));
        }

        public static Color GetDarkColor(this Color color, byte d)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            if ((color.R > d))
                r = (byte)(color.R - d);
            if ((color.G > d))
                g = (byte)(color.G - d);
            if ((color.B > d))
                b = (byte)(color.B - d);

            Color c1 = Color.FromArgb(r, g, b);
            return c1;
        }

        public static Color GetLightColor(this Color color, byte d)
        {
            byte r = 255;
            byte g = 255;
            byte b = 255;

            if (((int)color.R + (int)d <= 255))
                r = (byte)(color.R + d);
            if (((int)color.G + (int)d <= 255))
                g = (byte)(color.G + d);
            if (((int)color.B + (int)d <= 255))
                b = (byte)(color.B + d);

            Color c2 = Color.FromArgb(r, g, b);
            return c2;
        }

        public static int GetLuminosity(this Color color)
        {
            int num = System.Math.Max(System.Math.Max(color.R, color.G), color.B) + System.Math.Min(System.Math.Min(color.R, color.G), color.B);
            return ((num * 240) + 0xff) / 510;
        }

        public static int PerceivedBrightness(this Color color)
        {
            return (int)System.Math.Sqrt(
                color.R * color.R * .299 +
                color.G * color.G * .587 +
                color.B * color.B * .114);
        }

        public static Color ChangeColorBrightness(this Color color, float correctionFactor)
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

        public static Color LightenBy(this Color color, int percent)
        {
            return ChangeColorBrightness(color, (float)(percent / 100.0));
        }

        public static Color DarkenBy(this Color color, int percent)
        {
            return ChangeColorBrightness(color, (float)(-1 * percent / 100.0));
        }

        public static Color Invert(this Color color)
        {
            return Color.FromArgb(color.ToArgb() ^ 0xffffff);
        }

        public static Color Mix(this IEnumerable<Color> colors)
        {
            int a = 0;
            int r = 0;
            int g = 0;
            int b = 0;
            int count = 0;

            foreach (Color color in colors)
                if (!color.Equals(Color.Empty))
                {
                    a += color.A;
                    r += color.R;
                    g += color.G;
                    b += color.B;
                    count++;
                }

            if (count == 0)
                return Color.Empty;

            return Color.FromArgb(a / count, r / count, g / count, b / count);
        }

        public static Color VisibleTextColor(this Color color)
        {
            return PerceivedBrightness(color) > 130 ? Color.Black : Color.White;
        }

        public static Color GetDominantColor(this Image source)
        {
            int totalR = 0;
            int totalG = 0;
            int totalB = 0;

            Bitmap bmp = new Bitmap(source);

            for (int x = 0; x <= source.Width - 1; x++)
            {
                for (int y = 0; y <= source.Height - 1; y++)
                {
                    Color pixel = bmp.GetPixel(x, y);
                    totalR += pixel.R;
                    totalG += pixel.G;
                    totalB += pixel.B;
                }
            }

            int totalPixels = source.Height * source.Width;
            int averageR = totalR / totalPixels;
            int averageg = totalG / totalPixels;
            int averageb = totalB / totalPixels;
            return Color.FromArgb(averageR, averageg, averageb);
        }

        public static IEnumerable<Color> GetPalette(this Image @this)
        {
            return Bitmaps.Utilities.GetPalette(@this);
        }

        public static Image Tint(this Image source, Color tint)
        {
            return Tint(source, tint.B, tint.G, tint.R);
        }

        public static Image Tint(this Image source, float blueTint, float greenTint, float redTint)
        {
            var bitmap = new Bitmap(source);

            BitmapData sourceData = bitmap.LockBits(new Rectangle(0, 0,
                                    bitmap.Width, bitmap.Height),
                                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            bitmap.UnlockBits(sourceData);
            float blue = 0;
            float green = 0;
            float red = 0;
            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                blue = pixelBuffer[k] + (255 - pixelBuffer[k]) * blueTint;
                green = pixelBuffer[k + 1] + (255 - pixelBuffer[k + 1]) * greenTint;
                red = pixelBuffer[k + 2] + (255 - pixelBuffer[k + 2]) * redTint;
                if (blue > 255) { blue = 255; }
                if (green > 255) { green = 255; }
                if (red > 255) { red = 255; }
                pixelBuffer[k] = (byte)blue;
                pixelBuffer[k + 1] = (byte)green;
                pixelBuffer[k + 2] = (byte)red;
            }

            Bitmap resultBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                    resultBitmap.Width, resultBitmap.Height),
                                    ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }
    }
}