using Platform.Support.Drawing.Colors;
using Platform.Support.Drawing.Icons.Encoders;
using Platform.Support.Drawing.Icons.Exceptions;
using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Drawing.Icons
{
    public sealed unsafe class IconImage
    {
        internal IconImage()
        {
            this.mEncoder = new BMPEncoder();
        }

        internal IconImage(Stream stream, int resourceSize)
        {
            this.Read(stream, resourceSize);
        }

        public int ColorsInPalette
        {
            get
            {
                if (this.mEncoder.Header.biClrUsed != 0u)
                {
                    return (int)this.mEncoder.Header.biClrUsed;
                }
                if (this.mEncoder.Header.biBitCount > 8)
                {
                    return 0;
                }
                return 1 << (int)this.mEncoder.Header.biBitCount;
            }
        }

        public Size Size
        {
            get
            {
                return new Size((int)this.mEncoder.Header.biWidth, (int)(this.mEncoder.Header.biHeight / 2u));
            }
        }

        public PixelFormat PixelFormat
        {
            get
            {
                ushort biBitCount = this.mEncoder.Header.biBitCount;
                if (biBitCount <= 8)
                {
                    if (biBitCount == 1)
                    {
                        return PixelFormat.Format1bppIndexed;
                    }
                    if (biBitCount == 4)
                    {
                        return PixelFormat.Format4bppIndexed;
                    }
                    if (biBitCount == 8)
                    {
                        return PixelFormat.Format8bppIndexed;
                    }
                }
                else
                {
                    if (biBitCount == 16)
                    {
                        return PixelFormat.Format16bppRgb565;
                    }
                    if (biBitCount == 24)
                    {
                        return PixelFormat.Format24bppRgb;
                    }
                    if (biBitCount == 32)
                    {
                        return PixelFormat.Format32bppArgb;
                    }
                }
                return PixelFormat.Undefined;
            }
        }

        public Icon Icon
        {
            get
            {
                return this.mEncoder.Icon;
            }
        }

        public Bitmap Transparent
        {
            get
            {
                return this.Icon.ToBitmap();
            }
        }

        public Bitmap Image
        {
            get
            {
                IntPtr dc = User32.GetDC(IntPtr.Zero);
                BITMAPINFO bitmapinfo;
                bitmapinfo.icHeader = this.mEncoder.Header;
                bitmapinfo.icHeader.biHeight = bitmapinfo.icHeader.biHeight / 2u;
                bitmapinfo.icColors = Drawing.Utilities.StandarizePalette(this.mEncoder.Colors);
                IntPtr intPtr = Gdi32.CreateCompatibleDC(dc);
                IntPtr destination;
                IntPtr intPtr2 = Gdi32.CreateDIBSection(intPtr, ref bitmapinfo, 0u, out destination, IntPtr.Zero, 0u);
                Marshal.Copy(this.mEncoder.XOR, 0, destination, this.mEncoder.XOR.Length);
                Bitmap result = System.Drawing.Image.FromHbitmap(intPtr2);
                User32.ReleaseDC(IntPtr.Zero, dc);
                Gdi32.DeleteObject(intPtr2);
                Gdi32.DeleteDC(intPtr);
                return result;
            }
        }

        public Bitmap Mask
        {
            get
            {
                IntPtr dc = User32.GetDC(IntPtr.Zero);
                BITMAPINFO bitmapinfo;
                bitmapinfo.icHeader = this.mEncoder.Header;
                bitmapinfo.icHeader.biHeight = bitmapinfo.icHeader.biHeight / 2u;
                bitmapinfo.icHeader.biBitCount = 1;
                bitmapinfo.icColors = new RGBQUAD[256];
                bitmapinfo.icColors[0].Set(0, 0, 0);
                bitmapinfo.icColors[1].Set(byte.MaxValue, byte.MaxValue, byte.MaxValue);
                IntPtr intPtr = Gdi32.CreateCompatibleDC(dc);
                IntPtr destination;
                IntPtr intPtr2 = Gdi32.CreateDIBSection(intPtr, ref bitmapinfo, 0u, out destination, IntPtr.Zero, 0u);
                Marshal.Copy(this.mEncoder.AND, 0, destination, this.mEncoder.AND.Length);
                Bitmap result = System.Drawing.Image.FromHbitmap(intPtr2);
                User32.ReleaseDC(IntPtr.Zero, dc);
                Gdi32.DeleteObject(intPtr2);
                Gdi32.DeleteDC(intPtr);
                return result;
            }
        }

        public IconImageFormat IconImageFormat
        {
            get
            {
                return this.mEncoder.IconImageFormat;
            }
            set
            {
                if (value == IconImageFormat.UNKNOWN)
                {
                    throw new InvalidIconFormatSelectionException();
                }
                if (value == this.mEncoder.IconImageFormat)
                {
                    return;
                }
                ImageEncoder imageEncoder = null;
                if (value != IconImageFormat.BMP)
                {
                    if (value == IconImageFormat.PNG)
                    {
                        imageEncoder = new PNGEncoder();
                    }
                }
                else
                {
                    imageEncoder = new BMPEncoder();
                }
                imageEncoder.CopyFrom(this.mEncoder);
                this.mEncoder = imageEncoder;
            }
        }

        internal ImageEncoder Encoder
        {
            get
            {
                return this.mEncoder;
            }
        }

        internal int IconImageSize
        {
            get
            {
                return this.mEncoder.ImageSize;
            }
        }

        internal ICONDIRENTRY ICONDIRENTRY
        {
            get
            {
                ICONDIRENTRY result;
                result.bColorCount = (byte)this.mEncoder.Header.biClrUsed;
                result.bHeight = (byte)this.mEncoder.Header.biHeight;
                result.bReserved = 0;
                result.bWidth = (byte)this.mEncoder.Header.biWidth;
                result.dwBytesInRes = (uint)(sizeof(BITMAPINFOHEADER) + sizeof(RGBQUAD) * this.ColorsInPalette + this.mEncoder.XOR.Length + this.mEncoder.AND.Length);
                result.dwImageOffset = 0u;
                result.wBitCount = this.mEncoder.Header.biBitCount;
                result.wPlanes = this.mEncoder.Header.biPlanes;
                return result;
            }
        }

        internal GRPICONDIRENTRY GRPICONDIRENTRY
        {
            get
            {
                GRPICONDIRENTRY result;
                result.bColorCount = (byte)this.mEncoder.Header.biClrUsed;
                result.bHeight = (byte)this.mEncoder.Header.biHeight;
                result.bReserved = 0;
                result.bWidth = (byte)this.mEncoder.Header.biWidth;
                result.dwBytesInRes = (uint)this.IconImageSize;
                result.nID = 0;
                result.wBitCount = this.mEncoder.Header.biBitCount;
                result.wPlanes = this.mEncoder.Header.biPlanes;
                return result;
            }
        }

        public void Set(Bitmap bitmap, Bitmap bitmapMask, Color transparentColor)
        {
            Bitmap bitmap2 = (Bitmap)bitmap.Clone();
            Bitmap bitmap3 = (bitmapMask != null) ? ((Bitmap)bitmapMask.Clone()) : null;
            try
            {
                if (bitmap2.PixelFormat != PixelFormat.Format1bppIndexed)
                {
                    bitmap2.RotateFlip(RotateFlipType.Rotate180FlipX);
                }
                else
                {
                    Drawing.Bitmaps.Utilities.FlipYBitmap(bitmap2);
                }
                if (bitmap3 != null)
                {
                    Drawing.Bitmaps.Utilities.FlipYBitmap(bitmap3);
                }
                if (bitmap3 != null && (bitmap2.Size != bitmap3.Size || bitmap3.PixelFormat != PixelFormat.Format1bppIndexed))
                {
                    throw new InvalidMultiIconMaskBitmap();
                }
                RGBQUAD[] array = Drawing.Utilities.RGBQUADFromColorArray(bitmap2);
                BITMAPINFOHEADER header = default(BITMAPINFOHEADER);
                header.biSize = (uint)sizeof(BITMAPINFOHEADER);
                header.biWidth = (uint)bitmap2.Width;
                header.biHeight = (uint)(bitmap2.Height * 2);
                header.biPlanes = 1;
                header.biBitCount = (ushort)Drawing.Utilities.BitsFromPixelFormat(bitmap2.PixelFormat);
                header.biCompression = (int)IconImageFormat.BMP;
                header.biXPelsPerMeter = 0;
                header.biYPelsPerMeter = 0;
                header.biClrUsed = (uint)array.Length;
                header.biClrImportant = 0u;
                this.mEncoder.Header = header;
                this.mEncoder.Colors = array;
                BitmapData bitmapData = bitmap2.LockBits(new System.Drawing.Rectangle(0, 0, bitmap2.Width, bitmap2.Height), ImageLockMode.ReadOnly, bitmap2.PixelFormat);
                IntPtr scan = bitmapData.Scan0;
                this.mEncoder.XOR = new byte[System.Math.Abs(bitmapData.Stride) * bitmapData.Height];
                Marshal.Copy(scan, this.mEncoder.XOR, 0, this.mEncoder.XOR.Length);
                bitmap2.UnlockBits(bitmapData);
                header.biSizeImage = (uint)this.mEncoder.XOR.Length;
                if (bitmap3 == null)
                {
                    Bitmap bitmap4 = new Bitmap(bitmap2.Width, bitmap2.Height, PixelFormat.Format1bppIndexed);
                    BitmapData bitmapData2 = bitmap4.LockBits(new System.Drawing.Rectangle(0, 0, bitmap2.Width, bitmap2.Height), ImageLockMode.ReadWrite, bitmap4.PixelFormat);
                    IntPtr scan2 = bitmapData2.Scan0;
                    this.mEncoder.AND = new byte[System.Math.Abs(bitmapData2.Stride) * bitmapData2.Height];
                    int num = System.Math.Abs(bitmapData.Stride);
                    int num2 = System.Math.Abs(bitmapData2.Stride);
                    int num3 = Drawing.Utilities.BitsFromPixelFormat(bitmap2.PixelFormat);
                    if (num3 == 24)
                    {
                        transparentColor = Color.FromArgb(0, (int)transparentColor.R, (int)transparentColor.G, (int)transparentColor.B);
                    }
                    for (int i = 0; i < bitmapData.Height; i++)
                    {
                        int num4 = num2 * i;
                        int num5 = num * i;
                        for (int j = 0; j < bitmapData.Width; j++)
                        {
                            int num6 = num3;
                            if (num6 <= 8)
                            {
                                if (num6 != 1)
                                {
                                    if (num6 != 4)
                                    {
                                        if (num6 == 8)
                                        {
                                            int num7 = (int)this.mEncoder.XOR[j + num5];
                                            RGBQUAD rgbQuad = this.mEncoder.Colors[num7];
                                            if (Drawing.Utilities.CompareRGBQUADToColor(rgbQuad, transparentColor))
                                            {
                                                byte[] and = this.mEncoder.AND;
                                                int num8 = (j >> 3) + num4;
                                                and[num8] |= (byte)(128 >> (j & 7));
                                                this.mEncoder.XOR[j + num5] = 0;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        int num7 = (int)this.mEncoder.XOR[(j >> 1) + num5];
                                        RGBQUAD rgbQuad = this.mEncoder.Colors[((j & 1) == 0) ? (num7 >> 4) : (num7 & 15)];
                                        if (Drawing.Utilities.CompareRGBQUADToColor(rgbQuad, transparentColor))
                                        {
                                            byte[] and2 = this.mEncoder.AND;
                                            int num9 = (j >> 3) + num4;
                                            and2[num9] |= (byte)(128 >> (j & 7));
                                            byte[] xor = this.mEncoder.XOR;
                                            int num10 = (j >> 1) + num5;
                                            //xor[num10] &= (((j & 1) == 0) ? 15 : 240);
                                            //TODO: Validation
                                            xor[num10] &= (byte)(((j & 1) == 0) ? 15 : 240);
                                        }
                                    }
                                }
                                else
                                {
                                    this.mEncoder.AND[(j >> 3) + num5] = this.mEncoder.XOR[(j >> 3) + num5];
                                }
                            }
                            else
                            {
                                if (num6 == 16)
                                {
                                    throw new NotSupportedException("16 bpp images are not supported for Icons");
                                }
                                if (num6 != 24)
                                {
                                    if (num6 == 32)
                                    {
                                        if (transparentColor == Color.Transparent)
                                        {
                                            if (this.mEncoder.XOR[(j << 2) + num5 + 3] == 0)
                                            {
                                                byte[] and3 = this.mEncoder.AND;
                                                int num11 = (j >> 3) + num4;
                                                and3[num11] |= (byte)(128 >> (j & 7));
                                            }
                                        }
                                        else if (this.mEncoder.XOR[(j << 2) + num5] == transparentColor.B && this.mEncoder.XOR[(j << 2) + num5 + 1] == transparentColor.G && this.mEncoder.XOR[(j << 2) + num5 + 2] == transparentColor.R)
                                        {
                                            byte[] and4 = this.mEncoder.AND;
                                            int num12 = (j >> 3) + num4;
                                            and4[num12] |= (byte)(128 >> (j & 7));
                                            this.mEncoder.XOR[(j << 2) + num5] = 0;
                                            this.mEncoder.XOR[(j << 2) + num5 + 1] = 0;
                                            this.mEncoder.XOR[(j << 2) + num5 + 2] = 0;
                                        }
                                        else
                                        {
                                            this.mEncoder.XOR[(j << 2) + num5 + 3] = byte.MaxValue;
                                        }
                                    }
                                }
                                else
                                {
                                    int num13 = j * 3;
                                    Color left = Color.FromArgb(0, (int)this.mEncoder.XOR[num13 + num5], (int)this.mEncoder.XOR[num13 + num5 + 1], (int)this.mEncoder.XOR[num13 + num5 + 2]);
                                    if (left == transparentColor)
                                    {
                                        byte[] and5 = this.mEncoder.AND;
                                        int num14 = (j >> 3) + num4;
                                        and5[num14] |= (byte)(128 >> (j & 7));
                                    }
                                }
                            }
                        }
                    }
                    bitmap4.UnlockBits(bitmapData2);
                }
                else
                {
                    BitmapData bitmapData3 = bitmap3.LockBits(new System.Drawing.Rectangle(0, 0, bitmap3.Width, bitmap3.Height), ImageLockMode.ReadOnly, bitmap3.PixelFormat);
                    IntPtr scan3 = bitmapData3.Scan0;
                    this.mEncoder.AND = new byte[System.Math.Abs(bitmapData3.Stride) * bitmapData3.Height];
                    Marshal.Copy(scan3, this.mEncoder.AND, 0, this.mEncoder.AND.Length);
                    bitmap3.UnlockBits(bitmapData3);
                }
            }
            finally
            {
                if (bitmap2 != null)
                {
                    bitmap2.Dispose();
                }
                if (bitmap3 != null)
                {
                    bitmap3.Dispose();
                }
            }
        }

        internal void Read(Stream stream, int resourceSize)
        {
            IconImageFormat iconImageFormat = this.GetIconImageFormat(stream);
            if (iconImageFormat == IconImageFormat.BMP)
            {
                this.mEncoder = new BMPEncoder();
                this.mEncoder.Read(stream, resourceSize);
                return;
            }
            if (iconImageFormat != IconImageFormat.PNG)
            {
                return;
            }
            this.mEncoder = new PNGEncoder();
            this.mEncoder.Read(stream, resourceSize);
        }

        internal void Write(Stream stream)
        {
            this.mEncoder.Write(stream);
        }

        private IconImageFormat GetIconImageFormat(Stream stream)
        {
            long position = stream.Position;
            IconImageFormat result;
            try
            {
                BinaryReader binaryReader = new BinaryReader(stream);
                byte b = binaryReader.ReadByte();
                byte b2 = b;
                if (b2 != 40)
                {
                    if (b2 == 137)
                    {
                        if (binaryReader.ReadInt16() == 20048)
                        {
                            return IconImageFormat.PNG;
                        }
                    }
                    result = IconImageFormat.UNKNOWN;
                }
                else
                {
                    result = IconImageFormat.BMP;
                }
            }
            finally
            {
                stream.Position = position;
            }
            return result;
        }

        private ImageEncoder mEncoder;
    }
}