using System.Drawing;
using System.Reflection;

namespace Platform.Support.Drawing
{
    public static partial class Helpers
    {
        public static Icon Icon()
        {
            return System.Drawing.Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
        }
    }
}
