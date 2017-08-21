using System;
using Microsoft.Practices.Prism.ViewModel;

namespace Subway.ShenZhenLine11.Info
{
    public class EmergencyBordercastInfo : NotificationObject, ICloneable
    {
        private bool m_IsSelcet;
        public int Num { get; set; }
        public string Content { get; set; }

        public bool IsSelcet
        {
            get { return m_IsSelcet; }
            set
            {
                if (value == m_IsSelcet)
                {
                    return;
                }
                m_IsSelcet = value;
                if (value)
                {
                    OnSelectChanged(this);
                }
                RaisePropertyChanged(() => IsSelcet);
            }
        }

        public event Action<EmergencyBordercastInfo> SelectChanged;
        public object Clone()
        {
            var tmp = new EmergencyBordercastInfo
            {
                Num = this.Num,
                Content = this.Content
            };
            return tmp;
        }

        protected virtual void OnSelectChanged(EmergencyBordercastInfo obj)
        {
            SelectChanged?.Invoke(obj);
        }
    }
}