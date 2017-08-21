using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using CommonUtil.Util;

namespace Excel.Interface
{
    /// <summary>
    /// CSV 适配
    /// </summary>
    public class CSVAdapter : IExcelAdapter
    {
        //private const string ConnnectString = "Driver={{Microsoft Text Driver (*.txt; *.csv)}};Dbq={0};Extensions=asc,csv,tab,txt;Persist Security Info=False;;";
        private const string ConnnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Text;FMT=Delimited;HDR=YES;IMEX=1'";

        /// <summary>
        /// 根据config 得到xls的数据表
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public DataSet Adapter(IExcelReaderConfig config)
        {
            var ds = new DataSet(config.File);
            try
            {
                using (var con = new OleDbConnection(String.Format(ConnnectString, Path.GetDirectoryName(config.File))))
                {
                    var fileName = Path.GetFileName(config.File);
                    var primary = config.Coloumns.Find(f => f.IsPrimaryKey);
                    string queue = String.Format("select {0} from {1} ",
                        String.Join(",", config.Coloumns.Select(s => s.Name).ToArray()),
                        fileName
                        );

                    if (primary != null)
                    {
                        queue = String.Format("select {0} from {1}  where {2} is not null",
                            String.Join(",", config.Coloumns.Select(s => s.Name).ToArray()),
                            fileName,
                            primary.Name);
                    }
                    //var queue = String.Format("select {0} from [{1}]  where {2} is not null", String.Join(",", config.Coloumns.Select(s => s.Name).ToArray()), sheetName, primary);
                    var dt = new DataTable(fileName);
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
            catch (Exception e)
            {
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
        public DataSet Adapter(IExcelReaderConfig config, IODBCFacade odbc)
        {
            throw new NotImplementedException();
        }
    }
}