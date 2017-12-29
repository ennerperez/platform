using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Platform.Support.Branding;

namespace Platform.Samples.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            var assembly = Assembly.GetEntryAssembly();
            commandLinkBrand.Enabled = assembly.IsBranded();
        }

        private void commandLinkBrand_Click(object sender, System.EventArgs e)
        {
            var child = new Forms.FormBrand();
            child.ShowDialog();
        }

        private void commandLinkConsole_Click(object sender, System.EventArgs e)
        {
            Process.Start("Samples.Console.exe");
        }

        private void commandLinkReports_Click(object sender, EventArgs e)
        {
            var child = new Forms.FormReports();
            child.ShowDialog();
        }
    }
}