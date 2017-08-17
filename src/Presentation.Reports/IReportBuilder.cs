using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Presentation.Reports
{
    public interface IReportBuilder<T>
    {
        string BuildReport(T model = default(T));
    }
}
