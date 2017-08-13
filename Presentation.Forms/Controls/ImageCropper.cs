using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Controls
{
    [Obsolete("Considerate ImageCropControl instead")]
    [ToolboxBitmap(typeof(System.Windows.Forms.PictureBox))]
    [ToolboxItem(false)]
    public partial class ImageCropper : UserControl
    {
        public ImageCropper()
        {
            this.InitializeComponent();
        }

        public ImageCropper(Bitmap img, Color bgColor)
        {
            this.InitializeComponent();
            this.bg = bgColor;
            this.source = img;
            int width;
            int height;
            if (img.Width >= img.Height)
            {
                width = 580;
                height = (int)((float)img.Height / (float)img.Width * 580f);
            }
            else
            {
                width = (int)((float)img.Width / (float)img.Height * 580f);
                height = 580;
            }
            this.cImage = new Bitmap(width, height);
            Graphics expr_B2 = Graphics.FromImage(this.cImage);
            expr_B2.InterpolationMode = InterpolationMode.HighQualityBicubic;
            expr_B2.DrawImage(img, new Rectangle(0, 0, width, height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
        }

        private Bitmap source;
        private Bitmap cImage;

        private Color bg;

        private Rectangle CropRect = new Rectangle(145, 145, 290, 290);
        private Rectangle rcLT;
        private Rectangle rcRT;
        private Rectangle rcLB;
        private Rectangle rcRB;
        private Rectangle _maxRect = new Rectangle(0, 0, 580, 580);

        private Point start;
        private Point points;

        private bool IsDraging;
        private int loc;

        private void cropBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            if (this.cImage != null)
            {
                float arg_41_0 = (float)this.cImage.Width / 580f;
                float num = (float)this.cImage.Height / 580f;
                Point point = new Point(0, 0);
                int num2;
                int num3;
                if (arg_41_0 >= num)
                {
                    num2 = this.cropBox.Width;
                    num3 = (int)((float)this.cImage.Height / (float)this.cImage.Width * 580f);
                    point.X = 0;
                    point.Y = (num2 - num3) / 2;
                }
                else
                {
                    num3 = this.cropBox.Height;
                    num2 = (int)((float)this.cImage.Width / (float)this.cImage.Height * 580f);
                    point.Y = 0;
                    point.X = (num3 - num2) / 2;
                }
                graphics.FillRectangle(new SolidBrush(this.bg), 0, 0, this.cropBox.Width, this.cropBox.Height);
                graphics.DrawImage(this.cImage, point.X, point.Y, num2, num3);
            }
            Rectangle cropBounds = this.getCropBounds();
            Color arg_11D_0 = Color.Blue;
            graphics.SetClip(this.CropRect, CombineMode.Exclude);
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, 0, 0, 0)), 0, 0, this.cropBox.Width, this.cropBox.Height);
            this.rcLT = new Rectangle(cropBounds.X, cropBounds.Y, 7, 7);
            this.rcRT = new Rectangle(cropBounds.X + cropBounds.Width - 7, cropBounds.Y, 7, 7);
            this.rcLB = new Rectangle(cropBounds.X, cropBounds.Y + cropBounds.Height - 7, 7, 7);
            this.rcRB = new Rectangle(cropBounds.X + cropBounds.Width - 7, cropBounds.Y + cropBounds.Height - 7, 7, 7);
            graphics.SetClip(new Rectangle(0, 0, 580, 580), CombineMode.Union);
            graphics.FillRectangle(new SolidBrush(Color.Blue), this.rcLT);
            graphics.FillRectangle(new SolidBrush(Color.Blue), this.rcRT);
            graphics.FillRectangle(new SolidBrush(Color.Blue), this.rcLB);
            graphics.FillRectangle(new SolidBrush(Color.Blue), this.rcRB);
            base.OnPaint(e);
        }

        private void cropBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            if (!this.IsDraging)
            {
                if (this.rcLT.Contains(pt))
                {
                    this.Cursor = Cursors.SizeNWSE;
                    this.loc = 1;
                }
                else if (this.rcRT.Contains(pt))
                {
                    this.Cursor = Cursors.SizeNESW;
                    this.loc = 2;
                }
                else if (this.rcLB.Contains(pt))
                {
                    this.Cursor = Cursors.SizeNESW;
                    this.loc = 4;
                }
                else if (this.rcRB.Contains(pt))
                {
                    this.Cursor = Cursors.SizeNWSE;
                    this.loc = 3;
                }
                else if (this.CropRect.Contains(pt))
                {
                    this.Cursor = Cursors.SizeAll;
                    this.loc = 5;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    this.loc = 0;
                }
            }
            if (e.Button == MouseButtons.Left && this.loc != 0 && this.IsDraging)
            {
                if (this.loc == 1)
                {
                    if (pt.X < this._maxRect.X)
                    {
                        pt.X = this._maxRect.X;
                    }
                    if (pt.Y < this._maxRect.Y)
                    {
                        pt.Y = this._maxRect.Y;
                    }
                    int num = this.start.X - pt.X + this.CropRect.Width;
                    int num2 = this.start.Y - pt.Y + this.CropRect.Height;
                    if (num >= 80 && num2 >= 80)
                    {
                        if (num > num2)
                        {
                            if ((float)num / (float)num2 <= 1.77777779f)
                            {
                                this.CropRect.X = pt.X;
                                this.CropRect.Width = num;
                                this.CropRect.Y = pt.Y;
                                this.CropRect.Height = num2;
                                this.start.X = pt.X;
                                this.start.Y = pt.Y;
                            }
                            else
                            {
                                this.CropRect.Y = pt.Y;
                                this.CropRect.Height = num2;
                                num = (int)((float)num2 * 1.77777779f);
                                pt.X = this.CropRect.Width - num + this.CropRect.X;
                                this.CropRect.X = pt.X;
                                this.CropRect.Width = num;
                                this.start.X = pt.X;
                                this.start.Y = pt.Y;
                            }
                        }
                        else if ((float)num2 / (float)num <= 1.25f)
                        {
                            this.CropRect.X = pt.X;
                            this.CropRect.Width = num;
                            this.CropRect.Y = pt.Y;
                            this.CropRect.Height = num2;
                            this.start.X = pt.X;
                            this.start.Y = pt.Y;
                        }
                        else
                        {
                            this.CropRect.X = pt.X;
                            this.CropRect.Width = num;
                            num2 = (int)((float)num * 1.25f);
                            pt.Y = this.CropRect.Height - num2 + this.CropRect.Y;
                            this.CropRect.Y = pt.Y;
                            this.CropRect.Height = num2;
                            this.start.X = pt.X;
                            this.start.Y = pt.Y;
                        }
                    }
                }
                else if (this.loc == 2)
                {
                    if (pt.X > this._maxRect.Width)
                    {
                        pt.X = this._maxRect.Width;
                    }
                    if (pt.Y < this._maxRect.Y)
                    {
                        pt.Y = this._maxRect.Y;
                    }
                    int num3 = pt.X - this.start.X;
                    int num4 = this.start.Y - pt.Y + this.CropRect.Height;
                    if (num3 >= 80 && num4 >= 80)
                    {
                        if (num3 > num4)
                        {
                            if ((float)num3 / (float)num4 <= 1.77777779f)
                            {
                                this.CropRect.Width = num3;
                                this.CropRect.Y = pt.Y;
                                this.CropRect.Height = num4;
                                this.start.Y = pt.Y;
                            }
                            else
                            {
                                this.CropRect.Y = pt.Y;
                                this.CropRect.Height = num4;
                                num3 = (int)((float)num4 * 1.77777779f);
                                this.CropRect.Width = num3;
                                this.start.Y = pt.Y;
                            }
                        }
                        else if ((float)num4 / (float)num3 <= 1.25f)
                        {
                            this.CropRect.Width = num3;
                            this.CropRect.Y = pt.Y;
                            this.CropRect.Height = this.start.Y - pt.Y + this.CropRect.Height;
                            this.start.Y = pt.Y;
                        }
                        else
                        {
                            this.CropRect.Width = num3;
                            num4 = (int)((float)num3 * 1.25f);
                            pt.Y = this.CropRect.Height - num4 + this.CropRect.Y;
                            this.CropRect.Y = pt.Y;
                            this.CropRect.Height = num4;
                            this.start.Y = pt.Y;
                        }
                    }
                }
                else if (this.loc == 3)
                {
                    if (pt.X > this._maxRect.Width)
                    {
                        pt.X = this._maxRect.Width;
                    }
                    if (pt.Y > this._maxRect.Height)
                    {
                        pt.Y = this._maxRect.Height;
                    }
                    int num5 = pt.X - this.start.X;
                    int num6 = pt.Y - this.start.Y;
                    if (num5 >= 80 && num6 >= 80)
                    {
                        if (num5 > num6)
                        {
                            if ((float)num5 / (float)num6 <= 1.77777779f)
                            {
                                this.CropRect.Width = num5;
                                this.CropRect.Height = num6;
                            }
                            else
                            {
                                this.CropRect.Height = num6;
                                num5 = (int)((float)num6 * 1.77777779f);
                                this.CropRect.Width = num5;
                            }
                        }
                        else if ((float)num6 / (float)num5 <= 1.25f)
                        {
                            this.CropRect.Width = num5;
                            this.CropRect.Height = num6;
                        }
                        else
                        {
                            this.CropRect.Width = num5;
                            num6 = (int)((float)num5 * 1.25f);
                            this.CropRect.Height = num6;
                        }
                    }
                }
                else if (this.loc == 4)
                {
                    if (pt.X < this._maxRect.X)
                    {
                        pt.X = this._maxRect.X;
                    }
                    if (pt.Y > this._maxRect.Height)
                    {
                        pt.Y = this._maxRect.Height;
                    }
                    int num7 = this.start.X - pt.X + this.CropRect.Width;
                    int num8 = pt.Y - this.start.Y;
                    if (num7 >= 80 && num8 >= 80)
                    {
                        if (num7 > num8)
                        {
                            if ((float)num7 / (float)num8 <= 1.77777779f)
                            {
                                this.CropRect.X = pt.X;
                                this.CropRect.Width = num7;
                                this.CropRect.Height = num8;
                                this.start.X = pt.X;
                            }
                            else
                            {
                                this.CropRect.Height = num8;
                                num7 = (int)((float)num8 * 1.77777779f);
                                pt.X = this.CropRect.Width - num7 + this.CropRect.X;
                                this.CropRect.X = pt.X;
                                this.CropRect.Width = num7;
                                this.start.X = pt.X;
                            }
                        }
                        else if ((float)num8 / (float)num7 <= 1.25f)
                        {
                            this.CropRect.X = pt.X;
                            this.CropRect.Width = this.start.X - pt.X + this.CropRect.Width;
                            this.CropRect.Height = num8;
                            this.start.X = pt.X;
                        }
                        else
                        {
                            this.CropRect.X = pt.X;
                            this.CropRect.Width = num7;
                            num8 = (int)((float)num7 * 1.25f);
                            this.CropRect.Height = num8;
                            this.start.X = pt.X;
                        }
                    }
                }
                else if (this.loc == 5)
                {
                    this.CropRect.X = pt.X - this.points.X;
                    this.CropRect.Y = pt.Y - this.points.Y;
                    if (this.CropRect.X < this._maxRect.X)
                    {
                        this.CropRect.X = this._maxRect.X;
                    }
                    if (this.CropRect.X + this.CropRect.Width > this._maxRect.Width)
                    {
                        this.CropRect.X = this._maxRect.Width - this.CropRect.Width;
                    }
                    if (this.CropRect.Y < this._maxRect.Y)
                    {
                        this.CropRect.Y = this._maxRect.Y;
                    }
                    if (this.CropRect.Y + this.CropRect.Height > this._maxRect.Height)
                    {
                        this.CropRect.Y = this._maxRect.Height - this.CropRect.Height;
                    }
                }
                this.cropBox.Update();
                this.cropBox.Refresh();
            }
            base.OnMouseMove(e);
        }

        private void cropBox_MouseDown(object sender, MouseEventArgs e)
        {
            new Point(e.X, e.Y);
            this.start.X = this.CropRect.X;
            this.start.Y = this.CropRect.Y;
            this.points.X = e.X - this.CropRect.X;
            this.points.Y = e.Y - this.CropRect.Y;
            this.IsDraging = true;
        }

        private void cropBox_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void cropBox_MouseUp(object sender, MouseEventArgs e)
        {
            this.IsDraging = false;
        }

        public Bitmap getCroppedImage()
        {
            Point point = new Point(0, 0);
            int width;
            int height;
            if (this.source.Width >= this.source.Height)
            {
                width = this.source.Width;
                height = this.source.Width;
                point.X = 0;
                point.Y = (int)((float)(this.source.Width - this.source.Height) / 2f);
            }
            else
            {
                width = this.source.Height;
                height = this.source.Height;
                point.Y = 0;
                point.X = (this.source.Height - this.source.Width) / 2;
            }
            Bitmap bitmap = new Bitmap(width, height);
            Graphics expr_B7 = Graphics.FromImage(bitmap);
            expr_B7.FillRectangle(new SolidBrush(this.bg), 0, 0, width, height);
            expr_B7.DrawImage(this.source, point.X, point.Y, this.source.Width, this.source.Height);
            int width2 = this.source.Width;
            int height2 = this.source.Height;
            double num;
            if (width2 > height2)
            {
                num = (double)((float)width2 / 580f);
            }
            else
            {
                num = (double)((float)height2 / 580f);
            }
            Math.Max(width2, height2);
            Rectangle srcRect = new Rectangle((int)Math.Ceiling((double)this.CropRect.X * num), (int)Math.Ceiling((double)this.CropRect.Y * num), (int)Math.Floor((double)this.CropRect.Width * num), (int)Math.Floor((double)this.CropRect.Height * num));
            Size size = default(Size);
            double num2 = (double)srcRect.Height / (double)srcRect.Width;
            if (num2 > 1.25)
            {
                num2 = 1.25;
            }
            else if (num2 < 0.5625)
            {
                num2 = 0.5625;
            }
            if (num2 <= 1.0)
            {
                size.Width = ((srcRect.Width > 1080) ? 1080 : ((int)Math.Floor((double)srcRect.Width)));
                size.Height = (int)Math.Floor((double)size.Width * num2);
            }
            else
            {
                size.Height = ((srcRect.Height > 1350) ? 1350 : ((int)Math.Floor((double)srcRect.Height)));
                size.Width = (int)Math.Floor((double)size.Height / num2);
            }
            Bitmap expr_298 = new Bitmap(size.Width, size.Height);
            Graphics.FromImage(expr_298).DrawImage(bitmap, new Rectangle(0, 0, size.Width, size.Height), srcRect, GraphicsUnit.Pixel);
            bitmap.Dispose();
            return expr_298;
        }

        private Rectangle getCropBounds()
        {
            int num = this.CropRect.X;
            int num2 = this.CropRect.Y;
            int num3 = this.CropRect.Width;
            int num4 = this.CropRect.Height;
            if (num3 < 0)
            {
                num += num3;
                int expr_39 = num3;
                num3 = expr_39 - expr_39 * 2;
            }
            if (num4 < 0)
            {
                num2 += num4;
                int expr_47 = num4;
                num4 = expr_47 - expr_47 * 2;
            }
            return new Rectangle(num, num2, num3, num4);
        }

        public void setBGColor(Color bgColor)
        {
            this.bg = bgColor;
            this.cropBox.Update();
            this.cropBox.Refresh();
        }

        public void setBGType(int bgType)
        {
            if (bgType == 1)
            {
                int width = this.cImage.Width;
                int height = this.cImage.Height;
                if (width > height)
                {
                    double num = (double)((float)width / (float)height);
                    int x = 0;
                    int width2 = 580;
                    int num2 = (int)(580.0 / num);
                    int num3 = (int)((float)(580 - num2) / 2f);
                    this._maxRect = new Rectangle(x, num3, width2, num2 + num3);
                }
                else if (width == height)
                {
                    this._maxRect = new Rectangle(0, 0, 580, 580);
                }
                else
                {
                    double num4 = (double)((float)height / (float)width);
                    int y = 0;
                    int height2 = 580;
                    int num5 = (int)(580.0 / num4);
                    int num6 = (int)((float)(580 - num5) / 2f);
                    this._maxRect = new Rectangle(num6, y, num5 + num6, height2);
                }
            }
            else
            {
                this._maxRect = new Rectangle(0, 0, 580, 580);
            }
            this.CropRect = new Rectangle(145, 145, 290, 290);
            this.cropBox.Update();
            this.cropBox.Refresh();
        }
    }
}