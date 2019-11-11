namespace FSyncManager
{
    partial class HiddenMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HiddenMainForm));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installDeinstallFSyncServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installFSyncServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unstallFSyncServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.resumeToolStripMenuItem,
            this.installDeinstallFSyncServiceToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(191, 180);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.startToolStripMenuItem.Text = "Start FSync Service";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.stopToolStripMenuItem.Text = "Stop FSync Service";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.pauseToolStripMenuItem.Text = "Pause FSync Service";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // resumeToolStripMenuItem
            // 
            this.resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
            this.resumeToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.resumeToolStripMenuItem.Text = "Resume FSync Service";
            this.resumeToolStripMenuItem.Click += new System.EventHandler(this.resumeToolStripMenuItem_Click);
            // 
            // installDeinstallFSyncServiceToolStripMenuItem
            // 
            this.installDeinstallFSyncServiceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.installFSyncServiceToolStripMenuItem,
            this.unstallFSyncServiceToolStripMenuItem});
            this.installDeinstallFSyncServiceToolStripMenuItem.Name = "installDeinstallFSyncServiceToolStripMenuItem";
            this.installDeinstallFSyncServiceToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.installDeinstallFSyncServiceToolStripMenuItem.Text = "Install/Deinstall";
            // 
            // installFSyncServiceToolStripMenuItem
            // 
            this.installFSyncServiceToolStripMenuItem.Name = "installFSyncServiceToolStripMenuItem";
            this.installFSyncServiceToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.installFSyncServiceToolStripMenuItem.Text = "Install FSync Service";
            this.installFSyncServiceToolStripMenuItem.Click += new System.EventHandler(this.installFSyncServiceToolStripMenuItem_Click);
            // 
            // unstallGoolgleServiceToolStripMenuItem
            // 
            this.unstallFSyncServiceToolStripMenuItem.Name = "unstallFSyncServiceToolStripMenuItem";
            this.unstallFSyncServiceToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.unstallFSyncServiceToolStripMenuItem.Text = "Unstall FSync Service";
            this.unstallFSyncServiceToolStripMenuItem.Click += new System.EventHandler(this.unstallFServiceToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "File Sync Manager";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDown);
            // 
            // HiddenMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "HiddenMainForm";
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resumeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installDeinstallFSyncServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installFSyncServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unstallFSyncServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}

