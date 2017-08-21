using System;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Running
{
    public class RunningInfo : NotificationObject
    {
        private TrainPosition m_TrainPosition;
        private RunDirection m_RunDirection;
        private TrainSoonerOrLaterStatus m_TrainSoonerOrLaterStatus;
        private string m_TrainSoonerOrLaterTime;
        private VehicleRunningModel m_VehicleRunningModel;
        private string m_VehicleOnlineEqualintervalTime;

        /// <summary>
        /// 列车当前位置
        /// </summary>
        public TrainPosition TrainPosition
        {
            get { return m_TrainPosition; }
            set
            {
                if (value == m_TrainPosition)
                    return;

                m_TrainPosition = value;
                RaisePropertyChanged(() => TrainPosition);
            }
        }
        /// <summary>
        /// 运行方向
        /// </summary>
        public RunDirection RunDirection
        {
            get { return m_RunDirection; }
            set
            {
                if (value == m_RunDirection)
                    return;
                m_RunDirection = value;
                RaisePropertyChanged(() => RunDirection);
            }
        }

        /// <summary>
        /// 车载运行模式
        /// </summary>
        public VehicleRunningModel VehicleRunningModel
        {
            get { return m_VehicleRunningModel; }
            set
            {
                if (value == m_VehicleRunningModel)
                    return;

                m_VehicleRunningModel = value;
                RaisePropertyChanged(() => VehicleRunningModel);
            }
        }

        /// <summary>
        /// 等间隔时间
        /// </summary>
        public string VehicleOnlineEqualintervalTime
        {
            get { return m_VehicleOnlineEqualintervalTime; }
            set
            {
                if (value == m_VehicleOnlineEqualintervalTime)
                    return;

                m_VehicleOnlineEqualintervalTime = value;
                RaisePropertyChanged(() => VehicleOnlineEqualintervalTime);
            }
        }

        /// <summary>
        /// 列车早晚点状态
        /// </summary>
        public TrainSoonerOrLaterStatus TrainSoonerOrLaterStatus
        {
            get { return m_TrainSoonerOrLaterStatus; }
            set
            {
                if (value == m_TrainSoonerOrLaterStatus)
                    return;

                m_TrainSoonerOrLaterStatus = value;
                RaisePropertyChanged(() => TrainSoonerOrLaterStatus);
            }
        }

        /// <summary>
        /// 列车早晚点时间
        /// </summary>
        public string TrainSoonerOrLaterTime
        {
            get { return m_TrainSoonerOrLaterTime; }
            set
            {
                if (value.Equals(m_TrainSoonerOrLaterTime))
                    return;

                m_TrainSoonerOrLaterTime = value;
                RaisePropertyChanged(() => TrainSoonerOrLaterTime);
            }
        }
    }
}