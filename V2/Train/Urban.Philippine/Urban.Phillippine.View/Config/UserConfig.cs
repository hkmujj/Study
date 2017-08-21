using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Config
{
    [XmlRoot]
    public class UserConfig
    {
        [XmlElement("Info")]
        public List<UserInfo> AllUser { get; set; }

        public static void Test()
        {
            var us = new UserConfig
            {
                AllUser = new List<UserInfo>
                {
                    new UserInfo {Type = UserType.Driver, ID = "1001", Password = "8888"},
                    new UserInfo {Type = UserType.SuperMaintainer, ID = "0001", Password = "8888"}
                }
            };
            DataSerialization.SerializeToXmlFile(us, @"d:\userconifg.xml");
        }
    }

    public class UserInfo
    {
        [XmlElement]
        public UserType Type { get; set; }

        [XmlElement]
        public string ID { get; set; }

        [XmlElement]
        public string Password { get; set; }
    }
}