﻿using System;
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
using System.Security.Permissions;
using System.Reflection;
using System.Security.Policy;
using System.Security;

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
                //if (!DesignMode)
                //    Refresh();
                PropertyChanging?.Invoke(this, e);
            }

            private void RazorReportViewer_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                //Refresh();
            }

            public void Refresh(Dictionary<string, object> viewBag = null)
            {
                if (DesignMode)
                    return;

                if (viewBag == null)
                    viewBag = new Dictionary<string, object>
                {
                    { "PaperKind", this.PaperKind }
                };
                else
                    viewBag.Add("PaperKind", this.PaperKind);

                //if (AppDomain.CurrentDomain.IsDefaultAppDomain())
                //{
                //    // RazorEngine cannot clean up from the default appdomain...
                //    Console.WriteLine("Switching to second AppDomain, for RazorEngine...");
                //    AppDomainSetup adSetup = new AppDomainSetup
                //    {
                //        ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase
                //    };
                //    var current = AppDomain.CurrentDomain;
                //    // You only need to add strongnames when your appdomain is not a full trust environment.
                //    var strongNames = new StrongName[0];

                //    var domain = AppDomain.CreateDomain(
                //        AppDomain.CurrentDomain.FriendlyName, null,
                //        current.SetupInformation, new PermissionSet(PermissionState.Unrestricted),
                //        strongNames);
                //    var exitCode = domain.ExecuteAssembly(Assembly.GetEntryAssembly().Location);
                //    // RazorEngine will cleanup. 
                //    AppDomain.Unload(domain);
                //    return;
                //}

                try
                {
                    var report = ReportBuilder<object>.Create(DateTime.Now.Ticks.ToString())
                        .WithTemplate(Template)
                        .WithViewBag(viewBag)
                        .WithPrecompilation();

                    this.DocumentText = report.BuildReport(Model);

                }
                catch (Exception ex)
                {
                    ex.DebugThis();

                    var report = ReportBuilder<Exception>.Create(DateTime.Now.Ticks.ToString())
                       .WithTemplate(Presentation.Reports.Properties.Resources.Exception)
                       .WithViewBag(viewBag)
                       .WithPrecompilation();

                    this.DocumentText = report.BuildReport(ex);
                }
                finally
                {
                    if (this.Document != null && this.Document.Body != null)
                        this.Document.Body.Style = $"zoom:{Zoom}";
                    this.Update();
                }

                base.Refresh();

            }

        }
    }
}