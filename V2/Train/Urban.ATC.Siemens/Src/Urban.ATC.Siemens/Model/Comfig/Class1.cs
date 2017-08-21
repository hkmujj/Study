using Excel.Interface;
using System.Xml.Serialization;

namespace Urban.ATC.Siemens.Subsystem.Model.Comfig
{
    [XmlRoot]
    public class CommunicationInterfaceConfig
    {
        public ExcelReaderConfig InBoolConfig { set; get; }

        public ExcelReaderConfig OutBoolConfig { set; get; }

        public ExcelReaderConfig InFloatConfig { set; get; }

        public ExcelReaderConfig OutFloatConfig { set; get; }
    }
}