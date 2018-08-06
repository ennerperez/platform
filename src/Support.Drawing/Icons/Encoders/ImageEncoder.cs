using Platform.Support.Drawing.Colors;
using Platform.Support.Drawing.Icons;
using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Drawing.Icons.Encoders
{
    internal unsafe abstract class ImageEncoder
    {
        public virtual Icon Icon
        {
            get
            {
                MemoryStream memoryStream = new MemoryStream();
                ICONDIR initalizated = ICONDIR.Initalizated;
                initalizated.idCount = 1;
                initalizated.Write(memoryStream);
                ICONDIRENTRY icondirentry = default(ICONDIRENTRY);
                icondirentry.bColorCount = (byte)this.mHeader.biClrUsed;
                icondirentry.bHeight = (byte)(this.mHeader.biHeight / 2u);
                icondirentry.bReserved = 0;
                icondirentry.bWidth = (byte)this.mHeader.biWidth;
                icondirentry.dwBytesInRes = (uint)(sizeof(BITMAPINFOHEADER) + sizeof(RGBQUAD) * this.ColorsInPalette + this.mXOR.Length + this.mAND.Length);
                icondirentry.dwImageOffset = (uint)(sizeof(ICONDIR) + sizeof(ICONDIRENTRY));
                icondirentry.wBitCount = this.mHeader.biBitCount;
                icondirentry.wPlanes = this.mHeader.biPlanes;
                icondirentry.Write(memoryStream);
                memoryStream.Seek((long)((ulong)icondirentry.dwImageOffset), SeekOrigin.Begin);
                this.mHeader.Write(memoryStream);
                byte[] array = new byte[sizeof(RGBQUAD) * this.ColorsInPalette];
                GCHandle gchandle = GCHandle.Alloc(this.mColors, GCHandleType.Pinned);
                Marshal.Copy(gchandle.AddrOfPinnedObject(), array, 0, array.Length);
                gchandle.Free();
                memoryStream.Write(array, 0, array.Length);
                memoryStream.Write(this.mXOR, 0, this.mXOR.Length);
                memoryStream.Write(this.mAND, 0, this.mAND.Length);
                memoryStream.Position = 0L;
                Icon result = new Icon(memoryStream, (int)icondirentry.bWidth, (int)icondirentry.bHeight);
                memoryStream.Dispose();
                return result;
            }
        }

        public virtual BITMAPINFOHEADER Header
        {
            get
            {
                return this.mHeader;
            }
            set
            {
                this.mHeader = value;
            }
        }

        public virtual RGBQUAD[] Colors
        {
            get
            {
                return this.mColors;
            }
            set
            {
                this.mColors = value;
            }
        }

        public virtual byte[] XOR
        {
            get
            {
                return this.mXOR;
            }
            set
            {
                this.mHeader.biSizeImage = (uint)value.Length;
                this.mXOR = value;
            }
        }

        public virtual byte[] AND
        {
            get
            {
                return this.mAND;
            }
            set
            {
                this.mAND = value;
            }
        }

        public virtual int ColorsInPalette
        {
            get
            {
                if (this.mHeader.biClrUsed != 0u)
                {
                    return (int)this.mHeader.biClrUsed;
                }
                if (this.mHeader.biBitCount > 8)
                {
                    return 0;
                }
                return 1 << (int)this.mHeader.biBitCount;
            }
        }

        public virtual int ImageSize
        {
            get
            {
                return sizeof(BITMAPINFOHEADER) + sizeof(RGBQUAD) * this.ColorsInPalette + this.mXOR.Length + this.mAND.Length;
            }
        }

        public abstract IconImageFormat IconImageFormat { get; }

        public abstract void Read(Stream stream, int resourceSize);

        public abstract void Write(Stream stream);

        public void CopyFrom(ImageEncoder encoder)
        {
            this.mHeader = encoder.mHeader;
            this.mColors = encoder.mColors;
            this.mXOR = encoder.mXOR;
            this.mAND = encoder.mAND;
        }

        protected BITMAPINFOHEADER mHeader;

        protected RGBQUAD[] mColors;

        protected byte[] mXOR;

        protected byte[] mAND;
    }
}