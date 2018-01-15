using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Platform.Presentation.Forms
{
    public static class ControlExtensions
    {
        public static bool IsDesignerHosted(this Control @this)
        {
            Control ctrl = @this;
            while (ctrl != null)
            {
                if (ctrl.Site != null && ctrl.Site.DesignMode)
                    return true;
                else
                    ctrl = ctrl.Parent;
            }
            return false;
        }

        public static bool IsDesignMode(this Control @this)
        {
            return Assembly.GetEntryAssembly().Location.Contains("VisualStudio");
        }

        public static bool IsDesigntime(this Control @this)
        {
            return (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
        }

        public static bool IsRuntime(this Control @this)
        {
            return (LicenseManager.UsageMode == LicenseUsageMode.Runtime);
        }

        public static void SetLeave(this System.Windows.Forms.NumericUpDown @this, Boolean disposing = false)
        {
            if (!disposing)
                @this.Leave += numericUpDownLeave;
            else
                @this.Leave -= numericUpDownLeave;
        }

        internal static void numericUpDownLeave(object sender, EventArgs e)
        {
            var ctrl = (NumericUpDown)sender;
            {
                if (ctrl != null)
                {
                    if (ctrl.IsDisposed)
                        if (ctrl.Controls[1].Text == "")
                        {
                            ctrl.Text = ctrl.Minimum.ToString();
                            ctrl.Value = ctrl.Minimum;
                        }
                        else
                            ctrl.Leave -= numericUpDownLeave;
                }
            }
        }
    }
}