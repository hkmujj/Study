using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Serialization;
using CommonUtil.Model;
using CommonUtil.Util;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.WorkState
{
    public class WorkStateConfig
    {
        [XmlArray]
        public List<ParkConfig> ParkConfigs { set; get; }

        [XmlElement]
        public LevelConfigs LevenConfigs { set; get; }

    }

    [DebuggerDisplay("Name = {Name}")]
    public class ParkConfig : CRH2CommunicationPortRectangleModel
    {
        [XmlAttribute]
        public string Name { set; get; }

        [XmlIgnore]
        public Color TextColor { set; get; }

        [XmlAttribute("TextColor")]
        public string TextColorString
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

        [XmlIgnore]
        public Color BkColor { set; get; }

        [XmlAttribute("BkColor")]
        public string BkColorString
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
    }

    public class LevelConfigs : XmlRectangle
    {

        [XmlElement("Level")]
        public List<LevelConfig> Configs { set; get; }

        public LevelConfigs()
        {
            Rectangle = new Rectangle();
        }

    }


    [DebuggerDisplay("Name = {Name}  Priority = {Priority}")]
    [XmlType("Level")]
    public class LevelConfig : CRH2CommunicationPortModel
    {
        [XmlAttribute]
        public string Name { set; get; }

        [XmlAttribute]
        public int Priority { set; get; }
    }
}
