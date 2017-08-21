using System;
using System.Collections.Generic;

namespace Urban.QingDao3Line.MMI
{
    class EventMsg<T>where T:BaseEvent
    {
        private readonly List<T> m_EventList;

        public List<T> EventLsit
        {
            get { return m_EventList; }
        }

        public EventMsg()
        {
            m_EventList = new List<T>();
        }

        public void AddEvent(T theEvent, DateTime theTime)
        {
            theEvent.EventTime = theTime;
            m_EventList.Add(theEvent);
        }

        public void EventOver(int theLogicId,DateTime overTime)
        {
            try
            {
                foreach (T temp in m_EventList)
                {
                    if (temp.EventLogicId != theLogicId) continue;
                    m_EventList.Remove(temp);
                }
            }
            catch //(Exception ex)
            {

            }
           
        }
    }
}
