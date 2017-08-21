using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Model;
using CommonUtil.Util;

namespace CRH2MMI.AxleTemperature
{
    public class AxleTemperatureDetailConfig
    {
        public XmlPoint Location { get; set; }
        [XmlAttribute]
        public int NumWidht { get; set; }
        [XmlAttribute]
        public int ContentWidth { get; set; }
        [XmlAttribute]
        public int CarWidth { get; set; }

        public HeaderConfig HeightConfig { get; set; }
        public List<ContentConfig> AllContenConfig { get; set; }

        private static void Test()
        {
            var a = new AxleTemperatureDetailConfig();
            a.AllContenConfig = new List<ContentConfig>()
            {
                new ContentConfig()
                {
                    Num = 1,
                    SettingPlace = "韵达",
                    CarConfig = new List<CarConfig>()
                    {
                        new CarConfig()
                        {
                             CarNum = 1,
                             DispalyCarContent = "1车",
                        }, new CarConfig()
                        {
                             CarNum = 1,
                             DispalyCarContent = "1车",
                        }
                      
                    },Height = 20,Page = 1,Row = 1
                },
                  new ContentConfig()
                {
                    Num = 1,
                    SettingPlace = "韵达",
                    CarConfig = new List<CarConfig>()
                    {
                        new CarConfig()
                        {
                             CarNum = 1,
                             DispalyCarContent = "1车",
                        }, new CarConfig()
                        {
                             CarNum = 1,
                             DispalyCarContent = "1车",
                        }
                      
                    },Height = 20,Page = 1,Row =2
                }
            };
            a.NumWidht = 20;
            a.ContentWidth = 100;
            a.HeightConfig = new HeaderConfig()
            {
                Num = "No.",
                Content = "设置场所",
                DisplayContent = "轮轴温度",
                HeaderCarConfig = new List<CarConfig>() 
                {
                    new CarConfig(){ CarNum = 1,
                    DispalyCarContent = "1车",}, new CarConfig(){ CarNum = 1,
                    DispalyCarContent = "1车",}
                },
                Height = 50,

            };
            a.Location = new XmlPoint()
            {
                X = 10,
                Y = 20
            };
            DataSerialization.SerializeToXmlFile(a, @"d:\x.xml");
        }
    }

    public class HeaderConfig
    {
        [XmlAttribute]
        public string Num { get; set; }
        [XmlAttribute]
        public string DisplayContent { get; set; }
        [XmlAttribute]
        public string Content { get; set; }
        public List<CarConfig> HeaderCarConfig { get; set; }
        [XmlAttribute]
        public int Height { get; set; }

    }



    [XmlType("AxleCar")]
    public class CarConfig
    {
        [XmlAttribute]
        public int CarNum { get; set; }
        [XmlAttribute]
        public string DispalyCarContent { get; set; }
    }

    public class Content
    {
        [XmlAttribute("AxleRow")]
        public int Row { get; set; }
    }
    public class ContentConfig : Content
    {
        [XmlAttribute]
        public int Page { get; set; }
        [XmlAttribute]
        public int Num { get; set; }
        [XmlAttribute]
        public string SettingPlace { get; set; }
        public List<CarConfig> CarConfig { get; set; }
        [XmlAttribute]
        public int Height { get; set; }
    }
}