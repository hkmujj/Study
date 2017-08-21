using System.Diagnostics;
using System.Xml.Serialization;

namespace Motor.HMI.CRH1A.Config.ConfigModel
{
    [XmlType]
    [DebuggerDisplay("UserType={UserType}, Password={Password}")]
    public class PasswordConfig
    {
        [XmlAttribute]
        public UserType UserType { set; get; }

        [XmlAttribute]
        public string Password { set; get; }
    }

    public enum UserType
    {
        Driver,

        /// <summary>
        /// ป๚ะตสฆ
        /// </summary>
        Mechanician
    }
}