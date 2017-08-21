using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.Config.ConfigModel;


namespace CRH2MMI.Jack
{
    /// <summary>
    /// 配电盘配置
    /// </summary>
    [XmlRoot]
    public class JackConfig
    {
        [XmlAttribute]
        public int UnitWidth { set; get; }

        [XmlAttribute]
        public int UnitHeight { set; get; }

        [XmlElement]
        public List<JackOfCar> JackOfCars { set; get; }
    }

    [DebuggerDisplay("CarNo = {CarNo}")]
    public class JackOfCar
    {
        [XmlAttribute]
        public int CarNo { set; get; }

        [XmlAttribute]
        public int GroupID { set; get; }

        [XmlElement("JackUnit")]
        public List<JackUnit> Units { set; get; }
    }

    [DebuggerDisplay("Page = {Page}, X = {X}, Y = {Y}, Name = {Name}")]
    public class JackUnit : CRH2CommunicationPortModel
    {
        [XmlAttribute]
        public int Page { set; get; }

        /// <summary>
        /// 是否有连线
        /// </summary>
        [XmlAttribute]
        public int RelationShip { set; get; }

        [XmlAttribute]
        public int Y { set; get; }

        [XmlAttribute]
        public int X { set; get; }

        [XmlIgnore]
        public List<int> InBoolList { set; get; }

        [XmlAttribute("InBoolList")]
        public string InBoolListAttr
        {
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    InBoolList = value.Split(',').Select(DynamicInvokeHelper.DynamicInvoke<int>).ToList();
                }
            }
            get { return string.Join(",", InBoolList.Select(s => s.ToString()).ToArray()); }
        }

        [XmlIgnore]
        public List<Color> TextBkColors { set; get; }

        [XmlAttribute("TextBkColors")]
        public string TextBkColorsAttr
        {
            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    TextBkColors = value.Split(',').Select(ColorTranslator.FromHtml).ToList();
                }
            }
            get { return string.Join(",", TextBkColors.Select(ColorTranslator.ToHtml).ToArray()); }
        }


        [XmlAttribute]
        public string Name { set; get; }

        public JackUnit()
        {
            InBoolList = new List<int>();
            TextBkColors = new List<Color>();
        }

    }

    static class JackConfigTest
    {
        public static void Test()
        {
            var data = new JackConfig()
            {
                UnitWidth = 20,
                UnitHeight = 10,
                JackOfCars = new List<JackOfCar>()
                {
                    new JackOfCar()
                    {
                        CarNo = 0,
                        Units = new List<JackUnit>()
                        {
                            new JackUnit()
                            {
                                RelationShip = 1,
                                InBoolList = new List<int>() {11},
                                Name = "23",
                                TextBkColors = new List<Color>() {Color.White, Color.FromArgb(0,255,0)},
                                X = 10,
                                Y = 12
                            },
                            new JackUnit()
                            {
                                RelationShip = 1,
                                InBoolList = new List<int>() {121},
                                Name = "213",
                                TextBkColors = new List<Color>() {Color.White, Color.Red},
                                X = 110,
                                Y = 112
                            },
                        }
                    }
                }


            };

            var d = DataSerialization.DeserializeFromXmlFile<JackConfig>("D:\\a.xml");

            foreach (var jackOfCar in d.JackOfCars)
            {
                foreach (var jackUnit in jackOfCar.Units)
                {
                    if (jackUnit.Name.Contains("故障") || jackUnit.Name.Contains("切除"))
                    {
                        jackUnit.TextBkColors = new List<Color>() { Color.White, Color.Red };
                    }
                    else
                    {
                        jackUnit.TextBkColors = new List<Color>() { Color.White, Color.FromArgb(0, 255, 0) };
                    }
                }
            }

            DataSerialization.SerializeToXmlFile(d, "D:\\a.xml");
        }
    }

}
