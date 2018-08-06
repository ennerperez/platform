using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Platform.Presentation.Forms
{
    public static partial class Extensions
    {
        [Conditional("DEBUG")]
        internal static void ExtractResources(Image image, string name)
        {
            if (image != null)
            {
                var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                var dirPath = $@"..\..\{assemblyName}\Resources\";
                if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
                image.Save($@"..\..\{assemblyName}\Resources\{name}.png");
            }
        }

        [Conditional("DEBUG")]
        public static void ExtractResources(this ToolStrip source)
        {
            foreach (var item in source.Items.OfType<ToolStripButton>().Where(i => i.Image != null))
                ExtractResources(item.Image, item.Name);
            foreach (var item in source.Items.OfType<ToolStripDropDownButton>().Where(i => i.Image != null))
                ExtractResources(item.Image, item.Name);
        }
    }
}