using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

using Timer = System.Windows.Forms.Timer;
 
namespace VStudioCleaner_ns
{
	/// <summary>
    /// Author: Dennis Lang - 2009
    /// https://landenlabs.com/
    /// 
    /// This application is based off of Leonardo Paneque "Solution Cleaner"
    /// http://www.teknowmagic.net/
    /// 
	/// Scan folder (directory) paths and apply filters to extract 'located' files.
	/// </summary>
	public class FileScan : System.Windows.Forms.Form
    {
        private IContainer components;

        string pathList;
        ArrayList filterList;
        int maxFileList;

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label infoLbl;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label totalFilesLabel;
		private System.Windows.Forms.Label totalBytesLabel;
        private System.Windows.Forms.MenuItem RemoveMenuItem;
		private System.Windows.Forms.Button DelFilesBtn;
        private System.Windows.Forms.Button StopBtn;
        private ListView filesListview;
        private ColumnHeader columnHeader0;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ToolTip toolTip;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private Button exportBtn;
        private SaveFileDialog exportCsvDialog;
        private TableLayoutPanel btnTable;
        private Button closeBtn;
        private Label label1;
        private TextBox selFilterBox;
        private MenuItem RemoveInvMenuItem;
        private MenuItem deleteNowMenuItem;
        private CheckBox readonlyCk;
        private FlowLayoutPanel iconBar;
        private ProgressBar ProgressMon;
        private System.Windows.Forms.ContextMenu buttonMenu;
        private MenuItem btnMenuHide;
        private MenuItem btnMenuDelete;
        private MenuItem RemoveWhyGrpMenuItem;
        private MenuItem menuItem6;
        private MenuItem menuItem5;
        private MenuItem deleteWhyGroMenuItem;
        private MenuItem btnMenuRestore;
        private MenuItem menuItem4;
        private Label label2;
        private Label totalSelFiles;
        private System.Windows.Forms.ContextMenu FileListContextMenu;

