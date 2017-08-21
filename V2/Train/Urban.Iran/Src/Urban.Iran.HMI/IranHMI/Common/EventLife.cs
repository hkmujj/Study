using System;
using System.Diagnostics;
using CommonUtil.Util;
using Urban.Iran.HMI.Events;

namespace Urban.Iran.HMI.Common
{
    [DebuggerDisplay("EventKey={EventKey}, IsAcknownleaged={IsAcknownleaged}, EventInfo={EventInfo}")]
    public class EventLife
    {
        public int EventKey { private set; get; }

        public DateTime StartTime { private set; get; }

        public DateTime EndTime { set; get; }

        /// <summary>
        /// 是否已回应
        /// </summary>
        public bool IsAcknownleaged { set; get; }

        public EventInfo EventInfo { private set; get; }

        [DebuggerStepThrough]
        public EventLife(int eventKey, DateTime startTime)
        {
            EventKey = eventKey;
            StartTime = startTime;
            EndTime = startTime;
            IsAcknownleaged = false;

            if (EventManager.Instance.AllEvent.ContainsKey(eventKey))
            {
                EventInfo = EventManager.Instance.AllEvent[eventKey];
            }
            else
            {
                LogMgr.Warn(string.Format("Can not found the event info where the event code is {0}", eventKey));
            }
        }
    }
}