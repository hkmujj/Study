using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using Excel.Interface;
using MMI.Facility.Interface;

namespace Urban.Iran.DMI.Index
{
    public class IndexParam
    {
        public static IndexParam Instance { private set; get; }

        public CommonUtil.Model.IReadOnlyDictionary<string, int> InBoolKeyIndexDictionary { private set; get; }
        public CommonUtil.Model.IReadOnlyDictionary<string, int> InFloatKeyIndexDictionary { private set; get; }
        public CommonUtil.Model.IReadOnlyDictionary<string, int> OutFloatKeyIndexDictionary { private set; get; }
        public CommonUtil.Model.IReadOnlyDictionary<string, int> OutBoolKeyIndexDictionary { private set; get; }

        static IndexParam()
        {
            Instance = new IndexParam();
        }

        private IndexParam()
        {

        }

        public void Initalize(string configPath)
        {
            var file = Path.Combine(configPath, "Iran信号屏接口适配表_InBool.xls");
            InBoolKeyIndexDictionary = LoadKeyIndex(file).AsReadOnlyDictionary();

            file = Path.Combine(configPath, "Iran信号屏接口适配表_InFloat.xls");
            InFloatKeyIndexDictionary = LoadKeyIndex(file).AsReadOnlyDictionary();

            file = Path.Combine(configPath, "Iran信号屏接口适配表_OutFloat.xls");
            OutFloatKeyIndexDictionary = LoadKeyIndex(file).AsReadOnlyDictionary();

            file = Path.Combine(configPath, "Iran信号屏接口适配表_OutBool.xls");
            OutBoolKeyIndexDictionary = LoadKeyIndex(file).AsReadOnlyDictionary();
        }

        private Dictionary<string, int> LoadKeyIndex(string file)
        {
            var dic = new Dictionary<string, int>();
            if (!File.Exists(file))
            {
                LogMgr.Error(string.Format("can not found file {0}", file));
                return dic;
            }
            var config = new ExcelReaderConfig()
            {
                File = file,
                SheetNames = new List<string>() {"Sheet1"},
                Coloumns = new List<ColoumnConfig>()
                {
                    new ColoumnConfig() {IsPrimaryKey = true, Name = "Name"},
                    new ColoumnConfig() {IsPrimaryKey = false, Name = "Index"}
                }
            };
            var ds = config.Adapter();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var key = row[0].ToString();
                var index = int.MaxValue;
                if (!Convert.IsDBNull(row[1]))
                {
                    index = Convert.ToInt32(row[1]);
                }
                dic.Add(key, index);
            }
            return dic;
        }
    }
}