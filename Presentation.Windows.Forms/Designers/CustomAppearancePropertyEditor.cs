using Platform.Presentation.Windows.Forms.Components;
using Platform.Presentation.Windows.Forms.Customs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml.Serialization;

namespace Platform.Presentation.Windows.Forms.Designers
{   

    public class AppearanceEditor : System.Windows.Forms.Form
    {

        #region Designer


        //Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        //Required by the Windows Form Designer

        private System.ComponentModel.IContainer components = null;
        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.OK_Button = new System.Windows.Forms.Button();
            this.TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Button1 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.TableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.Load_Button = new System.Windows.Forms.Button();
            this.Save_Button = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.CustomizableToolStrip1 = new Presentation.Windows.Forms.Customs.ToolStrip();
            this.AppearanceControl1 = new Presentation.Windows.Forms.Components.AppearanceManager();
            this.NewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.OpenToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.SaveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.PrintToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.CutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CopyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.PasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.HelpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.NewToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.OpenToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.SaveToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.PrintToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.CutToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.CopyToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.PasteToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.HelpToolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.CustomizableStatusStrip1 = new Presentation.Windows.Forms.Customs.StatusStrip();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.ToolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.MenuItem3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItem2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomizableMenuStrip1 = new Presentation.Windows.Forms.Customs.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.PrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UndoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RedoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.SelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblPreview = new System.Windows.Forms.Label();
            this.PropertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.TableLayoutPanel1.SuspendLayout();
            this.TableLayoutPanel2.SuspendLayout();
            this.TableLayoutPanel3.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.CustomizableToolStrip1.SuspendLayout();
            this.CustomizableStatusStrip1.SuspendLayout();
            this.CustomizableMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanel1
            // 
            this.TableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel1.ColumnCount = 1;
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.Controls.Add(this.OK_Button, 0, 0);
            this.TableLayoutPanel1.Location = new System.Drawing.Point(303, 396);
            this.TableLayoutPanel1.Name = "TableLayoutPanel1";
            this.TableLayoutPanel1.RowCount = 1;
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.TableLayoutPanel1.Size = new System.Drawing.Size(73, 29);
            this.TableLayoutPanel1.TabIndex = 0;
            // 
            // OK_Button
            // 
            this.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OK_Button.Location = new System.Drawing.Point(3, 3);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(67, 23);
            this.OK_Button.TabIndex = 0;
            this.OK_Button.Text = "OK";
            this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
            // 
            // TableLayoutPanel2
            // 
            this.TableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TableLayoutPanel2.ColumnCount = 2;
            this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel2.Controls.Add(this.Button1, 0, 0);
            this.TableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.TableLayoutPanel2.Name = "TableLayoutPanel2";
            this.TableLayoutPanel2.RowCount = 1;
            this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.TableLayoutPanel2.TabIndex = 0;
            // 
            // Button1
            // 
            this.Button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button1.Location = new System.Drawing.Point(16, 38);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(67, 23);
            this.Button1.TabIndex = 0;
            this.Button1.Text = "OK";
            // 
            // Button2
            // 
            this.Button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button2.Location = new System.Drawing.Point(36, 3);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(28, 8);
            this.Button2.TabIndex = 1;
            this.Button2.Text = "Cancel";
            // 
            // TableLayoutPanel3
            // 
            this.TableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TableLayoutPanel3.ColumnCount = 2;
            this.TableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel3.Controls.Add(this.Load_Button, 0, 0);
            this.TableLayoutPanel3.Controls.Add(this.Save_Button, 1, 0);
            this.TableLayoutPanel3.Location = new System.Drawing.Point(12, 396);
            this.TableLayoutPanel3.Name = "TableLayoutPanel3";
            this.TableLayoutPanel3.RowCount = 1;
            this.TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanel3.Size = new System.Drawing.Size(146, 29);
            this.TableLayoutPanel3.TabIndex = 1;
            // 
            // Load_Button
            // 
            this.Load_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Load_Button.Location = new System.Drawing.Point(3, 3);
            this.Load_Button.Name = "Load_Button";
            this.Load_Button.Size = new System.Drawing.Size(67, 23);
            this.Load_Button.TabIndex = 0;
            this.Load_Button.Text = "Load";
            this.Load_Button.Click += new System.EventHandler(this.Load_Button_Click);
            // 
            // Save_Button
            // 
            this.Save_Button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Save_Button.Location = new System.Drawing.Point(76, 3);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(67, 23);
            this.Save_Button.TabIndex = 1;
            this.Save_Button.Text = "Save";
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.CustomizableToolStrip1);
            this.Panel1.Controls.Add(this.CustomizableStatusStrip1);
            this.Panel1.Controls.Add(this.CustomizableMenuStrip1);
            this.Panel1.Location = new System.Drawing.Point(15, 25);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(361, 88);
            this.Panel1.TabIndex = 2;
            // 
            // CustomizableToolStrip1
            // 
            this.CustomizableToolStrip1.Appearance = this.AppearanceControl1;
            this.CustomizableToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripButton,
            this.OpenToolStripButton,
            this.SaveToolStripButton,
            this.PrintToolStripButton,
            this.toolStripSeparator6,
            this.CutToolStripButton,
            this.CopyToolStripButton,
            this.PasteToolStripButton,
            this.toolStripSeparator7,
            this.HelpToolStripButton,
            this.NewToolStripButton1,
            this.OpenToolStripButton1,
            this.SaveToolStripButton1,
            this.PrintToolStripButton1,
            this.toolStripSeparator8,
            this.CutToolStripButton1,
            this.CopyToolStripButton1,
            this.PasteToolStripButton1,
            this.toolStripSeparator9,
            this.HelpToolStripButton1});
            this.CustomizableToolStrip1.Location = new System.Drawing.Point(0, 24);
            this.CustomizableToolStrip1.Name = "CustomizableToolStrip1";
            this.CustomizableToolStrip1.RoundedEdges = true;
            this.CustomizableToolStrip1.Size = new System.Drawing.Size(359, 25);
            this.CustomizableToolStrip1.TabIndex = 2;
            this.CustomizableToolStrip1.Text = "CustomizableToolStrip1";
            // 
            // AppearanceControl1
            // 
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.CheckedAppearance.intBackground = -16273;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.CheckedAppearance.intBorderHighlight = -13410648;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.CheckedAppearance.intGradientBegin = -8294;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.CheckedAppearance.intGradientEnd = -22964;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.CheckedAppearance.intGradientMiddle = -15500;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.CheckedAppearance.intHighlight = -3878683;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.CheckedAppearance.intPressedBackground = -98242;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.CheckedAppearance.intSelectedBackground = -98242;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.PressedAppearance.Border = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.PressedAppearance.intBorder = -16777088;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.PressedAppearance.intBorderHighlight = -13410648;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.PressedAppearance.intGradientBegin = -98242;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.PressedAppearance.intGradientEnd = -8294;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.PressedAppearance.intGradientMiddle = -20115;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.PressedAppearance.intHighlight = -6771246;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.SelectedAppearance.Border = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.SelectedAppearance.BorderHighlight = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.SelectedAppearance.intBorder = -16777088;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.SelectedAppearance.intBorderHighlight = -16777088;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.SelectedAppearance.intGradientBegin = -34;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.SelectedAppearance.intGradientEnd = -13432;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.SelectedAppearance.intGradientMiddle = -7764;
            this.AppearanceControl1.CustomAppearance.ButtonAppearance.SelectedAppearance.intHighlight = -3878683;
            this.AppearanceControl1.CustomAppearance.GripAppearance.intDark = -14204554;
            this.AppearanceControl1.CustomAppearance.GripAppearance.intLight = -1;
            this.AppearanceControl1.CustomAppearance.GripAppearance.Light = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AppearanceControl1.CustomAppearance.ImageMarginAppearance.Normal.intGradientBegin = -1839105;
            this.AppearanceControl1.CustomAppearance.ImageMarginAppearance.Normal.intGradientEnd = -8674080;
            this.AppearanceControl1.CustomAppearance.ImageMarginAppearance.Normal.intGradientMiddle = -3415556;
            this.AppearanceControl1.CustomAppearance.ImageMarginAppearance.Revealed.intGradientBegin = -3416586;
            this.AppearanceControl1.CustomAppearance.ImageMarginAppearance.Revealed.intGradientEnd = -9266217;
            this.AppearanceControl1.CustomAppearance.ImageMarginAppearance.Revealed.intGradientMiddle = -6175239;
            this.AppearanceControl1.CustomAppearance.MenuItemAppearance.Border = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            this.AppearanceControl1.CustomAppearance.MenuItemAppearance.intBorder = -16777088;
            this.AppearanceControl1.CustomAppearance.MenuItemAppearance.intPressedGradientBegin = -1839105;
            this.AppearanceControl1.CustomAppearance.MenuItemAppearance.intPressedGradientEnd = -8674080;
            this.AppearanceControl1.CustomAppearance.MenuItemAppearance.intPressedGradientMiddle = -6175239;
            this.AppearanceControl1.CustomAppearance.MenuItemAppearance.intSelected = -4414;
            this.AppearanceControl1.CustomAppearance.MenuItemAppearance.intSelectedGradientBegin = -34;
            this.AppearanceControl1.CustomAppearance.MenuItemAppearance.intSelectedGradientEnd = -13432;
            this.AppearanceControl1.CustomAppearance.MenuStripAppearance.intBorder = -16765546;
            this.AppearanceControl1.CustomAppearance.MenuStripAppearance.intGradientBegin = -6373643;
            this.AppearanceControl1.CustomAppearance.MenuStripAppearance.intGradientEnd = -3876102;
            this.AppearanceControl1.CustomAppearance.OverflowButtonAppearance.intGradientBegin = -8408582;
            this.AppearanceControl1.CustomAppearance.OverflowButtonAppearance.intGradientEnd = -16763503;
            this.AppearanceControl1.CustomAppearance.OverflowButtonAppearance.intGradientMiddle = -11370544;
            this.AppearanceControl1.CustomAppearance.RaftingContainerAppearance.intGradientBegin = -6373643;
            this.AppearanceControl1.CustomAppearance.RaftingContainerAppearance.intGradientEnd = -3876102;
            this.AppearanceControl1.CustomAppearance.SeparatorAppearance.intDark = -9794357;
            this.AppearanceControl1.CustomAppearance.SeparatorAppearance.intLight = -919041;
            this.AppearanceControl1.CustomAppearance.StatusStripAppearance.intGradientBegin = -6373643;
            this.AppearanceControl1.CustomAppearance.StatusStripAppearance.intGradientEnd = -3876102;
            this.AppearanceControl1.CustomAppearance.ToolStripAppearance.intBorder = -12885604;
            this.AppearanceControl1.CustomAppearance.ToolStripAppearance.intContentPanelGradientBegin = -6373643;
            this.AppearanceControl1.CustomAppearance.ToolStripAppearance.intContentPanelGradientEnd = -3876102;
            this.AppearanceControl1.CustomAppearance.ToolStripAppearance.intDropDownBackground = -592138;
            this.AppearanceControl1.CustomAppearance.ToolStripAppearance.intGradientBegin = -1839105;
            this.AppearanceControl1.CustomAppearance.ToolStripAppearance.intGradientEnd = -8674080;
            this.AppearanceControl1.CustomAppearance.ToolStripAppearance.intGradientMiddle = -3415556;
            this.AppearanceControl1.CustomAppearance.ToolStripAppearance.intPanelGradientBegin = -6373643;
            this.AppearanceControl1.CustomAppearance.ToolStripAppearance.intPanelGradientEnd = -3876102;
            this.AppearanceControl1.Preset = Presentation.Windows.Forms.Components.AppearanceManager.enumPresetStyles.Custom;
            this.AppearanceControl1.Renderer.RoundedEdges = true;
            // 
            // NewToolStripButton
            // 
            this.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewToolStripButton.Name = "NewToolStripButton";
            this.NewToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.NewToolStripButton.Text = "&New";
            // 
            // OpenToolStripButton
            // 
            this.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenToolStripButton.Name = "OpenToolStripButton";
            this.OpenToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.OpenToolStripButton.Text = "&Open";
            // 
            // SaveToolStripButton
            // 
            this.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripButton.Name = "SaveToolStripButton";
            this.SaveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.SaveToolStripButton.Text = "&Save";
            // 
            // PrintToolStripButton
            // 
            this.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintToolStripButton.Name = "PrintToolStripButton";
            this.PrintToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.PrintToolStripButton.Text = "&Print";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // CutToolStripButton
            // 
            this.CutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CutToolStripButton.Name = "CutToolStripButton";
            this.CutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.CutToolStripButton.Text = "C&ut";
            // 
            // CopyToolStripButton
            // 
            this.CopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CopyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyToolStripButton.Name = "CopyToolStripButton";
            this.CopyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.CopyToolStripButton.Text = "&Copy";
            // 
            // PasteToolStripButton
            // 
            this.PasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PasteToolStripButton.Name = "PasteToolStripButton";
            this.PasteToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.PasteToolStripButton.Text = "&Paste";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // HelpToolStripButton
            // 
            this.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HelpToolStripButton.Name = "HelpToolStripButton";
            this.HelpToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.HelpToolStripButton.Text = "He&lp";
            // 
            // NewToolStripButton1
            // 
            this.NewToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewToolStripButton1.Name = "NewToolStripButton1";
            this.NewToolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.NewToolStripButton1.Text = "&New";
            // 
            // OpenToolStripButton1
            // 
            this.OpenToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenToolStripButton1.Name = "OpenToolStripButton1";
            this.OpenToolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.OpenToolStripButton1.Text = "&Open";
            // 
            // SaveToolStripButton1
            // 
            this.SaveToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripButton1.Name = "SaveToolStripButton1";
            this.SaveToolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.SaveToolStripButton1.Text = "&Save";
            // 
            // PrintToolStripButton1
            // 
            this.PrintToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintToolStripButton1.Name = "PrintToolStripButton1";
            this.PrintToolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.PrintToolStripButton1.Text = "&Print";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // CutToolStripButton1
            // 
            this.CutToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CutToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CutToolStripButton1.Name = "CutToolStripButton1";
            this.CutToolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.CutToolStripButton1.Text = "C&ut";
            // 
            // CopyToolStripButton1
            // 
            this.CopyToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CopyToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyToolStripButton1.Name = "CopyToolStripButton1";
            this.CopyToolStripButton1.Size = new System.Drawing.Size(23, 4);
            this.CopyToolStripButton1.Text = "&Copy";
            // 
            // PasteToolStripButton1
            // 
            this.PasteToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PasteToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PasteToolStripButton1.Name = "PasteToolStripButton1";
            this.PasteToolStripButton1.Size = new System.Drawing.Size(23, 4);
            this.PasteToolStripButton1.Text = "&Paste";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 6);
            // 
            // HelpToolStripButton1
            // 
            this.HelpToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HelpToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HelpToolStripButton1.Name = "HelpToolStripButton1";
            this.HelpToolStripButton1.Size = new System.Drawing.Size(23, 4);
            this.HelpToolStripButton1.Text = "He&lp";
            // 
            // CustomizableStatusStrip1
            // 
            this.CustomizableStatusStrip1.Appearance = this.AppearanceControl1;
            this.CustomizableStatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel1,
            this.ToolStripProgressBar1,
            this.ToolStripStatusLabel2,
            this.ToolStripDropDownButton1});
            this.CustomizableStatusStrip1.Location = new System.Drawing.Point(0, 64);
            this.CustomizableStatusStrip1.Name = "CustomizableStatusStrip1";
            this.CustomizableStatusStrip1.Size = new System.Drawing.Size(359, 22);
            this.CustomizableStatusStrip1.TabIndex = 1;
            this.CustomizableStatusStrip1.Text = "CustomizableStatusStrip1";
            // 
            // ToolStripStatusLabel1
            // 
            this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            this.ToolStripStatusLabel1.Size = new System.Drawing.Size(35, 17);
            this.ToolStripStatusLabel1.Text = "Label";
            // 
            // ToolStripProgressBar1
            // 
            this.ToolStripProgressBar1.Name = "ToolStripProgressBar1";
            this.ToolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.ToolStripProgressBar1.Value = 50;
            // 
            // ToolStripStatusLabel2
            // 
            this.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2";
            this.ToolStripStatusLabel2.Size = new System.Drawing.Size(151, 17);
            this.ToolStripStatusLabel2.Spring = true;
            // 
            // ToolStripDropDownButton1
            // 
            this.ToolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem3ToolStripMenuItem,
            this.ToolStripMenuItem1,
            this.MenuItem2ToolStripMenuItem,
            this.MenuItem1ToolStripMenuItem});
            this.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1";
            this.ToolStripDropDownButton1.Size = new System.Drawing.Size(56, 20);
            this.ToolStripDropDownButton1.Text = "Button";
            // 
            // MenuItem3ToolStripMenuItem
            // 
            this.MenuItem3ToolStripMenuItem.Checked = true;
            this.MenuItem3ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuItem3ToolStripMenuItem.Name = "MenuItem3ToolStripMenuItem";
            this.MenuItem3ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.MenuItem3ToolStripMenuItem.Text = "Menu item 3";
            // 
            // ToolStripMenuItem1
            // 
            this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
            this.ToolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // MenuItem2ToolStripMenuItem
            // 
            this.MenuItem2ToolStripMenuItem.Name = "MenuItem2ToolStripMenuItem";
            this.MenuItem2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.MenuItem2ToolStripMenuItem.Text = "Menu item 2";
            // 
            // MenuItem1ToolStripMenuItem
            // 
            this.MenuItem1ToolStripMenuItem.Name = "MenuItem1ToolStripMenuItem";
            this.MenuItem1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.MenuItem1ToolStripMenuItem.Text = "Menu item 1";
            // 
            // CustomizableMenuStrip1
            // 
            this.CustomizableMenuStrip1.Appearance = this.AppearanceControl1;
            this.CustomizableMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.ToolsToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.CustomizableMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.CustomizableMenuStrip1.Name = "CustomizableMenuStrip1";
            this.CustomizableMenuStrip1.Size = new System.Drawing.Size(359, 24);
            this.CustomizableMenuStrip1.TabIndex = 0;
            this.CustomizableMenuStrip1.Text = "CustomizableMenuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.OpenToolStripMenuItem,
            this.toolStripSeparator,
            this.SaveToolStripMenuItem,
            this.SaveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.PrintToolStripMenuItem,
            this.PrintPreviewToolStripMenuItem,
            this.toolStripSeparator2,
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "&File";
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.NewToolStripMenuItem.Text = "&New";
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.OpenToolStripMenuItem.Text = "&Open";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(149, 6);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SaveToolStripMenuItem.Text = "&Save";
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SaveAsToolStripMenuItem.Text = "Save &As";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // PrintToolStripMenuItem
            // 
            this.PrintToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem";
            this.PrintToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PrintToolStripMenuItem.Text = "&Print";
            // 
            // PrintPreviewToolStripMenuItem
            // 
            this.PrintPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem";
            this.PrintPreviewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PrintPreviewToolStripMenuItem.Text = "Print Pre&view";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ExitToolStripMenuItem.Text = "E&xit";
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UndoToolStripMenuItem,
            this.RedoToolStripMenuItem,
            this.toolStripSeparator3,
            this.CutToolStripMenuItem,
            this.CopyToolStripMenuItem,
            this.PasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.SelectAllToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "&Edit";
            // 
            // UndoToolStripMenuItem
            // 
            this.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem";
            this.UndoToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.UndoToolStripMenuItem.Text = "&Undo";
            // 
            // RedoToolStripMenuItem
            // 
            this.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem";
            this.RedoToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.RedoToolStripMenuItem.Text = "&Redo";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(119, 6);
            // 
            // CutToolStripMenuItem
            // 
            this.CutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CutToolStripMenuItem.Name = "CutToolStripMenuItem";
            this.CutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.CutToolStripMenuItem.Text = "Cu&t";
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.CopyToolStripMenuItem.Text = "&Copy";
            // 
            // PasteToolStripMenuItem
            // 
            this.PasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem";
            this.PasteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.PasteToolStripMenuItem.Text = "&Paste";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(119, 6);
            // 
            // SelectAllToolStripMenuItem
            // 
            this.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem";
            this.SelectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.SelectAllToolStripMenuItem.Text = "Select &All";
            // 
            // ToolsToolStripMenuItem
            // 
            this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CustomizeToolStripMenuItem,
            this.OptionsToolStripMenuItem});
            this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.ToolsToolStripMenuItem.Text = "&Tools";
            // 
            // CustomizeToolStripMenuItem
            // 
            this.CustomizeToolStripMenuItem.Name = "CustomizeToolStripMenuItem";
            this.CustomizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CustomizeToolStripMenuItem.Text = "&Customize";
            // 
            // OptionsToolStripMenuItem
            // 
            this.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
            this.OptionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.OptionsToolStripMenuItem.Text = "&Options";
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContentsToolStripMenuItem,
            this.IndexToolStripMenuItem,
            this.SearchToolStripMenuItem,
            this.toolStripSeparator5,
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStripMenuItem.Text = "&Help";
            // 
            // ContentsToolStripMenuItem
            // 
            this.ContentsToolStripMenuItem.Name = "ContentsToolStripMenuItem";
            this.ContentsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ContentsToolStripMenuItem.Text = "&Contents";
            // 
            // IndexToolStripMenuItem
            // 
            this.IndexToolStripMenuItem.Name = "IndexToolStripMenuItem";
            this.IndexToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.IndexToolStripMenuItem.Text = "&Index";
            // 
            // SearchToolStripMenuItem
            // 
            this.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem";
            this.SearchToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.SearchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AboutToolStripMenuItem.Text = "&About...";
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Location = new System.Drawing.Point(12, 9);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(48, 13);
            this.lblPreview.TabIndex = 3;
            this.lblPreview.Text = "Preview:";
            // 
            // PropertyGrid1
            // 
            this.PropertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertyGrid1.Location = new System.Drawing.Point(15, 119);
            this.PropertyGrid1.Name = "PropertyGrid1";
            this.PropertyGrid1.Size = new System.Drawing.Size(361, 257);
            this.PropertyGrid1.TabIndex = 4;
            this.PropertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid1_PropertyValueChanged);
            // 
            // AppearanceEditor
            // 
            this.AcceptButton = this.OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 434);
            this.Controls.Add(this.PropertyGrid1);
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.TableLayoutPanel3);
            this.Controls.Add(this.TableLayoutPanel1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.CustomizableMenuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(404, 473);
            this.Name = "AppearanceEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Appearance Editor";
            this.TableLayoutPanel1.ResumeLayout(false);
            this.TableLayoutPanel2.ResumeLayout(false);
            this.TableLayoutPanel3.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.CustomizableToolStrip1.ResumeLayout(false);
            this.CustomizableToolStrip1.PerformLayout();
            this.CustomizableStatusStrip1.ResumeLayout(false);
            this.CustomizableStatusStrip1.PerformLayout();
            this.CustomizableMenuStrip1.ResumeLayout(false);
            this.CustomizableMenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel3;
        internal System.Windows.Forms.Button Load_Button;
        internal System.Windows.Forms.Button Save_Button;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.ToolStripButton NewToolStripButton;
        internal System.Windows.Forms.ToolStripButton OpenToolStripButton;
        internal System.Windows.Forms.ToolStripButton SaveToolStripButton;
        internal System.Windows.Forms.ToolStripButton PrintToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        internal System.Windows.Forms.ToolStripButton CutToolStripButton;
        internal System.Windows.Forms.ToolStripButton CopyToolStripButton;
        internal System.Windows.Forms.ToolStripButton PasteToolStripButton;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        internal System.Windows.Forms.ToolStripButton HelpToolStripButton;
        internal System.Windows.Forms.ToolStripButton NewToolStripButton1;
        internal System.Windows.Forms.ToolStripButton OpenToolStripButton1;
        internal System.Windows.Forms.ToolStripButton SaveToolStripButton1;
        internal System.Windows.Forms.ToolStripButton PrintToolStripButton1;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        internal System.Windows.Forms.ToolStripButton CutToolStripButton1;
        internal System.Windows.Forms.ToolStripButton CopyToolStripButton1;
        internal System.Windows.Forms.ToolStripButton PasteToolStripButton1;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        internal System.Windows.Forms.ToolStripButton HelpToolStripButton1;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel1;
        internal System.Windows.Forms.ToolStripProgressBar ToolStripProgressBar1;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel2;
        internal System.Windows.Forms.ToolStripDropDownButton ToolStripDropDownButton1;
        internal System.Windows.Forms.ToolStripMenuItem MenuItem3ToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripMenuItem1;
        internal System.Windows.Forms.ToolStripMenuItem MenuItem2ToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem MenuItem1ToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        internal System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem PrintToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem PrintPreviewToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem UndoToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem RedoToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripMenuItem CutToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem PasteToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        internal System.Windows.Forms.ToolStripMenuItem SelectAllToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem CustomizeToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem OptionsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ContentsToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem IndexToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem SearchToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        internal System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        internal System.Windows.Forms.Label lblPreview;
        internal Presentation.Windows.Forms.Customs.ToolStrip CustomizableToolStrip1;
        internal Presentation.Windows.Forms.Customs.StatusStrip CustomizableStatusStrip1;
        internal Presentation.Windows.Forms.Customs.MenuStrip CustomizableMenuStrip1;
        internal System.Windows.Forms.PropertyGrid PropertyGrid1;
        internal AppearanceManager AppearanceControl1;

        #endregion

        private AppearanceManager.AppearanceProperties _ap = null;
        public AppearanceEditor(AppearanceManager.AppearanceProperties ap)
        {
            InitializeComponent();
            _ap = ap;
            this.CustomizableMenuStrip1.Appearance.CustomAppearance = ap;
            this.CustomizableStatusStrip1.Appearance.CustomAppearance = ap;
            this.CustomizableToolStrip1.Appearance.CustomAppearance = ap;
            this.PropertyGrid1.SelectedObject = ap;         
            
        }

        public AppearanceManager.AppearanceProperties CustomAppearance
        {
            get { return _ap; }
        }

        // ERROR: Handles clauses are not supported in C#
        private void OK_Button_Click(System.Object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        // ERROR: Handles clauses are not supported in C#
        private void Load_Button_Click(System.Object sender, System.EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select XML File.";
                ofd.Filter = "XML Files (*.xml)|*.xml|All Files|*.*";

                if (ofd.ShowDialog() ==  System.Windows.Forms.DialogResult.OK)
                {
                    this.LoadAppearance(ofd.FileName);
                    CustomizableMenuStrip1.Invalidate();
                    CustomizableToolStrip1.Invalidate();
                    CustomizableStatusStrip1.Invalidate();
                }
            }
        }

        // ERROR: Handles clauses are not supported in C#
        private void Save_Button_Click(System.Object sender, System.EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Select XML File.";
                sfd.Filter = "XML Files (*.xml)|*.xml|All Files|*.*";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.SaveAppearance(sfd.FileName, AppearanceControl1);
                }
            }
        }

        public void LoadAppearance(string xmlFile)
        {
            try
            {
                using (FileStream fs = new FileStream(xmlFile, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(AppearanceManager.AppearanceProperties));

                    AppearanceManager.AppearanceProperties ap;
                    ap = (AppearanceManager.AppearanceProperties)ser.Deserialize(fs);
                    _ap = ap;
                    this.CustomizableMenuStrip1.Appearance.CustomAppearance = ap;
                    this.CustomizableStatusStrip1.Appearance.CustomAppearance = ap;
                    this.CustomizableToolStrip1.Appearance.CustomAppearance = ap;
                    this.PropertyGrid1.SelectedObject = ap;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LoadFromStream(Stream stream)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(AppearanceManager.AppearanceProperties));

                AppearanceManager.AppearanceProperties ap;
                ap = (AppearanceManager.AppearanceProperties)ser.Deserialize(stream);
                ap.SetAppearanceControl(AppearanceControl1);
                _ap = ap;
                this.AppearanceControl1.CustomAppearance = _ap;

                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SaveAppearance(string xmlFile, AppearanceManager ac)
        {
            try
            {
                using (FileStream fs = new FileStream(xmlFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(AppearanceManager.AppearanceProperties));
                    ser.Serialize(fs, ac.CustomAppearance);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // ERROR: Handles clauses are not supported in C#
        private void PropertyGrid1_PropertyValueChanged(System.Object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
        {
            this.CustomizableMenuStrip1.Invalidate();
            this.CustomizableStatusStrip1.Invalidate();
            this.CustomizableToolStrip1.Invalidate();
        }

    }

    public class CustomAppearancePropertyEditor : System.Drawing.Design.UITypeEditor
    {
        private AppearanceEditor _appearanceEditor;
        protected IWindowsFormsEditorService IEditorService;
        private Control m_EditControl;

        //private bool m_EscapePressed;
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            try
            {

                if (context != null && provider != null)
                {
                    //Uses the IWindowsFormsEditorService to display a modal dialog form
                    IEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                    if (IEditorService != null)
                    {
                        string PropName = context.PropertyDescriptor.Name;
                        m_EditControl = this.GetEditControl(PropName, value);
                        if (m_EditControl != null)
                        {
                            IEditorService.ShowDialog((Form)m_EditControl);

                            //Notify that our control has changed; otherwise changes are not stored
                            context.OnComponentChanged();

                            return this.GetEditedValue(m_EditControl, PropName, value);
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
            }
            return base.EditValue(context, provider, value);
        }

        private System.Windows.Forms.Control GetEditControl(string PropertyName, object CurrentValue)
        {
            AppearanceManager.AppearanceProperties ap = CurrentValue as AppearanceManager.AppearanceProperties;
            if (ap != null)
            {
                _appearanceEditor = new AppearanceEditor(ap);
                return _appearanceEditor;
            }
            else
            {
                return null;
            }
        }

        private object GetEditedValue(System.Windows.Forms.Control EditControl, string PropertyName, object OldValue)
        {
            if (_appearanceEditor == null || _appearanceEditor.DialogResult == DialogResult.Cancel)
            {
                return OldValue;
            }
            else
            {
                return _appearanceEditor.CustomAppearance;
            }
        }
    }

}
