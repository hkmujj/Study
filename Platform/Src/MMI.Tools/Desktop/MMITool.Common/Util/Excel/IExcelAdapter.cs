using System.Data;

namespace MMITool.Common.Util.Excel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExcelAdapter
    {
        /// <summary>
        /// 根据config 得到xls的数据表
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        DataSet Adapter(ExcelReaderConfig config);

        /// <summary>
        /// 根据config 得到xls的数据表
        /// </summary>
        /// <param name="config"></param>
        /// <param name="odbc"></param>
        /// <returns></returns>
        DataSet Adapter(ExcelReaderConfig config, IODBCFacade odbc);
    }
}
