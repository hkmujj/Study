using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Motor.HMI.CRH1A.Config.ConfigModel;
using Motor.HMI.CRH1A.Station.Door;

namespace Motor.HMI.CRH1A.Station
{
    [XmlRoot]
    public class StationConfig 
    {
        [XmlElement]
        public StationDoorStyle DoorStyle { set; get; }
    }
}
