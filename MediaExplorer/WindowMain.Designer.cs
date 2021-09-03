
namespace MediaExplorer
{
    partial class WindowMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tablePanRoot = new System.Windows.Forms.TableLayoutPanel();
            this.tablePanToolbar = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ButtonUp = new System.Windows.Forms.Button();
            this.ButtonForward = new System.Windows.Forms.Button();
            this.ButtonBack = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ButtonRefresh = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.TextBoxPath = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.lblLoading = new System.Windows.Forms.Label();
            this.contextMenuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDGW = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemLW = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tablePanRoot.SuspendLayout();
            this.tablePanToolbar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1050, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemDGW,
            this.ToolStripMenuItemLW});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "View";
            // 
            // tablePanRoot
            // 
            this.tablePanRoot.ColumnCount = 1;
            this.tablePanRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanRoot.Controls.Add(this.tablePanToolbar, 0, 0);
            this.tablePanRoot.Controls.Add(this.splitContainer1, 0, 2);
            this.tablePanRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanRoot.Location = new System.Drawing.Point(0, 24);
            this.tablePanRoot.Name = "tablePanRoot";
            this.tablePanRoot.RowCount = 3;
            this.tablePanRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tablePanRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tablePanRoot.Size = new System.Drawing.Size(1050, 636);
            this.tablePanRoot.TabIndex = 1;
            // 
            // tablePanToolbar
            // 
            this.tablePanToolbar.ColumnCount = 4;
            this.tablePanToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.82353F));
            this.tablePanToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.41176F));
            this.tablePanToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.76471F));
            this.tablePanToolbar.Controls.Add(this.panel1, 0, 0);
            this.tablePanToolbar.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tablePanToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanToolbar.Location = new System.Drawing.Point(3, 3);
            this.tablePanToolbar.Name = "tablePanToolbar";
            this.tablePanToolbar.RowCount = 1;
            this.tablePanToolbar.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanToolbar.Size = new System.Drawing.Size(1044, 34);
            this.tablePanToolbar.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ButtonUp);
            this.panel1.Controls.Add(this.ButtonForward);
            this.panel1.Controls.Add(this.ButtonBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(85, 28);
            this.panel1.TabIndex = 2;
            // 
            // ButtonUp
            // 
            this.ButtonUp.Dock = System.Windows.Forms.DockStyle.Left;
            this.ButtonUp.Location = new System.Drawing.Point(56, 0);
            this.ButtonUp.Name = "ButtonUp";
            this.ButtonUp.Padding = new System.Windows.Forms.Padding(3);
            this.ButtonUp.Size = new System.Drawing.Size(28, 28);
            this.ButtonUp.TabIndex = 5;
            this.ButtonUp.Text = "^";
            this.ButtonUp.UseVisualStyleBackColor = true;
            this.ButtonUp.Click += new System.EventHandler(this.ButtonUp_Click);
            // 
            // ButtonForward
            // 
            this.ButtonForward.Dock = System.Windows.Forms.DockStyle.Left;
            this.ButtonForward.Location = new System.Drawing.Point(28, 0);
            this.ButtonForward.Name = "ButtonForward";
            this.ButtonForward.Padding = new System.Windows.Forms.Padding(3);
            this.ButtonForward.Size = new System.Drawing.Size(28, 28);
            this.ButtonForward.TabIndex = 4;
            this.ButtonForward.Text = ">";
            this.ButtonForward.UseVisualStyleBackColor = true;
            this.ButtonForward.Click += new System.EventHandler(this.ButtonForward_Click);
            // 
            // ButtonBack
            // 
            this.ButtonBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.ButtonBack.Location = new System.Drawing.Point(0, 0);
            this.ButtonBack.Name = "ButtonBack";
            this.ButtonBack.Padding = new System.Windows.Forms.Padding(3);
            this.ButtonBack.Size = new System.Drawing.Size(28, 28);
            this.ButtonBack.TabIndex = 3;
            this.ButtonBack.Text = "<";
            this.ButtonBack.UseVisualStyleBackColor = true;
            this.ButtonBack.Click += new System.EventHandler(this.ButtonBack_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ButtonRefresh, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(91, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(560, 34);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // ButtonRefresh
            // 
            this.ButtonRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this.ButtonRefresh.Location = new System.Drawing.Point(529, 3);
            this.ButtonRefresh.Name = "ButtonRefresh";
            this.ButtonRefresh.Padding = new System.Windows.Forms.Padding(3);
            this.ButtonRefresh.Size = new System.Drawing.Size(28, 28);
            this.ButtonRefresh.TabIndex = 13;
            this.ButtonRefresh.Text = "R";
            this.ButtonRefresh.UseVisualStyleBackColor = true;
            this.ButtonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.TextBoxPath, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(526, 34);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // TextBoxPath
            // 
            this.TextBoxPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxPath.Location = new System.Drawing.Point(0, 7);
            this.TextBoxPath.Margin = new System.Windows.Forms.Padding(0);
            this.TextBoxPath.Name = "TextBoxPath";
            this.TextBoxPath.Size = new System.Drawing.Size(526, 20);
            this.TextBoxPath.TabIndex = 15;
            this.TextBoxPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxPath_KeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 43);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblLoading);
            this.splitContainer1.Size = new System.Drawing.Size(1044, 590);
            this.splitContainer1.SplitterDistance = 211;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(211, 590);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            this.treeView1.Click += new System.EventHandler(this.TreeView1_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoading.Location = new System.Drawing.Point(0, 0);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(829, 590);
            this.lblLoading.TabIndex = 0;
            this.lblLoading.Text = "Loading...";
            this.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuTree
            // 
            this.contextMenuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanToolStripMenuItem});
            this.contextMenuTree.Name = "contextMenuTree";
            this.contextMenuTree.Size = new System.Drawing.Size(100, 26);
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.scanToolStripMenuItem.Text = "Scan";
            this.scanToolStripMenuItem.Click += new System.EventHandler(this.ScanToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemDGW
            // 
            this.ToolStripMenuItemDGW.Checked = true;
            this.ToolStripMenuItemDGW.CheckOnClick = true;
            this.ToolStripMenuItemDGW.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItemDGW.Name = "ToolStripMenuItemDGW";
            this.ToolStripMenuItemDGW.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemDGW.Text = "DataGrid";
            this.ToolStripMenuItemDGW.Click += new System.EventHandler(this.ToolStripMenuItemDGW_Click);
            // 
            // ToolStripMenuItemLW
            // 
            this.ToolStripMenuItemLW.CheckOnClick = true;
            this.ToolStripMenuItemLW.Name = "ToolStripMenuItemLW";
            this.ToolStripMenuItemLW.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemLW.Text = "ListView";
            this.ToolStripMenuItemLW.Click += new System.EventHandler(this.ToolStripMenuItemLW_Click);
            // 
            // WindowMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 660);
            this.Controls.Add(this.tablePanRoot);
            this.Controls.Add(this.menuStrip1);
            this.Name = "WindowMain";
            this.Text = "MediaExplorer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WindowMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tablePanRoot.ResumeLayout(false);
            this.tablePanToolbar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tablePanRoot;
        private System.Windows.Forms.TableLayoutPanel tablePanToolbar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ButtonBack;
        private System.Windows.Forms.Button ButtonForward;
        private System.Windows.Forms.Button ButtonUp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button ButtonRefresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox TextBoxPath;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.ContextMenuStrip contextMenuTree;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDGW;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemLW;
    }
}

