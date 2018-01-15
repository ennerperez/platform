using Platform.Support.Drawing.Colors;
using Platform.Support.Drawing.Icons.EncodingFormats;
using Platform.Support.Drawing.Icons.Exceptions;
using Platform.Support.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using static Platform.Support.Windows.User32;

namespace Platform.Support.Drawing.Icons
{
    public class SingleIcon : IEnumerable<IconImage>, IEnumerable
    {
        internal SingleIcon(string name)
        {
            this.mName = name;
        }

        public int Count
        {
            get
            {
                return this.mIconImages.Count;
            }
        }

        public string Name
        {
            get
            {
                return this.mName;
            }
            set
            {
                this.mName = (value ?? string.Empty);
            }
        }

        public Icon Icon
        {
            get
            {
                if (this.mIconImages.Count == 0)
                {
                    return null;
                }
                MemoryStream memoryStream = new MemoryStream();
                this.Save(memoryStream);
                memoryStream.Position = 0L;
                return new Icon(memoryStream);
            }
        }

        public void Clear()
        {
            this.mIconImages.Clear();
        }

        public IconImage RemoveAt(int index)
        {
            if (index < 0 || index >= this.mIconImages.Count)
            {
                return null;
            }
            IconImage result = this.mIconImages[index];
            this.mIconImages.RemoveAt(index);
            return result;
        }

        public IEnumerator<IconImage> GetEnumerator()
        {
            return new SingleIcon.Enumerator(this);
        }

