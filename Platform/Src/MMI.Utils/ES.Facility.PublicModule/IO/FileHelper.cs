using System;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ES.Facility.PublicModule.IO
{
    /// <summary>
    /// 文件操作相关的
    /// </summary>
    public sealed class FileHelper
    {
        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::    该类为密封类不能被继承
        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        #region ::::::::::::::::::::::::::::::::  variable   ::::::::::::::::::::::::::::::::

        #endregion

        #region ::::::::::::::::::::::::::::::::  attribute  ::::::::::::::::::::::::::::::::

        #endregion

        #region ::::::::::::::::::::::::::::::::  structure  ::::::::::::::::::::::::::::::::
        FileHelper() { }

        #endregion

        #region ::::::::::::::::::::::::::::::::    event    ::::::::::::::::::::::::::::::::

        #endregion

        #region ::::::::::::::::::::::::::::::::   method    ::::::::::::::::::::::::::::::::
        /// <summary>
        /// 检查指定的文件是否存在,如果目录不存在会创建目录
        /// </summary>
        /// <param name="file_path">完整文件名</param>
        /// <param name="extend_name">带点的扩展名</param>
        /// <param name="cretype">是否允许创建不存在的目录</param>
        /// <returns>存在返回true</returns>
        public static bool GetCurFile(string file_path, string extend_name, bool cretype)
        {
            var outBool = false;

            if (Path.GetExtension(file_path) != extend_name)
            {
                file_path += extend_name;
            }

            var directory_path = Path.GetDirectoryName(file_path);

            if (!Directory.Exists(directory_path))
            {
                if (cretype) Directory.CreateDirectory(directory_path);      //如果允许创建目录
            }

            if (File.Exists(file_path)) outBool = true;

            return outBool;
        }
        /// <summary>
        /// 检查指定的文件是否存在,如果不存在会建立
        /// </summary>
        /// <param name="file_path"></param>
        /// <returns></returns>
        public static bool GetCurFileA(string file_path)
        {
            try
            {
                var dir_path = Path.GetDirectoryName(file_path);
                var ex_path = Path.GetExtension(file_path);
                return GetCurFile(file_path, ex_path, false);
            }
            catch { return false; }

        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static bool makePath(string _path)
        {

            var ex_path = Path.GetDirectoryName(_path);
            Directory.CreateDirectory(ex_path);
            return Directory.Exists(ex_path);
        }
        /// <summary>
        /// 检查输入的文件及路径是否存在
        /// </summary>
        /// <param name="file">文件目录</param>
        /// <param name="isFile">必须检查文件 如果文件不存在也返回false</param>
        /// <returns></returns>
        public static bool GetCurFile(string file, bool isFile)
        {
            var dir_path = Path.GetDirectoryName(file);
            var ex_path = Path.GetExtension(file);


            if (!Directory.Exists(dir_path)) return false;

            if (isFile)
            {
                if (ex_path.Equals(string.Empty))
                    return false;
                else
                    if (!File.Exists(file)) return false;
            }
            else
            {
                if (!ex_path.Equals(string.Empty))
                    if (!File.Exists(file)) return false;
            }

            return true;
        }
        public static bool GetCurFile(string file)
        {
            return GetCurFile(file, false);
        }

        #region GenerateFile 将字符串写成文件 －－ GenerateFile
        /// <summary>
        /// 将字符串写成文件 无提示
        /// </summary>
        /// <param name="file_path">文件完整目录 如果与最后的扩展名不同 那么扩展名将会被添加在最后</param>
        /// <param name="text">添加的内容</param>
        /// <param name="extend_name">扩展名 比较时要加 '。'</param>
        public static void GenerateFile(string file_path, string text, string extend_name)
        {
            if (Path.GetExtension(file_path) != extend_name)
            {
                file_path += extend_name;
            }

            var directory_path = Path.GetDirectoryName(file_path);
            if (!Directory.Exists(directory_path))
            {
                Directory.CreateDirectory(directory_path);
            }

            var fs = new FileStream(file_path, FileMode.Create, FileAccess.Write);

            var sw = new StreamWriter(fs, Encoding.Unicode);

            sw.Write(text);

            sw.Flush();

            sw.Close();
            fs.Close();
        }



        public static bool GenerateFileEx(string file_path, string text, string extend_name)
        {
            if (Path.GetExtension(file_path) != extend_name)
            {
                file_path += extend_name;
            }

            if (File.Exists(file_path))
            {
                if (DialogResult.No == MessageBox.Show(string.Format("文件{0}已经存在 ！你想覆盖吗？", file_path), "提示", MessageBoxButtons.YesNo))
                {
                    return false;
                }
            }

            var directory_path = Path.GetDirectoryName(file_path);
            if (!Directory.Exists(directory_path))
            {
                if (DialogResult.No == MessageBox.Show(string.Format("目的文件夹不存在 ！你想创建吗？", directory_path), "提示", MessageBoxButtons.YesNo))
                {
                    return false;
                }

                Directory.CreateDirectory(directory_path);
            }

            try
            {
                var fs = new FileStream(file_path, FileMode.Create, FileAccess.Write);

                var sw = new StreamWriter(fs);

                sw.Write(text);
                sw.Flush();

                sw.Close();
                fs.Close();

                return true;
            }
            catch (Exception ee)
            {
                //return false ;
                throw ee;
            }
            //MessageBox.Show("成功生成代码文件" + file_path + " ！") ;
        }
        #endregion

        #region  向指定文件追加文本
        public static bool AddTextToCurFile(string fName, string text)
        {
            var outBool = false;
            try
            {
                var fs = new FileStream(fName, FileMode.OpenOrCreate, FileAccess.Write);
                var m_streamWriter = new StreamWriter(fs, Encoding.Default);
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                m_streamWriter.WriteLine(text);
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
                outBool = true;
            }
            catch(Exception exMsg)
            {
                outBool = false;
                Trace.WriteLineIf(!outBool, exMsg);
            }
            return outBool;
        }
        #endregion

        #region GetFileContent 读取文本文件
        public static string GetFileContent(string file_path)
        {
            if (!File.Exists(file_path))
            {
                return null;
            }

            var reader = new StreamReader(file_path, Encoding.UTF7);
            var content = reader.ReadToEnd();
            reader.Close();

            return content;
        }
        #endregion

        #region WriteBuffToFile 将缓冲区的数据保存到文件中
        public static bool WriteBuffToFile(byte[] buff, int offset, int lenToWrite, string filePath, string extendName, bool queryOverwrite)
        {
            if (Path.GetExtension(filePath) != extendName)
            {
                filePath += extendName;
            }

            if (queryOverwrite)
            {
                if (File.Exists(filePath))
                {
                    if (DialogResult.No == MessageBox.Show(string.Format("文件{0}已经存在 ！你想覆盖吗？", filePath), "提示：", MessageBoxButtons.YesNo))
                    {
                        return false;
                    }
                }
            }

            var directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                if (queryOverwrite)
                {
                    if (DialogResult.No == MessageBox.Show(string.Format("目的文件夹不存在 ！你想创建吗？", directoryPath), "提示", MessageBoxButtons.YesNo))
                    {
                        return false;
                    }
                }

                Directory.CreateDirectory(directoryPath);
            }

            try
            {
                var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);

                var bw = new BinaryWriter(fs);

                bw.Write(buff, offset, lenToWrite);
                bw.Flush();

                bw.Close();
                fs.Close();

                //MessageBox.Show("成功生成文件" + file_path + " ！") ;
                return true;
            }
            catch (Exception exRet)
            {
                //Todo: 为unity屏蔽
                //System.Diagnostics.Debug.WriteLine(exRet);
                return false;
            }
        }

        public static bool WriteBuffToFile(byte[] buff, string file_path, string extend_name, bool query_overwrite)
        {
            return WriteBuffToFile(buff, 0, buff.Length, file_path, extend_name, query_overwrite);
        }


        #endregion

        #region ReadFileToBuff 将文件数据读到缓冲区中
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file_path"></param>
        /// <param name="buff">输出数据</param>
        /// <returns></returns>
        public static bool ReadFileToBuff(string filePath, out byte[] buff)
        {
            if (!File.Exists(filePath))
            {
                //MessageBox.Show(string.Format("失败，文件{0}不存在 ！" ,file_path)) ;
                buff = null;
                return false;
            }

            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var br = new BinaryReader(fs);

            buff = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            //MessageBox.Show("成功打开文件" + file_path + " ！") ;
            return true;
        }
        #endregion

        #region GetFileToOpen 得到需要打开（处理）的文件的路径
        public static string GetFileToOpen(string title)
        {
            var openDlg = new OpenFileDialog();
            openDlg.Filter = "All Files (*.*)|*.*";
            openDlg.FileName = "";
            if (title != null)
            {
                openDlg.Title = title;
            }

            openDlg.CheckFileExists = true;
            openDlg.CheckPathExists = true;

            var res = openDlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                return openDlg.FileName;
            }

            return null;
        }

        public static string GetFileToOpen(string title, string extendName)
        {
            var openDlg = new OpenFileDialog();
            openDlg.Filter = string.Format(title + "(*{0})|*{0}", extendName);
            openDlg.FileName = "";
            if (title != null)
            {
                openDlg.Title = title;
            }

            openDlg.CheckFileExists = true;
            openDlg.CheckPathExists = true;

            var res = openDlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                return openDlg.FileName;
            }

            return null;
        }
        #endregion

        #region GetFolderToOpen 打开的文件夹路径
        public static string GetFolderToOpen(bool newFolderButton)
        {
            var folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = newFolderButton;
            var res = folderDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                return folderDialog.SelectedPath;
            }

            return null;
        }
        #endregion

        #region GetPathToSave 得到保存文件的路径
        public static string GetPathToSave(string default_name, string title)
        {
            var saveDlg = new SaveFileDialog();
            saveDlg.Filter = "All Files (*.*)|*.*";
            saveDlg.FileName = default_name;
            saveDlg.OverwritePrompt = false;
            if (title != null)
            {
                saveDlg.Title = title;
            }

            var res = saveDlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                return saveDlg.FileName;
            }

            return null;
        }
        #endregion

        #region GetFileNameNoPath
        public static string GetFileNameNoPath(string fileName_path)
        {
            return Path.GetFileName(fileName_path);
        }
        #endregion

        #region GetFileSize
        public static int GetFileSize(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Open);
            var size = (int)fs.Length;
            fs.Close();

            return size;
        }
        public static int GetFileSizeA(string filePath)
        {
            var fi = new FileInfo(filePath);
            return Convert.ToInt32(fi.Length);
        }

        #endregion

        #region ReadFileData 读取指定大小的内容
        public static void ReadFileData(FileStream fs, byte[] buff, int count, int offset)
        {
            var readCount = 0;
            while (readCount < count)
            {
                var read = fs.Read(buff, offset + readCount, count - readCount);
                readCount += read;
            }

            return;
        }
        #endregion

        #region GetFileDir
        public static string GetFileDir(string fileName_path)
        {
            return Path.GetDirectoryName(fileName_path);
        }
        #endregion

        #region DeleteFile 删除文件
        public static void DeleteFile(string filePath, string extendName)
        {
            if (Path.GetExtension(filePath) != extendName)
            {
                filePath += extendName;
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        #endregion

        #region EnsureExtendName 确保扩展名正确
        public static string EnsureExtendName(string originPath, string extendName)
        {
            if (Path.GetExtension(originPath) != extendName)
            {
                originPath += extendName;
            }

            return originPath;
        }
        #endregion
        #endregion

    }
}
