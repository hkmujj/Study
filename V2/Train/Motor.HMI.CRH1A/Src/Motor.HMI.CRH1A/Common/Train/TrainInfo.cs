using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonUtil.Util;
using Motor.HMI.CRH1A.Common.EventArg;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Common.Train
{
    class TrainInfo
    {
        private float m_Speed;
        private TrainState m_TrainState;
        private DriverLocation m_DriverLocation;
        private float m_MaxSpeed;

        public const float DefaultMaxSpeed = 1000;

        public TrainInfo()
        {
            Speed = 0;
            MaxSpeed = DefaultMaxSpeed;
            DriverLocation = DriverLocation.Unkown;
        }

        /// <summary>
        /// 带单位的最大速度 
        /// </summary>
        public string MaxSpeedText { private set; get; }

        public float MaxSpeed
        {
            set
            {
                m_MaxSpeed = value;
                if (value < 0 || Math.Abs(value) > DefaultMaxSpeed)
                {
                    LogMgr.Warn(string.Format("The max speed must rang in [0,{1}], force let input max speed {0} to {1}", value, DefaultMaxSpeed));
                    m_MaxSpeed = Math.Min(Math.Abs(value), DefaultMaxSpeed);
                }
                MaxSpeedText = string.Format("{0} KM/H", m_MaxSpeed);
            }
            get { return m_MaxSpeed; }
        }

        /// <summary>
        /// 速度 
        /// </summary>
        public float Speed
        {
            set
            {
                m_Speed = value;
                AbsSpeed = Math.Abs(m_Speed);
                if (AbsSpeed > MaxSpeed)
                {
                    LogMgr.Info(string.Format("speed ={0} is larger than max = {1}, force let speed is {1}.", AbsSpeed, MaxSpeed));
                    m_Speed = MaxSpeed;
                    AbsSpeed = MaxSpeed;
                }
                if (float.Epsilon > AbsSpeed - 0f)
                {
                    TrainState = TrainState.Stop;
                }
                else if (m_Speed > 0)
                {
                    TrainState = TrainState.Forward;
                }
                else
                {
                    TrainState = TrainState.Backwards;
                }
            }
            get { return m_Speed; }
        }

        /// <summary>
        /// 绝对速度
        /// </summary>
        public float AbsSpeed { private set; get; }

        /// <summary>
        /// 列车状态
        /// </summary>
        public TrainState TrainState
        {
            set
            {
                var old = m_TrainState;
                m_TrainState = value;
                if (old != value)
                {
                    HandleUtil.OnHandle(GlobalEvent.Instance.TrainStateChanged, this, new TrainStateChangedArgs(old, m_TrainState));                    
                }
            }
            get { return m_TrainState; }
        }

        public DriverLocation DriverLocation
        {
            set
            {
                var old = m_DriverLocation;
                m_DriverLocation = value;
                if (old != value)
                {
                    HandleUtil.OnHandle(GlobalEvent.Instance.DriverLocationChanged,
                        this,
                        new EventArgs<ChangedArgs<DriverLocation>>(new ChangedArgs<DriverLocation>(old, value)));
                }
            }
            get { return m_DriverLocation; }
        }
    }
}
