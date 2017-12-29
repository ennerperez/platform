using Platform.Support.Branding;
using Platform.Support.Drawing;
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
            Icon = ReflectionHelper.Icon();

            if (assembly.IsBranded())
            {
                if (assembly.BrandProduct() != null)
                {
                    var bproduct = assembly.BrandProduct().Value;
                    labelInfo.Text = bproduct.Name + Environment.NewLine + bproduct.Description;
                    richTextBoxEULA.Text = bproduct.EULA;
                    var icon = ImageHelper.FromBytes(assembly.BrandLogo("main"));
                    this.Icon = ImageHelper.CreateIcon((Bitmap)icon, icon.Width);
                }
                linkLabelURL.Text = assembly.BrandURL("main");
                pictureBoxBrandBanner.BackColor = ColorHelper.ToColor(assembly.BrandColor("main"));
                pictureBoxBrandBanner.Image = ImageHelper.FromBytes(assembly.BrandLogo("sidebar"));
                pictureBoxBrandLogo.Image = ImageHelper.FromBytes(assembly.BrandLogo("main"));
                richTextBoxEULA.Text = richTextBoxEULA.Text + Environment.NewLine + assembly.BrandEULA();
            }
        }
    }
}