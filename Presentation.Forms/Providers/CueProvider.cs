using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Providers
{

    [ProvideProperty("HasCue", typeof(System.Windows.Forms.Control))]
    [ProvideProperty("CueText", typeof(System.Windows.Forms.Control))]
    [ToolboxBitmap(typeof(System.Windows.Forms.TextBox))]
    [ToolboxItem(true)]
    public class CueProvider : System.ComponentModel.Component, IExtenderProvider
    {

        private Hashtable m_properties = new Hashtable();

        protected System.Windows.Forms.Control m_activecontrol;
        private class Properties
        {
            public bool HasCue { get; set; }
            public string CueText { get; set; }
        }

        private Properties EnsurePropertiesExists(object key)
        {
            Properties p = (Properties)m_properties[key];

            if (p == null)
            {
                p = new Properties();
                m_properties[key] = p;
            }

            return p;
        }

        public bool CanExtend(object extendee)
        {
            //Or TypeOf extendee Is ComboBox Then 'Or TypeOf extendee Is Controls.DynamicControl
            if (extendee is TextBox)
            {
                m_activecontrol = (TextBox)extendee;
                return true;
            }
            else
            {
                m_activecontrol = null;
                return false;
            }
        }

        [DefaultValue(false)]
        public bool GetHasCue(Control b)
        {
            return EnsurePropertiesExists(b).HasCue;
        }

        public void SetHasCue(TextBox b, bool value)
        {
            EnsurePropertiesExists(b).HasCue = value;

            if (value)
            {
                CueProviderActions.SetCue(b, EnsurePropertiesExists(b).CueText);
                //    'AddHandler b.GotFocus, AddressOf Control_GotFocus
                //    'AddHandler b.LostFocus, AddressOf Control_LostFocus
            }
            else
            {
                CueProviderActions.ClearCue(b);
                //    'RemoveHandler b.GotFocus, AddressOf Control_GotFocus
                //    'RemoveHandler b.LostFocus, AddressOf Control_LostFocus
            }

            b.Invalidate();
        }

        [DefaultValue("CueText")]
        public string GetCueText(TextBox b)
        {
            return EnsurePropertiesExists(b).CueText;
        }

        public void SetCueText(TextBox b, string value)
        {
            EnsurePropertiesExists(b).CueText = value;
            if (EnsurePropertiesExists(b).HasCue)
            {
                CueProviderActions.SetCue(b, value);
            }
            else
            {
                CueProviderActions.ClearCue(b);
            }

            b.Invalidate();

        }

        //Private Sub Control_GotFocus(sender As Object, e As EventArgs)
        //    If sender.Text = sender.Tag Then
        //        sender.Text = Nothing
        //        sender.Font = New System.Drawing.Font(CType(sender, Control).Font.FontFamily, sender.Font.Size, System.Drawing.FontStyle.Regular)
        //        sender.ForeColor = System.Drawing.SystemColors.WindowText
        //    End If

        //    m_activecontrol = CType(sender, System.Windows.Forms.ContextMenuStrip).SourceControl
        //End Sub

        //Private Sub Control_LostFocus(sender As Object, e As EventArgs)
        //    If sender.Text.Trim = sender.Tag Or sender.Text.Trim = "" Then
        //        sender.Text = sender.Tag
        //        sender.Font = New System.Drawing.Font(CType(sender, Control).Font.FontFamily, sender.Font.Size, System.Drawing.FontStyle.Italic)
        //        sender.ForeColor = System.Drawing.SystemColors.ScrollBar
        //    End If
        //End Sub

    }

    public static class CueProviderActions
    {

        private const int EM_SETCUEBANNER = 0x1501;

        /// <summary>
        /// Sets a text box's cue text.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="cue">The cue text.</param>
        public static void SetCue(TextBox textBox, string cue)
        {
            NativeMethods.SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, cue);
        }

        /// <summary>
        /// Clears a text box's cue text.
        /// </summary>
        /// <param name="textBox">The text box</param>
        [SecurityCritical()]
        public static void ClearCue(TextBox textBox)
        {
            NativeMethods.SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, string.Empty);
        }

    }

}