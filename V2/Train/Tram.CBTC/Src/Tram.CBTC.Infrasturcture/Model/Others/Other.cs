using System;
using Microsoft.Practices.Prism.ViewModel;

namespace Tram.CBTC.Infrasturcture.Model.Others
{
    /// <summary>
    /// 其他  包含 时间   声音  亮度  等
    /// </summary>
    public class Other : NotificationObject
    {
        private float m_Volumne;
        private float m_Light;
        private TimeSpan m_ShowingTimeDifference;
        private DateTime m_NowInCBTC;
        private bool m_DateVisible;
        private bool m_TimeVisible;
        private bool m_DateTimeTitleVisible;


        public Other()
        {
            Light = 100;
            Volumne = 50;
            ShowingTimeDifference = TimeSpan.Zero;
        }
        /// <summary>
        ///  声音  最大100  最小 0
        /// </summary>
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
                m_Volumne = tmp; ;
                RaisePropertyChanged(() => Volumne);
            }
        }
        /// <summary>
        /// 亮度  最大100  最小0
        /// </summary>
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
        /// <summary>
        /// 设置的时间差
        /// </summary>
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
        /// <summary>
        /// 信号模块给的时间
        /// </summary>
        public DateTime NowInCBTC
        {
            set
            {
                if (value.Equals(m_NowInCBTC))
                {
                    return;
                }
                m_NowInCBTC = value;
                RaisePropertyChanged(() => NowInCBTC);
                RaisePropertyChanged(() => ShowingDateTime);
            }
            get { return m_NowInCBTC; }
        }
        /// <summary>
        /// 显示的时间
        /// </summary>
        public DateTime ShowingDateTime { get { return NowInCBTC.Add(ShowingTimeDifference); } }

        /// <summary>
        /// 时间标题是否可见
        /// </summary>
        public bool DateTimeTitleVisible
        {
            get { return m_DateTimeTitleVisible; }
            set
            {
                if (value == m_DateTimeTitleVisible)
                {
                    return;
                }

                m_DateTimeTitleVisible = value;
                RaisePropertyChanged(() => DateTimeTitleVisible);
            }
        }

        /// <summary>
        /// 当前日期是否可见
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