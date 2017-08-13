using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Controls
{
    [ToolboxBitmap(typeof(System.Windows.Forms.FlowLayoutPanel))]
    [ToolboxItem(true)]
    public class SmoothPanel : FlowLayoutPanel
    {
        private Timer scroller;

        private int t;

        private int c;

        private int b;

        private int d;

        private bool up;

        public event EventHandler ScrollDidEnd;

        public event EventHandler ScrollDidChange;

        public SmoothPanel()
        {
            this.scroller = new Timer();
            this.scroller.Interval = 1;
            this.scroller.Enabled = false;
            this.scroller.Tick += new EventHandler(this.Scroller_Tick);
            base.FlowDirection = FlowDirection.TopDown;
            this.MaximumSize = new Size(600, 2147483647);
            this.AutoSize = true;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!this.scroller.Enabled)
            {
                this.t = 0;
                this.c = 0;
                this.b = base.VerticalScroll.Value;
                this.d = 100;
            }
            if (e.Delta < 0)
            {
                if (this.up && this.scroller.Enabled)
                {
                    this.t = 0;
                    this.c = 0;
                    this.d = 100;
                    this.b = base.VerticalScroll.Value;
                    this.up = false;
                }
                this.c += Math.Abs(e.Delta);
                this.d += 10;
            }
            else
            {
                if (!this.up && this.scroller.Enabled)
                {
                    this.t = 0;
                    this.c = 0;
                    this.d = 100;
                    this.b = base.VerticalScroll.Value;
                    this.up = true;
                }
                this.c -= Math.Abs(e.Delta);
                this.d += 10;
            }
            this.scroller.Enabled = true;
        }

        private void Scroller_Tick(object sender, EventArgs e)
        {
            double num = (double)this.c * Math.Sin((double)this.t / (double)this.d * 1.5707963267948966) + (double)this.b;
            if (this.t >= this.d)
            {
                this.scroller.Enabled = false;
                return;
            }
            this.t += 5;
            EventArgs e2 = new EventArgs();
            this.ScrollDidChange(this, e2);
            if (Math.Ceiling(num) <= (double)base.VerticalScroll.Maximum && Math.Ceiling(num) >= (double)base.VerticalScroll.Minimum)
            {
                base.VerticalScroll.Value = (int)num;
                if (base.VerticalScroll.Value > base.VerticalScroll.Maximum - 50 - base.Height)
                {
                    this.ScrollDidEnd(this, e2);
                }
                return;
            }
            this.scroller.Enabled = false;
            if (Math.Ceiling(num) > (double)base.VerticalScroll.Maximum)
            {
                base.VerticalScroll.Value = base.VerticalScroll.Maximum;
                this.ScrollDidEnd(this, e2);
                return;
            }
            base.VerticalScroll.Value = base.VerticalScroll.Minimum;
        }
    }
}