using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.BreakLocked
{
    [XmlRoot]
    public class BrakeLockedConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlElement]
        public GridConfig Grid { set; get; }

        [XmlArray]
        [XmlArrayItem("SendModel")]
        public List<BreakLockedSendModel> BreakLockedSendModels { set; get; }
    }

    [DebuggerDisplay("CarNo={CarNo} Type={Type} OutBoolColoumNames={OutBoolXmlNames}")]
    
    public class BreakLockedSendModel : CRH2CommunicationPortModel
    {
        [XmlAttribute]
        public int CarNo { set; get; }

        [XmlAttribute]
        public int LockedLocation { set; get; }

        [XmlAttribute]
        public CtrolType Type { set; get; }

    }
}
