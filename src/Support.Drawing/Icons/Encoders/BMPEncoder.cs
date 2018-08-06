using Platform.Support.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Platform.Support.Drawing.Icons.Encoders
{
    internal unsafe class BMPEncoder : ImageEncoder
    {
        public override IconImageFormat IconImageFormat
        {
            get
            {
                return IconImageFormat.BMP;
            }
        }

        public override void Read(Stream stream, int resourceSize)
        {
            this.mHeader.Read(stream);
            this.mColors = new RGBQUAD[this.ColorsInPalette];
            byte[] array = new byte[this.mColors.Length * sizeof(RGBQUAD)];
            stream.Read(array, 0, array.Length);
            GCHandle gchandle = GCHandle.Alloc(this.mColors, GCHandleType.Pinned);
            Marshal.Copy(array, 0, gchandle.AddrOfPinnedObject(), array.Length);
            gchandle.Free();
            int num = (int)((ulong)(this.mHeader.biWidth * (uint)this.mHeader.biBitCount + 31u) & 18446744073709551584UL) >> 3;
            this.mXOR = new byte[(long)num * (long)((ulong)(this.mHeader.biHeight / 2u))];
            stream.Read(this.mXOR, 0, this.mXOR.Length);
            num = (int)((ulong)(this.mHeader.biWidth + 31u) & 18446744073709551584UL) >> 3;
            this.mAND = new byte[(long)num * (long)((ulong)(this.mHeader.biHeight / 2u))];
            stream.Read(this.mAND, 0, this.mAND.Length);
        }

        public override void Write(Stream stream)
        {
            new BinaryReader(stream);
            this.mHeader.Write(stream);
            byte[] array = new byte[this.ColorsInPalette * sizeof(RGBQUAD)];
            GCHandle gchandle = GCHandle.Alloc(this.mColors, GCHandleType.Pinned);
            Marshal.Copy(gchandle.AddrOfPinnedObject(), array, 0, array.Length);
            gchandle.Free();
            stream.Write(array, 0, array.Length);
            stream.Write(this.mXOR, 0, this.mXOR.Length);
            stream.Write(this.mAND, 0, this.mAND.Length);
        }
    }
}