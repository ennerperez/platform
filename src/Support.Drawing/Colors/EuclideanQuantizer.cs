using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing.Colors
{
    public class EuclideanQuantizer : IColorQuantizer
    {
        public EuclideanQuantizer() : this(new OctreeQuantizer(), new FloydSteinbergDithering())
        {
        }

        public EuclideanQuantizer(IPaletteQuantizer quantizer, IDithering dithering)
        {
#if NETFX_40 || NETFX_45
            if (quantizer == null)
                throw new Exception("param 'quantizer' cannot be null");
            else
                this.mQuantizer = quantizer;
#else
            this.mQuantizer = quantizer ?? throw new Exception("param 'quantizer' cannot be null");
#endif
            this.mDithering = dithering;
        }

        public unsafe Bitmap Convert(Bitmap source, PixelFormat outputFormat)
        {
            DateTime now = DateTime.Now;
            if ((outputFormat & PixelFormat.Indexed) != PixelFormat.Indexed)
            {
                throw new Exception("Output format must be one of the indexed formats");
            }
            Bitmap bitmap = new Bitmap(source.Width, source.Height, outputFormat);
            this.mColorMap = new Dictionary<uint, byte>();
            ColorPalette colorPalette;
            if (outputFormat != PixelFormat.Format1bppIndexed)
            {
                if (outputFormat != PixelFormat.Format4bppIndexed)
                {
                    if (outputFormat != PixelFormat.Format8bppIndexed)
                    {
                        throw new Exception("Indexed format not supported");
                    }
                    colorPalette = this.mQuantizer.CreatePalette(source, 256, 8);
                }
                else
                {
                    colorPalette = this.mQuantizer.CreatePalette(source, 16, 4);
                }
            }
            else
            {
                Bitmap bitmap2 = new Bitmap(1, 1, PixelFormat.Format1bppIndexed);
                colorPalette = bitmap2.Palette;
                bitmap2.Dispose();
                colorPalette.Entries[0] = Color.FromArgb(255, 0, 0, 0);
                colorPalette.Entries[1] = Color.FromArgb(255, 255, 255, 255);
            }
            DateTime now2 = DateTime.Now;
            BitmapData bitmapData = source.LockBits(new System.Drawing.Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadWrite, source.PixelFormat);
            BitmapData bitmapData2 = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            try
            {
                uint* ptr = (uint*)bitmapData.Scan0.ToPointer();
                byte* ptr2 = (byte*)bitmapData2.Scan0.ToPointer();
                int width = source.Width;
                int height = source.Height;
                byte bpp = (byte)Image.GetPixelFormatSize(source.PixelFormat);
                byte r = 0;
                byte g = 0;
                byte b = 0;
                uint key = 0u;
                bitmap.Palette = colorPalette;
                DateTime now3 = DateTime.Now;
                for (int i = 0; i < height; i++)
                {
                    byte* ptr3 = (byte*)(ptr + i * bitmapData.Stride / 4);
                    byte* ptr4 = ptr2 + i * bitmapData2.Stride;
                    for (int j = 0; j < width; j++)
                    {
                        this.GetRGB(ptr3, bpp, j, ref r, ref g, ref b, ref key);
                        byte b2 = 0;
                        if (!this.mColorMap.TryGetValue(key, out b2))
                        {
                            b2 = (byte)this.FindNearestColor(r, g, b, colorPalette.Entries);
                            this.mColorMap.Add(key, b2);
                        }
                        if (outputFormat != PixelFormat.Format1bppIndexed)
                        {
                            if (outputFormat != PixelFormat.Format4bppIndexed)
                            {
                                if (outputFormat == PixelFormat.Format8bppIndexed)
                                {
                                    *ptr4 = b2;
                                    ptr4++;
                                }
                            }
                            else
                            {
                                byte* ptr5 = ptr4;
                                *ptr5 |= (byte)(b2 << ((j - 1 & 1) << 2));
                                ptr4 += (j & 1);
                            }
                        }
                        else
                        {
                            byte b3 = (byte)(128 >> (j - 1 & 7));
                            if (b2 == 1)
                            {
                                byte* ptr6 = ptr4;
                                *ptr6 |= b3;
                            }
                            else
                            {
                                byte* ptr7 = ptr4;
                                ptr7 = ptr7 + (b3 ^ byte.MaxValue);
                                //TODO: Validation
                                //*ptr7 &= (b3 ^ byte.MaxValue);
                            }
                            ptr4 += ((j % 8 == 0 && j != 0) ? 1 : 0);
                        }
                        if (this.mDithering != null)
                        {
                            this.mDithering.Disperse(ptr3, j, i, bpp, bitmapData.Stride, width, height, colorPalette.Entries[(int)b2]);
                        }
                    }
                }
                DateTime.Now.Subtract(now3);
                now3.Subtract(now2);
                now2.Subtract(now);
            }
            finally
            {
                if (source != null)
                {
                    source.UnlockBits(bitmapData);
                }
                if (bitmap != null)
                {
                    bitmap.UnlockBits(bitmapData2);
                }
            }
            return bitmap;
        }

        private int FindNearestColor(byte R, byte G, byte B, Color[] paletteEntries)
        {
            int num = 195076;
            int result = 0;
            for (int i = 0; i < paletteEntries.Length; i++)
            {
                int num2 = (int)(R - paletteEntries[i].R);
                int num3 = (int)(G - paletteEntries[i].G);
                int num4 = (int)(B - paletteEntries[i].B);
                int num5 = num2 * num2 + num3 * num3 + num4 * num4;
                if (num5 < num)
                {
                    num = num5;
                    result = i;
                }
            }
            return result;
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

        private IPaletteQuantizer mQuantizer;

        private IDithering mDithering;

        private Dictionary<uint, byte> mColorMap;
    }
}