		public FileScan(string paths, ArrayList filterList, int maxFileList)
		{
            this.pathList = paths;
            this.filterList = filterList;
            this.maxFileList = maxFileList;
			
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            infoLbl.Visible = false;
            infoLbl.Text =
#if ANYCPU
#warning "Switch compiler target from Any CPU to x86 or x64 to enable deletion to Recycle Bin."
            "Right click on items to explorer or remove from list before pressing Delete Files.\n Delete Files will permanently delete selected items and NOT move to Recycle Bin";
#else
            "Right click on items to explorer or remove from list before pressing Delete Files.  Delete Files will move selected items to recycle bin.";
#endif

            this.Text = paths;
            this.filesListview.ListViewItemSorter = new ListViewColumnSorter(ListViewColumnSorter.SortDataType.eAuto);
            this.hiddenFiles = new ListView();
            foreach (ColumnHeader colHdr in this.filesListview.Columns)
                this.hiddenFiles.Columns.Add(colHdr.Clone() as ColumnHeader);
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileScan));
            this.DelFilesBtn = new System.Windows.Forms.Button();
            this.FileListContextMenu = new System.Windows.Forms.ContextMenu();
            this.RemoveMenuItem = new System.Windows.Forms.MenuItem();
            this.RemoveWhyGrpMenuItem = new System.Windows.Forms.MenuItem();
            this.RemoveInvMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.deleteNowMenuItem = new System.Windows.Forms.MenuItem();
            this.deleteWhyGroMenuItem = new System.Windows.Forms.MenuItem();
            this.StopBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.infoLbl = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.totalFilesLabel = new System.Windows.Forms.Label();
            this.totalBytesLabel = new System.Windows.Forms.Label();
            this.filesListview = new System.Windows.Forms.ListView();
            this.columnHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.exportBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.readonlyCk = new System.Windows.Forms.CheckBox();
            this.exportCsvDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnTable = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.selFilterBox = new System.Windows.Forms.TextBox();
            this.iconBar = new System.Windows.Forms.FlowLayoutPanel();
            this.ProgressMon = new System.Windows.Forms.ProgressBar();
            this.buttonMenu = new System.Windows.Forms.ContextMenu();
            this.btnMenuHide = new System.Windows.Forms.MenuItem();
            this.btnMenuRestore = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.btnMenuDelete = new System.Windows.Forms.MenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.totalSelFiles = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.btnTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // DelFilesBtn
            // 
            this.DelFilesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DelFilesBtn.AutoSize = true;
            this.DelFilesBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.DelFilesBtn.Enabled = false;
            this.DelFilesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DelFilesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DelFilesBtn.ForeColor = System.Drawing.Color.White;
            this.DelFilesBtn.Location = new System.Drawing.Point(3, 4);
            this.DelFilesBtn.Name = "DelFilesBtn";
            this.DelFilesBtn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.DelFilesBtn.Size = new System.Drawing.Size(86, 25);
            this.DelFilesBtn.TabIndex = 0;
            this.DelFilesBtn.Text = "Delete Files";
            this.toolTip.SetToolTip(this.DelFilesBtn, "Warning - deletes ALL files in list above.  ");
            this.DelFilesBtn.UseVisualStyleBackColor = false;
            this.DelFilesBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // FileListContextMenu
            // 
            this.FileListContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.RemoveMenuItem,
            this.RemoveWhyGrpMenuItem,
            this.RemoveInvMenuItem,
            this.menuItem6,
            this.menuItem1,
            this.menuItem2,
            this.menuItem5,
            this.deleteNowMenuItem,
            this.deleteWhyGroMenuItem});
            // 
            // RemoveMenuItem
            // 
            this.RemoveMenuItem.Index = 0;
            this.RemoveMenuItem.Text = "Remove files from list";
            this.RemoveMenuItem.Click += new System.EventHandler(this.RemoveMenuItem_Click);
            // 
            // RemoveWhyGrpMenuItem
            // 
            this.RemoveWhyGrpMenuItem.Index = 1;
            this.RemoveWhyGrpMenuItem.Text = "Remove \'why\' group frrom list";
            this.RemoveWhyGrpMenuItem.Click += new System.EventHandler(this.RemoveWhyGrpMenuItem_Click);
            // 
            // RemoveInvMenuItem
            // 
            this.RemoveInvMenuItem.Index = 2;
            this.RemoveInvMenuItem.Text = "Invert selection";
            this.RemoveInvMenuItem.Click += new System.EventHandler(this.InvMenuItem_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 3;
            this.menuItem6.Text = "-";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "Launch file explorer";
            this.menuItem1.Click += new System.EventHandler(this.LaunchExplorer_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 5;
            this.menuItem2.Text = "Launch cmd";
            this.menuItem2.Click += new System.EventHandler(this.LaunchCmd_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 6;
            this.menuItem5.Text = "-";
            // 
            // deleteNowMenuItem
            // 
            this.deleteNowMenuItem.Index = 7;
            this.deleteNowMenuItem.Text = "Delete files from Disk NOW!";
            this.deleteNowMenuItem.Click += new System.EventHandler(this.DeleteNowMenuItem_Click);
            // 
            // deleteWhyGroMenuItem
            // 
            this.deleteWhyGroMenuItem.Index = 8;
            this.deleteWhyGroMenuItem.Text = "Delete \'why\' group from Disk";
            this.deleteWhyGroMenuItem.Click += new System.EventHandler(this.deleteWhyGroMenuItem_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StopBtn.AutoSize = true;
            this.StopBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.StopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopBtn.ForeColor = System.Drawing.Color.White;
            this.StopBtn.Location = new System.Drawing.Point(600, 4);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 25);
            this.StopBtn.TabIndex = 2;
            this.StopBtn.Text = "Stop";
            this.toolTip.SetToolTip(this.StopBtn, "Stop active scan or deletion.\r\n\r\n");
            this.StopBtn.UseVisualStyleBackColor = false;
            this.StopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = global::VStudioCleaner_ns.Properties.Resources.blackBubbles2;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.infoLbl);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(948, 72);
            this.panel1.TabIndex = 8;
            // 
            // infoLbl
            // 
            this.infoLbl.BackColor = System.Drawing.Color.Transparent;
            this.infoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoLbl.ForeColor = System.Drawing.Color.White;
            this.infoLbl.Location = new System.Drawing.Point(17, 40);
            this.infoLbl.Name = "infoLbl";
            this.infoLbl.Size = new System.Drawing.Size(822, 31);
            this.infoLbl.TabIndex = 0;
            this.infoLbl.Text = "Right click on items to explorer or remove  from list before pressing Delete File" +
    "s.  Delete Files will move selected items to recycle bin.";
            this.infoLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.infoLbl.MouseEnter += new System.EventHandler(this.infoLbl_MouseEnter);
            this.infoLbl.MouseLeave += new System.EventHandler(this.infoLbl_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(845, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 50);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Total Files : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(216, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Total Space : ";
            // 
            // totalFilesLabel
            // 
            this.totalFilesLabel.AutoSize = true;
            this.totalFilesLabel.Location = new System.Drawing.Point(92, 86);
            this.totalFilesLabel.Name = "totalFilesLabel";
            this.totalFilesLabel.Size = new System.Drawing.Size(13, 13);
            this.totalFilesLabel.TabIndex = 11;
            this.totalFilesLabel.Text = "0";
            // 
            // totalBytesLabel
            // 
            this.totalBytesLabel.AutoSize = true;
            this.totalBytesLabel.Location = new System.Drawing.Point(299, 86);
            this.totalBytesLabel.Name = "totalBytesLabel";
            this.totalBytesLabel.Size = new System.Drawing.Size(13, 13);
            this.totalBytesLabel.TabIndex = 12;
            this.totalBytesLabel.Text = "0";
            // 
            // filesListview
            // 
            this.filesListview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesListview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filesListview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader0,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.filesListview.ContextMenu = this.FileListContextMenu;
            this.filesListview.FullRowSelect = true;
            this.filesListview.GridLines = true;
            this.filesListview.HideSelection = false;
            this.filesListview.Location = new System.Drawing.Point(86, 130);
            this.filesListview.Name = "filesListview";
            this.filesListview.Size = new System.Drawing.Size(837, 389);
            this.filesListview.TabIndex = 1;
            this.filesListview.UseCompatibleStateImageBehavior = false;
            this.filesListview.View = System.Windows.Forms.View.Details;
            this.filesListview.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ColumnClick);
            this.filesListview.SelectedIndexChanged += new System.EventHandler(this.filesListview_SelectedIndexChanged);
            this.filesListview.KeyUp += new System.Windows.Forms.KeyEventHandler(this.filesListview_KeyUp);
            // 
            // columnHeader0
            // 
            this.columnHeader0.Text = "Why";
            this.columnHeader0.Width = 70;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Location";
            this.columnHeader1.Width = 400;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Ext";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Size";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Status";
            // 
            // exportBtn
            // 
            this.exportBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportBtn.AutoSize = true;
            this.exportBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.exportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportBtn.ForeColor = System.Drawing.Color.White;
            this.exportBtn.Location = new System.Drawing.Point(229, 4);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(121, 25);
            this.exportBtn.TabIndex = 14;
            this.exportBtn.Text = "Export Files List...";
            this.toolTip.SetToolTip(this.exportBtn, "Export file list as CSV");
            this.exportBtn.UseVisualStyleBackColor = false;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.AutoSize = true;
            this.closeBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.ForeColor = System.Drawing.Color.White;
            this.closeBtn.Location = new System.Drawing.Point(826, 4);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 25);
            this.closeBtn.TabIndex = 15;
            this.closeBtn.Text = "Close";
            this.toolTip.SetToolTip(this.closeBtn, "Close Dialog");
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // readonlyCk
            // 
            this.readonlyCk.AutoSize = true;
            this.readonlyCk.ForeColor = System.Drawing.Color.Red;
            this.readonlyCk.Location = new System.Drawing.Point(405, 86);
            this.readonlyCk.Name = "readonlyCk";
            this.readonlyCk.Size = new System.Drawing.Size(105, 17);
            this.readonlyCk.TabIndex = 18;
            this.readonlyCk.Text = "Delete Readonly";
            this.toolTip.SetToolTip(this.readonlyCk, "If enabled it is okay to delete Readonly files.");
            this.readonlyCk.UseVisualStyleBackColor = true;
            // 
            // exportCsvDialog
            // 
            this.exportCsvDialog.DefaultExt = "csv";
            this.exportCsvDialog.Filter = "Csv|*.csv|Text|*.txt|All Files|*.*";
            this.exportCsvDialog.Title = "Export File List";
            // 
            // btnTable
            // 
            this.btnTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTable.ColumnCount = 4;
            this.btnTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.btnTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.btnTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.btnTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.btnTable.Controls.Add(this.closeBtn, 3, 0);
            this.btnTable.Controls.Add(this.StopBtn, 2, 0);
            this.btnTable.Controls.Add(this.exportBtn, 1, 0);
            this.btnTable.Controls.Add(this.DelFilesBtn, 0, 0);
            this.btnTable.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.btnTable.Location = new System.Drawing.Point(19, 525);
            this.btnTable.Name = "btnTable";
            this.btnTable.RowCount = 1;
            this.btnTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.btnTable.Size = new System.Drawing.Size(904, 32);
            this.btnTable.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Selection Filter (ex: *.xml):";
            // 
            // selFilterBox
            // 
            this.selFilterBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selFilterBox.HideSelection = false;
            this.selFilterBox.Location = new System.Drawing.Point(192, 103);
            this.selFilterBox.Name = "selFilterBox";
            this.selFilterBox.Size = new System.Drawing.Size(730, 20);
            this.selFilterBox.TabIndex = 17;
            this.selFilterBox.TextChanged += new System.EventHandler(this.selFilterBox_TextChanged);
            // 
            // iconBar
            // 
            this.iconBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.iconBar.AutoScroll = true;
            this.iconBar.Location = new System.Drawing.Point(18, 130);
            this.iconBar.Name = "iconBar";
            this.iconBar.Size = new System.Drawing.Size(62, 389);
            this.iconBar.TabIndex = 19;
            // 
            // ProgressMon
            // 
            this.ProgressMon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressMon.Location = new System.Drawing.Point(18, 102);
            this.ProgressMon.Maximum = 26;
            this.ProgressMon.Name = "ProgressMon";
            this.ProgressMon.Size = new System.Drawing.Size(905, 22);
            this.ProgressMon.TabIndex = 13;
            this.ProgressMon.Visible = false;
            // 
            // buttonMenu
            // 
            this.buttonMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.btnMenuHide,
            this.btnMenuRestore,
            this.menuItem4,
            this.btnMenuDelete});
            // 
            // btnMenuHide
            // 
            this.btnMenuHide.Index = 0;
            this.btnMenuHide.Text = "Hide these files in list";
            this.btnMenuHide.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // btnMenuRestore
            // 
            this.btnMenuRestore.Index = 1;
            this.btnMenuRestore.Text = "Restore hidden files";
            this.btnMenuRestore.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "-";
            // 
            // btnMenuDelete
            // 
            this.btnMenuDelete.Index = 3;
            this.btnMenuDelete.Text = "Delete files with this \'why\' tag";
            this.btnMenuDelete.Click += new System.EventHandler(this.btnMenuDelete_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(721, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Selected Items : ";
            // 
            // totalSelFiles
            // 
            this.totalSelFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.totalSelFiles.AutoSize = true;
            this.totalSelFiles.Location = new System.Drawing.Point(816, 86);
            this.totalSelFiles.Name = "totalSelFiles";
            this.totalSelFiles.Size = new System.Drawing.Size(13, 13);
            this.totalSelFiles.TabIndex = 21;
            this.totalSelFiles.Text = "0";
            // 
            // FileScan
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(948, 566);
            this.Controls.Add(this.iconBar);
            this.Controls.Add(this.readonlyCk);
            this.Controls.Add(this.btnTable);
            this.Controls.Add(this.filesListview);
            this.Controls.Add(this.totalSelFiles);
            this.Controls.Add(this.totalBytesLabel);
            this.Controls.Add(this.totalFilesLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ProgressMon);
            this.Controls.Add(this.selFilterBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FileScan";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Located Files";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ClosingEvent);
            this.Load += new System.EventHandler(this.LocateFiles);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.btnTable.ResumeLayout(false);
            this.btnTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        #region ==== Support methods
        /// <summary>
        /// Format size into Bytes, Kbytes or Mbytes.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string FormatSize(long val)
        {
            const long MByte = 1024 * 1024;
            if (val < 1024)
            {
                return val + " Bytes";
            }
            else if (val >= 1024 && val < MByte)
            {
                return val / 1024 + " KBytes";
            }
            else
            {
                return Math.Round((double)(val) / MByte, 2) + " MBytes";
            }
        }

        private void ClosingEvent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (working)
                e.Cancel = true;
        }

        #endregion

        #region ==== Thread to Locate Files
        bool working = false;
        bool isScanDone = false;
        bool cancel = false;
        const int sSizeCol = 4;
        const int sStatusCol = 5;
        bool haveReadOnly = false;

        Queue InfoQueue = new Queue();

        Thread thread;
		Timer timer;
        ArrayList subDirList;
        ArrayList extList;
        ArrayList patternList;
        ArrayList excludeList;
        ArrayList neverList;
        ListView hiddenFiles;

        class WhyInfo
        {
            public string why;
            public Color fg, bg;
        };
        Dictionary<string, WhyInfo> patternAndWhy = new Dictionary<string, WhyInfo>();
        Dictionary<WhyInfo, uint> whyCnt = new Dictionary<WhyInfo, uint>();

        /// <summary>
        /// Scan folders and isolated those matching filters.
        /// </summary>
		private void LocateFiles(object sender, System.EventArgs e)
		{
            subDirList = new ArrayList();
            extList = new ArrayList();
            patternList = new ArrayList();
            excludeList = new ArrayList();
            neverList = new ArrayList();

            ThreadWorking(false);

            // Separate filters into four types, exclude, directory, extension or wildcards
            //
            //  Syntax          Description         Examples
            //  -xxxxx          Exclude pattern     -*.exe  or   -*sdk/
            //  xxxxx/          Directory name      bin/    or   Release/     
            //  .xxx            File extension      .obj    or   .suo
            //  xxxx            wildcard            connect*.xml
            //
            foreach (ListViewItem item in this.filterList)
            {
                string actions = item.SubItems[1].Text.ToLower();
                string[] actionList = actions.Split(';');
                foreach (string action in actionList)
                {
                    if (action.Length == 0)
                        continue;
                    WhyInfo why = new WhyInfo() { why = item.Text, fg = item.ForeColor, bg = item.BackColor };
                    if (action[0] == '-')
                    {
                        if (item.Text.ToLower().StartsWith("never"))
                        {
                            neverList.Add(action);
                        }
                        else
                        {
                            excludeList.Add(action);
                            patternAndWhy.Add(action, why);
                        }
                    }
                    else
                    {
                        int n = action.Length;
                        if (n > 0 && action[n - 1] == '\\')
                        {
                            string d = action.TrimEnd('\\');
                            subDirList.Add(d);
                            patternAndWhy.Add(d, why);
                        }
                        else
                        {
                            if (action[0] == '.')
                                extList.Add(action);
                            else
                                patternList.Add(action);
                            patternAndWhy.Add(action, why);
                        }
                    }
                }
            }

            ThreadWorking(true);

            ProgressMon.Visible = true;
            ProgressMon.Minimum = 0;
            ProgressMon.Maximum = 100;
            haveReadOnly = false;

			thread = new Thread(new ThreadStart(scanFilesThread));
			thread.Start();
			timer = new Timer();
			timer.Tick+=new EventHandler(fileScanTimer_Tick);
			timer.Interval = 1000;
			timer.Enabled = true;
		}

        private void ThreadWorking(bool w)
        {
            this.working = w;
            this.StopBtn.Enabled = w;
            this.ProgressMon.Visible = w;

            // Disable a bunch of buttons while working.
            this.closeBtn.Enabled = !w;
            this.DelFilesBtn.Enabled = !w;
            this.exportBtn.Enabled = !w;

            UpdateColumnWidths();
        }

        private void UpdateColumnWidths()
        {
            if (filesListview.Items.Count == 0)
                this.filesListview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            else
            {
                this.filesListview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                this.filesListview.Invalidate();
                this.filesListview.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
                foreach (ColumnHeader colHeader in this.filesListview.Columns)
                {
                    if (colHeader.Width < colHeader.Text.Length * 12)
                        colHeader.Width = colHeader.Text.Length * 12;
                }
            }
        }

        private string errorMsg;

		private void scanFilesThread()
		{
			try
			{
                errorMsg = string.Empty;
                cancel = false;
                foreach (string path in this.pathList.Split(';'))
                {
                    if (Directory.Exists(path))
                        WalkFolder(path);
                }
			}
			catch (ThreadAbortException)
			{
                errorMsg = "Operation cancelled by user";
			}
			catch (Exception e)
			{
                errorMsg = "Error ocurred while scanning folders: " + e.Message;
			}
			finally
			{
                isScanDone = true;				
			}

            working = false;
		}

        /// <summary>
        /// File Find compare supports simple '*' wildcard pattern.
        /// Returns true if pattern matches even if raw text has more characters.
        /// </summary>
        /// <returns>true for partial match</returns>
        private bool WildCompare(string wildStr, int wildOff, string rawStr, int rawOff)
        {
            while (wildOff != wildStr.Length)
            {
                if (rawOff == rawStr.Length)
                    return (wildStr[wildOff] == '*');

                if (wildStr[wildOff] == '*')
                {
                    if (wildOff + 1 == wildStr.Length)
                        return true;

                    do
                    {
                        // Find match with char after '*'
                        while (rawOff < rawStr.Length &&
                            char.ToLower(rawStr[rawOff]) != char.ToLower(wildStr[wildOff + 1]))
                            rawOff++;
                        if (rawOff < rawStr.Length &&
                            WildCompare(wildStr, wildOff + 1, rawStr, rawOff))
                            return true;
                        ++rawOff;
                    } while (rawOff < rawStr.Length);

                    if (rawOff >= rawStr.Length)
                        return (wildStr[wildOff + 1] == '*');
                }
                else
                {
                    if (char.ToLower(rawStr[rawOff]) != char.ToLower(wildStr[wildOff]))
                        return false;
                    if (wildStr.Length == wildOff)
                        return true;
                    ++rawOff;
                }

                ++wildOff;
            }

            return true;
        }

        WhyInfo Why(string pattern)
        {
            WhyInfo whyInfo;
            patternAndWhy.TryGetValue(pattern, out whyInfo);
            return whyInfo;
        }

        const string neverStr = "never";
        bool Exclude(string file, string why)
        {
            foreach (string pattern in excludeList)
            {
                if ((Why(pattern).why == why || why.StartsWith(neverStr)) &&
                    WildCompare(pattern, 1, file, 0))
                {
                    return true;
                }
            }

            return false;
        }

		long TotalBytes = 0;
		long TotalFiles = 0;

		public void WalkFolder(string folder)
		{
            if (this.cancel)
                return; 

            string[] folderParts = folder.Split(Path.DirectorySeparatorChar);
            int n = folderParts.Length;
            string lastDir = folderParts[n - 1].ToLower();

            // Scan directories for match
            foreach (string dir in subDirList)
            {
                if (lastDir == dir && !Exclude(dir, Why(dir).why))              
                {
                    AddFileToList(folder, Why(dir));    // actually adding a directory
                    AddAllFilesInFolder(folder, Why(dir));
                    return;
                }
            }

            ArrayList fileList = new ArrayList();
            try
            {
                fileList.AddRange(Directory.GetFiles(folder, "*"));
            }
            catch { };

            // Scan files for match
            foreach (string file in fileList)
            {
                string fileLC = file.ToLower();
                string ext = Path.GetExtension(fileLC);

                if (extList.Contains(ext) && !Exclude(fileLC, Why(ext).why))
                {
                    AddFileToList(file, Why(ext));
                    continue;
                }

                foreach (string pattern in patternList)
                {
                    if (WildCompare(pattern, 0, fileLC, 0) && !Exclude(fileLC, Why(pattern).why))
                    {
                        AddFileToList(file, Why(pattern));
                        break;
                    }
                }
            }

			string[] directories = Directory.GetDirectories(folder);
			foreach (string directory in directories)
			{
				try
				{
					WalkFolder(directory);
				}
				catch (Exception e)
				{
					Console.Write(e);
				}
			}
		}

        public ArrayList GetAllFiles(string path)
        {
            ArrayList result = new ArrayList();
            result.AddRange(Directory.GetFiles(path, "*"));
            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                result.Add(directory); 
                result.AddRange(GetAllFiles(directory));
            }
            return result;
        }

        private void AddAllFilesInFolder(string path, WhyInfo why)
		{
			ArrayList allfiles = GetAllFiles(path);
			foreach (string s in allfiles)
			{
				AddFileToList(s, why);				
			}
		}

		private void AddFileToList(string file, WhyInfo why)
		{
            foreach (string p in neverList)
            {
                if (WildCompare(p, 1, file, 0))
                    return;
            }

			lock(InfoQueue.SyncRoot)
			{
                FileWhy fw = new FileWhy();
                fw.file = file;
                fw.why  = why;
                InfoQueue.Enqueue(fw);
			}
		}

        class FileWhy
        {
            public string file;
            public WhyInfo why;
        };


        /// <summary>
        /// Copy background files into foreground ListView
        /// </summary>
        private void fileScanTimer_Tick(object sender, EventArgs e)
		{
            ProgressMon.Value = (ProgressMon.Value + 1) % 100;

            lock(InfoQueue.SyncRoot)
            {
                this.filesListview.BeginUpdate();
				while (InfoQueue.Count > 0)
            	{
					//get the string info
                    FileWhy fw = InfoQueue.Dequeue() as FileWhy;
					
					//create the listviewitem
                    ListViewItem item = filesListview.Items.Add(fw.why.why);  // 0
                    item.SubItems.Add(Path.GetDirectoryName(fw.file));    // 1
                    item.SubItems.Add(Path.GetFileName(fw.file));         // 2
                    item.SubItems.Add(Path.GetExtension(fw.file));        // 3
                    item.SubItems.Add("0");       // length               // 4
                    item.SubItems.Add(" ");       // status               // 5
                    item.BackColor = fw.why.bg;
                    item.ForeColor = fw.why.fg;

                    item.SubItems[0].Tag = fw.why;
                    item.Tag = fw.file;

                    if (whyCnt.ContainsKey(fw.why))
                        whyCnt[fw.why] += 1;
                    else
                        whyCnt.Add(fw.why, 1);

            		try
            		{
                        FileInfo info = new FileInfo(fw.file);
                        if (info.Exists)
                        {
                            // TotalBytes += info.Length;
                            item.SubItems[sSizeCol].Text = info.Length.ToString();
                            if ((info.Attributes & FileAttributes.ReadOnly) ==
                                FileAttributes.ReadOnly)
                            {
                                item.SubItems[sStatusCol].Text = "r/o";
                                this.haveReadOnly = true;
                            }
                        }
                        else
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(fw.file);
                            if (dirInfo.Exists)
                            {
                                // Directory item
                                item.SubItems[sStatusCol].Text = "dir";
                                if (item.BackColor == filesListview.BackColor &&
                                    item.ForeColor == filesListview.ForeColor)
                                    item.BackColor = Color.LightBlue;
                            }
                        }
            		}
            		catch (Exception e2)
            		{
                        // Unknown file object
                        item.ForeColor = Color.Red;
                        item.BackColor = filesListview.BackColor;
                        item.SubItems[sStatusCol].Text = e2.Message;
            		}

                    // TotalFiles++;
                    AddToTotals(item);
                    if (maxFileList > 0 && TotalFiles >= maxFileList)
                    {
                        item = filesListview.Items.Add("Limit");  // why
                        item.ForeColor = Color.Red;
                        item.BackColor = filesListview.BackColor;
                        item.Tag = string.Empty;
                        item.SubItems.Add("File Limit reached, see options menu");
                        item.SubItems.Add(" ");  // file
                        item.SubItems.Add(" ");  // ext
                        item.SubItems.Add("0");  // size
                        item.SubItems.Add(" ");  // status
                        stopBtn_Click(null, EventArgs.Empty);
                        break;
                    }

					// totalBytesLabel.Text = FormatSize(TotalBytes);
					// totalFilesLabel.Text = TotalFiles.ToString();
            	}
                this.filesListview.EndUpdate();
            }

            if (!working)
            {
                ThreadWorking(working);
                timer.Stop();

                if (isScanDone)
                {
                    if (errorMsg.Length != 0)
                    {
                        bool isError = errorMsg.StartsWith("Error");
                        MessageBox.Show(errorMsg, isError ? "Error" : "Info", 
                            MessageBoxButtons.OK, 
                            isError ? MessageBoxIcon.Error : MessageBoxIcon.Information);
                    }

                    if (TotalFiles > 0)
                    {
                        DelFilesBtn.Enabled = exportBtn.Enabled = closeBtn.Enabled  = true;
                        infoLbl.Visible = true;
                    }
                    else
                    {
                        isScanDone = false;
                        MessageBox.Show(this, "There are no files to be deleted on this folder", "Ups!");
                        this.Close();
                    }

                    UpdateIconBar();
                    readonlyCk.Visible = this.haveReadOnly;
                }
            }
        }

        const int sHideMenuIdx = 0;
        const int sRestoreMenuIdx = 1;
        const int sDeleteMenuIdx = 3;

        /// <summary>
        /// Clean and Populate icon bar with latest item counts by 'why' classification.
        /// </summary>
        private void UpdateIconBar()
        {
            this.iconBar.Controls.Clear();

            foreach (WhyInfo why in whyCnt.Keys)
            {
                // TODO - Add click event handler and update FileListview...

                CheckBox button = new CheckBox();
                button.Checked = true;
                button.AutoCheck = false;
                button.Appearance = Appearance.Button;
                button.BackColor = why.bg;
                button.ForeColor = why.fg;
                button.Text = whyCnt[why].ToString();
                button.Size = new System.Drawing.Size(46, 46);
                button.Click += new EventHandler(whyButton_Click);
                button.Tag = why;

                ContextMenu btnContextMenu = new System.Windows.Forms.ContextMenu();
                btnContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                    this.btnMenuHide.CloneMenu(),
                    this.btnMenuRestore.CloneMenu(),
                    this.menuItem4.CloneMenu(),
                    this.btnMenuDelete.CloneMenu()});

                btnContextMenu.MenuItems[sHideMenuIdx].Text = "Hide " + button.Text + " " + why.why;
                btnContextMenu.MenuItems[sRestoreMenuIdx].Text = "Restore " + button.Text + " " + why.why;
                btnContextMenu.MenuItems[sDeleteMenuIdx].Text = "Delete files with why set to " + why.why;
                btnContextMenu.MenuItems[sRestoreMenuIdx].Enabled = false;

                button.ContextMenu = btnContextMenu;

                this.toolTip.SetToolTip(button, "Toggle to hide/show " + button.Text + " " + why.why);
                this.iconBar.Controls.Add(button);
            }
        }

        void whyButton_Click(object sender, EventArgs e)
        {
            CheckBox button = sender as CheckBox;
            WhyInfo why = button.Tag as WhyInfo;
            button.Checked = !button.Checked;

            this.filesListview.BeginUpdate();
            if (button.Checked)
            {
                // Restore 'why' group and update total disk space counters.
                button.ContextMenu.MenuItems[sHideMenuIdx].Enabled = true;
                button.ContextMenu.MenuItems[sRestoreMenuIdx].Enabled = false;

                for (int idx = this.hiddenFiles.Items.Count - 1; idx >= 0; idx--)
                {
                    ListViewItem item = this.hiddenFiles.Items[idx];
                    if (item.SubItems[0].Text == why.why)
                    {
                        this.hiddenFiles.Items.RemoveAt(idx);
                        this.filesListview.Items.Add(item);
                        AddToTotals(item);
                    }
                }
            }
            else
            {
                // Hide 'why' group and reduce total disk space counters.
                button.ContextMenu.MenuItems[sHideMenuIdx].Enabled = false;
                button.ContextMenu.MenuItems[sRestoreMenuIdx].Enabled = true;

                for (int idx = this.filesListview.Items.Count - 1; idx >= 0; idx--)
                {
                    ListViewItem item = this.filesListview.Items[idx];
                    if (item.SubItems[0].Text == why.why)
                    {
                        this.filesListview.Items.RemoveAt(idx);
                        RemoveFromTotals(item);
                        this.hiddenFiles.Items.Add(item);
                    }
                }
            }

            UpdateColumnWidths();
            this.filesListview.EndUpdate();
        }

        // Stop action (background thread or foreground deletion)
		private void stopBtn_Click(object sender, System.EventArgs e)
		{
            cancel = true;
            // if (thread.IsAlive)
			//    thread.Abort();
			// timer.Enabled = false;
        }
        #endregion

        #region ==== Delete file methods
        private void deleteBtn_Click(object sender, System.EventArgs e)
		{
			string msg = string.Format("You are about to delete {0} files and free up {1} of space, do you want to proceed?",
                TotalFiles,FormatSize(TotalBytes));
			if (DialogResult.Yes != 
                MessageBox.Show(this, msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) 
                return;

            DeleteFiles();
        }

        public void DeleteFiles()
        {
            ThreadWorking(true);
            cancel = false;
			DelFilesBtn.Enabled = exportBtn.Enabled = closeBtn.Enabled = false;
            ProgressMon.Minimum = 0;
            ProgressMon.Maximum = filesListview.Items.Count + 1;

            DeleteFiles(false);

            ThreadWorking(false);
        }

		private void DeleteFiles(bool onlySelected)
		{
            this.filesListview.BeginUpdate();
            try
			{
                bool anyDir = false;
                int itemCnt = onlySelected ? this.filesListview.SelectedItems.Count : this.filesListview.Items.Count;
                int fileIdx = 0;

                // Delete files.
				foreach (ListViewItem item in filesListview.Items)
				{
                    if (onlySelected && item.Selected == false)
                        continue;

                    // Allow window event such as pressing cancel button to get processed.
                    Application.DoEvents();

					if (cancel) 
                        return;
					try
					{
                        if (item.SubItems[sStatusCol].Text != "dir")
                        {
                            ProgressMon.Value = (++fileIdx * ProgressMon.Maximum / itemCnt);

                            string filePath = item.Tag as string;
                            FileInfo fileInfo = new FileInfo(filePath);
                            if (readonlyCk.Checked &&
                                (fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                            {
                                File.SetAttributes(filePath, fileInfo.Attributes ^ FileAttributes.ReadOnly);
                            }

#if ANYCPU
                            File.Delete(filePath);
#else
                            RecycleFile.DeleteFile(filePath);
#endif
                            RemoveFileFromList(item);
                        }
                        else
                            anyDir = true;
					}
					catch (Exception e)
					{
                        // Failed to delete file, maybe in use or protected.
						item.ForeColor = Color.Red;
                        item.BackColor = filesListview.BackColor;
                        if (item.SubItems.Count < 6)
                            item.SubItems.Add(e.Message);
                        else
                            item.SubItems[sStatusCol].Text = e.Message;
					}
				}

                if (anyDir)
                {
                    // sort by directory name in decending order
                    ListViewColumnSorter sorter = this.filesListview.ListViewItemSorter as ListViewColumnSorter;
                    sorter.Order = SortOrder.Descending;
                    sorter.SortColumn = 1;  // directory column
                    this.filesListview.Sort();

                    int dirIdx = 0;

                    // Delete Directories
                    foreach (ListViewItem item in filesListview.Items)
                    {
                        if (onlySelected && filesListview.SelectedItems.Contains(item) == false)
                            continue;

                        // Allow window event such as pressing cancel button to get processed.
                        Application.DoEvents();

                        // ProgressMon.Value = (ProgressMon.Value + 1) % ProgressMon.Maximum;

                        if (cancel)
                            return;
                        try
                        {
                            if (item.SubItems[sStatusCol].Text == "dir")
                            {
                                ProgressMon.Value = ((++dirIdx + fileIdx) * ProgressMon.Maximum / itemCnt);

                                if (Directory.Exists(item.Tag as string))
                                {
#if ANYCPU
                                    Directory.Delete(item.Tag as string);
#else
                                    RecycleFile.DeleteFile(item.Tag as string);
#endif
                                }
                                RemoveFileFromList(item);
                            }
                        }
                        catch (Exception e)
                        {
                            // Failed to delete file, maybe in use or protected.
                            item.ForeColor = Color.Red;
                            item.BackColor = filesListview.BackColor;
                            if (item.SubItems.Count < 6)
                                item.SubItems.Add(e.Message);
                            else
                                item.SubItems[sStatusCol].Text = e.Message;
                        }
                    }
                }
			}
			finally
			{
                ThreadWorking(false);

                if (cancel || filesListview.Items.Count > 0)
				{
                    DelFilesBtn.Enabled = exportBtn.Enabled = closeBtn.Enabled = true;
					ProgressMon.Visible = false;
					ProgressMon.Minimum = 0;
                    ProgressMon.Maximum = filesListview.Items.Count + 1;
				}
				else
				{
					infoLbl.Text = "Done deleting files";
				}
			}
            this.filesListview.EndUpdate();
            UpdateIconBar();
        }

        private void RemoveFromTotals(ListViewItem item)
        {
            if (item.SubItems[sStatusCol].Text != "dir")
                TotalFiles--;

            try
            {
                if (item.SubItems[sSizeCol].Text != null)
                    TotalBytes -= int.Parse(item.SubItems[sSizeCol].Text);
            }
            catch { }

            totalBytesLabel.Text = FormatSize(TotalBytes);
            totalFilesLabel.Text = TotalFiles.ToString();
        }

        private void AddToTotals(ListViewItem item)
        {
            // if (item.SubItems[sStatusCol].Text != "dir")
                TotalFiles++;

            try
            {
                if (item.SubItems[sSizeCol].Text != null)
                    TotalBytes += int.Parse(item.SubItems[sSizeCol].Text);
            }
            catch { }

            totalBytesLabel.Text = FormatSize(TotalBytes);
            totalFilesLabel.Text = TotalFiles.ToString();
        }

        #region ==== Context popup menu items
        private void RemoveFileFromList(ListViewItem item)
		{
            RemoveFromTotals(item);

            WhyInfo why = item.SubItems[0].Tag as WhyInfo;
            whyCnt[why] -= 1;
            // UpdateIconBar();

            filesListview.Items.Remove(item);
        }

        private void RemoveMenuItem_Click(object sender, System.EventArgs e)
        {
            this.filesListview.BeginUpdate();
            foreach (ListViewItem item in filesListview.SelectedItems)
            {
                RemoveFileFromList(item);
            }
            this.filesListview.EndUpdate();

            UpdateIconBar();
        }

        private void RemoveWhyGrpMenuItem_Click(object sender, EventArgs e)
        {
            if (filesListview.SelectedItems.Count != 0)
            {
                string why = filesListview.SelectedItems[0].Text;

                this.filesListview.BeginUpdate();
                for (int idx = this.filesListview.Items.Count - 1; idx >= 0; idx--)
                {
                    ListViewItem item = this.filesListview.Items[idx];
                    if (item.SubItems[0].Text == why)
                    {
                        RemoveFileFromList(item);
                    }
                }
                this.filesListview.EndUpdate();
                UpdateIconBar();
            }
        }

        private void InvMenuItem_Click(object sender, EventArgs e)
        {
            if (filesListview.SelectedItems.Count != 0)
            {
                this.selFilterBox.Text = "";
                this.doSelectChange = false;

                foreach (ListViewItem item in filesListview.Items)
                {
                    item.Selected = !filesListview.SelectedItems.Contains(item);
                }
                this.doSelectChange = true;
            }
        }

        // Deleted selected items 'now'.
        private void DeleteNowMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "Are you sure you want to delete " +
                filesListview.SelectedItems.Count.ToString() + " selected items";

           if (DialogResult.Yes != 
                MessageBox.Show(this, msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) 
                return;

            DeleteFiles(true);
        }


        private void deleteWhyGroMenuItem_Click(object sender, EventArgs e)
        {
            if (filesListview.SelectedItems.Count != 0)
            {
                string why = filesListview.SelectedItems[0].Text;
                DeleteWhyGroup(why);
            }
        }

        private void DeleteWhyGroup(string why)
        {
            this.filesListview.BeginUpdate();
            filesListview.SelectedItems.Clear();
            for (int idx = this.filesListview.Items.Count - 1; idx >= 0; idx--)
            {
                ListViewItem item = this.filesListview.Items[idx];
                if (item.SubItems[0].Text == why)
                {
                    item.Selected = true;
                }
            }
            this.filesListview.EndUpdate();

            if (filesListview.SelectedItems.Count != 0)
            {
                string msg = "Are you sure you want to delete the " +
                    filesListview.SelectedItems.Count.ToString() + " why='" + why + "' items";

                if (DialogResult.Yes !=
                        MessageBox.Show(this, msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return;

                ProgressMon.Minimum = 0;
                ProgressMon.Maximum = filesListview.SelectedItems.Count;
                ProgressMon.Visible = true;
                DeleteFiles(true);
                this.ProgressMon.Visible = false;
            }
        }

        private void LaunchExplorer_Click(object sender, EventArgs e)
        {
            if (filesListview.SelectedItems.Count != 0)
            {
                ListViewItem item = filesListview.SelectedItems[0];
                string dir = Path.GetDirectoryName(item.Tag as string);
                System.Diagnostics.Process.Start("explorer.exe", dir);
            }
        }

        private void LaunchCmd_Click(object sender, EventArgs e)
        {
            if (filesListview.SelectedItems.Count != 0)
            {
                ListViewItem item = filesListview.SelectedItems[0];
                string dir = Path.GetDirectoryName(item.Tag as string);
                System.Diagnostics.Process.Start("cmd.exe", "/k cd \"" + dir + "\"");
            }
        }
        #endregion

 

		private void filesListview_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete) 
                RemoveMenuItem_Click(null, null);
            else if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                selFilterBox.Text = "";
                foreach (ListViewItem item in filesListview.Items)
                {
                    item.Selected = true;
                }
            }
        }

        #endregion

        /// <summary>
        /// Column header click fires sort.
        /// </summary>
        private void ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView listView = sender as ListView;
            ListViewColumnSorter sorter = listView.ListViewItemSorter as ListViewColumnSorter;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == sorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (sorter.Order == SortOrder.Ascending)
                    sorter.Order = SortOrder.Descending;
                else
                    sorter.Order = SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            if (listView != null)
            {
                this.Cursor = Cursors.WaitCursor;
                listView.BeginUpdate();
                listView.Enabled = false;
                listView.Sort();
                listView.Enabled = true;
                listView.EndUpdate();
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        ///  Save ListView contents in as a CSV file.
        /// </summary>
        private void exportBtn_Click(object sender, EventArgs e)
        {
            if (filesListview == null || filesListview.Items.Count == 0)
                return;

            if (this.exportCsvDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = this.exportCsvDialog.FileName;
                TextWriter writer = new StreamWriter(filePath);

                string txtLine = string.Empty;
                foreach (ColumnHeader ch in filesListview.Columns)
                {
                    if (txtLine.Length != 0)
                        txtLine += ",";
                    txtLine += ch.Text;
                }
                writer.WriteLine(txtLine);

                foreach (ListViewItem item in filesListview.Items)
                {
                    txtLine = string.Empty;
                    foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                    {
                        if (txtLine.Length != 0)
                            txtLine += ",";
                        txtLine += subItem.Text.Replace(',', ';');
                    }

                    writer.WriteLine(txtLine);
                }

                writer.Close();

                infoLbl.Text = "Saved list to file:" + filePath;
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool isSearching = false;
        bool stopSearch = false;
        private void selFilterBox_TextChanged(object sender, EventArgs e)
        {
            if (selFilterBox.Text.Length == 0 || selFilterBox.Text == "*")
                return;     // ignore empty filter or all match filter.

            stopSearch = true;
            while (isSearching)
                Application.DoEvents();
            
            filesListview.SelectedItems.Clear();
            FilterSelect();
        }

        private void FilterSelect()
        {
            if (selFilterBox.Text.Length == 0 || selFilterBox.Text == "*")
                return;

            doSelectChange = false;
            string[] searchList = selFilterBox.Text.Split(';');

            filesListview.BeginUpdate();
            isSearching = true;
            stopSearch = false;

            foreach (string searchFor in searchList)
            {
                if (stopSearch)
                    break;

                foreach (ListViewItem item in filesListview.Items)
                {
                    if (stopSearch)
                        break;

                    bool foundIt = false;
                    foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                    {
                        if (subItem.Text.Length != 0 &&
                            (foundIt = WildCompare(searchFor, 0, subItem.Text, 0)))
                        {
                            break;
                        }
                    }

                    if (foundIt)
                    {
                        item.Selected = true;
                    }
                }
            }

            filesListview.EndUpdate();
            isSearching = false;
            doSelectChange = true;
        }


        private bool doSelectChange = true;
        private void filesListview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (doSelectChange)
                FilterSelect();
            this.totalSelFiles.Text = filesListview.SelectedItems.Count.ToString("N0");
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenu that contains this MenuItem
                ContextMenu menu = menuItem.GetContextMenu();

                // Get the control that is displaying this context menu
                Control sourceControl = menu.SourceControl;
                if (sourceControl != null && sourceControl.Tag != null)
                {
                    WhyInfo whyInfo = sourceControl.Tag as WhyInfo;
                    whyButton_Click(sourceControl, e);
                }
            }
        }

        private void btnMenuDelete_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenu that contains this MenuItem
                ContextMenu menu = menuItem.GetContextMenu();

                // Get the control that is displaying this context menu
                Control sourceControl = menu.SourceControl;
                if (sourceControl != null && sourceControl.Tag != null)
                {
                    WhyInfo whyInfo = sourceControl.Tag as WhyInfo;
                    DeleteWhyGroup(whyInfo.why);
                }
            }
        }

        private void infoLbl_MouseEnter(object sender, EventArgs e)
        {
            this.infoLbl.BackColor = Color.Gray;
        }

        private void infoLbl_MouseLeave(object sender, EventArgs e)
        {
            this.infoLbl.BackColor = Color.Transparent;
        }
	}
}
