using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using CommonUtil.Model;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.DriveState
{
    public class DriveStateConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlArray]
        public List<DriveStateCutInfoRegion> CutInfoRegions { set; get; }

        [XmlArray]
        public List<DriveStateDoorConfig> DoorConfigs { set; get; }
    }

    [DebuggerDisplay("CarNo = {CarNo} InBoolColoumNames = {InBoolXmlNames}")]
    [XmlType("DoorColse")]
    public class DriveStateDoorConfig : CRH2CommunicationPortRectangleModel
    {
        [XmlAttribute]
        public int CarNo { set; get; }
    }

    [XmlType("Region")]
    public class DriveStateCutInfoRegion : XmlRectangle
    {
        [XmlAttribute]
        public CutValueFrom CutValueFrom { set; get; }

        [XmlElement("DriveStateCutInfo")]
        public List<DriveStateCutInfo> DriveStateCutInfoList { set; get; }
    }

    public enum CutValueFrom
    {
        Bool,
        Event,
    }

    public class DriveStateCutInfo: CRH2CommunicationPortModel
    {
        [XmlAttribute]
        public string Text { set; get; }
    }
}
