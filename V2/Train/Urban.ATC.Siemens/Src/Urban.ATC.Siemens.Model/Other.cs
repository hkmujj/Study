using System;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.Service;

namespace Motor.ATP.Domain.Model
{
    public class Other : ATPPartialBase, IOther
    {
        private float m_Volumne;
        private float m_Light;
        private readonly IOpaqueLayerService m_OpaqueLayerService;
        private TimeSpan m_ShowingTimeDifference;
        private DateTime m_NowInATP;
        private bool m_DateVisible;
        private bool m_TimeVisible;


        public Other(ATPDomain parent)
            : base(parent)
        {
            m_OpaqueLayerService = parent.ServiceManager.GetService<IOpaqueLayerService>();
            Light = 100;
            Volumne = 50;
            ShowingTimeDifference = TimeSpan.Zero;
        }

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
                m_OpaqueLayerService.LightPencent = Light / 100;
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

        public DateTime NowInATP
        {
            set
            {
                if (value.Equals(m_NowInATP))
                {
                    return;
                }
                m_NowInATP = value;
                RaisePropertyChanged(() => NowInATP);
                RaisePropertyChanged(() => ShowingDateTime);
            }
            get { return m_NowInATP; }
        }

        public DateTime ShowingDateTime { get { return NowInATP.Add(ShowingTimeDifference); } }

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