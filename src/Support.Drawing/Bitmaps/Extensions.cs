using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Platform.Support.Drawing.Bitmaps
{
    public static partial class Extensions
    {
        #region Image

        public static void FromFile(this Image @this, string filename, bool safe = true)
        {
            @this = Bitmaps.Utilities.FromFile(filename, safe);
        }

        public static void FromURI(this Image @this, Uri uri)
        {
            @this = Bitmaps.Utilities.FromURI(uri);
        }

        public static void FromURL(this Image @this, string url)
        {
            @this = Bitmaps.Utilities.FromURL(url);
        }

        public static void FromBytes(this Image @this, byte[] data)
        {
            @this = Bitmaps.Utilities.FromBytes(data);
        }

        public static void FromBase64(this Image @this, string source)
        {
            @this = Bitmaps.Utilities.FromBase64(source);
        }

        public static byte[] ToBytes(this Image @this)
        {
            return Bitmaps.Utilities.ToBytes(@this);
        }

        public static string ToBase64(this Image @this, ImageFormat imageFormat = null)
        {
            return Bitmaps.Utilities.ToBase64(@this, imageFormat);
        }

        public static string ToBase64ImageTag(this Image @this, ImageFormat imageFormat = null)
        {
            return Bitmaps.Utilities.ToBase64ImageTag(@this, imageFormat);
        }

        public static bool DrawAdjustedImage(this Image @this, ColorMatrix cm)
        {
            return Bitmaps.Utilities.DrawAdjustedImage(@this, cm);
        }

        public static void GrayScale(this Image @this)
        {
            Bitmaps.Utilities.GrayScale(@this);
        }

        public static void Translate(this Image @this, float red, float green, float blue, float alpha = 0)
        {
            Bitmaps.Utilities.Translate(@this, red, green, blue, alpha);
        }

        public static void Negative(this Image @this)
        {
            Bitmaps.Utilities.Negative(@this);
        }

        public static Image ResizeImage(this Image @this, Size size, bool preserveAspectRatio = true)
        {
            return Bitmaps.Utilities.ResizeImage(@this, size, preserveAspectRatio);
        }

        public static Color GetDominantColor(this Image @this)
        {
            return Bitmaps.Utilities.GetDominantColor(@this);
        }

        public static IEnumerable<Color> GetPalette(this Image @this)
        {
            return Bitmaps.Utilities.GetPalette(@this);
        }

        #endregion Image
    }
}