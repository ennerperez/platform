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
            if (File.Exists("Brand.sku"))
            {
                var viewer = new Form()
                {
                    Size = new System.Drawing.Size(640, 480),
                    Icon = Icon,
                    Text = "Brand.sku",
                    StartPosition = FormStartPosition.Manual,
                    Location = new System.Drawing.Point(this.Location.X + this.Width + 6, this.Location.Y)
                };
                var xmlViewer = new Presentation.Forms.Controls.XMLViewer()
                {
                    Dock = DockStyle.Fill,
                };
                try
                {
                    xmlViewer.LoadFile("Brand.sku");
                }
                catch (Exception)
                {
                    var xml = File.ReadAllText("Brand.sku").Replace(@"xmlns=""http://www.w3.org/2018/brandingSchema""", "");
                    xmlViewer.Load(xml);
                }

                viewer.Controls.Add(xmlViewer);
                viewer.ShowDialog();
                var child1 = new Forms.FormBrand()
                {
                    StartPosition = FormStartPosition.Manual,
                    Location = new System.Drawing.Point(this.Location.X + this.Width + 6, this.Location.Y)
                };
                child1.ShowDialog();
            }
        }

        private void commandLinkConsole_Click(object sender, System.EventArgs e)
        {
#if NETFX
            Process.Start("Samples.Console.exe");
#elif NETCORE
            Process.Start("Samples.Console.NetCore.exe");
#endif
        }

        private void commandLinkReports_Click(object sender, EventArgs e)
        {
            var child = new Forms.FormReports()
            {
                StartPosition = FormStartPosition.Manual,
                Location = new System.Drawing.Point(this.Location.X + this.Width + 6, this.Location.Y)
            };
            child.ShowDialog();
        }

        private void commandLinkControls_Click(object sender, EventArgs e)
        {
            try
            {
                var child = new Forms.FormControls()
                {
                    StartPosition = FormStartPosition.Manual,
                    Location = new System.Drawing.Point(this.Location.X + this.Width + 6, this.Location.Y)
                };
                child.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}