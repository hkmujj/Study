using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.Model.Domain.System.GearboxMotorTemperature
{
    [Export]
    public class GearboxMotorTemperatureModel : NotificationObject
    {
        private double m_Car1DriveShaftEnd1;
        private double m_Car1DriveShaftEnd2;
        private double m_Car1DriveShaftEnd3;
        private double m_Car1DriveShaftEnd4;
        private double m_Car3DriveShaftEnd1;
        private double m_Car3DriveShaftEnd2;
        private double m_Car3DriveShaftEnd3;
        private double m_Car3DriveShaftEnd4;
        private double m_Car6DriveShaftEnd1;
        private double m_Car6DriveShaftEnd2;
        private double m_Car6DriveShaftEnd3;
        private double m_Car6DriveShaftEnd4;
        private double m_Car8DriveShaftEnd1;
        private double m_Car8DriveShaftEnd2;
        private double m_Car8DriveShaftEnd3;
        private double m_Car8DriveShaftEnd4;
        private double m_Car1NoDriveShaftEnd1;
        private double m_Car1NoDriveShaftEnd2;
        private double m_Car1NoDriveShaftEnd3;
        private double m_Car1NoDriveShaftEnd4;
        private double m_Car3NoDriveShaftEnd1;
        private double m_Car3NoDriveShaftEnd2;
        private double m_Car3NoDriveShaftEnd3;
        private double m_Car3NoDriveShaftEnd4;
        private double m_Car6NoDriveShaftEnd1;
        private double m_Car6NoDriveShaftEnd2;
        private double m_Car6NoDriveShaftEnd3;
        private double m_Car6NoDriveShaftEnd4;
        private double m_Car8NoDriveShaftEnd1;
        private double m_Car8NoDriveShaftEnd2;
        private double m_Car8NoDriveShaftEnd3;
        private double m_Car8NoDriveShaftEnd4;
        private double m_Car1BigGearMotor1;
        private double m_Car1BigGearMotor2;
        private double m_Car1BigGearMotor3;
        private double m_Car1BigGearMotor4;
        private double m_Car3BigGearMotor1;
        private double m_Car3BigGearMotor2;
        private double m_Car3BigGearMotor3;
        private double m_Car3BigGearMotor4;
        private double m_Car6BigGearMotor1;
        private double m_Car6BigGearMotor2;
        private double m_Car6BigGearMotor3;
        private double m_Car6BigGearMotor4;
        private double m_Car8BigGearMotor1;
        private double m_Car8BigGearMotor2;
        private double m_Car8BigGearMotor3;
        private double m_Car8BigGearMotor4;
        private double m_Car1BigGearWheel1;
        private double m_Car1BigGearWheel2;
        private double m_Car1BigGearWheel3;
        private double m_Car1BigGearWheel4;
        private double m_Car3BigGearWheel1;
        private double m_Car3BigGearWheel2;
        private double m_Car3BigGearWheel3;
        private double m_Car3BigGearWheel4;
        private double m_Car6BigGearWheel1;
        private double m_Car6BigGearWheel2;
        private double m_Car6BigGearWheel3;
        private double m_Car6BigGearWheel4;
        private double m_Car8BigGearWheel1;
        private double m_Car8BigGearWheel2;
        private double m_Car8BigGearWheel3;
        private double m_Car8BigGearWheel4;
        private double m_Car1SmallGearMotor1;
        private double m_Car1SmallGearMotor2;
        private double m_Car1SmallGearMotor3;
        private double m_Car1SmallGearMotor4;
        private double m_Car3SmallGearMotor1;
        private double m_Car3SmallGearMotor2;
        private double m_Car3SmallGearMotor3;
        private double m_Car3SmallGearMotor4;
        private double m_Car6SmallGearMotor1;
        private double m_Car6SmallGearMotor2;
        private double m_Car6SmallGearMotor3;
        private double m_Car6SmallGearMotor4;
        private double m_Car8SmallGearMotor1;
        private double m_Car8SmallGearMotor2;
        private double m_Car8SmallGearMotor3;
        private double m_Car8SmallGearMotor4;
        private double m_Car1SmallGearWheel1;
        private double m_Car1SmallGearWheel2;
        private double m_Car1SmallGearWheel3;
        private double m_Car1SmallGearWheel4;
        private double m_Car3SmallGearWheel1;
        private double m_Car3SmallGearWheel2;
        private double m_Car3SmallGearWheel3;
        private double m_Car3SmallGearWheel4;
        private double m_Car6SmallGearWheel1;
        private double m_Car6SmallGearWheel2;
        private double m_Car6SmallGearWheel3;
        private double m_Car6SmallGearWheel4;
        private double m_Car8SmallGearWheel1;
        private double m_Car8SmallGearWheel2;
        private double m_Car8SmallGearWheel3;
        private double m_Car8SmallGearWheel4;
        private double m_Car1MotorStator1;
        private double m_Car1MotorStator2;
        private double m_Car1MotorStator3;
        private double m_Car1MotorStator4;
        private double m_Car3MotorStator1;
        private double m_Car3MotorStator2;
        private double m_Car3MotorStator3;
        private double m_Car3MotorStator4;
        private double m_Car6MotorStator1;
        private double m_Car6MotorStator2;
        private double m_Car6MotorStator3;
        private double m_Car6MotorStator4;
        private double m_Car8MotorStator1;
        private double m_Car8MotorStator2;
        private double m_Car8MotorStator3;
        private double m_Car8MotorStator4;


        public double Car1DriveShaftEnd1
        {
            get { return m_Car1DriveShaftEnd1; }
            set
            {
                if (value.Equals(m_Car1DriveShaftEnd1))
                {
                    return;
                }

                m_Car1DriveShaftEnd1 = value;
                RaisePropertyChanged(() => Car1DriveShaftEnd1);
            }
        }

        public double Car1DriveShaftEnd2
        {
            get { return m_Car1DriveShaftEnd2; }
            set
            {
                if (value.Equals(m_Car1DriveShaftEnd2))
                {
                    return;
                }

                m_Car1DriveShaftEnd2 = value;
                RaisePropertyChanged(() => Car1DriveShaftEnd2);
            }
        }

        public double Car1DriveShaftEnd3
        {
            get { return m_Car1DriveShaftEnd3; }
            set
            {
                if (value.Equals(m_Car1DriveShaftEnd3))
                {
                    return;
                }

                m_Car1DriveShaftEnd3 = value;
                RaisePropertyChanged(() => Car1DriveShaftEnd3);
            }
        }

        public double Car1DriveShaftEnd4
        {
            get { return m_Car1DriveShaftEnd4; }
            set
            {
                if (value.Equals(m_Car1DriveShaftEnd4))
                {
                    return;
                }

                m_Car1DriveShaftEnd4 = value;
                RaisePropertyChanged(() => Car1DriveShaftEnd4);
            }
        }

        public double Car6DriveShaftEnd1
        {
            get { return m_Car6DriveShaftEnd1; }
            set
            {
                if (value.Equals(m_Car6DriveShaftEnd1))
                {
                    return;
                }

                m_Car6DriveShaftEnd1 = value;
                RaisePropertyChanged(() => Car6DriveShaftEnd1);
            }
        }

        public double Car6DriveShaftEnd2
        {
            get { return m_Car6DriveShaftEnd2; }
            set
            {
                if (value.Equals(m_Car6DriveShaftEnd2))
                {
                    return;
                }

                m_Car6DriveShaftEnd2 = value;
                RaisePropertyChanged(() => Car6DriveShaftEnd2);
            }
        }

        public double Car6DriveShaftEnd3
        {
            get { return m_Car6DriveShaftEnd3; }
            set
            {
                if (value.Equals(m_Car6DriveShaftEnd3))
                {
                    return;
                }

                m_Car6DriveShaftEnd3 = value;
                RaisePropertyChanged(() => Car6DriveShaftEnd3);
            }
        }

        public double Car6DriveShaftEnd4
        {
            get { return m_Car6DriveShaftEnd4; }
            set
            {
                if (value.Equals(m_Car6DriveShaftEnd4))
                {
                    return;
                }

                m_Car6DriveShaftEnd4 = value;
                RaisePropertyChanged(() => Car6DriveShaftEnd4);
            }
        }

        public double Car3DriveShaftEnd1
        {
            get { return m_Car3DriveShaftEnd1; }
            set
            {
                if (value.Equals(m_Car3DriveShaftEnd1))
                {
                    return;
                }

                m_Car3DriveShaftEnd1 = value;
                RaisePropertyChanged(() => Car3DriveShaftEnd1);
            }
        }

        public double Car3DriveShaftEnd2
        {
            get { return m_Car3DriveShaftEnd2; }
            set
            {
                if (value.Equals(m_Car3DriveShaftEnd2))
                {
                    return;
                }

                m_Car3DriveShaftEnd2 = value;
                RaisePropertyChanged(() => Car3DriveShaftEnd2);
            }
        }

        public double Car3DriveShaftEnd3
        {
            get { return m_Car3DriveShaftEnd3; }
            set
            {
                if (value.Equals(m_Car3DriveShaftEnd3))
                {
                    return;
                }

                m_Car3DriveShaftEnd3 = value;
                RaisePropertyChanged(() => Car3DriveShaftEnd3);
            }
        }

        public double Car3DriveShaftEnd4
        {
            get { return m_Car3DriveShaftEnd4; }
            set
            {
                if (value.Equals(m_Car3DriveShaftEnd4))
                {
                    return;
                }

                m_Car3DriveShaftEnd4 = value;
                RaisePropertyChanged(() => Car3DriveShaftEnd4);
            }
        }

        public double Car8DriveShaftEnd1
        {
            get { return m_Car8DriveShaftEnd1; }
            set
            {
                if (value.Equals(m_Car8DriveShaftEnd1))
                {
                    return;
                }

                m_Car8DriveShaftEnd1 = value;
                RaisePropertyChanged(() => Car8DriveShaftEnd1);
            }
        }

        public double Car8DriveShaftEnd2
        {
            get { return m_Car8DriveShaftEnd2; }
            set
            {
                if (value.Equals(m_Car8DriveShaftEnd2))
                {
                    return;
                }

                m_Car8DriveShaftEnd2 = value;
                RaisePropertyChanged(() => Car8DriveShaftEnd2);
            }
        }

        public double Car8DriveShaftEnd3
        {
            get { return m_Car8DriveShaftEnd3; }
            set
            {
                if (value.Equals(m_Car8DriveShaftEnd3))
                {
                    return;
                }

                m_Car8DriveShaftEnd3 = value;
                RaisePropertyChanged(() => Car8DriveShaftEnd3);
            }
        }

        public double Car8DriveShaftEnd4
        {
            get { return m_Car8DriveShaftEnd4; }
            set
            {
                if (value.Equals(m_Car8DriveShaftEnd4))
                {
                    return;
                }

                m_Car8DriveShaftEnd4 = value;
                RaisePropertyChanged(() => Car8DriveShaftEnd4);
            }
        }

        public double Car1NoDriveShaftEnd1
        {
            get { return m_Car1NoDriveShaftEnd1; }
            set
            {
                if (value.Equals(m_Car1NoDriveShaftEnd1))
                {
                    return;
                }

                m_Car1NoDriveShaftEnd1 = value;
                RaisePropertyChanged(() => Car1NoDriveShaftEnd1);
            }
        }

        public double Car1NoDriveShaftEnd2
        {
            get { return m_Car1NoDriveShaftEnd2; }
            set
            {
                if (value.Equals(m_Car1NoDriveShaftEnd2))
                {
                    return;
                }

                m_Car1NoDriveShaftEnd2 = value;
                RaisePropertyChanged(() => Car1NoDriveShaftEnd2);
            }
        }

        public double Car1NoDriveShaftEnd3
        {
            get { return m_Car1NoDriveShaftEnd3; }
            set
            {
                if (value.Equals(m_Car1NoDriveShaftEnd3))
                {
                    return;
                }

                m_Car1NoDriveShaftEnd3 = value;
                RaisePropertyChanged(() => Car1NoDriveShaftEnd3);
            }
        }

        public double Car1NoDriveShaftEnd4
        {
            get { return m_Car1NoDriveShaftEnd4; }
            set
            {
                if (value.Equals(m_Car1NoDriveShaftEnd4))
                {
                    return;
                }

                m_Car1NoDriveShaftEnd4 = value;
                RaisePropertyChanged(() => Car1NoDriveShaftEnd4);
            }
        }

        public double Car6NoDriveShaftEnd1
        {
            get { return m_Car6NoDriveShaftEnd1; }
            set
            {
                if (value.Equals(m_Car6NoDriveShaftEnd1))
                {
                    return;
                }

                m_Car6NoDriveShaftEnd1 = value;
                RaisePropertyChanged(() => Car6NoDriveShaftEnd1);
            }
        }

        public double Car6NoDriveShaftEnd2
        {
            get { return m_Car6NoDriveShaftEnd2; }
            set
            {
                if (value.Equals(m_Car6NoDriveShaftEnd2))
                {
                    return;
                }

                m_Car6NoDriveShaftEnd2 = value;
                RaisePropertyChanged(() => Car6NoDriveShaftEnd2);
            }
        }

        public double Car6NoDriveShaftEnd3
        {
            get { return m_Car6NoDriveShaftEnd3; }
            set
            {
                if (value.Equals(m_Car6NoDriveShaftEnd3))
                {
                    return;
                }

                m_Car6NoDriveShaftEnd3 = value;
                RaisePropertyChanged(() => Car6NoDriveShaftEnd3);
            }
        }

        public double Car6NoDriveShaftEnd4
        {
            get { return m_Car6NoDriveShaftEnd4; }
            set
            {
                if (value.Equals(m_Car6NoDriveShaftEnd4))
                {
                    return;
                }

                m_Car6NoDriveShaftEnd4 = value;
                RaisePropertyChanged(() => Car6NoDriveShaftEnd4);
            }
        }

        public double Car3NoDriveShaftEnd1
        {
            get { return m_Car3NoDriveShaftEnd1; }
            set
            {
                if (value.Equals(m_Car3NoDriveShaftEnd1))
                {
                    return;
                }

                m_Car3NoDriveShaftEnd1 = value;
                RaisePropertyChanged(() => Car3NoDriveShaftEnd1);
            }
        }

        public double Car3NoDriveShaftEnd2
        {
            get { return m_Car3NoDriveShaftEnd2; }
            set
            {
                if (value.Equals(m_Car3NoDriveShaftEnd2))
                {
                    return;
                }

                m_Car3NoDriveShaftEnd2 = value;
                RaisePropertyChanged(() => Car3NoDriveShaftEnd2);
            }
        }

        public double Car3NoDriveShaftEnd3
        {
            get { return m_Car3NoDriveShaftEnd3; }
            set
            {
                if (value.Equals(m_Car3NoDriveShaftEnd3))
                {
                    return;
                }

                m_Car3NoDriveShaftEnd3 = value;
                RaisePropertyChanged(() => Car3NoDriveShaftEnd3);
            }
        }

        public double Car3NoDriveShaftEnd4
        {
            get { return m_Car3NoDriveShaftEnd4; }
            set
            {
                if (value.Equals(m_Car3NoDriveShaftEnd4))
                {
                    return;
                }

                m_Car3NoDriveShaftEnd4 = value;
                RaisePropertyChanged(() => Car3NoDriveShaftEnd4);
            }
        }

        public double Car8NoDriveShaftEnd1
        {
            get { return m_Car8NoDriveShaftEnd1; }
            set
            {
                if (value.Equals(m_Car8NoDriveShaftEnd1))
                {
                    return;
                }

                m_Car8NoDriveShaftEnd1 = value;
                RaisePropertyChanged(() => Car8NoDriveShaftEnd1);
            }
        }

        public double Car8NoDriveShaftEnd2
        {
            get { return m_Car8NoDriveShaftEnd2; }
            set
            {
                if (value.Equals(m_Car8NoDriveShaftEnd2))
                {
                    return;
                }

                m_Car8NoDriveShaftEnd2 = value;
                RaisePropertyChanged(() => Car8NoDriveShaftEnd2);
            }
        }

        public double Car8NoDriveShaftEnd3
        {
            get { return m_Car8NoDriveShaftEnd3; }
            set
            {
                if (value.Equals(m_Car8NoDriveShaftEnd3))
                {
                    return;
                }

                m_Car8NoDriveShaftEnd3 = value;
                RaisePropertyChanged(() => Car8NoDriveShaftEnd3);
            }
        }

        public double Car8NoDriveShaftEnd4
        {
            get { return m_Car8NoDriveShaftEnd4; }
            set
            {
                if (value.Equals(m_Car8NoDriveShaftEnd4))
                {
                    return;
                }

                m_Car8NoDriveShaftEnd4 = value;
                RaisePropertyChanged(() => Car8NoDriveShaftEnd4);
            }
        }

        public double Car1BigGearMotor1
        {
            get { return m_Car1BigGearMotor1; }
            set
            {
                if (value.Equals(m_Car1BigGearMotor1))
                {
                    return;
                }

                m_Car1BigGearMotor1 = value;
                RaisePropertyChanged(() => Car1BigGearMotor1);
            }
        }

        public double Car1BigGearMotor2
        {
            get { return m_Car1BigGearMotor2; }
            set
            {
                if (value.Equals(m_Car1BigGearMotor2))
                {
                    return;
                }

                m_Car1BigGearMotor2 = value;
                RaisePropertyChanged(() => Car1BigGearMotor2);
            }
        }

        public double Car1BigGearMotor3
        {
            get { return m_Car1BigGearMotor3; }
            set
            {
                if (value.Equals(m_Car1BigGearMotor3))
                {
                    return;
                }

                m_Car1BigGearMotor3 = value;
                RaisePropertyChanged(() => Car1BigGearMotor3);
            }
        }

        public double Car1BigGearMotor4
        {
            get { return m_Car1BigGearMotor4; }
            set
            {
                if (value.Equals(m_Car1BigGearMotor4))
                {
                    return;
                }

                m_Car1BigGearMotor4 = value;
                RaisePropertyChanged(() => Car1BigGearMotor4);
            }
        }

        public double Car6BigGearMotor1
        {
            get { return m_Car6BigGearMotor1; }
            set
            {
                if (value.Equals(m_Car6BigGearMotor1))
                {
                    return;
                }

                m_Car6BigGearMotor1 = value;
                RaisePropertyChanged(() => Car6BigGearMotor1);
            }
        }

        public double Car6BigGearMotor2
        {
            get { return m_Car6BigGearMotor2; }
            set
            {
                if (value.Equals(m_Car6BigGearMotor2))
                {
                    return;
                }

                m_Car6BigGearMotor2 = value;
                RaisePropertyChanged(() => Car6BigGearMotor2);
            }
        }

        public double Car6BigGearMotor3
        {
            get { return m_Car6BigGearMotor3; }
            set
            {
                if (value.Equals(m_Car6BigGearMotor3))
                {
                    return;
                }

                m_Car6BigGearMotor3 = value;
                RaisePropertyChanged(() => Car6BigGearMotor3);
            }
        }

        public double Car6BigGearMotor4
        {
            get { return m_Car6BigGearMotor4; }
            set
            {
                if (value.Equals(m_Car6BigGearMotor4))
                {
                    return;
                }

                m_Car6BigGearMotor4 = value;
                RaisePropertyChanged(() => Car6BigGearMotor4);
            }
        }

        public double Car3BigGearMotor1
        {
            get { return m_Car3BigGearMotor1; }
            set
            {
                if (value.Equals(m_Car3BigGearMotor1))
                {
                    return;
                }

                m_Car3BigGearMotor1 = value;
                RaisePropertyChanged(() => Car3BigGearMotor1);
            }
        }

        public double Car3BigGearMotor2
        {
            get { return m_Car3BigGearMotor2; }
            set
            {
                if (value.Equals(m_Car3BigGearMotor2))
                {
                    return;
                }

                m_Car3BigGearMotor2 = value;
                RaisePropertyChanged(() => Car3BigGearMotor2);
            }
        }

        public double Car3BigGearMotor3
        {
            get { return m_Car3BigGearMotor3; }
            set
            {
                if (value.Equals(m_Car3BigGearMotor3))
                {
                    return;
                }

                m_Car3BigGearMotor3 = value;
                RaisePropertyChanged(() => Car3BigGearMotor3);
            }
        }

        public double Car3BigGearMotor4
        {
            get { return m_Car3BigGearMotor4; }
            set
            {
                if (value.Equals(m_Car3BigGearMotor4))
                {
                    return;
                }

                m_Car3BigGearMotor4 = value;
                RaisePropertyChanged(() => Car3BigGearMotor4);
            }
        }

        public double Car8BigGearMotor1
        {
            get { return m_Car8BigGearMotor1; }
            set
            {
                if (value.Equals(m_Car8BigGearMotor1))
                {
                    return;
                }

                m_Car8BigGearMotor1 = value;
                RaisePropertyChanged(() => Car8BigGearMotor1);
            }
        }

        public double Car8BigGearMotor2
        {
            get { return m_Car8BigGearMotor2; }
            set
            {
                if (value.Equals(m_Car8BigGearMotor2))
                {
                    return;
                }

                m_Car8BigGearMotor2 = value;
                RaisePropertyChanged(() => Car8BigGearMotor2);
            }
        }

        public double Car8BigGearMotor3
        {
            get { return m_Car8BigGearMotor3; }
            set
            {
                if (value.Equals(m_Car8BigGearMotor3))
                {
                    return;
                }

                m_Car8BigGearMotor3 = value;
                RaisePropertyChanged(() => Car8BigGearMotor3);
            }
        }

        public double Car8BigGearMotor4
        {
            get { return m_Car8BigGearMotor4; }
            set
            {
                if (value.Equals(m_Car8BigGearMotor4))
                {
                    return;
                }

                m_Car8BigGearMotor4 = value;
                RaisePropertyChanged(() => Car8BigGearMotor4);
            }
        }

        public double Car1BigGearWheel1
        {
            get { return m_Car1BigGearWheel1; }
            set
            {
                if (value.Equals(m_Car1BigGearWheel1))
                {
                    return;
                }

                m_Car1BigGearWheel1 = value;
                RaisePropertyChanged(() => Car1BigGearWheel1);
            }
        }

        public double Car1BigGearWheel2
        {
            get { return m_Car1BigGearWheel2; }
            set
            {
                if (value.Equals(m_Car1BigGearWheel2))
                {
                    return;
                }

                m_Car1BigGearWheel2 = value;
                RaisePropertyChanged(() => Car1BigGearWheel2);
            }
        }

        public double Car1BigGearWheel3
        {
            get { return m_Car1BigGearWheel3; }
            set
            {
                if (value.Equals(m_Car1BigGearWheel3))
                {
                    return;
                }

                m_Car1BigGearWheel3 = value;
                RaisePropertyChanged(() => Car1BigGearWheel3);
            }
        }

        public double Car1BigGearWheel4
        {
            get { return m_Car1BigGearWheel4; }
            set
            {
                if (value.Equals(m_Car1BigGearWheel4))
                {
                    return;
                }

                m_Car1BigGearWheel4 = value;
                RaisePropertyChanged(() => Car1BigGearWheel4);
            }
        }

        public double Car6BigGearWheel1
        {
            get { return m_Car6BigGearWheel1; }
            set
            {
                if (value.Equals(m_Car6BigGearWheel1))
                {
                    return;
                }

                m_Car6BigGearWheel1 = value;
                RaisePropertyChanged(() => Car6BigGearWheel1);
            }
        }

        public double Car6BigGearWheel2
        {
            get { return m_Car6BigGearWheel2; }
            set
            {
                if (value.Equals(m_Car6BigGearWheel2))
                {
                    return;
                }

                m_Car6BigGearWheel2 = value;
                RaisePropertyChanged(() => Car6BigGearWheel2);
            }
        }

        public double Car6BigGearWheel3
        {
            get { return m_Car6BigGearWheel3; }
            set
            {
                if (value.Equals(m_Car6BigGearWheel3))
                {
                    return;
                }

                m_Car6BigGearWheel3 = value;
                RaisePropertyChanged(() => Car6BigGearWheel3);
            }
        }

        public double Car6BigGearWheel4
        {
            get { return m_Car6BigGearWheel4; }
            set
            {
                if (value.Equals(m_Car6BigGearWheel4))
                {
                    return;
                }

                m_Car6BigGearWheel4 = value;
                RaisePropertyChanged(() => Car6BigGearWheel4);
            }
        }

        public double Car3BigGearWheel1
        {
            get { return m_Car3BigGearWheel1; }
            set
            {
                if (value.Equals(m_Car3BigGearWheel1))
                {
                    return;
                }

                m_Car3BigGearWheel1 = value;
                RaisePropertyChanged(() => Car3BigGearWheel1);
            }
        }

        public double Car3BigGearWheel2
        {
            get { return m_Car3BigGearWheel2; }
            set
            {
                if (value.Equals(m_Car3BigGearWheel2))
                {
                    return;
                }

                m_Car3BigGearWheel2 = value;
                RaisePropertyChanged(() => Car3BigGearWheel2);
            }
        }

        public double Car3BigGearWheel3
        {
            get { return m_Car3BigGearWheel3; }
            set
            {
                if (value.Equals(m_Car3BigGearWheel3))
                {
                    return;
                }

                m_Car3BigGearWheel3 = value;
                RaisePropertyChanged(() => Car3BigGearWheel3);
            }
        }

        public double Car3BigGearWheel4
        {
            get { return m_Car3BigGearWheel4; }
            set
            {
                if (value.Equals(m_Car3BigGearWheel4))
                {
                    return;
                }

                m_Car3BigGearWheel4 = value;
                RaisePropertyChanged(() => Car3BigGearWheel4);
            }
        }

        public double Car8BigGearWheel1
        {
            get { return m_Car8BigGearWheel1; }
            set
            {
                if (value.Equals(m_Car8BigGearWheel1))
                {
                    return;
                }

                m_Car8BigGearWheel1 = value;
                RaisePropertyChanged(() => Car8BigGearWheel1);
            }
        }

        public double Car8BigGearWheel2
        {
            get { return m_Car8BigGearWheel2; }
            set
            {
                if (value.Equals(m_Car8BigGearWheel2))
                {
                    return;
                }

                m_Car8BigGearWheel2 = value;
                RaisePropertyChanged(() => Car8BigGearWheel2);
            }
        }

        public double Car8BigGearWheel3
        {
            get { return m_Car8BigGearWheel3; }
            set
            {
                if (value.Equals(m_Car8BigGearWheel3))
                {
                    return;
                }

                m_Car8BigGearWheel3 = value;
                RaisePropertyChanged(() => Car8BigGearWheel3);
            }
        }

        public double Car8BigGearWheel4
        {
            get { return m_Car8BigGearWheel4; }
            set
            {
                if (value.Equals(m_Car8BigGearWheel4))
                {
                    return;
                }

                m_Car8BigGearWheel4 = value;
                RaisePropertyChanged(() => Car8BigGearWheel4);
            }
        }

        public double Car1SmallGearMotor1
        {
            get { return m_Car1SmallGearMotor1; }
            set
            {
                if (value.Equals(m_Car1SmallGearMotor1))
                {
                    return;
                }

                m_Car1SmallGearMotor1 = value;
                RaisePropertyChanged(() => Car1SmallGearMotor1);
            }
        }

        public double Car1SmallGearMotor2
        {
            get { return m_Car1SmallGearMotor2; }
            set
            {
                if (value.Equals(m_Car1SmallGearMotor2))
                {
                    return;
                }

                m_Car1SmallGearMotor2 = value;
                RaisePropertyChanged(() => Car1SmallGearMotor2);
            }
        }

        public double Car1SmallGearMotor3
        {
            get { return m_Car1SmallGearMotor3; }
            set
            {
                if (value.Equals(m_Car1SmallGearMotor3))
                {
                    return;
                }

                m_Car1SmallGearMotor3 = value;
                RaisePropertyChanged(() => Car1SmallGearMotor3);
            }
        }

        public double Car1SmallGearMotor4
        {
            get { return m_Car1SmallGearMotor4; }
            set
            {
                if (value.Equals(m_Car1SmallGearMotor4))
                {
                    return;
                }

                m_Car1SmallGearMotor4 = value;
                RaisePropertyChanged(() => Car1SmallGearMotor4);
            }
        }

        public double Car6SmallGearMotor1
        {
            get { return m_Car6SmallGearMotor1; }
            set
            {
                if (value.Equals(m_Car6SmallGearMotor1))
                {
                    return;
                }

                m_Car6SmallGearMotor1 = value;
                RaisePropertyChanged(() => Car6SmallGearMotor1);
            }
        }

        public double Car6SmallGearMotor2
        {
            get { return m_Car6SmallGearMotor2; }
            set
            {
                if (value.Equals(m_Car6SmallGearMotor2))
                {
                    return;
                }

                m_Car6SmallGearMotor2 = value;
                RaisePropertyChanged(() => Car6SmallGearMotor2);
            }
        }

        public double Car6SmallGearMotor3
        {
            get { return m_Car6SmallGearMotor3; }
            set
            {
                if (value.Equals(m_Car6SmallGearMotor3))
                {
                    return;
                }

                m_Car6SmallGearMotor3 = value;
                RaisePropertyChanged(() => Car6SmallGearMotor3);
            }
        }

        public double Car6SmallGearMotor4
        {
            get { return m_Car6SmallGearMotor4; }
            set
            {
                if (value.Equals(m_Car6SmallGearMotor4))
                {
                    return;
                }

                m_Car6SmallGearMotor4 = value;
                RaisePropertyChanged(() => Car6SmallGearMotor4);
            }
        }

        public double Car3SmallGearMotor1
        {
            get { return m_Car3SmallGearMotor1; }
            set
            {
                if (value.Equals(m_Car3SmallGearMotor1))
                {
                    return;
                }

                m_Car3SmallGearMotor1 = value;
                RaisePropertyChanged(() => Car3SmallGearMotor1);
            }
        }

        public double Car3SmallGearMotor2
        {
            get { return m_Car3SmallGearMotor2; }
            set
            {
                if (value.Equals(m_Car3SmallGearMotor2))
                {
                    return;
                }

                m_Car3SmallGearMotor2 = value;
                RaisePropertyChanged(() => Car3SmallGearMotor2);
            }
        }

        public double Car3SmallGearMotor3
        {
            get { return m_Car3SmallGearMotor3; }
            set
            {
                if (value.Equals(m_Car3SmallGearMotor3))
                {
                    return;
                }

                m_Car3SmallGearMotor3 = value;
                RaisePropertyChanged(() => Car3SmallGearMotor3);
            }
        }

        public double Car3SmallGearMotor4
        {
            get { return m_Car3SmallGearMotor4; }
            set
            {
                if (value.Equals(m_Car3SmallGearMotor4))
                {
                    return;
                }

                m_Car3SmallGearMotor4 = value;
                RaisePropertyChanged(() => Car3SmallGearMotor4);
            }
        }

        public double Car8SmallGearMotor1
        {
            get { return m_Car8SmallGearMotor1; }
            set
            {
                if (value.Equals(m_Car8SmallGearMotor1))
                {
                    return;
                }

                m_Car8SmallGearMotor1 = value;
                RaisePropertyChanged(() => Car8SmallGearMotor1);
            }
        }

        public double Car8SmallGearMotor2
        {
            get { return m_Car8SmallGearMotor2; }
            set
            {
                if (value.Equals(m_Car8SmallGearMotor2))
                {
                    return;
                }

                m_Car8SmallGearMotor2 = value;
                RaisePropertyChanged(() => Car8SmallGearMotor2);
            }
        }

        public double Car8SmallGearMotor3
        {
            get { return m_Car8SmallGearMotor3; }
            set
            {
                if (value.Equals(m_Car8SmallGearMotor3))
                {
                    return;
                }

                m_Car8SmallGearMotor3 = value;
                RaisePropertyChanged(() => Car8SmallGearMotor3);
            }
        }

        public double Car8SmallGearMotor4
        {
            get { return m_Car8SmallGearMotor4; }
            set
            {
                if (value.Equals(m_Car8SmallGearMotor4))
                {
                    return;
                }

                m_Car8SmallGearMotor4 = value;
                RaisePropertyChanged(() => Car8SmallGearMotor4);
            }
        }

        public double Car1SmallGearWheel1
        {
            get { return m_Car1SmallGearWheel1; }
            set
            {
                if (value.Equals(m_Car1SmallGearWheel1))
                {
                    return;
                }

                m_Car1SmallGearWheel1 = value;
                RaisePropertyChanged(() => Car1SmallGearWheel1);
            }
        }

        public double Car1SmallGearWheel2
        {
            get { return m_Car1SmallGearWheel2; }
            set
            {
                if (value.Equals(m_Car1SmallGearWheel2))
                {
                    return;
                }

                m_Car1SmallGearWheel2 = value;
                RaisePropertyChanged(() => Car1SmallGearWheel2);
            }
        }

        public double Car1SmallGearWheel3
        {
            get { return m_Car1SmallGearWheel3; }
            set
            {
                if (value.Equals(m_Car1SmallGearWheel3))
                {
                    return;
                }

                m_Car1SmallGearWheel3 = value;
                RaisePropertyChanged(() => Car1SmallGearWheel3);
            }
        }

        public double Car1SmallGearWheel4
        {
            get { return m_Car1SmallGearWheel4; }
            set
            {
                if (value.Equals(m_Car1SmallGearWheel4))
                {
                    return;
                }

                m_Car1SmallGearWheel4 = value;
                RaisePropertyChanged(() => Car1SmallGearWheel4);
            }
        }

        public double Car6SmallGearWheel1
        {
            get { return m_Car6SmallGearWheel1; }
            set
            {
                if (value.Equals(m_Car6SmallGearWheel1))
                {
                    return;
                }

                m_Car6SmallGearWheel1 = value;
                RaisePropertyChanged(() => Car6SmallGearWheel1);
            }
        }

        public double Car6SmallGearWheel2
        {
            get { return m_Car6SmallGearWheel2; }
            set
            {
                if (value.Equals(m_Car6SmallGearWheel2))
                {
                    return;
                }

                m_Car6SmallGearWheel2 = value;
                RaisePropertyChanged(() => Car6SmallGearWheel2);
            }
        }

        public double Car6SmallGearWheel3
        {
            get { return m_Car6SmallGearWheel3; }
            set
            {
                if (value.Equals(m_Car6SmallGearWheel3))
                {
                    return;
                }

                m_Car6SmallGearWheel3 = value;
                RaisePropertyChanged(() => Car6SmallGearWheel3);
            }
        }

        public double Car6SmallGearWheel4
        {
            get { return m_Car6SmallGearWheel4; }
            set
            {
                if (value.Equals(m_Car6SmallGearWheel4))
                {
                    return;
                }

                m_Car6SmallGearWheel4 = value;
                RaisePropertyChanged(() => Car6SmallGearWheel4);
            }
        }

        public double Car3SmallGearWheel1
        {
            get { return m_Car3SmallGearWheel1; }
            set
            {
                if (value.Equals(m_Car3SmallGearWheel1))
                {
                    return;
                }

                m_Car3SmallGearWheel1 = value;
                RaisePropertyChanged(() => Car3SmallGearWheel1);
            }
        }

        public double Car3SmallGearWheel2
        {
            get { return m_Car3SmallGearWheel2; }
            set
            {
                if (value.Equals(m_Car3SmallGearWheel2))
                {
                    return;
                }

                m_Car3SmallGearWheel2 = value;
                RaisePropertyChanged(() => Car3SmallGearWheel2);
            }
        }

        public double Car3SmallGearWheel3
        {
            get { return m_Car3SmallGearWheel3; }
            set
            {
                if (value.Equals(m_Car3SmallGearWheel3))
                {
                    return;
                }

                m_Car3SmallGearWheel3 = value;
                RaisePropertyChanged(() => Car3SmallGearWheel3);
            }
        }

        public double Car3SmallGearWheel4
        {
            get { return m_Car3SmallGearWheel4; }
            set
            {
                if (value.Equals(m_Car3SmallGearWheel4))
                {
                    return;
                }

                m_Car3SmallGearWheel4 = value;
                RaisePropertyChanged(() => Car3SmallGearWheel4);
            }
        }

        public double Car8SmallGearWheel1
        {
            get { return m_Car8SmallGearWheel1; }
            set
            {
                if (value.Equals(m_Car8SmallGearWheel1))
                {
                    return;
                }

                m_Car8SmallGearWheel1 = value;
                RaisePropertyChanged(() => Car8SmallGearWheel1);
            }
        }

        public double Car8SmallGearWheel2
        {
            get { return m_Car8SmallGearWheel2; }
            set
            {
                if (value.Equals(m_Car8SmallGearWheel2))
                {
                    return;
                }

                m_Car8SmallGearWheel2 = value;
                RaisePropertyChanged(() => Car8SmallGearWheel2);
            }
        }

        public double Car8SmallGearWheel3
        {
            get { return m_Car8SmallGearWheel3; }
            set
            {
                if (value.Equals(m_Car8SmallGearWheel3))
                {
                    return;
                }

                m_Car8SmallGearWheel3 = value;
                RaisePropertyChanged(() => Car8SmallGearWheel3);
            }
        }

        public double Car8SmallGearWheel4
        {
            get { return m_Car8SmallGearWheel4; }
            set
            {
                if (value.Equals(m_Car8SmallGearWheel4))
                {
                    return;
                }

                m_Car8SmallGearWheel4 = value;
                RaisePropertyChanged(() => Car8SmallGearWheel4);
            }
        }

        public double Car1MotorStator1
        {
            get { return m_Car1MotorStator1; }
            set
            {
                if (value.Equals(m_Car1MotorStator1))
                {
                    return;
                }

                m_Car1MotorStator1 = value;
                RaisePropertyChanged(() => Car1MotorStator1);
            }
        }

        public double Car1MotorStator2
        {
            get { return m_Car1MotorStator2; }
            set
            {
                if (value.Equals(m_Car1MotorStator2))
                {
                    return;
                }

                m_Car1MotorStator2 = value;
                RaisePropertyChanged(() => Car1MotorStator2);
            }
        }

        public double Car1MotorStator3
        {
            get { return m_Car1MotorStator3; }
            set
            {
                if (value.Equals(m_Car1MotorStator3))
                {
                    return;
                }

                m_Car1MotorStator3 = value;
                RaisePropertyChanged(() => Car1MotorStator3);
            }
        }

        public double Car1MotorStator4
        {
            get { return m_Car1MotorStator4; }
            set
            {
                if (value.Equals(m_Car1MotorStator4))
                {
                    return;
                }

                m_Car1MotorStator4 = value;
                RaisePropertyChanged(() => Car1MotorStator4);
            }
        }

        public double Car6MotorStator1
        {
            get { return m_Car6MotorStator1; }
            set
            {
                if (value.Equals(m_Car6MotorStator1))
                {
                    return;
                }

                m_Car6MotorStator1 = value;
                RaisePropertyChanged(() => Car6MotorStator1);
            }
        }

        public double Car6MotorStator2
        {
            get { return m_Car6MotorStator2; }
            set
            {
                if (value.Equals(m_Car6MotorStator2))
                {
                    return;
                }

                m_Car6MotorStator2 = value;
                RaisePropertyChanged(() => Car6MotorStator2);
            }
        }

        public double Car6MotorStator3
        {
            get { return m_Car6MotorStator3; }
            set
            {
                if (value.Equals(m_Car6MotorStator3))
                {
                    return;
                }

                m_Car6MotorStator3 = value;
                RaisePropertyChanged(() => Car6MotorStator3);
            }
        }

        public double Car6MotorStator4
        {
            get { return m_Car6MotorStator4; }
            set
            {
                if (value.Equals(m_Car6MotorStator4))
                {
                    return;
                }

                m_Car6MotorStator4 = value;
                RaisePropertyChanged(() => Car6MotorStator4);
            }
        }

        public double Car3MotorStator1
        {
            get { return m_Car3MotorStator1; }
            set
            {
                if (value.Equals(m_Car3MotorStator1))
                {
                    return;
                }

                m_Car3MotorStator1 = value;
                RaisePropertyChanged(() => Car3MotorStator1);
            }
        }

        public double Car3MotorStator2
        {
            get { return m_Car3MotorStator2; }
            set
            {
                if (value.Equals(m_Car3MotorStator2))
                {
                    return;
                }

                m_Car3MotorStator2 = value;
                RaisePropertyChanged(() => Car3MotorStator2);
            }
        }

        public double Car3MotorStator3
        {
            get { return m_Car3MotorStator3; }
            set
            {
                if (value.Equals(m_Car3MotorStator3))
                {
                    return;
                }

                m_Car3MotorStator3 = value;
                RaisePropertyChanged(() => Car3MotorStator3);
            }
        }

        public double Car3MotorStator4
        {
            get { return m_Car3MotorStator4; }
            set
            {
                if (value.Equals(m_Car3MotorStator4))
                {
                    return;
                }

                m_Car3MotorStator4 = value;
                RaisePropertyChanged(() => Car3MotorStator4);
            }
        }

        public double Car8MotorStator1
        {
            get { return m_Car8MotorStator1; }
            set
            {
                if (value.Equals(m_Car8MotorStator1))
                {
                    return;
                }

                m_Car8MotorStator1 = value;
                RaisePropertyChanged(() => Car8MotorStator1);
            }
        }

        public double Car8MotorStator2
        {
            get { return m_Car8MotorStator2; }
            set
            {
                if (value.Equals(m_Car8MotorStator2))
                {
                    return;
                }

                m_Car8MotorStator2 = value;
                RaisePropertyChanged(() => Car8MotorStator2);
            }
        }

        public double Car8MotorStator3
        {
            get { return m_Car8MotorStator3; }
            set
            {
                if (value.Equals(m_Car8MotorStator3))
                {
                    return;
                }

                m_Car8MotorStator3 = value;
                RaisePropertyChanged(() => Car8MotorStator3);
            }
        }

        public double Car8MotorStator4
        {
            get { return m_Car8MotorStator4; }
            set
            {
                if (value.Equals(m_Car8MotorStator4))
                {
                    return;
                }

                m_Car8MotorStator4 = value;
                RaisePropertyChanged(() => Car8MotorStator4);
            }
        }
    }
}
