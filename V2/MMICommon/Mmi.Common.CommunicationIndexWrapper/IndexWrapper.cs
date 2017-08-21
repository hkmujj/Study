using System;
using System.Collections.Generic;
using System.Data;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;

namespace Mmi.Common.CommunicationIndexWrapper
{
    public class IndexWrapper : IIndexWrapper
    {
        public IndexWrapper(DataTable dt, string nameCol, string indexCol)
        {
            if (dt == null)
            {
                LogMgr.Error("Can not wrapper null DataTable.");
                return;
            }

            if (!dt.Columns.Contains(nameCol) || !dt.Columns.Contains(indexCol))
            {
                LogMgr.Error("Can not found coloumns where name are {0} and {1}", nameCol, indexCol);
                return;
            }

            var dic = new Dictionary<string, int>();
            foreach (DataRow row in dt.Rows)
            {
                var name = row[nameCol].ToString();
                var index = int.MaxValue;
                if (Convert.IsDBNull(row[indexCol]))
                {
                    LogMgr.Warn(
                        "Can not parser index value where coloumn name={0}, table name={1}, we will ignore this row.",
                        indexCol, dt.TableName);
                }

                index = Convert.ToInt32(row[indexCol]);
                dic.Add(name, index);
            }
            ConcreatNameIndexDictionary = dic;
            NameIndexDictionary = dic.AsReadOnlyDictionary();
        }

        public Dictionary<string, int> ConcreatNameIndexDictionary { private set; get; }

        public IReadOnlyDictionary<string, int> NameIndexDictionary { get; private set; }
    }
}
