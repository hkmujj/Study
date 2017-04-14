using System.Xml.Serialization;
using MMI.Facility.Interface.Data.Config.Net;

namespace MMI.Facility.DataType.Config
{
    [XmlRoot]
    public class TypeNetConfigBase : ITypeNetConfig
    {
        [XmlAttribute]
        public NetType NetType { get; set; }
    }
}