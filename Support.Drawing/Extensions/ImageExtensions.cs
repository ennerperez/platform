using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Drawing
{
    public static partial class ImageExtensions
    {

        #region Image

        public static void FromFile(this Image @this, string filename, bool safe = true)
        {
            //_BackgroundWorker.RunWorkerAsync({this, uri.ToString})
            @this = ImageHelpers.FromFile(filename, safe);
        }

        public static void FromURI(this Image @this, Uri uri)
        {
            //_BackgroundWorker.RunWorkerAsync({this, uri.ToString})
            @this = ImageHelpers.FromURI(uri);
        }
        public static void FromURL(this Image @this, string url)
        {
            @this = ImageHelpers.FromURL(url);
            //_BackgroundWorker.RunWorkerAsync({this, url})
        }

        public static void FromBytes(this Image @this, byte[] data)
        {
            @this = ImageHelpers.FromBytes(data);
        }
        public static void FromBase64(this Image @this, string source)
        {
            @this = ImageHelpers.FromBase64(source);
        }

        public static byte[] ToBytes(this Image @this)
        {
            return ImageHelpers.ToBytes(@this);
        }
        public static string ToBase64(this Image @this, ImageFormat imageFormat = null)
        {
            return ImageHelpers.ToBase64(@this, imageFormat);
        }
        public static string ToBase64ImageTag(this Image @this, ImageFormat imageFormat = null)
        {
            return ImageHelpers.ToBase64ImageTag(@this, imageFormat);
        }
        public static bool DrawAdjustedImage(this Image @this, ColorMatrix cm)
        {
            return ImageHelpers.DrawAdjustedImage(@this, cm);
        }
        public static void DrawInsetCircle(this Graphics @this, ref Rectangle r, Pen p)
        {
            GeometricHelpers.DrawInsetCircle(ref @this, ref r, p);
        }

        public static void GrayScale(this Image @this)
        {
            ImageHelpers.GrayScale(@this);
        }
        public static void Translate(this Image @this, float red, float green, float blue, float alpha = 0)
        {
            ImageHelpers.Translate(@this, red, green, blue, alpha);
        }
        public static void Negative(this Image @this)
        {
            ImageHelpers.Negative(@this);
        }

        public static Image ResizeImage(this Image @this, Size size, bool preserveAspectRatio = true)
        {
            return ImageHelpers.ResizeImage(@this, size, preserveAspectRatio);
        }

        public static Color GetDominantColor(this Image @this)
        {
            return ImageHelpers.GetDominantColor(@this);
        }
        public static Color[] GetPalette(this Image @this)
        {
            return ImageHelpers.GetPalette(@this);
        }

        #endregion

    }
}
