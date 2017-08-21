using System.ComponentModel;
using System.Xml.Serialization;

namespace Engine.HMI.SS3B.View.Config
{
    public class SS3BConfig
    {
        public const string FileName = "SS3BConfig.xml";
        [XmlElement]
        public SS3BType Type { get; set; }
    }

    public enum SS3BType
    {
        [Description("柳州SS3B")]
        LiuZhou,
        [Description("昆明SS3B")]
        KunMing,
    }
}