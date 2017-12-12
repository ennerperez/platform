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

            var source = new List<Sample.Models.Person>()
                {
                    new Models.Person(){ Name="Enner Pérez", Age =31},
                    new Models.Person(){ Name="Richard Pérez", Age =30},
                    new Models.Person(){ Name="Jamile Pérez", Age =26},
                    new Models.Person(){ Name="Mariana Pérez", Age =24},
                    new Models.Person(){ Name="Suami Cepeda", Age =30},
                };

            if ((sender as ToolStripMenuItem).Text == "Person")
            {
                Report.Model = source[0];
            }
            else if ((sender as ToolStripMenuItem).Text == "Persons")
            {
                Report.Model = source;
            }
            Report.Template = System.IO.File.ReadAllText($"reports\\{(sender as ToolStripMenuItem).Text}.cshtml");
            Report.Refresh();
        }
    }
}