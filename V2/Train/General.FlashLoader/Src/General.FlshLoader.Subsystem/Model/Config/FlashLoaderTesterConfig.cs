using System.Xml.Serialization;
using CommonUtil.Util;

namespace General.FlashLoader.Subsystem.Model.Config
{
    [XmlRoot]
    public class FlashLoaderTesterConfig
    {
        public string BoolFile { set; get; }

        public string FloatFile { get; set; }

        private static void Test()
        {
            var data = new FlashLoaderTesterConfig(){ BoolFile = "a.xls", FloatFile = "B.xls"};
            DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");
        }
    }
}