namespace Platform.Samples.Forms
{
    partial class FormReports
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        /// </summary> 
        private void InitializeComponent()
        {
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rdclReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.razorReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialogExport = new System.Windows.Forms.SaveFileDialog();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.dataGridViewDataSource = new System.Windows.Forms.DataGridView();
            this.panelZoom = new System.Windows.Forms.Panel();
            this.trackBarZoom = new System.Windows.Forms.TrackBar();
            this.menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDataSource)).BeginInit();
            this.panelZoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.rdclReportsToolStripMenuItem,
            this.razorReportsToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(624, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exportToolStripMenuItem.Text = "&Export...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // rdclReportsToolStripMenuItem
            // 
            this.rdclReportsToolStripMenuItem.Name = "rdclReportsToolStripMenuItem";
            this.rdclReportsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.rdclReportsToolStripMenuItem.Text = "&RDCL";
            // 
            // razorReportsToolStripMenuItem
            // 
            this.razorReportsToolStripMenuItem.Name = "razorReportsToolStripMenuItem";
            this.razorReportsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.razorReportsToolStripMenuItem.Text = "Ra&zor";
            // 
            // saveFileDialogExport
            // 
            this.saveFileDialogExport.DefaultExt = "html";
            this.saveFileDialogExport.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialogExport_FileOk);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 24);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.dataGridViewDataSource);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelZoom);
            this.splitContainerMain.Size = new System.Drawing.Size(624, 417);
            this.splitContainerMain.SplitterDistance = 208;
            this.splitContainerMain.TabIndex = 1;
            // 
            // dataGridViewDataSource
            // 
            this.dataGridViewDataSource.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewDataSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDataSource.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewDataSource.Name = "dataGridViewDataSource";
            this.dataGridViewDataSource.RowHeadersVisible = false;
            this.dataGridViewDataSource.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDataSource.Size = new System.Drawing.Size(208, 417);
            this.dataGridViewDataSource.TabIndex = 0;
            // 
            // panelZoom
            // 
            this.panelZoom.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelZoom.Controls.Add(this.trackBarZoom);
            this.panelZoom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelZoom.Location = new System.Drawing.Point(0, 379);
            this.panelZoom.Name = "panelZoom";
            this.panelZoom.Padding = new System.Windows.Forms.Padding(6);
            this.panelZoom.Size = new System.Drawing.Size(412, 38);
            this.panelZoom.TabIndex = 0;
            this.panelZoom.Visible = false;
            // 
            // trackBarZoom
            // 
            this.trackBarZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarZoom.Location = new System.Drawing.Point(6, 6);
            this.trackBarZoom.Maximum = 100;
            this.trackBarZoom.Name = "trackBarZoom";
            this.trackBarZoom.Size = new System.Drawing.Size(400, 26);
            this.trackBarZoom.TabIndex = 0;
            this.trackBarZoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarZoom.Value = 100;
            this.trackBarZoom.ValueChanged += new System.EventHandler(this.trackBarZoom_ValueChanged);
            // 
            // FormReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormReports";
            this.Text = "Reports";
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDataSource)).EndInit();
            this.panelZoom.ResumeLayout(false);
            this.panelZoom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem razorReportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rdclReportsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogExport;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.DataGridView dataGridViewDataSource;
        private System.Windows.Forms.Panel panelZoom;
        private System.Windows.Forms.TrackBar trackBarZoom;
    }
}

