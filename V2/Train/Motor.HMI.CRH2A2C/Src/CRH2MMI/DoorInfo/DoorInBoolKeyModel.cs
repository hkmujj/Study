using System.Diagnostics;
using CRH2MMI.Common.Models;

namespace CRH2MMI.DoorInfo
{
    [DebuggerDisplay("CarNo={CarNo} OperType={OperType} Location={Location}")]
    class DoorInBoolKeyModel : DictionaryKeyModel
    {
        public int CarNo { set; get; }

        public DoorLocation Location { set; get; }

        public DoorOperType OperType { set; get; }

        public override string ToString()
        {
            return string.Format("{0}车厢,{1},{2}",CarNo+1, OperType, Location);
        }
    }
}
