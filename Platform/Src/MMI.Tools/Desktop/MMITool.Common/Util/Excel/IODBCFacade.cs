using System.Data;

namespace MMITool.Common.Util.Excel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IODBCFacade
    {
        /// <summary>
        /// 
        /// </summary>
        string Sql { set; get; }

        /// <summary>
        /// 
        /// </summary>
        string ConnectSting { set; get; }

        /// <summary>
        /// 
        /// </summary>
        void ExcuteSql();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DataSet ExcuteSqlReturn();
    }
}
