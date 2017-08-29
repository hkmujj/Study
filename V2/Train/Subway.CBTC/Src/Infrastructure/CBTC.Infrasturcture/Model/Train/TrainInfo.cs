using System;
using CBTC.Infrasturcture.Events;
using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Train.Brake;
using CBTC.Infrasturcture.Model.Train.Carriage;
using CBTC.Infrasturcture.Model.Train.Door;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace CBTC.Infrasturcture.Model.Train
{
    public class TrainInfo : NotificationObject
    {
        private DoorInfo m_DoorInfo;
        private CompleteState m_CompleteState;
        private CarriageInfo m_CarriageInfo;
        private BrakeInfo m_BrakeInfo;
        private string m_TrainLength;
        private WorkState m_WorkState;
        private PowerState m_PowerState;
        private TrainEquipment m_TrainEquipment;
        private bool m_DepotEntry;          //进入车辆段
        private bool m_DepotDriver;         //列车定位丢失
        private bool m_RelesaseSpeed;   //低速释放
        private RealTimeWokeStatus m_RealTimeWokeStatus;
        private double m_VOBCNumber;
        private bool m_BlackTimeEnable; //黑屏时显示时间
        private BlackText m_BlackText;  //黑屏时显示文本
        private bool m_ScreenSaverEnable;
        private RATOState m_RATOState;
        private bool m_ScreenSaverBlackEnable;


        public TrainInfo()
        {
            BrakeInfo = new BrakeInfo();
            CarriageInfo = new CarriageInfo();
            DoorInfo = new DoorInfo();
        }

        public PowerState PowerState
        {
            get { return m_PowerState; }
            set
            {
                if (value == m_PowerState)
                    return;

                ServiceLocator.Current.GetInstance<IEventAggregator>()
                    .GetEvent<PowerStateChangedEvent>()
                    .Publish(new PowerStateChangedEvent.Args(value));

                m_PowerState = value;
                RaisePropertyChanged(() => PowerState);
            }
        }

        /// <summary>
        /// 工况
        /// </summary>
        public WorkState WorkState
        {
            get { return m_WorkState; }
            set
            {
                if (value == m_WorkState)
                    return;

                m_WorkState = value;
                RaisePropertyChanged(() => WorkState);
            }
        }

        public string TrainLength
        {
            get { return m_TrainLength; }
            set
            {
                if (value == m_TrainLength)
                    return;

                m_TrainLength = value;
                RaisePropertyChanged(() => TrainLength);
            }
        }

        public BrakeInfo BrakeInfo
        {
            get { return m_BrakeInfo; }
            protected set
            {
                if (Equals(value, m_BrakeInfo))
                    return;

                m_BrakeInfo = value;
                RaisePropertyChanged(() => BrakeInfo);
            }
        }

        /// <summary>
        /// 车厢信息
        /// </summary>
        public CarriageInfo CarriageInfo
        {
            get { return m_CarriageInfo; }
            private set
            {
                if (Equals(value, m_CarriageInfo))
                    return;

                m_CarriageInfo = value;
                RaisePropertyChanged(() => CarriageInfo);
            }
        }

        /// <summary>
        /// 列车完整性
        /// </summary>
        public CompleteState CompleteState
        {
            get { return m_CompleteState; }
            set
            {
                if (value == m_CompleteState)
                    return;

                m_CompleteState = value;
                RaisePropertyChanged(() => CompleteState);
            }
        }

        /// <summary>
        /// 车门信息
        /// </summary>
        public DoorInfo DoorInfo
        {
            get { return m_DoorInfo; }
            set
            {
                if (Equals(value, m_DoorInfo))
                    return;

                m_DoorInfo = value;
                RaisePropertyChanged(() => DoorInfo);
            }
        }

        /// <summary>
        /// 列车头尾设备状态
        /// </summary>
        public TrainEquipment TrainEquipment
        {
            get { return m_TrainEquipment; }
            set
            {
                if (value == m_TrainEquipment)
                    return;

                m_TrainEquipment = value;
                RaisePropertyChanged(() => TrainEquipment);
            }
        }

        /// <summary>
        /// 进入车辆段
        /// </summary>
        public bool DepotEntry
        {
            get { return m_DepotEntry; }
            set
            {
                if (value == m_DepotEntry)
                    return;

                m_DepotEntry = value;
                RaisePropertyChanged(() => DepotEntry);
            }
        }

        /// <summary>
        /// 定位丢失
        /// </summary>
        public bool DepotDriver
        {
            get { return m_DepotDriver; }
            set
            {
                if (value == m_DepotDriver)
                    return;

                m_DepotDriver = value;
                RaisePropertyChanged(() => DepotDriver);
            }
        }

        /// <summary>
        /// 低速释放
        /// </summary>
        public bool RelesaseSpeed
        {
            get { return m_RelesaseSpeed; }
            set
            {
                if (value == m_RelesaseSpeed)
                    return;

                m_RelesaseSpeed = value;
                RaisePropertyChanged(() => RelesaseSpeed);
            }
        }

        /// <summary>
        /// 车辆实时工况
        /// </summary>
        public RealTimeWokeStatus RealTimeWokeStatus
        {
            get { return m_RealTimeWokeStatus; }
            set
            {
                if (value == m_RealTimeWokeStatus)
                    return;

                m_RealTimeWokeStatus = value;
                RaisePropertyChanged(() => RealTimeWokeStatus);
            }
        }

        /// <summary>
        /// VOBC 个数
        /// </summary>
        public double VOBCNumber
        {
            get { return m_VOBCNumber; }
            set
            {
                if (value.Equals(m_VOBCNumber))
                    return;
                m_VOBCNumber = value;
                RaisePropertyChanged(() => VOBCNumber);
            }
        }

        /// <summary>
        /// 屏保使能通知事件
        /// </summary>
        //public event Action<bool> ScreenSaverEnableChangedEvent;

        /// <summary>
        /// 屏保
        /// </summary>
        public bool ScreenSaverEnable
        {
            get { return m_ScreenSaverEnable; }
            set
            {
                if (value.Equals(m_ScreenSaverEnable))
                    return;
                m_ScreenSaverEnable = value;
                //OnScreenSaverEnableChangedEvent(value);
                ServiceLocator.Current.GetInstance<IEventAggregator>()
                    .GetEvent<ScreenSaverEnableChangedEvent>()
                    .Publish(new ScreenSaverEnableChangedEvent.Args(value));
                RaisePropertyChanged(() => ScreenSaverEnable);
            }
        }

        /// <summary>
        /// 黑屏屏保
        /// </summary>
        public bool ScreenSaverBlackEnable
        {
            get { return m_ScreenSaverBlackEnable; }
            set
            {
                if (value.Equals(m_ScreenSaverBlackEnable))
                    return;
                m_ScreenSaverBlackEnable = value;
                RaisePropertyChanged(() => ScreenSaverBlackEnable);
            }
        }

        /// <summary>
        /// 黑屏时显示时间
        /// </summary>
        public bool BlackTimeEnable
        {
            get { return m_BlackTimeEnable; }
            set
            {
                if (value.Equals(m_BlackTimeEnable))
                    return;
                m_BlackTimeEnable = value;
                RaisePropertyChanged(() => BlackTimeEnable);
            }
        }

        /// <summary>
        /// 黑屏时显示文本
        /// </summary>
        public BlackText BlackText
        {
            get { return m_BlackText; }
            set
            {
                if (value.Equals(m_BlackText))
                    return;
                m_BlackText = value;
                RaisePropertyChanged(() => BlackText);
            }
        }

//        protected virtual void OnScreenSaverEnableChangedEvent(bool obj)
//        {
//            var handler = ScreenSaverEnableChangedEvent;
//            if (handler != null)
//                handler(obj);
//        }

        /// <summary>
        /// RATO状态
        /// </summary>
        public RATOState RATOState
        {
            get { return m_RATOState; }
            set
            {
                if (value.Equals(m_RATOState))
                    return;
                m_RATOState = value;
                RaisePropertyChanged(() => RATOState);
            }
        }
        
    }
}