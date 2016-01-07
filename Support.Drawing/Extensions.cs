using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Platform.Support.Drawing
{
    public static partial class Extensions
    {

        #region Image

        public static void FromFile(this Image @this, string filename, bool safe = true)
        {
            //_BackgroundWorker.RunWorkerAsync({this, uri.ToString})
            @this = Helpers.FromFile(filename, safe);
        }

        public static void FromURI(this Image @this, Uri uri)
        {
            //_BackgroundWorker.RunWorkerAsync({this, uri.ToString})
            @this = Helpers.FromURI(uri);
        }
        public static void FromURL(this Image @this, string url)
        {
            @this = Helpers.FromURL(url);
            //_BackgroundWorker.RunWorkerAsync({this, url})
        }

        public static void FromBytes(this Image @this, byte[] data)
        {
            @this = Helpers.FromBytes(data);
        }
        public static void FromBase64(this Image @this, string source)
        {
            @this = Helpers.FromBase64(source);
        }

        public static byte[] ToBytes(this Image @this)
        {
            return Helpers.ToBytes(@this);
        }
        public static string ToBase64(this Image @this, ImageFormat imageFormat = null)
        {
            return Helpers.ToBase64(@this, imageFormat);
        }

        public static bool DrawAdjustedImage(this Image @this, ColorMatrix cm)
        {
            return Helpers.DrawAdjustedImage(@this, cm);
        }
        public static void DrawInsetCircle(this Graphics @this, ref Rectangle r, Pen p)
        {
            Helpers.DrawInsetCircle(ref @this, ref r, p);
        }

        public static void GrayScale(this Image @this)
        {
            Helpers.GrayScale(@this);
        }
        public static void Translate(this Image @this, float red, float green, float blue, float alpha = 0)
        {
            Helpers.Translate(@this, red, green, blue, alpha);
        }
        public static void Negative(this Image @this)
        {
            Helpers.Negative(@this);
        }

        public static Image ResizeImage(this Image @this, Size size, bool preserveAspectRatio = true)
        {
            return Helpers.ResizeImage(@this, size, preserveAspectRatio);
        }

        public static Color GetDominantColor(this Image @this)
        {
            return Helpers.GetDominantColor(@this);
        }
        public static Color[] GetPalette(this Image @this)
        {
            return Helpers.GetPalette(@this);
        }

        #endregion

        #region Color
               
        public static void FromString(this Color @this, string @string)
        {
            @this = Helpers.ToColor(@string);
        }

        public static Color GetDarkColor(this Color @this, byte d)
        {
            return Helpers.GetDarkColor(@this, d);
        }
        public static Color GetLightColor(this Color @this, byte d)
        {
            return Helpers.GetLightColor(@this, d);
        }

        public static int GetLuminosity(this Color @this)
        {
            return Helpers.GetLuminosity(@this);
        }

        public static Color ChangeColorBrightness(this Color @this, float correctionFactor)
        {
            return Helpers.ChangeColorBrightness(@this, correctionFactor);
        }

        public static Color LightenBy(this Color @this, int percent)
        {
            return Helpers.LightenBy(@this, percent);
        }

        public static Color DarkenBy(this Color @this, int percent)
        {
            return Helpers.DarkenBy(@this, percent);
        }

        #endregion
        
    }
}
