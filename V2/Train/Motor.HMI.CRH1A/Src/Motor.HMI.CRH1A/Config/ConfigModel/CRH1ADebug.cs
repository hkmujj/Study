using System.Xml.Serialization;

namespace Motor.HMI.CRH1A.Config.ConfigModel
{
    [XmlType]
    public class CRH1ADebug 
    {
        [XmlElement]
        public bool AutoLightUpScreen { set; get; }

        public string Name { set; get; }
    }
}
