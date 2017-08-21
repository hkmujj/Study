using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowsApplication1
{
    /// <summary>
    /// 
    /// </summary>
    public class EventRecord
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configPath"></param>
        /// <param name="type"></param>
        public EventRecord(string configPath, ConfigType type)
        {
            path = configPath;
        }

        /// <summary>
        /// 载入文本
        /// </summary>
        /// <returns>成功返回true</returns>
        public bool EventRecord_Load()
        {
            //文本不存在
            if (!File.Exists(path)) return false;

            ////打开文件
            //FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            //通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
            StreamReader m_streamReader = new StreamReader(path, System.Text.Encoding.GetEncoding("GB2312"));
            //使用StreamReader类来读取文件
            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

            //从数据流中读取每一行，直到文件最后一行，并存到_contentList中            
            String strLine = m_streamReader.ReadLine();
            while (strLine != null)
            {
                _contentList.Add(strLine);
                strLine = m_streamReader.ReadLine();
            }

            //关闭此StreamReader对象
            m_streamReader.Close();

            //_couldModifyList字典添加内容
            for (int i = 0; i < _contentList.Count; i++)
            {
                string[] splitValue = _contentList[i].Split('=');
                DictEdit(i, splitValue);
            }            

            return true;
        }

        /// <summary>
        /// _couldModifyList修改
        /// </summary>
        /// <param name="index"></param>
        /// <param name="strArr"></param>
        private void DictEdit(int index, string[] strArr)
        {
            List<string> strArrList = new List<string>();
            foreach (string str in strArr)
            {
                strArrList.Add(str);
            }

            _couldModifyList.Add(index, strArrList);
        }

        /// <summary>
        /// 修改文本
        /// </summary>
        /// <param name="titleValue"></param>
        /// <param name="ModValue"></param>
        public void ModifyConfigContent(string titleValue, string ModValue)
        {
            if (_couldModifyList.Count > 0)
            {
                foreach (List<string> strList in _couldModifyList.Values)
                {
                    if (strList.Count == 2 && strList[0] == titleValue)
                    {
                        strList[1] = ModValue;
                    }
                }
            }
        }

        /// <summary>
        /// 找到对应的config
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public string FindConfigContent(string indexName)
        {
            var content = _couldModifyList.Values.FirstOrDefault(f => f.Count == 2 && f[0] == indexName);
            if (content != null)
            {
                return content[1];
            }
            return string.Empty;
        }

        private string ModiFyListEdit(List<string> strList)
        {
            string modifyListEdit = string.Empty;

            if (strList.Count == 2)
                modifyListEdit = strList[0] + "=" + strList[1];
            else
            {
                foreach (string str in strList)
                {
                    modifyListEdit += str;
                }
            }

            return modifyListEdit + "\r\n";
        }

        /// <summary>
        /// 输出文本的内容
        /// </summary>
        /// <returns></returns>
        private string WriteLines()
        {
            string writeLines = string.Empty;

            foreach (List<string> strList in _couldModifyList.Values)
            {
                writeLines += ModiFyListEdit(strList);
            }

            return writeLines;
        }

        /// <summary>
        /// 保存文本
        /// </summary>
        /// <returns></returns>
        public bool SaveToTxtFile()
        {
            //FileStream fs = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write);

            //通过指定字符编码方式可以实现对汉字的支持，否则在用记事本打开查看会出现乱码
            /*改函数很重要由于要将修改的内容覆盖原来的文件内容，故设第二个参数为false。
             * 同时还不能用其他类如FileString来操作文件(多线程占用，不能覆盖修改文件)
             * 例如
             * new StreamWriter(fs, true, Encoding.GetEncoding("GB2312"));是错误的
             */
            StreamWriter m_streamWriter = new StreamWriter(path, false, Encoding.GetEncoding("GB2312"));

            m_streamWriter.Flush();

            //使用StreamWriter来往文件中写入内容
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);

            //把_couldModifyList中的内容写入文件
            m_streamWriter.Write(WriteLines());

            //关闭此文件
            m_streamWriter.Flush();

            m_streamWriter.Close();

            return true;
        }

        private string path = string.Empty;

        private List<string> _contentList = new List<string>();
        /// <summary>
        /// 所有内容列表
        /// </summary>
        public List<string> ContentList { get { return _contentList; } }

        private Dictionary<int, List<string>> _couldModifyList = new Dictionary<int, List<string>>();
        /// <summary>
        /// 可修改内容列表
        /// </summary>
        public Dictionary<int, List<string>> CouldModifyList { get { return _couldModifyList; } }
        
    }

    public enum ConfigType : int
    {
        MainConfig,
        SubConfig,
    }
}
