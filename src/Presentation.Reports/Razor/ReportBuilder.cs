﻿using Platform.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RazorEngine;
using RazorEngine.Templating;
using System.Reflection;
using System.IO;
using System.Collections;
using RazorEngine.Configuration;

namespace Platform.Presentation.Reports.Razor
{
    public class ReportBuilder : ReportBuilder<dynamic>
    {
    }

    public class ReportBuilder<T> : IRazorReportBuilder<T>
    {
        private readonly IRazorEngineService Engine = null;

        private string name;
        private string mainTemplate;
        private string styleSheet;
        private bool precompile;
        private bool needsCompilation = true;
        private DynamicViewBag viewBag;

        public ReportBuilder() : base()
        {
            try
            {
                var config = new TemplateServiceConfiguration();
#if DEBUG
                config.Debug = true;
#endif
                var service = RazorEngineService.Create(config);
                Engine = service;

                mainTemplate = Properties.Resources._PrintLayout;
                Engine.AddTemplate("PrintLayout", Properties.Resources._PrintLayout);
                Engine.AddTemplate("ExceptionLayout", Properties.Resources._ExceptionLayout);
                Engine.AddTemplate("LoadingLayout", Properties.Resources._LoadingLayout);
                Engine.Compile(Properties.Resources._PrintLayout, "PrintLayout", typeof(object));
                Engine.Compile(Properties.Resources._ExceptionLayout, "ExceptionLayout", typeof(Exception));
                Engine.Compile(Properties.Resources._LoadingLayout, "LoadingLayout", typeof(object));
            }
            catch (Exception ex)
            {
                ex.DebugThis();
            }
        }

        public static IRazorReportBuilder<T> Create(string name)
        {
            return new ReportBuilder<T> { name = name };
        }

        public string BuildReport(T model = default(T))
        {
            return precompile ? CompiledReport(model) : Report(model);
        }

        private string CompiledReport(T model)
        {
            if (needsCompilation)
            {
                var template = PrepareTemplate();
                Engine.AddTemplate(name, template);
                Engine.Compile(template, name, model.GetType());// typeof(T));
                needsCompilation = false;
            }
            return Engine.Run(name, model != null ? model.GetType() : typeof(T), model != null ? model : default(T), viewBag);
        }

        private string Report(T model)
        {
            return Engine.Run(name, typeof(T), model != null ? model : default(T), viewBag);
        }

        private string PrepareTemplate()
        {
            if (string.IsNullOrEmpty(mainTemplate))
                throw new InvalidOperationException("ReportBuilder must have Template configured before use.");
            return mainTemplate.Replace("@@STYLES", PrepareStylesheet());
        }

        private string PrepareStylesheet()
        {
            return string.IsNullOrEmpty(styleSheet) ? string.Empty : $"<style type='text/css'>{Environment.NewLine}{styleSheet}{Environment.NewLine}</style>";
        }

        #region Razor.IReportBuilder

        public IRazorReportBuilder<T> WithPrecompilation()
        {
            precompile = true;
            return this;
        }

        public IRazorReportBuilder<T> WithCss(string css)
        {
            needsCompilation = styleSheet != css;
            styleSheet = css;
            return this;
        }

        public IRazorReportBuilder<T> WithCssFromFileSystem(string cssPath)
        {
            return WithCss(File.ReadAllText(cssPath));
        }

        public IRazorReportBuilder<T> WithCssFromResource(string resourceName, Assembly assembly)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (TextReader reader = new StreamReader(stream))
                return WithCss(reader.ReadToEnd());
        }

        public IRazorReportBuilder<T> WithTemplate(string template)
        {
            needsCompilation = mainTemplate != template;
            mainTemplate = template;
            return this;
        }

        public IRazorReportBuilder<T> WithTemplateFromFileSystem(string templatePath)
        {
            return WithTemplate(File.ReadAllText(templatePath));
        }

        public IRazorReportBuilder<T> WithTemplateFromResource(string resourceName, Assembly assembly)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (TextReader reader = new StreamReader(stream))
                return WithTemplate(reader.ReadToEnd());
        }

        public IRazorReportBuilder<T> WithViewBag(IDictionary<string, object> source)
        {
            viewBag = new DynamicViewBag(source);
            return this;
        }

        #endregion Razor.IReportBuilder
    }
}