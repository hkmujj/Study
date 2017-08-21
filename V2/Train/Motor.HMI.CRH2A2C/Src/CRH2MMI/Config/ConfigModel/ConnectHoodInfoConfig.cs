using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using CommonUtil.Model;
using CommonUtil.Util;

namespace CRH2MMI.Config.ConfigModel
{
    public class ConnectHoodInfoConfig
    {

        public List<CarConnectInfo> ConnectHoodInfo { get; set; }
        static void Test()
        {
            var a = new ConnectHoodInfoConfig();
            a.ConnectHoodInfo = new List<CarConnectInfo>()
            {
                new CarConnectInfo()
                {
                    TitleName = "T1车",
                    Rectangle = new Rectangle(),
                    InnerInfo = new List<ContentInfo>()
                    {
                        new ContentInfo()
                        {
                            Text = "打开头罩锁",
                            InBoolName = "连接头罩信息 T1车 打开头罩锁",
                            XmlColorsString = "White,Green",
                            Rectangle = new Rectangle()
                        },new ContentInfo()
                        {
                            Text = "打开头罩",
                            InBoolName = "连接头罩信息 T1车 打开头罩",
                            XmlColorsString = "White,Green",
                            Rectangle = new Rectangle()
                        }
                         
                    }
                },
                 new CarConnectInfo()
                {
                    TitleName = "T2车",
                    Rectangle = new Rectangle(),
                    InnerInfo = new List<ContentInfo>()
                    {
                        new ContentInfo()
                        {
                            Text = "打开头罩锁",
                            InBoolName = "连接头罩信息 T2车 打开头罩锁",
                            XmlColorsString = "White,Green"
                        }
                         
                    }
                }
            };

            DataSerialization.SerializeToXmlFile(a, @"d:\a.xml");
        }
    }

    public class CarConnectInfo : XmlRectangle
    {
        [XmlAttribute]
        public string TitleName { get; set; }

        [XmlElement]
        public List<ContentInfo> InnerInfo { get; set; }
    }
    public class ContentInfo : XmlRectangle
    {
        [XmlAttribute]
        public string Text { get; set; }
        [XmlAttribute]
        public string InBoolName { get; set; }
        [XmlIgnore]

        public List<Color> Colors { get; set; }
        [XmlAttribute("Colors")]
        public string XmlColorsString
        {
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    Colors = value.Split(',').Select(ColorTranslator.FromHtml).ToList();
                }
            }
            get { return string.Join(",", Colors.Select(ColorTranslator.ToHtml).ToArray()); }
        }
    }
}