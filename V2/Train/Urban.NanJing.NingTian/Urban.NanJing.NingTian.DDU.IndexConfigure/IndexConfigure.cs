using System.Collections.Generic;
using System.Data;
using System.IO;
using CommonUtil.Model;
using Excel.Interface;
using Mmi.Common.CommunicationIndexWrapper;
using Mmi.Communication.Index.Adapter;

namespace Urban.NanJing.NingTian.DDU.Index
{
    public class IndexConfigure
    {
        public static readonly IndexConfigure Instance = new IndexConfigure();

        public const string InBoolConfigFile = "南京宁天线车辆屏接口_InBool.xls";
        public const string InFloatConfigFile = "南京宁天线车辆屏接口_InFloat.xls";
        public const string OutBoolConfigFile = "南京宁天线车辆屏接口_OutBool.xls";
        public const string OutFloatConfigFile = "南京宁天线车辆屏接口_OutFloat.xls";

        public const string NameColoumn = "Name";
        public const string IndexColoumn = "Index";

        public CommunicationIndexFacade CommunicationIndexFacade { private set; get; }


        private IndexConfigure()
        {
        }

        public void Load(string configDirectory, bool alwaysLoad = false)
        {
            if (alwaysLoad || CommunicationIndexFacade == null)
            {
                CommunicationIndexFacade = new CommunicationIndexFacade();
                CommunicationIndexFacade.Initalize(new List<CommunicationIndexConfig>
                {
                    CreateCommunicationIndexConfig(CommunicationIndexType.InBool, Path.Combine(configDirectory, InBoolConfigFile)),
                    CreateCommunicationIndexConfig(CommunicationIndexType.InFloat,Path.Combine(configDirectory, InFloatConfigFile) ),
                    CreateCommunicationIndexConfig(CommunicationIndexType.OutBool, Path.Combine(configDirectory, OutBoolConfigFile)),
                    CreateCommunicationIndexConfig(CommunicationIndexType.OutFloat, Path.Combine(configDirectory, OutFloatConfigFile)),
                });
            }
        }

        public DataTable LoadConfig(string fileName)
        {
            var config = new ExcelReaderConfig
            {
                File = fileName,
                Coloumns =
                    new List<ColoumnConfig>
                    {
                        new ColoumnConfig {Name = NameColoumn, IsPrimaryKey = true},
                        new ColoumnConfig {Name = IndexColoumn, IsPrimaryKey = false}
                    },
                SheetNames = new List<string> {"Sheet1"},
            };
            return config.Adapter().Tables[0];
        }

        private CommunicationIndexConfig CreateCommunicationIndexConfig(CommunicationIndexType type, string fileName)
        {
            return new CommunicationIndexConfig
            {
                Type = type,
                File = fileName,
                Coloumns =
                    new List<ColoumnConfig>
                    {
                        new ColoumnConfig {Name = NameColoumn, IsPrimaryKey = true},
                        new ColoumnConfig {Name = IndexColoumn, IsPrimaryKey = false}
                    },
                SheetNames = new List<string> { "Sheet1" },
            };
        }
    }
}
