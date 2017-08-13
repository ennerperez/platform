using System.Drawing;

namespace Platform.Support.Drawing
{
    public static partial class ColorHelper
    {
        public static Color ToColor(int r, int g, int b)
        {
            return Color.FromArgb(r, g, b);
        }

        public static string RGB(Color source)
        {
            return string.Join(",", new string[] { source.R.ToString(), source.G.ToString(), source.B.ToString() });
        }

        public static string RGBA(Color source)
        {
            return string.Join(",", new string[] { source.R.ToString(), source.G.ToString(), source.B.ToString(), source.A.ToString() });
        }
    }

    public static partial class ColorExtensions
    {
    }
}