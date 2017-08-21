using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Urban.GuiYang.DDU.Model.Train
{
    [Export]
    public class OtherModel : NotificationObject
    {
        private float m_Volumne;
        private float m_Light;
        private TimeSpan m_ShowingTimeDifference;
        private DateTime m_NowInDDU;
        private bool m_DateVisible;
        private bool m_TimeVisible;

        public float Volumne
        {
            get { return m_Volumne; }
            set
            {
                var tmp = Math.Max(0, Math.Min(100, value));
                if (tmp.Equals(m_Volumne))
                {
                    return;
                }

                m_Volumne = tmp;
                ;
                RaisePropertyChanged(() => Volumne);
            }
        }

        public float Light
        {
            get { return m_Light; }
            set
            {
                var tmp = Math.Max(0, Math.Min(100, value));
                if (tmp.Equals(m_Light))
                {
                    return;
                }

                m_Light = tmp;
                RaisePropertyChanged(() => Light);
            }
        }

        public TimeSpan ShowingTimeDifference
        {
            get { return m_ShowingTimeDifference; }
            set
            {
                if (value.Equals(m_ShowingTimeDifference))
                {
                    return;
                }

                m_ShowingTimeDifference = value;
                if (m_ShowingTimeDifference == null)
                {
                    m_ShowingTimeDifference = TimeSpan.Zero;
                }
                RaisePropertyChanged(() => ShowingTimeDifference);
                RaisePropertyChanged(() => ShowingDateTime);
            }
        }

        public DateTime NowInDDU
        {
            set
            {
                if (value.Equals(m_NowInDDU))
                {
                    return;
                }

                m_NowInDDU = value;
                RaisePropertyChanged(() => NowInDDU);
                RaisePropertyChanged(() => ShowingDateTime);
            }
            get { return m_NowInDDU; }
        }

        public DateTime ShowingDateTime
        {
            get { return NowInDDU.Add(ShowingTimeDifference); }
        }

        /// <summary>
        /// 当前时间是否可见
        /// </summary>
        public bool DateVisible
        {
            get { return m_DateVisible; }
            set
            {
                if (value == m_DateVisible)
                {
                    return;
                }

                m_DateVisible = value;
                RaisePropertyChanged(() => DateVisible);
            }
        }

        /// <summary>
        /// 当前时间是否可见
        /// </summary>
        public bool TimeVisible
        {
            get { return m_TimeVisible; }
            set
            {
                if (value == m_TimeVisible)
                {
                    return;
                }

                m_TimeVisible = value;
                RaisePropertyChanged(() => TimeVisible);
            }
        }
    }
}