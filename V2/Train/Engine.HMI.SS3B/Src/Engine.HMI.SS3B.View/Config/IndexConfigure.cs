using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel.Interface;
using Mmi.Communication.Index.Adapter;

namespace Engine.HMI.SS3B.View.Config
{
    public class IndexConfigure
    {
        public static readonly IndexConfigure Instance = new IndexConfigure();

        const string InBoolConfigFile = "韶山3BHMI接口_InBool.xls";
        const string InFloatConfigFile = "韶山3BHMI接口_InFloat.xls";
        const string OutBoolConfigFile = "韶山3BHMI接口_OutBool.xls";
        const string OutFloatConfigFile = "韶山3BHMI接口_OutFloat.xls";

        const string NameColoumn = "Name";
        const string IndexColoumn = "Index";

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
                    CreateCommunicationIndexConfig(CommunicationIndexType.OutFloat, Path.Combine(configDirectory, OutFloatConfigFile)),
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