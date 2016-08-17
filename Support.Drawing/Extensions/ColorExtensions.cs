using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Platform.Support.Drawing
{
    public static partial class ColorExtensions
    {

        public static void FromString(this Color @this, string @string)
        {
            @this = ColorHelpers.ToColor(@string);
        }

        public static Color GetDarkColor(this Color @this, byte d)
        {
            return ColorHelpers.GetDarkColor(@this, d);
        }
        public static Color GetLightColor(this Color @this, byte d)
        {
            return ColorHelpers.GetLightColor(@this, d);
        }

        public static int GetLuminosity(this Color @this)
        {
            return ColorHelpers.GetLuminosity(@this);
        }

        public static Color ChangeColorBrightness(this Color @this, float correctionFactor)
        {
            return ColorHelpers.ChangeColorBrightness(@this, correctionFactor);
        }

        public static Color LightenBy(this Color @this, int percent)
        {
            return ColorHelpers.LightenBy(@this, percent);
        }

        public static Color DarkenBy(this Color @this, int percent)
        {
            return ColorHelpers.DarkenBy(@this, percent);
        }



    }
}
