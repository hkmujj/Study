using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using CommonUtil.Util;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.Common.View.Train
{
    [DebuggerDisplay("UnitName={UnitName} CarNos={XmlCarNoString}")]
    [XmlType("Unit")]
    public class TrainUnitConfig : CRH2CommunicationPortModel
    {
        public TrainUnitConfig()
        {
            CarNos = new List<int>();
        }

        [XmlAttribute]
        public string UnitName { set; get; }

        [XmlIgnore]
        public List<int> CarNos { set; get; }

        [XmlAttribute("CarNos")]
        public string XmlCarNoString
        {
            set { CarNos = value.Split(new char[] {','}).Select(s => s.AsInt()).ToList(); } 
            get { return string.Join(",", CarNos.Select(s => s.ToString()).ToArray()); }
        }

        [XmlIgnore]
        public List<Color> Colors { set; get; }

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