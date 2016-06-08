namespace Sample
{
    partial class FormMain
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
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.dataGridViewExtended1 = new Platform.Presentation.Forms.Controls.DataGridViewExtended();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commandLink1 = new Platform.Presentation.Forms.Controls.CommandLink();
            this.commandLink2 = new Platform.Presentation.Forms.VistaControls.CommandLink();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtended1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewExtended1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridViewExtended1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewExtended1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewExtended1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewExtended1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridViewExtended1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExtended1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridViewExtended1.ColumnsWidths = null;
            this.dataGridViewExtended1.Exclude = ((System.Collections.Generic.List<string>)(resources.GetObject("dataGridViewExtended1.Exclude")));
            this.dataGridViewExtended1.Location = new System.Drawing.Point(12, 79);
            this.dataGridViewExtended1.MultiSelect = false;
            this.dataGridViewExtended1.Name = "dataGridViewExtended1";
            this.dataGridViewExtended1.RowHeadersVisible = false;
            this.dataGridViewExtended1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewExtended1.Size = new System.Drawing.Size(600, 350);
            this.dataGridViewExtended1.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            // 
            // commandLink1
            // 
            this.commandLink1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.commandLink1.Image = ((System.Drawing.Image)(resources.GetObject("commandLink1.Image")));
            this.commandLink1.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.commandLink1.Location = new System.Drawing.Point(12, 12);
            this.commandLink1.Name = "commandLink1";
            this.commandLink1.Note = "This is a demo control";
            this.commandLink1.Size = new System.Drawing.Size(248, 60);
            this.commandLink1.TabIndex = 0;
            this.commandLink1.Text = "Command link action";
            this.commandLink1.UseVisualStyleBackColor = true;
            // 
            // commandLink2
            // 
            this.commandLink2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.commandLink2.Image = ((System.Drawing.Bitmap)(resources.GetObject("commandLink2.Image")));
            this.commandLink2.ImageScalingSize = new System.Drawing.Size(0, 0);
            this.commandLink2.Location = new System.Drawing.Point(266, 12);
            this.commandLink2.Name = "commandLink2";
            this.commandLink2.Note = "This is a demo control";
            this.commandLink2.Size = new System.Drawing.Size(346, 61);
            this.commandLink2.TabIndex = 1;
            this.commandLink2.Text = "commandLink2";
            this.commandLink2.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.dataGridViewExtended1);
            this.Controls.Add(this.commandLink2);
            this.Controls.Add(this.commandLink1);
            this.Name = "FormMain";
            this.Text = "Sample";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExtended1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Platform.Presentation.Forms.Controls.CommandLink commandLink1;
        private Platform.Presentation.Forms.VistaControls.CommandLink commandLink2;
        private Platform.Presentation.Forms.Controls.DataGridViewExtended dataGridViewExtended1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}

