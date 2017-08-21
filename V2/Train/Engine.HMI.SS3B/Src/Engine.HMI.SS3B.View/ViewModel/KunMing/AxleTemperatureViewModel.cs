using System.ComponentModel.Composition;

namespace Engine.HMI.SS3B.View.ViewModel.KunMing
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AxleTemperatureViewModel:ViewModelBase
    {
        private double m_SensorNumOther;
        private double m_MaxAxisTemperatureLocationOther;
        private double m_MaxAxisTemperatureOther;
        private double m_Axis6HeldBoxRighrOther;
        private double m_Axis6AxisBoxRighrOther;
        private double m_Axis6HeldBoxLeftOther;
        private double m_Axis6AxisBoxLeftOther;
        private double m_Axis5HeldBoxRighrOther;
        private double m_Axis5AxisBoxRighrOther;
        private double m_Axis5HeldBoxLeftOther;
        private double m_Axis5AxisBoxLeftOther;
        private double m_Axis4HeldBoxRighrOther;
        private double m_Axis4AxisBoxRighrOther;
        private double m_Axis4HeldBoxLeftOther;
        private double m_Axis4AxisBoxLeftOther;
        private double m_Axis3HeldBoxRighrOther;
        private double m_Axis3AxisBoxRighrOther;
        private double m_Axis3HeldBoxLeftOther;
        private double m_Axis3AxisBoxLeftOther;
        private double m_Axis2HeldBoxRighrOther;
        private double m_Axis2AxisBoxRighrOther;
        private double m_Axis2HeldBoxLeftOther;
        private double m_Axis2AxisBoxLeftOther;
        private double m_Axis1HeldBoxRighrOther;
        private double m_Axis1AxisBoxRighrOther;
        private double m_Axis1HeldBoxLeftOther;
        private double m_Axis1AxisBoxLeftOther;
        private double m_EnvironmentTemperatureOther;
        private double m_SensorNumOrigin;
        private double m_MaxAxisTemperatureLocationOrigin;
        private double m_MaxAxisTemperatureOrigin;
        private double m_Axis6HeldBoxRighrOrigin;
        private double m_Axis6AxisBoxRighrOrigin;
        private double m_Axis6HeldBoxLeftOrigin;
        private double m_Axis6AxisBoxLeftOrigin;
        private double m_Axis5HeldBoxRighrOrigin;
        private double m_Axis5AxisBoxRighrOrigin;
        private double m_Axis5HeldBoxLeftOrigin;
        private double m_Axis5AxisBoxLeftOrigin;
        private double m_Axis4HeldBoxRighrOrigin;
        private double m_Axis4AxisBoxRighrOrigin;
        private double m_Axis4HeldBoxLeftOrigin;
        private double m_Axis4AxisBoxLeftOrigin;
        private double m_Axis3HeldBoxRighrOrigin;
        private double m_Axis3AxisBoxRighrOrigin;
        private double m_Axis3HeldBoxLeftOrigin;
        private double m_Axis3AxisBoxLeftOrigin;
        private double m_Axis2HeldBoxRighrOrigin;
        private double m_Axis2AxisBoxRighrOrigin;
        private double m_Axis2HeldBoxLeftOrigin;
        private double m_Axis2AxisBoxLeftOrigin;
        private double m_Axis1HeldBoxRighrOrigin;
        private double m_Axis1AxisBoxRighrOrigin;
        private double m_Axis1HeldBoxLeftOrigin;
        private double m_Axis1AxisBoxLeftOrigin;
        private double m_EnvironmentTemperatureOrigin;

        public double EnvironmentTemperatureOrigin
        {
            get { return m_EnvironmentTemperatureOrigin; }
            set
            {
                if (value.Equals(m_EnvironmentTemperatureOrigin))
                {
                    return;
                }
                m_EnvironmentTemperatureOrigin = value;
                RaisePropertyChanged(() => EnvironmentTemperatureOrigin);
            }
        }

        public double Axis1AxisBoxLeftOrigin
        {
            get { return m_Axis1AxisBoxLeftOrigin; }
            set
            {
                if (value.Equals(m_Axis1AxisBoxLeftOrigin))
                {
                    return;
                }
                m_Axis1AxisBoxLeftOrigin = value;
                RaisePropertyChanged(() => Axis1AxisBoxLeftOrigin);
            }
        }

        public double Axis1HeldBoxLeftOrigin
        {
            get { return m_Axis1HeldBoxLeftOrigin; }
            set
            {
                if (value.Equals(m_Axis1HeldBoxLeftOrigin))
                {
                    return;
                }
                m_Axis1HeldBoxLeftOrigin = value;
                RaisePropertyChanged(() => Axis1HeldBoxLeftOrigin);
            }
        }

        public double Axis1AxisBoxRighrOrigin
        {
            get { return m_Axis1AxisBoxRighrOrigin; }
            set
            {
                if (value.Equals(m_Axis1AxisBoxRighrOrigin))
                {
                    return;
                }
                m_Axis1AxisBoxRighrOrigin = value;
                RaisePropertyChanged(() => Axis1AxisBoxRighrOrigin);
            }
        }

        public double Axis1HeldBoxRighrOrigin
        {
            get { return m_Axis1HeldBoxRighrOrigin; }
            set
            {
                if (value.Equals(m_Axis1HeldBoxRighrOrigin))
                {
                    return;
                }
                m_Axis1HeldBoxRighrOrigin = value;
                RaisePropertyChanged(() => Axis1HeldBoxRighrOrigin);
            }
        }

        public double Axis2AxisBoxLeftOrigin
        {
            get { return m_Axis2AxisBoxLeftOrigin; }
            set
            {
                if (value.Equals(m_Axis2AxisBoxLeftOrigin))
                {
                    return;
                }
                m_Axis2AxisBoxLeftOrigin = value;
                RaisePropertyChanged(() => Axis2AxisBoxLeftOrigin);
            }
        }

        public double Axis2HeldBoxLeftOrigin
        {
            get { return m_Axis2HeldBoxLeftOrigin; }
            set
            {
                if (value.Equals(m_Axis2HeldBoxLeftOrigin))
                {
                    return;
                }
                m_Axis2HeldBoxLeftOrigin = value;
                RaisePropertyChanged(() => Axis2HeldBoxLeftOrigin);
            }
        }

        public double Axis2AxisBoxRighrOrigin
        {
            get { return m_Axis2AxisBoxRighrOrigin; }
            set
            {
                if (value.Equals(m_Axis2AxisBoxRighrOrigin))
                {
                    return;
                }
                m_Axis2AxisBoxRighrOrigin = value;
                RaisePropertyChanged(() => Axis2AxisBoxRighrOrigin);
            }
        }

        public double Axis2HeldBoxRighrOrigin
        {
            get { return m_Axis2HeldBoxRighrOrigin; }
            set
            {
                if (value.Equals(m_Axis2HeldBoxRighrOrigin))
                {
                    return;
                }
                m_Axis2HeldBoxRighrOrigin = value;
                RaisePropertyChanged(() => Axis2HeldBoxRighrOrigin);
            }
        }

        public double Axis3AxisBoxLeftOrigin
        {
            get { return m_Axis3AxisBoxLeftOrigin; }
            set
            {
                if (value.Equals(m_Axis3AxisBoxLeftOrigin))
                {
                    return;
                }
                m_Axis3AxisBoxLeftOrigin = value;
                RaisePropertyChanged(() => Axis3AxisBoxLeftOrigin);
            }
        }

        public double Axis3HeldBoxLeftOrigin
        {
            get { return m_Axis3HeldBoxLeftOrigin; }
            set
            {
                if (value.Equals(m_Axis3HeldBoxLeftOrigin))
                {
                    return;
                }
                m_Axis3HeldBoxLeftOrigin = value;
                RaisePropertyChanged(() => Axis3HeldBoxLeftOrigin);
            }
        }

        public double Axis3AxisBoxRighrOrigin
        {
            get { return m_Axis3AxisBoxRighrOrigin; }
            set
            {
                if (value.Equals(m_Axis3AxisBoxRighrOrigin))
                {
                    return;
                }
                m_Axis3AxisBoxRighrOrigin = value;
                RaisePropertyChanged(() => Axis3AxisBoxRighrOrigin);
            }
        }

        public double Axis3HeldBoxRighrOrigin
        {
            get { return m_Axis3HeldBoxRighrOrigin; }
            set
            {
                if (value.Equals(m_Axis3HeldBoxRighrOrigin))
                {
                    return;
                }
                m_Axis3HeldBoxRighrOrigin = value;
                RaisePropertyChanged(() => Axis3HeldBoxRighrOrigin);
            }
        }

        public double Axis4AxisBoxLeftOrigin
        {
            get { return m_Axis4AxisBoxLeftOrigin; }
            set
            {
                if (value.Equals(m_Axis4AxisBoxLeftOrigin))
                {
                    return;
                }
                m_Axis4AxisBoxLeftOrigin = value;
                RaisePropertyChanged(() => Axis4AxisBoxLeftOrigin);
            }
        }

        public double Axis4HeldBoxLeftOrigin
        {
            get { return m_Axis4HeldBoxLeftOrigin; }
            set
            {
                if (value.Equals(m_Axis4HeldBoxLeftOrigin))
                {
                    return;
                }
                m_Axis4HeldBoxLeftOrigin = value;
                RaisePropertyChanged(() => Axis4HeldBoxLeftOrigin);
            }
        }

        public double Axis4AxisBoxRighrOrigin
        {
            get { return m_Axis4AxisBoxRighrOrigin; }
            set
            {
                if (value.Equals(m_Axis4AxisBoxRighrOrigin))
                {
                    return;
                }
                m_Axis4AxisBoxRighrOrigin = value;
                RaisePropertyChanged(() => Axis4AxisBoxRighrOrigin);
            }
        }

        public double Axis4HeldBoxRighrOrigin
        {
            get { return m_Axis4HeldBoxRighrOrigin; }
            set
            {
                if (value.Equals(m_Axis4HeldBoxRighrOrigin))
                {
                    return;
                }
                m_Axis4HeldBoxRighrOrigin = value;
                RaisePropertyChanged(() => Axis4HeldBoxRighrOrigin);
            }
        }

        public double Axis5AxisBoxLeftOrigin
        {
            get { return m_Axis5AxisBoxLeftOrigin; }
            set
            {
                if (value.Equals(m_Axis5AxisBoxLeftOrigin))
                {
                    return;
                }
                m_Axis5AxisBoxLeftOrigin = value;
                RaisePropertyChanged(() => Axis5AxisBoxLeftOrigin);
            }
        }

        public double Axis5HeldBoxLeftOrigin
        {
            get { return m_Axis5HeldBoxLeftOrigin; }
            set
            {
                if (value.Equals(m_Axis5HeldBoxLeftOrigin))
                {
                    return;
                }
                m_Axis5HeldBoxLeftOrigin = value;
                RaisePropertyChanged(() => Axis5HeldBoxLeftOrigin);
            }
        }

        public double Axis5AxisBoxRighrOrigin
        {
            get { return m_Axis5AxisBoxRighrOrigin; }
            set
            {
                if (value.Equals(m_Axis5AxisBoxRighrOrigin))
                {
                    return;
                }
                m_Axis5AxisBoxRighrOrigin = value;
                RaisePropertyChanged(() => Axis5AxisBoxRighrOrigin);
            }
        }

        public double Axis5HeldBoxRighrOrigin
        {
            get { return m_Axis5HeldBoxRighrOrigin; }
            set
            {
                if (value.Equals(m_Axis5HeldBoxRighrOrigin))
                {
                    return;
                }
                m_Axis5HeldBoxRighrOrigin = value;
                RaisePropertyChanged(() => Axis5HeldBoxRighrOrigin);
            }
        }

        public double Axis6AxisBoxLeftOrigin
        {
            get { return m_Axis6AxisBoxLeftOrigin; }
            set
            {
                if (value.Equals(m_Axis6AxisBoxLeftOrigin))
                {
                    return;
                }
                m_Axis6AxisBoxLeftOrigin = value;
                RaisePropertyChanged(() => Axis6AxisBoxLeftOrigin);
            }
        }

        public double Axis6HeldBoxLeftOrigin
        {
            get { return m_Axis6HeldBoxLeftOrigin; }
            set
            {
                if (value.Equals(m_Axis6HeldBoxLeftOrigin))
                {
                    return;
                }
                m_Axis6HeldBoxLeftOrigin = value;
                RaisePropertyChanged(() => Axis6HeldBoxLeftOrigin);
            }
        }

        public double Axis6AxisBoxRighrOrigin
        {
            get { return m_Axis6AxisBoxRighrOrigin; }
            set
            {
                if (value.Equals(m_Axis6AxisBoxRighrOrigin))
                {
                    return;
                }
                m_Axis6AxisBoxRighrOrigin = value;
                RaisePropertyChanged(() => Axis6AxisBoxRighrOrigin);
            }
        }

        public double Axis6HeldBoxRighrOrigin
        {
            get { return m_Axis6HeldBoxRighrOrigin; }
            set
            {
                if (value.Equals(m_Axis6HeldBoxRighrOrigin))
                {
                    return;
                }
                m_Axis6HeldBoxRighrOrigin = value;
                RaisePropertyChanged(() => Axis6HeldBoxRighrOrigin);
            }
        }

        public double MaxAxisTemperatureOrigin
        {
            get { return m_MaxAxisTemperatureOrigin; }
            set
            {
                if (value.Equals(m_MaxAxisTemperatureOrigin))
                {
                    return;
                }
                m_MaxAxisTemperatureOrigin = value;
                RaisePropertyChanged(() => MaxAxisTemperatureOrigin);
            }
        }

        public double MaxAxisTemperatureLocationOrigin
        {
            get { return m_MaxAxisTemperatureLocationOrigin; }
            set
            {
                if (value.Equals(m_MaxAxisTemperatureLocationOrigin))
                {
                    return;
                }
                m_MaxAxisTemperatureLocationOrigin = value;
                RaisePropertyChanged(() => MaxAxisTemperatureLocationOrigin);
            }
        }

        public double SensorNumOrigin
        {
            get { return m_SensorNumOrigin; }
            set
            {
                if (value.Equals(m_SensorNumOrigin))
                {
                    return;
                }
                m_SensorNumOrigin = value;
                RaisePropertyChanged(() => SensorNumOrigin);
            }
        }

        public double EnvironmentTemperatureOther
        {
            get { return m_EnvironmentTemperatureOther; }
            set
            {
                if (value.Equals(m_EnvironmentTemperatureOther))
                {
                    return;
                }
                m_EnvironmentTemperatureOther = value;
                RaisePropertyChanged(() => EnvironmentTemperatureOther);
            }
        }

        public double Axis1AxisBoxLeftOther
        {
            get { return m_Axis1AxisBoxLeftOther; }
            set
            {
                if (value.Equals(m_Axis1AxisBoxLeftOther))
                {
                    return;
                }
                m_Axis1AxisBoxLeftOther = value;
                RaisePropertyChanged(() => Axis1AxisBoxLeftOther);
            }
        }

        public double Axis1HeldBoxLeftOther
        {
            get { return m_Axis1HeldBoxLeftOther; }
            set
            {
                if (value.Equals(m_Axis1HeldBoxLeftOther))
                {
                    return;
                }
                m_Axis1HeldBoxLeftOther = value;
                RaisePropertyChanged(() => Axis1HeldBoxLeftOther);
            }
        }

        public double Axis1AxisBoxRighrOther
        {
            get { return m_Axis1AxisBoxRighrOther; }
            set
            {
                if (value.Equals(m_Axis1AxisBoxRighrOther))
                {
                    return;
                }
                m_Axis1AxisBoxRighrOther = value;
                RaisePropertyChanged(() => Axis1AxisBoxRighrOther);
            }
        }

        public double Axis1HeldBoxRighrOther
        {
            get { return m_Axis1HeldBoxRighrOther; }
            set
            {
                if (value.Equals(m_Axis1HeldBoxRighrOther))
                {
                    return;
                }
                m_Axis1HeldBoxRighrOther = value;
                RaisePropertyChanged(() => Axis1HeldBoxRighrOther);
            }
        }

        public double Axis2AxisBoxLeftOther
        {
            get { return m_Axis2AxisBoxLeftOther; }
            set
            {
                if (value.Equals(m_Axis2AxisBoxLeftOther))
                {
                    return;
                }
                m_Axis2AxisBoxLeftOther = value;
                RaisePropertyChanged(() => Axis2AxisBoxLeftOther);
            }
        }

        public double Axis2HeldBoxLeftOther
        {
            get { return m_Axis2HeldBoxLeftOther; }
            set
            {
                if (value.Equals(m_Axis2HeldBoxLeftOther))
                {
                    return;
                }
                m_Axis2HeldBoxLeftOther = value;
                RaisePropertyChanged(() => Axis2HeldBoxLeftOther);
            }
        }

        public double Axis2AxisBoxRighrOther
        {
            get { return m_Axis2AxisBoxRighrOther; }
            set
            {
                if (value.Equals(m_Axis2AxisBoxRighrOther))
                {
                    return;
                }
                m_Axis2AxisBoxRighrOther = value;
                RaisePropertyChanged(() => Axis2AxisBoxRighrOther);
            }
        }

        public double Axis2HeldBoxRighrOther
        {
            get { return m_Axis2HeldBoxRighrOther; }
            set
            {
                if (value.Equals(m_Axis2HeldBoxRighrOther))
                {
                    return;
                }
                m_Axis2HeldBoxRighrOther = value;
                RaisePropertyChanged(() => Axis2HeldBoxRighrOther);
            }
        }

        public double Axis3AxisBoxLeftOther
        {
            get { return m_Axis3AxisBoxLeftOther; }
            set
            {
                if (value.Equals(m_Axis3AxisBoxLeftOther))
                {
                    return;
                }
                m_Axis3AxisBoxLeftOther = value;
                RaisePropertyChanged(() => Axis3AxisBoxLeftOther);
            }
        }

        public double Axis3HeldBoxLeftOther
        {
            get { return m_Axis3HeldBoxLeftOther; }
            set
            {
                if (value.Equals(m_Axis3HeldBoxLeftOther))
                {
                    return;
                }
                m_Axis3HeldBoxLeftOther = value;
                RaisePropertyChanged(() => Axis3HeldBoxLeftOther);
            }
        }

        public double Axis3AxisBoxRighrOther
        {
            get { return m_Axis3AxisBoxRighrOther; }
            set
            {
                if (value.Equals(m_Axis3AxisBoxRighrOther))
                {
                    return;
                }
                m_Axis3AxisBoxRighrOther = value;
                RaisePropertyChanged(() => Axis3AxisBoxRighrOther);
            }
        }

        public double Axis3HeldBoxRighrOther
        {
            get { return m_Axis3HeldBoxRighrOther; }
            set
            {
                if (value.Equals(m_Axis3HeldBoxRighrOther))
                {
                    return;
                }
                m_Axis3HeldBoxRighrOther = value;
                RaisePropertyChanged(() => Axis3HeldBoxRighrOther);
            }
        }

        public double Axis4AxisBoxLeftOther
        {
            get { return m_Axis4AxisBoxLeftOther; }
            set
            {
                if (value.Equals(m_Axis4AxisBoxLeftOther))
                {
                    return;
                }
                m_Axis4AxisBoxLeftOther = value;
                RaisePropertyChanged(() => Axis4AxisBoxLeftOther);
            }
        }

        public double Axis4HeldBoxLeftOther
        {
            get { return m_Axis4HeldBoxLeftOther; }
            set
            {
                if (value.Equals(m_Axis4HeldBoxLeftOther))
                {
                    return;
                }
                m_Axis4HeldBoxLeftOther = value;
                RaisePropertyChanged(() => Axis4HeldBoxLeftOther);
            }
        }

        public double Axis4AxisBoxRighrOther
        {
            get { return m_Axis4AxisBoxRighrOther; }
            set
            {
                if (value.Equals(m_Axis4AxisBoxRighrOther))
                {
                    return;
                }
                m_Axis4AxisBoxRighrOther = value;
                RaisePropertyChanged(() => Axis4AxisBoxRighrOther);
            }
        }

        public double Axis4HeldBoxRighrOther
        {
            get { return m_Axis4HeldBoxRighrOther; }
            set
            {
                if (value.Equals(m_Axis4HeldBoxRighrOther))
                {
                    return;
                }
                m_Axis4HeldBoxRighrOther = value;
                RaisePropertyChanged(() => Axis4HeldBoxRighrOther);
            }
        }

        public double Axis5AxisBoxLeftOther
        {
            get { return m_Axis5AxisBoxLeftOther; }
            set
            {
                if (value.Equals(m_Axis5AxisBoxLeftOther))
                {
                    return;
                }
                m_Axis5AxisBoxLeftOther = value;
                RaisePropertyChanged(() => Axis5AxisBoxLeftOther);
            }
        }

        public double Axis5HeldBoxLeftOther
        {
            get { return m_Axis5HeldBoxLeftOther; }
            set
            {
                if (value.Equals(m_Axis5HeldBoxLeftOther))
                {
                    return;
                }
                m_Axis5HeldBoxLeftOther = value;
                RaisePropertyChanged(() => Axis5HeldBoxLeftOther);
            }
        }

        public double Axis5AxisBoxRighrOther
        {
            get { return m_Axis5AxisBoxRighrOther; }
            set
            {
                if (value.Equals(m_Axis5AxisBoxRighrOther))
                {
                    return;
                }
                m_Axis5AxisBoxRighrOther = value;
                RaisePropertyChanged(() => Axis5AxisBoxRighrOther);
            }
        }

        public double Axis5HeldBoxRighrOther
        {
            get { return m_Axis5HeldBoxRighrOther; }
            set
            {
                if (value.Equals(m_Axis5HeldBoxRighrOther))
                {
                    return;
                }
                m_Axis5HeldBoxRighrOther = value;
                RaisePropertyChanged(() => Axis5HeldBoxRighrOther);
            }
        }

        public double Axis6AxisBoxLeftOther
        {
            get { return m_Axis6AxisBoxLeftOther; }
            set
            {
                if (value.Equals(m_Axis6AxisBoxLeftOther))
                {
                    return;
                }
                m_Axis6AxisBoxLeftOther = value;
                RaisePropertyChanged(() => Axis6AxisBoxLeftOther);
            }
        }

        public double Axis6HeldBoxLeftOther
        {
            get { return m_Axis6HeldBoxLeftOther; }
            set
            {
                if (value.Equals(m_Axis6HeldBoxLeftOther))
                {
                    return;
                }
                m_Axis6HeldBoxLeftOther = value;
                RaisePropertyChanged(() => Axis6HeldBoxLeftOther);
            }
        }

        public double Axis6AxisBoxRighrOther
        {
            get { return m_Axis6AxisBoxRighrOther; }
            set
            {
                if (value.Equals(m_Axis6AxisBoxRighrOther))
                {
                    return;
                }
                m_Axis6AxisBoxRighrOther = value;
                RaisePropertyChanged(() => Axis6AxisBoxRighrOther);
            }
        }

        public double Axis6HeldBoxRighrOther
        {
            get { return m_Axis6HeldBoxRighrOther; }
            set
            {
                if (value.Equals(m_Axis6HeldBoxRighrOther))
                {
                    return;
                }
                m_Axis6HeldBoxRighrOther = value;
                RaisePropertyChanged(() => Axis6HeldBoxRighrOther);
            }
        }

        public double MaxAxisTemperatureOther
        {
            get { return m_MaxAxisTemperatureOther; }
            set
            {
                if (value.Equals(m_MaxAxisTemperatureOther))
                {
                    return;
                }
                m_MaxAxisTemperatureOther = value;
                RaisePropertyChanged(() => MaxAxisTemperatureOther);
            }
        }

        public double MaxAxisTemperatureLocationOther
        {
            get { return m_MaxAxisTemperatureLocationOther; }
            set
            {
                if (value.Equals(m_MaxAxisTemperatureLocationOther))
                {
                    return;
                }
                m_MaxAxisTemperatureLocationOther = value;
                RaisePropertyChanged(() => MaxAxisTemperatureLocationOther);
            }
        }

        public double SensorNumOther
        {
            get { return m_SensorNumOther; }
            set
            {
                if (value.Equals(m_SensorNumOther))
                {
                    return;
                }
                m_SensorNumOther = value;
                RaisePropertyChanged(() => SensorNumOther);
            }
        }
    }
}