using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Support.Drawing
{
    public static class Image
    {

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
                source.Save( ms, System.Drawing.Imaging.ImageFormat.Jpeg);
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

    }
}
