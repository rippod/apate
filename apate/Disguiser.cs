using System.Buffers;
using System.IO;

namespace apate;
/// <summary>
/// 伪装器
/// </summary>
public class Disguiser : IDisposable
{
	private readonly Stream _mask;
	string _maskExtension;
	/// <summary>
	/// 通过本地构造面具流
	/// </summary>
	/// <param name="path">本地文件路径</param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public Disguiser(string path, long maxLength = 2147483647 / 7)//  2GB/7=约300MB
	{
		try
		{
			if (!File.Exists(path))
			{
				throw new ArgumentOutOfRangeException(nameof(path), "文件不存在/无访问权限");
			}
			_maskExtension = Path.GetExtension(path);
			if (!_maskExtension.StartsWith('.') || _maskExtension.Length == 1)
			{
				throw new ArgumentOutOfRangeException(nameof(path), "面具文件必须具有扩展名");
			}
			_mask = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
			if (_mask.Length == 0)
			{
				throw new ArgumentOutOfRangeException(nameof(path), "面具文件为空");
			}
			else if (_mask.Length > maxLength)
			{
				throw new ArgumentOutOfRangeException(nameof(path), "面具文件过大，请更换其他面具文件（小于" + maxLength / 1024 / 1024 + "MB）并重新拖入");
			}
		}
		catch (Exception)
		{
			Dispose();
			throw;
		}
	}
	/// <summary>
	/// 通过已有的二进制文件构造面具流
	/// </summary>
	/// <param name="resources">二进制资源</param>
	/// <param name="maskExtension">面具扩展名</param>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public Disguiser(byte[] resources, string maskExtension)
	{
		try
		{
			if (maskExtension == null || !maskExtension.StartsWith('.') || maskExtension.Length == 1)
			{
				throw new ArgumentOutOfRangeException(nameof(maskExtension), "面具文件必须具有扩展名");
			}
			_mask = new MemoryStream(resources);
			_maskExtension = maskExtension;
		}
		catch (Exception)
		{
			Dispose();
			throw;
		}
	}
	/// <summary>
	/// 伪装文件
	/// </summary>
	/// <param name="filePath"></param>
	public void Disguise(string filePath)
	{
		_mask.Position = 0;//面具文件流被重复利用，使用前先归位
		{
			using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			using FileStream readStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			using BinaryWriter binary = new BinaryWriter(fileStream);

			fileStream.SetLength(fileStream.Length+ _mask.Length);
			ReverseAndOverwriteFileRange(readStream, fileStream, 0, fileStream.Length, Math.Min(fileStream.Length, readStream.Length));

			//使用最后的若干字节记录面具文件长度
			fileStream.Position = fileStream.Length;
			binary.Write((int)_mask.Length);
		}
		File.Move(filePath, filePath + _maskExtension);
	}
	/// <summary>
	/// 还原文件
	/// </summary>
	/// <param name="filePath"></param>
	public static void Reveal(string filePath)
	{
		if (!Path.GetExtension(filePath).StartsWith('.'))
		{
			throw new ArgumentOutOfRangeException(nameof(filePath), "文件不具有有效后缀");
		}
		{
			using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			using FileStream readStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
			using BinaryReader binary = new BinaryReader(fileStream);

			fileStream.Position = fileStream.Length - sizeof(int);
			int maskHeadLength = binary.ReadInt32();

			ReverseAndOverwriteFileRange(readStream
				, fileStream
				, Math.Max(fileStream.Length - maskHeadLength - sizeof(int), maskHeadLength)
				, Math.Min(fileStream.Length - maskHeadLength - sizeof(int), maskHeadLength)
				, Math.Min(fileStream.Length - maskHeadLength - sizeof(int), maskHeadLength));

			fileStream.SetLength(fileStream.Length - maskHeadLength - sizeof(int));//把文件末尾多余的部分截掉 
		}
		File.Move(filePath, new Uri(new Uri(filePath), Path.GetFileNameWithoutExtension(filePath)).LocalPath);
	}

	/// <summary>
	/// 读取流的部分数据，写入到流指定位置 
	/// </summary>
	/// <param name="readStream">读取流</param>
	/// <param name="writeStream">写入流</param>
	/// <param name="readIndex">读取开始位置</param>
	/// <param name="writeIndex">写入开始位置</param>
	/// <param name="length">长度</param>
	/// <remarks>文件流自带缓存。但交替的读写无法利用缓存。因此应该创建两个流同时操作同一文件</remarks>
	public static void ReverseAndOverwriteFileRange(FileStream readStream, FileStream writeStream, long readIndex, long writeIndex, long length)
	{
		using IMemoryOwner<byte>? owner = MemoryPool<byte>.Shared.Rent();//从内存池借用一段内存做缓存
		Span<byte> span = owner.Memory.Span;//获取内存

		try
		{
			readStream.Position = readIndex;//指定读取开始位置
			writeStream.Position = writeIndex;//指定写入开始位置
		}
		catch (Exception)
		{
			MessageBox.Show(readIndex + ":" + writeIndex);

			throw;
		}


		int bytesRead, totalBytesRead = 0;

		while (totalBytesRead < length)//读取量小于应读取长度
		{
			bytesRead = readStream.Read(span);//从流读取片段
			if (bytesRead < span.Length)//如果已经达到文件末尾
			{
				span = span[..bytesRead];//裁切
			}
			totalBytesRead += bytesRead;//更新已经读取量
			if (totalBytesRead > length)//如果已经读取量超过需要读取量
			{
				bytesRead -= (int)(totalBytesRead - length);
				span = span[..^(totalBytesRead - (int)length)];//裁切
			}
			span.Reverse();//反转数据
			try
			{
				writeStream.Position -= bytesRead;//预留写入空间
				writeStream.Write(span);//写入数据
				writeStream.Position -= bytesRead;//回到写入前的位置准备下一次写入
			}
			catch (Exception)
			{
				MessageBox.Show(writeStream.Position + ":" + bytesRead);
				throw;
			}

		}
	}
	/// <summary>
	/// 遍历目录树执行操作
	/// </summary>
	/// <param name="path">路径列表</param>
	/// <param name="action">操作</param>
	/// <returns>处理失败文件列表</returns>
	public static (int successCount, AggregateException exception) DirectoryTree(IEnumerable<string> path, Action<string> action)
	{
		int successCount = 0;
		List<Exception> list = new List<Exception>();
		foreach (var item in path)
		{
			if (File.Exists(item))
			{
				try
				{
					action(item);
					successCount++;
				}
				catch (Exception e)
				{
					list.Add(e);
				}
			}
			else if (Directory.Exists(item))
			{
				foreach (var item2 in Directory.EnumerateFiles(item, "*", SearchOption.AllDirectories))
				{
					try
					{
						action(item2);
						successCount++;
					}
					catch (Exception e)
					{
						list.Add(e);
					}
				}
			}
		}
		return (successCount, new AggregateException(list));
	}
	#region 释放资源
	private bool _disposedValue;
	protected virtual void Dispose(bool disposing)
	{
		if (!_disposedValue)
		{
			if (disposing)
			{
				_mask?.Dispose();
			}
			_disposedValue = true;
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
	#endregion
}
