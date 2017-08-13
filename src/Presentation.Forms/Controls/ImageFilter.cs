using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Controls
{
    //TODO: WIP

    [ToolboxBitmap(typeof(System.Windows.Forms.PictureBox))]
    [ToolboxItem(false)]
    public partial class ImageFilter : UserControl
    {
        public IGFilter iFilter;

        public event EventHandler<EventArgs> filterClicked;

        public ImageFilter()
        {
            this.InitializeComponent();
        }

        public ImageFilter(IGFilter filter, Bitmap sImg)
        {
            this.InitializeComponent();
            this.iFilter = filter;
            this.fName.Text = filter.filterName;
            this.fImage.Image = GetFilterImage(sImg, AppDomain.CurrentDomain.BaseDirectory + "filters\\" + filter.filterPath, filter.filterType);
        }

        public static Bitmap GetFilterImage(Bitmap croppedOriginal, string filterPath, int FilterType)
        {
            int width = croppedOriginal.Width;
            int height = croppedOriginal.Height;
            switch (FilterType)
            {
                case 0:
                    return croppedOriginal;

                case 1:
                    {
                        Bitmap bitmap = (Bitmap)Image.FromFile(filterPath);
                        Bitmap bitmap2 = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                        for (int i = 0; i < height; i++)
                        {
                            for (int j = 0; j < width; j++)
                            {
                                Color pixel = croppedOriginal.GetPixel(j, i);
                                int num = (int)(0.12890625f * (float)pixel.B);
                                int num2 = (int)(0.12890625f * (float)pixel.G);
                                int arg_9D_0 = (int)(0.12890625f * (float)pixel.R);
                                int y = num * 33 + num2;
                                int x = arg_9D_0;
                                Color pixel2 = bitmap.GetPixel(x, y);
                                bitmap2.SetPixel(j, i, pixel2);
                            }
                        }
                        return bitmap2;
                    }
                case 2:
                    {
                        Bitmap bitmap3 = (Bitmap)Image.FromFile(filterPath);
                        Bitmap bitmap2 = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                        for (int k = 0; k < height; k++)
                        {
                            for (int l = 0; l < width; l++)
                            {
                                Color pixel3 = croppedOriginal.GetPixel(l, k);
                                int r = (int)bitmap3.GetPixel((int)pixel3.R, 0).R;
                                int g = (int)bitmap3.GetPixel((int)pixel3.G, 1).G;
                                int b = (int)bitmap3.GetPixel((int)pixel3.B, 2).B;
                                bitmap2.SetPixel(l, k, Color.FromArgb(r, g, b));
                            }
                        }
                        return bitmap2;
                    }
                case 3:
                    {
                        Bitmap bitmap4 = (Bitmap)Image.FromFile(filterPath);
                        Bitmap bitmap2 = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                        for (int m = 0; m < height; m++)
                        {
                            for (int n = 0; n < width; n++)
                            {
                                Color pixel4 = croppedOriginal.GetPixel(n, m);
                                int r2 = (int)bitmap4.GetPixel((int)pixel4.R, 0).R;
                                int g2 = (int)bitmap4.GetPixel((int)pixel4.G, 0).G;
                                int b2 = (int)bitmap4.GetPixel((int)pixel4.B, 0).B;
                                bitmap2.SetPixel(n, m, Color.FromArgb(r2, g2, b2));
                            }
                        }
                        return bitmap2;
                    }
                case 4:
                    {
                        Bitmap bitmap5 = (Bitmap)Image.FromFile(filterPath);
                        Bitmap bitmap2 = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                        for (int num3 = 0; num3 < height; num3++)
                        {
                            for (int num4 = 0; num4 < width; num4++)
                            {
                                Color pixel5 = croppedOriginal.GetPixel(num4, num3);
                                int num5 = (int)((double)bitmap5.GetPixel((int)pixel5.R, 0).R * 0.299);
                                int num6 = (int)((double)bitmap5.GetPixel((int)pixel5.G, 0).G * 0.587);
                                int num7 = (int)((double)bitmap5.GetPixel((int)pixel5.B, 0).B * 0.114);
                                bitmap2.SetPixel(num4, num3, Color.FromArgb(num5 + num6 + num7, num5 + num6 + num7, num5 + num6 + num7));
                            }
                        }
                        return bitmap2;
                    }
                case 5:
                    {
                        Bitmap bitmap6 = (Bitmap)Image.FromFile(filterPath);
                        Bitmap bitmap2 = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                        for (int num8 = 0; num8 < height; num8++)
                        {
                            for (int num9 = 0; num9 < width; num9++)
                            {
                                Color pixel6 = croppedOriginal.GetPixel(num9, num8);
                                int num10 = (int)((double)bitmap6.GetPixel((int)pixel6.R, 0).R * 0.299);
                                int num11 = (int)((double)bitmap6.GetPixel((int)pixel6.G, 1).G * 0.587);
                                int num12 = (int)((double)bitmap6.GetPixel((int)pixel6.B, 2).B * 0.114);
                                bitmap2.SetPixel(num9, num8, Color.FromArgb(num10 + num11 + num12, num10 + num11 + num12, num10 + num11 + num12));
                            }
                        }
                        return bitmap2;
                    }
                default:
                    return croppedOriginal;
            }
        }

        private void imgFilter_Click(object sender, EventArgs e)
        {
            EventArgs e2 = new EventArgs();
            this.filterClicked(this, e2);
        }

        private void fImage_Click(object sender, EventArgs e)
        {
            this.imgFilter_Click(sender, e);
        }

        private void fName_Click(object sender, EventArgs e)
        {
            this.imgFilter_Click(sender, e);
        }

        public class IGFilter
        {
            public string filterName;

            public string filterPath;

            public int filterID;

            public int filterType;

            public IGFilter(string fname, string fpath, int fid, int ft)
            {
                this.filterID = fid;
                this.filterName = fname;
                this.filterType = ft;
                this.filterPath = fpath;
            }
        }
    }
}