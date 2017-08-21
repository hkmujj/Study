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
        private EmergencyInfo m_EmergencyInfo;
        private bool m_StopStationTimeVisibility;
        private DateTime m_DepartTime;
        private bool m_DepartTimeVisible;
        private bool m_DepartSecondVisble;

        /// <summary>
        /// 到站时间
        /// </summary>
        public DateTime ArriveTime { get; set; }

        /// <summary>
        /// 离站时间
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
        /// 离站时间显示
        /// </summary>
        public bool DepartTimeVisible
        {
            get { return m_DepartTimeVisible; }
            set
            {
                if (value == m_DepartTimeVisible)
                    return;

                m_DepartTimeVisible = value;
                RaisePropertyChanged(() => DepartTimeVisible);
            }
        }

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
        /// 发车时间显示
        /// </summary>
        public bool DepartSecondVisble
        {
            get { return m_DepartSecondVisble; }
            set
            {
                if (value.Equals(m_DepartSecondVisble))
                    return;

                m_DepartSecondVisble = value;
                RaisePropertyChanged(() => DepartSecondVisble);
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
        /// 停站倒计时是否显示
        /// </summary>
        public bool StopStationTimeVisibility
        {
            get { return m_StopStationTimeVisibility; }
            set
            {
                if (value == m_StopStationTimeVisibility)
                    return;

                m_StopStationTimeVisibility = value;
                RaisePropertyChanged(() => StopStationTimeVisibility);
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

        /// <summary>
        /// 紧急信息
        /// </summary>
        public EmergencyInfo EmergencyInfo
        {
            get { return m_EmergencyInfo; }
            set
            {
                if (value == m_EmergencyInfo) return;
                m_EmergencyInfo = value;
                RaisePropertyChanged(() => EmergencyInfo);
            }
        }

    }
}