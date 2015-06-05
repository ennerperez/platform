﻿using Platform.Presentation.Windows.Forms.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Platform.Presentation.Windows.Forms.Customs
{
    [ToolboxBitmap(typeof(System.Windows.Forms.StatusStrip))]
    public class StatusStrip : System.Windows.Forms.StatusStrip
    {

        public event EventHandler AppearanceControlChanged;

        public StatusStrip() : base()
        {
        }

        private AppearanceControl _Appearance;
        public AppearanceControl Appearance
        {
            get { return _Appearance; }
            set
            {
                _Appearance = value;
                if (value != null)
                {
                    this.Renderer = value.Renderer;
                }
                this.Invalidate();
                this.OnAppearanceControlChanged(EventArgs.Empty);
            }
        }

        protected virtual void OnAppearanceControlChanged(EventArgs e)
        {
            if (this.Appearance != null)
            {
                this.Appearance.AppearanceChanged += AppearanceControl_AppearanceChanged;
                this.Appearance.Disposed += AppearanceControl_Disposed;
                this.Renderer = this.Appearance.Renderer;
            }
            else
            {
                this.Renderer = new ToolStripProfessionalRenderer();
            }
            this.Invalidate();

            if (AppearanceControlChanged != null)
            {
                AppearanceControlChanged(this, e);
            }
        }

        private void AppearanceControl_Disposed(object sender, EventArgs e)
        {
            this.Appearance = null;
            this.OnAppearanceControlChanged(EventArgs.Empty);
        }

        private void AppearanceControl_AppearanceChanged(object sender, EventArgs e)
        {
            this.Renderer = this.Appearance.Renderer;
            this.Invalidate();
        }

    }

}