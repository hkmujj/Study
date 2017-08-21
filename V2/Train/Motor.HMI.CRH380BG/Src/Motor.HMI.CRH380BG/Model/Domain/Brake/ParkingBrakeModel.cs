using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.HMI.CRH380BG.Model.Domain.Brake
{
    [Export]
    public class ParkingBrakeModel : NotificationObject
    {
        //停放制动施加
        private ParkingState m_IsParkingBrake8Apply;
        private ParkingState m_IsParkingBrake7Apply;
        private ParkingState m_IsParkingBrake6Apply;
        private ParkingState m_IsParkingBrake5Apply;
        private ParkingState m_IsParkingBrake4Apply;
        private ParkingState m_IsParkingBrake3Apply;
        private ParkingState m_IsParkingBrake2Apply;
        private ParkingState m_IsParkingBrake1Apply;

        //停放制动有效
        private ParkingStateEfficence m_IsParkingBrake8Efficent;
        private ParkingStateEfficence m_IsParkingBrake7Efficent;
        private ParkingStateEfficence m_IsParkingBrake6Efficent;
        private ParkingStateEfficence m_IsParkingBrake5Efficent;
        private ParkingStateEfficence m_IsParkingBrake4Efficent;
        private ParkingStateEfficence m_IsParkingBrake3Efficent;
        private ParkingStateEfficence m_IsParkingBrake2Efficent;
        private ParkingStateEfficence m_IsParkingBrake1Efficent;

        //停放制动未知
        private ParkingStateUnkown m_IsParkingBrake8Unkown;
        private ParkingStateUnkown m_IsParkingBrake7Unkown;
        private ParkingStateUnkown m_IsParkingBrake6Unkown;
        private ParkingStateUnkown m_IsParkingBrake5Unkown;
        private ParkingStateUnkown m_IsParkingBrake4Unkown;
        private ParkingStateUnkown m_IsParkingBrake3Unkown;
        private ParkingStateUnkown m_IsParkingBrake2Unkown;
        private ParkingStateUnkown m_IsParkingBrake1Unkown;

        public ParkingState IsParkingBrake1Apply
        {
            get { return m_IsParkingBrake1Apply; }
            set
            {
                if (value == m_IsParkingBrake1Apply)
                {
                    return;
                }
                m_IsParkingBrake1Apply = value;
                RaisePropertyChanged(() => IsParkingBrake1Apply);
            }
        }

        public ParkingState IsParkingBrake2Apply
        {
            get { return m_IsParkingBrake2Apply; }
            set
            {
                if (value == m_IsParkingBrake2Apply)
                {
                    return;
                }
                m_IsParkingBrake2Apply = value;
                RaisePropertyChanged(() => IsParkingBrake2Apply);
            }
        }

        public ParkingState IsParkingBrake3Apply
        {
            get { return m_IsParkingBrake3Apply; }
            set
            {
                if (value == m_IsParkingBrake3Apply)
                {
                    return;
                }
                m_IsParkingBrake3Apply = value;
                RaisePropertyChanged(() => IsParkingBrake3Apply);
            }
        }

        public ParkingState IsParkingBrake4Apply
        {
            get { return m_IsParkingBrake4Apply; }
            set
            {
                if (value == m_IsParkingBrake4Apply)
                {
                    return;
                }
                m_IsParkingBrake4Apply = value;
                RaisePropertyChanged(() => IsParkingBrake4Apply);
            }
        }

        public ParkingState IsParkingBrake5Apply
        {
            get { return m_IsParkingBrake5Apply; }
            set
            {
                if (value == m_IsParkingBrake5Apply)
                {
                    return;
                }
                m_IsParkingBrake5Apply = value;
                RaisePropertyChanged(() => IsParkingBrake5Apply);
            }
        }

        public ParkingState IsParkingBrake6Apply
        {
            get { return m_IsParkingBrake6Apply; }
            set
            {
                if (value == m_IsParkingBrake6Apply)
                {
                    return;
                }
                m_IsParkingBrake6Apply = value;
                RaisePropertyChanged(() => IsParkingBrake6Apply);
            }
        }

        public ParkingState IsParkingBrake7Apply
        {
            get { return m_IsParkingBrake7Apply; }
            set
            {
                if (value == m_IsParkingBrake7Apply)
                {
                    return;
                }
                m_IsParkingBrake7Apply = value;
                RaisePropertyChanged(() => IsParkingBrake7Apply);
            }
        }

        public ParkingState IsParkingBrake8Apply
        {
            get { return m_IsParkingBrake8Apply; }
            set
            {
                if (value == m_IsParkingBrake8Apply)
                {
                    return;
                }
                m_IsParkingBrake8Apply = value;
                RaisePropertyChanged(() => IsParkingBrake8Apply);
            }
        }

        public ParkingStateEfficence IsParkingBrake1Efficent
        {
            get { return m_IsParkingBrake1Efficent; }
            set
            {
                if (value == m_IsParkingBrake1Efficent)
                {
                    return;
                }
                m_IsParkingBrake1Efficent = value;
                RaisePropertyChanged(() => IsParkingBrake1Efficent);
            }
        }

        public ParkingStateEfficence IsParkingBrake2Efficent
        {
            get { return m_IsParkingBrake2Efficent; }
            set
            {
                if (value == m_IsParkingBrake2Efficent)
                {
                    return;
                }
                m_IsParkingBrake2Efficent = value;
                RaisePropertyChanged(() => IsParkingBrake2Efficent);
            }
        }

        public ParkingStateEfficence IsParkingBrake3Efficent
        {
            get { return m_IsParkingBrake3Efficent; }
            set
            {
                if (value == m_IsParkingBrake3Efficent)
                {
                    return;
                }
                m_IsParkingBrake3Efficent = value;
                RaisePropertyChanged(() => IsParkingBrake3Efficent);
            }
        }

        public ParkingStateEfficence IsParkingBrake4Efficent
        {
            get { return m_IsParkingBrake4Efficent; }
            set
            {
                if (value == m_IsParkingBrake4Efficent)
                {
                    return;
                }
                m_IsParkingBrake4Efficent = value;
                RaisePropertyChanged(() => IsParkingBrake4Efficent);
            }
        }

        public ParkingStateEfficence IsParkingBrake5Efficent
        {
            get { return m_IsParkingBrake5Efficent; }
            set
            {
                if (value == m_IsParkingBrake5Efficent)
                {
                    return;
                }
                m_IsParkingBrake5Efficent = value;
                RaisePropertyChanged(() => IsParkingBrake5Efficent);
            }
        }

        public ParkingStateEfficence IsParkingBrake6Efficent
        {
            get { return m_IsParkingBrake6Efficent; }
            set
            {
                if (value == m_IsParkingBrake6Efficent)
                {
                    return;
                }
                m_IsParkingBrake6Efficent = value;
                RaisePropertyChanged(() => IsParkingBrake6Efficent);
            }
        }

        public ParkingStateEfficence IsParkingBrake7Efficent
        {
            get { return m_IsParkingBrake7Efficent; }
            set
            {
                if (value == m_IsParkingBrake7Efficent)
                {
                    return;
                }
                m_IsParkingBrake7Efficent = value;
                RaisePropertyChanged(() => IsParkingBrake7Efficent);
            }
        }

        public ParkingStateEfficence IsParkingBrake8Efficent
        { 
            get { return m_IsParkingBrake8Efficent; }
            set
            {
                if (value == m_IsParkingBrake8Efficent)
                {
                    return;
                }
                m_IsParkingBrake8Efficent = value;
                RaisePropertyChanged(() => IsParkingBrake8Efficent);
            }
        }

        public ParkingStateUnkown IsParkingBrake1Unkown
        {
            get { return m_IsParkingBrake1Unkown; }
            set
            {
                if (value == m_IsParkingBrake1Unkown)
                {
                    return;
                }
                m_IsParkingBrake1Unkown = value;
                RaisePropertyChanged(() => IsParkingBrake1Unkown);
            }
        }

        public ParkingStateUnkown IsParkingBrake2Unkown
        {
            get { return m_IsParkingBrake2Unkown; }
            set
            {
                if (value == m_IsParkingBrake2Unkown)
                {
                    return;
                }
                m_IsParkingBrake2Unkown = value;
                RaisePropertyChanged(() => IsParkingBrake2Unkown);
            }
        }

        public ParkingStateUnkown IsParkingBrake3Unkown
        {
            get { return m_IsParkingBrake3Unkown; }
            set
            {
                if (value == m_IsParkingBrake3Unkown)
                {
                    return;


                }
                m_IsParkingBrake3Unkown = value;
                RaisePropertyChanged(() => IsParkingBrake3Unkown);
            }
        }

        public ParkingStateUnkown IsParkingBrake4Unkown
        {
            get { return m_IsParkingBrake4Unkown; }
            set
            {
                if (value == m_IsParkingBrake4Unkown)
                {
                    return;
                }
                m_IsParkingBrake4Unkown = value;
                RaisePropertyChanged(() => IsParkingBrake4Unkown);
            }
        }

        public ParkingStateUnkown IsParkingBrake5Unkown
        {
            get { return m_IsParkingBrake5Unkown; }
            set
            {
                if (value == m_IsParkingBrake5Unkown)
                {
                    return;
                }
                m_IsParkingBrake5Unkown = value;
                RaisePropertyChanged(() => IsParkingBrake5Unkown);
            }
        }

        public ParkingStateUnkown IsParkingBrake6Unkown
        {
            get { return m_IsParkingBrake6Unkown; }
            set
            {
                if (value == m_IsParkingBrake6Unkown)
                {
                    return;
                }
                m_IsParkingBrake6Unkown = value;
                RaisePropertyChanged(() => IsParkingBrake6Unkown);
            }
        }

        public ParkingStateUnkown IsParkingBrake7Unkown
        {
            get { return m_IsParkingBrake7Unkown; }
            set
            {
                if (value == m_IsParkingBrake7Unkown)
                {
                    return;
                }
                m_IsParkingBrake7Unkown = value;
                RaisePropertyChanged(() => IsParkingBrake7Unkown);
            }
        }

        public ParkingStateUnkown IsParkingBrake8Unkown
        {
            get { return m_IsParkingBrake8Unkown; }
            set
            {
                if (value == m_IsParkingBrake8Unkown)
                {
                    return;
                }
                m_IsParkingBrake8Unkown = value;
                RaisePropertyChanged(() => IsParkingBrake8Unkown);
            }
        }


    }
}