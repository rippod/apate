using System;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace apate
{
    internal static class Program
    {
        public static byte[] fileHead = new byte[] { };
        public static int maximumMaskLength = 2147483647/7;//2GB/7=约300MB
        public static int maskLengthIndicatorLength = 4;//存储面具长度的标记，长度为4个字节，可表示4GB的文件长度
        public static byte[] jpgHead = new byte[] { 0xff, 0xd8, 0xff, 0xe1 };
        public static byte[] movHead = new byte[] { 0x6d, 0x6f, 0x6f, 0x76 };
        public static byte[] mp4Head = new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x69,
            0x73, 0x6F, 0x6D, 0x00, 0x00, 0x02, 0x00, 0x69, 0x73, 0x6F, 0x6D, 0x69, 0x73, 0x6F, 0x32, 0x61, 
            0x76, 0x63, 0x31, 0x6D, 0x70, 0x34, 0x31 };
        public static byte[] exeHead = new byte[] { 0x4D, 0x5A, 0x90, 0x00, 0x03, 0x00, 0x00, 0x00, 0x04,
            0x00, 0x00, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x0E, 0x1F, 0xBA, 0x0E, 0x00, 0xB4, 0x09, 0xCD, 0x21,
            0xB8, 0x01, 0x4C, 0xCD, 0x21, 0x54, 0x68, 0x69, 0x73, 0x20, 0x70, 0x72, 0x6F, 0x67, 0x72, 0x61,
            0x6D, 0x20, 0x63, 0x61, 0x6E, 0x6E, 0x6F, 0x74, 0x20, 0x62, 0x65, 0x20, 0x72, 0x75, 0x6E, 0x20,
            0x69, 0x6E, 0x20, 0x44, 0x4F, 0x53, 0x20, 0x6D, 0x6F, 0x64, 0x65, 0x2E, 0x0D, 0x0D, 0x0A, 0x24,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            System.Windows.Forms.Application.Run(new ApateUI());
        }

        /// <summary>
        /// 伪装文件
        /// </summary>
        /// <param name="filePath">真身文件路径</param>
        /// <param name="maskHead">面具（字节形式）</param>
        /// <returns>成功返回1，失败返回-1</returns>
        public static int Disguise(string filePath,byte[] maskHead)
        {

            FileStream myStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            BinaryWriter myWriter = new BinaryWriter(myStream);
            BinaryReader myReader = new BinaryReader(myStream);
            try
            {
                byte[] originalHead;
                if (new FileInfo(filePath).Length >= maskHead.Length)//正常情况下：真实文件的长度大于面具长度，以面具的长度读取真实文件的头部信息
                {
                    originalHead = myReader.ReadBytes(maskHead.Length);
                }
                else//非正常情况：真实文件长度还没有面具文件长度大
                {
                    originalHead = myReader.ReadBytes(Convert.ToInt32(new FileInfo(filePath).Length));
                }
                myWriter.Seek(0, SeekOrigin.Begin);
                myWriter.Write(maskHead);
                myWriter.Seek(0, SeekOrigin.End);
                myWriter.Write(ReverseByteArray(originalHead));
                //使用最后的若干字节记录面具文件长度
                myWriter.Write(IntToBytes(maskHead.Length));
                myWriter.Close();
                myReader.Close();
                myStream.Close();
                return 1;
            }catch (Exception) {
                myWriter.Close();
                myReader.Close();
                myStream.Close();
                return -1;
            }
        }
        //还原
        /// <summary>
        /// 还原文件
        /// </summary>
        /// <param name="filePath">经过伪装的文件路径</param>
        /// <returns>成功返回1，失败返回-1</returns>
        public static int Reveal(string filePath)
        {
            FileInfo disguisedFileInfo = new FileInfo(filePath);
            FileStream myStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            BinaryWriter myWriter = new BinaryWriter(myStream);
            BinaryReader myReader = new BinaryReader(myStream);
            try
            {
                myReader.BaseStream.Position = disguisedFileInfo.Length - maskLengthIndicatorLength;
                int maskHeadLength = BytesToInt(myReader.ReadBytes(maskLengthIndicatorLength));
                byte[] originalHead;
                //正常情况下，面具长度小于真实文件长度
                if (maskHeadLength <= (disguisedFileInfo.Length - maskLengthIndicatorLength - maskHeadLength))
                {
                    myReader.BaseStream.Position = disguisedFileInfo.Length - maskLengthIndicatorLength - maskHeadLength;
                    originalHead = myReader.ReadBytes(maskHeadLength);
                }
                else//非正常情况下，面具长度大于真实文件长度
                {
                    myReader.BaseStream.Position = maskHeadLength;
                    originalHead = myReader.ReadBytes(Convert.ToInt32(disguisedFileInfo.Length - maskLengthIndicatorLength - maskHeadLength));
                }
                myWriter.BaseStream.SetLength(myWriter.BaseStream.Length - maskHeadLength - maskLengthIndicatorLength);//把文件末尾多余的部分截掉
                myWriter.Seek(0, SeekOrigin.Begin);
                myWriter.Write(ReverseByteArray(originalHead));
                myWriter.Close();
                myReader.Close();
                myStream.Close();
                return 1;
            }
            catch (Exception)
            {
                myWriter.Close();
                myReader.Close();
                myStream.Close();
                return -1;
            }
            
        }
        /// <summary>
        /// 将字节数组逆序排列
        /// </summary>
        /// <param name="buffer">目标字节数组</param>
        /// <returns>逆序字节数组</returns>
        private static byte[] ReverseByteArray(byte[] buffer)
        {
            byte[] result = new byte[buffer.Length];
            for (int i = 0;i< buffer.Length; i++)
            {
                result[i]= buffer[i];
            }
            Array.Reverse(result);
            return result;

        }
        /// <summary>
        /// 递归遍历目标路径，得到所有的文件
        /// </summary>
        /// <param name="path">目标路径</param>
        /// <returns>目标路径下所有的文件</returns>
        public static List<string> GetAllFilesRecursively(string path)
        {
            List<string> files = new List<string>();
            if(Directory.Exists(path))
            {
                List<string> subDirFiles = new List<string>(Directory.GetFiles(path));
                for(int i = 0;i<subDirFiles.Count; i++)
                {
                    files.Add(subDirFiles[i]);
                }
                List<string> subDirDirectories = new List<string>(Directory.GetDirectories(path));
                for(int i=0;i<subDirDirectories.Count; i++)
                {
                    List<string> tmp = GetAllFilesRecursively(subDirDirectories[i]);
                    files.AddRange(tmp);
                }
                
            }
            else if(File.Exists(path))
            {
                files.Add(path);
            }
            return files;
        }
        /// <summary>
        /// 将int转换为字节数组
        /// </summary>
        /// <param name="intLength"></param>
        /// <returns></returns>
        private static byte[] IntToBytes(int intLength)
        {
            byte[] result = BitConverter.GetBytes(intLength);
            return result;
        }
        /// <summary>
        /// 将字节数组转换为int
        /// </summary>
        /// <param name="byteLength"></param>
        /// <returns></returns>
        private static int BytesToInt(byte[] byteLength)
        {
            int result = BitConverter.ToInt32(byteLength, 0);
            return result;
        }
        /// <summary>
        /// 将文件转换为字节数组，大小受限于maximumMaskLength的限制，如果超出大小限制，则返回的数组长度为0
        /// </summary>
        /// <param name="filePath">目标文件路径</param>
        /// <returns>目标文件转换的字节数组，如果超出大小限制，则返回的数组长度为0</returns>
        public static byte[] FileToBytes(string filePath)
        {
            byte[] bytes = new byte[] { };
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Length > 0 && fileInfo.Length < maximumMaskLength)
            {
                bytes = File.ReadAllBytes(filePath);
            }
            return bytes;
        }
    }
}
