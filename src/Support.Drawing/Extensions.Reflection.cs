using System.Drawing;
using System.Reflection;

namespace Platform.Support.Reflection
{
    public static partial class Extensions
    {
        public static Icon Icon(this Assembly assembly)
        {
            return System.Drawing.Icon.ExtractAssociatedIcon(assembly.Location);
        }
    }
}