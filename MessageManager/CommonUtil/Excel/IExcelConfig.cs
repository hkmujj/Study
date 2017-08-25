using System.Collections.Generic;

namespace CommonUtil.Excel
{
    public interface IExcelConfig
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// Sheet集合
        /// </summary>
        IList<IExcelShettConfig> Sheets { get; set; }
    }

    public class ExcelConfig : IExcelConfig
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName">文件名称</param>
        public ExcelConfig(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; protected set; }

        /// <summary>
        /// Sheet集合
        /// </summary>
        public IList<IExcelShettConfig> Sheets { get; set; }
    }
}
