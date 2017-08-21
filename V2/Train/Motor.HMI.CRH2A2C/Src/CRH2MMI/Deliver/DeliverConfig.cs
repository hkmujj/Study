using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.Deliver
{
    [XmlRoot]
    public class DeliverConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlElement]
        public DeliverResultType DeliverResultType { set; get; }

        [XmlArray]
        public List<DeliverCarConfig> DeliverCarConfigs { set; get; }

    }

    [DebuggerDisplay("CarNo={CarNo}")]
    public class DeliverCarConfig : CRH2CommunicationPortRectangleModel
    {
        [XmlAttribute]
        public int CarNo { set; get; }

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
}
