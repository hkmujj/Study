using System.Collections.Generic;

namespace CommonUtil.Excel
{
    /// <summary>
    /// ExcelSheet 配置
    /// </summary>
    public interface IExcelShettConfig
    {
        /// <summary>
        /// Sheet名称
        /// </summary>
        string SheetName { get; }
        /// <summary>
        /// 列集合
        /// </summary>
        IList<string> ColumnNames { get; }
    }

    public class ExcelShettConfig : IExcelShettConfig
    {
        /// <summary>
        /// Sheet名称
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// 列集合
        /// </summary>
        public IList<string> ColumnNames { get; set; }
    }
}
