using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Urban.QingDao3Line.MMI
{
    class Read
    {
        private readonly string m_Filepath;
        private readonly Dictionary<int, string[]> m_Dictionary = new Dictionary<int, string[]>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath">路径</param>
        /// <param name="dictionary"></param>
        public Read(string filepath,Dictionary<int,string[]> dictionary)
        {
            m_Filepath = filepath;
            m_Dictionary = dictionary;
        }
        public void LoadConfigs()
        {
           try
            {
                if (File.Exists(m_Filepath))
                {
                    int index = 0;
                    string[] strs;
                    StreamReader sr = new StreamReader(m_Filepath, Encoding.Default);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (index != 0)
                        {
                            strs = line.Split(';');
                            if (!m_Dictionary.ContainsKey(int.Parse(strs[0])) && strs.Length > 1)
                            {
                                m_Dictionary.Add(int.Parse(strs[0]), strs);
                            }

                        }
                        index++;

                    }

                }
            }
           catch (Exception ex)
            {
               Console.WriteLine(ex.ToString());

            }
        


        }
    }
}
