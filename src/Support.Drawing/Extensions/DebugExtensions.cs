using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Platform.Support.Drawing
{
    /// <summary>
    /// Class DebugExtensions.
    /// </summary>
    public static partial class Extensions
    {
        [Conditional("DEBUG")]
        public static void AsEditorIcon(this Image img, Color? color = null)
        {
            if (color == null) color = Color.FromArgb(43, 43, 43);
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            using (Graphics gfx = Graphics.FromImage(bmp))
            using (SolidBrush brush = new SolidBrush(color.Value))
            {
                gfx.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
                gfx.DrawImage(img, 0, 0);
            }

            var assemblyName = Assembly.GetEntryAssembly().GetName().Name;

            bmp.Save(@"..\..\.editoricon.png");
            ImageHelper.ConvertToIcon(@"..\..\.editoricon.png", $@"..\..\{assemblyName}\App.ico", (img.Width + img.Height) / 2, true);
        }
    }
}