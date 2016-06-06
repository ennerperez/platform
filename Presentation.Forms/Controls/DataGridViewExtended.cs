using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Platform.Presentation.Forms.Controls
{

    public class DataGridViewExtended : DataGridView
    {

        #region Designer

        private System.ComponentModel.IContainer components;

        [System.Diagnostics.DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAutoSize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorOptions = new System.Windows.Forms.ToolStripSeparator();
            this.mainContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this).BeginInit();
            this.SuspendLayout();

            //
            //MainContextMenuStrip
            //
            this.mainContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.toolStripMenuItemAutoSize, this.toolStripSeparatorOptions });
            this.mainContextMenuStrip.Name = "MainContextMenuStrip";
            this.mainContextMenuStrip.Size = new System.Drawing.Size(172, 32);
            //
            //toolStripMenuItemAutoSize
            //
            this.toolStripMenuItemAutoSize.CheckOnClick = true;
            this.toolStripMenuItemAutoSize.Name = "ToolStripMenuItemAutoSize";
            this.toolStripMenuItemAutoSize.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItemAutoSize.Text = "Autosize columns.";
            //
            //toolStripSeparatorOptions
            //
            this.toolStripSeparatorOptions.Name = "ToolStripSeparatorOptions";
            this.toolStripSeparatorOptions.Size = new System.Drawing.Size(168, 6);

            //
            //DataGridViewExtended
            //
            this.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle() { BackColor = System.Drawing.SystemColors.ControlLight };
            this.AutoGenerateColumns = false;
            this.BackgroundColor = System.Drawing.SystemColors.Window;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MultiSelect = false;
            this.RowHeadersVisible = false;
            this.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this).EndInit();
            this.ResumeLayout(false);

        }

        internal ContextMenuStrip mainContextMenuStrip;
        internal ToolStripMenuItem toolStripMenuItemAutoSize;
        internal ToolStripSeparator toolStripSeparatorOptions;

        #endregion

        public DataGridViewExtended() : base()
        {
            InitializeComponent();
            toolStripMenuItemAutoSize.CheckedChanged += toolStripMenuItemAutoSize_CheckedChanged;
            this.ColumnHeaderMouseClick += dataGridView_ColumnHeaderMouseClick;
        }

        private string autoSizeText = "Autosize columns";
        [Browsable(true), DefaultValue("Autosize columns"), Localizable(true), Category("Extended")]
        public string AutoSizeText
        {
            get { return autoSizeText; }
            set { autoSizeText = value; }
        }

        private List<string> exclude = new List<string> { "internal" };
        [Browsable(true), Category("Extended"), DefaultValue("internal")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [Localizable(false)]
        public List<string> Exclude
        {
            get { return exclude; }
            set { exclude = value; }
        }

        private IEnumerable<int> columnsWidths = null;
        [Browsable(false)]
        public IEnumerable<int> ColumnsWidths
        {
            get { return columnsWidths; }
            set { columnsWidths = value; }
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            e.Column.Visible = !(exclude.Contains(e.Column.HeaderText));
            base.OnColumnAdded(e);
        }

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                this.toolStripMenuItemAutoSize.Checked = this.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill;

                if (this.Exclude == null)
                    this.Exclude = new List<string>();

                foreach (DataGridViewColumn column in this.Columns.OfType<DataGridViewColumn>().Where(c => c.HeaderText != "" & !this.Exclude.Contains(c.HeaderText)))
                {
                    ToolStripMenuItem toolStripItemMenuItem = new ToolStripMenuItem();
                    {
                        toolStripItemMenuItem.Text = column.HeaderText;
                        toolStripItemMenuItem.CheckOnClick = true;
                        toolStripItemMenuItem.Checked = column.Visible;
                        toolStripItemMenuItem.Name = column.Name;
                    }

                    toolStripItemMenuItem.CheckedChanged += toolStripItemMenuItem_CheckedChanged;

                    this.mainContextMenuStrip.Items.Add(toolStripItemMenuItem);
                    column.HeaderCell.ContextMenuStrip = mainContextMenuStrip;

                }

                this.toolStripMenuItemAutoSize.Text = this.AutoSizeText;
                this.mainContextMenuStrip.Show(MousePosition);

            }
        }

        private void toolStripMenuItemAutoSize_CheckedChanged(object sender, System.EventArgs e)
        {
            var toolStripMenuItem = (sender as ToolStripMenuItem);
            switch (toolStripMenuItem.Checked)
            {
                case true:
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    break;
                case false:
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    if (ColumnsWidths != null && ColumnsWidths.Count() > 0)
                        foreach (DataGridViewColumn item in Columns)
                            item.Width = (this.ColumnsWidths.ToArray()[item.Index]);
                    else
                        toolStripMenuItem.Checked = false;

                    break;
            }
        }

        private void toolStripItemMenuItem_CheckedChanged(object sender, System.EventArgs e)
        {
            var toolStripMenuItem = (sender as ToolStripMenuItem);
            if (toolStripMenuItem.Name != "" && (toolStripMenuItem.Name != toolStripMenuItemAutoSize.Name))
            {
                int _c = 0;
                foreach (DataGridViewColumn _item in Columns)
                    if (_item.Visible)
                        _c += 1;

                if (_c <= 1 & toolStripMenuItem.Checked == false)
                    toolStripMenuItem.Checked = true;

                Columns[toolStripMenuItem.Name].Visible = toolStripMenuItem.Checked;

            }
        }

    }
}
