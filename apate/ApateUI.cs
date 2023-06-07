namespace apate
{
    public partial class ApateUI : Form
    {
        private byte[] maskBytes = new byte[] { };
        private string maskExtension = "";
        //private string maskFilePath;
        public ApateUI()
        {
            InitializeComponent();
            ModeSelect(ModeEnum.OneKey);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUI aboutUI = new AboutUI();
            //如果主窗口是置顶，则弹窗也置顶
            if (TopMost == true)
            {
                aboutUI.TopMost = true;
            }
            else
            {
                aboutUI.TopMost = false;
            }
            aboutUI.ShowDialog();
        }

        private void MaskFileLabel_DragDrop(object sender, DragEventArgs e)
        {
            toolStripStatusLabel1.Text = "正在处理";
            Array fileObjectArray = (Array)e.Data.GetData(DataFormats.FileDrop);
            List<string> filePathList = new List<string>();
            for (int i = 0; i < fileObjectArray.Length; i++)
            {
                filePathList.Add(fileObjectArray.GetValue(i).ToString());
            }
            if (filePathList.Count > 1)//如果拖入了多份文件
            {
                toolStripStatusLabel1.Text = "仅可拖入1份面具文件，请更换其他面具文件并重新拖入";
                return;
            }
            else if (Directory.Exists(filePathList[0]))//如果拖入的是文件夹
            {
                toolStripStatusLabel1.Text = "不支持将文件夹作为面具，请更换其他面具文件并重新拖入";
                return;
            }
            if (new FileInfo(filePathList[0]).Length > Program.maximumMaskLength)//文件长度检测
            {
                toolStripStatusLabel1.Text = "面具文件过大，请更换其他面具文件（小于" + Program.maximumMaskLength / 1024 / 1024 + "MB）并重新拖入";
                return;
            }
            maskBytes = Program.FileToBytes(filePathList[0]);//将文件转换为字节数组
            maskExtension = new FileInfo(filePathList[0]).Extension;//保存文件扩展名
            if (maskBytes.Length == 0)
            {
                toolStripStatusLabel1.Text = "未知错误，请更换其他面具文件并重新拖入";
            }
            toolStripStatusLabel1.Text = "完成！";
            //激活伪装区
            MaskFileDragLabel.Image = Properties.Resources.accept;
            MaskFileDragLabel.Text = "";
            TrueFileDragLabel.AllowDrop = true;
            TrueFileDragLabel.Image = Properties.Resources.drag;
            TrueFileDragLabel.Text = "\r\n\r\n\r\n拖入\r\n进行伪装";
        }

        private void MaskFileLabel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void TrueFileLabel_DragDrop(object sender, DragEventArgs e)
        {
            toolStripStatusLabel1.Text = "正在处理";
            Array fileObjectArray = (Array)e.Data.GetData(DataFormats.FileDrop);
            List<string> filePathList = new List<string>();
            for (int i = 0; i < fileObjectArray.Length; i++)
            {
                filePathList.AddRange(Program.GetAllFilesRecursively(fileObjectArray.GetValue(i).ToString()));//递归遍历文件夹，并将文件添加到filePathList
            }


            if (maskBytes.Length > 0)//如果面具文件有效
            {
                int successCount = 0;
                int failCount = 0;
                for (int i = 0; i < filePathList.Count; i++)
                {

                    if (Program.Disguise(filePathList[i], maskBytes) == 1)//如果伪装成功
                    {
                        successCount++;
                        File.Move(filePathList[i], filePathList[i] + maskExtension);
                    }
                    else//如果伪装失败
                    {
                        failCount++;

                    }
                }
                toolStripStatusLabel1.Text = "完成！成功" + successCount + "个，失败" + failCount + "个";
            }
            else
            {
                toolStripStatusLabel1.Text = "尚未拖入面具文件";
            }
        }

        private void TrueFileLabel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void RevealMaskLabel_DragDrop(object sender, DragEventArgs e)
        {
            Activate();
            //弹窗确认
            InfoBox infoBox = new InfoBox("注意！", "如果拖入未经过伪装的文件，可能会对该文件造成严重的数据破坏，且无法恢复！请务必做好备份！\r\n\r\n是否继续？");
            //如果主窗口是置顶，则弹窗也置顶
            if (TopMost == true)
            {
                infoBox.TopMost = true;
            }
            else
            {
                infoBox.TopMost = false;
            }
            if (infoBox.ShowDialog() == DialogResult.Yes)
            {
                toolStripStatusLabel1.Text = "正在处理";
                Array fileObjectArray = (Array)e.Data.GetData(DataFormats.FileDrop);
                List<string> filePathList = new List<string>();
                for (int i = 0; i < fileObjectArray.Length; i++)
                {
                    filePathList.AddRange(Program.GetAllFilesRecursively(fileObjectArray.GetValue(i).ToString()));//递归遍历文件夹，并将文件添加到filePathList
                }
                int successCount = 0;
                int failCount = 0;
                for (int i = 0; i < filePathList.Count; i++)
                {
                    if (Program.Reveal(filePathList[i]) == 1)
                    {
                        successCount++;
                        File.Move(filePathList[i], filePathList[i].Substring(0, filePathList[i].LastIndexOf('.')));
                    }
                    else
                    {
                        failCount++;
                    }
                }
                toolStripStatusLabel1.Text = "完成！成功" + successCount + "个，失败" + failCount + "个";
            }
        }

        private void RevealMaskLabel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ModeSelect(ModeEnum mode)
        {
            一键伪装ToolStripMenuItem.Checked = false;
            面具伪装ToolStripMenuItem.Checked = false;
            简易伪装ToolStripMenuItem.Checked = false;
            eXEToolStripMenuItem.Checked = false;
            jPGToolStripMenuItem.Checked = false;
            mP4ToolStripMenuItem.Checked = false;
            mOVToolStripMenuItem.Checked = false;
            maskBytes = new byte[] { };
            maskExtension = "";
            MaskFileDragLabel.AllowDrop = false;
            MaskFileDragLabel.Image = Properties.Resources.cancel;
            MaskFileDragLabel.Text = "";
            TrueFileDragLabel.AllowDrop = false;
            TrueFileDragLabel.Image = Properties.Resources.cancel;
            TrueFileDragLabel.Text = "";
            switch (mode)
            {

                case ModeEnum.OneKey://一键伪装
                    一键伪装ToolStripMenuItem.Checked = true;
                    maskBytes = Properties.Resources.mask;
                    maskExtension = ".mp4";
                    TrueFileDragLabel.AllowDrop = true;
                    TrueFileDragLabel.Image = Properties.Resources.drag;
                    TrueFileDragLabel.Text = "\r\n\r\n\r\n拖入\r\n一键伪装";
                    break;
                case ModeEnum.Mask://面具伪装
                    面具伪装ToolStripMenuItem.Checked = true;
                    MaskFileDragLabel.AllowDrop = true;
                    MaskFileDragLabel.Image = Properties.Resources.drag;
                    MaskFileDragLabel.Text = "\r\n\r\n\r\n拖入\r\n面具文件";
                    break;
                case ModeEnum.Exe://EXE
                    简易伪装ToolStripMenuItem.Checked = true;
                    eXEToolStripMenuItem.Checked = true;
                    maskBytes = Program.exeHead;
                    maskExtension = ".exe";
                    TrueFileDragLabel.AllowDrop = true;
                    TrueFileDragLabel.Image = Properties.Resources.drag;
                    TrueFileDragLabel.Text = "\r\n\r\n\r\n拖入\r\n伪装为EXE";
                    break;
                case ModeEnum.Jpg://JPG
                    简易伪装ToolStripMenuItem.Checked = true;
                    jPGToolStripMenuItem.Checked = true;
                    maskBytes = Program.jpgHead;
                    maskExtension = ".jpg";
                    TrueFileDragLabel.AllowDrop = true;
                    TrueFileDragLabel.Image = Properties.Resources.drag;
                    TrueFileDragLabel.Text = "\r\n\r\n\r\n拖入\r\n伪装为JPG";
                    break;
                case ModeEnum.Mp4://MP4
                    简易伪装ToolStripMenuItem.Checked = true;
                    mP4ToolStripMenuItem.Checked = true;
                    maskBytes = Program.mp4Head;
                    maskExtension = ".mp4";
                    TrueFileDragLabel.AllowDrop = true;
                    TrueFileDragLabel.Image = Properties.Resources.drag;
                    TrueFileDragLabel.Text = "\r\n\r\n\r\n拖入\r\n伪装为MP4";
                    break;
                case ModeEnum.Mov://MOV
                    简易伪装ToolStripMenuItem.Checked = true;
                    mOVToolStripMenuItem.Checked = true;
                    maskBytes = Program.movHead;
                    maskExtension = ".mov";
                    TrueFileDragLabel.AllowDrop = true;
                    TrueFileDragLabel.Image = Properties.Resources.drag;
                    TrueFileDragLabel.Text = "\r\n\r\n\r\n拖入\r\n伪装为MOV";
                    break;
            }
        }

        private void 一键伪装ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModeSelect(ModeEnum.OneKey);
        }

        private void 拼接伪装模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModeSelect(ModeEnum.Mask);
        }

        private void eXEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModeSelect(ModeEnum.Exe);

        }

        private void jPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModeSelect(ModeEnum.Jpg);
        }

        private void mP4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModeSelect(ModeEnum.Mp4);
        }

        private void mOVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModeSelect(ModeEnum.Mov);
        }

        private void 窗口置顶ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = !TopMost;
            窗口置顶ToolStripMenuItem.Checked = !窗口置顶ToolStripMenuItem.Checked;
        }

    }
}
