using System.Data;

namespace Excel.Interface
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
        DataSet Adapter(IExcelReaderConfig config);

        /// <summary>
        /// 根据config 得到xls的数据表
        /// </summary>
        /// <param name="config"></param>
        /// <param name="odbc"></param>
        /// <returns></returns>
        DataSet Adapter(IExcelReaderConfig config, IODBCFacade odbc);
    }
}
