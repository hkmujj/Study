using System;
using System.Diagnostics;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Fault.Detail
{
    [DebuggerDisplay("Content={InfoConfig.Content}")]
    public class FaultItem : NotificationObject
    {
        private DateTime m_ResetTime;

        [DebuggerStepThrough]
        public FaultItem(NotifyInfoConfig infoConfig, DateTime occuseTime, WorkState workState, double rotationRate, double speed, double param)
        {
            InfoConfig = infoConfig;
            OccurseTime = occuseTime;
            WorkState = workState;
            RotationRate = rotationRate;
            Speed = speed;
            Param = param;
            FaultState = FaultState.Occursed;
        }

        public NotifyInfoConfig InfoConfig { get; private set; }

        public DateTime OccurseTime { private set; get; }

        public DateTime ResetTime
        {
            get { return m_ResetTime; }
            set
            {
                if (value.Equals(m_ResetTime))
                {
                    return;
                }

                m_ResetTime = value;
                RaisePropertyChanged(() => ResetTime);
            }
        }

        public WorkState WorkState { private set; get; }

        public double Param { get; private set; }

        public double RotationRate { get; private set; }

        public double Speed { get; private set; }

        public FaultState FaultState { get; set; }
    }
}