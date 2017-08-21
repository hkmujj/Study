using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using CommonUtil.Util;

namespace Excel.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public class ExcelAdapter : IExcelAdapter
    {
        //private const string ConnectString = "Driver={{Microsoft Excel Driver (*.xls)}};DriverID=790;Dbq={0};Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
        private const string ConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public DataSet Adapter(IExcelReaderConfig config)
        {
            var ds = new DataSet(config.File);
            try
            {
                using (var con = new OleDbConnection(string.Format(ConnectString, config.File)))
                {
                    foreach (var sheetName in config.SheetNames)
                    {
                        var primary = config.Coloumns.Find(f => f.IsPrimaryKey);
                        string queue;
                        if (primary != null)
                        {
                            queue = string.Format("select {0} from [{1}$]  where {2} is not null",
                                string.Join(",", config.Coloumns.Select(s => s.Name).ToArray()),
                                sheetName,
                                primary.Name);
                        }
                        else
                        {
                            queue =string.Format("select {0} from [{1}$]",
                                string.Join(",", config.Coloumns.Select(s => s.Name).ToArray()),
                                sheetName
                                );
                        }
                        var dt = new DataTable(sheetName);
                        using (var adpt = new OleDbDataAdapter(queue, con))
                        {
                            adpt.Fill(dt);
                            if (config.Coloumns.Any(a => a.IsPrimaryKey))
                            {
                                dt.PrimaryKey =
                                    config.Coloumns.FindAll(f => f.IsPrimaryKey).Select(s => dt.Columns[s.Name]).ToArray();
                            }
                            ds.Tables.Add(dt);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                AppLog.Error(e.ToString());
            }
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="odbc"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Obsolete("未实现")]
        public DataSet Adapter(IExcelReaderConfig config, IODBCFacade odbc)
        {
            throw new NotImplementedException();
        }
    }
}
