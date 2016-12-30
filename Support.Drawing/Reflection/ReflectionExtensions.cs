using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

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
