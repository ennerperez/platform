using System;

namespace Support.Drawing
{
    public static class Extensions
    {

        #region Image
        
        public static void FromFile(this System.Drawing.Image @this, string filename, bool safe = true)
        {
            //_BackgroundWorker.RunWorkerAsync({this, uri.ToString})
            @this = Helpers.FromFile(filename, safe);
        }

        public static void FromURI(this System.Drawing.Image @this, Uri uri)
        {
            //_BackgroundWorker.RunWorkerAsync({this, uri.ToString})
            @this = Helpers.FromURI(uri);
        }
        public static void FromURL(this System.Drawing.Image @this, string url)
        {
            @this = Helpers.FromURL(url);
            //_BackgroundWorker.RunWorkerAsync({this, url})
        }

        public static void FromBytes(this System.Drawing.Image @this, byte[] data)
        {
            @this = Helpers.FromBytes(data);
        }
        public static void FromBase64(this System.Drawing.Image @this, string source)
        {
            @this = Helpers.FromBase64(source);
        }

        public static byte[] ToBytes(this System.Drawing.Image @this)
        {
            return Helpers.ToBytes(@this);
        }
        public static string ToBase64(this System.Drawing.Image @this, System.Drawing.Imaging.ImageFormat imageFormat = null)
        {
            return Helpers.ToBase64(@this, imageFormat);
        }

        public static System.Drawing.Color GetDominantColor(this System.Drawing.Image @this)
        {
            return Helpers.GetDominantColor(@this);
        }
        public static System.Drawing.Color[] GetPalette(this System.Drawing.Image @this)
        {
            return Helpers.GetPalette(@this);
        }

        public static bool DrawAdjustedImage(this System.Drawing.Image @this, System.Drawing.Imaging.ColorMatrix cm)
        {
            return Helpers.DrawAdjustedImage(@this, cm);
        }
        public static void GrayScale(this System.Drawing.Image @this)
        {
            Helpers.GrayScale(@this);
        }
        public static void Translate(this System.Drawing.Image @this, float red, float green, float blue, float alpha = 0)
        {
            Helpers.Translate(@this, red, green, blue, alpha);
        }
        public static void Negative(this System.Drawing.Image @this)
        {
            Helpers.Negative(@this);
        }

        public static System.Drawing.Image ResizeImage(this System.Drawing.Image @this, System.Drawing.Size size, bool preserveAspectRatio = true)
        {
            return Helpers.ResizeImage(@this, size, preserveAspectRatio);
        }

        #endregion

        #region Color

        public static void FromHex(this System.Drawing.Color @this, string hex, int alpha = 255)
        {
            @this = Helpers.FromHex(hex, alpha);
        }
        public static void FromString(this System.Drawing.Color @this, string @string)
        {
            @this = Helpers.FromString(@string);
        }

        public static string ToHex(this System.Drawing.Color @this)
        {
            return Helpers.ToHex(@this);
        }

        public static System.Drawing.Color GetDarkColor(this System.Drawing.Color @this, byte d)
        {
            return Helpers.GetDarkColor(@this, d);
        }
        public static System.Drawing.Color GetLightColor(this System.Drawing.Color @this, byte d)
        {
            return Helpers.GetLightColor(@this, d);
        }

        public static int GetLuminosity(this System.Drawing.Color @this)
        {
            return Helpers.GetLuminosity(@this);
        }

        public static System.Drawing.Color ChangeColorBrightness(this System.Drawing.Color @this, float correctionFactor)
        {
            return Helpers.ChangeColorBrightness(@this, correctionFactor);
        }

        public static System.Drawing.Color LightenBy(this System.Drawing.Color @this, int percent)
        {
            return Helpers.LightenBy(@this, percent);
        }

        public static System.Drawing.Color DarkenBy(this System.Drawing.Color @this, int percent)
        {
            return Helpers.DarkenBy(@this, percent);
        }

        #endregion


    }
}
