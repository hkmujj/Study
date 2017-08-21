using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;

namespace CRH2MMI.Config.ConfigModel
{
    [XmlRoot("DetailConfigFile")]
    public class ConfigFileModel 
    {

        [XmlElement]
        public List<TypeFileRelation> TypeFileRelationShips { set; get; }

        public string GetCurrentDetailConfigFile(CRH2Type type)
        {
            return TypeFileRelationShips.Find(f => f.Type == type).File;
        }

    }

    public class TypeFileRelation
    {
        [XmlAttribute]
        public CRH2Type Type { set; get; }

        [XmlAttribute]
        public string File { set; get; }
    }

    class ConfigFileModelTest
    {
        public static void Test()
        {
            var data = new ConfigFileModel()
            {
                TypeFileRelationShips = new List<TypeFileRelation>()
                {
                    new TypeFileRelation() {Type = CRH2Type.CRH2A, File = "CRH2AConfig.xml"},
                    new TypeFileRelation() {Type = CRH2Type.CRH2C, File = "CRH2CConfig.xml"}
                }
            };
            DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");
        }
    }

}
