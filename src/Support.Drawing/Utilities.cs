using Platform.Support.Drawing.Colors;
using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing
{
    public static partial class Utilities
    {
        internal static bool CompareRGBQUADToColor(RGBQUAD rgbQuad, Color color)
        {
            return rgbQuad.rgbRed == color.R && rgbQuad.rgbGreen == color.G && rgbQuad.rgbBlue == color.B;
        }

        internal static RGBQUAD[] StandarizePalette(RGBQUAD[] palette)
        {
            RGBQUAD[] array = new RGBQUAD[256];
            for (int i = 0; i < palette.Length; i++)
            {
                array[i] = palette[i];
            }
            return array;
        }

        internal static RGBQUAD[] RGBQUADFromColorArray(Bitmap bmp)
        {
            int num = BitsFromPixelFormat(bmp.PixelFormat);
            RGBQUAD[] array = new RGBQUAD[(num <= 8) ? (1 << num) : 0];
            Color[] entries = bmp.Palette.Entries;
            for (int i = 0; i < entries.Length; i++)
            {
                array[i].rgbRed = entries[i].R;
                array[i].rgbGreen = entries[i].G;
                array[i].rgbBlue = entries[i].B;
            }
            return array;
        }

        public static int BitsFromPixelFormat(PixelFormat pixelFormat)
        {
            if (pixelFormat <= PixelFormat.Format8bppIndexed)
            {
                if (pixelFormat <= PixelFormat.Format32bppRgb)
                {
                    switch (pixelFormat)
                    {
                        case PixelFormat.Format16bppRgb555:
                        case PixelFormat.Format16bppRgb565:
                            break;

                        default:
                            if (pixelFormat == PixelFormat.Format24bppRgb)
                            {
                                return 24;
                            }
                            if (pixelFormat != PixelFormat.Format32bppRgb)
                            {
                                return 0;
                            }
                            return 32;
                    }
                }
                else
                {
                    if (pixelFormat == PixelFormat.Format1bppIndexed)
                    {
                        return 1;
                    }
                    if (pixelFormat == PixelFormat.Format4bppIndexed)
                    {
                        return 4;
                    }
                    if (pixelFormat != PixelFormat.Format8bppIndexed)
                    {
                        return 0;
                    }
                    return 8;
                }
            }
            else
            {
                if (pixelFormat > PixelFormat.Format16bppGrayScale)
                {
                    if (pixelFormat != PixelFormat.Format64bppPArgb)
                    {
                        if (pixelFormat == PixelFormat.Format32bppArgb)
                        {
                            return 32;
                        }
                        if (pixelFormat != PixelFormat.Format64bppArgb)
                        {
                            return 0;
                        }
                    }
                    return 64;
                }
                if (pixelFormat != PixelFormat.Format16bppArgb1555)
                {
                    if (pixelFormat == PixelFormat.Format32bppPArgb)
                    {
                        return 32;
                    }
                    if (pixelFormat != PixelFormat.Format16bppGrayScale)
                    {
                        return 0;
                    }
                }
            }
            return 16;
        }
    }
}