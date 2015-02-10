using System;

namespace Support.Drawing
{
    public static partial class Helpers
    {

        #region Color

        public static System.Drawing.Color FromHex(string hex, int alpha = 255)
        {
            System.Drawing.Color _return;

            try
            {
                _return = System.Drawing.ColorTranslator.FromHtml(hex);
            }
            catch
            {
                throw new Exception("Hexadecimal string is not a valid color format");
            }

            return _return;
        }
        public static System.Drawing.Color FromString(string source)
        {
            System.Drawing.Color _return;

            try
            {
                int r;
                int g;
                int b;
                int a;
                string[] _source = source.Split(',');

                r = int.Parse(_source[0]);
                g = int.Parse(_source[1]);
                b = int.Parse(_source[2]);

                if (_source.Length < 4)
                {
                    _return = System.Drawing.Color.FromArgb(r, g, b);
                }
                else
                {
                    a = int.Parse(_source[3]);
                    _return = System.Drawing.Color.FromArgb(a, r, g, b);
                }
            }
            catch
            {
                throw new Exception("String is not a valid color format");
            }

            return _return;
        }

        public static string ToHex(System.Drawing.Color source)
        {
            return ToHex(source.R, source.G, source.B);
        }
        public static string ToHex(int r, int g, int b)
        {
            return "#" + System.Drawing.ColorTranslator.FromHtml(string.Format("#{0:X2}{1:X2}{2:X2}", r, g, b)).Name.Remove(0, 2);
        }

        public static System.Drawing.Color Hex(string val)
        {
            return FromHex(val);
        }
        public static System.Drawing.Color RGB(int r, int g, int b)
        {
            return System.Drawing.Color.FromArgb(r, g, b);
        }

        public static System.Drawing.Color GetDarkColor(System.Drawing.Color c, byte d)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            if ((c.R > d))
                r = (byte)(c.R - d);
            if ((c.G > d))
                g = (byte)(c.G - d);
            if ((c.B > d))
                b = (byte)(c.B - d);

            System.Drawing.Color c1 = System.Drawing.Color.FromArgb(r, g, b);
            return c1;
        }
        public static System.Drawing.Color GetLightColor(System.Drawing.Color c, byte d)
        {
            byte r = 255;
            byte g = 255;
            byte b = 255;

            if (((int)c.R + (int)d <= 255))
                r = (byte)(c.R + d);
            if (((int)c.G + (int)d <= 255))
                g = (byte)(c.G + d);
            if (((int)c.B + (int)d <= 255))
                b = (byte)(c.B + d);

            System.Drawing.Color c2 = System.Drawing.Color.FromArgb(r, g, b);
            return c2;
        }

