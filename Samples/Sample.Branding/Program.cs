using Platform.Support;
using Platform.Support.Attributes;
using System;
using System.Windows.Forms;

namespace Sample.Branding
{
    [Tag(Tags.Important, "Demo")]
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}