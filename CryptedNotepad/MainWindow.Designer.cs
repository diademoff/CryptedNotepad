namespace CryptedNotepad
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.pnl_status = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lbl_status = new System.Windows.Forms.Label();
            this.stripMenu = new System.Windows.Forms.MenuStrip();
            this.tool_File = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_new = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tool_save = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_saveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_fontSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_replace = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_info = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_about = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_status.SuspendLayout();
            this.stripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Location = new System.Drawing.Point(1, 27);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(797, 399);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // pnl_status
            // 
            this.pnl_status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_status.Controls.Add(this.progressBar);
            this.pnl_status.Controls.Add(this.lbl_status);
            this.pnl_status.Location = new System.Drawing.Point(0, 429);
            this.pnl_status.Name = "pnl_status";
            this.pnl_status.Size = new System.Drawing.Size(797, 20);
            this.pnl_status.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(173, 5);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(621, 10);
            this.progressBar.TabIndex = 3;
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(7, 3);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(72, 13);
            this.lbl_status.TabIndex = 3;
            this.lbl_status.Text = "Total chars: 0";
            // 
            // stripMenu
            // 
            this.stripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_File,
            this.tool_edit,
            this.tool_info});
            this.stripMenu.Location = new System.Drawing.Point(0, 0);
            this.stripMenu.Name = "stripMenu";
            this.stripMenu.Size = new System.Drawing.Size(800, 24);
            this.stripMenu.TabIndex = 2;
            this.stripMenu.Text = "Stripmenu";
            // 
            // tool_File
            // 
            this.tool_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_new,
            this.tool_open,
            this.toolStripSeparator,
            this.tool_save,
            this.tool_saveAs,
            this.tool_fontSettings,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.tool_exit});
            this.tool_File.Name = "tool_File";
            this.tool_File.Size = new System.Drawing.Size(35, 20);
            this.tool_File.Text = "&File";
            // 
            // tool_new
            // 
            this.tool_new.Image = ((System.Drawing.Image)(resources.GetObject("tool_new.Image")));
            this.tool_new.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_new.Name = "tool_new";
            this.tool_new.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tool_new.Size = new System.Drawing.Size(180, 22);
            this.tool_new.Text = "&New";
            // 
            // tool_open
            // 
            this.tool_open.Image = ((System.Drawing.Image)(resources.GetObject("tool_open.Image")));
            this.tool_open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_open.Name = "tool_open";
            this.tool_open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tool_open.Size = new System.Drawing.Size(180, 22);
            this.tool_open.Text = "&Open";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(177, 6);
            // 
            // tool_save
            // 
            this.tool_save.Image = ((System.Drawing.Image)(resources.GetObject("tool_save.Image")));
            this.tool_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_save.Name = "tool_save";
            this.tool_save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tool_save.Size = new System.Drawing.Size(180, 22);
            this.tool_save.Text = "&Save";
            // 
            // tool_saveAs
            // 
            this.tool_saveAs.Name = "tool_saveAs";
            this.tool_saveAs.Size = new System.Drawing.Size(180, 22);
            this.tool_saveAs.Text = "Save &As";
            // 
            // tool_fontSettings
            // 
            this.tool_fontSettings.Name = "tool_fontSettings";
            this.tool_fontSettings.Size = new System.Drawing.Size(180, 22);
            this.tool_fontSettings.Text = "Font settings";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // tool_exit
            // 
            this.tool_exit.Name = "tool_exit";
            this.tool_exit.Size = new System.Drawing.Size(180, 22);
            this.tool_exit.Text = "E&xit";
            // 
            // tool_edit
            // 
            this.tool_edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findToolStripMenuItem,
            this.tool_replace});
            this.tool_edit.Name = "tool_edit";
            this.tool_edit.Size = new System.Drawing.Size(37, 20);
            this.tool_edit.Text = "Edit";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.findToolStripMenuItem.Text = "Find";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.tool_find_Click);
            // 
            // tool_replace
            // 
            this.tool_replace.Name = "tool_replace";
            this.tool_replace.Size = new System.Drawing.Size(112, 22);
            this.tool_replace.Text = "Replace";
            // 
            // tool_info
            // 
            this.tool_info.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_about});
            this.tool_info.Name = "tool_info";
            this.tool_info.Size = new System.Drawing.Size(39, 20);
            this.tool_info.Text = "Info";
            // 
            // tool_about
            // 
            this.tool_about.Name = "tool_about";
            this.tool_about.Size = new System.Drawing.Size(103, 22);
            this.tool_about.Text = "About";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnl_status);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.stripMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.stripMenu;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File.ctxt";
            this.pnl_status.ResumeLayout(false);
            this.pnl_status.PerformLayout();
            this.stripMenu.ResumeLayout(false);
            this.stripMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Panel pnl_status;
        private System.Windows.Forms.MenuStrip stripMenu;
        private System.Windows.Forms.ToolStripMenuItem tool_edit;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tool_replace;
        private System.Windows.Forms.ToolStripMenuItem tool_info;
        private System.Windows.Forms.ToolStripMenuItem tool_about;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ToolStripMenuItem tool_File;
        private System.Windows.Forms.ToolStripMenuItem tool_new;
        private System.Windows.Forms.ToolStripMenuItem tool_open;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem tool_save;
        private System.Windows.Forms.ToolStripMenuItem tool_saveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tool_exit;
        private System.Windows.Forms.ToolStripMenuItem tool_fontSettings;
    }
}