        /// <summary>Get the luminosity of a color.</summary>
        /// <param name="color">The color to be analyzed.</param>
        /// <returns>The luminosity of the color.</returns>
        public static int GetLuminosity(System.Drawing.Color color)
        {
            int num = System.Math.Max(System.Math.Max(color.R, color.G), color.B) + System.Math.Min(System.Math.Min(color.R, color.G), color.B);
            return ((num * 240) + 0xff) / 510;
        }

        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public static System.Drawing.Color ChangeColorBrightness(System.Drawing.Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return System.Drawing.Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        public static System.Drawing.Color LightenBy(System.Drawing.Color color, int percent)
        {
            return ChangeColorBrightness(color, (float)(percent / 100.0));
        }
        public static System.Drawing.Color DarkenBy(System.Drawing.Color color, int percent)
        {
            return ChangeColorBrightness(color, (float)(-1 * percent / 100.0));
        }

        #endregion

        #region Image

        public static System.Drawing.Image FromURI(Uri uri)
        {
            System.Drawing.Image _return;

            System.Net.HttpWebRequest _HttpWebRequest = null;
            System.Net.HttpWebResponse _HttpWebResponse = null;

            System.IO.Stream _Stream = null;

            try
            {
                _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(uri);
                _HttpWebRequest.AllowWriteStreamBuffering = true;

                _HttpWebResponse = (System.Net.HttpWebResponse)_HttpWebRequest.GetResponse();
                _Stream = _HttpWebResponse.GetResponseStream();
                _return = System.Drawing.Image.FromStream(_Stream);

            }
            finally
            {
                _Stream.Close();
                _HttpWebResponse.Close();

            }
            return _return;
        }
        public static System.Drawing.Image FromURL(string url)
        {
            return FromURI(new Uri(url));
        }
        public static System.Drawing.Image FromBytes(byte[] data)
        {
            System.Drawing.Image _return;

            try
            {
                if (data == null)
                {
                    throw new ArgumentNullException("Image Binary Data Cannot be Null or Empty", "data");
                }
                _return = System.Drawing.Image.FromStream(new System.IO.MemoryStream(data));
            }
            catch
            {
                _return = null;
            }

            return _return;
        }
        public static System.Drawing.Image FromBase64(string source)
        {
            System.IO.MemoryStream memStream = new System.IO.MemoryStream(Convert.FromBase64String(source));
            System.Drawing.Image result = System.Drawing.Image.FromStream(memStream);
            memStream.Close();
            return result;
        }

        public static byte[] ToBytes(System.Drawing.Image source)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            if (source != null)
            {
                source.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
            else
            {
                return null;
            }
        }
        public static string ToBase64(System.Drawing.Image source, System.Drawing.Imaging.ImageFormat imageFormat = null)
        {
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            if (imageFormat == null)
                imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
            source.Save(memStream, imageFormat);
            string result = Convert.ToBase64String(memStream.ToArray());
            memStream.Close();
            return result;
        }

        public static System.Drawing.Color GetDominantColor(System.Drawing.Image bmp)
        {
            //Used for tally
            int r = 0;
            int g = 0;
            int b = 0;

            int total = 0;

            int x = 0;
            while (x < bmp.Width)
            {
                int y = 0;
                while (y < bmp.Height)
                {
                    System.Drawing.Color clr = new System.Drawing.Bitmap(bmp).GetPixel(x, y);

                    r += clr.R;
                    g += clr.G;
                    b += clr.B;

                    System.Math.Max(System.Threading.Interlocked.Increment(ref total), total - 1);
                    System.Math.Max(System.Threading.Interlocked.Increment(ref y), y - 1);
                }
                System.Math.Max(System.Threading.Interlocked.Increment(ref x), x - 1);
            }

            //Calculate average
            r /= total;
            g /= total;
            b /= total;

            return System.Drawing.Color.FromArgb(r, g, b);
        }
        public static System.Drawing.Color[] GetPalette(System.Drawing.Image image)
        {

            System.Collections.Generic.List<System.Drawing.Color> colors;
            using (var b = new System.Drawing.Bitmap(image))
            {
                var bd = b.LockBits(new System.Drawing.Rectangle(0, 0, b.Width, b.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                byte[] arr = new byte[bd.Width * bd.Height * 3];
                colors = new System.Collections.Generic.List<System.Drawing.Color>();
                System.Runtime.InteropServices.Marshal.Copy(bd.Scan0, arr, 0, arr.Length);
                b.UnlockBits(bd);

                for (int i = 0; i < ((bd.Width * bd.Height)); i++)
                {
                    var start = i * 3;
                    colors.Add(System.Drawing.Color.FromArgb(arr[start], arr[start + 1], arr[start + 2]));
                }
            }

            return colors.ToArray();

        }

        public static bool DrawAdjustedImage(System.Drawing.Image img, System.Drawing.Imaging.ColorMatrix cm)
        {


            try
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img);
                // create a copy of the source image 
                System.Drawing.Imaging.ImageAttributes imgattr = new System.Drawing.Imaging.ImageAttributes();
                System.Drawing.Rectangle rc = new System.Drawing.Rectangle(0, 0, img.Width, img.Height);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);

                // associate the ColorMatrix object with an ImageAttributes object
                imgattr.SetColorMatrix(cm);

                // draw the copy of the source image back over the original image, 
                //applying the ColorMatrix
                g.DrawImage(bmp, rc, 0, 0, img.Width, img.Height, System.Drawing.GraphicsUnit.Pixel, imgattr);

                g.Dispose();

                return true;

            }
            catch
            {
                return false;
            }

        }
        public static void GrayScale(System.Drawing.Image img)
        {

            System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix(new float[][] {
			new float[] {
				0.299F,
				0.299F,
				0.299F,
				0,
				0
			},
			new float[] {
				0.587F,
				0.587F,
				0.587F,
				0,
				0
			},
			new float[] {
				0.114F,
				0.114F,
				0.114F,
				0,
				0
			},
			new float[] {
				0,
				0,
				0,
				1,
				0
			},
			new float[] {
				0,
				0,
				0,
				0,
				1
			}
		});


            DrawAdjustedImage(img, cm);

        }
        public static void Translate(System.Drawing.Image img, float red, float green, float blue, float alpha = 0)
        {

            float sr;
            float sg;
            float sb;
            float sa;

            // noramlize the color components to 1
            sr = red / 255;
            sg = green / 255;
            sb = blue / 255;
            sa = alpha / 255;

            // create the color matrix
            System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix(new float[][] {
			new float[] {
				1,
				0,
				0,
				0,
				0
			},
			new float[] {
				0,
				1,
				0,
				0,
				0
			},
			new float[] {
				0,
				0,
				1,
				0,
				0
			},
			new float[] {
				0,
				0,
				0,
				1,
				0
			},
			new float[] {
				sr,
				sg,
				sb,
				sa,
				1
			}
		});

            // apply the matrix to the image
            DrawAdjustedImage(img, cm);

        }
        public static void Negative(System.Drawing.Image img)
        {
            System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix(new float[][] {
			new float[] {
				-1,
				0,
				0,
				0,
				0
			},
			new float[] {
				0,
				-1,
				0,
				0,
				0
			},
			new float[] {
				0,
				0,
				-1,
				0,
				0
			},
			new float[] {
				0,
				0,
				0,
				1,
				0
			},
			new float[] {
				0,
				0,
				0,
				0,
				1
			}
		});

            DrawAdjustedImage(img, cm);

        }

