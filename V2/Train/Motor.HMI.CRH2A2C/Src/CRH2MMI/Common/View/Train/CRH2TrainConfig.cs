using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using CommonUtil.Model;
using CommonUtil.Util;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.Common.View.Train
{
    public class CRH2TrainConfig
    {
        [XmlArray]
        [XmlArrayItem("Unit")]
        public List<TrainUnitConfig> TrainUnits { set; get; }

        [XmlArray]
        public List<CarConfig> CarConfigs { set; get; }

        [XmlArray]
        public List<AcceptEleCarConfig> AcceptEleCarConfigs { set; get; }

    }

    [DebuggerDisplay("CarNo={CarNo} CarName={CarName} CarType={CarType}")]
    public class CarConfig : CRH2CommunicationPortRectangleModel
    {
        [XmlAttribute]
        public int CarNo { set; get; }

        [XmlAttribute]
        public string CarName { set; get; }

        [XmlAttribute]
        public CarType CarType { set; get; }

        [XmlAttribute]
        public bool IsHeadCar { set; get; }

        [XmlAttribute]
        public Direction HeadDirection { set; get; }

        [XmlIgnore]
        public List<Color> Colors { set; get; }

        [XmlAttribute("Colors")]
        public string XmlColorString
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

    [DebuggerDisplay("CarNo={CarNo} InBool={InBoolXmlNames}")]
    [XmlType("AcceptEleCar")]
    public class AcceptEleCarConfig : CRH2CommunicationPortModel
    {
        [XmlAttribute]
        public int CarNo { set; get; }

        /// <summary>
        /// 界面位置
        /// </summary>
        [XmlAttribute]
        public Direction Location { set; get; }

        /// <summary>
        /// 弓方向
        /// </summary>
        [XmlAttribute]
        public Direction Direction { set; get; }

    }
}