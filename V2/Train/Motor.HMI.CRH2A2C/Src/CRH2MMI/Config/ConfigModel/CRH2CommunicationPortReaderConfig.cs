using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.Common.Global;
using Excel.Interface;


namespace CRH2MMI.Config.ConfigModel
{
    public class CRH2ExcelReaderConfig : ExcelReaderConfig
    {
        [XmlAttribute]
        public CRH2Type Type { set; get; }

        public void Correction()
        {
            this.File = Path.Combine(GlobalParam.RootConfigPath, File);
        }
    }

    public class CRH2FileConfig
    {
        [XmlArray()]
        [XmlArrayItem("CRH2CommunicationPortReaderConfig")]
        public List<CRH2ExcelReaderConfig> CommunicationPortReaderConfigs { set; get; }

        [XmlArray()]
        [XmlArrayItem("FaultReaderConfig")]
        public List<CRH2ExcelReaderConfig> FaultReaderConfigs { set; get; }

    }

    class CRH2CommunicationPortReaderConfigTest
    {
        public static void Test()
        {
            var data = new List<CRH2ExcelReaderConfig>
            {
                new CRH2ExcelReaderConfig()
                {
                    Coloumns = new List<ColoumnConfig>()
                    {
                        new ColoumnConfig() {Name = "CRH2A", IsPrimaryKey = false},
                        new ColoumnConfig() {Name = "Name", IsPrimaryKey = true}
                    },
                    
                    
                    File = "a.xls",
                    SheetNames = new List<string>() {"InFloat"},
                    Type = CRH2Type.CRH2C,
                },
                new CRH2ExcelReaderConfig()
                {
                    Coloumns = new List<ColoumnConfig>()
                    {
                        new ColoumnConfig() {Name = "CRH2A", IsPrimaryKey = false},
                        new ColoumnConfig() {Name = "Name", IsPrimaryKey = true}
                    },
                    
                    
                    File = "a.xls",
                    SheetNames = new List<string>() {"InFloat"},
                    Type = CRH2Type.CRH2A,
                }
            };
            DataSerialization.SerializeToXmlFile(data, "D:\\a.xml", "root");
        }
    }
}
