using Platform.Support.Branding;
using Platform.Support.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sample.Branding
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //BrandingManager brand = new BrandingManager();
            //brand.Load(assembly.DirectoryPath(), assembly.GUID());

            if (assembly.IsBranded())
            {
                if (assembly.BrandProduct() != null)
                {
                    var bproduct = assembly.BrandProduct().Value;
                    labelInfo.Text = bproduct.Name + Environment.NewLine + bproduct.Description;
                    richTextBoxEULA.Text = bproduct.EULA;
                }
                linkLabelURL.Text = assembly.BrandURL("main");
                pictureBoxBrandBanner.Image = Helpers.FromBytes( assembly.BrandLogo("sidebar"));
                pictureBoxBrandLogo.Image = Helpers.FromBytes(assembly.BrandLogo("main"));
                richTextBoxEULA.Text = richTextBoxEULA.Text+ Environment.NewLine + assembly.BrandEULA();
            }
        }
    }
}
