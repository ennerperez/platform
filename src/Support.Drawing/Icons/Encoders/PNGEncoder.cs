using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing.Icons.Encoders
{
    internal class PNGEncoder : ImageEncoder
    {
        public override IconImageFormat IconImageFormat
        {
            get
            {
                return IconImageFormat.PNG;
            }
        }

        public override int ImageSize
        {
            get
            {
                MemoryStream memoryStream = new MemoryStream();
                this.Icon.ToBitmap().Save(memoryStream, ImageFormat.Png);
                return (int)memoryStream.Length;
            }
        }

        public override void Read(Stream stream, int resourceSize)
        {
            byte[] array = new byte[resourceSize];
            stream.Read(array, 0, array.Length);
            MemoryStream stream2 = new MemoryStream(array);
            Bitmap bitmap = new Bitmap(stream2);
            IconImage iconImage = new IconImage();
            iconImage.Set(bitmap, null, Color.Transparent);
            bitmap.Dispose();
            base.CopyFrom(iconImage.Encoder);
        }

        public override void Write(Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream();
            this.Icon.ToBitmap().Save(memoryStream, ImageFormat.Png);
            byte[] buffer = memoryStream.GetBuffer();
            stream.Write(buffer, 0, (int)memoryStream.Length);
        }
    }
}