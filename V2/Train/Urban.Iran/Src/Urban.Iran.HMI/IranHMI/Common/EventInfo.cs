using System;
using System.Diagnostics;
using Urban.Iran.HMI.Events;

namespace Urban.Iran.HMI.Common
{
    [DebuggerDisplay("Type={Priority}, LogicIndex={LogicIndex}, EventCode={EventCode}")]
    public class EventInfo
    {
        public EventInfo(int logicIndex, EventPriority type, int eventCode, EventGroup group, string description,
            string msgPage1, string msgPage2, string carName)
        {
            CarName = carName;
            MsgPage2 = msgPage2;
            MsgPage1 = msgPage1;
            Description = description;
            Group = group;
            EventCode = eventCode;
            Priority = type;

            Color = EventColor.White;
            switch (Priority)
            {
                case EventPriority.Unkown:
                    break;
                case EventPriority.Information:
                    break;
                case EventPriority.FireAlarm:
                case EventPriority.Fault:
                case EventPriority.AAlarm:
                    // ºì
                    Color = EventColor.Red;
                    break;
                case EventPriority.BAlarm:
                    Color = EventColor.Yellow;
                    break;
                case EventPriority.ManualFault:
                    break;
                case EventPriority.Event:
                    Color = EventColor.White;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            LogicIndex = logicIndex;
        }

        public int LogicIndex { private set; get; }
        public EventPriority Priority { private set; get; }
        public int EventCode { private set; get; }
        public string CarName { private set; get; }
        public EventColor Color { private set; get; }
        public EventGroup Group { private set; get; }
        public string Description { private set; get; }
        public string MsgPage1 { private set; get; }
        public string MsgPage2 { private set; get; }
    }
}