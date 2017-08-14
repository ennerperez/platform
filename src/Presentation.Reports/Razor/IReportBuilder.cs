using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Platform.Presentation.Reports.Razor
{
    public interface IReportBuilder<T>
    {
        IReportBuilder<T> WithTemplate(string template);
        IReportBuilder<T> WithCss(string css);
        IReportBuilder<T> WithTemplateFromFileSystem(string templatePath);
        IReportBuilder<T> WithCssFromFileSystem(string cssPath);
        IReportBuilder<T> WithTemplateFromResource(string resourceName, Assembly assembly);
        IReportBuilder<T> WithCssFromResource(string resourceName, Assembly assembly);
        IReportBuilder<T> WithPrecompilation();

        string BuildReport(T model);
    }
}
