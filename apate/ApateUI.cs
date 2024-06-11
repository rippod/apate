
namespace apate
{
	public partial class ApateUI : Form
	{
		readonly static byte[] fileHead = new byte[] { };
		readonly static byte[] jpgHead = new byte[] { 0xff, 0xd8, 0xff, 0xe1 };
		readonly static byte[] movHead = new byte[] { 0x6d, 0x6f, 0x6f, 0x76 };
		readonly static byte[] mp4Head = new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x69,
			0x73, 0x6F, 0x6D, 0x00, 0x00, 0x02, 0x00, 0x69, 0x73, 0x6F, 0x6D, 0x69, 0x73, 0x6F, 0x32, 0x61,
			0x76, 0x63, 0x31, 0x6D, 0x70, 0x34, 0x31 };
		readonly static byte[] exeHead = new byte[] { 0x4D, 0x5A, 0x90, 0x00, 0x03, 0x00, 0x00, 0x00, 0x04,
			0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40,
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x0E, 0x1F, 0xBA, 0x0E, 0x00, 0xB4, 0x09, 0xCD, 0x21,
			0xB8, 0x01, 0x4C, 0xCD, 0x21, 0x54, 0x68, 0x69, 0x73, 0x20, 0x70, 0x72, 0x6F, 0x67, 0x72, 0x61,
			0x6D, 0x20, 0x63, 0x61, 0x6E, 0x6E, 0x6F, 0x74, 0x20, 0x62, 0x65, 0x20, 0x72, 0x75, 0x6E, 0x20,
			0x69, 0x6E, 0x20, 0x44, 0x4F, 0x53, 0x20, 0x6D, 0x6F, 0x64, 0x65, 0x2E, 0x0D, 0x0D, 0x0A, 0x24,
			0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

		private Disguiser? _disguiser = null;
		//private string maskFilePath;
		public ApateUI()
		{
			InitializeComponent();
			ModeSelect(ModeEnum.OneKey);
		}

		private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_disguiser?.Dispose();
			this.Dispose();
		}

		private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			AboutUI aboutUI = new AboutUI();
			aboutUI.TopMost = TopMost;
			aboutUI.ShowDialog();
		}

		private void MaskFileLabel_DragDrop(object sender, DragEventArgs e)
		{
			toolStripStatusLabel1.Text = "正在处理";
			if (e.Data.GetData(DataFormats.FileDrop) is not string[] fileObjectArray)
			{
				toolStripStatusLabel1.Text = "程序内部错误，值类型不正确";
				return;
			}
			else if (fileObjectArray.Length > 1)//如果拖入了多份文件
			{
				toolStripStatusLabel1.Text = "仅可拖入1份面具文件，请更换其他面具文件并重新拖入";
				return;
			}
			try
			{
				_disguiser = new Disguiser(fileObjectArray[0]);
			}
			catch (Exception exp)
			{
				toolStripStatusLabel1.Text = exp.Message;
				return;
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
			if (e.Data.GetData(DataFormats.FileDrop) is not string[] selectPath)
			{
				toolStripStatusLabel1.Text = "文件列表无效";
				return;
			}
			if (_disguiser != null)//如果面具文件有效
			{
				(int successCount, AggregateException exception) = Disguiser.DirectoryTree(selectPath, _disguiser.Disguise);
				toolStripStatusLabel1.Text = $"完成！成功{successCount}个，失败{exception.InnerExceptions.Count}个";
				if (exception.InnerExceptions.Any())
				{
					MessageBox.Show(string.Join("\r\n", exception.InnerExceptions.Select(e => e.Message))); 
				}
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
			InfoBox infoBox = new InfoBox("注意！", "如果拖入未经过伪装的文件，可能会对该文件造成严重的数据破坏，且无法恢复！请务必做好备份！\r\n是否继续？");
			//如果主窗口是置顶，则弹窗也置顶
			infoBox.TopMost = TopMost;
			if (infoBox.ShowDialog() == DialogResult.Yes)
			{
				toolStripStatusLabel1.Text = "正在处理";
				if (e.Data.GetData(DataFormats.FileDrop) is not string[] selectPath)
				{
					toolStripStatusLabel1.Text = "文件列表无效";
					return;
				}
				(int successCount, AggregateException exception) = Disguiser.DirectoryTree(selectPath, Disguiser.Reveal);
				toolStripStatusLabel1.Text = $"完成！成功{successCount} 个，失败 {exception.InnerExceptions.Count}个";
				if (exception.InnerExceptions.Any())
				{
					MessageBox.Show(string.Join("\r\n", exception.InnerExceptions.Select(e=>e.Message)));
				}
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
			_disguiser?.Dispose();
			_disguiser = null;
			一键伪装ToolStripMenuItem.Checked = false;
			面具伪装ToolStripMenuItem.Checked = false;
			简易伪装ToolStripMenuItem.Checked = false;
			eXEToolStripMenuItem.Checked = false;
			jPGToolStripMenuItem.Checked = false;
			mP4ToolStripMenuItem.Checked = false;
			mOVToolStripMenuItem.Checked = false;
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
					TrueFileDragLabel.AllowDrop = true;
					_disguiser = new Disguiser(Properties.Resources.mask, ".mp4");
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
					_disguiser = new Disguiser(exeHead, ".exe");
					TrueFileDragLabel.AllowDrop = true;
					TrueFileDragLabel.Image = Properties.Resources.drag;
					TrueFileDragLabel.Text = "\r\n\r\n\r\n拖入\r\n伪装为EXE";
					break;
				case ModeEnum.Jpg://JPG
					简易伪装ToolStripMenuItem.Checked = true;
					jPGToolStripMenuItem.Checked = true;
					_disguiser = new Disguiser(jpgHead, ".jpg");
					TrueFileDragLabel.AllowDrop = true;
					TrueFileDragLabel.Image = Properties.Resources.drag;
					TrueFileDragLabel.Text = "\r\n\r\n\r\n拖入\r\n伪装为JPG";
					break;
				case ModeEnum.Mp4://MP4
					简易伪装ToolStripMenuItem.Checked = true;
					mP4ToolStripMenuItem.Checked = true;
					_disguiser = new Disguiser(mp4Head, ".mp4");
					TrueFileDragLabel.AllowDrop = true;
					TrueFileDragLabel.Image = Properties.Resources.drag;
					TrueFileDragLabel.Text = "\r\n\r\n\r\n拖入\r\n伪装为MP4";
					break;
				case ModeEnum.Mov://MOV
					简易伪装ToolStripMenuItem.Checked = true;
					mOVToolStripMenuItem.Checked = true;
					_disguiser = new Disguiser(movHead, ".mov");
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
