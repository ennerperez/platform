using Platform.Support;
using Platform.Support.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

[assembly: InternalsVisibleTo("Sample")]

namespace Platform.Presentation.Forms.Controls
{
    public partial class DynamicControl : Control, INotifyPropertyChanged
    {
        public Type GetDataType()
        {
            switch (Type)
            {
                case VariantType.Short:
                    return typeof(short);

                case VariantType.Integer:
                    return typeof(int);

                case VariantType.Single:
                    return typeof(Single);

                case VariantType.Double:
                case VariantType.Currency:
                    return typeof(double);

                case VariantType.Date:
                    return typeof(DateTime);

                case VariantType.String:
                    return typeof(string);

                case VariantType.Object:
                case VariantType.Variant:
                case VariantType.DataObject:
                case VariantType.UserDefinedType:
                    return typeof(object);

                case VariantType.Error:
                    return typeof(Exception);

                case VariantType.Boolean:
                    return typeof(bool);

                case VariantType.Decimal:
                    return typeof(decimal);

                case VariantType.Byte:
                    return typeof(byte);

                case VariantType.Char:
                    return typeof(char);

                case VariantType.Long:
                    return typeof(long);

                case VariantType.Array:
                    return typeof(IEnumerable);

                default:
                    return null;
            }
        }

        private VariantType type = VariantType.String;
        public VariantType Type { get { return type; } set { this.SetField(ref type, value, "Type"); } }

        private Control internalControl = new TextBox();

        public Control Control { get { return internalControl; } }

        #region TextBox

        //
        // Summary:
        //     Gets or sets a value indicating whether pressing ENTER in a multiline System.Windows.Forms.TextBox
        //     control creates a new line of text in the control or activates the default button
        //     for the form.
        //
        // Returns:
        //     true if the ENTER key creates a new line of text in a multiline version of the
        //     control; false if the ENTER key activates the default button for the form. The
        //     default is false.
        [DefaultValue(false)]
        [Category("TextBox")]
        public bool AcceptsReturn { get; set; }

        //
        // Summary:
        //     Gets or sets a custom System.Collections.Specialized.StringCollection to use
        //     when the System.Windows.Forms.TextBox.AutoCompleteSource property is set to CustomSource.
        //
        // Returns:
        //     A System.Collections.Specialized.StringCollection to use with System.Windows.Forms.TextBox.AutoCompleteSource.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        [Category("TextBox")]
        public AutoCompleteStringCollection AutoCompleteCustomSource { get; set; } = new AutoCompleteStringCollection();

        //
        // Summary:
        //     Gets or sets an option that controls how automatic completion works for the System.Windows.Forms.TextBox.
        //
        // Returns:
        //     One of the values of System.Windows.Forms.AutoCompleteMode. The following are
        //     the values. System.Windows.Forms.AutoCompleteMode.AppendAppends the remainder
        //     of the most likely candidate string to the existing characters, highlighting
        //     the appended characters.System.Windows.Forms.AutoCompleteMode.SuggestDisplays
        //     the auxiliary drop-down list associated with the edit control. This drop-down
        //     is populated with one or more suggested completion strings.System.Windows.Forms.AutoCompleteMode.SuggestAppendAppends
        //     both Suggest and Append options.System.Windows.Forms.AutoCompleteMode.NoneDisables
        //     automatic completion. This is the default.
        //
        // Exceptions:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     The specified value is not one of the values of System.Windows.Forms.AutoCompleteMode.
        [Browsable(true)]
        [DefaultValue(AutoCompleteMode.None)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("TextBox")]
        public AutoCompleteMode AutoCompleteMode { get; set; } = AutoCompleteMode.None;

        //
        // Summary:
        //     Gets or sets a value specifying the source of complete strings used for automatic
        //     completion.
        //
        // Returns:
        //     One of the values of System.Windows.Forms.AutoCompleteSource. The options are
        //     AllSystemSources, AllUrl, FileSystem, HistoryList, RecentlyUsedList, CustomSource,
        //     and None. The default is None.
        //
        // Exceptions:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     The specified value is not one of the values of System.Windows.Forms.AutoCompleteSource.
        [Browsable(true)]
        [DefaultValue(System.Windows.Forms.AutoCompleteSource.None)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("TextBox")]
        public System.Windows.Forms.AutoCompleteSource AutoCompleteSource { get; set; } = System.Windows.Forms.AutoCompleteSource.None;

