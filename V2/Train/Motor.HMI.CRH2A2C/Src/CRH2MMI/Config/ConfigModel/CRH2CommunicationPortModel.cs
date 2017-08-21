using System.Diagnostics;
using System.Xml.Serialization;

namespace CRH2MMI.Config.ConfigModel
{
    /// <summary>
    ///  通信接口的 xml 模型
    /// </summary>
    [DebuggerDisplay("InBoolColoumNames={InBoolXmlNames} InFloatColoumNames={InFloatXmlNames} OutBoolColoumNames={OutBoolXmlNames} OutFloatColoumNames={OutFloatXmlNames}")]
    [XmlRoot]
    public class CRH2CommunicationPortModel
    {
        public CRH2CommunicationPortModel()
        {
            InBoolColoumNames = new string[0];
            OutBoolColoumNames = new string[0];
            InFloatColoumNames = new string[0];
            OutFloatColoumNames = new string[0];
        }

        [XmlIgnore]
        public string[] InBoolColoumNames { set; get; }
        [XmlAttribute("InBoolNames")]
        public string InBoolXmlNames
        {
            set { InBoolColoumNames = value.Split(new []{','}); }
            get { return string.Join(",", InBoolColoumNames); }
        }


        [XmlIgnore]
        public string[] OutBoolColoumNames { set; get; }
        [XmlAttribute("OutBoolNames")]
        public string OutBoolXmlNames
        {
            set { OutBoolColoumNames = value.Split(new[] { ',' }); }
            get { return string.Join(",", OutBoolColoumNames); }
        }


        [XmlIgnore]
        public string[] InFloatColoumNames { set; get; }
        [XmlAttribute("InFloatNames")]
        public string InFloatXmlNames
        {
            set { InFloatColoumNames = value.Split(new[] { ',' }); }
            get { return string.Join(",", InFloatColoumNames); }
        }

        [XmlIgnore]
        public string[] OutFloatColoumNames { set; get; }
        [XmlAttribute("OutFloatNames")]
        public string OutFloatXmlNames
        {
            set { OutFloatColoumNames = value.Split(new[] { ',' }); }
            get { return string.Join(",", OutFloatColoumNames); }
        }

    }
}
