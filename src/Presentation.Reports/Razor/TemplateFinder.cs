using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Platform.Presentation.Reports.Razor
{
    public class TemplateFinder
    {

        public static string GetTemplateFromResource(string resourceName, Assembly assembly)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (TextReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        public static string GetTemplateFromFileSystem(string templatePath)
        {
            return File.ReadAllText(templatePath);
        }
    }
}