        //
        // Summary:
        //     Gets or sets whether the System.Windows.Forms.TextBox control modifies the case
        //     of characters as they are typed.
        //
        // Returns:
        //     One of the System.Windows.Forms.CharacterCasing enumeration values that specifies
        //     whether the System.Windows.Forms.TextBox control modifies the case of characters.
        //     The default is CharacterCasing.Normal.
        //
        // Exceptions:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     A value that is not within the range of valid values for the enumeration was
        //     assigned to the property.
        [DefaultValue(CharacterCasing.Normal)]
        [Category("TextBox")]
        public CharacterCasing CharacterCasing { get; set; } = CharacterCasing.Normal;

        //
        // Summary:
        //     Gets or sets a value indicating whether this is a multiline System.Windows.Forms.TextBox
        //     control.
        //
        // Returns:
        //     true if the control is a multiline System.Windows.Forms.TextBox control; otherwise,
        //     false. The default is false.
        [Category("TextBox")]
        public bool Multiline { get; set; }

        //
        // Summary:
        //     Gets or sets the character used to mask characters of a password in a single-line
        //     System.Windows.Forms.TextBox control.
        //
        // Returns:
        //     The character used to mask characters entered in a single-line System.Windows.Forms.TextBox
        //     control. Set the value of this property to 0 (character value) if you do not
        //     want the control to mask characters as they are typed. Equals 0 (character value)
        //     by default.
        [DefaultValue('\0')]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category("TextBox")]
        public char PasswordChar { get; set; } = '\0';

        //
        // Summary:
        //     Gets or sets which scroll bars should appear in a multiline System.Windows.Forms.TextBox
        //     control.
        //
        // Returns:
        //     One of the System.Windows.Forms.ScrollBars enumeration values that indicates
        //     whether a multiline System.Windows.Forms.TextBox control appears with no scroll
        //     bars, a horizontal scroll bar, a vertical scroll bar, or both. The default is
        //     ScrollBars.None.
        //
        // Exceptions:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     A value that is not within the range of valid values for the enumeration was
        //     assigned to the property.
        [DefaultValue(ScrollBars.None)]
        [Localizable(true)]
        [Category("TextBox")]
        public ScrollBars ScrollBars { get; set; } = ScrollBars.None;

        //
        // Summary:
        //     Gets or sets how text is aligned in a System.Windows.Forms.TextBox control.
        //
        // Returns:
        //     One of the System.Windows.Forms.HorizontalAlignment enumeration values that specifies
        //     how text is aligned in the control. The default is HorizontalAlignment.Left.
        //
        // Exceptions:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     A value that is not within the range of valid values for the enumeration was
        //     assigned to the property.
        [DefaultValue(HorizontalAlignment.Left)]
        [Localizable(true)]
        [Category("TextBox")]
        public HorizontalAlignment TextAlign { get; set; } = HorizontalAlignment.Left;

        //
        // Summary:
        //     Gets or sets a value indicating whether the text in the System.Windows.Forms.TextBox
        //     control should appear as the default password character.
        //
        // Returns:
        //     true if the text in the System.Windows.Forms.TextBox control should appear as
        //     the default password character; otherwise, false.
        [DefaultValue(false)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category("TextBox")]
        public bool UseSystemPasswordChar { get; set; }

        #endregion TextBox

        #region NumericUpDown

        [Category("NumericUpDown")]
        public int DecimalPlaces { get; set; }

        //
        // Summary:
        //     Gets or sets a value indicating whether the spin box (also known as an up-down
        //     control) should display the value it contains in hexadecimal format.
        //
        // Returns:
        //     true if the spin box should display its value in hexadecimal format; otherwise,
        //     false. The default is false.
        [DefaultValue(false)]
        [Category("NumericUpDown")]
        public bool Hexadecimal { get; set; }

        //
        // Summary:
        //     Gets or sets the value to increment or decrement the spin box (also known as
        //     an up-down control) when the up or down buttons are clicked.
        //
        // Returns:
        //     The value to increment or decrement the System.Windows.Forms.NumericUpDown.Value
        //     property when the up or down buttons are clicked on the spin box. The default
        //     value is 1.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     The assigned value is not greater than or equal to zero.
        [Category("NumericUpDown")]
        public decimal Increment { get; set; }

