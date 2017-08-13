using System.Drawing;
using System.Reflection;

namespace Platform.Support.Drawing
{
    public static partial class ReflectionExtensions
    {
        public static Icon Icon(this Assembly assembly)
        {
            return ReflectionHelper.Icon(assembly.Location);
        }
    }
}