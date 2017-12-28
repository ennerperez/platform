using Platform.Presentation.Reports;
using Platform.Support.Drawing;
using System;
using System.IO;
using System.Windows.Forms;

namespace Sample
{
    public class RootObject
    {
        public string ip { get; set; }
        public string hostname { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public string loc { get; set; }
        public string org { get; set; }
    }

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

            //CefAssemblyResolve.Resolve();

            Application.Run(new FormMain());
        }
    }
}