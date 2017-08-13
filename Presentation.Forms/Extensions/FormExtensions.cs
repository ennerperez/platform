using System;
using System.Windows.Forms;

namespace Platform.Presentation.Forms
{
    public static class FormExtensions
    {
        public static void ShowIn(this Form form, Form parent, EventHandler shown, params object[] param)
        {
            System.Type _t = form.GetType();
            Form _ChildForm;
            if (param != null)
            {
                _ChildForm = (Form)Activator.CreateInstance(_t, param);
            }
            else
            {
                _ChildForm = (Form)Activator.CreateInstance(_t);
            }
            _ChildForm.MdiParent = parent;
            if (shown != null) _ChildForm.Shown += shown;

            _ChildForm.Show();
        }

        public static void ShowIn(this Form form, Form parent, params object[] param)
        {
            ShowIn(form, parent, null, param);
        }

        public static DialogResult ShowAsDialog(this Form form, EventHandler shown, params object[] param)
        {
            System.Type _t = form.GetType();
            Form _ChildForm;
            if (param != null)
            {
                _ChildForm = (Form)Activator.CreateInstance(_t, param);
            }
            else
            {
                _ChildForm = (Form)Activator.CreateInstance(_t);
            }

            if (shown != null) _ChildForm.Shown += shown;

            return _ChildForm.ShowDialog();
        }

        public static System.Windows.Forms.Form GetParentForm(this System.Windows.Forms.Control @this)
        {
            System.Windows.Forms.Control _return = @this.Parent;

            while (!(@this.Parent.GetType() == typeof(System.Windows.Forms.Form)))
            {
                _return = GetParentForm(@this.Parent);
            }

            return (System.Windows.Forms.Form)_return;
        }

        public static Screen GetScreen(this Form @this)
        {
            return System.Windows.Forms.Screen.FromRectangle(new System.Drawing.Rectangle(@this.Location, @this.Size));
        }
    }
}