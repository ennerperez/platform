using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Reporting.Common;

namespace Platform.Presentation.Reports
{
    namespace Windows.Forms
    {
        
        using Microsoft.Reporting.WinForms;
        public class ReportGenerator
        {
            protected readonly RDLC.Report Report = new RDLC.Report();
            protected readonly ReportDataSourceCollection DataSources;

            private readonly LocalReport localReport;

            public ReportGenerator(LocalReport localReport)
            {
                if (localReport == null)
                {
                    throw new ArgumentNullException("localReport");
                }

                this.localReport = localReport;
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

    namespace Web.Forms
    {

        using Microsoft.Reporting.WebForms;
        public class ReportGenerator
        {
            protected readonly RDLC.Report Report = new RDLC.Report();
            protected readonly ReportDataSourceCollection DataSources;

            private readonly LocalReport localReport;

            public ReportGenerator(LocalReport localReport)
            {
                if (localReport == null)
                {
                    throw new ArgumentNullException("localReport");
                }

                this.localReport = localReport;
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

}
