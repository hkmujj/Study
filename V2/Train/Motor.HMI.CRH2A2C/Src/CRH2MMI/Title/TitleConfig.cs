using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.Title
{
    public class TitleConfig
    {
        [XmlArray]
        public List<TitleUnit> TitleUnits { set; get; }


    }

    public class TitleUnit : CRH2CommunicationPortRectangleModel
    {
        [XmlAttribute]
        public string Name { set; get; }


        //[XmlAnyElement]
        //public XmlRectangle Rectangle { set; get; }
        [XmlAttribute]
        public int FontWidth { set; get; }

        [XmlAttribute]
        public string ToStringFormat { set; get; }

        [XmlAttribute]
        public int BkImgaeIndex { set; get; }

        [XmlIgnore]
        public Color BkColor { set; get; }

        [XmlIgnore]
        public Color TextColor { set; get; }


        [XmlAttribute("BkColor")]
        public string BkColorStr
        {
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    BkColor = ColorTranslator.FromHtml(value);
                }
            }
            get { return ColorTranslator.ToHtml(BkColor); }
        }

        [XmlAttribute("TextColor")]
        public string TextColorStr
        {
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    TextColor = ColorTranslator.FromHtml(value);
                }
            }
            get { return ColorTranslator.ToHtml(TextColor); }
        }

    }

    class TitleConfigTest
    {
        public static void Test()
        {
            var data = new TitleConfig
            {
                TitleUnits = new List<TitleUnit>()
                {
                    new TitleUnit()
                    {
                        Name = "火",
                        BkImgaeIndex = 1,
                        Rectangle = new Rectangle(0, 1, 2, 3),
                        InBoolColoumNames = new[] {"aa",}
                    },
                    new TitleUnit()
                    {
                        Name = "火",
                        BkImgaeIndex = 1,
                        Rectangle = new Rectangle(0, 1, 2, 3),
                        InBoolColoumNames = new[] {"aa",}
                    },
                }
            };
            DataSerialization.SerializeToXmlFile(data, "d:\\a.xml");
        }
    }
}
