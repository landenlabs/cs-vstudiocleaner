using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace VStudioCleaner_ns
{
	/// <summary>
	/// Summary description for About.
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label titleBar;
		private System.Windows.Forms.LinkLabel linkLabel1;
        private Label author;
        private RichTextBox richTextBox1;
        private Label buildDateLb;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public About(MainForm mainform)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            titleBar.Text = mainform.ProductNameAndVersion(); 

            // If AssemblyVersion is n.m.*, then third number is julian date with 0= 1-Jan-2000
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build);
            buildDateLb.Text = "Build " + buildDate.ToString("dd-MMM-yyyy");

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.titleBar = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.author = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buildDateLb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(15, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(105, 96);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.Silver;
            this.titleBar.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBar.ForeColor = System.Drawing.Color.Red;
            this.titleBar.Location = new System.Drawing.Point(175, 12);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(206, 23);
            this.titleBar.TabIndex = 3;
            this.titleBar.Text = "VStudio Cleaner v1.4 2009";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Silver;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(142, 92);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(267, 16);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://landenlabs.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // author
            // 
            this.author.BackColor = System.Drawing.Color.Silver;
            this.author.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author.ForeColor = System.Drawing.Color.Red;
            this.author.Location = new System.Drawing.Point(175, 35);
            this.author.Name = "author";
            this.author.Size = new System.Drawing.Size(206, 23);
            this.author.TabIndex = 5;
            this.author.Text = "By: Dennis Lang";
            this.author.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.Color.LightGray;
            this.richTextBox1.Location = new System.Drawing.Point(15, 133);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(404, 380);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);
            // 
            // buildDateLb
            // 
            this.buildDateLb.BackColor = System.Drawing.Color.Silver;
            this.buildDateLb.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buildDateLb.ForeColor = System.Drawing.Color.Red;
            this.buildDateLb.Location = new System.Drawing.Point(175, 58);
            this.buildDateLb.Name = "buildDateLb";
            this.buildDateLb.Size = new System.Drawing.Size(206, 23);
            this.buildDateLb.TabIndex = 7;
            this.buildDateLb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // About
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(435, 528);
            this.Controls.Add(this.buildDateLb);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.author);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel linkLabel = sender as LinkLabel;
            System.Diagnostics.Process.Start(linkLabel.Text);
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

	}
}
