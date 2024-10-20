namespace QuanLyNhanSu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hệThốngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chứcNăngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbQLNV = new System.Windows.Forms.ToolStripButton();
            this.tsbCCTL = new System.Windows.Forms.ToolStripButton();
            this.tsbBCNS = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hệThốngToolStripMenuItem,
            this.chứcNăngToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(946, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hệThốngToolStripMenuItem
            // 
            this.hệThốngToolStripMenuItem.Name = "hệThốngToolStripMenuItem";
            this.hệThốngToolStripMenuItem.Size = new System.Drawing.Size(74, 21);
            this.hệThốngToolStripMenuItem.Text = "Hệ thống";
            // 
            // chứcNăngToolStripMenuItem
            // 
            this.chứcNăngToolStripMenuItem.Name = "chứcNăngToolStripMenuItem";
            this.chứcNăngToolStripMenuItem.Size = new System.Drawing.Size(82, 21);
            this.chứcNăngToolStripMenuItem.Text = "Chức năng";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbQLNV,
            this.tsbCCTL,
            this.tsbBCNS,
            this.tsbExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(946, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbQLNV
            // 
            this.tsbQLNV.Image = ((System.Drawing.Image)(resources.GetObject("tsbQLNV.Image")));
            this.tsbQLNV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbQLNV.Name = "tsbQLNV";
            this.tsbQLNV.Size = new System.Drawing.Size(153, 24);
            this.tsbQLNV.Text = "Quản lý TT nhân viên";
            this.tsbQLNV.Click += new System.EventHandler(this.toolQLNV_Click);
            // 
            // tsbCCTL
            // 
            this.tsbCCTL.Image = ((System.Drawing.Image)(resources.GetObject("tsbCCTL.Image")));
            this.tsbCCTL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCCTL.Name = "tsbCCTL";
            this.tsbCCTL.Size = new System.Drawing.Size(178, 24);
            this.tsbCCTL.Text = "Chấm công và tính lương";
            this.tsbCCTL.Click += new System.EventHandler(this.toolCCTL_Click);
            // 
            // tsbBCNS
            // 
            this.tsbBCNS.Image = ((System.Drawing.Image)(resources.GetObject("tsbBCNS.Image")));
            this.tsbBCNS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBCNS.Name = "tsbBCNS";
            this.tsbBCNS.Size = new System.Drawing.Size(129, 24);
            this.tsbBCNS.Text = "Báo cáo nhân sự";
            this.tsbBCNS.Click += new System.EventHandler(this.toolBCNS_Click);
            // 
            // tsbExit
            // 
            this.tsbExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbExit.Image")));
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(65, 24);
            this.tsbExit.Text = "Thoát";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 547);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMain";
            this.Text = "Quản lý nhân sự";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbQLNV;
        private System.Windows.Forms.ToolStripButton tsbCCTL;
        private System.Windows.Forms.ToolStripButton tsbBCNS;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.ToolStripMenuItem hệThốngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chứcNăngToolStripMenuItem;
    }
}

