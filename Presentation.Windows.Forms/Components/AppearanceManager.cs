using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Platform.Presentation.Windows.Forms
{

    namespace Components
    {

        using Presentation.Windows.Forms.Designers;
        using Presentation.Windows.Forms.Renderers.ColorTables;
        using System.Drawing.Design;
        using System.IO;
        using System.Windows.Forms.Design;
        using System.Xml.Serialization;

        public class AppearanceManager : Component
        {

            public event EventHandler AppearanceChanged;

            #region Renders

            private ToolStripProfessionalRenderer customRenderer = null;
            private ToolStripProfessionalRenderer office2007Renderer = null;
            private ToolStripProfessionalRenderer blueRenderer = null;
            private ToolStripProfessionalRenderer silverRenderer = null;
            private ToolStripProfessionalRenderer oliveRenderer = null;
            private ToolStripProfessionalRenderer xpRenderer = null;
            private ToolStripProfessionalRenderer classicRenderer = null;

            #endregion

            public enum enumPresetStyles
            {
                Custom = 0,
                Office2007 = 1,
                Office2003Blue = 2,
                Office2003Silver = 3,
                Office2003Olive = 4,
                OfficeXP = 5,
                OfficeClassic = 6
            }

            public AppearanceManager()
            {
                customRenderer = new ToolStripProfessionalRenderer(new CustomColorTable(this));
                office2007Renderer = new ToolStripProfessionalRenderer(new Office2007ColorTable());
                blueRenderer = new ToolStripProfessionalRenderer(new Office2003BlueColorTable());
                silverRenderer = new ToolStripProfessionalRenderer(new Office2003SilverColorTable());
                oliveRenderer = new ToolStripProfessionalRenderer(new Office2003OliveColorTable());
                xpRenderer = new ToolStripProfessionalRenderer(new OfficeXPColorTable());
                classicRenderer = new ToolStripProfessionalRenderer(new OfficeClassicColorTable());

                _Renderer = customRenderer;
                _CustomAppearance = new AppearanceProperties(this);
            }

            private ToolStripProfessionalRenderer _Renderer = null;
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            [Browsable(false)]
            public ToolStripProfessionalRenderer Renderer
            {
                get { return _Renderer; }
                set { _Renderer = value; }
            }

            private enumPresetStyles _Preset;
            [Category("Appearance")]
            public enumPresetStyles Preset
            {
                get { return _Preset; }
                set
                {
                    _Preset = value;

                    switch (value)
                    {
                        case enumPresetStyles.Custom:
                            this.Renderer = customRenderer;
                            break;
                        case enumPresetStyles.Office2003Blue:
                            this.Renderer = blueRenderer;
                            break;
                        case enumPresetStyles.Office2003Olive:
                            this.Renderer = oliveRenderer;
                            break;
                        case enumPresetStyles.Office2003Silver:
                            this.Renderer = silverRenderer;
                            break;
                        case enumPresetStyles.Office2007:
                            this.Renderer = office2007Renderer;
                            break;
                        case enumPresetStyles.OfficeClassic:
                            this.Renderer = classicRenderer;
                            break;
                        case enumPresetStyles.OfficeXP:
                            this.Renderer = xpRenderer;
                            break;
                    }

                    this.OnAppearanceChanged(EventArgs.Empty);
                }
            }

            private AppearanceProperties _CustomAppearance;
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            [Category("Appearance")]
            [Editor(typeof(CustomAppearancePropertyEditor), typeof(UITypeEditor))]
            public AppearanceProperties CustomAppearance
            {
                get { return _CustomAppearance; }
                set { _CustomAppearance = value; }
            }

            [Serializable()]
            public class AppearanceProperties
            {

                //Parameterless ctor required for serialization
                public AppearanceProperties()
                {
                }

                public void SetAppearanceControl(AppearanceManager ap)
                {
                    _ButtonAppearance = new ButtonAppearanceProperties(ap);
                    _ButtonAppearance.SelectedAppearance.SetAppearanceControl(ap);
                    _ButtonAppearance.PressedAppearance.SetAppearanceControl(ap);
                    _ButtonAppearance.CheckedAppearance.SetAppearanceControl(ap);
                    _GripAppearance.SetAppearanceControl(ap);
                    _ImageMarginAppearance = new ImageMarginAppearanceProperties(ap);
                    _ImageMarginAppearance.Normal.SetAppearanceControl(ap);
                    _ImageMarginAppearance.Revealed.SetAppearanceControl(ap);
                    _MenuStripAppearance.SetAppearanceControl(ap);
                    _MenuItemAppearance.SetAppearanceControl(ap);
                    _RaftingContainerAppearance.SetAppearanceControl(ap);
                    _SeparatorAppearance.SetAppearanceControl(ap);
                    _StatusStripAppearance.SetAppearanceControl(ap);
                    _ToolStripAppearance.SetAppearanceControl(ap);
                    _OverflowButtonAppearance.SetAppearanceControl(ap);
                }

                public AppearanceProperties(AppearanceManager appearanceControl)
                {
                    _ButtonAppearance = new ButtonAppearanceProperties(appearanceControl);
                    _GripAppearance = new GripAppearanceProperties(appearanceControl);
                    _ImageMarginAppearance = new ImageMarginAppearanceProperties(appearanceControl);
                    _MenuStripAppearance = new MenuStripAppearanceProperties(appearanceControl);
                    _MenuItemAppearance = new MenuItemAppearanceProperties(appearanceControl);
                    _RaftingContainerAppearance = new RaftingContainerAppearanceProperties(appearanceControl);
                    _SeparatorAppearance = new SeparatorAppearanceProperties(appearanceControl);
                    _StatusStripAppearance = new StatusStripAppearanceProperties(appearanceControl);
                    _ToolStripAppearance = new ToolStripAppearanceProperties(appearanceControl);
                    _OverflowButtonAppearance = new OverflowButtonAppearanceProperties(appearanceControl);
                }

                private ButtonAppearanceProperties _ButtonAppearance;
                [TypeConverter(typeof(ExpandableObjectConverter))]
                [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                [Category("Appearance")]
                public ButtonAppearanceProperties ButtonAppearance
                {
                    get { return _ButtonAppearance; }
                    set { _ButtonAppearance = value; }
                }

                private GripAppearanceProperties _GripAppearance;
                [TypeConverter(typeof(ExpandableObjectConverter))]
                [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                [Category("Appearance")]
                public GripAppearanceProperties GripAppearance
                {
                    get { return _GripAppearance; }
                    set { _GripAppearance = value; }
                }

                private ImageMarginAppearanceProperties _ImageMarginAppearance;
                [TypeConverter(typeof(ExpandableObjectConverter))]
                [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                [Category("Appearance")]
                public ImageMarginAppearanceProperties ImageMarginAppearance
                {
                    get { return _ImageMarginAppearance; }
                    set { _ImageMarginAppearance = value; }
                }

                private MenuStripAppearanceProperties _MenuStripAppearance;
                [TypeConverter(typeof(ExpandableObjectConverter))]
                [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                [Category("Appearance")]
                public MenuStripAppearanceProperties MenuStripAppearance
                {
                    get { return _MenuStripAppearance; }
                    set { _MenuStripAppearance = value; }
                }

                private MenuItemAppearanceProperties _MenuItemAppearance;
                [TypeConverter(typeof(ExpandableObjectConverter))]
                [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                [Category("Appearance")]
                public MenuItemAppearanceProperties MenuItemAppearance
                {
                    get { return _MenuItemAppearance; }
                    set { _MenuItemAppearance = value; }
                }

                private RaftingContainerAppearanceProperties _RaftingContainerAppearance;
                [TypeConverter(typeof(ExpandableObjectConverter))]
                [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                [Category("Appearance")]
                public RaftingContainerAppearanceProperties RaftingContainerAppearance
                {
                    get { return _RaftingContainerAppearance; }
                    set { _RaftingContainerAppearance = value; }
                }

                private SeparatorAppearanceProperties _SeparatorAppearance;
                [TypeConverter(typeof(ExpandableObjectConverter))]
                [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                [Category("Appearance")]
                public SeparatorAppearanceProperties SeparatorAppearance
                {
                    get { return _SeparatorAppearance; }
                    set { _SeparatorAppearance = value; }
                }

                private StatusStripAppearanceProperties _StatusStripAppearance;
                [TypeConverter(typeof(ExpandableObjectConverter))]
                [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                [Category("Appearance")]
                public StatusStripAppearanceProperties StatusStripAppearance
                {
                    get { return _StatusStripAppearance; }
                    set { _StatusStripAppearance = value; }
                }

                private ToolStripAppearanceProperties _ToolStripAppearance;
                [TypeConverter(typeof(ExpandableObjectConverter))]
                [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                [Category("Appearance")]
                public ToolStripAppearanceProperties ToolStripAppearance
                {
                    get { return _ToolStripAppearance; }
                    set { _ToolStripAppearance = value; }
                }

                private OverflowButtonAppearanceProperties _OverflowButtonAppearance;
                [TypeConverter(typeof(ExpandableObjectConverter))]
                [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                [Category("Appearance")]
                public OverflowButtonAppearanceProperties OverflowButtonAppearance
                {
                    get { return _OverflowButtonAppearance; }
                    set { _OverflowButtonAppearance = value; }
                }

                [Serializable()]
                public class ButtonAppearanceProperties
                {

                    //Parameterless ctor required for serialization
                    public ButtonAppearanceProperties()
                    {
                    }

                    public ButtonAppearanceProperties(AppearanceManager appearanceControl)
                    {
                        _SelectedAppearance = new SelectedButtonAppearanceProperties(appearanceControl);
                        _CheckedAppearance = new CheckedButtonAppearanceProperties(appearanceControl);
                        _PressedAppearance = new PressedButtonAppearanceProperties(appearanceControl);
                    }

                    private SelectedButtonAppearanceProperties _SelectedAppearance;
                    [TypeConverter(typeof(ExpandableObjectConverter))]
                    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                    public SelectedButtonAppearanceProperties SelectedAppearance
                    {
                        get { return _SelectedAppearance; }
                        set { _SelectedAppearance = value; }
                    }

                    private CheckedButtonAppearanceProperties _CheckedAppearance;
                    [TypeConverter(typeof(ExpandableObjectConverter))]
                    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                    public CheckedButtonAppearanceProperties CheckedAppearance
                    {
                        get { return _CheckedAppearance; }
                        set { _CheckedAppearance = value; }
                    }

                    private PressedButtonAppearanceProperties _PressedAppearance;
                    [TypeConverter(typeof(ExpandableObjectConverter))]
                    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                    public PressedButtonAppearanceProperties PressedAppearance
                    {
                        get { return _PressedAppearance; }
                        set { _PressedAppearance = value; }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }
                }

                [Serializable()]
                public class ImageMarginAppearanceProperties
                {

                    //Parameterless ctor required for serialization
                    public ImageMarginAppearanceProperties()
                    {
                    }

                    public ImageMarginAppearanceProperties(AppearanceManager appearanceControl)
                    {
                        _Normal = new ImageMarginNormalAppearanceProperties(appearanceControl);
                        _Revealed = new ImageMarginRevealedAppearanceProperties(appearanceControl);
                    }

                    private ImageMarginNormalAppearanceProperties _Normal;
                    [TypeConverter(typeof(ExpandableObjectConverter))]
                    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                    public ImageMarginNormalAppearanceProperties Normal
                    {
                        get { return _Normal; }
                        set { _Normal = value; }
                    }

                    private ImageMarginRevealedAppearanceProperties _Revealed;
                    [TypeConverter(typeof(ExpandableObjectConverter))]
                    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
                    public ImageMarginRevealedAppearanceProperties Revealed
                    {
                        get { return _Revealed; }
                        set { _Revealed = value; }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }
                }

                #region  Property Group Classes

                [Serializable()]
                public class SelectedButtonAppearanceProperties
                {

                    public SelectedButtonAppearanceProperties()
                    {
                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._Border = pct.ButtonSelectedBorder;
                        this._BorderHighlight = pct.ButtonSelectedHighlightBorder;
                        this._GradientBegin = pct.ButtonSelectedGradientBegin;
                        this._GradientEnd = pct.ButtonSelectedGradientEnd;
                        this._GradientMiddle = pct.ButtonSelectedGradientMiddle;
                        this._Highlight = pct.ButtonSelectedHighlight;
                    }

                    private AppearanceManager ap;
                    public SelectedButtonAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }

                    private Color _GradientBegin = Color.FromArgb(255, 255, 222);
                    [DefaultValue(typeof(Color), "255, 255, 222")]
                    [XmlIgnore()]
                    public Color GradientBegin
                    {
                        get { return _GradientBegin; }
                        set
                        {
                            _GradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientMiddle = Color.FromArgb(255, 225, 172);
                    [DefaultValue(typeof(Color), "255, 225, 172")]
                    [XmlIgnore()]
                    public Color GradientMiddle
                    {
                        get { return _GradientMiddle; }
                        set
                        {
                            _GradientMiddle = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientEnd = Color.FromArgb(255, 203, 136);
                    [DefaultValue(typeof(Color), "255, 203, 136")]
                    [XmlIgnore()]
                    public Color GradientEnd
                    {
                        get { return _GradientEnd; }
                        set
                        {
                            _GradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _Highlight = Color.FromArgb(196, 208, 229);
                    [DefaultValue(typeof(Color), "196, 208, 229")]
                    [XmlIgnore()]
                    public Color Highlight
                    {
                        get { return _Highlight; }
                        set
                        {
                            _Highlight = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _BorderHighlight = Color.FromArgb(0, 0, 128);
                    [DefaultValue(typeof(Color), "0, 0, 128")]
                    [XmlIgnore()]
                    public Color BorderHighlight
                    {
                        get { return _BorderHighlight; }
                        set
                        {
                            _BorderHighlight = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _Border = Color.FromArgb(0, 0, 128);
                    [DefaultValue(typeof(Color), "0, 0, 128")]
                    [XmlIgnore()]
                    public Color Border
                    {
                        get { return _Border; }
                        set
                        {
                            _Border = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }
                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intGradientBegin
                    {
                        get { return this.GradientBegin.ToArgb(); }
                        set { this.GradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientMiddle
                    {
                        get { return this.GradientMiddle.ToArgb(); }
                        set { this.GradientMiddle = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientEnd
                    {
                        get { return this.GradientEnd.ToArgb(); }
                        set { this.GradientEnd = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intHighlight
                    {
                        get { return this.Highlight.ToArgb(); }
                        set { this.Highlight = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intBorderHighlight
                    {
                        get { return this.BorderHighlight.ToArgb(); }
                        set { this.BorderHighlight = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intBorder
                    {
                        get { return this.Border.ToArgb(); }
                        set { this.Border = Color.FromArgb(value); }
                    }

                }

                [Serializable()]
                public class PressedButtonAppearanceProperties
                {
                    public PressedButtonAppearanceProperties()
                    {
                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._Border = pct.ButtonPressedBorder;
                        this._BorderHighlight = pct.ButtonPressedHighlightBorder;
                        this._GradientBegin = pct.ButtonPressedGradientBegin;
                        this._GradientEnd = pct.ButtonPressedGradientEnd;
                        this._GradientMiddle = pct.ButtonPressedGradientMiddle;
                        this._Highlight = pct.ButtonPressedHighlight;

                    }
                    private AppearanceManager ap;
                    public PressedButtonAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }

                    private Color _GradientBegin = Color.FromArgb(254, 128, 62);
                    [DefaultValue(typeof(Color), "254, 128, 62")]
                    [XmlIgnore()]
                    public Color GradientBegin
                    {
                        get { return _GradientBegin; }
                        set
                        {
                            _GradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientMiddle = Color.FromArgb(255, 177, 109);
                    [DefaultValue(typeof(Color), "255, 177, 109")]
                    [XmlIgnore()]
                    public Color GradientMiddle
                    {
                        get { return _GradientMiddle; }
                        set
                        {
                            _GradientMiddle = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientEnd = Color.FromArgb(255, 223, 154);
                    [DefaultValue(typeof(Color), "255, 223, 154")]
                    [XmlIgnore()]
                    public Color GradientEnd
                    {
                        get { return _GradientEnd; }
                        set
                        {
                            _GradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _Highlight = Color.FromArgb(152, 173, 210);
                    [DefaultValue(typeof(Color), "152, 173, 210")]
                    [XmlIgnore()]
                    public Color Highlight
                    {
                        get { return _Highlight; }
                        set
                        {
                            _Highlight = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _BorderHighlight = Color.FromArgb(51, 94, 168);
                    [DefaultValue(typeof(Color), "51, 94, 168")]
                    [XmlIgnore()]
                    public Color BorderHighlight
                    {
                        get { return _BorderHighlight; }
                        set
                        {
                            _BorderHighlight = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _Border = Color.FromArgb(0, 0, 128);
                    [DefaultValue(typeof(Color), "0, 0, 128")]
                    [XmlIgnore()]
                    public Color Border
                    {
                        get { return _Border; }
                        set
                        {
                            _Border = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }
                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intGradientBegin
                    {
                        get { return this.GradientBegin.ToArgb(); }
                        set { this.GradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientMiddle
                    {
                        get { return this.GradientMiddle.ToArgb(); }
                        set { this.GradientMiddle = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientEnd
                    {
                        get { return this.GradientEnd.ToArgb(); }
                        set { this.GradientEnd = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intHighlight
                    {
                        get { return this.Highlight.ToArgb(); }
                        set { this.Highlight = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intBorderHighlight
                    {
                        get { return this.BorderHighlight.ToArgb(); }
                        set { this.BorderHighlight = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intBorder
                    {
                        get { return this.Border.ToArgb(); }
                        set { this.Border = Color.FromArgb(value); }
                    }


                }

                [Serializable()]
                public class CheckedButtonAppearanceProperties
                {
                    public CheckedButtonAppearanceProperties()
                    {
                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._Background = pct.CheckBackground;
                        this._BorderHighlight = pct.ButtonSelectedBorder;
                        this._GradientBegin = pct.ButtonCheckedGradientBegin;
                        this._GradientEnd = pct.ButtonCheckedGradientEnd;
                        this._GradientMiddle = pct.ButtonCheckedGradientMiddle;
                        this._Highlight = pct.ButtonCheckedHighlight;
                        this._PressedBackrgound = pct.CheckPressedBackground;
                        this._SelectedBackground = pct.CheckSelectedBackground;
                    }
                    private AppearanceManager ap;
                    public CheckedButtonAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }
                    private Color _GradientBegin = Color.FromArgb(255, 223, 154);
                    [DefaultValue(typeof(Color), "255, 223, 154")]
                    [XmlIgnore()]
                    public Color GradientBegin
                    {
                        get { return _GradientBegin; }
                        set
                        {
                            _GradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientMiddle = Color.FromArgb(255, 195, 116);
                    [DefaultValue(typeof(Color), "255, 195, 116")]
                    [XmlIgnore()]
                    public Color GradientMiddle
                    {
                        get { return _GradientMiddle; }
                        set
                        {
                            _GradientMiddle = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientEnd = Color.FromArgb(255, 166, 76);
                    [DefaultValue(typeof(Color), "255, 166, 76")]
                    [XmlIgnore()]
                    public Color GradientEnd
                    {
                        get { return _GradientEnd; }
                        set
                        {
                            _GradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _Highlight = Color.FromArgb(196, 208, 229);
                    [DefaultValue(typeof(Color), "196, 208, 229")]
                    [XmlIgnore()]
                    public Color Highlight
                    {
                        get { return _Highlight; }
                        set
                        {
                            _Highlight = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _BorderHighlight = Color.FromArgb(51, 94, 168);
                    [DefaultValue(typeof(Color), "51, 94, 168")]
                    [XmlIgnore()]
                    public Color BorderHighlight
                    {
                        get { return _BorderHighlight; }
                        set
                        {
                            _BorderHighlight = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _Background = Color.FromArgb(255, 192, 111);
                    [DefaultValue(typeof(Color), "255, 192, 111")]
                    [XmlIgnore()]
                    public Color Background
                    {
                        get { return _Background; }
                        set
                        {
                            _Background = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _SelectedBackground = Color.FromArgb(254, 128, 62);
                    [DefaultValue(typeof(Color), "254, 128, 62")]
                    [XmlIgnore()]
                    public Color SelectedBackground
                    {
                        get { return _SelectedBackground; }
                        set
                        {
                            _SelectedBackground = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _PressedBackrgound = Color.FromArgb(254, 128, 62);
                    [DefaultValue(typeof(Color), "254, 128, 62")]
                    [XmlIgnore()]
                    public Color PressedBackground
                    {
                        get { return _PressedBackrgound; }
                        set
                        {
                            _PressedBackrgound = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intGradientBegin
                    {
                        get { return this.GradientBegin.ToArgb(); }
                        set { this.GradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientMiddle
                    {
                        get { return this.GradientMiddle.ToArgb(); }
                        set { this.GradientMiddle = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientEnd
                    {
                        get { return this.GradientEnd.ToArgb(); }
                        set { this.GradientEnd = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intHighlight
                    {
                        get { return this.Highlight.ToArgb(); }
                        set { this.Highlight = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intBorderHighlight
                    {
                        get { return this.BorderHighlight.ToArgb(); }
                        set { this.BorderHighlight = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intBackground
                    {
                        get { return this.Background.ToArgb(); }
                        set { this.Background = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intSelectedBackground
                    {
                        get { return this.SelectedBackground.ToArgb(); }
                        set { this.SelectedBackground = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intPressedBackground
                    {
                        get { return this.PressedBackground.ToArgb(); }
                        set { this.PressedBackground = Color.FromArgb(value); }
                    }

                }

                [Serializable()]
                public class GripAppearanceProperties
                {
                    public GripAppearanceProperties()
                    {
                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._Dark = pct.GripDark;
                        this._Light = pct.GripLight;

                    }
                    private AppearanceManager ap;
                    public GripAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }
                    private Color _Dark = Color.FromArgb(39, 65, 118);
                    [DefaultValue(typeof(Color), "39, 65, 118")]
                    [XmlIgnore()]
                    public Color Dark
                    {
                        get { return _Dark; }
                        set
                        {
                            _Dark = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _Light = Color.FromArgb(255, 255, 255);
                    [DefaultValue(typeof(Color), "255, 255, 255")]
                    [XmlIgnore()]
                    public Color Light
                    {
                        get { return _Light; }
                        set
                        {
                            _Light = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intDark
                    {
                        get { return this.Dark.ToArgb(); }
                        set { this.Dark = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intLight
                    {
                        get { return this.Light.ToArgb(); }
                        set { this.Light = Color.FromArgb(value); }
                    }

                }

                [Serializable()]
                public class MenuStripAppearanceProperties
                {
                    public MenuStripAppearanceProperties()
                    {
                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._Border = pct.ButtonSelectedBorder;
                        this._GradientBegin = pct.ButtonSelectedGradientBegin;
                        this._GradientEnd = pct.ButtonSelectedGradientEnd;

                    }
                    private AppearanceManager ap;
                    public MenuStripAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }
                    private Color _Border = Color.FromArgb(0, 45, 150);
                    [DefaultValue(typeof(Color), "0, 45, 150")]
                    [XmlIgnore()]
                    public Color Border
                    {
                        get { return _Border; }
                        set
                        {
                            _Border = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientBegin = Color.FromArgb(158, 190, 245);
                    [DefaultValue(typeof(Color), "158, 190, 245")]
                    [XmlIgnore()]
                    public Color GradientBegin
                    {
                        get { return _GradientBegin; }
                        set
                        {
                            _GradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientEnd = Color.FromArgb(196, 218, 250);
                    [DefaultValue(typeof(Color), "196, 218, 250")]
                    [XmlIgnore()]
                    public Color GradientEnd
                    {
                        get { return _GradientEnd; }
                        set
                        {
                            _GradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intBorder
                    {
                        get { return this.Border.ToArgb(); }
                        set { this.Border = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientBegin
                    {
                        get { return this.GradientBegin.ToArgb(); }
                        set { this.GradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientEnd
                    {
                        get { return this.GradientEnd.ToArgb(); }
                        set { this.GradientEnd = Color.FromArgb(value); }
                    }

                }

                [Serializable()]
                public class MenuItemAppearanceProperties
                {
                    public MenuItemAppearanceProperties()
                    {
                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._Border = pct.MenuItemBorder;
                        this._PressedGradientBegin = pct.MenuItemPressedGradientBegin;
                        this._PressedGradientEnd = pct.MenuItemPressedGradientEnd;
                        this._PressedGradientMiddle = pct.MenuItemPressedGradientMiddle;
                        this._Selected = pct.MenuItemSelected;
                        this._SelectedGradientBegin = pct.MenuItemSelectedGradientBegin;
                        this._SelectedGradientEnd = pct.MenuItemSelectedGradientEnd;

                    }
                    private AppearanceManager ap;
                    public MenuItemAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }
                    private Color _Selected = Color.FromArgb(255, 238, 194);
                    [DefaultValue(typeof(Color), "255, 238, 194")]
                    [XmlIgnore()]
                    public Color Selected
                    {
                        get { return _Selected; }
                        set
                        {
                            _Selected = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _Border = Color.FromArgb(0, 0, 128);
                    [DefaultValue(typeof(Color), "0, 0, 128")]
                    [XmlIgnore()]
                    public Color Border
                    {
                        get { return _Border; }
                        set
                        {
                            _Border = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _SelectedGradientBegin = Color.FromArgb(255, 255, 222);
                    [DefaultValue(typeof(Color), "255, 255, 222")]
                    [XmlIgnore()]
                    public Color SelectedGradientBegin
                    {
                        get { return _SelectedGradientBegin; }
                        set
                        {
                            _SelectedGradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _SelectedGradientEnd = Color.FromArgb(255, 203, 136);
                    [DefaultValue(typeof(Color), "255, 203, 136")]
                    [XmlIgnore()]
                    public Color SelectedGradientEnd
                    {
                        get { return _SelectedGradientEnd; }
                        set
                        {
                            _SelectedGradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _PressedGradientBegin = Color.FromArgb(227, 239, 255);
                    [DefaultValue(typeof(Color), "227, 239, 255")]
                    [XmlIgnore()]
                    public Color PressedGradientBegin
                    {
                        get { return _PressedGradientBegin; }
                        set
                        {
                            _PressedGradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _PressedGradientMiddle = Color.FromArgb(161, 197, 249);
                    [DefaultValue(typeof(Color), "161, 197, 249")]
                    [XmlIgnore()]
                    public Color PressedGradientMiddle
                    {
                        get { return _PressedGradientMiddle; }
                        set
                        {
                            _PressedGradientMiddle = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _PressedGradientEnd = Color.FromArgb(123, 164, 224);
                    [DefaultValue(typeof(Color), "123, 164, 224")]
                    [XmlIgnore()]
                    public Color PressedGradientEnd
                    {
                        get { return _PressedGradientEnd; }
                        set
                        {
                            _PressedGradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intSelected
                    {
                        get { return this.Selected.ToArgb(); }
                        set { this.Selected = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intBorder
                    {
                        get { return this.Border.ToArgb(); }
                        set { this.Border = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intSelectedGradientBegin
                    {
                        get { return this.SelectedGradientBegin.ToArgb(); }
                        set { this.SelectedGradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intSelectedGradientEnd
                    {
                        get { return this.SelectedGradientEnd.ToArgb(); }
                        set { this.SelectedGradientEnd = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intPressedGradientBegin
                    {
                        get { return this.PressedGradientBegin.ToArgb(); }
                        set { this.PressedGradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intPressedGradientMiddle
                    {
                        get { return this.PressedGradientMiddle.ToArgb(); }
                        set { this.PressedGradientMiddle = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intPressedGradientEnd
                    {
                        get { return this.PressedGradientEnd.ToArgb(); }
                        set { this.PressedGradientEnd = Color.FromArgb(value); }
                    }

                }

                [Serializable()]
                public class RaftingContainerAppearanceProperties
                {
                    public RaftingContainerAppearanceProperties()
                    {
                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._GradientBegin = pct.RaftingContainerGradientBegin;
                        this._GradientEnd = pct.RaftingContainerGradientEnd;

                    }
                    private AppearanceManager ap;
                    public RaftingContainerAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }
                    private Color _GradientBegin = Color.FromArgb(158, 190, 245);
                    [DefaultValue(typeof(Color), "158, 190, 245")]
                    [XmlIgnore()]
                    public Color GradientBegin
                    {
                        get { return _GradientBegin; }
                        set
                        {
                            _GradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientEnd = Color.FromArgb(196, 218, 250);
                    [DefaultValue(typeof(Color), "196, 218, 250")]
                    [XmlIgnore()]
                    public Color GradientEnd
                    {
                        get { return _GradientEnd; }
                        set
                        {
                            _GradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intGradientBegin
                    {
                        get { return this.GradientBegin.ToArgb(); }
                        set { this.GradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientEnd
                    {
                        get { return this.GradientEnd.ToArgb(); }
                        set { this.GradientEnd = Color.FromArgb(value); }
                    }

                }

                [Serializable()]
                public class StatusStripAppearanceProperties
                {
                    public StatusStripAppearanceProperties()
                    {

                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._GradientBegin = pct.StatusStripGradientBegin;
                        this._GradientEnd = pct.StatusStripGradientEnd;

                    }
                    private AppearanceManager ap;
                    public StatusStripAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }
                    private Color _GradientBegin = Color.FromArgb(158, 190, 245);
                    [DefaultValue(typeof(Color), "158, 190, 245")]
                    [XmlIgnore()]
                    public Color GradientBegin
                    {
                        get { return _GradientBegin; }
                        set
                        {
                            _GradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientEnd = Color.FromArgb(196, 218, 250);
                    [DefaultValue(typeof(Color), "196, 218, 250")]
                    [XmlIgnore()]
                    public Color GradientEnd
                    {
                        get { return _GradientEnd; }
                        set
                        {
                            _GradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intGradientBegin
                    {
                        get { return this.GradientBegin.ToArgb(); }
                        set { this.GradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientEnd
                    {
                        get { return this.GradientEnd.ToArgb(); }
                        set { this.GradientEnd = Color.FromArgb(value); }
                    }

                }

                [Serializable()]
                public class SeparatorAppearanceProperties
                {
                    public SeparatorAppearanceProperties()
                    {

                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._Dark = pct.SeparatorDark;
                        this._Light = pct.SeparatorLight;

                    }
                    private AppearanceManager ap;
                    public SeparatorAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }
                    private Color _Dark = Color.FromArgb(106, 140, 203);
                    [DefaultValue(typeof(Color), "106, 140, 203")]
                    [XmlIgnore()]
                    public Color Dark
                    {
                        get { return _Dark; }
                        set
                        {
                            _Dark = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _Light = Color.FromArgb(241, 249, 255);
                    [DefaultValue(typeof(Color), "241, 249, 255")]
                    [XmlIgnore()]
                    public Color Light
                    {
                        get { return _Light; }
                        set
                        {
                            _Light = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intDark
                    {
                        get { return this.Dark.ToArgb(); }
                        set { this.Dark = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intLight
                    {
                        get { return this.Light.ToArgb(); }
                        set { this.Light = Color.FromArgb(value); }
                    }

                }

                [Serializable()]
                public class ToolStripAppearanceProperties
                {

                    public ToolStripAppearanceProperties()
                    {

                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._Border = pct.ToolStripBorder;
                        this._ContentPanelGradientBegin = pct.ToolStripContentPanelGradientBegin;
                        this._ContentPanelGradientEnd = pct.ToolStripContentPanelGradientEnd;
                        this._DropDownBackground = pct.ToolStripDropDownBackground;
                        this._GradientBegin = pct.ToolStripGradientBegin;
                        this._GradientEnd = pct.ToolStripGradientEnd;
                        this._GradientMiddle = pct.ToolStripGradientMiddle;
                        this._PanelGradientBegin = pct.ToolStripPanelGradientBegin;
                        this._PanelGradientEnd = pct.ToolStripPanelGradientEnd;

                    }

                    private AppearanceManager ap;
                    public ToolStripAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }

                    private Color _GradientBegin = Color.FromArgb(227, 239, 255);
                    [DefaultValue(typeof(Color), "227, 239, 255")]
                    [XmlIgnore()]
                    public Color GradientBegin
                    {
                        get { return _GradientBegin; }
                        set
                        {
                            _GradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientMiddle = Color.FromArgb(203, 225, 252);
                    [DefaultValue(typeof(Color), "203, 225, 252")]
                    [XmlIgnore()]
                    public Color GradientMiddle
                    {
                        get { return _GradientMiddle; }
                        set
                        {
                            _GradientMiddle = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientEnd = Color.FromArgb(123, 164, 224);
                    [DefaultValue(typeof(Color), "123, 164, 224")]
                    [XmlIgnore()]
                    public Color GradientEnd
                    {
                        get { return _GradientEnd; }
                        set
                        {
                            _GradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _Border = Color.FromArgb(59, 97, 156);
                    [DefaultValue(typeof(Color), "59, 97, 156")]
                    [XmlIgnore()]
                    public Color Border
                    {
                        get { return _Border; }
                        set
                        {
                            _Border = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _DropDownBackground = Color.FromArgb(246, 246, 246);
                    [DefaultValue(typeof(Color), "246, 246, 246")]
                    [XmlIgnore()]
                    public Color DropDownBackground
                    {
                        get { return _DropDownBackground; }
                        set
                        {
                            _DropDownBackground = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _ContentPanelGradientBegin = Color.FromArgb(158, 190, 245);
                    [DefaultValue(typeof(Color), "158, 190, 245")]
                    [XmlIgnore()]
                    public Color ContentPanelGradientBegin
                    {
                        get { return _ContentPanelGradientBegin; }
                        set
                        {
                            _ContentPanelGradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _ContentPanelGradientEnd = Color.FromArgb(196, 218, 250);
                    [DefaultValue(typeof(Color), "196, 218, 250")]
                    [XmlIgnore()]
                    public Color ContentPanelGradientEnd
                    {
                        get { return _ContentPanelGradientEnd; }
                        set
                        {
                            _ContentPanelGradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _PanelGradientBegin = Color.FromArgb(158, 190, 245);
                    [DefaultValue(typeof(Color), "158, 190, 245")]
                    [XmlIgnore()]
                    public Color PanelGradientBegin
                    {
                        get { return _PanelGradientBegin; }
                        set
                        {
                            _PanelGradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _PanelGradientEnd = Color.FromArgb(196, 218, 250);
                    [DefaultValue(typeof(Color), "196, 218, 250")]
                    [XmlIgnore()]
                    public Color PanelGradientEnd
                    {
                        get { return _PanelGradientEnd; }
                        set
                        {
                            _PanelGradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intGradientBegin
                    {
                        get { return this.GradientBegin.ToArgb(); }
                        set { this.GradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientMiddle
                    {
                        get { return this.GradientMiddle.ToArgb(); }
                        set { this.GradientMiddle = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientEnd
                    {
                        get { return this.GradientEnd.ToArgb(); }
                        set { this.GradientEnd = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intBorder
                    {
                        get { return this.Border.ToArgb(); }
                        set { this.Border = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intDropDownBackground
                    {
                        get { return this.DropDownBackground.ToArgb(); }
                        set { this.DropDownBackground = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intContentPanelGradientBegin
                    {
                        get { return this.ContentPanelGradientBegin.ToArgb(); }
                        set { this.ContentPanelGradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intContentPanelGradientEnd
                    {
                        get { return this.ContentPanelGradientEnd.ToArgb(); }
                        set { this.ContentPanelGradientEnd = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intPanelGradientBegin
                    {
                        get { return this.PanelGradientBegin.ToArgb(); }
                        set { this.PanelGradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intPanelGradientEnd
                    {
                        get { return this.PanelGradientEnd.ToArgb(); }
                        set { this.PanelGradientEnd = Color.FromArgb(value); }
                    }

                }

                [Serializable()]
                public class OverflowButtonAppearanceProperties
                {
                    public OverflowButtonAppearanceProperties()
                    {

                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._GradientBegin = pct.OverflowButtonGradientBegin;
                        this._GradientEnd = pct.OverflowButtonGradientEnd;
                        this._GradientMiddle = pct.OverflowButtonGradientMiddle;

                    }
                    private AppearanceManager ap;
                    public OverflowButtonAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }
                    private Color _GradientBegin = Color.FromArgb(127, 177, 250);
                    [DefaultValue(typeof(Color), "127, 177, 250")]
                    [XmlIgnore()]
                    public Color GradientBegin
                    {
                        get { return _GradientBegin; }
                        set
                        {
                            _GradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientMiddle = Color.FromArgb(82, 127, 208);
                    [DefaultValue(typeof(Color), "82, 127, 208")]
                    [XmlIgnore()]
                    public Color GradientMiddle
                    {
                        get { return _GradientMiddle; }
                        set
                        {
                            _GradientMiddle = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientEnd = Color.FromArgb(0, 53, 145);
                    [DefaultValue(typeof(Color), "0, 53, 145")]
                    [XmlIgnore()]
                    public Color GradientEnd
                    {
                        get { return _GradientEnd; }
                        set
                        {
                            _GradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intGradientBegin
                    {
                        get { return this.GradientBegin.ToArgb(); }
                        set { this.GradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientMiddle
                    {
                        get { return this.GradientMiddle.ToArgb(); }
                        set { this.GradientMiddle = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientEnd
                    {
                        get { return this.GradientEnd.ToArgb(); }
                        set { this.GradientEnd = Color.FromArgb(value); }
                    }

                }

                [Serializable()]
                public class ImageMarginNormalAppearanceProperties
                {
                    public ImageMarginNormalAppearanceProperties()
                    {

                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._GradientBegin = pct.ImageMarginGradientBegin;
                        this._GradientEnd = pct.ImageMarginGradientEnd;
                        this._GradientMiddle = pct.ImageMarginGradientMiddle;

                    }
                    private AppearanceManager ap;
                    public ImageMarginNormalAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }
                    private Color _GradientBegin = Color.FromArgb(227, 239, 255);
                    [DefaultValue(typeof(Color), "227, 239, 255")]
                    [XmlIgnore()]
                    public Color GradientBegin
                    {
                        get { return _GradientBegin; }
                        set
                        {
                            _GradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientMiddle = Color.FromArgb(203, 225, 252);
                    [DefaultValue(typeof(Color), "203, 225, 252")]
                    [XmlIgnore()]
                    public Color GradientMiddle
                    {
                        get { return _GradientMiddle; }
                        set
                        {
                            _GradientMiddle = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientEnd = Color.FromArgb(123, 164, 224);
                    [DefaultValue(typeof(Color), "123, 164, 224")]
                    [XmlIgnore()]
                    public Color GradientEnd
                    {
                        get { return _GradientEnd; }
                        set
                        {
                            _GradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intGradientBegin
                    {
                        get { return this.GradientBegin.ToArgb(); }
                        set { this.GradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientMiddle
                    {
                        get { return this.GradientMiddle.ToArgb(); }
                        set { this.GradientMiddle = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientEnd
                    {
                        get { return this.GradientEnd.ToArgb(); }
                        set { this.GradientEnd = Color.FromArgb(value); }
                    }

                }

                [Serializable()]
                public class ImageMarginRevealedAppearanceProperties
                {
                    public ImageMarginRevealedAppearanceProperties()
                    {

                        ProfessionalColorTable pct = new ProfessionalColorTable();

                        this._GradientBegin = pct.ImageMarginRevealedGradientBegin;
                        this._GradientEnd = pct.ImageMarginRevealedGradientEnd;
                        this._GradientMiddle = pct.ImageMarginRevealedGradientMiddle;

                    }
                    private AppearanceManager ap;
                    public ImageMarginRevealedAppearanceProperties(AppearanceManager appearanceControl)
                        : this()
                    {
                        ap = appearanceControl;
                    }

                    public void SetAppearanceControl(AppearanceManager appearanceControl)
                    {
                        ap = appearanceControl;
                    }
                    private Color _GradientBegin = Color.FromArgb(203, 221, 246);
                    [DefaultValue(typeof(Color), "203, 221, 246")]
                    [XmlIgnore()]
                    public Color GradientBegin
                    {
                        get { return _GradientBegin; }
                        set
                        {
                            _GradientBegin = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientMiddle = Color.FromArgb(161, 197, 249);
                    [DefaultValue(typeof(Color), "161, 197, 249")]
                    [XmlIgnore()]
                    public Color GradientMiddle
                    {
                        get { return _GradientMiddle; }
                        set
                        {
                            _GradientMiddle = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    private Color _GradientEnd = Color.FromArgb(114, 155, 215);
                    [DefaultValue(typeof(Color), "114, 155, 215")]
                    [XmlIgnore()]
                    public Color GradientEnd
                    {
                        get { return _GradientEnd; }
                        set
                        {
                            _GradientEnd = value;
                            if (ap != null)
                                ap.OnAppearanceChanged(EventArgs.Empty);
                        }
                    }

                    public override string ToString()
                    {
                        return string.Empty;
                    }

                    [Browsable(false)]
                    public int intGradientBegin
                    {
                        get { return this.GradientBegin.ToArgb(); }
                        set { this.GradientBegin = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientMiddle
                    {
                        get { return this.GradientMiddle.ToArgb(); }
                        set { this.GradientMiddle = Color.FromArgb(value); }
                    }

                    [Browsable(false)]
                    public int intGradientEnd
                    {
                        get { return this.GradientEnd.ToArgb(); }
                        set { this.GradientEnd = Color.FromArgb(value); }
                    }

                }

                #endregion

                public override string ToString()
                {
                    return string.Empty;
                }

            }

            public virtual void OnAppearanceChanged(EventArgs e)
            {
                if (AppearanceChanged != null)
                {
                    AppearanceChanged(this, e);
                }
            }

        }

    }

    namespace Renderers.ColorTables
    {

        using Presentation.Windows.Forms.Components;

        public class CustomColorTable : ProfessionalColorTable
        {


            private AppearanceManager ac = null;
            public CustomColorTable(AppearanceManager appearanceControl)
            {
                ac = appearanceControl;
            }

            public override Color ButtonSelectedHighlight
            {
                get { return ac.CustomAppearance.ButtonAppearance.SelectedAppearance.Highlight; }
            }

            public override Color ButtonSelectedHighlightBorder
            {
                get { return ac.CustomAppearance.ButtonAppearance.SelectedAppearance.BorderHighlight; }
            }

            public override Color ButtonPressedHighlight
            {
                get { return ac.CustomAppearance.ButtonAppearance.PressedAppearance.Highlight; }
            }

            public override Color ButtonPressedHighlightBorder
            {
                get { return ac.CustomAppearance.ButtonAppearance.PressedAppearance.BorderHighlight; }
            }

            public override Color ButtonCheckedHighlight
            {
                get { return ac.CustomAppearance.ButtonAppearance.CheckedAppearance.Highlight; }
            }

            public override Color ButtonCheckedHighlightBorder
            {
                get { return ac.CustomAppearance.ButtonAppearance.CheckedAppearance.BorderHighlight; }
            }

            public override Color ButtonPressedBorder
            {
                get { return ac.CustomAppearance.ButtonAppearance.PressedAppearance.Border; }
            }

            public override Color ButtonSelectedBorder
            {
                get { return ac.CustomAppearance.ButtonAppearance.SelectedAppearance.Border; }
            }

            public override Color ButtonCheckedGradientBegin
            {
                get { return ac.CustomAppearance.ButtonAppearance.CheckedAppearance.GradientBegin; }
            }

            public override Color ButtonCheckedGradientMiddle
            {
                get { return ac.CustomAppearance.ButtonAppearance.CheckedAppearance.GradientMiddle; }
            }

            public override Color ButtonCheckedGradientEnd
            {
                get { return ac.CustomAppearance.ButtonAppearance.CheckedAppearance.GradientEnd; }
            }

            public override Color ButtonSelectedGradientBegin
            {
                get { return ac.CustomAppearance.ButtonAppearance.SelectedAppearance.GradientBegin; }
            }

            public override Color ButtonSelectedGradientMiddle
            {
                get { return ac.CustomAppearance.ButtonAppearance.SelectedAppearance.GradientMiddle; }
            }

            public override Color ButtonSelectedGradientEnd
            {
                get { return ac.CustomAppearance.ButtonAppearance.SelectedAppearance.GradientEnd; }
            }

            public override Color ButtonPressedGradientBegin
            {
                get { return ac.CustomAppearance.ButtonAppearance.PressedAppearance.GradientBegin; }
            }

            public override Color ButtonPressedGradientMiddle
            {
                get { return ac.CustomAppearance.ButtonAppearance.PressedAppearance.GradientMiddle; }
            }

            public override Color ButtonPressedGradientEnd
            {
                get { return ac.CustomAppearance.ButtonAppearance.PressedAppearance.GradientEnd; }
            }

            public override Color CheckBackground
            {
                get { return ac.CustomAppearance.ButtonAppearance.CheckedAppearance.Background; }
            }

            public override Color CheckSelectedBackground
            {
                get { return ac.CustomAppearance.ButtonAppearance.CheckedAppearance.SelectedBackground; }
            }

            public override Color CheckPressedBackground
            {
                get { return ac.CustomAppearance.ButtonAppearance.CheckedAppearance.PressedBackground; }
            }

            public override Color GripDark
            {
                get { return ac.CustomAppearance.GripAppearance.Dark; }
            }

            public override Color GripLight
            {
                get { return ac.CustomAppearance.GripAppearance.Light; }
            }

            public override Color ImageMarginGradientBegin
            {
                get { return ac.CustomAppearance.ImageMarginAppearance.Normal.GradientBegin; }
            }

            public override Color ImageMarginGradientMiddle
            {
                get { return ac.CustomAppearance.ImageMarginAppearance.Normal.GradientMiddle; }
            }

            public override Color ImageMarginGradientEnd
            {
                get { return ac.CustomAppearance.ImageMarginAppearance.Normal.GradientEnd; }
            }

            public override Color ImageMarginRevealedGradientBegin
            {
                get { return ac.CustomAppearance.ImageMarginAppearance.Revealed.GradientBegin; }
            }

            public override Color ImageMarginRevealedGradientMiddle
            {
                get { return ac.CustomAppearance.ImageMarginAppearance.Revealed.GradientMiddle; }
            }

            public override Color ImageMarginRevealedGradientEnd
            {
                get { return ac.CustomAppearance.ImageMarginAppearance.Revealed.GradientEnd; }
            }

            public override Color MenuStripGradientBegin
            {
                get { return ac.CustomAppearance.MenuStripAppearance.GradientBegin; }
            }

            public override Color MenuStripGradientEnd
            {
                get { return ac.CustomAppearance.MenuStripAppearance.GradientEnd; }
            }

            public override Color MenuItemSelected
            {
                get { return ac.CustomAppearance.MenuItemAppearance.Selected; }
            }

            public override Color MenuItemBorder
            {
                get { return ac.CustomAppearance.MenuItemAppearance.Border; }
            }

            public override Color MenuBorder
            {
                get { return ac.CustomAppearance.MenuStripAppearance.Border; }
            }

            public override Color MenuItemSelectedGradientBegin
            {
                get { return ac.CustomAppearance.MenuItemAppearance.SelectedGradientBegin; }
            }

            public override Color MenuItemSelectedGradientEnd
            {
                get { return ac.CustomAppearance.MenuItemAppearance.SelectedGradientEnd; }
            }

            public override Color MenuItemPressedGradientBegin
            {
                get { return ac.CustomAppearance.MenuItemAppearance.PressedGradientBegin; }
            }

            public override Color MenuItemPressedGradientMiddle
            {
                get { return ac.CustomAppearance.MenuItemAppearance.PressedGradientMiddle; }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get { return ac.CustomAppearance.MenuItemAppearance.PressedGradientEnd; }
            }

            public override Color RaftingContainerGradientBegin
            {
                get { return ac.CustomAppearance.RaftingContainerAppearance.GradientBegin; }
            }

            public override Color RaftingContainerGradientEnd
            {
                get { return ac.CustomAppearance.RaftingContainerAppearance.GradientEnd; }
            }

            public override Color SeparatorDark
            {
                get { return ac.CustomAppearance.SeparatorAppearance.Dark; }
            }

            public override Color SeparatorLight
            {
                get { return ac.CustomAppearance.SeparatorAppearance.Light; }
            }

            public override Color StatusStripGradientBegin
            {
                get { return ac.CustomAppearance.StatusStripAppearance.GradientBegin; }
            }

            public override Color StatusStripGradientEnd
            {
                get { return ac.CustomAppearance.StatusStripAppearance.GradientEnd; }
            }

            public override Color ToolStripBorder
            {
                get { return ac.CustomAppearance.ToolStripAppearance.Border; }
            }

            public override Color ToolStripDropDownBackground
            {
                get { return ac.CustomAppearance.ToolStripAppearance.DropDownBackground; }
            }

            public override Color ToolStripGradientBegin
            {
                get { return ac.CustomAppearance.ToolStripAppearance.GradientBegin; }
            }

            public override Color ToolStripGradientMiddle
            {
                get { return ac.CustomAppearance.ToolStripAppearance.GradientMiddle; }
            }

            public override Color ToolStripGradientEnd
            {
                get { return ac.CustomAppearance.ToolStripAppearance.GradientEnd; }
            }

            public override Color ToolStripContentPanelGradientBegin
            {
                get { return ac.CustomAppearance.ToolStripAppearance.ContentPanelGradientBegin; }
            }

            public override Color ToolStripContentPanelGradientEnd
            {
                get { return ac.CustomAppearance.ToolStripAppearance.ContentPanelGradientEnd; }
            }

            public override Color ToolStripPanelGradientBegin
            {
                get { return ac.CustomAppearance.ToolStripAppearance.PanelGradientBegin; }
            }

            public override Color ToolStripPanelGradientEnd
            {
                get { return ac.CustomAppearance.ToolStripAppearance.PanelGradientEnd; }
            }

            public override Color OverflowButtonGradientBegin
            {
                get { return ac.CustomAppearance.OverflowButtonAppearance.GradientBegin; }
            }

            public override Color OverflowButtonGradientMiddle
            {
                get { return ac.CustomAppearance.OverflowButtonAppearance.GradientMiddle; }
            }

            public override Color OverflowButtonGradientEnd
            {
                get { return ac.CustomAppearance.OverflowButtonAppearance.GradientEnd; }
            }

        }


    }
}


