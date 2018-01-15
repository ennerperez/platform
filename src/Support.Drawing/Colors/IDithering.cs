using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing.Colors
{
    public interface IDithering
    {
        unsafe void Disperse(byte* pixelSource, int x, int y, byte bpp, int stride, int width, int height, Color colorEntry);
    }
}