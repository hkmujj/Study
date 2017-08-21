using System;

namespace Subway.ShiJiaZhuangLine1.Interface
{
    public class EventInfo : IEventInfo
    {

        public int LogicId { get; set; }
        public int Number { get; set; }
        public string FaultCode { get; set; }
        public EventLevel Level { get; set; }
        public string Description { get; set; }
        public string System { get; set; }
        public string CarNumber { get; set; }
        public bool IsCofirm { get; set; }
        public DateTime HappenDateTime { get; set; }
        public DateTime CofirmDateTime { get; set; }
        public string Handbook { get; set; }
        public string Guideline { get; set; }
        public int CompareTo(IEventInfo other)
        {
            if (HappenDateTime.CompareTo(other.HappenDateTime) == 0)
            {
                return Number.CompareTo(other.Number) == 0 ? Level.CompareTo(other.Level) : Number.CompareTo(other.Number);
            }
            else
            {
                return HappenDateTime.CompareTo(other.HappenDateTime);
            }
        }

        public object Clone()
        {
            var tmp = new EventInfo();

            tmp.CarNumber = CarNumber;
            tmp.LogicId = LogicId;
            tmp.FaultCode = FaultCode;
            tmp.Level = Level;
            tmp.Description = Description;
            tmp.System = System;
            tmp.Handbook = Handbook;
            tmp.Guideline = Guideline;
            return tmp;


        }
    }
}