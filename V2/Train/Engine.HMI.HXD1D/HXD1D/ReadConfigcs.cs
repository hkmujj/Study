using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MMI.Facility.DataType;


namespace HXD1D
{
    public class ReadConfigcs : baseClass
    {

        public void ReaFile(string filename, Dictionary<int, string> dictionary)
        {
            try
            {
                string strFileRootPath;

                strFileRootPath = AppDomain.CurrentDomain.BaseDirectory + "\\1D\\config\\";

                string strFileName = strFileRootPath + filename + ".txt";
                if (File.Exists(strFileName))
                {
                    var allLines = File.ReadAllLines(strFileName, Encoding.GetEncoding("GB2312"));
                    foreach (var line in allLines)
                    {
                        string[] str = line.Split(';');
                        if (str.Count() == 2)
                        {
                            if (!dictionary.ContainsKey(int.Parse(str[0])))
                            {
                                dictionary.Add(Int32.Parse(str[0]), str[1]);
                            }


                        }

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







