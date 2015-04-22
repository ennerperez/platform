using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Presentation.Windows.Forms.Controls
{

    /// <summary>
    /// TODO: Fix, render problems when scrollings.
    /// </summary>

    [ToolboxBitmap(typeof(System.Windows.Forms.ProgressBar))]
    public class FillBar : Control
    {

        #region  Events

        public event EventHandler<EventArgs> ValueChanged;
        public void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        #endregion

        #region Properties

        private float _Value = 0;
        [DefaultValue(0)]
        public float Value
        {
            get { return _Value; }
            set
            {
                if (!_Value.Equals(value))
                {
                    if (value >= this._MinValue && value <= this._MaxValue)
                    {
                        _Value = value;
                        this.Invalidate();
                        this.OnValueChanged(new EventArgs());
                    }
                }
            }
        }

        private float _MaxValue = 100;
        [DefaultValue(100)]
        public float MaxValue
        {
            get { return _MaxValue; }
            set
            {
                if (!_MaxValue.Equals(value))
                {
                    if (value > this._MinValue)
                    {
                        _MaxValue = value;
                        if (this._Value < _MaxValue)
                            this._Value = _MaxValue;
                        this.Invalidate();
                    }
                }
            }
        }

        private float _MinValue = 0;
        [DefaultValue(0)]
        public float MinValue
        {
            get { return _MinValue; }
            set
            {
                if (!_MinValue.Equals(value))
                {
                    if (value < this._MaxValue)
                    {
                        _MinValue = value;
                        if (this._Value < _MinValue)
                            this._Value = _MinValue;
                        this.Invalidate();
                    }
                }
            }
        }

        private Color _FillColor = SystemColors.Highlight;
        [DefaultValue(typeof(System.Drawing.Color), "SystemColors.Highlight")]
        public Color FillColor
        {
            get { return _FillColor; }
            set
            {
                if (!_FillColor.Equals(value))
                {
                    _FillColor = value;
                    this.Invalidate();
                }
            }
        }

        [Browsable(false)]
        public Brush CustomBrush { get; set; }

        private int _BorderWidth = 2;
        [DefaultValue(2)]
        public int BorderWidth
        {
            get { return _BorderWidth; }
            set
            {
                if (!_BorderWidth.Equals(value))
                {
                    _BorderWidth = value;
                    this.Invalidate();
                }
            }
        }


        private Color _BorderColor = SystemColors.ControlDark;
        [DefaultValue(typeof(Color), "SystemColors.ControlDark")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                _BorderColor = value;
                this.Invalidate();
            }
        }

        private Orientation _Orientation = Orientation.Horizontal;
        [DefaultValue(typeof(Orientation), "Orientation.Horizontal")]
        public Orientation Orientation
        {
            get { return _Orientation; }
            set
            {
                _Orientation = value;
                this.Invalidate();
            }
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
            e.Graphics.InterpolationMode = InterpolationMode.Low;

            Brush _bsh = new SolidBrush(this.FillColor);
            if (this.CustomBrush != null)
                _bsh = this.CustomBrush;

            //Dim _w  As Double = Me.Width = 100% = Me.MaxValue
            //                    ???????? = ???? = Me.Value

            float _x = 0;

            RectangleF _recFill;
            if (this.Orientation == System.Windows.Forms.Orientation.Horizontal)
            {
                _x = (((this.Value * 100) / this.MaxValue) * e.ClipRectangle.Width) / 100;
                if (this.RightToLeft == System.Windows.Forms.RightToLeft.Yes)
                {
                    _recFill = new RectangleF(e.ClipRectangle.Width - _x, 0, _x, e.ClipRectangle.Width);
                }
                else
                {
                    _recFill = new RectangleF(0, 0, _x, e.ClipRectangle.Width);
                }
            }
            else
            {
                _x = (((this.Value * 100) / this.MaxValue) * e.ClipRectangle.Height) / 100;
                if (this.RightToLeft == System.Windows.Forms.RightToLeft.Yes)
                {
                    _recFill = new RectangleF(0, 0, e.ClipRectangle.Width, _x);
                }
                else
                {
                    _recFill = new RectangleF(0, e.ClipRectangle.Height - _x, e.ClipRectangle.Width, _x);
                }
            }

            e.Graphics.FillRectangle(_bsh, _recFill);
            e.Graphics.DrawRectangle(new Pen(this.BorderColor, this.BorderWidth), e.ClipRectangle);

        }

    }

}
