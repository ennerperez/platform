using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Platform.Support.Reflection;
using RazorEngine;
using Platform.Support;
using System.Drawing.Printing;
using RazorEngine.Templating;
using System.IO;
using Platform.Presentation.Reports.Razor;

namespace Platform.Presentation.Reports
{
    namespace Windows.Forms
    {
        public class RazorReportViewer : System.Windows.Forms.WebBrowser, INotifyPropertyChanged, INotifyPropertyChanging
        {

            public RazorReportViewer() : base()
            {
                InitializeComponent();
            }

            private void InitializeComponent()
            {
                this.SuspendLayout();

                PropertyChanged += RazorReportViewer_PropertyChanged;

                this.ResumeLayout(false);
            }

            private int zoon;
            public int Zoom { get { return zoon; } set { this.SetField(ref zoon, value); } }

            private string template;
            public string Template { get { return template; } set { this.SetField(ref template, value); } }

            private PaperKind paperKind = PaperKind.Letter;
            public PaperKind PaperKind { get { return paperKind; } set { this.SetField(ref paperKind, value); } }

            //private Orientation paperLayout;
            //public Orientation PaperLayout { get { return paperLayout; } set { this.SetField(ref paperLayout, value); } }

            private object model;
            public object Model { get { return model; } set { this.SetField(ref model, value); } }

            //private PaperSize paperSize;
            //public PaperSize PaperSize { get { return paperSize; } set { this.SetField(ref paperSize, value); } }

            public event PropertyChangingEventHandler PropertyChanging;
            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                PropertyChanged?.Invoke(this, e);
            }
            protected void OnPropertyChanging(PropertyChangingEventArgs e)
            {
                if (!DesignMode)
                    Refresh();
                PropertyChanging?.Invoke(this, e);
            }

            private void RazorReportViewer_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                //if ((e.PropertyName == "Zoom" || string.IsNullOrEmpty(e.PropertyName)) && )
                Refresh();
            }

            public override void Refresh()
            {
                base.Refresh();

                if (DesignMode)
                    return;

                try
                {
                    var viewBag = new Dictionary<string, object>();
                    //if ((e.PropertyName == "PaperKind" || string.IsNullOrEmpty(e.PropertyName)))
                    viewBag.Add("PaperKind", this.PaperKind);

                    //if ((e.PropertyName == "Template" || string.IsNullOrEmpty(e.PropertyName)))
                    var report = ReportBuilder.Create(DateTime.Now.Ticks.ToString())
                        .WithTemplate(File.ReadAllText(Template))
                        .WithViewBag(viewBag)
                        .WithPrecompilation();

                    this.DocumentText = report.BuildReport(Model);
                }
                catch (Exception ex)
                {
                    ex.DebugThis();
                    this.DocumentText = ReportBuilder.Create("ExceptionLayout").WithPrecompilation().BuildReport(ex);
                }
                finally
                {
                    if (this.Document != null && this.Document.Body != null)
                        this.Document.Body.Style = $"zoom:{Zoom}";
                    this.Update();
                }

            }

        }
    }
}
