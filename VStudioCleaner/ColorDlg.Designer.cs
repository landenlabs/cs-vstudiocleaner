namespace VStudioCleaner_ns
{
    partial class ColorDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorDlg));
            this.cancelBtn = new System.Windows.Forms.Button();
            this.acceptBtn = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.fgColorBtn = new System.Windows.Forms.Button();
            this.bgColorBtn = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(84, 66);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 0;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // acceptBtn
            // 
            this.acceptBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptBtn.Location = new System.Drawing.Point(3, 66);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(75, 23);
            this.acceptBtn.TabIndex = 1;
            this.acceptBtn.Text = "Accept";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.acceptBtn_Click);
            // 
            // label
            // 
            this.label.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.Location = new System.Drawing.Point(3, 13);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(156, 20);
            this.label.TabIndex = 2;
            this.label.Text = "Color Settings";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fgColorBtn
            // 
            this.fgColorBtn.Location = new System.Drawing.Point(6, 36);
            this.fgColorBtn.Name = "fgColorBtn";
            this.fgColorBtn.Size = new System.Drawing.Size(75, 23);
            this.fgColorBtn.TabIndex = 3;
            this.fgColorBtn.Text = "Foreground";
            this.fgColorBtn.UseVisualStyleBackColor = true;
            this.fgColorBtn.Click += new System.EventHandler(this.fgColorBtn_Click);
            // 
            // bgColorBtn
            // 
            this.bgColorBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bgColorBtn.Location = new System.Drawing.Point(84, 36);
            this.bgColorBtn.Name = "bgColorBtn";
            this.bgColorBtn.Size = new System.Drawing.Size(75, 23);
            this.bgColorBtn.TabIndex = 4;
            this.bgColorBtn.Text = "Background";
            this.bgColorBtn.UseVisualStyleBackColor = true;
            this.bgColorBtn.Click += new System.EventHandler(this.bgColorBtn_Click_1);
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.FullOpen = true;
            // 
            // ColorDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(162, 101);
            this.Controls.Add(this.bgColorBtn);
            this.Controls.Add(this.fgColorBtn);
            this.Controls.Add(this.label);
            this.Controls.Add(this.acceptBtn);
            this.Controls.Add(this.cancelBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ColorDlg";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Color";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button acceptBtn;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button fgColorBtn;
        private System.Windows.Forms.Button bgColorBtn;
        private System.Windows.Forms.ColorDialog colorDialog;
    }
}