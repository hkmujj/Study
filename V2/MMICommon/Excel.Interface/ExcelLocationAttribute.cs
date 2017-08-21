using System;

namespace Excel.Interface
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ExcelLocationAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="sheet"></param>
        public ExcelLocationAttribute(string file, string sheet)
        {
            File = file;
            Sheet = sheet;
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string File { private set; get; }

        /// <summary>
        /// 表名。
        /// </summary>
        public string Sheet { private set; get; }
    }
}