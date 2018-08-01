using Platform.Support.Branding;
using Platform.Support.Drawing;
using Platform.Support.Reflection;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Platform.Samples.Forms
{
    public partial class FormBrand : Form
    {
        public FormBrand()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            var assembly = Assembly.GetEntryAssembly();
            Icon = assembly.Icon();

            if (assembly.IsBranded())
            {
                var bproduct = assembly.BrandProduct();

                if (bproduct != null)
                {
                    labelInfo.Text = bproduct.Name + Environment.NewLine + bproduct.Description;
                    richTextBoxEULA.Text = bproduct.EULA;
                    var icon = Platform.Support.Drawing.Bitmaps.Utilities.FromBytes(assembly.BrandImage("main"));
                    Icon = icon.AsIcon(icon.Width);
                }

                var data = assembly.BrandLicenseData(null, "F03017BFED26");
                if (data != null)
                    richTextBoxLicense.Text = string.Join(System.Environment.NewLine, data);

                linkLabelURL.Text = assembly.BrandURL("main");
                pictureBoxBrandBanner.BackColor = Platform.Support.Drawing.Extensions.FromHex(pictureBoxBrandBanner.BackColor, assembly.BrandColor("main"));
                pictureBoxBrandBanner.Image = Platform.Support.Drawing.Bitmaps.Utilities.FromBytes(assembly.BrandImage("sidebar"));
                pictureBoxBrandLogo.Image = Platform.Support.Drawing.Bitmaps.Utilities.FromBytes(assembly.BrandImage("main"));
                richTextBoxEULA.Text = richTextBoxEULA.Text + Environment.NewLine + assembly.BrandEULA();
            }
        }
    }
}