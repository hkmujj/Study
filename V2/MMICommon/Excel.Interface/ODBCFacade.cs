using System;
using System.Data;

namespace Excel.Interface
{
    /// <summary>
    /// ODBC的操作
    /// </summary>
    public class ODBCFacade : IODBCFacade
    {
        /// <summary>
        /// 
        /// </summary>
        public string Sql { get; set; }
        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public DataSet ExcuteSqlReturn()
        {
            throw new NotImplementedException();
        }
    }
}
