using System;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Signal.Warning;

namespace Tram.CBTC.Infrasturcture.Model.Signal
{
    public class SignalInfo : NotificationObject
    {
        private WarningIntervention m_WarningIntervention;
        private string m_AfterDistanceAndTime;
        private string m_ForwardDistanceAndTime;
        private string m_NextStationTime;
        private ATPProtectionControlStatus m_ATPProtectionControlStatus;
        private ATPProhibitStatus m_ATPProhibitStatus;
        private BeaconStatus m_BeaconStatus;
        private float m_AfterDistance;
        private float m_ForwardDistance;
        private DateTime m_AfterCarTime;
        private DateTime m_ForwardCarTime;

        public SignalInfo()
        {
            Speed = new Speed.Speed();
            WarningIntervention = new WarningIntervention();
            Alram = new Alram.Alram();
        }
        /// <summary>
        /// 速度
        /// </summary>
        public Speed.Speed Speed { get; protected set; }

        /// <summary>
        /// 报警介入
        /// </summary>
        public WarningIntervention WarningIntervention
        {
            get { return m_WarningIntervention; }
            private set
            {
                if (Equals(value, m_WarningIntervention))
                    return;

                m_WarningIntervention = value;
                RaisePropertyChanged(() => WarningIntervention);
            }
        }

        /// <summary>
        /// 前车距离和时间
        /// </summary>
        public string ForwardDistanceAndTime
        {
            get { return m_ForwardDistanceAndTime; }
            set
            {
                if (value == m_ForwardDistanceAndTime)
                    return;
                m_ForwardDistanceAndTime = value;
                RaisePropertyChanged(() => ForwardDistanceAndTime);
            }
        }

        /// <summary>
        /// 距离前车时间
        /// </summary>
        public DateTime ForwardCarTime
        {
            get { return m_ForwardCarTime; }
            set
            {
                if (value.Equals(m_ForwardCarTime))
                {
                    return;
                }
                m_ForwardCarTime = value;
                RaisePropertyChanged(() => ForwardCarTime);
            }
        }

        /// <summary>
        /// 距离后车时间
        /// </summary>
        public DateTime AfterCarTime
        {
            get { return m_AfterCarTime; }
            set
            {
                if (value.Equals(m_AfterCarTime))
                {
                    return;
                }
                m_AfterCarTime = value;
                RaisePropertyChanged(() => AfterCarTime);
            }
        }

        /// <summary>
        /// 前车距离（M）
        /// </summary>
        public float ForwardDistance
        {
            get { return m_ForwardDistance; }
            set
            {
                if (value.Equals(m_ForwardDistance))
                {
                    return;
                }
                m_ForwardDistance = value;
                RaisePropertyChanged(() => ForwardDistance);
            }
        }

        /// <summary>
        /// 后车距离（M）
        /// </summary>
        public float AfterDistance
        {
            get { return m_AfterDistance; }
            set
            {
                if (value.Equals(m_AfterDistance))
                {
                    return;
                }
                m_AfterDistance = value;
                RaisePropertyChanged(() => AfterDistance);
            }
        }

        /// <summary>
        /// 后车距离和时间
        /// </summary>
        public string AfterDistanceAndTime
        {
            get { return m_AfterDistanceAndTime; }
            set
            {
                if (value == m_AfterDistanceAndTime)
                    return;
                m_AfterDistanceAndTime = value;
                RaisePropertyChanged(() => AfterDistanceAndTime);
            }
        }

        /// <summary>
        /// 警报
        /// </summary>
        public Alram.Alram Alram { get; private set; }

        /// <summary>
        /// 到下一站时间
        /// </summary>

        public string NextStationTime
        {
            get { return m_NextStationTime; }
            set
            {
                if (Equals(value, m_NextStationTime))
                    return;
                m_NextStationTime = value;
                RaisePropertyChanged(() => NextStationTime);
            }
        }

        /// <summary>
        /// ATP保护功能
        /// </summary>
        public ATPProtectionControlStatus ATPProtectionControlStatus
        {
            get { return m_ATPProtectionControlStatus; }
            set
            {
                if (value == m_ATPProtectionControlStatus)
                    return;

                m_ATPProtectionControlStatus = value;
                RaisePropertyChanged(() => ATPProtectionControlStatus);
            }
        }

        /// <summary>
        /// ATP禁止状态
        /// </summary>
        public ATPProhibitStatus ATPProhibitStatus
        {
            get { return m_ATPProhibitStatus; }
            set
            {
                if (value == m_ATPProhibitStatus)
                    return;

                m_ATPProhibitStatus = value;
                RaisePropertyChanged(() => ATPProhibitStatus);
            }
        }

        /// <summary>
        /// 信标状态
        /// </summary>
        public BeaconStatus BeaconStatus
        {
            get { return m_BeaconStatus; }
            set
            {
                if (value == m_BeaconStatus)
                    return;

                m_BeaconStatus = value;
                RaisePropertyChanged(() => BeaconStatus);
            }
        }
    }


}