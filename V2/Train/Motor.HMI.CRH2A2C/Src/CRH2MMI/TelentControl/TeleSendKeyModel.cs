using System.Diagnostics;
using System.Xml.Serialization;

namespace CRH2MMI.TelentControl
{
    [DebuggerDisplay("{Unit}U, Type={Type}")]
    public class TeleSendKeyModel 
    {
        [XmlAttribute]
        public TeleControlBtnType Type { set; get; }

        [XmlAttribute]
        public int Unit { set; get; }

        public override string ToString()
        {
            return string.Format("{0}U, Type={1}", Unit, Type);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }
    }
}
