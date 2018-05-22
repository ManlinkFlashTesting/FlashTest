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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvCmd = new System.Windows.Forms.DataGridView();
            this.cboDeviceType = new System.Windows.Forms.ComboBox();
            this.cboAlg = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveLog = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnCSend = new System.Windows.Forms.Button();
            this.txtCMsg = new System.Windows.Forms.TextBox();
            this.labelManualCommand = new System.Windows.Forms.Label();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmImportCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeleteCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnEndCmd = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.tsmDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPatternDownload = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmd)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCmd
            // 
            this.dgvCmd.AllowUserToAddRows = false;
            this.dgvCmd.AllowUserToDeleteRows = false;
            this.dgvCmd.AllowUserToResizeColumns = false;
            this.dgvCmd.AllowUserToResizeRows = false;
            this.dgvCmd.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCmd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCmd.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCmd.Location = new System.Drawing.Point(13, 108);
            this.dgvCmd.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCmd.MultiSelect = false;
            this.dgvCmd.Name = "dgvCmd";
            this.dgvCmd.ReadOnly = true;
            this.dgvCmd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCmd.Size = new System.Drawing.Size(321, 418);
            this.dgvCmd.TabIndex = 7;
            // 
            // cboDeviceType
            // 
            this.cboDeviceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeviceType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDeviceType.FormattingEnabled = true;
            this.cboDeviceType.Location = new System.Drawing.Point(16, 72);
            this.cboDeviceType.Margin = new System.Windows.Forms.Padding(4);
            this.cboDeviceType.Name = "cboDeviceType";
            this.cboDeviceType.Size = new System.Drawing.Size(167, 26);
            this.cboDeviceType.TabIndex = 6;
            this.cboDeviceType.SelectedIndexChanged += new System.EventHandler(this.cboDeviceType_SelectedIndexChanged);
            // 
            // cboAlg
            // 
            this.cboAlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAlg.Location = new System.Drawing.Point(191, 72);
            this.cboAlg.Margin = new System.Windows.Forms.Padding(4);
            this.cboAlg.Name = "cboAlg";
            this.cboAlg.Size = new System.Drawing.Size(143, 26);
            this.cboAlg.TabIndex = 6;
            this.cboAlg.SelectedIndexChanged += new System.EventHandler(this.cboAlg_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Device Type List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(188, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Algorithm List";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(478, 108);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 18);
            this.label3.TabIndex = 57;
            this.label3.Text = "Communication Log:";
            // 
            // btnSaveLog
            // 
            this.btnSaveLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveLog.Location = new System.Drawing.Point(347, 470);
            this.btnSaveLog.Margin = new System.Windows.Forms.Padding(5);
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Size = new System.Drawing.Size(124, 30);
            this.btnSaveLog.TabIndex = 52;
            this.btnSaveLog.Text = "Save Log";
            this.btnSaveLog.UseVisualStyleBackColor = true;
            this.btnSaveLog.Click += new System.EventHandler(this.btnSaveLog_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearLog.Location = new System.Drawing.Point(347, 370);
            this.btnClearLog.Margin = new System.Windows.Forms.Padding(5);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(124, 30);
            this.btnClearLog.TabIndex = 53;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // btnCSend
            // 
            this.btnCSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCSend.Location = new System.Drawing.Point(1034, 69);
            this.btnCSend.Margin = new System.Windows.Forms.Padding(5);
            this.btnCSend.Name = "btnCSend";
            this.btnCSend.Size = new System.Drawing.Size(124, 30);
            this.btnCSend.TabIndex = 54;
            this.btnCSend.Text = "Send";
            this.btnCSend.UseVisualStyleBackColor = true;
            this.btnCSend.Click += new System.EventHandler(this.btnCSend_Click);
            // 
            // txtCMsg
            // 
            this.txtCMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCMsg.Location = new System.Drawing.Point(481, 71);
            this.txtCMsg.Margin = new System.Windows.Forms.Padding(5);
            this.txtCMsg.Multiline = true;
            this.txtCMsg.Name = "txtCMsg";
            this.txtCMsg.Size = new System.Drawing.Size(543, 29);
            this.txtCMsg.TabIndex = 55;
            // 
            // labelManualCommand
            // 
            this.labelManualCommand.AutoSize = true;
            this.labelManualCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelManualCommand.Location = new System.Drawing.Point(478, 40);
            this.labelManualCommand.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelManualCommand.Name = "labelManualCommand";
            this.labelManualCommand.Size = new System.Drawing.Size(134, 18);
            this.labelManualCommand.TabIndex = 56;
            this.labelManualCommand.Text = "Manual Command:";
            // 
            // txtMsg
            // 
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsg.Location = new System.Drawing.Point(481, 135);
            this.txtMsg.Margin = new System.Windows.Forms.Padding(5);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMsg.Size = new System.Drawing.Size(677, 390);
            this.txtMsg.TabIndex = 51;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFile,
            this.tsmDatabase,
            this.tsmDownload,
            this.tsmAbout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1172, 28);
            this.menuStrip1.TabIndex = 58;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmFile
            // 
            this.tsmFile.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tsmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmExit});
            this.tsmFile.Name = "tsmFile";
            this.tsmFile.Size = new System.Drawing.Size(44, 24);
            this.tsmFile.Text = "File";
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(108, 26);
            this.tsmExit.Text = "Exit";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // tsmDatabase
            // 
            this.tsmDatabase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmImportCmd,
            this.tsmDeleteCmd,
            this.tsmShowDatabase});
            this.tsmDatabase.Name = "tsmDatabase";
            this.tsmDatabase.Size = new System.Drawing.Size(84, 24);
            this.tsmDatabase.Text = "Database";
            // 
            // tsmImportCmd
            // 
            this.tsmImportCmd.Name = "tsmImportCmd";
            this.tsmImportCmd.Size = new System.Drawing.Size(190, 26);
            this.tsmImportCmd.Text = "Import Cmd List";
            this.tsmImportCmd.Click += new System.EventHandler(this.tsmImportCmd_Click);
            // 
            // tsmDeleteCmd
            // 
            this.tsmDeleteCmd.Name = "tsmDeleteCmd";
            this.tsmDeleteCmd.Size = new System.Drawing.Size(190, 26);
            this.tsmDeleteCmd.Text = "Delete Cmd List";
            this.tsmDeleteCmd.Click += new System.EventHandler(this.tsmDeleteCmd_Click);
            // 
            // tsmShowDatabase
            // 
            this.tsmShowDatabase.Name = "tsmShowDatabase";
            this.tsmShowDatabase.Size = new System.Drawing.Size(190, 26);
            this.tsmShowDatabase.Text = "Show Database";
            this.tsmShowDatabase.Click += new System.EventHandler(this.tsmShowDatabase_Click);
            // 
            // tsmAbout
            // 
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.Size = new System.Drawing.Size(62, 24);
            this.tsmAbout.Text = "About";
            this.tsmAbout.Click += new System.EventHandler(this.tsmAbout_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslTime,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 530);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1172, 25);
            this.statusStrip1.TabIndex = 59;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslTime
            // 
            this.tsslTime.Name = "tsslTime";
            this.tsslTime.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(561, 20);
            this.toolStripStatusLabel3.Text = "                                                                                 " +
    "                                                        |";
            this.toolStripStatusLabel3.ToolTipText = "  ";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(141, 20);
            this.toolStripStatusLabel1.Text = "Support By ManLink";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnEndCmd
            // 
            this.btnEndCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndCmd.Location = new System.Drawing.Point(347, 270);
            this.btnEndCmd.Margin = new System.Windows.Forms.Padding(5);
            this.btnEndCmd.Name = "btnEndCmd";
            this.btnEndCmd.Size = new System.Drawing.Size(124, 30);
            this.btnEndCmd.TabIndex = 54;
            this.btnEndCmd.Text = "End Cmd";
            this.btnEndCmd.UseVisualStyleBackColor = true;
            this.btnEndCmd.Click += new System.EventHandler(this.btnEndCmd_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.Location = new System.Drawing.Point(347, 170);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(5);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(124, 30);
            this.btnExecute.TabIndex = 54;
            this.btnExecute.Text = "Execute Cmd";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // tsmDownload
            // 
            this.tsmDownload.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmPatternDownload});
            this.tsmDownload.Name = "tsmDownload";
            this.tsmDownload.Size = new System.Drawing.Size(90, 24);
            this.tsmDownload.Text = "Download";
            // 
            // tsmPatternDownload
            // 
            this.tsmPatternDownload.Name = "tsmPatternDownload";
            this.tsmPatternDownload.Size = new System.Drawing.Size(204, 26);
            this.tsmPatternDownload.Text = "Pattern Download";
            this.tsmPatternDownload.Click += new System.EventHandler(this.tsmPatternDownload_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 555);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSaveLog);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.btnEndCmd);
            this.Controls.Add(this.btnCSend);
            this.Controls.Add(this.txtCMsg);
            this.Controls.Add(this.labelManualCommand);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCmd);
            this.Controls.Add(this.cboAlg);
            this.Controls.Add(this.cboDeviceType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Flash Testing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmd)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvCmd;
        private System.Windows.Forms.ComboBox cboDeviceType;
        private System.Windows.Forms.ComboBox cboAlg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSaveLog;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnCSend;
        private System.Windows.Forms.TextBox txtCMsg;
        private System.Windows.Forms.Label labelManualCommand;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmFile;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmDatabase;
        private System.Windows.Forms.ToolStripMenuItem tsmImportCmd;
        private System.Windows.Forms.ToolStripMenuItem tsmShowDatabase;
        private System.Windows.Forms.ToolStripMenuItem tsmAbout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnEndCmd;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteCmd;
        private System.Windows.Forms.ToolStripMenuItem tsmDownload;
        private System.Windows.Forms.ToolStripMenuItem tsmPatternDownload;
    }
}