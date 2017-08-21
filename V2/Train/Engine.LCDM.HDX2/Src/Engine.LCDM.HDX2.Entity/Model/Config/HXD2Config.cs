using System.ComponentModel;
using System.Xml.Serialization;
using CommonUtil.Util;

namespace Engine.LCDM.HDX2.Entity.Model.Config
{
    [XmlRoot()]
    public class HXD2Config
    {
        [DisplayName("是否需要外部按键")]
        public bool NeedOutButton { set; get; }

        private static void Test()
        {
            var d = new HXD2Config() { NeedOutButton = true };
            DataSerialization.SerializeToXmlFile(d, "D:\\a.xml");
        }
    }
}