using System;
using System.ComponentModel.Composition;
using System.Timers;
using Engine.LCDM.HDX2.Entity.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    public class WorkFlags : NotificationObject
    {
        private bool m_LowTotalAir;
        private bool m_AirAndEleInterlock;
        private bool m_EmergenceBrake;
        private int m_EmergenceTime;

        public const int MaxEmergenceTime = 60;

        private readonly Timer m_UpdateEmergenceTime;

        private readonly IEventAggregator m_EventAggregator;

        private CompositePresentationEvent<EmergenceTimeUpEventArg> EmergenceTimeUpEvent
        {
            get { return m_EventAggregator.GetEvent<CompositePresentationEvent<EmergenceTimeUpEventArg>>(); }
        }

        public int EmergenceTime
        {
            private set
            {
                if (value == m_EmergenceTime)
                {
                    return;
                }
                m_EmergenceTime = value;
                RaisePropertyChanged(() => EmergenceTime);
            }
            get { return m_EmergenceTime; }
        }

        public bool LowTotalAir
        {
            set
            {
                if (value == m_LowTotalAir)
                {
                    return;
                }
                m_LowTotalAir = value;
                RaisePropertyChanged(() => LowTotalAir);
            }
            get { return m_LowTotalAir; }
        }

        public bool EmergenceBrake
        {
            set
            {
                if (value == m_EmergenceBrake)
                {
                    return;
                }
                m_EmergenceBrake = value;
                if (value)
                {
                    EmergenceTime = MaxEmergenceTime;
                    m_UpdateEmergenceTime.Start();
                    EmergenceTimeUpEvent.Publish(new EmergenceTimeUpEventArg(EmergenceTimerState.Counting));
                }
                else
                {
                    m_UpdateEmergenceTime.Stop();
                    EmergenceTimeUpEvent.Publish(new EmergenceTimeUpEventArg(EmergenceTimerState.Stopped));
                }
                RaisePropertyChanged(() => EmergenceBrake);
            }
            get { return m_EmergenceBrake; }
        }

        /// <summary>
        /// 空电联锁阀
        /// </summary>
        public bool AirAndEleInterlock
        {
            set
            {
                if (value == m_AirAndEleInterlock)
                {
                    return;
                }
                m_AirAndEleInterlock = value;
                RaisePropertyChanged(() => AirAndEleInterlock);
            }
            get { return m_AirAndEleInterlock; }
        }

        public WorkFlags()
        {
            m_EventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            m_UpdateEmergenceTime = new Timer(1000) { AutoReset = true };
            m_UpdateEmergenceTime.Elapsed += UpdateEmergenceTimeOnElapsed;
        }

        private void UpdateEmergenceTimeOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            --EmergenceTime;

            if (EmergenceTime <= 0)
            {
                m_UpdateEmergenceTime.Stop();
                EmergenceTimeUpEvent.Publish(new EmergenceTimeUpEventArg(EmergenceTimerState.End));
            }
        }
    }
}