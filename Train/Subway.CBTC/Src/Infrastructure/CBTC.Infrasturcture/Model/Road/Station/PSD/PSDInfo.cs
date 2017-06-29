using System;
using CBTC.Infrasturcture.Model.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Road.Station.PSD
{
    public class PSDInfo : NotificationObject
    {
        private bool m_IsTimeOut;
        private double m_StopStationTime;
        private DepartState m_DepartState;
        private StationParkingInfo m_StationParkingInfo;
        private double m_DepartSecond;

        /// <summary>
        /// 到站时间
        /// </summary>
        public DateTime ArriveTime { get; set; }

        /// <summary>
        /// 离站时间
        /// </summary>
        public DateTime DepartTime { get; set; }

        /// <summary>
        /// 发车时间  
        /// </summary>
        public double DepartSecond
        {
            get { return m_DepartSecond; }
            set
            {
                if (value.Equals(m_DepartSecond))
                    return;
                m_DepartSecond = value;
                RaisePropertyChanged(() => DepartSecond);
            }
        }

        /// <summary>
        /// 停站倒计时
        /// </summary>
        public double StopStationTime
        {
            get { return m_StopStationTime; }
            set
            {
                if (value.Equals(m_StopStationTime))
                    return;
                m_StopStationTime = value;
                RaisePropertyChanged(() => StopStationTime);
            }
        }

        /// <summary>
        /// 是都停站超时   true 超时  false 未超时
        /// </summary>
        public bool IsTimeOut
        {
            get { return m_IsTimeOut; }
            set
            {
                if (value == m_IsTimeOut)
                    return;
                m_IsTimeOut = value;
                RaisePropertyChanged(() => IsTimeOut);
            }
        }


        public StationParkingInfo StationParkingInfo
        {
            get { return m_StationParkingInfo; }
            set
            {
                if (value == m_StationParkingInfo)
                    return;
                m_StationParkingInfo = value;
                RaisePropertyChanged(() => StationParkingInfo);
            }
        }
    }
}