using System;
using System.Data;
using System.Data.Odbc;
using System.Linq;

namespace MMITool.Common.Util.Excel
{
    /// <summary>
    /// 
    /// </summary>
    public class ExcelAdapter : IExcelAdapter
    {
        private const string ConnectString = "Driver={{Microsoft Excel Driver (*.xls)}};DriverID=790;Dbq={0}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public DataSet Adapter(ExcelReaderConfig config)
        {
            var ds = new DataSet(config.File);
            try
            {
                using (var con = new OdbcConnection(string.Format(ConnectString, config.File)))
                {
                    foreach (var sheetName in config.SheetNames)
                    {
                        var primaries = config.Coloumns.Where(w => w.IsPrimaryKey).Select(s => s.ColoumnName).ToList();

                        var condition = string.Join(" and ", primaries.Select(s => string.Format("{0} is not null", s)));

                        var queue = string.Format("select {0} from [{1}$]", string.Join(",", config.Coloumns.Select(s => s.ColoumnName).ToArray()), sheetName);

                        if (!string.IsNullOrEmpty(condition))
                        {
                            queue += string.Format("where {0}", condition);
                        }

                        var dt = new DataTable(sheetName);
                        using (var adpt = new OdbcDataAdapter(queue, con))
                        {
                            adpt.Fill(dt);
                            dt.PrimaryKey = primaries.Select(s => dt.Columns[s]).ToArray();
                            ds.Tables.Add(dt);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogMgr.Error(string.Format("Read excel file 【{0}】 error", config.File));
                LogMgr.Error(e.ToString());
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
        public DataSet Adapter(ExcelReaderConfig config, IODBCFacade odbc)
        {
            throw new NotImplementedException();
        }
    }
}
