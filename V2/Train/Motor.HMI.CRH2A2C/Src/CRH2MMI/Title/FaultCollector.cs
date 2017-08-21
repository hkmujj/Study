using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtil.Util;

namespace CRH2MMI.Title
{
    internal class FaultCollector
    {
        public bool HasAnyFault
        {
            get { return m_FaultStateDic.Any(a => a.Value); }
        }

        public EventHandler FaultStateChaned;

        private readonly Dictionary<string, bool> m_FaultStateDic;

        public FaultCollector()
        {
            m_FaultStateDic = new Dictionary<string, bool>();
        }

        public void Clear()
        {
            m_FaultStateDic.Clear();
        }

        public void AddOrRefresh(string name, bool state)
        {
            if  ((m_FaultStateDic.ContainsKey(name) &&  m_FaultStateDic[name] != state)|| !m_FaultStateDic.ContainsKey(name))
            {
                m_FaultStateDic[name] = state;
                HandleUtil.OnHandle(FaultStateChaned, this, null);
            }
            
        }

    }
}
