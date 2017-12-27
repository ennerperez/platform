using System.Collections.Generic;
using System.Reflection;

namespace Platform.Presentation.Reports.Razor
{
    public interface IRazorReportBuilder<T> : IReportBuilder<T>
    {
        IRazorReportBuilder<T> WithTemplate(string template);

        IRazorReportBuilder<T> WithCss(string css);

        IRazorReportBuilder<T> WithTemplateFromFileSystem(string templatePath);

        IRazorReportBuilder<T> WithCssFromFileSystem(string cssPath);

        IRazorReportBuilder<T> WithTemplateFromResource(string resourceName, Assembly assembly);

        IRazorReportBuilder<T> WithCssFromResource(string resourceName, Assembly assembly);

        IRazorReportBuilder<T> WithPrecompilation();

        IRazorReportBuilder<T> WithViewBag(IDictionary<string, object> source);

        //string BuildReport(T model, DynamicViewBag viewBag);
    }
}