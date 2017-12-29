using System;
using System.IO;

namespace Platform.Presentation.Reports.RDLC
{
    namespace Windows.Forms
    {
        using Microsoft.Reporting.WinForms;

        public class ReportGenerator : System.ComponentModel.Component
        {
            protected readonly RDLC.Report Report = new RDLC.Report();

            protected readonly ReportDataSourceCollection DataSources;

            private readonly LocalReport localReport;

            public ReportGenerator(LocalReport localReport)
            {
                this.localReport = localReport ?? throw new ArgumentNullException("localReport");
                this.DataSources = localReport.DataSources;
            }

            public virtual void Run()
            {
                ////this.Report.Element.Save(Console.Out);  // Uncomment this to show the entire RDLC in the Output window.
                this.LoadReportDefinition();
            }

            private void LoadReportDefinition()
            {
                using (var stream = new MemoryStream())
                {
                    this.Report.Element.Save(stream);
                    stream.Position = 0;
                    this.localReport.LoadReportDefinition(stream);
                }
            }
        }
    }

    #region Obsolete

    //namespace Web.Forms
    //{
    //    using Microsoft.Reports.WebForms;

    //    public class ReportGenerator : System.ComponentModel.Component
    //    {
    //        protected readonly RDLC.Report Report = new RDLC.Report();
    //        protected readonly ReportDataSourceCollection DataSources;

    //        private readonly LocalReport localReport;

    //        public ReportGenerator(LocalReport localReport)
    //        {
    //            this.localReport = localReport ?? throw new ArgumentNullException("localReport");
    //            this.DataSources = localReport.DataSources;
    //        }

    //        public virtual void Run()
    //        {
    //            ////this.Report.Element.Save(Console.Out);  // Uncomment this to show the entire RDLC in the Output window.
    //            this.LoadReportDefinition();
    //        }

    //        private void LoadReportDefinition()
    //        {
    //            using (var stream = new MemoryStream())
    //            {
    //                this.Report.Element.Save(stream);
    //                stream.Position = 0;
    //                this.localReport.LoadReportDefinition(stream);
    //            }
    //        }
    //    }
    //}

    #endregion Obsolete
}