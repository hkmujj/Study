using System.Collections.Generic;
using System.Xml.Serialization;
using CRH2MMI.Common.View;
using CRH2MMI.Common.View.Train;

namespace CRH2MMI.Vigilant
{
    [XmlType("VigilantView")]
    public class VigilantConfig
    {
        [XmlElement]
        public TrainViewLocation TrainViewLocation { set; get; }

        [XmlArray("MatrixViews")]
        [XmlArrayItem("MatrixViewItem")]
        public List<MatrixViewConfig> ViewItems { set; get; }
    }

}
