using Platform.Presentation.Reports.Windows.Forms;
using Platform.Samples.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform.Samples.Forms
{
    public partial class FormReports : Form
    {
        public FormReports()
        {
            InitializeComponent();
        }

        public Control ReportViewer { get; internal set; }

        private void FormMain_Shown(object sender, System.EventArgs e)
        {
            foreach (var item in System.IO.Directory.GetFiles("reports", "*.cshtml"))
            {
                var child = new ToolStripMenuItem(System.IO.Path.GetFileNameWithoutExtension(item));
                child.Click += RazorChild_Click;
                reportsToolStripMenuItem.DropDownItems.Add(child);
            }
        }

        private void RazorChild_Click(object sender, System.EventArgs e)
        {
            if (Controls.Contains(ReportViewer)) Controls.Remove(ReportViewer);

            var Report = new RazorReportViewer()
            {
                Dock = DockStyle.Fill,
            };

            var source = new List<Person>()
                {
                    new Person(){ Name="Enner Pérez", Age =31},
                    new Person(){ Name="Richard Pérez", Age =30},
                    new Person(){ Name="Jamile Pérez", Age =26},
                    new Person(){ Name="Mariana Pérez", Age =24},
                    new Person(){ Name="Suami Cepeda", Age =30},
                };

            if ((sender as ToolStripMenuItem).Text == "Person")
            {
                Report.Model = source[0];
            }
            else if ((sender as ToolStripMenuItem).Text == "Persons")
            {
                Report.Model = source;
            }

            Report.Template = File.ReadAllText($"reports\\{(sender as ToolStripMenuItem).Text}.cshtml");
            Report.Refresh();

            ReportViewer = Report;

            Controls.Add(ReportViewer);
            ReportViewer.BringToFront();
        }

        private void RDCLChild_Click(object sender, System.EventArgs e)
        {
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ReportViewer != null)
                saveFileDialogExport.ShowDialog();
        }

        private void saveFileDialogExport_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}