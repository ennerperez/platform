using System.Drawing;
using System.Reflection;

namespace Platform.Support.Drawing
{
    public static partial class ReflectionExtensions
    {
        public static Icon Icon(this Assembly assembly)
        {
            return System.Drawing.Icon.ExtractAssociatedIcon(assembly.Location);
        }
    }
}