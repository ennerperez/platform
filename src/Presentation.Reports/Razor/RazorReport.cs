using Platform.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorEngine;
using RazorEngine.Templating;

namespace Platform.Presentation.Reports.Razor
{
    public class RazorReport
    {

        public RazorReport()
        {
            try
            {
                Engine.Razor.AddTemplate("_PrintLayout", Properties.Resources._PrintLayout);

                //Engine.Razor.AddTemplate("_ExceptionLayout", Properties.Resources._ExceptionLayout);
                //Engine.Razor.AddTemplate("_LoadingLayout", Properties.Resources._LoadingLayout);

                Engine.Razor.Compile(Properties.Resources._ExceptionLayout, "_ExceptionLayout", typeof(Exception));
                Engine.Razor.Compile(Properties.Resources._LoadingLayout, "_LoadingLayout", typeof(object));

            }
            catch (Exception ex)
            {
                ex.DebugThis();
            }
        }

        public void Compile(string preparedTemplate, string name)
        {
            Engine.Razor.Compile(preparedTemplate, name, typeof(object));
        }
        public void Compile<T>(string preparedTemplate, string name)
        {
            Engine.Razor.Compile(preparedTemplate, name, typeof(T));
        }

        public string Run(object model, string name, DynamicViewBag viewBag = null)
        {
            return Engine.Razor.Run(name, model != null ? model.GetType() : typeof(object), model, viewBag);
        }
        public string Run<T>(T model, string name, DynamicViewBag viewBag = null)
        {
            return Engine.Razor.Run(name, typeof(T), model, viewBag);
        }

    }
}
