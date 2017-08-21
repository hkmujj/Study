using System.Xml.Serialization;
using CRH2MMI.BreakLocked;

namespace CRH2MMI.AxisTemperature
{
    class AxisTemperatureSendKeyModel
    {
        [XmlAttribute]
        public int CarNo { set; get; }

        [XmlAttribute]
        public int AxisTemperaturedLocation { set; get; }

        [XmlAttribute]
        public CtrolType Type { set; get; }

        public override string ToString()
        {
            return string.Format("{0}车厢,轴温{1},{2}", CarNo, AxisTemperaturedLocation, Type);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ToString().GetHashCode().Equals(obj.ToString().GetHashCode());
        }
    }
}
