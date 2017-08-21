using System;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Tram.CBTC.Infrasturcture.Events;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Train.Brake;
using Tram.CBTC.Infrasturcture.Model.Train.Carriage;
using Tram.CBTC.Infrasturcture.Model.Train.Door;

namespace Tram.CBTC.Infrasturcture.Model.Train
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

        private bool m_DepotEntry;          //进入车辆段
        private bool m_DepotDriver;         //列车定位丢失
        private bool m_RelesaseSpeed;   //低速释放


        private bool m_BlackTimeEnable; //黑屏时显示时间

        private bool m_ScreenSaverEnable;
        private string m_TrainMarshalling;

        public TrainInfo()
        {
            BrakeInfo = new BrakeInfo();
            CarriageInfo = new CarriageInfo();
            DoorInfo = new DoorInfo();
            Device = new Device();
            System = new System();
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

        /// <summary>
        /// 编组
        /// </summary>
        public string TrainMarshalling
        {
            get { return m_TrainMarshalling; }
            set
            {
                if (value == m_TrainMarshalling)
                    return;
                m_TrainMarshalling = value;
                RaisePropertyChanged(() => TrainMarshalling);
            }
        }

        /// <summary>
        /// 列车长度
        /// </summary>
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
        /// 屏保使能通知事件
        /// </summary>
        public event Action<bool> ScreenSaverEnableChangedEvent;

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
                RaisePropertyChanged(() => ScreenSaverEnable);
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
        /// 系统
        /// </summary>
        public System System { get; private set; }
        /// <summary>
        /// 设备
        /// </summary>
        public Device Device { get; private set; }

    }
}