using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.Model.Domain.System.WheelTemperature
{
    [Export]
    public class WheelTemperatureModel : NotificationObject
    {
        private double m_Car1Right1Wheel;
        private double m_Car1Right2Wheel;
        private double m_Car1Right3Wheel;
        private double m_Car1Right4Wheel;
        private double m_Car1Left1Wheel;
        private double m_Car1Left2Wheel;
        private double m_Car1Left3Wheel;
        private double m_Car1Left4Wheel;
        private double m_Car2Right1Wheel;
        private double m_Car2Right2Wheel;
        private double m_Car2Right3Wheel;
        private double m_Car2Right4Wheel;
        private double m_Car2Left1Wheel;
        private double m_Car2Left2Wheel;
        private double m_Car2Left3Wheel;
        private double m_Car2Left4Wheel;
        private double m_Car3Right1Wheel;
        private double m_Car3Right2Wheel;
        private double m_Car3Right3Wheel;
        private double m_Car3Right4Wheel;
        private double m_Car3Left1Wheel;
        private double m_Car3Left2Wheel;
        private double m_Car3Left3Wheel;
        private double m_Car3Left4Wheel;
        private double m_Car4Right1Wheel;
        private double m_Car4Right2Wheel;
        private double m_Car4Right3Wheel;
        private double m_Car4Right4Wheel;
        private double m_Car4Left1Wheel;
        private double m_Car4Left2Wheel;
        private double m_Car4Left3Wheel;
        private double m_Car4Left4Wheel;
        private double m_Car5Right1Wheel;
        private double m_Car5Right2Wheel;
        private double m_Car5Right3Wheel;
        private double m_Car5Right4Wheel;
        private double m_Car5Left1Wheel;
        private double m_Car5Left2Wheel;
        private double m_Car5Left3Wheel;
        private double m_Car5Left4Wheel;
        private double m_Car6Right1Wheel;
        private double m_Car6Right2Wheel;
        private double m_Car6Right3Wheel;
        private double m_Car6Right4Wheel;
        private double m_Car6Left1Wheel;
        private double m_Car6Left2Wheel;
        private double m_Car6Left3Wheel;
        private double m_Car6Left4Wheel;
        private double m_Car7Right1Wheel;
        private double m_Car7Right2Wheel;
        private double m_Car7Right3Wheel;
        private double m_Car7Right4Wheel;
        private double m_Car7Left1Wheel;
        private double m_Car7Left2Wheel;
        private double m_Car7Left3Wheel;
        private double m_Car7Left4Wheel;
        private double m_Car8Right1Wheel;
        private double m_Car8Right2Wheel;
        private double m_Car8Right3Wheel;
        private double m_Car8Right4Wheel;
        private double m_Car8Left1Wheel;
        private double m_Car8Left2Wheel;
        private double m_Car8Left3Wheel;
        private double m_Car8Left4Wheel;

        public double Car1Right1Wheel
        {
            get { return m_Car1Right1Wheel; }
            set
            {
                if (value.Equals(m_Car1Right1Wheel))
                {
                    return;
                }

                m_Car1Right1Wheel = value;
                RaisePropertyChanged(() => Car1Right1Wheel);
            }
        }

        public double Car1Right2Wheel
        {
            get { return m_Car1Right2Wheel; }
            set
            {
                if (value.Equals(m_Car1Right2Wheel))
                {
                    return;
                }

                m_Car1Right2Wheel = value;
                RaisePropertyChanged(() => Car1Right2Wheel);
            }
        }

        public double Car1Right3Wheel
        {
            get { return m_Car1Right3Wheel; }
            set
            {
                if (value.Equals(m_Car1Right3Wheel))
                {
                    return;
                }

                m_Car1Right3Wheel = value;
                RaisePropertyChanged(() => Car1Right3Wheel);
            }
        }

        public double Car1Right4Wheel
        {
            get { return m_Car1Right4Wheel; }
            set
            {
                if (value.Equals(m_Car1Right4Wheel))
                {
                    return;
                }

                m_Car1Right4Wheel = value;
                RaisePropertyChanged(() => Car1Right4Wheel);
            }
        }

        public double Car1Left1Wheel
        {
            get { return m_Car1Left1Wheel; }
            set
            {
                if (value.Equals(m_Car1Left1Wheel))
                {
                    return;
                }

                m_Car1Left1Wheel = value;
                RaisePropertyChanged(() => Car1Left1Wheel);
            }
        }

        public double Car1Left2Wheel
        {
            get { return m_Car1Left2Wheel; }
            set
            {
                if (value.Equals(m_Car1Left2Wheel))
                {
                    return;
                }

                m_Car1Left2Wheel = value;
                RaisePropertyChanged(() => Car1Left2Wheel);
            }
        }

        public double Car1Left3Wheel
        {
            get { return m_Car1Left3Wheel; }
            set
            {
                if (value.Equals(m_Car1Left3Wheel))
                {
                    return;
                }

                m_Car1Left3Wheel = value;
                RaisePropertyChanged(() => Car1Left3Wheel);
            }
        }

        public double Car1Left4Wheel
        {
            get { return m_Car1Left4Wheel; }
            set
            {
                if (value.Equals(m_Car1Left4Wheel))
                {
                    return;
                }

                m_Car1Left4Wheel = value;
                RaisePropertyChanged(() => Car1Left4Wheel);
            }
        }

        public double Car2Right1Wheel
        {
            get { return m_Car2Right1Wheel; }
            set
            {
                if (value.Equals(m_Car2Right1Wheel))
                {
                    return;
                }

                m_Car2Right1Wheel = value;
                RaisePropertyChanged(() => Car2Right1Wheel);
            }
        }

        public double Car2Right2Wheel
        {
            get { return m_Car2Right2Wheel; }
            set
            {
                if (value.Equals(m_Car2Right2Wheel))
                {
                    return;
                }

                m_Car2Right2Wheel = value;
                RaisePropertyChanged(() => Car2Right2Wheel);
            }
        }

        public double Car2Right3Wheel
        {
            get { return m_Car2Right3Wheel; }
            set
            {
                if (value.Equals(m_Car2Right3Wheel))
                {
                    return;
                }

                m_Car2Right3Wheel = value;
                RaisePropertyChanged(() => Car2Right3Wheel);
            }
        }

        public double Car2Right4Wheel
        {
            get { return m_Car2Right4Wheel; }
            set
            {
                if (value.Equals(m_Car2Right4Wheel))
                {
                    return;
                }

                m_Car2Right4Wheel = value;
                RaisePropertyChanged(() => Car2Right4Wheel);
            }
        }

        public double Car2Left1Wheel
        {
            get { return m_Car2Left1Wheel; }
            set
            {
                if (value.Equals(m_Car2Left1Wheel))
                {
                    return;
                }

                m_Car2Left1Wheel = value;
                RaisePropertyChanged(() => Car2Left1Wheel);
            }
        }

        public double Car2Left2Wheel
        {
            get { return m_Car2Left2Wheel; }
            set
            {
                if (value.Equals(m_Car2Left2Wheel))
                {
                    return;
                }

                m_Car2Left2Wheel = value;
                RaisePropertyChanged(() => Car2Left2Wheel);
            }
        }

        public double Car2Left3Wheel
        {
            get { return m_Car2Left3Wheel; }
            set
            {
                if (value.Equals(m_Car2Left3Wheel))
                {
                    return;
                }

                m_Car2Left3Wheel = value;
                RaisePropertyChanged(() => Car2Left3Wheel);
            }
        }

        public double Car2Left4Wheel
        {
            get { return m_Car2Left4Wheel; }
            set
            {
                if (value.Equals(m_Car2Left4Wheel))
                {
                    return;
                }

                m_Car2Left4Wheel = value;
                RaisePropertyChanged(() => Car2Left4Wheel);
            }
        }

        public double Car3Right1Wheel
        {
            get { return m_Car3Right1Wheel; }
            set
            {
                if (value.Equals(m_Car3Right1Wheel))
                {
                    return;
                }

                m_Car3Right1Wheel = value;
                RaisePropertyChanged(() => Car3Right1Wheel);
            }
        }

        public double Car3Right2Wheel
        {
            get { return m_Car3Right2Wheel; }
            set
            {
                if (value.Equals(m_Car3Right2Wheel))
                {
                    return;
                }

                m_Car3Right2Wheel = value;
                RaisePropertyChanged(() => Car3Right2Wheel);
            }
        }

        public double Car3Right3Wheel
        {
            get { return m_Car3Right3Wheel; }
            set
            {
                if (value.Equals(m_Car3Right3Wheel))
                {
                    return;
                }

                m_Car3Right3Wheel = value;
                RaisePropertyChanged(() => Car3Right3Wheel);
            }
        }

        public double Car3Right4Wheel
        {
            get { return m_Car3Right4Wheel; }
            set
            {
                if (value.Equals(m_Car3Right4Wheel))
                {
                    return;
                }

                m_Car3Right4Wheel = value;
                RaisePropertyChanged(() => Car3Right4Wheel);
            }
        }

        public double Car3Left1Wheel
        {
            get { return m_Car3Left1Wheel; }
            set
            {
                if (value.Equals(m_Car3Left1Wheel))
                {
                    return;
                }

                m_Car3Left1Wheel = value;
                RaisePropertyChanged(() => Car3Left1Wheel);
            }
        }

        public double Car3Left2Wheel
        {
            get { return m_Car3Left2Wheel; }
            set
            {
                if (value.Equals(m_Car3Left2Wheel))
                {
                    return;
                }

                m_Car3Left2Wheel = value;
                RaisePropertyChanged(() => Car3Left2Wheel);
            }
        }

        public double Car3Left3Wheel
        {
            get { return m_Car3Left3Wheel; }
            set
            {
                if (value.Equals(m_Car3Left3Wheel))
                {
                    return;
                }

                m_Car3Left3Wheel = value;
                RaisePropertyChanged(() => Car3Left3Wheel);
            }
        }

        public double Car3Left4Wheel
        {
            get { return m_Car3Left4Wheel; }
            set
            {
                if (value.Equals(m_Car3Left4Wheel))
                {
                    return;
                }

                m_Car3Left4Wheel = value;
                RaisePropertyChanged(() => Car3Left4Wheel);
            }
        }

        public double Car4Right1Wheel
        {
            get { return m_Car4Right1Wheel; }
            set
            {
                if (value.Equals(m_Car4Right1Wheel))
                {
                    return;
                }

                m_Car4Right1Wheel = value;
                RaisePropertyChanged(() => Car4Right1Wheel);
            }
        }

        public double Car4Right2Wheel
        {
            get { return m_Car4Right2Wheel; }
            set
            {
                if (value.Equals(m_Car4Right2Wheel))
                {
                    return;
                }

                m_Car4Right2Wheel = value;
                RaisePropertyChanged(() => Car4Right2Wheel);
            }
        }

        public double Car4Right3Wheel
        {
            get { return m_Car4Right3Wheel; }
            set
            {
                if (value.Equals(m_Car4Right3Wheel))
                {
                    return;
                }

                m_Car4Right3Wheel = value;
                RaisePropertyChanged(() => Car4Right3Wheel);
            }
        }

        public double Car4Right4Wheel
        {
            get { return m_Car4Right4Wheel; }
            set
            {
                if (value.Equals(m_Car4Right4Wheel))
                {
                    return;
                }

                m_Car4Right4Wheel = value;
                RaisePropertyChanged(() => Car4Right4Wheel);
            }
        }

        public double Car4Left1Wheel
        {
            get { return m_Car4Left1Wheel; }
            set
            {
                if (value.Equals(m_Car4Left1Wheel))
                {
                    return;
                }

                m_Car4Left1Wheel = value;
                RaisePropertyChanged(() => Car4Left1Wheel);
            }
        }

        public double Car4Left2Wheel
        {
            get { return m_Car4Left2Wheel; }
            set
            {
                if (value.Equals(m_Car4Left2Wheel))
                {
                    return;
                }

                m_Car4Left2Wheel = value;
                RaisePropertyChanged(() => Car4Left2Wheel);
            }
        }

        public double Car4Left3Wheel
        {
            get { return m_Car4Left3Wheel; }
            set
            {
                if (value.Equals(m_Car4Left3Wheel))
                {
                    return;
                }

                m_Car4Left3Wheel = value;
                RaisePropertyChanged(() => Car4Left3Wheel);
            }
        }

        public double Car4Left4Wheel
        {
            get { return m_Car4Left4Wheel; }
            set
            {
                if (value.Equals(m_Car4Left4Wheel))
                {
                    return;
                }

                m_Car4Left4Wheel = value;
                RaisePropertyChanged(() => Car4Left4Wheel);
            }
        }

        public double Car5Right1Wheel
        {
            get { return m_Car5Right1Wheel; }
            set
            {
                if (value.Equals(m_Car5Right1Wheel))
                {
                    return;
                }

                m_Car5Right1Wheel = value;
                RaisePropertyChanged(() => Car5Right1Wheel);
            }
        }

        public double Car5Right2Wheel
        {
            get { return m_Car5Right2Wheel; }
            set
            {
                if (value.Equals(m_Car5Right2Wheel))
                {
                    return;
                }

                m_Car5Right2Wheel = value;
                RaisePropertyChanged(() => Car5Right2Wheel);
            }
        }

        public double Car5Right3Wheel
        {
            get { return m_Car5Right3Wheel; }
            set
            {
                if (value.Equals(m_Car5Right3Wheel))
                {
                    return;
                }

                m_Car5Right3Wheel = value;
                RaisePropertyChanged(() => Car5Right3Wheel);
            }
        }

        public double Car5Right4Wheel
        {
            get { return m_Car5Right4Wheel; }
            set
            {
                if (value.Equals(m_Car5Right4Wheel))
                {
                    return;
                }

                m_Car5Right4Wheel = value;
                RaisePropertyChanged(() => Car5Right4Wheel);
            }
        }

        public double Car5Left1Wheel
        {
            get { return m_Car5Left1Wheel; }
            set
            {
                if (value.Equals(m_Car5Left1Wheel))
                {
                    return;
                }

                m_Car5Left1Wheel = value;
                RaisePropertyChanged(() => Car5Left1Wheel);
            }
        }

        public double Car5Left2Wheel
        {
            get { return m_Car5Left2Wheel; }
            set
            {
                if (value.Equals(m_Car5Left2Wheel))
                {
                    return;
                }

                m_Car5Left2Wheel = value;
                RaisePropertyChanged(() => Car5Left2Wheel);
            }
        }

        public double Car5Left3Wheel
        {
            get { return m_Car5Left3Wheel; }
            set
            {
                if (value.Equals(m_Car5Left3Wheel))
                {
                    return;
                }

                m_Car5Left3Wheel = value;
                RaisePropertyChanged(() => Car5Left3Wheel);
            }
        }

        public double Car5Left4Wheel
        {
            get { return m_Car5Left4Wheel; }
            set
            {
                if (value.Equals(m_Car5Left4Wheel))
                {
                    return;
                }

                m_Car5Left4Wheel = value;
                RaisePropertyChanged(() => Car5Left4Wheel);
            }
        }

        public double Car6Right1Wheel
        {
            get { return m_Car6Right1Wheel; }
            set
            {
                if (value.Equals(m_Car6Right1Wheel))
                {
                    return;
                }

                m_Car6Right1Wheel = value;
                RaisePropertyChanged(() => Car6Right1Wheel);
            }
        }

        public double Car6Right2Wheel
        {
            get { return m_Car6Right2Wheel; }
            set
            {
                if (value.Equals(m_Car6Right2Wheel))
                {
                    return;
                }

                m_Car6Right2Wheel = value;
                RaisePropertyChanged(() => Car6Right2Wheel);
            }
        }

        public double Car6Right3Wheel
        {
            get { return m_Car6Right3Wheel; }
            set
            {
                if (value.Equals(m_Car6Right3Wheel))
                {
                    return;
                }

                m_Car6Right3Wheel = value;
                RaisePropertyChanged(() => Car6Right3Wheel);
            }
        }

        public double Car6Right4Wheel
        {
            get { return m_Car6Right4Wheel; }
            set
            {
                if (value.Equals(m_Car6Right4Wheel))
                {
                    return;
                }

                m_Car6Right4Wheel = value;
                RaisePropertyChanged(() => Car6Right4Wheel);
            }
        }

        public double Car6Left1Wheel
        {
            get { return m_Car6Left1Wheel; }
            set
            {
                if (value.Equals(m_Car6Left1Wheel))
                {
                    return;
                }

                m_Car6Left1Wheel = value;
                RaisePropertyChanged(() => Car6Left1Wheel);
            }
        }

        public double Car6Left2Wheel
        {
            get { return m_Car6Left2Wheel; }
            set
            {
                if (value.Equals(m_Car6Left2Wheel))
                {
                    return;
                }

                m_Car6Left2Wheel = value;
                RaisePropertyChanged(() => Car6Left2Wheel);
            }
        }

        public double Car6Left3Wheel
        {
            get { return m_Car6Left3Wheel; }
            set
            {
                if (value.Equals(m_Car6Left3Wheel))
                {
                    return;
                }

                m_Car6Left3Wheel = value;
                RaisePropertyChanged(() => Car6Left3Wheel);
            }
        }

        public double Car6Left4Wheel
        {
            get { return m_Car6Left4Wheel; }
            set
            {
                if (value.Equals(m_Car6Left4Wheel))
                {
                    return;
                }

                m_Car6Left4Wheel = value;
                RaisePropertyChanged(() => Car6Left4Wheel);
            }
        }

        public double Car7Right1Wheel
        {
            get { return m_Car7Right1Wheel; }
            set
            {
                if (value.Equals(m_Car7Right1Wheel))
                {
                    return;
                }

                m_Car7Right1Wheel = value;
                RaisePropertyChanged(() => Car7Right1Wheel);
            }
        }

        public double Car7Right2Wheel
        {
            get { return m_Car7Right2Wheel; }
            set
            {
                if (value.Equals(m_Car7Right2Wheel))
                {
                    return;
                }

                m_Car7Right2Wheel = value;
                RaisePropertyChanged(() => Car7Right2Wheel);
            }
        }

        public double Car7Right3Wheel
        {
            get { return m_Car7Right3Wheel; }
            set
            {
                if (value.Equals(m_Car7Right3Wheel))
                {
                    return;
                }

                m_Car7Right3Wheel = value;
                RaisePropertyChanged(() => Car7Right3Wheel);
            }
        }

        public double Car7Right4Wheel
        {
            get { return m_Car7Right4Wheel; }
            set
            {
                if (value.Equals(m_Car7Right4Wheel))
                {
                    return;
                }

                m_Car7Right4Wheel = value;
                RaisePropertyChanged(() => Car7Right4Wheel);
            }
        }

        public double Car7Left1Wheel
        {
            get { return m_Car7Left1Wheel; }
            set
            {
                if (value.Equals(m_Car7Left1Wheel))
                {
                    return;
                }

                m_Car7Left1Wheel = value;
                RaisePropertyChanged(() => Car7Left1Wheel);
            }
        }

        public double Car7Left2Wheel
        {
            get { return m_Car7Left2Wheel; }
            set
            {
                if (value.Equals(m_Car7Left2Wheel))
                {
                    return;
                }

                m_Car7Left2Wheel = value;
                RaisePropertyChanged(() => Car7Left2Wheel);
            }
        }

        public double Car7Left3Wheel
        {
            get { return m_Car7Left3Wheel; }
            set
            {
                if (value.Equals(m_Car7Left3Wheel))
                {
                    return;
                }

                m_Car7Left3Wheel = value;
                RaisePropertyChanged(() => Car7Left3Wheel);
            }
        }

        public double Car7Left4Wheel
        {
            get { return m_Car7Left4Wheel; }
            set
            {
                if (value.Equals(m_Car7Left4Wheel))
                {
                    return;
                }

                m_Car7Left4Wheel = value;
                RaisePropertyChanged(() => Car7Left4Wheel);
            }
        }

        public double Car8Right1Wheel
        {
            get { return m_Car8Right1Wheel; }
            set
            {
                if (value.Equals(m_Car8Right1Wheel))
                {
                    return;
                }

                m_Car8Right1Wheel = value;
                RaisePropertyChanged(() => Car8Right1Wheel);
            }
        }

        public double Car8Right2Wheel
        {
            get { return m_Car8Right2Wheel; }
            set
            {
                if (value.Equals(m_Car8Right2Wheel))
                {
                    return;
                }

                m_Car8Right2Wheel = value;
                RaisePropertyChanged(() => Car8Right2Wheel);
            }
        }

        public double Car8Right3Wheel
        {
            get { return m_Car8Right3Wheel; }
            set
            {
                if (value.Equals(m_Car8Right3Wheel))
                {
                    return;
                }

                m_Car8Right3Wheel = value;
                RaisePropertyChanged(() => Car8Right3Wheel);
            }
        }

        public double Car8Right4Wheel
        {
            get { return m_Car8Right4Wheel; }
            set
            {
                if (value.Equals(m_Car8Right4Wheel))
                {
                    return;
                }

                m_Car8Right4Wheel = value;
                RaisePropertyChanged(() => Car8Right4Wheel);
            }
        }

        public double Car8Left1Wheel
        {
            get { return m_Car8Left1Wheel; }
            set
            {
                if (value.Equals(m_Car8Left1Wheel))
                {
                    return;
                }

                m_Car8Left1Wheel = value;
                RaisePropertyChanged(() => Car8Left1Wheel);
            }
        }

        public double Car8Left2Wheel
        {
            get { return m_Car8Left2Wheel; }
            set
            {
                if (value.Equals(m_Car8Left2Wheel))
                {
                    return;
                }

                m_Car8Left2Wheel = value;
                RaisePropertyChanged(() => Car8Left2Wheel);
            }
        }

        public double Car8Left3Wheel
        {
            get { return m_Car8Left3Wheel; }
            set
            {
                if (value.Equals(m_Car8Left3Wheel))
                {
                    return;
                }

                m_Car8Left3Wheel = value;
                RaisePropertyChanged(() => Car8Left3Wheel);
            }
        }

        public double Car8Left4Wheel
        {
            get { return m_Car8Left4Wheel; }
            set
            {
                if (value.Equals(m_Car8Left4Wheel))
                {
                    return;
                }

                m_Car8Left4Wheel = value;
                RaisePropertyChanged(() => Car8Left4Wheel);
            }
        }
    }
}
