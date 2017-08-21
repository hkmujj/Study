using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace ATP200C.PublicComponents
{
    /// <summary>
    /// Excel操作类
    /// </summary>
    /// Microsoft Excel 12.0 Object Library
    public class ExcelHelper
    {
        /// <summary>
        /// 获取Excel文件数据表列表
        /// </summary>
        /// <param name="ExcelFileName">路径文件名</param>
        /// <returns>Excel所有表名字列表</returns>
        public static ArrayList GetExcelTables(string ExcelFileName)
        {
            var tablesList = new ArrayList();

            if (!File.Exists(ExcelFileName)) return tablesList;

            using (var conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 12.0 Xml;Data Source=" + ExcelFileName))
            {
                conn.Open();
                var dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                //获取数据表个数
                if (dt == null) return tablesList;
                var tablecount = dt.Rows.Count;
                for (var i = 0; i < tablecount; i++)
                {
                    var tablename = dt.Rows[i][2].ToString().Trim().TrimEnd('$');
                    if (tablesList.IndexOf(tablename) < 0)
                    {
                        tablesList.Add(tablename);
                    }
                }
            }
            return tablesList;
        }

        /// <summary>
        /// 将Excel文件导出至DataTable(第一行作为表头)
        /// </summary>
        /// <param name="ExcelFilePath">Excel文件路径</param>
        /// <param name="TableName">数据表名，如果数据表名错误，默认为第一个数据表名</param>
        /// <returns>名字为TableName的DataTable表</returns>
        public static DataTable InputFromExcel(string ExcelFilePath, string TableName)
        {
            if (!File.Exists(ExcelFilePath))
            {
                throw new Exception("Excel文件不存在！");
            }

            //如果数据表名不存在，则数据表名为Excel文件的第一个数据表
            var tableList = GetExcelTables(ExcelFilePath);

            if (TableName.IndexOf(TableName, StringComparison.Ordinal) < 0)
            {
                TableName = tableList[0].ToString().Trim();
            }

            var table = new DataTable();
            var dbcon = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 12.0 Xml");
            var cmd = new OleDbCommand("select * from [" + TableName + "$]", dbcon);
            var adapter = new OleDbDataAdapter(cmd);

            try
            {
                if (dbcon.State == ConnectionState.Closed)
                {
                    dbcon.Open();
                }
                adapter.Fill(table);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (dbcon.State == ConnectionState.Open)
                {
                    dbcon.Close();
                }
            }
            return table;
        }

        /// <summary>
        /// 获取Excel文件指定数据表的数据列表
        /// </summary>
        /// <param name="ExcelFileName">Excel文件名</param>
        /// <param name="TableName">数据表名</param>
        public static ArrayList GetExcelTableColumns(string ExcelFileName, string TableName)
        {
            var ColsList = new ArrayList();
            if (!File.Exists(ExcelFileName))
            {
                return ColsList;
            }
            using (var conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 12.0 Xml;Data Source=" + ExcelFileName))
            {
                conn.Open();
                var dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, TableName, null });

                //获取列个数
                if (dt == null)
                {
                    return ColsList;
                }
                var colcount = dt.Rows.Count;
                for (var i = 0; i < colcount; i++)
                {
                    var colname = dt.Rows[i]["Column_Name"].ToString().Trim();
                    ColsList.Add(colname);
                }
            }
            return ColsList;
        }
    }
}
