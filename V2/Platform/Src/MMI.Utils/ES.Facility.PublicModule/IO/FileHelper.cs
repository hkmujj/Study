using System;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ES.Facility.PublicModule.IO
{
    /// <summary>
    /// �ļ�������ص�
    /// </summary>
    public sealed class FileHelper
    {
        //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::    ����Ϊ�ܷ��಻�ܱ��̳�
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
        /// ���ָ�����ļ��Ƿ����,���Ŀ¼�����ڻᴴ��Ŀ¼
        /// </summary>
        /// <param name="file_path">�����ļ���</param>
        /// <param name="extend_name">�������չ��</param>
        /// <param name="cretype">�Ƿ������������ڵ�Ŀ¼</param>
        /// <returns>���ڷ���true</returns>
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
                if (cretype) Directory.CreateDirectory(directory_path);      //���������Ŀ¼
            }

            if (File.Exists(file_path)) outBool = true;

            return outBool;
        }
        /// <summary>
        /// ���ָ�����ļ��Ƿ����,��������ڻὨ��
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
        /// ����Ŀ¼
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
        /// ���������ļ���·���Ƿ����
        /// </summary>
        /// <param name="file">�ļ�Ŀ¼</param>
        /// <param name="isFile">�������ļ� ����ļ�������Ҳ����false</param>
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

        #region GenerateFile ���ַ���д���ļ� ���� GenerateFile
        /// <summary>
        /// ���ַ���д���ļ� ����ʾ
        /// </summary>
        /// <param name="file_path">�ļ�����Ŀ¼ �����������չ����ͬ ��ô��չ�����ᱻ��������</param>
        /// <param name="text">��ӵ�����</param>
        /// <param name="extend_name">��չ�� �Ƚ�ʱҪ�� '��'</param>
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
                if (DialogResult.No == MessageBox.Show(string.Format("�ļ�{0}�Ѿ����� �����븲����", file_path), "��ʾ", MessageBoxButtons.YesNo))
                {
                    return false;
                }
            }

            var directory_path = Path.GetDirectoryName(file_path);
            if (!Directory.Exists(directory_path))
            {
                if (DialogResult.No == MessageBox.Show(string.Format("Ŀ���ļ��в����� �����봴����", directory_path), "��ʾ", MessageBoxButtons.YesNo))
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
            //MessageBox.Show("�ɹ����ɴ����ļ�" + file_path + " ��") ;
        }
        #endregion

        #region  ��ָ���ļ�׷���ı�
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

        #region GetFileContent ��ȡ�ı��ļ�
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

        #region WriteBuffToFile �������������ݱ��浽�ļ���
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
                    if (DialogResult.No == MessageBox.Show(string.Format("�ļ�{0}�Ѿ����� �����븲����", filePath), "��ʾ��", MessageBoxButtons.YesNo))
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
                    if (DialogResult.No == MessageBox.Show(string.Format("Ŀ���ļ��в����� �����봴����", directoryPath), "��ʾ", MessageBoxButtons.YesNo))
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

                //MessageBox.Show("�ɹ������ļ�" + file_path + " ��") ;
                return true;
            }
            catch (Exception exRet)
            {
                //Todo: Ϊunity����
                //System.Diagnostics.Debug.WriteLine(exRet);
                return false;
            }
        }

        public static bool WriteBuffToFile(byte[] buff, string file_path, string extend_name, bool query_overwrite)
        {
            return WriteBuffToFile(buff, 0, buff.Length, file_path, extend_name, query_overwrite);
        }


        #endregion

        #region ReadFileToBuff ���ļ����ݶ�����������
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file_path"></param>
        /// <param name="buff">�������</param>
        /// <returns></returns>
        public static bool ReadFileToBuff(string filePath, out byte[] buff)
        {
            if (!File.Exists(filePath))
            {
                //MessageBox.Show(string.Format("ʧ�ܣ��ļ�{0}������ ��" ,file_path)) ;
                buff = null;
                return false;
            }

            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var br = new BinaryReader(fs);

            buff = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            //MessageBox.Show("�ɹ����ļ�" + file_path + " ��") ;
            return true;
        }
        #endregion

        #region GetFileToOpen �õ���Ҫ�򿪣��������ļ���·��
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

        #region GetFolderToOpen �򿪵��ļ���·��
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

        #region GetPathToSave �õ������ļ���·��
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

        #region ReadFileData ��ȡָ����С������
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

        #region DeleteFile ɾ���ļ�
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

        #region EnsureExtendName ȷ����չ����ȷ
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
