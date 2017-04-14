using System;
using System.Data;

namespace MMITool.Common.Util.Excel
{
    /// <summary>
    /// ODBC的操作
    /// </summary>
    public class ODBCFacade : IODBCFacade
    {
        /// <summary>
        /// Sql
        /// </summary>
        public string Sql { get; set; }
        /// <summary>
        /// ConnectSting
        /// </summary>
        public string ConnectSting { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void ExcuteSql()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ExcuteSqlReturn
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public DataSet ExcuteSqlReturn()
        {
            throw new NotImplementedException();
        }
    }
}
