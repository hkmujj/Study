using System.Xml.Serialization;

namespace Urban.Iran.HMI.Config.ConfigModel
{
    [XmlRoot]
    public class PasswordConfig
    {
        public string DriverPassword { set; get; }

        public string ReconditionPassword { set; get; }
    }
}