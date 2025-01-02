using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32;

namespace VStudioCleaner_ns
{
    /// <summary>
    /// Author: Dennis Lang - 2009
    /// https://landenlabs.com/
    /// 
    /// This application is based off of Leonardo Paneque "Solution Cleaner"
    /// http://www.teknowmagic.net/
    /// 
    /// Application to scan folders for files which match filters and allow them to be deleted.
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Panel titlePanel;
        private System.Windows.Forms.TextBox FolderBox;
        private System.Windows.Forms.Label InfoHelp;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button LocateFilesBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem RegisterOnShellMenuItem;
        public System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem menuItem3;
        public System.Windows.Forms.MenuItem AboutMenuItem;
        private System.Windows.Forms.MenuItem menuItem2;
        private ToolTip toolTip;
        private Label activeBookmarkTx;
        private Button pathBtn;
        private FolderBrowserDialog pathDialog;
        private ListView filterListView;
        private ColumnHeader colhdr1;
        private ColumnHeader colHdr2;
        private Button addFilterBtn;
        private Button delFilterBtn;
        private ColumnHeader colHdr3;
        private IContainer components;
        private ListView bookmarkList;
        private GroupBox bookmarkGrp;
        private SplitContainer mainSplitter;
        private ColumnHeader columnHeader1;
        private Button delBmBtn;
        private Button addBmBtn;
        private Button setBmBtn;
        private SaveFileDialog exportCsvDialog;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem exportAsCSVToolStripMenuItem;
        private Panel mainPanel;
        private ToolStripContainer toolStripContainer1;
        private MenuItem limitLocateTo1000;
        private MenuItem resetFilter;
        private MenuItem regeditSettings;
        private Button helpBtn;
        private MenuItem menuItem4;
        private ColumnHeader color;
        private ToolStripMenuItem renameToolStripMenuItem;
        private Microsoft.Win32.RegistryKey regKey;

        private bool cmdOpenLocate = false;
        private bool cmdDelete = false;

        /// <summary>
        /// Optional command line path[;path]... [@locate | @delete]
        /// Where @locate will automatically press the Locate Files button.
        /// Where @delete will automatically press the delete button on the located files.
        /// 
        /// </summary>
        /// <param name="cmdLineArgs"></param>
        public MainForm(string[] cmdLineArgs)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            // A little more ui setup work.
            this.FolderBox.Text = Environment.CurrentDirectory;
            this.filterListView.ListViewItemSorter = new ListViewColumnSorter(ListViewColumnSorter.SortDataType.eAlpha);
            this.bookmarkList.ListViewItemSorter = new ListViewColumnSorter(ListViewColumnSorter.SortDataType.eAlpha);
            this.toolTip.SetToolTip(this.titlePanel, ProductNameAndVersion());
            this.activeBookmarkTx.Text = ProductNameAndVersion();
            this.Text = ProductNameAndVersion();