        //
        // Summary:
        //     Gets or sets the maximum value for the spin box (also known as an up-down control).
        //
        // Returns:
        //     The maximum value for the spin box. The default value is 100.
        [RefreshProperties(RefreshProperties.All)]
        [Category("NumericUpDown")]
        public decimal Maximum { get; set; }

        //
        // Summary:
        //     Gets or sets the minimum allowed value for the spin box (also known as an up-down
        //     control).
        //
        // Returns:
        //     The minimum allowed value for the spin box. The default value is 0.
        [RefreshProperties(RefreshProperties.All)]
        [Category("NumericUpDown")]
        public decimal Minimum { get; set; }

        //
        // Summary:
        //     Gets or sets a value indicating whether a thousands separator is displayed in
        //     the spin box (also known as an up-down control) when appropriate.
        //
        // Returns:
        //     true if a thousands separator is displayed in the spin box when appropriate;
        //     otherwise, false. The default value is false.
        [DefaultValue(false)]
        [Localizable(true)]
        [Category("NumericUpDown")]
        public bool ThousandsSeparator { get; set; }

        #endregion NumericUpDown

        #region DateTimePicker

        //
        // Summary:
        //     Gets or sets the font style applied to the calendar.
        //
        // Returns:
        //     A System.Drawing.Font that represents the font style applied to the calendar.
        [AmbientValue(null)]
        [Localizable(true)]
        [Category("DateTimePicker")]
        public Font CalendarFont { get; set; }

        //
        // Summary:
        //     Gets or sets the foreground color of the calendar.
        //
        // Returns:
        //     A System.Drawing.Color that represents the foreground color of the calendar.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The value assigned is null.
        [Category("DateTimePicker")]
        public Color CalendarForeColor { get; set; }

        //
        // Summary:
        //     Gets or sets the background color of the calendar month.
        //
        // Returns:
        //     A System.Drawing.Color that represents the background color of the calendar month.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The value assigned is null.
        [Category("DateTimePicker")]
        public Color CalendarMonthBackground { get; set; }

        //
        // Summary:
        //     Gets or sets the background color of the calendar title.
        //
        // Returns:
        //     A System.Drawing.Color that represents the background color of the calendar title.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The value assigned is null.
        [Category("DateTimePicker")]
        public Color CalendarTitleBackColor { get; set; }

        //
        // Summary:
        //     Gets or sets the foreground color of the calendar title.
        //
        // Returns:
        //     A System.Drawing.Color that represents the foreground color of the calendar title.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The value assigned is null.
        [Category("DateTimePicker")]
        public Color CalendarTitleForeColor { get; set; }

        //
        // Summary:
        //     Gets or sets the foreground color of the calendar trailing dates.
        //
        // Returns:
        //     A System.Drawing.Color that represents the foreground color of the calendar trailing
        //     dates.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The value assigned is null.
        [Category("DateTimePicker")]
        public Color CalendarTrailingForeColor { get; set; }

