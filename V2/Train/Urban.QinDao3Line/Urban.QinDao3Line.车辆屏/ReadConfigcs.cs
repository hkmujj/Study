using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Urban.QingDao3Line.MMI
{
    public class ReadConfigcs
    {
        private readonly string m_FileName;
        private readonly string m_FilePath;
        private readonly Dictionary<int, string> m_Dictionary;
        private Dictionary<int, string[]> m_Sdictionary;

        /// </summary>
        /// <param name="filename">文件名字</param>
        /// <param name="dictionary">字典类型</param>
        /// <param name="rectangles">列表类型</param>
        /// <param name="type">判断是字典还是列表</param>
        public ReadConfigcs(string filename, Dictionary<int, string> dictionary)
        {
            m_FileName = filename;
            m_Dictionary = dictionary;

        }

        public ReadConfigcs(string filepath, Dictionary<int, string[]> sdictionary)
        {
            m_FilePath = filepath;
            m_Sdictionary = sdictionary;
        }

        public void ReaFile(string directory)
        {
            string strFileName = Path.Combine(directory, m_FileName + ".txt");
            string strFilepath = Path.Combine(directory, m_FilePath + ".txt");
            LoadConfig(strFileName);
          

        }

        public void LoadConfig(string file)
        {
            string[] str = new string[2];
            try
            {

                if (File.Exists(file))
                {
                    var allLines = File.ReadAllLines(file, Encoding.GetEncoding("GB2312"));

                    foreach (var line in allLines)
                    {
                        if (string.IsNullOrEmpty(line))

                            return;

                        str = line.Split(';');

                        if (str.Length < 2)
                        {
                            continue;
                        }

                        if (!m_Dictionary.ContainsKey(int.Parse(str[0])))
                        {
                            m_Dictionary.Add(int.Parse(str[0]), str[1]);
                        }

                    }
                }
            }
            catch (FormatException e)
            {
                MessageBox.Show(str.Length < 2
                    ? string.Format("length = {0}, {1},{2}", str.Length, str[0], e.ToString())
                    : string.Format("{0}   {1}   {2}", str[0], str[1], e.ToString()));
            }

        }

    


}
}
        

    
    


           
       
     
