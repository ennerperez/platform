using System.Drawing;

namespace Platform.Support.Drawing
{
    public static partial class ColorExtensions
    {
        public static void FromString(this Color @this, string @string)
        {
            @this = ColorHelper.ToColor(@string);
        }

        public static Color GetDarkColor(this Color @this, byte d)
        {
            return ColorHelper.GetDarkColor(@this, d);
        }

        public static Color GetLightColor(this Color @this, byte d)
        {
            return ColorHelper.GetLightColor(@this, d);
        }

        public static int GetLuminosity(this Color @this)
        {
            return ColorHelper.GetLuminosity(@this);
        }

        public static Color ChangeColorBrightness(this Color @this, float correctionFactor)
        {
            return ColorHelper.ChangeColorBrightness(@this, correctionFactor);
        }

        public static Color LightenBy(this Color @this, int percent)
        {
            return ColorHelper.LightenBy(@this, percent);
        }

        public static Color DarkenBy(this Color @this, int percent)
        {
            return ColorHelper.DarkenBy(@this, percent);
        }

        public static Color Invert(this Color @this)
        {
            return ColorHelper.Invert(@this);
        }
    }
}