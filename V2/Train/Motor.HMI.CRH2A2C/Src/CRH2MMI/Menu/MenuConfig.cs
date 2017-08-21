using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.Common.Global;


namespace CRH2MMI.Menu
{
    [XmlType("MenuView")]
    public class MenuConfig
    {
        [XmlArray("DriverMenus")]
        [XmlArrayItem("Menu")]
        public List<DriverMenuContent> DriverMenus { set; get; }
    }

    internal class MenuConfigTest
    {
        public static void Test()
        {
            var mens = new MenuConfig()
            {
                DriverMenus = new List<DriverMenuContent>()
                {
                    new DriverMenuContent(){ Goto = 1, Name = "1", Xoffset = 0, Yoffset = 0},
                    new DriverMenuContent(){ Goto = 1, Name = "1", Xoffset = 0, Yoffset = 0},
                }
            };

            DataSerialization.SerializeToXmlFile(new List<MenuConfig> { mens }, "D:\\a.xml", GlobalResource.ViewConfigXmlRootName);
        }
    }
}
