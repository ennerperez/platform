using System;
using System.Windows.Forms;

namespace Platform.Presentation.Windows.Forms
{
    public static class Extensions
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

        public static DialogResult ShowAsDialog(this Form form,EventHandler shown, params object[] param)
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

        //#region  Draggable

        //// TKey is control to drag, TValue is a flag used while dragging
        //private Dictionary<System.Windows.Forms.Control, bool> draggables = new Dictionary<System.Windows.Forms.Control, bool>();

        //private System.Drawing.Size mouseOffset;
        ///// <summary>
        ///// Enabling/disabling dragging for control
        ///// </summary>
        //public void Draggable(System.Windows.Forms.Control control, bool Enable)
        //{
        //    if (Enable)
        //    {
        //        // enabling drag feature
        //        if (draggables.ContainsKey(control))
        //        {
        //            // return if control is already draggable
        //            return;
        //        }
        //        draggables.Add(control, false);
        //        // 'false' - initial state is 'not dragging'
        //        // assign required event handlers
        //        control.MouseDown += control_MouseDown;
        //        control.MouseUp += control_MouseUp;
        //        control.MouseMove += control_MouseMove;
        //    }
        //    else
        //    {
        //        // disabling drag feature
        //        if (!draggables.ContainsKey(control))
        //        {
        //            // return if control is not draggable
        //            return;
        //        }
        //        // remove event handlers
        //        control.MouseDown -= control_MouseDown;
        //        control.MouseUp -= control_MouseUp;
        //        control.MouseMove -= control_MouseMove;
        //        draggables.Remove(control);
        //    }
        //}

        ////Handles Control.MouseDown
        //void control_MouseDown(object sender, MouseEventArgs e)
        //{
        //    mouseOffset = new System.Drawing.Size(e.Location);
        //    // turning on dragging
        //    draggables((System.Windows.Forms.Control)sender) = true;
        //}
        ////Handles Control.MouseUp
        //void control_MouseUp(object sender, MouseEventArgs e)
        //{
        //    // turning off dragging
        //    draggables((System.Windows.Forms.Control)sender) = false;
        //}
        ////Handles Control.MouseMove
        //void control_MouseMove(object sender, MouseEventArgs e)
        //{
        //    // only if dragging is turned on
        //    if (draggables((System.Windows.Forms.Control)sender) == true)
        //    {
        //        // calculations of control's new position
        //        System.Drawing.Point newLocationOffset = e.Location - mouseOffset;
        //        ((System.Windows.Forms.Control)sender).Left += newLocationOffset.X;
        //        ((System.Windows.Forms.Control)sender).Top += newLocationOffset.Y;
        //    }
        //}

        //#endregion

        #region Designer

        public static bool IsDesignerHosted(this Control @this)
        {
            Control ctrl = @this;
            while (ctrl != null)
            {
                if (ctrl.Site != null && ctrl.Site.DesignMode)
                {
                    return true;
                }
                else
                {
                    ctrl = ctrl.Parent;
                }
            }
            return false;
        }

        public static bool IsInDesignMode(this  Control @this)
        {
            return System.Reflection.Assembly.GetExecutingAssembly().Location.Contains("VisualStudio");
        }

        #endregion

        //#region Globalization

        //public void ApplyCulture(Form form, CultureInfo culture)
        //{
        //    // Create a resource manager for this Form and determine its fields via reflection.
        //    ComponentResourceManager resources = new ComponentResourceManager(form.GetType());
        //    FieldInfo[] fieldInfos = form.GetType().GetFields(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);

        //    // Call SuspendLayout for Form and all fields derived from Control, so assignment of 
        //    //   localized text doesn't change layout immediately.
        //    form.SuspendLayout();
        //    int index = 0;
        //    while (index < fieldInfos.Length)
        //    {
        //        if (fieldInfos(index).FieldType.IsSubclassOf(typeof(Control)))
        //        {
        //            fieldInfos(index).FieldType.InvokeMember("SuspendLayout", BindingFlags.InvokeMethod, null, fieldInfos(index).GetValue(form), null);
        //        }
        //        System.Math.Max(System.Threading.Interlocked.Increment(ref index), index - 1);
        //    }

        //    // If available, assign localized text to Form and fields with Text property.
        //    // If available, assign localized Localtion and Size to fields
        //    System.Drawing.Point point;
        //    System.Nullable<System.Drawing.Size> size = null;
        //    size = resources.GetObject("$this.ClientSize");
        //    if (size != null)
        //    {
        //        form.ClientSize = size.Value;
        //    }
        //    String text = resources.GetString("$this.Text");
        //    if (text != null)
        //    {
        //        form.Text = text;
        //    }
        //    index = 0;
        //    while (index < fieldInfos.Length)
        //    {
        //        if (fieldInfos(index).FieldType.GetProperty("Text", typeof(String)) != null)
        //        {
        //            text = resources.GetString(fieldInfos(index).Name + ".Text");
        //            if (text != null)
        //            {
        //                fieldInfos(index).FieldType.InvokeMember("Text", BindingFlags.SetProperty, null, fieldInfos(index).GetValue(form), new object[] { text });
        //            }
        //        }
        //        if (fieldInfos(index).FieldType.GetProperty("Location", typeof(System.Drawing.Point)) != null)
        //        {
        //            point = (System.Drawing.Point)resources.GetObject(fieldInfos(index).Name + ".Location");
        //            if (point != null)
        //            {
        //                fieldInfos(index).FieldType.InvokeMember("Location", BindingFlags.SetProperty, null, fieldInfos(index).GetValue(form), new object[] { point });
        //            }
        //        }
        //        if (fieldInfos(index).FieldType.GetProperty("Size", typeof(System.Drawing.Size)) != null)
        //        {
        //            size = resources.GetObject(fieldInfos(index).Name + ".Size");
        //            if (size != null)
        //            {
        //                fieldInfos(index).FieldType.InvokeMember("Size", BindingFlags.SetProperty, null, fieldInfos(index).GetValue(form), new object[] { size.Value });
        //            }
        //        }
        //        System.Math.Max(System.Threading.Interlocked.Increment(ref index), index - 1);
        //    }

        //    // Call ResumeLayout for Form and all fields derived from Control to resume layout logic.
        //    // Call PerformLayout, so layout changes due to assignment of localized text are performed.
        //    index = 0;
        //    while (index < fieldInfos.Length)
        //    {
        //        if (fieldInfos(index).FieldType.IsSubclassOf(typeof(Control)))
        //        {
        //            fieldInfos(index).FieldType.InvokeMember("ResumeLayout", BindingFlags.InvokeMethod, null, fieldInfos(index).GetValue(form), new object[] { true });
        //        }
        //        System.Math.Max(System.Threading.Interlocked.Increment(ref index), index - 1);
        //    }
        //    form.ResumeLayout(false);
        //    form.PerformLayout();
        //}

        //#endregion

        #region FontAwesome

        public static void SetIcon(this ToolStripItem @this, Platform.Support.Drawing.FontAwesome.IconType type)
        {
            @this.Font = new System.Drawing.Font(Platform.Support.Drawing.FontAwesome.Helpers.FontFamily, @this.Font.Size, System.Drawing.GraphicsUnit.Point);
            @this.DisplayStyle = ToolStripItemDisplayStyle.Text;
            @this.ToolTipText = @this.Text;
            @this.Text = char.ConvertFromUtf32((int)type);
        }

        #endregion

    }
}
