using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace Platform.Support.Drawing
{
    public static partial class Helpers
    {

        public static Icon Icon()
        {
            return System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetCallingAssembly().Location);
        }

    }
}
