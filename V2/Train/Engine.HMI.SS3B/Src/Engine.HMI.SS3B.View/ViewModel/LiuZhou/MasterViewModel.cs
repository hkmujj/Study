using System.ComponentModel.Composition;
using Engine.HMI.SS3B.Interface.ViewState;

namespace Engine.HMI.SS3B.View.ViewModel.LiuZhou
{

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MasterViewModel : ViewModelBase
    {
        public MasterViewModel()
        {

            ValvePressureTwoOther = 0;
            ValvePressureTwoOrigin = 0;
            ValvePressureOneOther = 0;
            ValvePressureOneOrigin = 0;
            Direction = 0;
            MagneticLevel = 0;
            BrakeLevel = 0;
            TrainSpeed = 0;
            MotorGalvanicSixOther = 0;
            MotorGalvanicSixOrigin = 0;
            MotorGalvanicFiveOther = 0;
            MotorGalvanicFiveOrigin = 0;
            MotorGalvanicFourOther = 0;
            MotorGalvanicFourOrigin = 0;
            MotorGalvanicThreeOther = 0;
            MotorGalvanicThreeOrigin = 0;
            MotorGalvanicTwoOther = 0;
            MotorGalvanicTwoOrigin = 0;
            MotorGalvanicOneOther = 0;
            MotorGalvanicOneOrigin = 0;
            MagneticGalvanicOther = 0;
            MagneticGalvanicOrigin = 0;
            MotorVoltageTwoOther = 0;
            MotorVoltageTwoOrigin = 0;
            MotorVoltageOneOther = 0;
            MotorVoltageOneOrigin = 0;
            CameraTwoOther = 0;
            CameraTwoOrigin = 0;
            CameraOneOther = 0;
            CameraOneOrigin = 0;
            NetTwoOther = 0;
            NetTwoOrigin = 0;
            NetOneOther = 0;
            NetOneOrigin = 0;
            DestineOther = 0;
            DestineOrigin = 0;
            MasterOther = 0;
            MasterOrigin = 0;

        }
        private double m_ValvePressureTwoOther;
        private double m_ValvePressureTwoOrigin;
        private double m_ValvePressureOneOther;
        private double m_ValvePressureOneOrigin;
        private Direction m_Direction;
        private double m_MagneticLevel;
        private WorkModle m_Worke;
        private double m_BrakeLevel;
        private double m_TrainSpeed;
        private double m_MotorGalvanicSixOther;
        private double m_MotorGalvanicSixOrigin;
        private double m_MotorGalvanicFiveOther;
        private double m_MotorGalvanicFiveOrigin;
        private double m_MotorGalvanicFourOther;
        private double m_MotorGalvanicFourOrigin;
        private double m_MotorGalvanicThreeOther;
        private double m_MotorGalvanicThreeOrigin;
        private double m_MotorGalvanicTwoOther;
        private double m_MotorGalvanicTwoOrigin;
        private double m_MotorGalvanicOneOther;
        private double m_MotorGalvanicOneOrigin;
        private double m_MagneticGalvanicOther;
        private double m_MagneticGalvanicOrigin;
        private double m_MotorVoltageTwoOther;
        private double m_MotorVoltageTwoOrigin;
        private double m_MotorVoltageOneOther;
        private double m_MotorVoltageOneOrigin;
        private ColorLevel m_CameraTwoOther;
        private ColorLevel m_CameraTwoOrigin;
        private ColorLevel m_CameraOneOther;
        private ColorLevel m_CameraOneOrigin;
        private ColorLevel m_NetTwoOther;
        private ColorLevel m_NetTwoOrigin;
        private ColorLevel m_NetOneOther;
        private ColorLevel m_NetOneOrigin;
        private ColorLevel m_DestineOther;
        private ColorLevel m_DestineOrigin;
        private ColorLevel m_MasterOther;
        private ColorLevel m_MasterOrigin;


        public ColorLevel MasterOrigin
        {
            get { return m_MasterOrigin; }
            set
            {
                if (value == m_MasterOrigin)
                {
                    return;
                }
                m_MasterOrigin = value;
                RaisePropertyChanged(() => MasterOrigin);
            }
        }

        public ColorLevel MasterOther
        {
            get { return m_MasterOther; }
            set
            {
                if (value == m_MasterOther)
                {
                    return;
                }
                m_MasterOther = value;
                RaisePropertyChanged(() => MasterOther);
            }
        }

        public ColorLevel DestineOrigin
        {
            get { return m_DestineOrigin; }
            set
            {
                if (value == m_DestineOrigin)
                {
                    return;
                }
                m_DestineOrigin = value;
                RaisePropertyChanged(() => DestineOrigin);
            }
        }

        public ColorLevel DestineOther
        {
            get { return m_DestineOther; }
            set
            {
                if (value == m_DestineOther)
                {
                    return;
                }
                m_DestineOther = value;
                RaisePropertyChanged(() => DestineOther);
            }
        }

        public ColorLevel NetOneOrigin
        {
            get { return m_NetOneOrigin; }
            set
            {
                if (value == m_NetOneOrigin)
                {
                    return;
                }
                m_NetOneOrigin = value;
                RaisePropertyChanged(() => NetOneOrigin);
            }
        }

        public ColorLevel NetOneOther
        {
            get { return m_NetOneOther; }
            set
            {
                if (value == m_NetOneOther)
                {
                    return;
                }
                m_NetOneOther = value;
                RaisePropertyChanged(() => NetOneOther);
            }
        }

        public ColorLevel NetTwoOrigin
        {
            get { return m_NetTwoOrigin; }
            set
            {
                if (value == m_NetTwoOrigin)
                {
                    return;
                }
                m_NetTwoOrigin = value;
                RaisePropertyChanged(() => NetTwoOrigin);
            }
        }

        public ColorLevel NetTwoOther
        {
            get { return m_NetTwoOther; }
            set
            {
                if (value == m_NetTwoOther)
                {
                    return;
                }
                m_NetTwoOther = value;
                RaisePropertyChanged(() => NetTwoOther);
            }
        }

        public ColorLevel CameraOneOrigin
        {
            get { return m_CameraOneOrigin; }
            set
            {
                if (value == m_CameraOneOrigin)
                {
                    return;
                }
                m_CameraOneOrigin = value;
                RaisePropertyChanged(() => CameraOneOrigin);
            }
        }

        public ColorLevel CameraOneOther
        {
            get { return m_CameraOneOther; }
            set
            {
                if (value == m_CameraOneOther)
                {
                    return;
                }
                m_CameraOneOther = value;
                RaisePropertyChanged(() => CameraOneOther);
            }
        }

        public ColorLevel CameraTwoOrigin
        {
            get { return m_CameraTwoOrigin; }
            set
            {
                if (value == m_CameraTwoOrigin)
                {
                    return;
                }
                m_CameraTwoOrigin = value;
                RaisePropertyChanged(() => CameraTwoOrigin);
            }
        }

        public ColorLevel CameraTwoOther
        {
            get { return m_CameraTwoOther; }
            set
            {
                if (value == m_CameraTwoOther)
                {
                    return;
                }
                m_CameraTwoOther = value;
                RaisePropertyChanged(() => CameraTwoOther);
            }
        }

        public double MotorVoltageOneOrigin
        {
            get { return m_MotorVoltageOneOrigin; }
            set
            {
                if (value.Equals(m_MotorVoltageOneOrigin))
                {
                    return;
                }
                m_MotorVoltageOneOrigin = value;
                RaisePropertyChanged(() => MotorVoltageOneOrigin);
            }
        }

        public double MotorVoltageOneOther
        {
            get { return m_MotorVoltageOneOther; }
            set
            {
                if (value.Equals(m_MotorVoltageOneOther))
                {
                    return;
                }
                m_MotorVoltageOneOther = value;
                RaisePropertyChanged(() => MotorVoltageOneOther);
            }
        }

        public double MotorVoltageTwoOrigin
        {
            get { return m_MotorVoltageTwoOrigin; }
            set
            {
                if (value.Equals(m_MotorVoltageTwoOrigin))
                {
                    return;
                }
                m_MotorVoltageTwoOrigin = value;
                RaisePropertyChanged(() => MotorVoltageTwoOrigin);
            }
        }

        public double MotorVoltageTwoOther
        {
            get { return m_MotorVoltageTwoOther; }
            set
            {
                if (value.Equals(m_MotorVoltageTwoOther))
                {
                    return;
                }
                m_MotorVoltageTwoOther = value;
                RaisePropertyChanged(() => MotorVoltageTwoOther);
            }
        }

        public double MagneticGalvanicOrigin
        {
            get { return m_MagneticGalvanicOrigin; }
            set
            {
                if (value.Equals(m_MagneticGalvanicOrigin))
                {
                    return;
                }
                m_MagneticGalvanicOrigin = value;
                RaisePropertyChanged(() => MagneticGalvanicOrigin);
            }
        }

        public double MagneticGalvanicOther
        {
            get { return m_MagneticGalvanicOther; }
            set
            {
                if (value.Equals(m_MagneticGalvanicOther))
                {
                    return;
                }
                m_MagneticGalvanicOther = value;
                RaisePropertyChanged(() => MagneticGalvanicOther);
            }
        }

        public double MotorGalvanicOneOrigin
        {
            get { return m_MotorGalvanicOneOrigin; }
            set
            {
                if (value.Equals(m_MotorGalvanicOneOrigin))
                {
                    return;
                }
                m_MotorGalvanicOneOrigin = value;
                RaisePropertyChanged(() => MotorGalvanicOneOrigin);
            }
        }

        public double MotorGalvanicOneOther
        {
            get { return m_MotorGalvanicOneOther; }
            set
            {
                if (value.Equals(m_MotorGalvanicOneOther))
                {
                    return;
                }
                m_MotorGalvanicOneOther = value;
                RaisePropertyChanged(() => MotorGalvanicOneOther);
            }
        }

        public double MotorGalvanicTwoOrigin
        {
            get { return m_MotorGalvanicTwoOrigin; }
            set
            {
                if (value.Equals(m_MotorGalvanicTwoOrigin))
                {
                    return;
                }
                m_MotorGalvanicTwoOrigin = value;
                RaisePropertyChanged(() => MotorGalvanicTwoOrigin);
            }
        }

        public double MotorGalvanicTwoOther
        {
            get { return m_MotorGalvanicTwoOther; }
            set
            {
                if (value.Equals(m_MotorGalvanicTwoOther))
                {
                    return;
                }
                m_MotorGalvanicTwoOther = value;
                RaisePropertyChanged(() => MotorGalvanicTwoOther);
            }
        }

        public double MotorGalvanicThreeOrigin
        {
            get { return m_MotorGalvanicThreeOrigin; }
            set
            {
                if (value.Equals(m_MotorGalvanicThreeOrigin))
                {
                    return;
                }
                m_MotorGalvanicThreeOrigin = value;
                RaisePropertyChanged(() => MotorGalvanicThreeOrigin);
            }
        }

        public double MotorGalvanicThreeOther
        {
            get { return m_MotorGalvanicThreeOther; }
            set
            {
                if (value.Equals(m_MotorGalvanicThreeOther))
                {
                    return;
                }
                m_MotorGalvanicThreeOther = value;
                RaisePropertyChanged(() => MotorGalvanicThreeOther);
            }
        }

        public double MotorGalvanicFourOrigin
        {
            get { return m_MotorGalvanicFourOrigin; }
            set
            {
                if (value.Equals(m_MotorGalvanicFourOrigin))
                {
                    return;
                }
                m_MotorGalvanicFourOrigin = value;
                RaisePropertyChanged(() => MotorGalvanicFourOrigin);
            }
        }

        public double MotorGalvanicFourOther
        {
            get { return m_MotorGalvanicFourOther; }
            set
            {
                if (value.Equals(m_MotorGalvanicFourOther))
                {
                    return;
                }
                m_MotorGalvanicFourOther = value;
                RaisePropertyChanged(() => MotorGalvanicFourOther);
            }
        }

        public double MotorGalvanicFiveOrigin
        {
            get { return m_MotorGalvanicFiveOrigin; }
            set
            {
                if (value.Equals(m_MotorGalvanicFiveOrigin))
                {
                    return;
                }
                m_MotorGalvanicFiveOrigin = value;
                RaisePropertyChanged(() => MotorGalvanicFiveOrigin);
            }
        }

        public double MotorGalvanicFiveOther
        {
            get { return m_MotorGalvanicFiveOther; }
            set
            {
                if (value.Equals(m_MotorGalvanicFiveOther))
                {
                    return;
                }
                m_MotorGalvanicFiveOther = value;
                RaisePropertyChanged(() => MotorGalvanicFiveOther);
            }
        }

        public double MotorGalvanicSixOrigin
        {
            get { return m_MotorGalvanicSixOrigin; }
            set
            {
                if (value.Equals(m_MotorGalvanicSixOrigin))
                {
                    return;
                }
                m_MotorGalvanicSixOrigin = value;
                RaisePropertyChanged(() => MotorGalvanicSixOrigin);
            }
        }

        public double MotorGalvanicSixOther
        {
            get { return m_MotorGalvanicSixOther; }
            set
            {
                if (value.Equals(m_MotorGalvanicSixOther))
                {
                    return;
                }
                m_MotorGalvanicSixOther = value;
                RaisePropertyChanged(() => MotorGalvanicSixOther);
            }
        }

        public double TrainSpeed
        {
            get { return m_TrainSpeed; }
            set
            {
                if (value.Equals(m_TrainSpeed))
                {
                    return;
                }
                m_TrainSpeed = (int)value;
                RaisePropertyChanged(() => TrainSpeed);
            }
        }

        public double BrakeLevel
        {
            get { return m_BrakeLevel; }
            set
            {
                if (value.Equals(m_BrakeLevel))
                {
                    return;
                }
                m_BrakeLevel = value;
                RaisePropertyChanged(() => BrakeLevel);
            }
        }

        public WorkModle Worke
        {
            get { return m_Worke; }
            set
            {
                if (value == m_Worke)
                {
                    return;
                }
                m_Worke = value;
                RaisePropertyChanged(() => Worke);
            }
        }

        public double MagneticLevel
        {
            get { return m_MagneticLevel; }
            set
            {
                if (value.Equals(m_MagneticLevel))
                {
                    return;
                }
                m_MagneticLevel = value;
                RaisePropertyChanged(() => MagneticLevel);
            }
        }

        public Direction Direction
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

        public double ValvePressureOneOrigin
        {
            get { return m_ValvePressureOneOrigin; }
            set
            {
                if (value.Equals(m_ValvePressureOneOrigin))
                {
                    return;
                }
                m_ValvePressureOneOrigin = value;
                RaisePropertyChanged(() => ValvePressureOneOrigin);
            }
        }

        public double ValvePressureOneOther
        {
            get { return m_ValvePressureOneOther; }
            set
            {
                if (value.Equals(m_ValvePressureOneOther))
                {
                    return;
                }
                m_ValvePressureOneOther = value;
                RaisePropertyChanged(() => ValvePressureOneOther);
            }
        }

        public double ValvePressureTwoOrigin
        {
            get { return m_ValvePressureTwoOrigin; }
            set
            {
                if (value.Equals(m_ValvePressureTwoOrigin))
                {
                    return;
                }
                m_ValvePressureTwoOrigin = value;
                RaisePropertyChanged(() => ValvePressureTwoOrigin);
            }
        }

        public double ValvePressureTwoOther
        {
            get { return m_ValvePressureTwoOther; }
            set
            {
                if (value.Equals(m_ValvePressureTwoOther))
                {
                    return;
                }
                m_ValvePressureTwoOther = value;
                RaisePropertyChanged(() => ValvePressureTwoOther);
            }
        }
    }
}
