using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.Tow2
{
    [XmlRoot]
    public class TowInfo2Config
    {
        [XmlArray]
        public List<TowInfo2Cars> Cars { set; get; }
    }

    [XmlType("Car")]
    public class TowInfo2Cars : CRH2CommunicationPortModel
    {
        /// <summary>
        /// 车厢名
        /// </summary>
        [XmlAttribute]
        public string CarName { set; get; }
    }

    static class TowInfo2ConfigTest
    {
        public static void Test()
        {
            var data = new TowInfo2Config()
            {
                Cars= new List<TowInfo2Cars>()
                {
                    new TowInfo2Cars(){ CarName = "2 车厢"},
                    new TowInfo2Cars(){ CarName = "3 车厢"}
                }
            };

            DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");
        }
    }
}
