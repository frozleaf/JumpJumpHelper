namespace JumpJumpHelper.UIs
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCapture = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonManual = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAuto = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMark = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStartPoint = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelEndPoint = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelScreenSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemManual = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabelTimeCoefficient = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCapture,
            this.toolStripButtonManual,
            this.toolStripButtonAuto,
            this.toolStripButtonMark});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(553, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonCapture
            // 
            this.toolStripButtonCapture.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCapture.Image")));
            this.toolStripButtonCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCapture.Name = "toolStripButtonCapture";
            this.toolStripButtonCapture.Size = new System.Drawing.Size(52, 22);
            this.toolStripButtonCapture.Text = "截屏";
            this.toolStripButtonCapture.Visible = false;
            this.toolStripButtonCapture.Click += new System.EventHandler(this.toolStripButtonCapture_Click);
            // 
            // toolStripButtonManual
            // 
            this.toolStripButtonManual.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonManual.Image")));
            this.toolStripButtonManual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonManual.Name = "toolStripButtonManual";
            this.toolStripButtonManual.Size = new System.Drawing.Size(76, 22);
            this.toolStripButtonManual.Text = "手动跳跃";
            this.toolStripButtonManual.Visible = false;
            this.toolStripButtonManual.Click += new System.EventHandler(this.toolStripButtonMannual_Click);
            // 
            // toolStripButtonAuto
            // 
            this.toolStripButtonAuto.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAuto.Image")));
            this.toolStripButtonAuto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAuto.Name = "toolStripButtonAuto";
            this.toolStripButtonAuto.Size = new System.Drawing.Size(76, 22);
            this.toolStripButtonAuto.Text = "自动跳跃";
            this.toolStripButtonAuto.Click += new System.EventHandler(this.toolStripButtonAuto_Click);
            // 
            // toolStripButtonMark
            // 
            this.toolStripButtonMark.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMark.Image")));
            this.toolStripButtonMark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMark.Name = "toolStripButtonMark";
            this.toolStripButtonMark.Size = new System.Drawing.Size(88, 22);
            this.toolStripButtonMark.Text = "标注关键点";
            this.toolStripButtonMark.Visible = false;
            this.toolStripButtonMark.Click += new System.EventHandler(this.toolStripButtonMark_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStartPoint,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabelEndPoint,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabelScreenSize,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabelTimeCoefficient,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 444);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(553, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStartPoint
            // 
            this.toolStripStatusLabelStartPoint.Name = "toolStripStatusLabelStartPoint";
            this.toolStripStatusLabelStartPoint.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabelStartPoint.Text = "起点";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // toolStripStatusLabelEndPoint
            // 
            this.toolStripStatusLabelEndPoint.Name = "toolStripStatusLabelEndPoint";
            this.toolStripStatusLabelEndPoint.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabelEndPoint.Text = "终点";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel4.Text = "|";
            // 
            // toolStripStatusLabelScreenSize
            // 
            this.toolStripStatusLabelScreenSize.Name = "toolStripStatusLabelScreenSize";
            this.toolStripStatusLabelScreenSize.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabelScreenSize.Text = "屏幕尺寸";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel6.Text = "|";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(239, 17);
            this.toolStripStatusLabel7.Spring = true;
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabelStatus.Text = "状态";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(553, 394);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemMode});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(553, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemLoad,
            this.toolStripMenuItemQuit});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(58, 21);
            this.toolStripMenuItemFile.Text = "文件(&F)";
            // 
            // toolStripMenuItemLoad
            // 
            this.toolStripMenuItemLoad.Name = "toolStripMenuItemLoad";
            this.toolStripMenuItemLoad.Size = new System.Drawing.Size(138, 22);
            this.toolStripMenuItemLoad.Text = "加载图像(&L)";
            this.toolStripMenuItemLoad.Visible = false;
            this.toolStripMenuItemLoad.Click += new System.EventHandler(this.toolStripMenuItemLoad_Click);
            // 
            // toolStripMenuItemQuit
            // 
            this.toolStripMenuItemQuit.Name = "toolStripMenuItemQuit";
            this.toolStripMenuItemQuit.Size = new System.Drawing.Size(138, 22);
            this.toolStripMenuItemQuit.Text = "退出(&Q)";
            this.toolStripMenuItemQuit.Click += new System.EventHandler(this.toolStripMenuItemQuit_Click);
            // 
            // toolStripMenuItemMode
            // 
            this.toolStripMenuItemMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemManual,
            this.toolStripMenuItemAuto});
            this.toolStripMenuItemMode.Name = "toolStripMenuItemMode";
            this.toolStripMenuItemMode.Size = new System.Drawing.Size(64, 21);
            this.toolStripMenuItemMode.Text = "模式(&M)";
            // 
            // toolStripMenuItemManual
            // 
            this.toolStripMenuItemManual.Name = "toolStripMenuItemManual";
            this.toolStripMenuItemManual.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItemManual.Text = "手动模式(&M)";
            this.toolStripMenuItemManual.Click += new System.EventHandler(this.toolStripMenuItemManual_Click);
            // 
            // toolStripMenuItemAuto
            // 
            this.toolStripMenuItemAuto.Checked = true;
            this.toolStripMenuItemAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemAuto.Name = "toolStripMenuItemAuto";
            this.toolStripMenuItemAuto.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItemAuto.Text = "自动模式(&A)";
            this.toolStripMenuItemAuto.Click += new System.EventHandler(this.toolStripMenuItemAuto_Click);
            // 
            // toolStripStatusLabelTimeCoefficient
            // 
            this.toolStripStatusLabelTimeCoefficient.Name = "toolStripStatusLabelTimeCoefficient";
            this.toolStripStatusLabelTimeCoefficient.Size = new System.Drawing.Size(80, 17);
            this.toolStripStatusLabelTimeCoefficient.Text = "当前时间系数";
            this.toolStripStatusLabelTimeCoefficient.Visible = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 466);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "跳一跳助手";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCapture;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStartPoint;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelEndPoint;
        private System.Windows.Forms.ToolStripButton toolStripButtonManual;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAuto;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelScreenSize;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemQuit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMode;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemManual;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAuto;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLoad;
        private System.Windows.Forms.ToolStripButton toolStripButtonMark;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTimeCoefficient;
    }
}

