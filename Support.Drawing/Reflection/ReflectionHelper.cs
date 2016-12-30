using System.Drawing;
using System.Reflection;

namespace Platform.Support.Drawing
{
    public static partial class ReflectionHelper
    {
        public static Icon Icon(string path)
        {
            return System.Drawing.Icon.ExtractAssociatedIcon(path);
        }

        public static Icon Icon(Assembly assembly)
        {
            return Icon(assembly.Location);
        }

        public static Icon Icon()
        {
            return Icon(Assembly.GetEntryAssembly());
        }

    }
}
