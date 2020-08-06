namespace ED_Systems
{
    partial class FormImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImage));
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.priveousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.pbxImg = new System.Windows.Forms.PictureBox();
            this.cmsImg = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fullSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msMain.SuspendLayout();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImg)).BeginInit();
            this.cmsImg.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.msMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.priveousToolStripMenuItem,
            this.nextToolStripMenuItem});
            this.msMain.Location = new System.Drawing.Point(0, 426);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(800, 24);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "menuStrip1";
            // 
            // priveousToolStripMenuItem
            // 
            this.priveousToolStripMenuItem.Name = "priveousToolStripMenuItem";
            this.priveousToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.priveousToolStripMenuItem.Text = "< Priveous";
            this.priveousToolStripMenuItem.Click += new System.EventHandler(this.priveousToolStripMenuItem_Click);
            // 
            // nextToolStripMenuItem
            // 
            this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
            this.nextToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.nextToolStripMenuItem.Text = "Next >";
            this.nextToolStripMenuItem.Click += new System.EventHandler(this.nextToolStripMenuItem_Click);
            // 
            // pnlImage
            // 
            this.pnlImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlImage.AutoScroll = true;
            this.pnlImage.Controls.Add(this.pbxImg);
            this.pnlImage.Location = new System.Drawing.Point(0, 0);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(800, 425);
            this.pnlImage.TabIndex = 1;
            // 
            // pbxImg
            // 
            this.pbxImg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxImg.ContextMenuStrip = this.cmsImg;
            this.pbxImg.Location = new System.Drawing.Point(3, 3);
            this.pbxImg.Name = "pbxImg";
            this.pbxImg.Size = new System.Drawing.Size(794, 419);
            this.pbxImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxImg.TabIndex = 0;
            this.pbxImg.TabStop = false;
            // 
            // cmsImg
            // 
            this.cmsImg.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullSizeToolStripMenuItem});
            this.cmsImg.Name = "cmsImg";
            this.cmsImg.Size = new System.Drawing.Size(116, 26);
            // 
            // fullSizeToolStripMenuItem
            // 
            this.fullSizeToolStripMenuItem.CheckOnClick = true;
            this.fullSizeToolStripMenuItem.Name = "fullSizeToolStripMenuItem";
            this.fullSizeToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.fullSizeToolStripMenuItem.Text = "Full size";
            this.fullSizeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.fullSizeToolStripMenuItem_CheckedChanged);
            // 
            // FormImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "FormImage";
            this.Text = "ED Images";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormImage_Load);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.pnlImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImg)).EndInit();
            this.cmsImg.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.ToolStripMenuItem priveousToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbxImg;
        private System.Windows.Forms.ContextMenuStrip cmsImg;
        private System.Windows.Forms.ToolStripMenuItem fullSizeToolStripMenuItem;
    }
}