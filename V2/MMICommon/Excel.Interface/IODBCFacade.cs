using System.Data;

namespace Excel.Interface
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
