using System;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Road.Station;

namespace Tram.CBTC.Infrasturcture.Model.Road
{
    public class RoadInfo : NotificationObject
    {
        private string m_DesID;
        private string m_TrainID;
        private string m_DriverID;
        private ReturnInfo m_ReturnState;
        private string m_TrainNum;
        private string m_RoadID;
        private string m_OneWayID;
        private string m_LineID;
        private string m_ForwardTagrt;
        private LineRunDirection m_LineRunDirection = LineRunDirection.Up;
        private string m_PlanID;
        public ReturnIndicateInfo m_ReturnIndicateInfo;
        public GarageIndicateInfo m_GarageIndicateInfo;


        public RoadInfo()
        {
            StationInfo = new StationInfo();
            ReturnIndicateInfo = new ReturnIndicateInfo();
            GarageIndicateInfo = new GarageIndicateInfo();
        }


        /// <summary>
        /// 车次号使能通知事件
        /// </summary>
        public event Action TrainIDChangedEvent;


        public void OnTrainIDChangedEvent()
        {
            var handler = TrainIDChangedEvent;
            if (handler != null)
            {
                handler();
            }
        }


        /// <summary>
        /// 列车运行方向状态使能通知事件
        /// </summary>
        public event Action LineRunDirectionStatusChangedEvent;


        public void OnLineRunDirectionStatusChangedEvent()
        {
            var handler = LineRunDirectionStatusChangedEvent;
            if (handler != null)
            {
                handler();
            }
        }


        public StationInfo StationInfo { get; protected set; }

        /// <summary>
        /// 路径号
        /// </summary>
        public string RoadID
        {
            get { return m_RoadID; }
            set
            {
                if (value == m_RoadID)
                    return;
                m_RoadID = value;
                RaisePropertyChanged(() => RoadID);
            }
        }

        /// <summary>
        /// 线路号
        /// </summary>
        public string LineID
        {
            get { return m_LineID; }
            set
            {
                if (value == m_LineID)
                    return;
                m_LineID = value;
                RaisePropertyChanged(() => LineID);
            }
        }

        /// <summary>
        /// 单程号
        /// </summary>
        public string OneWayID
        {
            get { return m_OneWayID; }
            set
            {
                if (value == m_OneWayID)
                    return;
                m_OneWayID = value;
                RaisePropertyChanged(() => OneWayID);
            }
        }

        /// <summary>
        /// 折返信息
        /// </summary>
        public ReturnInfo ReturnState
        {
            get { return m_ReturnState; }
            set
            {
                if (value == m_ReturnState)
                    return;

                m_ReturnState = value;
                RaisePropertyChanged(() => ReturnState);
            }
        }

        /// <summary>
        /// 司机号
        /// </summary>
        public string DriverID
        {
            get { return m_DriverID; }
            set
            {
                if (value == m_DriverID)
                    return;

                m_DriverID = value;
                RaisePropertyChanged(() => DriverID);
            }
        }

        /// <summary>
        /// 列车号
        /// </summary>
        public string TrainNum
        {
            get { return m_TrainNum; }
            set
            {
                if (value == m_TrainNum)
                    return;
                m_TrainNum = value;
                RaisePropertyChanged(() => TrainNum);
            }
        }

        /// <summary>
        /// 车次号
        /// </summary>
        public string TrainID
        {
            get { return m_TrainID; }
            set
            {
                if (value == m_TrainID)
                    return;

                m_TrainID = value;
                OnTrainIDChangedEvent();
                RaisePropertyChanged(() => TrainID);
            }
        }

        /// <summary>
        /// 目的地
        /// </summary>
        public string DesID
        {
            get { return m_DesID; }
            set
            {
                if (value == m_DesID)
                    return;

                m_DesID = value;
                RaisePropertyChanged(() => DesID);
            }
        }

        /// <summary>
        /// 前方目标
        /// </summary>
        public string ForwardTagrt
        {
            get { return m_ForwardTagrt; }
            set
            {
                if (value == m_ForwardTagrt)
                    return;
                m_ForwardTagrt = value;
                RaisePropertyChanged(() => ForwardTagrt);
            }
        }

        /// <summary>
        /// 计划号
        /// </summary>
        public string PlanID
        {
            get { return m_PlanID; }
            set
            {
                if (value == m_PlanID)
                    return;

                m_PlanID = value;
                RaisePropertyChanged(() => PlanID);
            }
        }
     
        /// <summary>
        /// 线路运行方向
        /// </summary>
        public LineRunDirection LineRunDirection
        {
            get { return m_LineRunDirection; }
            set
            {
                if (value == m_LineRunDirection)
                    return;

                m_LineRunDirection = value;

                OnLineRunDirectionStatusChangedEvent();

                RaisePropertyChanged(() => LineRunDirection);
            }
        }

        /// <summary>
        /// 折返指示信息
        /// </summary>
        public ReturnIndicateInfo ReturnIndicateInfo
        {
            get { return m_ReturnIndicateInfo; }
            set
            {
                if (Equals(value, m_ReturnIndicateInfo))
                    return;

                m_ReturnIndicateInfo = value;
                m_ReturnIndicateInfo.PropertyChanged +=
                    (sender, args) => RaisePropertyChanged(() => ReturnIndicateInfo);

                RaisePropertyChanged(() => ReturnIndicateInfo);
            }
        }

        /// <summary>
        /// 进出车库指示信息
        /// </summary>
        public GarageIndicateInfo GarageIndicateInfo
        {
            get { return m_GarageIndicateInfo; }
            set
            {
                if (Equals(value, m_GarageIndicateInfo))
                    return;

                m_GarageIndicateInfo = value;
                m_GarageIndicateInfo.PropertyChanged +=
                    (sender, args) => RaisePropertyChanged(() => GarageIndicateInfo);

                RaisePropertyChanged(() => GarageIndicateInfo);
            }
        }
    }
}