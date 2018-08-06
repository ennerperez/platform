using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Platform.Samples.Forms
{
    public partial class FormControls : Form
    {
        public FormControls()
        {
            InitializeComponent();
        }

        private void FormControls_Load(object sender, EventArgs e)
        {
            var assembly = System.Reflection.Assembly.GetAssembly(typeof(Platform.Presentation.Forms.Controls.CommandLink));
            var controls = from item in assembly.GetTypes()
                           where item.FullName.Contains("Platform.Presentation.Forms.Controls")
                           && item.GetInterface("System.ComponentModel.IComponent") != null
                           orderby item.Name
                           select item;

            comboBoxControls.DisplayMember = "Name";
            comboBoxControls.DataSource = controls.ToList();
        }

        private void comboBoxControls_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelPreview.Controls.Clear();
            propertyGridControl.SelectedObject = null;
            try
            {
                var type = (Type)comboBoxControls.SelectedValue;
                var control = (Control)Activator.CreateInstance(type);
                control.Dock = DockStyle.Top;
                panelPreview.Controls.Add(control);
                propertyGridControl.SelectedObject = control;
            }
            catch (Exception)
            {
            }
        }
    }
}