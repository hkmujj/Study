using Excel.Interface;
using Mmi.Communication.Index.Adapter;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Urban.ATC.Siemens.Model
{
    public class IndexConfigure
    {
        public static readonly IndexConfigure Instance = new IndexConfigure();

        private const string InBoolConfigFile = "西门子信号屏ATC接口_InBool.xls";
        private const string InFloatConfigFile = "西门子信号屏ATC接口_InFloat.xls";
        private const string OutBoolConfigFile = "西门子信号屏ATC接口_OutBool.xls";
        private const string OutFloatConfigFile = "西门子信号屏ATC接口_OutFloat.xls";

        private const string NameColoumn = "Name";
        private const string IndexColoumn = "Index";

        public CommunicationIndexFacade IndexFacade { private set; get; }

        private IndexConfigure()
        {
        }

        public void Load(string configDirectory, bool alwaysLoad = false)
        {
            if (alwaysLoad || IndexFacade == null)
            {
                IndexFacade = new CommunicationIndexFacade();
                IndexFacade.Initalize(new List<CommunicationIndexConfig>()
                {
                    CreateCommunicationIndexConfig(CommunicationIndexType.InBool, Path.Combine(configDirectory, InBoolConfigFile)),
                    CreateCommunicationIndexConfig(CommunicationIndexType.InFloat,Path.Combine(configDirectory, InFloatConfigFile) ),
                    CreateCommunicationIndexConfig(CommunicationIndexType.OutBool, Path.Combine(configDirectory, OutBoolConfigFile)),
                    CreateCommunicationIndexConfig(CommunicationIndexType.OutFloat, Path.Combine(configDirectory, OutBoolConfigFile)),
                });
            }
        }

        public DataTable LoadConfig(string fileName)
        {
            var config = new ExcelReaderConfig()
            {
                File = fileName,
                Coloumns =
                    new List<ColoumnConfig>()
                    {
                        new ColoumnConfig() {Name = NameColoumn, IsPrimaryKey = true},
                        new ColoumnConfig() {Name = IndexColoumn, IsPrimaryKey = false}
                    },
                SheetNames = new List<string>() { "Sheet1" },
            };
            return config.Adapter().Tables[0];
        }

        private CommunicationIndexConfig CreateCommunicationIndexConfig(CommunicationIndexType type, string fileName)
        {
            return new CommunicationIndexConfig()
            {
                Type = type,
                File = fileName,
                Coloumns =
                    new List<ColoumnConfig>()
                    {
                        new ColoumnConfig() {Name = NameColoumn, IsPrimaryKey = true},
                        new ColoumnConfig() {Name = IndexColoumn, IsPrimaryKey = false}
                    },
                SheetNames = new List<string>() { "Sheet1" },
            };
        }
    }
}