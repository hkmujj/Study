using System;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Road.Station.PSD
{
    public class PSDInfo : NotificationObject
    {
        private StationControlCarStatus m_StationControlCarStatus;
        private float m_StopStationTimeSec;
        private CBTCColor m_StopStationTimeColor;
        private bool m_StopStationVisible;
        private bool m_StopStationFlicker;
        private DateTime m_DepartTime;
        private DateTime m_ArriveTime;

        /// <summary>
        /// 到站时间
        /// </summary>
        public DateTime ArriveTime
        {
            get { return m_ArriveTime; }
            set
            {
                if (value.Equals(m_ArriveTime))
                {
                    return;
                }
                m_ArriveTime = value;
                RaisePropertyChanged(() => ArriveTime);
            }
        }

        /// <summary>
        /// 发车时间
        /// </summary>
        public DateTime DepartTime
        {
            get { return m_DepartTime; }
            set
            {
                if (value.Equals(m_DepartTime))
                {
                    return;
                }
                m_DepartTime = value;
                RaisePropertyChanged(() => DepartTime);
            }
        }

        /// <summary>
        /// 停站时间   单位 s
        /// </summary>
        public float StopStationTimeSec
        {
            get { return m_StopStationTimeSec; }
            set
            {
                if (Equals(value,m_StopStationTimeSec))
                    return;
                m_StopStationTimeSec = value;
                RaisePropertyChanged(() => StopStationTimeSec);
            }
        }
        /// <summary>
        /// 停站倒计时颜色
        /// </summary>
        public CBTCColor StopStationTimeColor
        {
            get { return m_StopStationTimeColor; }
            set
            {
                if (value == m_StopStationTimeColor)
                    return;
                m_StopStationTimeColor = value;
                RaisePropertyChanged(() => StopStationTimeColor);
            }
        }
        /// <summary>
        /// 停站倒计时显示
        /// </summary>
        public bool StopStationVisible
        {
            get { return m_StopStationVisible; }
            set
            {
                if (value == m_StopStationVisible)
                    return;
                m_StopStationVisible = value;
                RaisePropertyChanged(() => StopStationVisible);
            }
        }

        /// <summary>
        /// 停站倒计时闪烁
        /// </summary>
        public bool StopStationFlicker
        {
            get { return m_StopStationFlicker; }
            set
            {
                if (value == m_StopStationFlicker)
                    return;
                m_StopStationFlicker = value;
                RaisePropertyChanged(() => StopStationFlicker);
            }
        }

        /// <summary>
        /// 车站控车状态
        /// </summary>
        public StationControlCarStatus StationControlCarStatus
        {
            get { return m_StationControlCarStatus; }
            set
            {
                if (value == m_StationControlCarStatus)
                    return;
                m_StationControlCarStatus = value;
                RaisePropertyChanged(() => StationControlCarStatus);
            }
        }


    }
}