using System.Windows.Forms;

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
            if (!saved)
            {
                if (MessageBox.Show($"{LocalStrings.Exit_no_save}", $"{LocalStrings.Info}", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }
            }
            ConfigSaver.Font = richTextBox.Font;
            ConfigSaver.FormSize = this.Size;
            ConfigSaver.Save();
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
            this.tool_find = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_replace = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_info = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_about = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_deleteProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_status.SuspendLayout();
            this.stripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            resources.ApplyResources(this.richTextBox, "richTextBox");
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Name = "richTextBox";
            // 
            // pnl_status
            // 
            resources.ApplyResources(this.pnl_status, "pnl_status");
            this.pnl_status.Controls.Add(this.progressBar);
            this.pnl_status.Controls.Add(this.lbl_status);
            this.pnl_status.Name = "pnl_status";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // lbl_status
            // 
            resources.ApplyResources(this.lbl_status, "lbl_status");
            this.lbl_status.Name = "lbl_status";
            // 
            // stripMenu
            // 
            resources.ApplyResources(this.stripMenu, "stripMenu");
            this.stripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_File,
            this.tool_edit,
            this.tool_info});
            this.stripMenu.Name = "stripMenu";
            // 
            // tool_File
            // 
            resources.ApplyResources(this.tool_File, "tool_File");
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
            // 
            // tool_new
            // 
            resources.ApplyResources(this.tool_new, "tool_new");
            this.tool_new.Name = "tool_new";
            // 
            // tool_open
            // 
            resources.ApplyResources(this.tool_open, "tool_open");
            this.tool_open.Name = "tool_open";
            // 
            // toolStripSeparator
            // 
            resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
            this.toolStripSeparator.Name = "toolStripSeparator";
            // 
            // tool_save
            // 
            resources.ApplyResources(this.tool_save, "tool_save");
            this.tool_save.Name = "tool_save";
            // 
            // tool_saveAs
            // 
            resources.ApplyResources(this.tool_saveAs, "tool_saveAs");
            this.tool_saveAs.Name = "tool_saveAs";
            // 
            // tool_fontSettings
            // 
            resources.ApplyResources(this.tool_fontSettings, "tool_fontSettings");
            this.tool_fontSettings.Name = "tool_fontSettings";
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // tool_exit
            // 
            resources.ApplyResources(this.tool_exit, "tool_exit");
            this.tool_exit.Name = "tool_exit";
            // 
            // tool_edit
            // 
            resources.ApplyResources(this.tool_edit, "tool_edit");
            this.tool_edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_find,
            this.tool_replace});
            this.tool_edit.Name = "tool_edit";
            // 
            // tool_find
            // 
            resources.ApplyResources(this.tool_find, "tool_find");
            this.tool_find.Name = "tool_find";
            this.tool_find.Click += new System.EventHandler(this.tool_find_Click);
            // 
            // tool_replace
            // 
            resources.ApplyResources(this.tool_replace, "tool_replace");
            this.tool_replace.Name = "tool_replace";
            // 
            // tool_info
            // 
            resources.ApplyResources(this.tool_info, "tool_info");
            this.tool_info.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_about,
            this.tool_deleteProgram});
            this.tool_info.Name = "tool_info";
            // 
            // tool_about
            // 
            resources.ApplyResources(this.tool_about, "tool_about");
            this.tool_about.Name = "tool_about";
            // 
            // tool_deleteProgram
            // 
            resources.ApplyResources(this.tool_deleteProgram, "tool_deleteProgram");
            this.tool_deleteProgram.Name = "tool_deleteProgram";
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_status);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.stripMenu);
            this.MainMenuStrip = this.stripMenu;
            this.Name = "MainWindow";
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
        private System.Windows.Forms.ToolStripMenuItem tool_find;
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
        private ToolStripMenuItem tool_deleteProgram;
    }
}

