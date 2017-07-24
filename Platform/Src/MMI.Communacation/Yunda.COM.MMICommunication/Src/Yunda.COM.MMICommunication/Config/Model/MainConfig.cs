using System.IO;
using System.Xml.Serialization;
using Yunda.COM.MMICommunication.Model;
using Yunda.COM.MMICommunication.Utils;

namespace Yunda.COM.MMICommunication.Config.Model
{
    [XmlRoot]
    public class MainConfig : IInitalizeParam
    {
        public const string File = "Yunda.COM.MMICommunication.xml";

        public bool IsHost { get; set; }

        private void Test()
        {
            var d = new MainConfig();

            DataSerialization.SerializeToXmlFile(d, Path.Combine("D:\\", File));
        }
    }
}