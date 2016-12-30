using System.ComponentModel.Design;
using System.Drawing.Drawing2D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Components
{
    
    [ProvideProperty("Image", typeof(MenuItem))]
    public sealed partial class VistaMenu : Component, IExtenderProvider, ISupportInitialize
    {
        Container components;
        readonly Hashtable properties = new Hashtable();
        readonly Hashtable menuParents = new Hashtable();

        bool formHasBeenIntialized;
        readonly bool isVistaOrLater;
        

        public VistaMenu()
        {
            isVistaOrLater = Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6;

            InitializeComponent();
        }

        public VistaMenu(IContainer container)
            : this()
        {
            container.Add(this);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent()
        {
            components = new Container();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //release all the HBitmap handles created
                foreach (DictionaryEntry de in properties)
                {
                    if (((Properties)de.Value).renderBmpHbitmap != IntPtr.Zero)
                        NativeMethods.DeleteObject(((Properties)de.Value).renderBmpHbitmap);
                }


                if (components != null)
                    components.Dispose();
            }

            base.Dispose(disposing);
        }

        bool IExtenderProvider.CanExtend(object o)
        {
            if (o is MenuItem)
            {
                // reject the menuitem if it's a top level element on a MainMenu bar
                if (((MenuItem)o).Parent != null)
                    return ((MenuItem)o).Parent.GetType() != typeof(MainMenu);

                // parent is null - meaning it's a context menu
                return true;
            }

            if (o is Form)
                return true;

            return false;
        }

        Properties EnsurePropertiesExists(MenuItem key)
        {
            Properties p = (Properties)properties[key];

            if (p == null)
            {
                p = new Properties();

                properties[key] = p;
            }

            return p;
        }


        [DefaultValue(null)]
        [Description("The Image for the MenuItem")]
        [Category("Appearance")]
        public Image GetImage(MenuItem mnuItem)
        {
            return EnsurePropertiesExists(mnuItem).Image;
        }

        [DefaultValue(null)]
        public void SetImage(MenuItem mnuItem, Image value)
        {
            Properties prop = EnsurePropertiesExists(mnuItem);

            prop.Image = value;

            if (!DesignMode && isVistaOrLater)
            {
                //Destroy old bitmap object
                if (prop.renderBmpHbitmap != IntPtr.Zero)
                {
                    NativeMethods.DeleteObject(prop.renderBmpHbitmap);
                    prop.renderBmpHbitmap = IntPtr.Zero;
                }

                //if there's no Image, then just bail out
                if (value == null)
                    return;

                //convert to 32bppPArgb (the 'P' means The red, green, and blue components are premultiplied, according to the alpha component.)
                using (Bitmap renderBmp = new Bitmap(value.Width, value.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
                {
                    using (Graphics g = Graphics.FromImage(renderBmp))
                        g.DrawImage(value, 0, 0, value.Width, value.Height);

                    prop.renderBmpHbitmap = renderBmp.GetHbitmap(Color.FromArgb(0, 0, 0, 0));
                }

                if (formHasBeenIntialized)
                    AddVistaMenuItem(mnuItem);
            }


            //for every Pre-Vista Windows, add the parent of the menu item to the list of parents
            if (!DesignMode && !isVistaOrLater && formHasBeenIntialized)
            {
                AddPreVistaMenuItem(mnuItem);
            }
        }



        void ISupportInitialize.BeginInit() { }

        readonly NativeMethods.MENUINFO mnuInfo = new NativeMethods.MENUINFO();

        void AddVistaMenuItem(MenuItem mnuItem)
        {
            //get the bitmap children of the parent
            if (menuParents[mnuItem.Parent] == null)
            {
                if (mnuItem.Parent.GetType() == typeof(ContextMenu))
                    ((ContextMenu)mnuItem.Parent).Popup += MenuItem_Popup;
                else
                    ((MenuItem)mnuItem.Parent).Popup += MenuItem_Popup;

                //intialize all the topmost menus to be of type "MNS_CHECKORBMP" (for Vista classic theme)
                NativeMethods.SetMenuInfo(new HandleRef(null, mnuItem.Parent.Handle), mnuInfo);

                menuParents[mnuItem.Parent] = true;
            }
        }

        void AddPreVistaMenuItem(MenuItem mnuItem)
        {
            if (menuParents[mnuItem.Parent] == null)
            {
                menuParents[mnuItem.Parent] = true;

                if (formHasBeenIntialized)
                {
                    //add all the menu items with custom paint events
                    foreach (MenuItem menu in mnuItem.Parent.MenuItems)
                    {
                        menu.DrawItem += MenuItem_DrawItem;
                        menu.MeasureItem += MenuItem_MeasureItem;
                        menu.OwnerDraw = true;
                    }
                }
            }
        }

        void ISupportInitialize.EndInit()
        {
            if (!DesignMode)
            {
                if (isVistaOrLater)
                {
                    foreach (DictionaryEntry de in properties)
                    {
                        AddVistaMenuItem((MenuItem)de.Key);
                    }
                }
                else // Pre-Vista menus
                {
                    // Declare the fonts once: 
                    //    If the user changes the menu fonts while your program is 
                    //    running, it's tough luck for the user.
                    //
                    //    This keeps a cap on the memory by avoiding unnecessary Font object 
                    //    creation/destruction on every MenuItem .Measure() and .Draw()
                    menuBoldFont = new Font(SystemFonts.MenuFont, FontStyle.Bold);


                    if (ownerForm != null)
                        ownerForm.ChangeUICues += ownerForm_ChangeUICues;

                    foreach (DictionaryEntry de in properties)
                    {
                        AddPreVistaMenuItem((MenuItem)de.Key);
                    }

                    //add event handle for each menu item's measure & draw routines
                    foreach (DictionaryEntry parent in menuParents)
                    {
                        foreach (MenuItem mnuItem in ((Menu)parent.Key).MenuItems)
                        {
                            mnuItem.DrawItem += MenuItem_DrawItem;
                            mnuItem.MeasureItem += MenuItem_MeasureItem;
                            mnuItem.OwnerDraw = true;
                        }
                    }
                }

                formHasBeenIntialized = true;
            }
        }

        void MenuItem_Popup(object sender, EventArgs e)
        {
            NativeMethods.MENUITEMINFO_T_RW menuItemInfo = new NativeMethods.MENUITEMINFO_T_RW();

            // get the menu items collection
            Menu.MenuItemCollection mi = sender.GetType() == typeof(ContextMenu) ? ((ContextMenu)sender).MenuItems : ((MenuItem)sender).MenuItems;

            // we have to track the menuPosition ourselves
            // because MenuItem.Index is only correct when
            // all the menu items are visible.
            int miOn = 0;
            for (int i = 0; i < mi.Count; i++)
            {
                if (mi[i].Visible)
                {
                    Properties p = ((Properties)properties[mi[i]]);

                    if (p != null)
                    {
                        menuItemInfo.hbmpItem = p.renderBmpHbitmap;

                        //refresh the menu item where ((Menu)sender).Handle is the parent handle
                        NativeMethods.SetMenuItemInfo(new HandleRef(null, ((Menu)sender).Handle),
                                        miOn,
                                        true,
                                        menuItemInfo);
                    }

                    miOn++;
                }
            }
        }
    }


    //Properties for the MenuItem
    internal class Properties
    {
        public Image Image;
        public IntPtr renderBmpHbitmap = IntPtr.Zero;
    }


    public partial class VistaMenu
    {
        ContainerControl ownerForm;

        //conditionally draw the little lines under menu items with keyboard accelators on Win 2000+
        private bool isUsingKeyboardAccel;


        static Font menuBoldFont;

        public VistaMenu(ContainerControl parentControl)
            : this()
        {
            ownerForm = parentControl;
        }
        public ContainerControl ContainerControl
        {
            get { return ownerForm; }
            set { ownerForm = value; }
        }
        public override ISite Site
        {
            set
            {
                // Runs at design time, ensures designer initializes ContainerControl
                base.Site = value;
                if (value == null) return;
                IDesignerHost service = value.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (service == null) return;
                IComponent rootComponent = service.RootComponent;
                ContainerControl = rootComponent as ContainerControl;
            }
        }


        void ownerForm_ChangeUICues(object sender, UICuesEventArgs e)
        {
            isUsingKeyboardAccel = e.ShowKeyboard;
        }


        const int SEPARATOR_HEIGHT = 9;
        const int BORDER_VERTICAL = 4;
        const int LEFT_MARGIN = 4;
        const int RIGHT_MARGIN = 6;
        const int SHORTCUT_MARGIN = 20;
        const int ARROW_MARGIN = 12;
        const int ICON_SIZE = 16;


        static void MenuItem_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            Font font = ((MenuItem)sender).DefaultItem
                            ? menuBoldFont
                            : SystemFonts.MenuFont;

            if (((MenuItem)sender).Text == "-")
                e.ItemHeight = SEPARATOR_HEIGHT;
            else
            {
                e.ItemHeight = ((SystemFonts.MenuFont.Height > ICON_SIZE) ? SystemFonts.MenuFont.Height : ICON_SIZE)
                                + BORDER_VERTICAL;

                e.ItemWidth = LEFT_MARGIN + ICON_SIZE + RIGHT_MARGIN

                    //item text width
                    + TextRenderer.MeasureText(((MenuItem)sender).Text, font, Size.Empty, TextFormatFlags.SingleLine | TextFormatFlags.NoClipping).Width
                    + SHORTCUT_MARGIN

                    //shortcut text width
                    + TextRenderer.MeasureText(ShortcutToString(((MenuItem)sender).Shortcut), font, Size.Empty, TextFormatFlags.SingleLine | TextFormatFlags.NoClipping).Width

                    //arrow width
                    + ((((MenuItem)sender).IsParent) ? ARROW_MARGIN : 0);
            }
        }

        void MenuItem_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
            e.Graphics.InterpolationMode = InterpolationMode.Low;

            bool menuSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            if (menuSelected)
                e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
            else
                e.Graphics.FillRectangle(SystemBrushes.Menu, e.Bounds);

            if (((MenuItem)sender).Text == "-")
            {
                //draw the separator
                int yCenter = e.Bounds.Top + (e.Bounds.Height / 2) - 1;

                e.Graphics.DrawLine(SystemPens.ControlDark, e.Bounds.Left + 1, yCenter, (e.Bounds.Left + e.Bounds.Width - 2), yCenter);
                e.Graphics.DrawLine(SystemPens.ControlLightLight, e.Bounds.Left + 1, yCenter + 1, (e.Bounds.Left + e.Bounds.Width - 2), yCenter + 1);
            }
            else //regular menu items
            {
                //draw the item text
                DrawText(sender, e, menuSelected);

                if (((MenuItem)sender).Checked)
                {
                    if (((MenuItem)sender).RadioCheck)
                    {
                        //draw the bullet
                        ControlPaint.DrawMenuGlyph(e.Graphics,
                            e.Bounds.Left + (LEFT_MARGIN + ICON_SIZE + RIGHT_MARGIN - SystemInformation.MenuCheckSize.Width) / 2,
                            e.Bounds.Top + (e.Bounds.Height - SystemInformation.MenuCheckSize.Height) / 2 + 1,
                            SystemInformation.MenuCheckSize.Width,
                            SystemInformation.MenuCheckSize.Height,
                            MenuGlyph.Bullet,
                            menuSelected ? SystemColors.HighlightText : SystemColors.MenuText,
                            menuSelected ? SystemColors.Highlight : SystemColors.Menu);
                    }
                    else
                    {
                        //draw the check mark
                        ControlPaint.DrawMenuGlyph(e.Graphics,
                            e.Bounds.Left + (LEFT_MARGIN + ICON_SIZE + RIGHT_MARGIN - SystemInformation.MenuCheckSize.Width) / 2,
                            e.Bounds.Top + (e.Bounds.Height - SystemInformation.MenuCheckSize.Height) / 2 + 1,
                            SystemInformation.MenuCheckSize.Width,
                            SystemInformation.MenuCheckSize.Height,
                            MenuGlyph.Checkmark,
                            menuSelected ? SystemColors.HighlightText : SystemColors.MenuText,
                            menuSelected ? SystemColors.Highlight : SystemColors.Menu);
                    }
                }
                else
                {
                    Image drawImg = GetImage(((MenuItem)sender));

                    if (drawImg != null)
                    {
                        //draw the image
                        if (((MenuItem)sender).Enabled)
                            e.Graphics.DrawImage(drawImg, e.Bounds.Left + LEFT_MARGIN,
                                e.Bounds.Top + ((e.Bounds.Height - ICON_SIZE) / 2),
                                ICON_SIZE, ICON_SIZE);
                        else
                            ControlPaint.DrawImageDisabled(e.Graphics, drawImg,
                                e.Bounds.Left + LEFT_MARGIN,
                                e.Bounds.Top + ((e.Bounds.Height - ICON_SIZE) / 2),
                                SystemColors.Menu);
                    }
                }
            }
        }


        private static string ShortcutToString(Shortcut shortcut)
        {
            if (shortcut != Shortcut.None)
            {
                Keys keys = (Keys)shortcut;
                return TypeDescriptor.GetConverter(keys.GetType()).ConvertToString(keys);
            }

            return null;
        }

        private void DrawText(object sender, DrawItemEventArgs e, bool isSelected)
        {
            string shortcutText = ShortcutToString(((MenuItem)sender).Shortcut);

            int yPos = e.Bounds.Top + (e.Bounds.Height - SystemFonts.MenuFont.Height) / 2;

            Font font = ((MenuItem)sender).DefaultItem
                ? menuBoldFont
                : SystemFonts.MenuFont;

            Size textSize = TextRenderer.MeasureText(((MenuItem)sender).Text,
                                  font, Size.Empty, TextFormatFlags.SingleLine | TextFormatFlags.NoClipping);

            Rectangle textRect = new Rectangle(e.Bounds.Left + LEFT_MARGIN + ICON_SIZE + RIGHT_MARGIN, yPos,
                                   textSize.Width, textSize.Height);

            if (!((MenuItem)sender).Enabled && !isSelected) // disabled and not selected
            {
                textRect.Offset(1, 1);

                TextRenderer.DrawText(e.Graphics, ((MenuItem)sender).Text, font,
                    textRect,
                    SystemColors.ControlLightLight,
                    TextFormatFlags.SingleLine | (isUsingKeyboardAccel ? 0 : TextFormatFlags.HidePrefix) | TextFormatFlags.NoClipping);

                textRect.Offset(-1, -1);
            }

            //Draw the menu item text
            TextRenderer.DrawText(e.Graphics, ((MenuItem)sender).Text, font,
                textRect,
                ((MenuItem)sender).Enabled ? (isSelected ? SystemColors.HighlightText : SystemColors.MenuText) : SystemColors.GrayText,
                TextFormatFlags.SingleLine | (isUsingKeyboardAccel ? 0 : TextFormatFlags.HidePrefix) | TextFormatFlags.NoClipping);



            //Draw the shortcut text
            if (shortcutText != null)
            {
                textSize = TextRenderer.MeasureText(shortcutText,
                                  font, Size.Empty, TextFormatFlags.SingleLine | TextFormatFlags.NoClipping);


                textRect = new Rectangle(e.Bounds.Width - textSize.Width - ARROW_MARGIN, yPos, textSize.Width,
                                         textSize.Height);

                if (!((MenuItem)sender).Enabled && !isSelected) // disabled and not selected
                {
                    textRect.Offset(1, 1);

                    TextRenderer.DrawText(e.Graphics, shortcutText, font,
                        textRect,
                        SystemColors.ControlLightLight,
                        TextFormatFlags.SingleLine | (isUsingKeyboardAccel ? 0 : TextFormatFlags.HidePrefix) | TextFormatFlags.NoClipping);

                    textRect.Offset(-1, -1);
                }

                TextRenderer.DrawText(e.Graphics, shortcutText, font,
                    textRect,
                    ((MenuItem)sender).Enabled ? (isSelected ? SystemColors.HighlightText : SystemColors.MenuText) : SystemColors.GrayText,
                    TextFormatFlags.SingleLine | TextFormatFlags.NoClipping);
            }
        }
    }

}