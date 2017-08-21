using System.Collections.Generic;
using System.Xml.Serialization;

namespace Motor.HMI.CRH5A.Config.ConfigModel
{
    [XmlRoot]
    public class ProjectDetailConfig
    {
         public ElectronicInstrumentConfig ElectronicInstrumentConfig { set; get; }
    }

    public class ElectronicInstrumentConfig
    {
        [XmlArray]
        public List<CommunicationDataModel> PageOneDataNameCollection { set; get; }

        [XmlArray]
        public List<CommunicationDataModel> PageTwoDataNameCollection { set; get; }

        [XmlArray]
        public List<CommunicationDataModel> PageThreeDataNameCollection { set; get; }
    }
}