        public static System.Drawing.Image ResizeImage(System.Drawing.Image image, System.Drawing.Size size, bool preserveAspectRatio = true)
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }

            System.Drawing.Image newImage = new System.Drawing.Bitmap(newWidth, newHeight);

            using (System.Drawing.Graphics graphicsHandle = System.Drawing.Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;

        }

        #endregion

        #region Geometrics

        public static bool IsPointInRectangle(System.Drawing.Point p, System.Drawing.Rectangle r)
        {
            bool flag = false;
            if ((p.X > r.X & p.X < r.X + r.Width & p.Y > r.Y & p.Y < r.Y + r.Height))
            {
                flag = true;
            }
            return flag;
        }

        public static void DrawInsetCircle(ref System.Drawing.Graphics g, ref System.Drawing.Rectangle r, System.Drawing.Pen p)
        {
            int i;
            System.Drawing.Pen p1 = new System.Drawing.Pen(p.Color); //GetDarkColor(p.Color, 50));
            System.Drawing.Pen p2 = new System.Drawing.Pen(p.Color); //GetLightColor(p.Color, 50));

            for (i = 0; i <= p.Width; i++)
            {
                System.Drawing.Rectangle r1 = new System.Drawing.Rectangle(r.X + i, r.Y + i, r.Width - i * 2, r.Height - i * 2);

                g.DrawArc(p2, r1, -45, 180);
                g.DrawArc(p1, r1, 135, 180);
            }
        }

        #endregion

        #region Assembly Info

        public static System.Drawing.Icon Icon()
        {
            return System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetCallingAssembly().Location);
        }

        #endregion

    }
}
