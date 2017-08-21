using System.Xml.Serialization;
using CRH2MMI.Common.Models;
using CRH2MMI.Common.View.Train;

namespace CRH2MMI.Notify
{
    [XmlRoot]
    public class NotifyConfig 
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        public GridConfig Grid { set; get; }
    }
}