        public void Load(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            try
            {
                this.Load(fileStream);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public void Load(Stream stream)
        {
            IconFormat iconFormat = new IconFormat();
            if (!iconFormat.IsRecognizedFormat(stream))
            {
                throw new InvalidFileException();
            }
            MultiIcon multiIcon = iconFormat.Load(stream);
            if (multiIcon.Count < 1)
            {
                return;
            }
            this.CopyFrom(multiIcon[0]);
        }

        public void Save(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            try
            {
                this.Save(fileStream);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public void Save(Stream stream)
        {
            new IconFormat().Save(new MultiIcon(this), stream);
        }

        public IconImage Add(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }
            if (bitmap.PixelFormat == PixelFormat.Format32bppArgb || bitmap.PixelFormat == PixelFormat.Format32bppPArgb)
            {
                IconImage iconImage = this.Add(bitmap, null, Color.Transparent);
                if (bitmap.RawFormat.Guid == ImageFormat.Png.Guid)
                {
                    iconImage.IconImageFormat = IconImageFormat.PNG;
                }
                return iconImage;
            }
            return this.Add(bitmap, null, bitmap.GetPixel(0, 0));
        }

        public IconImage Add(Bitmap bitmap, Color transparentColor)
        {
            return this.Add(bitmap, null, transparentColor);
        }

        public IconImage Add(Bitmap bitmap, Bitmap bitmapMask)
        {
            if (bitmapMask == null)
            {
                throw new ArgumentNullException("bitmapMask");
            }
            return this.Add(bitmap, bitmapMask, Color.Empty);
        }

        public IconImage Add(Icon icon)
        {
            if (icon == null)
            {
                throw new ArgumentNullException("icon");
            }
            ICONINFO iconinfo;
            if (!User32.GetIconInfo(icon.Handle, out iconinfo))
            {
                throw new InvalidMultiIconFileException();
            }
            Bitmap bitmap = null;
            Bitmap bitmap2 = null;
            IconImage result;
            try
            {
                bitmap = Image.FromHbitmap(iconinfo.hbmColor);
                bitmap2 = Image.FromHbitmap(iconinfo.hbmMask);
                if (Drawing.Utilities.BitsFromPixelFormat(bitmap.PixelFormat) == 16)
                {
                    bitmap.Dispose();
                    bitmap2.Dispose();
                    result = this.Add(icon.ToBitmap(), Color.Transparent);
                }
                else
                {
                    result = this.Add(bitmap, bitmap2, Color.Empty);
                }
            }
            finally
            {
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                if (bitmap2 != null)
                {
                    bitmap2.Dispose();
                }
            }
            return result;
        }

        private Bitmap CreateSmoothBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawImage(bmp, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
            graphics.Dispose();
            return bitmap;
        }

        public void CreateFrom(string fileName)
        {
            this.CreateFrom(fileName, IconOutputFormat.FromWin95);
        }

        public void CreateFrom(string fileName, IconOutputFormat format)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(fileName);
            if (bitmap == null)
            {
                throw new InvalidFileException();
            }
            try
            {
                this.CreateFrom(bitmap, format);
            }
            finally
            {
                bitmap.Dispose();
            }
        }

        public void CreateFrom(Bitmap bitmap)
        {
            this.CreateFrom(bitmap, IconOutputFormat.FromWin95);
        }

        private Bitmap ResizeBitmap(Bitmap bitmap, int width, int height)
        {
            if (bitmap.Width == width && bitmap.Height == height)
            {
                return bitmap;
            }
            Bitmap bitmap2 = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bitmap2);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(bitmap, 0, 0, width, height);
            graphics.Dispose();
            return bitmap2;
        }

        public void CreateFrom(Bitmap bitmap, IconOutputFormat format)
        {
            IconImage iconImage = null;
            IColorQuantizer colorQuantizer = new EuclideanQuantizer();
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
            {
                throw new InvalidPixelFormatException(PixelFormat.Undefined, PixelFormat.Format32bppArgb);
            }
            this.mIconImages.Clear();
            if (format == IconOutputFormat.None)
            {
                this.Add(bitmap);
                return;
            }
            if ((format & IconOutputFormat.Vista) == IconOutputFormat.Vista)
            {
                this.Add(bitmap);
            }
            try
            {
                if ((format & IconOutputFormat.WinXPUnpopular) == IconOutputFormat.WinXPUnpopular)
                {
                    Bitmap bitmap2 = this.ResizeBitmap(bitmap, 64, 64);
                    iconImage = this.Add(bitmap2);
                    bitmap2.Dispose();
                }
            }
            catch (Exception)
            {
            }
            try
            {
                Bitmap bitmap2 = this.ResizeBitmap(bitmap, 48, 48);
                if ((format & IconOutputFormat.WinXP) == IconOutputFormat.WinXP)
                {
                    iconImage = this.Add(bitmap2);
                }
                if ((format & IconOutputFormat.Win95) == IconOutputFormat.Win95)
                {
                    this.Add(colorQuantizer.Convert(bitmap2, PixelFormat.Format8bppIndexed), iconImage.Mask);
                }
                if ((format & IconOutputFormat.Win31) == IconOutputFormat.Win31)
                {
                    this.Add(colorQuantizer.Convert(bitmap2, PixelFormat.Format4bppIndexed), iconImage.Mask);
                }
                bitmap2.Dispose();
            }
            catch (Exception)
            {
            }
            try
            {
                Bitmap bitmap2 = this.ResizeBitmap(bitmap, 32, 32);
                if ((format & IconOutputFormat.WinXP) == IconOutputFormat.WinXP)
                {
                    iconImage = this.Add(bitmap2);
                }
                if ((format & IconOutputFormat.Win95) == IconOutputFormat.Win95)
                {
                    this.Add(colorQuantizer.Convert(bitmap2, PixelFormat.Format8bppIndexed), iconImage.Mask);
                }
                if ((format & IconOutputFormat.Win31) == IconOutputFormat.Win31)
                {
                    this.Add(colorQuantizer.Convert(bitmap2, PixelFormat.Format4bppIndexed), iconImage.Mask);
                }
                if ((format & IconOutputFormat.Win30) == IconOutputFormat.Win30)
                {
                    this.Add(colorQuantizer.Convert(bitmap2, PixelFormat.Format1bppIndexed), iconImage.Mask);
                }
                bitmap2.Dispose();
            }
            catch (Exception)
            {
            }
            try
            {
                Bitmap bitmap2 = this.ResizeBitmap(bitmap, 24, 24);
                if ((format & IconOutputFormat.WinXPUnpopular) == IconOutputFormat.WinXPUnpopular)
                {
                    iconImage = this.Add(bitmap2);
                }
                if ((format & IconOutputFormat.Win95Unpopular) == IconOutputFormat.Win95Unpopular)
                {
                    this.Add(colorQuantizer.Convert(bitmap2, PixelFormat.Format8bppIndexed), iconImage.Mask);
                }
                if ((format & IconOutputFormat.Win31Unpopular) == IconOutputFormat.Win31Unpopular)
                {
                    this.Add(colorQuantizer.Convert(bitmap2, PixelFormat.Format4bppIndexed), iconImage.Mask);
                }
                if ((format & IconOutputFormat.Win30) == IconOutputFormat.Win30)
                {
                    this.Add(colorQuantizer.Convert(bitmap2, PixelFormat.Format1bppIndexed), iconImage.Mask);
                }
                bitmap2.Dispose();
            }
            catch (Exception)
            {
            }
            try
            {
                Bitmap bitmap2 = this.ResizeBitmap(bitmap, 16, 16);
                if ((format & IconOutputFormat.WinXP) == IconOutputFormat.WinXP)
                {
                    iconImage = this.Add(bitmap2);
                }
                if ((format & IconOutputFormat.Win95) == IconOutputFormat.Win95)
                {
                    this.Add(colorQuantizer.Convert(bitmap2, PixelFormat.Format8bppIndexed), iconImage.Mask);
                }
                if ((format & IconOutputFormat.Win31) == IconOutputFormat.Win31)
                {
                    this.Add(colorQuantizer.Convert(bitmap2, PixelFormat.Format4bppIndexed), iconImage.Mask);
                }
                if ((format & IconOutputFormat.Win30) == IconOutputFormat.Win30)
                {
                    this.Add(colorQuantizer.Convert(bitmap2, PixelFormat.Format1bppIndexed), iconImage.Mask);
                }
                bitmap2.Dispose();
            }
            catch (Exception)
            {
            }
        }

        internal IconImage Add(IconImage iconImage)
        {
            this.mIconImages.Add(iconImage);
            return iconImage;
        }

        internal void CopyFrom(SingleIcon singleIcon)
        {
            this.mName = singleIcon.mName;
            this.mIconImages = singleIcon.mIconImages;
        }

        private IconImage Add(Bitmap bitmap, Bitmap bitmapMask, Color transparentColor)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }
            if (this.IndexOf(bitmap.Size, Drawing.Utilities.BitsFromPixelFormat(bitmap.PixelFormat)) != -1)
            {
                throw new ImageAlreadyExistsException();
            }
            if (bitmap.Width > 256 || bitmap.Height > 256)
            {
                throw new ImageTooBigException();
            }
            IconImage iconImage = new IconImage();
            iconImage.Set(bitmap, bitmapMask, transparentColor);
            this.mIconImages.Add(iconImage);
            return iconImage;
        }

        private int IndexOf(Size size, int bitCount)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Size == size && Drawing.Utilities.BitsFromPixelFormat(this[i].PixelFormat) == bitCount)
                {
                    return i;
                }
            }
            return -1;
        }

        public override string ToString()
        {
            return this.Name;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IconImage this[int index]
        {
            get
            {
                return this.mIconImages[index];
            }
        }

        private string mName = "";

        private List<IconImage> mIconImages = new List<IconImage>();

        [Serializable]
        public struct Enumerator : IEnumerator<IconImage>, IDisposable, IEnumerator
        {
            internal Enumerator(SingleIcon list)
            {
                this.mList = list;
                this.mIndex = 0;
                this.mCurrent = null;
            }

            public IconImage Current
            {
                get
                {
                    return this.mCurrent;
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (this.mIndex < this.mList.Count)
                {
                    this.mCurrent = this.mList[this.mIndex];
                    this.mIndex++;
                    return true;
                }
                this.mIndex = this.mList.Count + 1;
                this.mCurrent = null;
                return false;
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            void IEnumerator.Reset()
            {
                this.mIndex = 0;
                this.mCurrent = null;
            }

            private SingleIcon mList;

            private int mIndex;

            private IconImage mCurrent;
        }
    }
}