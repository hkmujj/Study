using System.Xml.Serialization;
using CommonUtil.Util;

namespace Engine.TCMS.HXD3C.Config
{
    public class PasswordConfig
    {

        [XmlElement]
        public string Password { get; set; }
        [XmlElement]
        public int MaxLength { get; set; }
        private static void Test()
        {
            var a = new PasswordConfig();

            a.Password = "000";
            DataSerialization.SerializeToXmlFile(a, @"d:\a.xml");
        }
    }

}