        //
        // Summary:
        //     Gets or sets the custom date/time format string.
        //
        // Returns:
        //     A string that represents the custom date/time format. The default is null.
        [DefaultValue(null)]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category("DateTimePicker")]
        public string CustomFormat { get; set; }

        //
        // Summary:
        //     Gets or sets the alignment of the drop-down calendar on the System.Windows.Forms.DateTimePicker
        //     control.
        //
        // Returns:
        //     The alignment of the drop-down calendar on the control. The default is System.Windows.Forms.LeftRightAlignment.Left.
        //
        // Exceptions:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     The value assigned is not one of the System.Windows.Forms.LeftRightAlignment
        //     values.
        [DefaultValue(LeftRightAlignment.Left)]
        [Localizable(true)]
        [Category("DateTimePicker")]
        public LeftRightAlignment DropDownAlign { get; set; } = LeftRightAlignment.Left;

        //
        // Summary:
        //     Gets or sets the format of the date and time displayed in the control.
        //
        // Returns:
        //     One of the System.Windows.Forms.DateTimePickerFormat values. The default is System.Windows.Forms.DateTimePickerFormat.Long.
        //
        // Exceptions:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     The value assigned is not one of the System.Windows.Forms.DateTimePickerFormat
        //     values.
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category("DateTimePicker")]
        public DateTimePickerFormat Format { get; set; } = DateTimePickerFormat.Long;

        //
        // Summary:
        //     Gets or sets the maximum date and time that can be selected in the control.
        //
        // Returns:
        //     The maximum date and time that can be selected in the control. The default is
        //     12/31/9998 23:59:59.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The value assigned is less than the System.Windows.Forms.DateTimePicker.MinDate
        //     value.
        //
        //   T:System.SystemException:
        //     The value assigned is greater than the System.Windows.Forms.DateTimePicker.MaxDateTime
        //     value.
        [Category("DateTimePicker")]
        public DateTime MaxDate { get; set; } = DateTimePicker.MaximumDateTime;

        //
        // Summary:
        //     Gets or sets the minimum date and time that can be selected in the control.
        //
        // Returns:
        //     The minimum date and time that can be selected in the control. The default is
        //     1/1/1753 00:00:00.
        //
        // Exceptions:
        //   T:System.ArgumentException:
        //     The value assigned is not less than the System.Windows.Forms.DateTimePicker.MaxDate
        //     value.
        //
        //   T:System.SystemException:
        //     The value assigned is less than the System.Windows.Forms.DateTimePicker.MinDateTime
        //     value.
        [Category("DateTimePicker")]
        public DateTime MinDate { get; set; } = DateTimePicker.MinimumDateTime;

        //
        // Summary:
        //     Gets or sets whether the contents of the System.Windows.Forms.DateTimePicker
        //     are laid out from right to left.
        //
        // Returns:
        //     true if the layout of the System.Windows.Forms.DateTimePicker contents is from
        //     right to left; otherwise, false. The default is false.
        [DefaultValue(false)]
        [Localizable(true)]
        [Category("DateTimePicker")]
        public virtual bool RightToLeftLayout { get; set; }

        //
        // Summary:
        //     Gets or sets a value indicating whether a check box is displayed to the left
        //     of the selected date.
        //
        // Returns:
        //     true if a check box is displayed to the left of the selected date; otherwise,
        //     false. The default is false.
        [DefaultValue(false)]
        [Category("DateTimePicker")]
        public bool ShowCheckBox { get; set; }

        //
        // Summary:
        //     Gets or sets a value indicating whether a spin button control (also known as
        //     an up-down control) is used to adjust the date/time value.
        //
        // Returns:
        //     true if a spin button control is used to adjust the date/time value; otherwise,
        //     false. The default is false.
        [DefaultValue(false)]
        [Category("DateTimePicker")]
        public bool ShowUpDown { get; set; }

        #endregion DateTimePicker

        #region CheckBox

        //
        // Summary:
        //     Gets or sets the value that determines the appearance of a System.Windows.Forms.CheckBox
        //     control.
        //
        // Returns:
        //     One of the System.Windows.Forms.Appearance values. The default value is System.Windows.Forms.Appearance.Normal.
        //
        // Exceptions:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     The value assigned is not one of the System.Windows.Forms.Appearance values.
        [DefaultValue(Appearance.Normal)]
        [Localizable(true)]
        [Category("CheckBox")]
        public Appearance Appearance { get; set; } = Appearance.Normal;

        //
        // Summary:
        //     Gets or set a value indicating whether the System.Windows.Forms.CheckBox.Checked
        //     or System.Windows.Forms.CheckBox.CheckState values and the System.Windows.Forms.CheckBox's
        //     appearance are automatically changed when the System.Windows.Forms.CheckBox is
        //     clicked.
        //
        // Returns:
        //     true if the System.Windows.Forms.CheckBox.Checked value or System.Windows.Forms.CheckBox.CheckState
        //     value and the appearance of the control are automatically changed on the System.Windows.Forms.Control.Click
        //     event; otherwise, false. The default value is true.
        [DefaultValue(true)]
        [Category("CheckBox")]
        public bool AutoCheck { get; set; } = true;

        //
        // Summary:
        //     Gets or sets the horizontal and vertical alignment of the check mark on a System.Windows.Forms.CheckBox
        //     control.
        //
        // Returns:
        //     One of the System.Drawing.ContentAlignment values. The default value is MiddleLeft.
        //
        // Exceptions:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     The value assigned is not one of the System.Drawing.ContentAlignment enumeration
        //     values.
        [Bindable(true)]
        [DefaultValue(ContentAlignment.MiddleLeft)]
        [Localizable(true)]
        [Category("CheckBox")]
        public ContentAlignment CheckAlign { get; set; } = ContentAlignment.MiddleLeft;

        //
        // Summary:
        //     Gets or sets the state of the System.Windows.Forms.CheckBox.
        //
        // Returns:
        //     One of the System.Windows.Forms.CheckState enumeration values. The default value
        //     is Unchecked.
        //
        // Exceptions:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     The value assigned is not one of the System.Windows.Forms.CheckState enumeration
        //     values.
        [Bindable(true)]
        [DefaultValue(CheckState.Unchecked)]
        [RefreshProperties(RefreshProperties.All)]
        [Category("CheckBox")]
        public CheckState CheckState { get; set; } = CheckState.Unchecked;

        //
        // Summary:
        //     Gets or sets a value indicating whether the System.Windows.Forms.CheckBox will
        //     allow three check states rather than two.
        //
        // Returns:
        //     true if the System.Windows.Forms.CheckBox is able to display three check states;
        //     otherwise, false. The default value is false.
        [DefaultValue(false)]
        [Category("CheckBox")]
        public bool ThreeState { get; set; }

        #endregion CheckBox

        #region Shared

        //
        // Summary:
        //     Gets or sets a value indicating whether the control should redraw its surface
        //     using a secondary buffer. Setting this property has no effect on the System.Windows.Forms.Control
        //     control.
        //
        // Returns:
        //     true if the control should redraw its surface using a secondary buffer; otherwise,
        //     false.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Control")]
        protected override bool DoubleBuffered { get; set; }

        //
        // Summary:
        //     Gets or sets a value indicating the background color of the System.Windows.Forms.Control
        //     control.
        //
        // Returns:
        //     The background System.Drawing.Color of the System.Windows.Forms.Control.
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Control")]
        public override Color BackColor { get; set; }

        //
        // Summary:
        //     Gets or sets the background image for the control.
        //
        // Returns:
        //     The background image for the control.
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Control")]
        public override Image BackgroundImage { get; set; }

        //
        // Summary:
        //     Gets or sets the layout of the background image of the System.Windows.Forms.Control
        //     control.
        //
        // Returns:
        //     One of the System.Windows.Forms.ImageLayout values.
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Control")]
        public override ImageLayout BackgroundImageLayout { get; set; }

        //
        // Summary:
        //     Gets or sets the foreground color of the System.Windows.Forms.Control
        //     control.
        //
        // Returns:
        //     The foreground System.Drawing.Color of the System.Windows.Forms.Control.
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Category("Control")]
        public override Color ForeColor { get; set; }

        //
        // Summary:
        //     Gets or sets a value that indicates whether the control resizes based on its
        //     contents.
        //
        // Returns:
        //     true if the control automatically resizes based on its contents; otherwise, false.
        //     The default is true.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Control")]
        public override bool AutoSize { get; set; }

        #endregion Shared

        #region Values

        private string text;

        //
        // Summary:
        //     Gets or sets the current text in the text box.
        //
        // Returns:
        //     The text displayed in the control.
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [Category("Control")]
        public override string Text { get { return text; } set { this.SetField(ref text, value, "Text"); } }

        private object value;

        //
        // Summary:
        //     Gets or sets the value assigned to the spin box (also known as an up-down control).
        //
        // Returns:
        //     The numeric value of the System.Windows.Forms.NumericUpDown control.
        //
        // Exceptions:
        //   T:System.ArgumentOutOfRangeException:
        //     The assigned value is less than the System.Windows.Forms.NumericUpDown.Minimum
        //     property value.-or- The assigned value is greater than the System.Windows.Forms.NumericUpDown.Maximum
        //     property value.
        [Bindable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Control")]
        public object Value { get { return value; } set { this.SetField(ref this.value, value, "Value"); } }

        private bool @checked = true;

        //
        // Summary:
        //     Gets or sets a value indicating whether the System.Windows.Forms.Control
        //     property has been set with a valid date/time value and the displayed value is
        //     able to be updated.
        //
        // Returns:
        //     true if the System.Windows.Forms.Control property has been set with
        //     a valid System.DateTime value and the displayed value is able to be updated;
        //     otherwise, false. The default is true.
        [Bindable(true)]
        [DefaultValue(true)]
        [Category("Control")]
        public bool Checked { get { return @checked; } set { this.SetField(ref @checked, value, "Checked"); } }

        #endregion Values

        public DynamicControl() : base()
        {
            InitializeComponent();
            InitializeControl();
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            this.DataBindings.Clear();

            IEnumerable<PropertyInfo> externalProps = null;

            if (internalControl.GetType() == typeof(TextBox))
                externalProps = typeof(TextBox).GetProperties();

            if (internalControl.GetType() == typeof(NumericUpDown))
                externalProps = typeof(NumericUpDown).GetProperties();

            if (internalControl.GetType() == typeof(DateTimePicker))
                externalProps = typeof(DateTimePicker).GetProperties();

            if (externalProps != null && internalControl != null)
            {
                var _type = internalControl.GetType();
                var internalProps = typeof(DynamicControl).GetProperties().Where((item) =>
                {
                    var attributes = (CategoryAttribute)item.GetCustomAttributes(typeof(CategoryAttribute), false).FirstOrDefault();
                    return (attributes != null && attributes.Category == _type.Name);
                });

                //var exclude = new[] { "Parent","Location", "Top", "Left", "Margin", "Padding", "Index", "Tag" };
                if (internalControl.GetType() == typeof(TextBox))
                    internalControl.DataBindings.Add(new Binding("Text", this, "Text", true, DataSourceUpdateMode.OnPropertyChanged));

                if (internalControl.GetType() == typeof(NumericUpDown) || internalControl.GetType() == typeof(DateTimePicker))
                    internalControl.DataBindings.Add(new Binding("Value", this, "Value", true, DataSourceUpdateMode.OnPropertyChanged));

                if (internalControl.GetType() == typeof(CheckBox) || internalControl.GetType() == typeof(DateTimePicker))
                    internalControl.DataBindings.Add(new Binding("Checked", this, "Checked", true, DataSourceUpdateMode.OnPropertyChanged));

                if (internalControl.GetType() == typeof(CheckBox))
                    internalControl.DataBindings.Add(new Binding("AutoSize", this, "AutoSize", true, DataSourceUpdateMode.OnPropertyChanged));

                internalControl.DataBindings.Add(new Binding("Width", this, "Width", true, DataSourceUpdateMode.OnPropertyChanged));
                internalControl.DataBindings.Add(new Binding("Height", this, "Height", true, DataSourceUpdateMode.OnPropertyChanged));

                var joined = from eitem in externalProps
                             join iitem in internalProps
                             on eitem.Name equals iitem.Name
                             where eitem.CanWrite && iitem.CanWrite
                             select eitem;

                foreach (var item in joined)
                {
                    //var attributes = (CategoryAttribute)item.GetCustomAttributes(typeof(CategoryAttribute), false).FirstOrDefault();
                    //if (attributes != null && attributes.Category == _type.Name)
                    //{
                    internalControl.DataBindings.Add(new Binding(item.Name, this, item.Name, true, DataSourceUpdateMode.OnPropertyChanged));
                    //}
                }
            }
        }

        private void InitializeControl()
        {
            this.SuspendLayout();
            this.Controls.Clear();

            if (internalControl != null || !internalControl.IsDisposed)
            {
                internalControl.Dispose();
                internalControl = null;
            }

            if (GetDataType() == typeof(string))
                internalControl = new TextBox();

            if (GetDataType() == typeof(DateTime))
                internalControl = new DateTimePicker();

            if (GetDataType() == typeof(int) || GetDataType() == typeof(short) || GetDataType() == typeof(long))
                internalControl = new NumericUpDown() { DecimalPlaces = 0 };

            if (GetDataType() == typeof(float) || GetDataType() == typeof(decimal))
                internalControl = new NumericUpDown() { DecimalPlaces = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalDigits };

            if (GetDataType() == typeof(bool))
                internalControl = new CheckBox() { AutoSize = false };

            if (internalControl != null)
            {
                if (internalControl.GetType() != typeof(TextBox) || !this.Multiline)
                    this.Height = internalControl.Height;

                if (this.Width < internalControl.Width)
                    this.Width = internalControl.Width;

                if (this.Height < internalControl.Height)
                    this.Height = internalControl.Height;

                this.Controls.Add(internalControl);
            }

            this.ResumeLayout();
            this.Invalidate();
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InitializeControl();
            InitializeBindings();
            PropertyChanged?.Invoke(sender, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}