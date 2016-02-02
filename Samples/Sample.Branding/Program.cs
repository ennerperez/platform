using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Platform.Support;
using Platform.Support.Branding;
using Platform.Support.Attributes;

namespace Sample.Branding
{
    [Tag(Tags.Important, "Demo")]
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
