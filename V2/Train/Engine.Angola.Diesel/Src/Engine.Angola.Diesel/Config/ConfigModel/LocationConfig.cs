using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Model;
using CommonUtil.Util;

namespace Engine.Angola.Diesel.Config.ConfigModel
{
    public class LocationConfig
    {
        public const string FileName = "LocationConfig.xml";

        [XmlArray]
        [XmlArrayItem("Rectangle")]
        public List<XmlRectangleF> Rectangles { set; get; }

        // ReSharper disable once UnusedMember.Local
        private static void Test()
        {
            var d = new LocationConfig()
            {
                Rectangles = new List<XmlRectangleF>()
                {
                    new XmlRectangleF(),
                    new XmlRectangleF(),
                }
            };
            DataSerialization.SerializeToXmlFile(d, "D:\\a.xml");
        }
    }
}
