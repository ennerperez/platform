using Platform.Support;
using Platform.Support.Attributes;
using Platform.Support.Drawing;
using Platform.Support.Drawing.Colors;
using System;
using System.IO;
using System.Windows.Forms;

namespace Platform.Samples
{
    [Tag(Tags.Important, "Sample")]
    internal static class Program
    {
        public static Palette Palette { get; set; }

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (File.Exists("Palettes\\colors_blue_indigo.xml"))
                Palette = Palette.ReadFrom("Palettes\\colors_blue_indigo.xml");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.FormMain());
        }
    }
}