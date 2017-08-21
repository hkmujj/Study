using System.Collections.Generic;
using System.Xml.Serialization;

namespace CRH2MMI.Common.Models
{
    public class GridLineItemData : GridLineItemConifg
    {
        [XmlArray]
        [XmlArrayItem("DataItem")]
        public List<MatrixViewConfigItem> Datas { set; get; }
    }
}