            // If AssemblyVersion is n.m.*, then third number is julian date with 0= 1-Jan-2000
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build);

            this.Text += "  Build:" + buildDate.ToString("dd-MMM-yyyy");

            // Load previous settings saved in registry
            string appName = Application.ProductName;
            this.regKey =Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\" + appName);
            this.LoadFromRegistry();

            // Set bindings in buttons which use shared methods.
            this.addBmBtn.Tag = this.bookmarkList;
            this.addFilterBtn.Tag = this.filterListView;
            this.delBmBtn.Tag = this.bookmarkList;
            this.delFilterBtn.Tag = this.filterListView;

            // Lastly parse any command line arguments as path values.
            if (cmdLineArgs.Length != 0)
            {
                string paths = "";
                for (int argIdx = 0; argIdx < cmdLineArgs.Length; argIdx++)
                {
                    if (Directory.Exists(cmdLineArgs[argIdx]))
                    {
                        if (paths.Length != 0)
                            paths += ";";
                        paths += cmdLineArgs[argIdx];
                    }
                    else
                    {
                        if (cmdLineArgs[argIdx].ToLower().Equals("@locate"))
                            this.cmdOpenLocate = true;
                        if (cmdLineArgs[argIdx].ToLower().Equals("@delete"))
                            this.cmdDelete = true;
                    }
                }
                SetPath(paths);
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveToRegistry();
            base.OnClosing(e);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.LocateFilesBtn = new System.Windows.Forms.Button();
			this.titlePanel = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.activeBookmarkTx = new System.Windows.Forms.Label();
			this.FolderBox = new System.Windows.Forms.TextBox();
			this.InfoHelp = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.limitLocateTo1000 = new System.Windows.Forms.MenuItem();
			this.resetFilter = new System.Windows.Forms.MenuItem();
			this.regeditSettings = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.RegisterOnShellMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.AboutMenuItem = new System.Windows.Forms.MenuItem();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.filterListView = new System.Windows.Forms.ListView();
			this.colhdr1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colHdr2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colHdr3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.color = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.exportAsCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addFilterBtn = new System.Windows.Forms.Button();
			this.delFilterBtn = new System.Windows.Forms.Button();
			this.pathBtn = new System.Windows.Forms.Button();
			this.bookmarkList = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.addBmBtn = new System.Windows.Forms.Button();
			this.delBmBtn = new System.Windows.Forms.Button();
			this.setBmBtn = new System.Windows.Forms.Button();
			this.helpBtn = new System.Windows.Forms.Button();
			this.pathDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.bookmarkGrp = new System.Windows.Forms.GroupBox();
			this.mainSplitter = new System.Windows.Forms.SplitContainer();
			this.exportCsvDialog = new System.Windows.Forms.SaveFileDialog();
			this.mainPanel = new System.Windows.Forms.Panel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.titlePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel2.SuspendLayout();
			this.contextMenu.SuspendLayout();
			this.bookmarkGrp.SuspendLayout();
			this.mainSplitter.Panel1.SuspendLayout();
			this.mainSplitter.Panel2.SuspendLayout();
			this.mainSplitter.SuspendLayout();
			this.mainPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// LocateFilesBtn
			// 
			this.LocateFilesBtn.AutoSize = true;
			this.LocateFilesBtn.BackColor = System.Drawing.SystemColors.Highlight;
			this.LocateFilesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.LocateFilesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LocateFilesBtn.ForeColor = System.Drawing.Color.White;
			this.LocateFilesBtn.Location = new System.Drawing.Point(182, 2);
			this.LocateFilesBtn.Name = "LocateFilesBtn";
			this.LocateFilesBtn.Size = new System.Drawing.Size(124, 25);
			this.LocateFilesBtn.TabIndex = 4;
			this.LocateFilesBtn.Text = "Locate Files...";
			this.toolTip.SetToolTip(this.LocateFilesBtn, "Select filter tags then press \"Locate\" files button. Located file list dialog wil" +
        "l appear where you can delete files.\r\n");
			this.LocateFilesBtn.UseVisualStyleBackColor = false;
			this.LocateFilesBtn.Click += new System.EventHandler(this.LocateFilesBtn_Click);
			this.LocateFilesBtn.MouseEnter += new System.EventHandler(this.ShowHelp);
			// 
			// titlePanel
			// 
			this.titlePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.titlePanel.BackColor = System.Drawing.Color.White;
			this.titlePanel.BackgroundImage = global::VStudioCleaner_ns.Properties.Resources.blackBubbles2;
			this.titlePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.titlePanel.Controls.Add(this.pictureBox1);
			this.titlePanel.Location = new System.Drawing.Point(0, 0);
			this.titlePanel.Name = "titlePanel";
			this.titlePanel.Size = new System.Drawing.Size(975, 68);
			this.titlePanel.TabIndex = 7;
			this.toolTip.SetToolTip(this.titlePanel, "Bubbles stupid");
			this.titlePanel.Click += new System.EventHandler(this.ShowAboutDialog);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pictureBox1.Location = new System.Drawing.Point(23, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(91, 61);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Click += new System.EventHandler(this.ShowAboutDialog);
			// 
			// activeBookmarkTx
			// 
			this.activeBookmarkTx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.activeBookmarkTx.BackColor = System.Drawing.Color.Silver;
			this.activeBookmarkTx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.activeBookmarkTx.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.activeBookmarkTx.ForeColor = System.Drawing.Color.Red;
			this.activeBookmarkTx.Location = new System.Drawing.Point(350, 4);
			this.activeBookmarkTx.Name = "activeBookmarkTx";
			this.activeBookmarkTx.Size = new System.Drawing.Size(275, 22);
			this.activeBookmarkTx.TabIndex = 0;
			this.toolTip.SetToolTip(this.activeBookmarkTx, "Active Bookmark");
			this.activeBookmarkTx.MouseEnter += new System.EventHandler(this.ShowHelp);
			// 
			// FolderBox
			// 
			this.FolderBox.AllowDrop = true;
			this.FolderBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FolderBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.FolderBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.FolderBox.BackColor = System.Drawing.Color.White;
			this.FolderBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.FolderBox.Location = new System.Drawing.Point(64, 77);
			this.FolderBox.Name = "FolderBox";
			this.FolderBox.Size = new System.Drawing.Size(908, 20);
			this.FolderBox.TabIndex = 8;
			this.toolTip.SetToolTip(this.FolderBox, "List of folders separated by semicolons.  Ex:  \\foo;\\master\\subdir;d:\\alt\\dir");
			this.FolderBox.TextChanged += new System.EventHandler(this.FolderBox_TextChanged);
			this.FolderBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.FolderBox_DragDrop);
			this.FolderBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.FolderBox_DragEnter);
			this.FolderBox.MouseEnter += new System.EventHandler(this.ShowHelp);
			this.FolderBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FolderBox_PreviewKeyDown);
			// 
			// InfoHelp
			// 
			this.InfoHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
			this.InfoHelp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InfoHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.InfoHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.InfoHelp.Location = new System.Drawing.Point(3, 3);
			this.InfoHelp.Margin = new System.Windows.Forms.Padding(0);
			this.InfoHelp.Name = "InfoHelp";
			this.InfoHelp.Size = new System.Drawing.Size(322, 97);
			this.InfoHelp.TabIndex = 9;
			this.InfoHelp.Text = "Delete Visual Studio temporary and optionally build results to keep your disk cle" +
    "an. Toggle  selection filters then press Locate Files. ";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.Add(this.InfoHelp);
			this.panel2.Location = new System.Drawing.Point(6, 3);
			this.panel2.Margin = new System.Windows.Forms.Padding(0);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(3);
			this.panel2.Size = new System.Drawing.Size(328, 103);
			this.panel2.TabIndex = 10;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.AboutMenuItem});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.limitLocateTo1000,
            this.resetFilter,
            this.regeditSettings,
            this.menuItem4,
            this.RegisterOnShellMenuItem,
            this.menuItem2,
            this.menuItem3});
			this.menuItem1.Text = "Options";
			// 
			// limitLocateTo1000
			// 
			this.limitLocateTo1000.Checked = true;
			this.limitLocateTo1000.Index = 0;
			this.limitLocateTo1000.Text = "Limit Located files to 1000";
			this.limitLocateTo1000.Click += new System.EventHandler(this.limitLocateTo1000_Click);
			// 
			// resetFilter
			// 
			this.resetFilter.Index = 1;
			this.resetFilter.Text = "Reset Filter List";
			this.resetFilter.Click += new System.EventHandler(this.resetFilters_Click);
			// 
			// regeditSettings
			// 
			this.regeditSettings.Index = 2;
			this.regeditSettings.Text = "Open Visual Studio Cleaner Registry";
			this.regeditSettings.Click += new System.EventHandler(this.openRegistry_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 3;
			this.menuItem4.Text = "Open explorer menu registry";
			this.menuItem4.Click += new System.EventHandler(this.openRegistryShell_Click);
			// 
			// RegisterOnShellMenuItem
			// 
			this.RegisterOnShellMenuItem.Index = 4;
			this.RegisterOnShellMenuItem.Text = "Register on  Explorer menu";
			this.RegisterOnShellMenuItem.Click += new System.EventHandler(this.RegisterOnShellMenuItem_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 5;
			this.menuItem2.Text = "-";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 6;
			this.menuItem3.Text = "Close";
			this.menuItem3.Click += new System.EventHandler(this.closeBtn_Click);
			// 
			// AboutMenuItem
			// 
			this.AboutMenuItem.Index = 1;
			this.AboutMenuItem.Text = "About";
			this.AboutMenuItem.Click += new System.EventHandler(this.ShowAboutDialog);
			// 
			// filterListView
			// 
			this.filterListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.filterListView.CheckBoxes = true;
			this.filterListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colhdr1,
            this.colHdr2,
            this.colHdr3,
            this.color});
			this.filterListView.ContextMenuStrip = this.contextMenu;
			this.filterListView.FullRowSelect = true;
			this.filterListView.GridLines = true;
			this.filterListView.HideSelection = false;
			this.filterListView.Location = new System.Drawing.Point(3, 32);
			this.filterListView.Name = "filterListView";
			this.filterListView.Size = new System.Drawing.Size(627, 381);
			this.filterListView.TabIndex = 12;
			this.toolTip.SetToolTip(this.filterListView, "List of Search Tags");
			this.filterListView.UseCompatibleStateImageBehavior = false;
			this.filterListView.View = System.Windows.Forms.View.Details;
			this.filterListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.SortColumn_Click);
			this.filterListView.SelectedIndexChanged += new System.EventHandler(this.filterListView_SelectedIndexChanged);
			this.filterListView.DoubleClick += new System.EventHandler(this.ListViewDbl_Click);
			this.filterListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListView_KeyUp);
			this.filterListView.MouseEnter += new System.EventHandler(this.ShowHelp);
			this.filterListView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveEvent);
			// 
			// colhdr1
			// 
			this.colhdr1.Text = "Filter";
			this.colhdr1.Width = 100;
			// 
			// colHdr2
			// 
			this.colHdr2.Text = "Detail";
			this.colHdr2.Width = 300;
			// 
			// colHdr3
			// 
			this.colHdr3.Text = "Description";
			this.colHdr3.Width = 400;
			// 
			// color
			// 
			this.color.Text = "Color";
			this.color.Width = 0;
			// 
			// contextMenu
			// 
			this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAsCSVToolStripMenuItem,
            this.renameToolStripMenuItem});
			this.contextMenu.Name = "contextMenu";
			this.contextMenu.Size = new System.Drawing.Size(147, 48);
			// 
			// exportAsCSVToolStripMenuItem
			// 
			this.exportAsCSVToolStripMenuItem.Name = "exportAsCSVToolStripMenuItem";
			this.exportAsCSVToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.exportAsCSVToolStripMenuItem.Text = "Export as CSV";
			this.exportAsCSVToolStripMenuItem.Click += new System.EventHandler(this.ListViewExport_Click);
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.renameToolStripMenuItem.Text = "Rename";
			this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
			// 
			// addFilterBtn
			// 
			this.addFilterBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.addFilterBtn.ForeColor = System.Drawing.Color.Green;
			this.addFilterBtn.Location = new System.Drawing.Point(6, 3);
			this.addFilterBtn.Name = "addFilterBtn";
			this.addFilterBtn.Size = new System.Drawing.Size(75, 23);
			this.addFilterBtn.TabIndex = 13;
			this.addFilterBtn.Text = "Add Filter";
			this.toolTip.SetToolTip(this.addFilterBtn, "Add a new filter");
			this.addFilterBtn.UseVisualStyleBackColor = true;
			this.addFilterBtn.Click += new System.EventHandler(this.ListViewAdd_Click);
			this.addFilterBtn.MouseEnter += new System.EventHandler(this.ShowHelp);
			// 
			// delFilterBtn
			// 
			this.delFilterBtn.AutoSize = true;
			this.delFilterBtn.Enabled = false;
			this.delFilterBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.delFilterBtn.ForeColor = System.Drawing.Color.Red;
			this.delFilterBtn.Location = new System.Drawing.Point(87, 3);
			this.delFilterBtn.Name = "delFilterBtn";
			this.delFilterBtn.Size = new System.Drawing.Size(86, 23);
			this.delFilterBtn.TabIndex = 14;
			this.delFilterBtn.Text = "Delete Filter";
			this.toolTip.SetToolTip(this.delFilterBtn, "Delete selected  filter.\r\n");
			this.delFilterBtn.UseVisualStyleBackColor = true;
			this.delFilterBtn.Click += new System.EventHandler(this.ListViewDel_Click);
			this.delFilterBtn.MouseEnter += new System.EventHandler(this.ShowHelp);
			// 
			// pathBtn
			// 
			this.pathBtn.Location = new System.Drawing.Point(4, 74);
			this.pathBtn.Name = "pathBtn";
			this.pathBtn.Size = new System.Drawing.Size(54, 24);
			this.pathBtn.TabIndex = 11;
			this.pathBtn.Text = "Path...";
			this.toolTip.SetToolTip(this.pathBtn, "Browse and select base file folder (directory) used to locate files.");
			this.pathBtn.UseVisualStyleBackColor = true;
			this.pathBtn.Click += new System.EventHandler(this.pathBtn_Click);
			this.pathBtn.MouseEnter += new System.EventHandler(this.ShowHelp);
			// 
			// bookmarkList
			// 
			this.bookmarkList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bookmarkList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.bookmarkList.ContextMenuStrip = this.contextMenu;
			this.bookmarkList.FullRowSelect = true;
			this.bookmarkList.GridLines = true;
			this.bookmarkList.HideSelection = false;
			this.bookmarkList.Location = new System.Drawing.Point(0, 47);
			this.bookmarkList.Name = "bookmarkList";
			this.bookmarkList.Size = new System.Drawing.Size(328, 321);
			this.bookmarkList.TabIndex = 15;
			this.toolTip.SetToolTip(this.bookmarkList, "Add bookmark to remember path and filter settings. Double click to activate a boo" +
        "kmark.");
			this.bookmarkList.UseCompatibleStateImageBehavior = false;
			this.bookmarkList.View = System.Windows.Forms.View.Details;
			this.bookmarkList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.SortColumn_Click);
			this.bookmarkList.SelectedIndexChanged += new System.EventHandler(this.bookmarkList_SelectedIndexChanged);
			this.bookmarkList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListView_KeyUp);
			this.bookmarkList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.bookmarkList_MouseDoubleClick);
			this.bookmarkList.MouseEnter += new System.EventHandler(this.ShowHelp);
			this.bookmarkList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveEvent);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 300;
			// 
			// addBmBtn
			// 
			this.addBmBtn.AutoSize = true;
			this.addBmBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.addBmBtn.ForeColor = System.Drawing.Color.Green;
			this.addBmBtn.Location = new System.Drawing.Point(0, 18);
			this.addBmBtn.Name = "addBmBtn";
			this.addBmBtn.Size = new System.Drawing.Size(78, 23);
			this.addBmBtn.TabIndex = 16;
			this.addBmBtn.Text = "Add Bmark";
			this.toolTip.SetToolTip(this.addBmBtn, "Add bookmark to remember path and filter settings.");
			this.addBmBtn.UseVisualStyleBackColor = true;
			this.addBmBtn.Click += new System.EventHandler(this.ListViewAdd_Click);
			this.addBmBtn.MouseEnter += new System.EventHandler(this.ShowHelp);
			// 
			// delBmBtn
			// 
			this.delBmBtn.AutoSize = true;
			this.delBmBtn.Enabled = false;
			this.delBmBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.delBmBtn.ForeColor = System.Drawing.Color.Red;
			this.delBmBtn.Location = new System.Drawing.Point(165, 18);
			this.delBmBtn.Name = "delBmBtn";
			this.delBmBtn.Size = new System.Drawing.Size(93, 23);
			this.delBmBtn.TabIndex = 17;
			this.delBmBtn.Text = "Delete Bmark";
			this.toolTip.SetToolTip(this.delBmBtn, "Delete selected Bookmark");
			this.delBmBtn.UseVisualStyleBackColor = true;
			this.delBmBtn.Click += new System.EventHandler(this.ListViewDel_Click);
			this.delBmBtn.MouseEnter += new System.EventHandler(this.ShowHelp);
			// 
			// setBmBtn
			// 
			this.setBmBtn.AutoSize = true;
			this.setBmBtn.Enabled = false;
			this.setBmBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.setBmBtn.ForeColor = System.Drawing.Color.Black;
			this.setBmBtn.Location = new System.Drawing.Point(84, 18);
			this.setBmBtn.Name = "setBmBtn";
			this.setBmBtn.Size = new System.Drawing.Size(75, 23);
			this.setBmBtn.TabIndex = 18;
			this.setBmBtn.Text = "Set Bmark";
			this.toolTip.SetToolTip(this.setBmBtn, "Set selected Bookmark to currrent Path and Filter Tag settings.");
			this.setBmBtn.UseVisualStyleBackColor = true;
			this.setBmBtn.Click += new System.EventHandler(this.setBmBtn_Click);
			this.setBmBtn.MouseEnter += new System.EventHandler(this.ShowHelp);
			// 
			// helpBtn
			// 
			this.helpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.helpBtn.Location = new System.Drawing.Point(312, 3);
			this.helpBtn.Name = "helpBtn";
			this.helpBtn.Size = new System.Drawing.Size(32, 24);
			this.helpBtn.TabIndex = 15;
			this.helpBtn.Text = "?";
			this.toolTip.SetToolTip(this.helpBtn, "Help on filter rules");
			this.helpBtn.UseVisualStyleBackColor = true;
			this.helpBtn.Click += new System.EventHandler(this.helpBtn_Click);
			// 
			// pathDialog
			// 
			this.pathDialog.Description = "Search Path";
			this.pathDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.pathDialog.ShowNewFolderButton = false;
			// 
			// bookmarkGrp
			// 
			this.bookmarkGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bookmarkGrp.BackColor = System.Drawing.Color.Silver;
			this.bookmarkGrp.Controls.Add(this.setBmBtn);
			this.bookmarkGrp.Controls.Add(this.delBmBtn);
			this.bookmarkGrp.Controls.Add(this.addBmBtn);
			this.bookmarkGrp.Controls.Add(this.bookmarkList);
			this.bookmarkGrp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.bookmarkGrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bookmarkGrp.Location = new System.Drawing.Point(6, 112);
			this.bookmarkGrp.Name = "bookmarkGrp";
			this.bookmarkGrp.Size = new System.Drawing.Size(328, 301);
			this.bookmarkGrp.TabIndex = 16;
			this.bookmarkGrp.TabStop = false;
			this.bookmarkGrp.Text = "Bookmarks:";
			// 
			// mainSplitter
			// 
			this.mainSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.mainSplitter.BackColor = System.Drawing.Color.Gray;
			this.mainSplitter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mainSplitter.BackgroundImage")));
			this.mainSplitter.Location = new System.Drawing.Point(0, 104);
			this.mainSplitter.Name = "mainSplitter";
			// 
			// mainSplitter.Panel1
			// 
			this.mainSplitter.Panel1.BackColor = System.Drawing.Color.Silver;
			this.mainSplitter.Panel1.Controls.Add(this.helpBtn);
			this.mainSplitter.Panel1.Controls.Add(this.activeBookmarkTx);
			this.mainSplitter.Panel1.Controls.Add(this.filterListView);
			this.mainSplitter.Panel1.Controls.Add(this.addFilterBtn);
			this.mainSplitter.Panel1.Controls.Add(this.delFilterBtn);
			this.mainSplitter.Panel1.Controls.Add(this.LocateFilesBtn);
			// 
			// mainSplitter.Panel2
			// 
			this.mainSplitter.Panel2.BackColor = System.Drawing.Color.Silver;
			this.mainSplitter.Panel2.Controls.Add(this.panel2);
			this.mainSplitter.Panel2.Controls.Add(this.bookmarkGrp);
			this.mainSplitter.Size = new System.Drawing.Size(975, 410);
			this.mainSplitter.SplitterDistance = 633;
			this.mainSplitter.SplitterWidth = 6;
			this.mainSplitter.TabIndex = 17;
			// 
			// exportCsvDialog
			// 
			this.exportCsvDialog.DefaultExt = "csv";
			this.exportCsvDialog.Filter = "Csv|*.csv|Text|*.txt|All Files|*.*";
			this.exportCsvDialog.Title = "Export List as CSV";
			// 
			// mainPanel
			// 
			this.mainPanel.Controls.Add(this.titlePanel);
			this.mainPanel.Controls.Add(this.pathBtn);
			this.mainPanel.Controls.Add(this.mainSplitter);
			this.mainPanel.Controls.Add(this.FolderBox);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(975, 517);
			this.mainPanel.TabIndex = 18;
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.mainPanel);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(975, 517);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			// 
			// toolStripContainer1.LeftToolStripPanel
			// 
			this.toolStripContainer1.LeftToolStripPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStripContainer1.LeftToolStripPanel.BackgroundImage")));
			this.toolStripContainer1.Location = new System.Drawing.Point(8, 8);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(975, 542);
			this.toolStripContainer1.TabIndex = 19;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Silver;
			this.ClientSize = new System.Drawing.Size(991, 558);
			this.Controls.Add(this.toolStripContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.Name = "MainForm";
			this.Padding = new System.Windows.Forms.Padding(8);
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "VStudio Cleaner";
			this.Load += new System.EventHandler(this.LoadEvent);
			this.titlePanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.contextMenu.ResumeLayout(false);
			this.bookmarkGrp.ResumeLayout(false);
			this.bookmarkGrp.PerformLayout();
			this.mainSplitter.Panel1.ResumeLayout(false);
			this.mainSplitter.Panel1.PerformLayout();
			this.mainSplitter.Panel2.ResumeLayout(false);
			this.mainSplitter.ResumeLayout(false);
			this.mainPanel.ResumeLayout(false);
			this.mainPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.ResumeLayout(false);

        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Application.Idle += new EventHandler(OnLoaded);
        }

        public void OnLoaded(object sender, EventArgs args)
        {
            // Form fully loaded - must remove idle handler
            Application.Idle -= new EventHandler(OnLoaded);
           
            /*
            if (this.cmdDelete)
            {
                ArrayList locatedFiles = LocateFiles();
            }
            else */
            if (this.cmdOpenLocate || this.cmdDelete)
                this.LocateFilesBtn_Click(null, EventArgs.Empty);
        }

        void fileScan_Load(object sender, EventArgs e)
        {
            if (this.cmdDelete)
                Application.Idle += new EventHandler(OnFileScanLoaded); 
        }

        public void OnFileScanLoaded(object sender, EventArgs args)
        {
            // FileScan fully loaded - must remove idle handler
            Application.Idle -= new EventHandler(OnLoaded);

            if (this.cmdDelete)
            {
                // Pess delete ?
                FileScan fileScan = (FileScan)sender;
                fileScan.DeleteFiles();
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] cmdLineArgs)
        {
            Application.Run(new MainForm(cmdLineArgs));
        }

        #region ==== Manage registry to save and restore application settings

        string keyPath = "Path";
        string keyFilter = "Filter";
        string lastBookmark = "lastBookmark";
        string lastPath = "lastPath";

        /// <summary>
        /// Load Registry and restore application state
        /// </summary>
        private void LoadFromRegistry()
        {
            string paths = this.regKey.GetValue(keyPath, "") as string;
            SetPath(paths);
            int tagIdx = 0;
            string tagFields = null;
            while ((tagFields = regKey.GetValue(tagIdx.ToString("000"), null) as string) != null)
            {
                string[] fields = tagFields.Split('|');
                if (this.filterListView.Items.Count == 0 ||
                    this.filterListView.FindItemWithText(fields[0], false, 0) == null)
                    AddFilter(fields);
                else
                    System.Diagnostics.Debug.WriteLine("Ignore duplicate:" + tagFields);
                tagIdx++;
            }

            string[] bmKeyNames = this.regKey.GetSubKeyNames();
            foreach (string bmKeyName in bmKeyNames)
            {
                RegistryKey bmKey = this.regKey.CreateSubKey(bmKeyName);
                ListViewItem bmItem = this.bookmarkList.Items.Add(bmKeyName);

                bmItem.SubItems.Add(bmKey.GetValue(keyPath, "") as string);
                bmItem.SubItems.Add(bmKey.GetValue(keyFilter, "") as string);
            }

            string lastBm = this.regKey.GetValue(lastBookmark, "") as string;
            if (lastBm != null && lastBm.Length != 0 && this.bookmarkList.Items.Count != 0)
            {
                ListViewItem lastBmItem = this.bookmarkList.FindItemWithText(lastBm, false, 0);
                if (lastBmItem != null)
                    ApplyBookmark(lastBmItem);
            }

            // if (!IsPathValid())
            {
                SetPath(this.regKey.GetValue(lastPath, this.FolderBox.Text) as string);
            }

        }

        /// <summary>
        /// Save application state to registry
        /// </summary>
        private void SaveToRegistry()
        {
            try
            {
                string appName = Application.ProductName;
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree(@"Software\" + appName);
                this.regKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\" + appName);
            }
            catch { }

            this.regKey.SetValue(keyPath, this.FolderBox.Text);
            int tagCnt = this.filterListView.Items.Count;
            ListViewItem item;

            for (int tagIdx = 0; tagIdx != tagCnt; tagIdx++)
            {
                item = this.filterListView.Items[tagIdx];
                int fldCnt = item.SubItems.Count;
                string tagFields = "";
                for (int fldIdx = 0; fldIdx != fldCnt; fldIdx++)
                {
                    if (fldIdx != 0)
                        tagFields += "|";
                    tagFields += item.SubItems[fldIdx].Text;
                }
                tagFields += "|" + item.ForeColor.ToArgb().ToString();
                tagFields += "|" + item.BackColor.ToArgb().ToString();

                this.regKey.SetValue(tagIdx.ToString("000"), tagFields);
            }

            foreach (ListViewItem bmItem in this.bookmarkList.Items)
            {
                RegistryKey bmKey = this.regKey.CreateSubKey(bmItem.Text);
                bmKey.SetValue(keyPath, bmItem.SubItems[1].Text);
                bmKey.SetValue(keyFilter, bmItem.SubItems[2].Text);
            }

            if (activeBookmarkTx.Text.Length != 0)
            {
                this.regKey.SetValue(lastBookmark, activeBookmarkTx.Text);
            }

            this.regKey.SetValue(lastPath, this.FolderBox.Text);
        }
        #endregion

        #region ==== Manager shell registration

        private const string RegFolderShell = "Folder\\shell\\";

        private void CheckSecurity()
        {
            string appName = Application.ProductName;

            //check registry permissions
            RegistryPermission regPerm;
            regPerm = new RegistryPermission(RegistryPermissionAccess.Write, "HKEY_CLASSES_ROOT\\" + RegFolderShell + appName);
            regPerm.AddPathList(RegistryPermissionAccess.Write, string.Format("HKEY_CLASSES_ROOT\\{0}{1}\\command", RegFolderShell, appName));
            regPerm.Demand();
        }
        
        private void RegisterOnShellMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (RegisterOnShellMenuItem.Checked)
                {
                    Unregister();
                }
                else
                {
                    Register();
                }
                RegisterOnShellMenuItem.Checked = !RegisterOnShellMenuItem.Checked;
            }
            catch (Exception e1)
            {
                MessageBox.Show(this, "Unable to perform operation : " + e1.Message);
            }
        }

        private void Register()
        {
            string appName = Application.ProductName;
            string RegShell = RegFolderShell + appName;
            string Command = RegShell + "\\command";

            RegistryKey regmenu = null;
            RegistryKey regcmd = null;
            try
            {
                regmenu = Registry.ClassesRoot.CreateSubKey(RegShell);
                if (regmenu != null)
                    regmenu.SetValue("", "Clear using " + appName);
                regcmd = Registry.ClassesRoot.CreateSubKey(Command);
                if (regcmd != null)
                    regcmd.SetValue("", Environment.CommandLine + "\"%1\"" + " @locate");
                regmenu.SetValue("Icon", Environment.CommandLine.Trim().Replace("\"", ""));
                regmenu.SetValue("MUIVerb", "VStudioCleaner");      // &Open, ...
            }
            catch (Exception ex)
            {
                throw new Exception("Problem registering application on Explorer menu " + ex.Message);
            }
            finally
            {
                if (regmenu != null)
                    regmenu.Close();
                if (regcmd != null)
                    regcmd.Close();
            }
        }

        private void Unregister()
        {
            string appName = Application.ProductName;
            string RegShell = RegFolderShell + appName;
            string Command = RegShell + "\\command";

            try
            {
                RegistryKey reg = Registry.ClassesRoot.OpenSubKey(Command);
                if (reg != null)
                {
                    reg.Close();
                    Registry.ClassesRoot.DeleteSubKey(Command);
                }
                reg = Registry.ClassesRoot.OpenSubKey(RegShell);
                if (reg != null)
                {
                    reg.Close();
                    Registry.ClassesRoot.DeleteSubKey(RegShell);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problem removing application info from Shell, " + ex.Message);
            }
        }

        public string ProductNameAndVersion()
        {
            string appName = Application.ProductName;
            string appVern = Application.ProductVersion;
            return appName + "V" + appVern.Substring(0, 3); //  Get part of version string "n.n"
        }

        private void openRegistryShell_Click(object sender, EventArgs e)
        {
            string appName = Application.ProductName;
            string RegShell = RegFolderShell + appName;

            // Preset last place RegEdit visited to our registry section
            RegistryKey regEditKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit");
            if (System.Environment.OSVersion.Version.Major >= 6)
                regEditKey.SetValue("LastKey", @"Computer\HKEY_CLASSES_ROOT\" + RegShell);  // vista, win7, ...
            else
                regEditKey.SetValue("LastKey", @"My Computer\HKEY_CLASSES_ROOT\" + RegShell);
            regEditKey.Close();

            // Launch RegEdit which will default at the last place it was (our stuff).
            System.Diagnostics.Process.Start("regedit.exe");
        }

        private void openRegistry_Click(object sender, EventArgs e)
        {
            string appName = Application.ProductName;

            // Preset last place RegEdit visited to our registry section
            RegistryKey regEditKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Applets\Regedit");
            if (System.Environment.OSVersion.Version.Major >= 6)
                regEditKey.SetValue("LastKey", @"Computer\HKEY_CURRENT_USER\Software\" + appName);
            else
                regEditKey.SetValue("LastKey", @"My Computer\HKEY_CURRENT_USER\Software\" + appName);
            regEditKey.Close();

            // Launch RegEdit which will default at the last place it was (our stuff).
            System.Diagnostics.Process.Start("regedit.exe");
        }

        #endregion

        /// <summary>
        /// Main method to check shell registration state and present filters and bookmarks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadEvent(object sender, System.EventArgs e)
        {
            //Application.EnableVisualStyles();
            if (this.filterListView.Items.Count == 0)
                LoadDefaultFilters();

            RegistryKey regmenu = null;
            RegistryKey regcmd = null;

            try
            {
                this.CheckSecurity();

                string appName = Application.ProductName;
                string RegShell = RegFolderShell + appName;

                regmenu = Registry.ClassesRoot.OpenSubKey(RegShell, false);
                if (regmenu != null)
                { 
                    RegisterOnShellMenuItem.Checked = true;
                }
                else
                    RegisterOnShellMenuItem.Checked = false;
            }
            catch (ArgumentException ex)
            {
                // RegistryPermissionAccess.AllAccess can not be used as a parameter for GetPathList.
                MessageBox.Show(this, "An ArgumentException occured as a result of using AllAccess.  "
                    + "AllAccess cannot be used as a parameter in GetPathList because it represents more than one "
                    + "type of registry variable access : \n" + ex);
            }
            catch (SecurityException ex)
            {
                // RegistryPermissionAccess.AllAccess can not be used as a parameter for GetPathList.
                MessageBox.Show(this, "An ArgumentException occured as a result of using AllAccess.  " + ex);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString());
            }
            finally
            {
                if (regmenu != null)
                    regmenu.Close();
                if (regcmd != null)
                    regcmd.Close();
            }
        }

        /// <summary>
        /// Close application
        /// </summary>
        private void closeBtn_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void limitLocateTo1000_Click(object sender, EventArgs e)
        {
            this.limitLocateTo1000.Checked = !this.limitLocateTo1000.Checked;
        }

        /// <summary>
        ///  Show helpful info for each control.
        /// </summary>
        private void ShowHelp(object sender, EventArgs e)
        {
            ListView lstView = sender as ListView;
            if (lstView != null)
            {
                Point p = lstView.PointToClient(System.Windows.Forms.Control.MousePosition);
                ListViewItem item = lstView.GetItemAt(p.X, p.Y);

                if (item != null)
                {
                    string helpMsg = string.Empty;
                    if (lstView == this.filterListView)
                        helpMsg = "Filter Rule\n" + item.SubItems[2].Text;
                    else if (lstView == this.bookmarkList)
                        helpMsg = "Double click to load \"" + item.SubItems[0].Text + "\" bookmark rules";

                    if (helpMsg.Length != 0)
                    {
                        InfoHelp.Text = helpMsg;
                        return;
                    }
                }
            }

            InfoHelp.Text = toolTip.GetToolTip((Control)sender);
        }


        /// <summary>
        /// Use mouse movement events to help spawn 'ShowHelp'
        /// </summary>
        private void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            ShowHelp(sender, EventArgs.Empty);
        }

       
        /// <summary>
        /// Main action - spawn 'LocateFiles' dialog to locate matching files.
        /// </summary>
        private void LocateFilesBtn_Click(object sender, System.EventArgs e)
        {
            ArrayList arrayList = LocateFiles();
            FileScan fileScan = new FileScan(FolderBox.Text, arrayList, limitLocateTo1000.Checked?1000:-1);
            fileScan.Load += new EventHandler(fileScan_Load);
            fileScan.Show(this);    // non-modal since I hate modal dialogs.
        }

        private ArrayList LocateFiles()
        {
            ArrayList arrayList = new ArrayList();

            int cnt = this.filterListView.Items.Count;
            for (int idx = 0; idx < cnt; idx++)
            {
                ListViewItem item = this.filterListView.Items[idx];
                if (item.Checked == true)
                    arrayList.Add(item);
            }

            return arrayList;
        }

        private void ShowAboutDialog(object sender, System.EventArgs e)
        {
            About about = new About(this);
            about.Show(this);       // non-modal since I hate modal dialogs.
        }

        #region ==== Path Buttons and Events

        private void SetPath(string pathList)
        {
            this.FolderBox.Text = pathList;
        }

        private bool IsPathValid()
        {
            string[] paths = this.FolderBox.Text.Split(';');
            bool valid = true;
            foreach (string path in paths)
            {
                if (!Directory.Exists(path))
                {
                    valid = false;
                    break;
                }
            }
            return valid;
        }


        private void pathBtn_Click(object sender, EventArgs e)
        {
            // Try catches any garbage paths
            try
            {
                this.pathDialog.SelectedPath = this.FolderBox.Text.Split(';')[0];
                while (this.pathDialog.SelectedPath.Length != 0 &&
                    !Directory.Exists(this.pathDialog.SelectedPath))
                {
                    this.pathDialog.SelectedPath = Path.GetDirectoryName(this.pathDialog.SelectedPath);
                }
            }
            catch { }
#if true
            FolderBrowserDialogEx folderDlgEx = new FolderBrowserDialogEx();
            folderDlgEx.ShowNewFolderButton = false;
            folderDlgEx.SelectedPath = this.pathDialog.SelectedPath;
            if (folderDlgEx.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetPath(folderDlgEx.SelectedPath);
            }
#else
            if (this.pathDialog.ShowDialog() == DialogResult.OK)
            {
                SetPath(this.pathDialog.SelectedPath);
            }
#endif
        }

        /// <summary>
        /// Treat TAB as input so auto-complete suggestions work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;
                return;
            }
        }

        private void FolderBox_TextChanged(object sender, EventArgs e)
        {
            this.FolderBox.BackColor = IsPathValid() ? Color.White : Color.Pink;
        }

        #endregion 

        #region ==== Manager Filters
        private void resetFilters_Click(object sender, EventArgs e)
        {
            this.filterListView.Items.Clear();
            LoadDefaultFilters();
        }

        private void LoadDefaultFilters()
        {
            string[] defFilters = new string[] 
            { 
                "Debug Folder|Debug\\|Results of debug build. Keep if you need executables. Safe to delete if you can rebuild project|0|-16777216|-4980813",
                "Release Folder|Release\\|Results of release build.|0|-16777216|-4980813",
                "X64|x64\\|64bit builds|0|-16777216|-4980813",
                "Bin|bin\\|Executables created from build process|0|-16777216|-4980813",
                "Obj|obj\\|Objects created from build process|0|-16777216|-8323200",
                "Backup|Backup\\|Upgrade backup files|-16777216|-4144960",
                "Upgrade|*\\UpgradeLog.XML;_UpgradeReport_Files\\|Upgrade files|-16777216|-4144960",
                "Pdb|.pdb|Support files created from debug build",
                "Ncb|.ncb;.ilk|Temporary build files|0|-16777216|-128",
                "Xml|*\\setting*.xml;*\\connection*.xml|Temporary xml files",
                "Suo|.suo|Temporary solution file",
                "Sdf|.sdf|Temporary build file (vs201x)|0|-16777216|-128",
                "Ipch|ipch\\|Temporary build directory (vs201x)",
                "Manifest|*.manifest*|Temporary manifests vs201x",
                "never-exe|-*.exe|Never remove executables|0|-16777216|-18469",
                "never-dll|-*.dll|DLL|Color|-16777216|-65536",
                "any-obj|*.obj|Any Objects|Color|-1|-16744448",
                "any-bak|*.bak|Any Backup file|Color|-16777216|-16711681"
            };

            foreach (string s in defFilters)
            {
                string[] fields = s.Split('|');
                if (this.filterListView.Items.Count == 0 || 
                    this.filterListView.FindItemWithText(fields[0], false, 0) == null)
                    AddFilter(fields);
            }
        }

        private void AddFilter(string[] fields)
        {
            Color fgColor = this.filterListView.ForeColor;
            Color bgColor = this.filterListView.BackColor;

            if (fields.Length > this.filterListView.Columns.Count)
            {
                int rgba;
             
                if (int.TryParse(fields[fields.Length - 2], out rgba))
                {
                    fgColor = Color.FromArgb(rgba);
                }
                if (int.TryParse(fields[fields.Length - 1], out rgba))
                {
                    bgColor = Color.FromArgb(rgba);
                }
            }

            ListViewItem item = null;
            foreach (string f in fields)
            {
                if (item == null)
                {
                    item = this.filterListView.Items.Add(f);
                    item.ForeColor = fgColor;
                    item.BackColor = bgColor;
                    item.Checked = true;
                }
                else if (item.SubItems.Count < this.filterListView.Columns.Count)
                {
                    item.SubItems.Add(f);
                }
            }
        }
        #endregion

        #region ==== Manager bookmarks

        private void setBmBtn_Click(object sender, EventArgs e)
        {
            if (this.bookmarkList.SelectedItems.Count != 0)
            {
                SetBookmark(this.bookmarkList.SelectedItems[0]);
            }
        }
        
        /// <summary>
        /// Set bookmark by placing folder path in subitem[1] and filters in subitem[2].
        /// </summary>
        private void SetBookmark(ListViewItem item)
        {
            while (item.SubItems.Count < 3)
                item.SubItems.Add("");
            item.SubItems[1].Text = this.FolderBox.Text;

            int filterOnCnt = 0;
            string filters = "";
            foreach (ListViewItem filter in this.filterListView.Items)
            {
                if (filter.Checked == true)
                {
                    filters += filter.Text + "|";
                    filterOnCnt++;
                }
            }

            item.SubItems[2].Text = filters;
            InfoHelp.Text = "Bookmark:" + item.Text + " set to current path and " + filterOnCnt.ToString() + " filters";
        }

        private void ApplyBookmark(ListViewItem item)
        {
            if (item.SubItems.Count == 3)
            {
                // Only reset folder search path of one saved with Bookmark.
                if (item.SubItems[1].Text.Length != 0)
                    SetPath(item.SubItems[1].Text);

                string[] filters = item.SubItems[2].Text.Split('|');

                foreach (ListViewItem filterItem in this.filterListView.Items)
                    filterItem.Checked = false;

                foreach (string filter in filters)
                {
                    ListViewItem filterItem = this.filterListView.FindItemWithText(filter, false, 0);
                    if (filterItem != null)
                        filterItem.Checked = true;
                }

                activeBookmarkTx.Text = item.Text;
            }
        }

        private void bookmarkList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView listView = this.bookmarkList;
            Point p = listView.PointToClient(System.Windows.Forms.Control.MousePosition);
            ListViewItem itemAt = listView.GetItemAt(p.X, p.Y);
            if (itemAt != null)
            {
                itemAt.Selected = true;
                itemAt.Focused = true;
                ApplyBookmark(itemAt);
            }
        }

      
        #endregion

        #region ==== ListView button controls for both (filters and bookmarks)

        /// <summary>
        /// Column header clicked - fire sort.
        /// </summary>
        private void SortColumn_Click(object sender, ColumnClickEventArgs e)
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
                listView.Sort();
        }

        private void bookmarkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool haveItems = bookmarkList.SelectedItems.Count != 0;
            this.setBmBtn.Enabled = haveItems;
            this.delBmBtn.Enabled = haveItems;
        }

        private void ListView_KeyUp(object sender, KeyEventArgs e)
        {
            ListView listView = sender as ListView;
            Point p = listView.PointToClient(System.Windows.Forms.Control.MousePosition);
            ListViewItem itemAt = listView.GetItemAt(p.X, p.Y);

            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                foreach (ListViewItem item in listView.Items)
                {
                    item.Selected = true;
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (listView.SelectedItems.Count != 0)
                {
                    foreach (ListViewItem item in listView.SelectedItems)
                    {
                        listView.Items.Remove(item);
                    }
                }
                else if (itemAt != null)
                {
                    listView.Items.Remove(itemAt);
                }
            }
        }

        private void ListViewDel_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            ListView listView = button.Tag as ListView;

            foreach (ListViewItem item in listView.SelectedItems)
            {
                listView.Items.Remove(item);
            }
        }

        private void ListViewAdd_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            ListView listView = button.Tag as ListView;

            ListViewItem item = listView.Items.Add("<new>");
            int cnt = listView.Items.Count;
            if (cnt > 0)
                listView.EnsureVisible(cnt - 1);

            ListViewEdit(listView, item);

            if (listView == this.bookmarkList)
                SetBookmark(item);
        }


        private void ListViewEdit(ListView listView, ListViewItem item)
        {
            int colCnt = listView.Columns.Count;
            for (int colIdx = 0; colIdx < colCnt; colIdx++)
            {
                ColumnHeader colHeader = listView.Columns[colIdx];
                if (colIdx == item.SubItems.Count)
                    item.SubItems.Add(colHeader.Text);

                if (listView.DisplayRectangle.Contains(item.SubItems[colIdx].Bounds.Location) == false)
                {
                    // Force item in view by scrolling box to the right.

                    item.Selected = true;
                    item.Focused = true;

                    int shift = item.SubItems[colIdx].Bounds.Location.X - listView.DisplayRectangle.X;
                    shift /= 8;
                    for (int i = 0; i < shift; i++)
                    {
                        SendKeys.Send("{RIGHT}");
                        SendKeys.Flush();
                    }
                }

                if (colHeader.Text == "Color")
                {
                    ColorDlg colorDlg = new ColorDlg();
                    colorDlg.FgColor = item.ForeColor;
                    colorDlg.BgColor = item.BackColor;
                    if (colorDlg.ShowDialog() == DialogResult.OK)
                    {
                        // item.UseItemStyleForSubItems = false;
                        item.ForeColor = colorDlg.FgColor;
                        item.BackColor = colorDlg.BgColor;
                    }
                }
                else
                {
                    FieldBox fieldBox = new FieldBox();
                    fieldBox.FieldText = item.SubItems[colIdx].Text;
                    fieldBox.Location = listView.PointToScreen(item.SubItems[colIdx].Bounds.Location);

                    if (fieldBox.ShowDialog() == DialogResult.OK)
                    {
                        item.SubItems[colIdx].Text = fieldBox.FieldText;
                    }
                }
            }
        }

        private void ListViewDbl_Click(object sender, EventArgs e)
        {
            ListView listView = sender as ListView;
            Point p = listView.PointToClient(System.Windows.Forms.Control.MousePosition);
            ListViewItem itemAt = listView.GetItemAt(p.X, p.Y);
            if (itemAt != null)
                ListViewEdit(listView, itemAt);
        }

        /// <summary>
        ///  Save ListView contents in as a CSV file.
        /// </summary>
        private void ListViewExport_Click(object sender, EventArgs e)
        {
            // ToolStripDropDownItem viewItem = (ToolStripDropDownItem)sender;
            // ToolStripDropDownMenu viewItems = viewItem.DropDown.OwnerItem.Owner as ToolStripDropDownMenu;
            Point p;
            ListView listView = null;
            
            p = filterListView.PointToClient(System.Windows.Forms.Control.MousePosition);
            if (this.filterListView.DisplayRectangle.Contains(p))
                listView = this.filterListView;

            p = bookmarkList.PointToClient(System.Windows.Forms.Control.MousePosition);
            if (this.bookmarkList.DisplayRectangle.Contains(p))
                listView = this.bookmarkList;


            if (listView == null || listView.Items.Count == 0)
                return;

            if (this.exportCsvDialog.ShowDialog() == DialogResult.OK)
            {
                String filePath = this.exportCsvDialog.FileName;
                TextWriter writer = new StreamWriter(filePath);

                string txtLine = string.Empty;
                foreach (ColumnHeader ch in listView.Columns)
                {
                    if (txtLine.Length != 0)
                        txtLine += ",";
                    txtLine += ch.Text;
                }
                writer.WriteLine(txtLine);

                foreach (ListViewItem item in listView.Items)
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

                InfoHelp.Text = "Saved list to file:" + filePath;
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bookmarkList.SelectedItems.Count == 1)
            {
                ListViewEdit(bookmarkList, bookmarkList.SelectedItems[0]);
            }
        }

        /// <summary>
        /// Enable/Disable filter delete button when filter selected/unselected.
        /// </summary>
        private void filterListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.delFilterBtn.Enabled = filterListView.SelectedItems.Count != 0;
        }

        #endregion

        private void helpBtn_Click(object sender, EventArgs e)
        {
            HelpDialog helpDialog = new HelpDialog();
            helpDialog.Show();
        }

        private void FolderBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                string folderList = string.Empty;
                foreach (string folder in FileList)
                {
                    if (folderList.Length != 0)
                        folderList += ';';
                    folderList += folder;
                }
                FolderBox.Text = folderList;
            }
        }

        private void FolderBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

    }
}
