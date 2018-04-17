namespace FlashTest
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnImportCmd = new System.Windows.Forms.Button();
            this.dgvCmd = new System.Windows.Forms.DataGridView();
            this.cboDeviceType = new System.Windows.Forms.ComboBox();
            this.cboAlg = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmd)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImportCmd
            // 
            this.btnImportCmd.Location = new System.Drawing.Point(674, 40);
            this.btnImportCmd.Name = "btnImportCmd";
            this.btnImportCmd.Size = new System.Drawing.Size(143, 39);
            this.btnImportCmd.TabIndex = 0;
            this.btnImportCmd.Text = "Import Cmd";
            this.btnImportCmd.UseVisualStyleBackColor = true;
            this.btnImportCmd.Click += new System.EventHandler(this.btnImportCmd_Click);
            // 
            // dgvCmd
            // 
            this.dgvCmd.AllowUserToAddRows = false;
            this.dgvCmd.AllowUserToDeleteRows = false;
            this.dgvCmd.AllowUserToResizeColumns = false;
            this.dgvCmd.AllowUserToResizeRows = false;
            this.dgvCmd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCmd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCmd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCmd.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCmd.Location = new System.Drawing.Point(13, 69);
            this.dgvCmd.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCmd.Name = "dgvCmd";
            this.dgvCmd.ReadOnly = true;
            this.dgvCmd.RowHeadersVisible = false;
            this.dgvCmd.RowHeadersWidth = 430;
            this.dgvCmd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCmd.Size = new System.Drawing.Size(297, 431);
            this.dgvCmd.TabIndex = 7;
            // 
            // cboDeviceType
            // 
            this.cboDeviceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeviceType.FormattingEnabled = true;
            this.cboDeviceType.Location = new System.Drawing.Point(13, 37);
            this.cboDeviceType.Margin = new System.Windows.Forms.Padding(4);
            this.cboDeviceType.Name = "cboDeviceType";
            this.cboDeviceType.Size = new System.Drawing.Size(143, 24);
            this.cboDeviceType.TabIndex = 6;
            this.cboDeviceType.SelectedIndexChanged += new System.EventHandler(this.cboDeviceType_SelectedIndexChanged);
            // 
            // cboAlg
            // 
            this.cboAlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlg.Location = new System.Drawing.Point(167, 37);
            this.cboAlg.Margin = new System.Windows.Forms.Padding(4);
            this.cboAlg.Name = "cboAlg";
            this.cboAlg.Size = new System.Drawing.Size(143, 24);
            this.cboAlg.Sorted = true;
            this.cboAlg.TabIndex = 6;
            this.cboAlg.SelectedIndexChanged += new System.EventHandler(this.cboAlg_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Device Type List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Algorithm List";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 513);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCmd);
            this.Controls.Add(this.cboAlg);
            this.Controls.Add(this.cboDeviceType);
            this.Controls.Add(this.btnImportCmd);
            this.Name = "frmMain";
            this.Text = "frmMain";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImportCmd;
        private System.Windows.Forms.DataGridView dgvCmd;
        private System.Windows.Forms.ComboBox cboDeviceType;
        private System.Windows.Forms.ComboBox cboAlg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}