using Platform.Presentation.Reports.Windows.Forms;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sample
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private RazorReportViewer Report;

        private void FormMain_Shown(object sender, System.EventArgs e)
        {

            foreach (var item in System.IO.Directory.GetFiles("reports", "*.cshtml"))
            {
                var child = new ToolStripMenuItem(System.IO.Path.GetFileNameWithoutExtension(item));
                child.Click += Child_Click;
                reportsToolStripMenuItem.DropDownItems.Add(child);
            }

            Report = new RazorReportViewer()
            {
                Dock = DockStyle.Fill,
            };
            this.Controls.Add(Report);

            Report.BringToFront();
            //Report.Refresh();
            //Report.Update();

        }

        private void Child_Click(object sender, System.EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Text == "Person")
            {
                Report.Model = new Sample.Models.Person()
                {
                    Name = "Suami Cepeda",
                    Age = 27
                };
            }
            else if ((sender as ToolStripMenuItem).Text == "Persons")
            {
                Report.Model = new List<Sample.Models.Person>()
                {
                    new Models.Person(){ Name="Enner Pérez", Age =30},
                    new Models.Person(){ Name="Suami Cepeda", Age =30},
                };
                
            }
            Report.Template = System.IO.File.ReadAllText($"reports\\{(sender as ToolStripMenuItem).Text}.cshtml");
            Report.Refresh();
        }
    }
}