using Platform.Presentation.Reports.Windows.Forms;
using Platform.Samples.Core.Models;
using Platform.Support.Collections;
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

        public List<Person> Models { get; internal set; }

        private void FormMain_Shown(object sender, System.EventArgs e)
        {
            foreach (var item in System.IO.Directory.GetFiles("reports", "*.cshtml"))
            {
                var child = new ToolStripMenuItem(System.IO.Path.GetFileNameWithoutExtension(item));
                child.Click += RazorChild_Click;
                razorReportsToolStripMenuItem.DropDownItems.Add(child);
            }
            foreach (var item in System.IO.Directory.GetFiles("reports", "*.rdcl"))
            {
                var child = new ToolStripMenuItem(System.IO.Path.GetFileNameWithoutExtension(item));
                child.Click += RDCLChild_Click;
                rdclReportsToolStripMenuItem.DropDownItems.Add(child);
            }

            Models = new List<Person>()
                {
                    new Person(){ Name="Enner Pérez", Age =31},
                    new Person(){ Name="Richard Pérez", Age =30},
                    new Person(){ Name="Jamile Pérez", Age =26},
                    new Person(){ Name="Mariana Pérez", Age =24},
                    new Person(){ Name="Suami Cepeda", Age =30},
                };

            dataGridViewDataSource.DataSource = Models;
        }

        private void RazorChild_Click(object sender, System.EventArgs e)
        {
            if (splitContainerMain.Panel2.Controls.Contains(ReportViewer))
                splitContainerMain.Panel2.Controls.Remove(ReportViewer);

            ReportViewer = null;
            panelZoom.Visible = true;
            trackBarZoom.Value = 100;

            var viewer = new RazorReportViewer()
            {
                Dock = DockStyle.Fill,
            };

            if ((sender as ToolStripMenuItem).Text == "Person" && dataGridViewDataSource.SelectedRows.Count > 0)
                viewer.Model = Models[dataGridViewDataSource.SelectedRows[0].Index];
            else if ((sender as ToolStripMenuItem).Text == "Persons")
                viewer.Model = Models;

            viewer.Template = File.ReadAllText($"reports\\{(sender as ToolStripMenuItem).Text}.cshtml");
            ReportViewer = viewer;

            splitContainerMain.Panel2.Controls.Add(ReportViewer);
            ReportViewer.BringToFront();
            (viewer as RazorReportViewer).Show();
        }

        private void RDCLChild_Click(object sender, System.EventArgs e)
        {
            if (splitContainerMain.Panel2.Controls.Contains(ReportViewer))
                splitContainerMain.Panel2.Controls.Remove(ReportViewer);

            ReportViewer = null;
            panelZoom.Visible = false;
            trackBarZoom.Value = 100;

            var viewer = new Microsoft.Reporting.WinForms.ReportViewer()
            {
                Dock = DockStyle.Fill,
            };

            var report = new Platform.Presentation.Reports.RDLC.ReportBuilder<Person>
            {
                AutoGenerateReport = true
            };

            var ds = new System.Data.DataSet("Person");
            ds.Tables.Add(Models.ToDataTable());
            report.DataSource = ds;

            var xml = report.BuildReport();
            var rpt = report.BuildReportAsStream();

            viewer.LocalReport.LoadReportDefinition(rpt);
            viewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("Person", Models));
            viewer.RefreshReport();

            ReportViewer = viewer;
            splitContainerMain.Panel2.Controls.Add(ReportViewer);
            ReportViewer.BringToFront();
            ReportViewer.Refresh();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ReportViewer != null)
                saveFileDialogExport.ShowDialog();
        }

        private void saveFileDialogExport_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void trackBarZoom_ValueChanged(object sender, EventArgs e)
        {
            if (ReportViewer == null)
                return;

            if (ReportViewer.GetType() == typeof(RazorReportViewer))
                (ReportViewer as RazorReportViewer).Zoom = trackBarZoom.Value;
        }
    }
}