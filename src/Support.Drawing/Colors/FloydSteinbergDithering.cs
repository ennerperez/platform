using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing.Colors
{
    public class FloydSteinbergDithering : IDithering
    {
        public unsafe void Disperse(byte* pixelSource, int x, int y, byte bpp, int stride, int width, int height, Color colorEntry)
        {
            byte b = 0;
            byte b2 = 0;
            byte b3 = 0;
            uint num = 0u;
            this.GetRGB(pixelSource, bpp, x, ref b, ref b2, ref b3, ref num);
            int num2 = (int)(b - colorEntry.R);
            int num3 = (int)(b2 - colorEntry.G);
            int num4 = (int)(b3 - colorEntry.B);
            if (bpp != 16)
            {
                if (bpp != 24)
                {
                    if (bpp != 32)
                    {
                        return;
                    }
                    if (x + 1 < width)
                    {
                        byte* ptr = pixelSource + (x + 1) * 4;
                        *ptr = this.Limits((int)(*ptr), num4 * 7 >> 4);
                        ptr++;
                        *ptr = this.Limits((int)(*ptr), num3 * 7 >> 4);
                        ptr++;
                        *ptr = this.Limits((int)(*ptr), num2 * 7 >> 4);
                    }
                    if (y + 1 < height)
                    {
                        byte* ptr;
                        if (x - 1 > 0)
                        {
                            ptr = pixelSource + (x - 1) * 4 + stride;
                            *ptr = this.Limits((int)(*ptr), num4 * 3 >> 4);
                            ptr++;
                            *ptr = this.Limits((int)(*ptr), num3 * 3 >> 4);
                            ptr++;
                            *ptr = this.Limits((int)(*ptr), num2 * 3 >> 4);
                        }
                        ptr = pixelSource + x * 4 + stride;
                        *ptr = this.Limits((int)(*ptr), num4 * 5 >> 4);
                        ptr++;
                        *ptr = this.Limits((int)(*ptr), num3 * 5 >> 4);
                        ptr++;
                        *ptr = this.Limits((int)(*ptr), num2 * 5 >> 4);
                        if (x + 1 < width)
                        {
                            ptr = pixelSource + (x + 1) * 4 + stride;
                            *ptr = this.Limits((int)(*ptr), num4 >> 4);
                            ptr++;
                            *ptr = this.Limits((int)(*ptr), num3 >> 4);
                            ptr++;
                            *ptr = this.Limits((int)(*ptr), num2 >> 4);
                        }
                    }
                }
                else
                {
                    if (x + 1 < width)
                    {
                        byte* ptr = pixelSource + (x + 1) * 3;
                        *ptr = this.Limits((int)(*ptr), num4 * 7 >> 4);
                        ptr++;
                        *ptr = this.Limits((int)(*ptr), num3 * 7 >> 4);
                        ptr++;
                        *ptr = this.Limits((int)(*ptr), num2 * 7 >> 4);
                    }
                    if (y + 1 < height)
                    {
                        byte* ptr;
                        if (x - 1 > 0)
                        {
                            ptr = pixelSource + (x - 1) * 3 + stride;
                            *ptr = this.Limits((int)(*ptr), num4 * 3 >> 4);
                            ptr++;
                            *ptr = this.Limits((int)(*ptr), num3 * 3 >> 4);
                            ptr++;
                            *ptr = this.Limits((int)(*ptr), num2 * 3 >> 4);
                        }
                        ptr = pixelSource + x * 3 + stride;
                        *ptr = this.Limits((int)(*ptr), num4 * 5 >> 4);
                        ptr++;
                        *ptr = this.Limits((int)(*ptr), num3 * 5 >> 4);
                        ptr++;
                        *ptr = this.Limits((int)(*ptr), num2 * 5 >> 4);
                        if (x + 1 < width)
                        {
                            ptr = pixelSource + (x + 1) * 3 + stride;
                            *ptr = this.Limits((int)(*ptr), num4 >> 4);
                            ptr++;
                            *ptr = this.Limits((int)(*ptr), num3 >> 4);
                            ptr++;
                            *ptr = this.Limits((int)(*ptr), num2 >> 4);
                            return;
                        }
                    }
                }
            }
            else
            {
                if (x + 1 < width)
                {
                    ushort* ptr2 = (ushort*)(pixelSource + (x + 1) * 2);
                    b = (byte)((float)((*ptr2 & 31744) >> 7) * 1.02409637f);
                    b2 = (byte)((float)((*ptr2 & 992) >> 2) * 1.02409637f);
                    b3 = (byte)((float)((*ptr2 & 31) << 3) * 1.02409637f);
                    b = (this.Limits(b, (int)(num2 * 7 >> 4) & 248));
                    b2 = (this.Limits(b2, (int)(num3 * 7 >> 4) & 248));
                    b3 = (this.Limits(b3, (int)(num4 * 7 >> 4) & 248));
                    *ptr2 = (ushort)((int)b << 7 | (int)b2 << 2 | b3 >> 3);
                }
                if (y + 1 < height)
                {
                    ushort* ptr2;
                    if (x - 1 > 0)
                    {
                        ptr2 = (ushort*)(pixelSource + (x - 1) * 2 + stride);
                        b = (byte)((float)((*ptr2 & 31744) >> 7) * 1.02409637f);
                        b2 = (byte)((float)((*ptr2 & 992) >> 2) * 1.02409637f);
                        b3 = (byte)((float)((*ptr2 & 31) << 3) * 1.02409637f);
                        b = (this.Limits(b, (int)(num2 * 3 >> 4) & 248));
                        b2 = (this.Limits(b2, (int)(num3 * 3 >> 4) & 248));
                        b3 = (this.Limits(b3, (int)(num4 * 3 >> 4) & 248));
                        *ptr2 = (ushort)((int)b << 7 | (int)b2 << 2 | b3 >> 3);
                    }
                    ptr2 = (ushort*)(pixelSource + x * 2 + stride);
                    b = (byte)((float)((*ptr2 & 31744) >> 7) * 1.02409637f);
                    b2 = (byte)((float)((*ptr2 & 992) >> 2) * 1.02409637f);
                    b3 = (byte)((float)((*ptr2 & 31) << 3) * 1.02409637f);
                    b = (this.Limits(b, (int)(num2 * 5 >> 4) & 248));
                    b2 = (this.Limits(b2, (int)(num3 * 5 >> 4) & 248));
                    b3 = (this.Limits(b3, (int)(num4 * 5 >> 4) & 248));
                    *ptr2 = (ushort)((int)b << 7 | (int)b2 << 2 | b3 >> 3);
                    if (x + 1 < width)
                    {
                        ptr2 = (ushort*)(pixelSource + (x + 1) * 2 + stride);
                        b = (byte)((float)((*ptr2 & 31744) >> 7) * 1.02409637f);
                        b2 = (byte)((float)((*ptr2 & 992) >> 2) * 1.02409637f);
                        b3 = (byte)((float)((*ptr2 & 31) << 3) * 1.02409637f);
                        b = (this.Limits(b, (int)(num2 >> 4) & 248));
                        b2 = (this.Limits(b2, (int)(num3 >> 4) & 248));
                        b3 = (this.Limits(b3, (int)(num4 >> 4) & 248));
                        *ptr2 = (ushort)((int)b << 7 | (int)b2 << 2 | b3 >> 3);
                        return;
                    }
                }
            }
        }

        private byte Limits(int a, int b)
        {
            if (a + b < 0)
            {
                return 0;
            }
            if (a + b <= 255)
            {
                return (byte)(a + b);
            }
            return byte.MaxValue;
        }

        private unsafe void GetRGB(byte* firstStridePixel, byte bpp, int x, ref byte r, ref byte g, ref byte b, ref uint ARGBColor)
        {
            byte* ptr;
            if (bpp == 16)
            {
                ptr = firstStridePixel + x * 2;
                r = (byte)((*(ushort*)ptr & 31744) >> 7);
                g = (byte)((*(ushort*)ptr & 992) >> 2);
                b = (byte)((*(ushort*)ptr & 31) << 3);
                ARGBColor = (uint)(*(ushort*)ptr);
                return;
            }
            if (bpp == 24)
            {
                ptr = firstStridePixel + x * 3;
                r = ptr[2];
                g = ptr[1];
                b = *ptr;
                ARGBColor = (uint)((int)r << 16 | (int)g << 8 | (int)b);
                return;
            }
            if (bpp != 32)
            {
                return;
            }
            ptr = firstStridePixel + x * 4;
            r = ptr[2];
            g = ptr[1];
            b = *ptr;
            ARGBColor = *(uint*)ptr;
        }

        private const float mOffset16 = 1.02409637f;
    }
}