using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Platform.Support.Reflection;
using System.IO;
using Platform.Support.Drawing;

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

    static class Program
    {

        public static Palette Palette { get; set; }

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (File.Exists("Palettes\\colors_blue_indigo.xml"))
                Palette = Palette.ReadFrom("Palettes\\colors_blue_indigo.xml");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
