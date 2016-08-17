using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Platform.Presentation.Forms
{
    public static class ControlExtensions
    {

        //TODO: Crear Helpers

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

    }
}
