using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing.Colors
{
    public interface IColorQuantizer
    {
        Bitmap Convert(Bitmap source, PixelFormat outputFormat);
    }
}