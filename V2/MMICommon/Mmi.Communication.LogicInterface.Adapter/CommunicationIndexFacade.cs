using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CommonUtil.Model;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using Excel.Interface;

namespace Mmi.Communication.Index.Adapter
{
    public class CommunicationIndexFacade
    {
        public IReadOnlyDictionary<string, int> InBoolDictionary { private set; get; }

        public IReadOnlyDictionary<string, int> InFloatDictionary { private set; get; }

        public IReadOnlyDictionary<string, int> OutBoolDictionary { private set; get; }

        public IReadOnlyDictionary<string, int> OutFloatDictionary { private set; get; }


        public void Initalize(List<CommunicationIndexConfig> configs)
        {
            var adpt = new ExcelAdapter();
            var inb = configs.FirstOrDefault(f => f.Type == CommunicationIndexType.InBool);
            if (inb == null)
            {
                LogMgr.Warn("There is not InBool communication config in Iran root config.");
            }
            else
            {
                var ds = adpt.Adapter(inb);
                InBoolDictionary = ToDictionary(ds.Tables[0]);
            }

            var inf = configs.FirstOrDefault(f => f.Type == CommunicationIndexType.InFloat);
            if (inf == null)
            {
                LogMgr.Warn("There is not InFloat communication config in Iran root config.");
            }
            else
            {
                var ds = adpt.Adapter(inf);
                InFloatDictionary = ToDictionary(ds.Tables[0]);
            }

            var outb = configs.FirstOrDefault(f => f.Type == CommunicationIndexType.OutBool);
            if (outb == null)
            {
                LogMgr.Warn("There is not OutBool communication config in Iran root config.");
            }
            else
            {
                var ds = adpt.Adapter(outb);
                OutBoolDictionary = ToDictionary(ds.Tables[0]);
            }

            var outf = configs.FirstOrDefault(f => f.Type == CommunicationIndexType.OutFloat);
            if (outf == null)
            {
                LogMgr.Warn("There is not OutFloat communication config in Iran root config.");
            }
            else
            {
                var ds = adpt.Adapter(outf);
                OutFloatDictionary = ToDictionary(ds.Tables[0]);
            }
        }

        private IReadOnlyDictionary<string, int> ToDictionary(DataTable table)
        {
            var dic = table.Rows.Cast<DataRow>()
                .ToDictionary(row => row[0].ToString(),
                    row => Convert.IsDBNull(row[1]) ? int.MaxValue : Convert.ToInt32(row[1]));
            return dic.AsReadOnlyDictionary();
        }
    }
}