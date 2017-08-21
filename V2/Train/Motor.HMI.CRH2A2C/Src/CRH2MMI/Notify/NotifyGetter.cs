using System;
using System.Linq;

namespace CRH2MMI.Notify
{
    class NotifyGetter
    {
        private NotifyManager m_NotifyManager;
        private bool m_HasResponse;

        public event Action<NotifyGetter> HasResponseHasChanged;

        public object Tag { set; get; }

        /// <summary>
        /// 有通知
        /// </summary>
        public bool HasNotify() { return m_NotifyManager.NotifyDictionary.Any(a => a.Value.Any(an => an.HasOccur)); }

        /// <summary>
        /// 有通知
        /// </summary>  
        public bool HasNotify(NotifyType type)
        {
            return m_NotifyManager.NotifyDictionary[type].Any(a => a.HasOccur);
        }

        public bool HasResponse
        {
            set
            {
                var old = m_HasResponse;
                m_HasResponse = value;
                if (m_HasResponse != old && HasResponseHasChanged != null)
                {
                    HasResponseHasChanged(this);
                }
                
            }
            get { return m_HasResponse; }
        }
    }
}
