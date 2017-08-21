using System;
using System.ComponentModel.Composition;
using System.Threading;

namespace Engine.HMI.SS3B.View.ViewModel.KunMing
{

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class TitleViewModel : ViewModelBase
    {
        private string m_Time;
        private string m_Date;
        private string m_Direction;
        private string m_MagneticLevel;
        private string m_WorkingStatus;
        private string m_BrakeMonitorLevel;
        private string m_TrainSpeed;
        private Timer m_Timer;
        public TitleViewModel()
        {
            TrainSpeed = "11";
            BrakeMonitorLevel = "6.1";
            WorkingStatus = "主断合";
            MagneticLevel = "10";
            Direction = "向前";
            m_Timer = new Timer(state =>
            {
                Date = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Day.ToString().PadLeft(2, '0'));
                Time = string.Format("{0}:{1}:{2}", DateTime.Now.Hour.ToString().PadLeft(2, '0'), DateTime.Now.Minute.ToString().PadLeft(2, '0'), DateTime.Now.Second.ToString().PadLeft(2, '0'));
            }, null, 0, 1000);
        }
        public string TrainSpeed
        {
            get { return m_TrainSpeed; }
            set
            {
                if (value == m_TrainSpeed)
                {
                    return;
                }
                m_TrainSpeed = value;
                RaisePropertyChanged(() => TrainSpeed);
            }
        }

        public string BrakeMonitorLevel
        {
            get { return m_BrakeMonitorLevel; }
            set
            {
                if (value == m_BrakeMonitorLevel)
                {
                    return;
                }
                m_BrakeMonitorLevel = value;
                RaisePropertyChanged(() => BrakeMonitorLevel);
            }
        }

        public string WorkingStatus
        {
            get { return m_WorkingStatus; }
            set
            {
                if (value == m_WorkingStatus)
                {
                    return;
                }
                m_WorkingStatus = value;
                RaisePropertyChanged(() => WorkingStatus);
            }
        }

        public string MagneticLevel
        {
            get { return m_MagneticLevel; }
            set
            {
                if (value == m_MagneticLevel)
                {
                    return;
                }
                m_MagneticLevel = value;
                RaisePropertyChanged(() => MagneticLevel);
            }
        }

        public string Direction
        {
            get { return m_Direction; }
            set
            {
                if (value == m_Direction)
                {
                    return;
                }
                m_Direction = value;
                RaisePropertyChanged(() => Direction);
            }
        }

        public string Date
        {
            get { return m_Date; }
            set
            {
                if (value == m_Date)
                {
                    return;
                }
                m_Date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        public string Time
        {
            get { return m_Time; }
            set
            {
                if (value == m_Time)
                {
                    return;
                }
                m_Time = value;
                RaisePropertyChanged(() => Time);
            }
        }
    }
}
