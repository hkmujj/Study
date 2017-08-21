using System.Xml.Serialization;


namespace CRH2MMI.Menu
{
    [XmlRoot]
    [XmlType("Menu")]
    public class DriverMenuContent
    {
        /// <summary>
        /// 按键名字
        /// </summary>
        [XmlAttribute]
        public string Name { set; get; }

        /// <summary>
        /// 点击后切换到的页
        /// </summary>
        [XmlAttribute]
        public int Goto { set; get; }

        /// <summary>
        /// 位置
        /// </summary>
        [XmlAttribute]
        public int Xoffset{ set; get; }
        [XmlAttribute]
        public int Yoffset { set; get; }
    }

}
