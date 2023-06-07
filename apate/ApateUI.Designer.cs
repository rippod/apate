namespace apate
{
    partial class ApateUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApateUI));
            menuStrip1 = new MenuStrip();
            选项ToolStripMenuItem = new ToolStripMenuItem();
            一键伪装ToolStripMenuItem = new ToolStripMenuItem();
            面具伪装ToolStripMenuItem = new ToolStripMenuItem();
            简易伪装ToolStripMenuItem = new ToolStripMenuItem();
            eXEToolStripMenuItem = new ToolStripMenuItem();
            jPGToolStripMenuItem = new ToolStripMenuItem();
            mP4ToolStripMenuItem = new ToolStripMenuItem();
            mOVToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            窗口置顶ToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            退出ToolStripMenuItem = new ToolStripMenuItem();
            帮助ToolStripMenuItem = new ToolStripMenuItem();
            关于ToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            WholeSplitContainer = new SplitContainer();
            MaskDisguiseZoneSplitContainer = new SplitContainer();
            MaskFileDragLabel = new Label();
            TrueFileDragLabel = new Label();
            RevealFileDragLabel = new Label();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WholeSplitContainer).BeginInit();
            WholeSplitContainer.Panel1.SuspendLayout();
            WholeSplitContainer.Panel2.SuspendLayout();
            WholeSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MaskDisguiseZoneSplitContainer).BeginInit();
            MaskDisguiseZoneSplitContainer.Panel1.SuspendLayout();
            MaskDisguiseZoneSplitContainer.Panel2.SuspendLayout();
            MaskDisguiseZoneSplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 选项ToolStripMenuItem, 帮助ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(522, 25);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // 选项ToolStripMenuItem
            // 
            选项ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 一键伪装ToolStripMenuItem, 面具伪装ToolStripMenuItem, 简易伪装ToolStripMenuItem, toolStripSeparator2, 窗口置顶ToolStripMenuItem, toolStripSeparator1, 退出ToolStripMenuItem });
            选项ToolStripMenuItem.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            选项ToolStripMenuItem.Name = "选项ToolStripMenuItem";
            选项ToolStripMenuItem.Size = new Size(44, 21);
            选项ToolStripMenuItem.Text = "选项";
            // 
            // 一键伪装ToolStripMenuItem
            // 
            一键伪装ToolStripMenuItem.Checked = true;
            一键伪装ToolStripMenuItem.CheckState = CheckState.Checked;
            一键伪装ToolStripMenuItem.Name = "一键伪装ToolStripMenuItem";
            一键伪装ToolStripMenuItem.Size = new Size(180, 22);
            一键伪装ToolStripMenuItem.Text = "一键伪装";
            一键伪装ToolStripMenuItem.ToolTipText = "使用预置面具文件，对真身文件进行伪装\r\n伪装后，真身文件看起来与面具文件一样\r\n适用大部分应用场景";
            一键伪装ToolStripMenuItem.Click += 一键伪装ToolStripMenuItem_Click;
            // 
            // 面具伪装ToolStripMenuItem
            // 
            面具伪装ToolStripMenuItem.Name = "面具伪装ToolStripMenuItem";
            面具伪装ToolStripMenuItem.Size = new Size(180, 22);
            面具伪装ToolStripMenuItem.Text = "面具伪装";
            面具伪装ToolStripMenuItem.ToolTipText = "使用自定义面具文件，对真身文件进行伪装\r\n伪装后，真身文件看起来与面具文件一样\r\n适用范围取决于面具文件的选择，建议在一键伪装失效时尝试使用";
            面具伪装ToolStripMenuItem.Click += 拼接伪装模式ToolStripMenuItem_Click;
            // 
            // 简易伪装ToolStripMenuItem
            // 
            简易伪装ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { eXEToolStripMenuItem, jPGToolStripMenuItem, mP4ToolStripMenuItem, mOVToolStripMenuItem });
            简易伪装ToolStripMenuItem.Name = "简易伪装ToolStripMenuItem";
            简易伪装ToolStripMenuItem.Size = new Size(180, 22);
            简易伪装ToolStripMenuItem.Text = "简易伪装";
            // 
            // eXEToolStripMenuItem
            // 
            eXEToolStripMenuItem.Name = "eXEToolStripMenuItem";
            eXEToolStripMenuItem.Size = new Size(180, 22);
            eXEToolStripMenuItem.Text = "EXE";
            eXEToolStripMenuItem.ToolTipText = "不使用面具文件，而是使用EXE文件的二进制特征文件头，对真身文件进行伪装\r\n伪装后，真身文件对于操作系统来说已经是EXE格式，只是无法被双击执行\r\n建议真身文件不超过2G";
            eXEToolStripMenuItem.Click += eXEToolStripMenuItem_Click;
            // 
            // jPGToolStripMenuItem
            // 
            jPGToolStripMenuItem.Name = "jPGToolStripMenuItem";
            jPGToolStripMenuItem.Size = new Size(180, 22);
            jPGToolStripMenuItem.Text = "JPG";
            jPGToolStripMenuItem.ToolTipText = "不使用面具文件，而是使用JPG文件的二进制特征文件头，对真身文件进行伪装\r\n伪装后，真身文件对于操作系统来说已经是JPG格式，只是无法被预览\r\n适配场景较少，不建议使用";
            jPGToolStripMenuItem.Click += jPGToolStripMenuItem_Click;
            // 
            // mP4ToolStripMenuItem
            // 
            mP4ToolStripMenuItem.Name = "mP4ToolStripMenuItem";
            mP4ToolStripMenuItem.Size = new Size(180, 22);
            mP4ToolStripMenuItem.Text = "MP4";
            mP4ToolStripMenuItem.ToolTipText = "不使用面具文件，而是使用MP4文件的二进制特征文件头，对真身文件进行伪装\r\n伪装后，真身文件对于操作系统来说已经是MP4格式，只是无法被播放\r\n适配场景较少，不建议使用";
            mP4ToolStripMenuItem.Click += mP4ToolStripMenuItem_Click;
            // 
            // mOVToolStripMenuItem
            // 
            mOVToolStripMenuItem.Name = "mOVToolStripMenuItem";
            mOVToolStripMenuItem.Size = new Size(180, 22);
            mOVToolStripMenuItem.Text = "MOV";
            mOVToolStripMenuItem.ToolTipText = "不使用面具文件，而是使用MOV文件的二进制特征文件头，对真身文件进行伪装\r\n伪装后，真身文件对于操作系统来说已经是MOV格式，只是无法被播放\r\n适配场景较少，不建议使用";
            mOVToolStripMenuItem.Click += mOVToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(177, 6);
            // 
            // 窗口置顶ToolStripMenuItem
            // 
            窗口置顶ToolStripMenuItem.Checked = true;
            窗口置顶ToolStripMenuItem.CheckState = CheckState.Checked;
            窗口置顶ToolStripMenuItem.Name = "窗口置顶ToolStripMenuItem";
            窗口置顶ToolStripMenuItem.Size = new Size(180, 22);
            窗口置顶ToolStripMenuItem.Text = "窗口置顶";
            窗口置顶ToolStripMenuItem.Click += 窗口置顶ToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            退出ToolStripMenuItem.Size = new Size(180, 22);
            退出ToolStripMenuItem.Text = "退出";
            退出ToolStripMenuItem.Click += 退出ToolStripMenuItem_Click;
            // 
            // 帮助ToolStripMenuItem
            // 
            帮助ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 关于ToolStripMenuItem });
            帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            帮助ToolStripMenuItem.Size = new Size(44, 21);
            帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 关于ToolStripMenuItem
            // 
            关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            关于ToolStripMenuItem.Size = new Size(100, 22);
            关于ToolStripMenuItem.Text = "关于";
            关于ToolStripMenuItem.Click += 关于ToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 296);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(522, 22);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(32, 17);
            toolStripStatusLabel1.Text = "就绪";
            // 
            // WholeSplitContainer
            // 
            WholeSplitContainer.Dock = DockStyle.Fill;
            WholeSplitContainer.IsSplitterFixed = true;
            WholeSplitContainer.Location = new Point(0, 25);
            WholeSplitContainer.Name = "WholeSplitContainer";
            // 
            // WholeSplitContainer.Panel1
            // 
            WholeSplitContainer.Panel1.Controls.Add(MaskDisguiseZoneSplitContainer);
            // 
            // WholeSplitContainer.Panel2
            // 
            WholeSplitContainer.Panel2.Controls.Add(RevealFileDragLabel);
            WholeSplitContainer.Size = new Size(522, 271);
            WholeSplitContainer.SplitterDistance = 346;
            WholeSplitContainer.SplitterWidth = 3;
            WholeSplitContainer.TabIndex = 7;
            // 
            // MaskDisguiseZoneSplitContainer
            // 
            MaskDisguiseZoneSplitContainer.Dock = DockStyle.Fill;
            MaskDisguiseZoneSplitContainer.IsSplitterFixed = true;
            MaskDisguiseZoneSplitContainer.Location = new Point(0, 0);
            MaskDisguiseZoneSplitContainer.Name = "MaskDisguiseZoneSplitContainer";
            // 
            // MaskDisguiseZoneSplitContainer.Panel1
            // 
            MaskDisguiseZoneSplitContainer.Panel1.Controls.Add(MaskFileDragLabel);
            // 
            // MaskDisguiseZoneSplitContainer.Panel2
            // 
            MaskDisguiseZoneSplitContainer.Panel2.Controls.Add(TrueFileDragLabel);
            MaskDisguiseZoneSplitContainer.Size = new Size(346, 271);
            MaskDisguiseZoneSplitContainer.SplitterDistance = 170;
            MaskDisguiseZoneSplitContainer.SplitterWidth = 3;
            MaskDisguiseZoneSplitContainer.TabIndex = 0;
            // 
            // MaskFileDragLabel
            // 
            MaskFileDragLabel.AllowDrop = true;
            MaskFileDragLabel.BackColor = Color.LightGreen;
            MaskFileDragLabel.Dock = DockStyle.Fill;
            MaskFileDragLabel.Font = new Font("等线", 18F, FontStyle.Bold, GraphicsUnit.Point);
            MaskFileDragLabel.ImageAlign = ContentAlignment.TopCenter;
            MaskFileDragLabel.Location = new Point(0, 0);
            MaskFileDragLabel.Name = "MaskFileDragLabel";
            MaskFileDragLabel.Size = new Size(170, 271);
            MaskFileDragLabel.TabIndex = 0;
            MaskFileDragLabel.TextAlign = ContentAlignment.MiddleCenter;
            MaskFileDragLabel.DragDrop += MaskFileLabel_DragDrop;
            MaskFileDragLabel.DragEnter += MaskFileLabel_DragEnter;
            // 
            // TrueFileDragLabel
            // 
            TrueFileDragLabel.BackColor = Color.Gold;
            TrueFileDragLabel.Dock = DockStyle.Fill;
            TrueFileDragLabel.Font = new Font("等线", 18F, FontStyle.Bold, GraphicsUnit.Point);
            TrueFileDragLabel.ImageAlign = ContentAlignment.TopCenter;
            TrueFileDragLabel.Location = new Point(0, 0);
            TrueFileDragLabel.Name = "TrueFileDragLabel";
            TrueFileDragLabel.Size = new Size(173, 271);
            TrueFileDragLabel.TabIndex = 0;
            TrueFileDragLabel.TextAlign = ContentAlignment.MiddleCenter;
            TrueFileDragLabel.DragDrop += TrueFileLabel_DragDrop;
            TrueFileDragLabel.DragEnter += TrueFileLabel_DragEnter;
            // 
            // RevealFileDragLabel
            // 
            RevealFileDragLabel.AllowDrop = true;
            RevealFileDragLabel.BackColor = Color.Violet;
            RevealFileDragLabel.Dock = DockStyle.Fill;
            RevealFileDragLabel.Font = new Font("等线", 18F, FontStyle.Bold, GraphicsUnit.Point);
            RevealFileDragLabel.Image = Properties.Resources.drag;
            RevealFileDragLabel.ImageAlign = ContentAlignment.TopCenter;
            RevealFileDragLabel.Location = new Point(0, 0);
            RevealFileDragLabel.Name = "RevealFileDragLabel";
            RevealFileDragLabel.Size = new Size(173, 271);
            RevealFileDragLabel.TabIndex = 0;
            RevealFileDragLabel.Text = "\r\n\r\n\r\n拖入\r\n进行还原";
            RevealFileDragLabel.TextAlign = ContentAlignment.MiddleCenter;
            RevealFileDragLabel.DragDrop += RevealMaskLabel_DragDrop;
            RevealFileDragLabel.DragEnter += RevealMaskLabel_DragEnter;
            // 
            // ApateUI
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(522, 318);
            Controls.Add(WholeSplitContainer);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2, 3, 2, 3);
            MaximizeBox = false;
            MinimumSize = new Size(538, 357);
            Name = "ApateUI";
            Text = "Apate";
            TopMost = true;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            WholeSplitContainer.Panel1.ResumeLayout(false);
            WholeSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)WholeSplitContainer).EndInit();
            WholeSplitContainer.ResumeLayout(false);
            MaskDisguiseZoneSplitContainer.Panel1.ResumeLayout(false);
            MaskDisguiseZoneSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)MaskDisguiseZoneSplitContainer).EndInit();
            MaskDisguiseZoneSplitContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 帮助ToolStripMenuItem;
        private ToolStripMenuItem 关于ToolStripMenuItem;
        private ToolStripMenuItem 选项ToolStripMenuItem;
        private ToolStripMenuItem 退出ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private SplitContainer WholeSplitContainer;
        private Label MaskFileDragLabel;
        private Label TrueFileDragLabel;
        private Label RevealFileDragLabel;
        private ToolStripMenuItem 面具伪装ToolStripMenuItem;
        private SplitContainer MaskDisguiseZoneSplitContainer;
        private ToolStripMenuItem 简易伪装ToolStripMenuItem;
        private ToolStripMenuItem eXEToolStripMenuItem;
        private ToolStripMenuItem mP4ToolStripMenuItem;
        private ToolStripMenuItem mOVToolStripMenuItem;
        private ToolStripMenuItem 一键伪装ToolStripMenuItem;
        private ToolStripMenuItem 窗口置顶ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem jPGToolStripMenuItem;
    }
}