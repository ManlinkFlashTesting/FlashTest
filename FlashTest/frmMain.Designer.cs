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
            this.btnImportCmd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImportCmd
            // 
            this.btnImportCmd.Location = new System.Drawing.Point(72, 204);
            this.btnImportCmd.Name = "btnImportCmd";
            this.btnImportCmd.Size = new System.Drawing.Size(143, 39);
            this.btnImportCmd.TabIndex = 0;
            this.btnImportCmd.Text = "Import Cmd";
            this.btnImportCmd.UseVisualStyleBackColor = true;
            this.btnImportCmd.Click += new System.EventHandler(this.btnImportCmd_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 255);
            this.Controls.Add(this.btnImportCmd);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImportCmd;
    }
}