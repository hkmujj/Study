using System.ComponentModel.Composition;

namespace Engine.HMI.SS3B.View.ViewModel.LiuZhou
{

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MainCircuitPageViewModel:ViewModelBase
    {
        private double m_DriverKeyOther;
        private double m_Level23MagneticOther;
        private double m_Level13MagneticOther;
        private double m_Bit2BrakeOther;
        private double m_Bit2TowOther;
        private double m_Bit1BrakeOther;
        private double m_Bit1TowOther;
        private double m_MagneticTouchOther;
        private double m_LineTouch56Other;
        private double m_LineTouch4Other;
        private double m_LineTouch23Other;
        private double m_LineTouch1Other;
        private double m_MasterClosedOther;
        private double m_MasterClosingOther;
        private double m_BanMasterOther;
        private double m_BowDownMasterOther;
        private double m_Frame2FaultOther;
        private double m_Frame1FaultOther;
        private double m_BrakeOther;
        private double m_TowOther;
        private double m_DriverKeyOrigin;
        private double m_Level23MagneticOrigin;
        private double m_Level13MagneticOrigin;
        private double m_Bit2BrakeOrigin;
        private double m_Bit2TowOrigin;
        private double m_Bit1BrakeOrigin;
        private double m_Bit1TowOrigin;
        private double m_MagneticTouchOrigin;
        private double m_LineTouch56Origin;
        private double m_LineTouch4Origin;
        private double m_LineTouch23Origin;
        private double m_LineTouch1Origin;
        private double m_MasterClosedOrigin;
        private double m_MasterClosingOrigin;
        private double m_BanMasterOrigin;
        private double m_BowDownMasterOrigin;
        private double m_Frame2FaultOrigin;
        private double m_Frame1FaultOrigin;
        private double m_BrakeOrigin;
        private double m_TowOrigin;
        private double m_NetPressOther;
        private double m_MotorGalvanic6Other;
        private double m_MotorGalvanic5Other;
        private double m_MotorGalvanic4Other;
        private double m_MotorGalvanic3Other;
        private double m_MotorGalvanic2Other;
        private double m_MotorGalvanic1Other;
        private double m_MagneticGalvanicOther;
        private double m_Frame2MotorVoltageOther;
        private double m_Frame1MOtorVoltageOther;
        private double m_NetPressOrigin;
        private double m_MotorGalvanic6Origin;
        private double m_MotorGalvanic5Origin;
        private double m_MotorGalvanic4Origin;
        private double m_MotorGalvanic3Origin;
        private double m_MotorGalvanic2Origin;
        private double m_MotorGalvanic1Origin;
        private double m_MagneticGalvanicOrigin;
        private double m_Frame2MotorVoltageOrigin;
        private double m_Frame1MOtorVoltageOrigin;

        public double TowOrigin
        {
            get { return m_TowOrigin; }
            set
            {
                if (value.Equals(m_TowOrigin))
                {
                    return;
                }
                m_TowOrigin = value;
                RaisePropertyChanged(() => TowOrigin);
            }
        }

        public double BrakeOrigin
        {
            get { return m_BrakeOrigin; }
            set
            {
                if (value.Equals(m_BrakeOrigin))
                {
                    return;
                }
                m_BrakeOrigin = value;
                RaisePropertyChanged(() => BrakeOrigin);
            }
        }

        public double Frame1FaultOrigin
        {
            get { return m_Frame1FaultOrigin; }
            set
            {
                if (value.Equals(m_Frame1FaultOrigin))
                {
                    return;
                }
                m_Frame1FaultOrigin = value;
                RaisePropertyChanged(() => Frame1FaultOrigin);
            }
        }

        public double Frame2FaultOrigin
        {
            get { return m_Frame2FaultOrigin; }
            set
            {
                if (value.Equals(m_Frame2FaultOrigin))
                {
                    return;
                }
                m_Frame2FaultOrigin = value;
                RaisePropertyChanged(() => Frame2FaultOrigin);
            }
        }

        public double BowDownMasterOrigin
        {
            get { return m_BowDownMasterOrigin; }
            set
            {
                if (value.Equals(m_BowDownMasterOrigin))
                {
                    return;
                }
                m_BowDownMasterOrigin = value;
                RaisePropertyChanged(() => BowDownMasterOrigin);
            }
        }

        public double BanMasterOrigin
        {
            get { return m_BanMasterOrigin; }
            set
            {
                if (value.Equals(m_BanMasterOrigin))
                {
                    return;
                }
                m_BanMasterOrigin = value;
                RaisePropertyChanged(() => BanMasterOrigin);
            }
        }

        public double MasterClosingOrigin
        {
            get { return m_MasterClosingOrigin; }
            set
            {
                if (value.Equals(m_MasterClosingOrigin))
                {
                    return;
                }
                m_MasterClosingOrigin = value;
                RaisePropertyChanged(() => MasterClosingOrigin);
            }
        }

        public double MasterClosedOrigin
        {
            get { return m_MasterClosedOrigin; }
            set
            {
                if (value.Equals(m_MasterClosedOrigin))
                {
                    return;
                }
                m_MasterClosedOrigin = value;
                RaisePropertyChanged(() => MasterClosedOrigin);
            }
        }

        public double LineTouch1Origin
        {
            get { return m_LineTouch1Origin; }
            set
            {
                if (value.Equals(m_LineTouch1Origin))
                {
                    return;
                }
                m_LineTouch1Origin = value;
                RaisePropertyChanged(() => LineTouch1Origin);
            }
        }

        public double LineTouch23Origin
        {
            get { return m_LineTouch23Origin; }
            set
            {
                if (value.Equals(m_LineTouch23Origin))
                {
                    return;
                }
                m_LineTouch23Origin = value;
                RaisePropertyChanged(() => LineTouch23Origin);
            }
        }

        public double LineTouch4Origin
        {
            get { return m_LineTouch4Origin; }
            set
            {
                if (value.Equals(m_LineTouch4Origin))
                {
                    return;
                }
                m_LineTouch4Origin = value;
                RaisePropertyChanged(() => LineTouch4Origin);
            }
        }

        public double LineTouch56Origin
        {
            get { return m_LineTouch56Origin; }
            set
            {
                if (value.Equals(m_LineTouch56Origin))
                {
                    return;
                }
                m_LineTouch56Origin = value;
                RaisePropertyChanged(() => LineTouch56Origin);
            }
        }

        public double MagneticTouchOrigin
        {
            get { return m_MagneticTouchOrigin; }
            set
            {
                if (value.Equals(m_MagneticTouchOrigin))
                {
                    return;
                }
                m_MagneticTouchOrigin = value;
                RaisePropertyChanged(() => MagneticTouchOrigin);
            }
        }

        public double Bit1TowOrigin
        {
            get { return m_Bit1TowOrigin; }
            set
            {
                if (value.Equals(m_Bit1TowOrigin))
                {
                    return;
                }
                m_Bit1TowOrigin = value;
                RaisePropertyChanged(() => Bit1TowOrigin);
            }
        }

        public double Bit1BrakeOrigin
        {
            get { return m_Bit1BrakeOrigin; }
            set
            {
                if (value.Equals(m_Bit1BrakeOrigin))
                {
                    return;
                }
                m_Bit1BrakeOrigin = value;
                RaisePropertyChanged(() => Bit1BrakeOrigin);
            }
        }

        public double Bit2TowOrigin
        {
            get { return m_Bit2TowOrigin; }
            set
            {
                if (value.Equals(m_Bit2TowOrigin))
                {
                    return;
                }
                m_Bit2TowOrigin = value;
                RaisePropertyChanged(() => Bit2TowOrigin);
            }
        }

        public double Bit2BrakeOrigin
        {
            get { return m_Bit2BrakeOrigin; }
            set
            {
                if (value.Equals(m_Bit2BrakeOrigin))
                {
                    return;
                }
                m_Bit2BrakeOrigin = value;
                RaisePropertyChanged(() => Bit2BrakeOrigin);
            }
        }

        public double Level13MagneticOrigin
        {
            get { return m_Level13MagneticOrigin; }
            set
            {
                if (value.Equals(m_Level13MagneticOrigin))
                {
                    return;
                }
                m_Level13MagneticOrigin = value;
                RaisePropertyChanged(() => Level13MagneticOrigin);
            }
        }

        public double Level23MagneticOrigin
        {
            get { return m_Level23MagneticOrigin; }
            set
            {
                if (value.Equals(m_Level23MagneticOrigin))
                {
                    return;
                }
                m_Level23MagneticOrigin = value;
                RaisePropertyChanged(() => Level23MagneticOrigin);
            }
        }

        public double DriverKeyOrigin
        {
            get { return m_DriverKeyOrigin; }
            set
            {
                if (value.Equals(m_DriverKeyOrigin))
                {
                    return;
                }
                m_DriverKeyOrigin = value;
                RaisePropertyChanged(() => DriverKeyOrigin);
            }
        }

        public double TowOther
        {
            get { return m_TowOther; }
            set
            {
                if (value.Equals(m_TowOther))
                {
                    return;
                }
                m_TowOther = value;
                RaisePropertyChanged(() => TowOther);
            }
        }

        public double BrakeOther
        {
            get { return m_BrakeOther; }
            set
            {
                if (value.Equals(m_BrakeOther))
                {
                    return;
                }
                m_BrakeOther = value;
                RaisePropertyChanged(() => BrakeOther);
            }
        }

        public double Frame1FaultOther
        {
            get { return m_Frame1FaultOther; }
            set
            {
                if (value.Equals(m_Frame1FaultOther))
                {
                    return;
                }
                m_Frame1FaultOther = value;
                RaisePropertyChanged(() => Frame1FaultOther);
            }
        }

        public double Frame2FaultOther
        {
            get { return m_Frame2FaultOther; }
            set
            {
                if (value.Equals(m_Frame2FaultOther))
                {
                    return;
                }
                m_Frame2FaultOther = value;
                RaisePropertyChanged(() => Frame2FaultOther);
            }
        }

        public double BowDownMasterOther
        {
            get { return m_BowDownMasterOther; }
            set
            {
                if (value.Equals(m_BowDownMasterOther))
                {
                    return;
                }
                m_BowDownMasterOther = value;
                RaisePropertyChanged(() => BowDownMasterOther);
            }
        }

        public double BanMasterOther
        {
            get { return m_BanMasterOther; }
            set
            {
                if (value.Equals(m_BanMasterOther))
                {
                    return;
                }
                m_BanMasterOther = value;
                RaisePropertyChanged(() => BanMasterOther);
            }
        }

        public double MasterClosingOther
        {
            get { return m_MasterClosingOther; }
            set
            {
                if (value.Equals(m_MasterClosingOther))
                {
                    return;
                }
                m_MasterClosingOther = value;
                RaisePropertyChanged(() => MasterClosingOther);
            }
        }

        public double MasterClosedOther
        {
            get { return m_MasterClosedOther; }
            set
            {
                if (value.Equals(m_MasterClosedOther))
                {
                    return;
                }
                m_MasterClosedOther = value;
                RaisePropertyChanged(() => MasterClosedOther);
            }
        }

        public double LineTouch1Other
        {
            get { return m_LineTouch1Other; }
            set
            {
                if (value.Equals(m_LineTouch1Other))
                {
                    return;
                }
                m_LineTouch1Other = value;
                RaisePropertyChanged(() => LineTouch1Other);
            }
        }

        public double LineTouch23Other
        {
            get { return m_LineTouch23Other; }
            set
            {
                if (value.Equals(m_LineTouch23Other))
                {
                    return;
                }
                m_LineTouch23Other = value;
                RaisePropertyChanged(() => LineTouch23Other);
            }
        }

        public double LineTouch4Other
        {
            get { return m_LineTouch4Other; }
            set
            {
                if (value.Equals(m_LineTouch4Other))
                {
                    return;
                }
                m_LineTouch4Other = value;
                RaisePropertyChanged(() => LineTouch4Other);
            }
        }

        public double LineTouch56Other
        {
            get { return m_LineTouch56Other; }
            set
            {
                if (value.Equals(m_LineTouch56Other))
                {
                    return;
                }
                m_LineTouch56Other = value;
                RaisePropertyChanged(() => LineTouch56Other);
            }
        }

        public double MagneticTouchOther
        {
            get { return m_MagneticTouchOther; }
            set
            {
                if (value.Equals(m_MagneticTouchOther))
                {
                    return;
                }
                m_MagneticTouchOther = value;
                RaisePropertyChanged(() => MagneticTouchOther);
            }
        }

        public double Bit1TowOther
        {
            get { return m_Bit1TowOther; }
            set
            {
                if (value.Equals(m_Bit1TowOther))
                {
                    return;
                }
                m_Bit1TowOther = value;
                RaisePropertyChanged(() => Bit1TowOther);
            }
        }

        public double Bit1BrakeOther
        {
            get { return m_Bit1BrakeOther; }
            set
            {
                if (value.Equals(m_Bit1BrakeOther))
                {
                    return;
                }
                m_Bit1BrakeOther = value;
                RaisePropertyChanged(() => Bit1BrakeOther);
            }
        }

        public double Bit2TowOther
        {
            get { return m_Bit2TowOther; }
            set
            {
                if (value.Equals(m_Bit2TowOther))
                {
                    return;
                }
                m_Bit2TowOther = value;
                RaisePropertyChanged(() => Bit2TowOther);
            }
        }

        public double Bit2BrakeOther
        {
            get { return m_Bit2BrakeOther; }
            set
            {
                if (value.Equals(m_Bit2BrakeOther))
                {
                    return;
                }
                m_Bit2BrakeOther = value;
                RaisePropertyChanged(() => Bit2BrakeOther);
            }
        }

        public double Level13MagneticOther
        {
            get { return m_Level13MagneticOther; }
            set
            {
                if (value.Equals(m_Level13MagneticOther))
                {
                    return;
                }
                m_Level13MagneticOther = value;
                RaisePropertyChanged(() => Level13MagneticOther);
            }
        }

        public double Level23MagneticOther
        {
            get { return m_Level23MagneticOther; }
            set
            {
                if (value.Equals(m_Level23MagneticOther))
                {
                    return;
                }
                m_Level23MagneticOther = value;
                RaisePropertyChanged(() => Level23MagneticOther);
            }
        }

        public double DriverKeyOther
        {
            get { return m_DriverKeyOther; }
            set
            {
                if (value.Equals(m_DriverKeyOther))
                {
                    return;
                }
                m_DriverKeyOther = value;
                RaisePropertyChanged(() => DriverKeyOther);
            }
        }

        public double Frame1MOtorVoltageOrigin
        {
            get { return m_Frame1MOtorVoltageOrigin; }
            set
            {
                if (value.Equals(m_Frame1MOtorVoltageOrigin))
                {
                    return;
                }
                m_Frame1MOtorVoltageOrigin = value;
                RaisePropertyChanged(() => Frame1MOtorVoltageOrigin);
            }
        }

        public double Frame2MotorVoltageOrigin
        {
            get { return m_Frame2MotorVoltageOrigin; }
            set
            {
                if (value.Equals(m_Frame2MotorVoltageOrigin))
                {
                    return;
                }
                m_Frame2MotorVoltageOrigin = value;
                RaisePropertyChanged(() => Frame2MotorVoltageOrigin);
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

        public double MotorGalvanic1Origin
        {
            get { return m_MotorGalvanic1Origin; }
            set
            {
                if (value.Equals(m_MotorGalvanic1Origin))
                {
                    return;
                }
                m_MotorGalvanic1Origin = value;
                RaisePropertyChanged(() => MotorGalvanic1Origin);
            }
        }

        public double MotorGalvanic2Origin
        {
            get { return m_MotorGalvanic2Origin; }
            set
            {
                if (value.Equals(m_MotorGalvanic2Origin))
                {
                    return;
                }
                m_MotorGalvanic2Origin = value;
                RaisePropertyChanged(() => MotorGalvanic2Origin);
            }
        }

        public double MotorGalvanic3Origin
        {
            get { return m_MotorGalvanic3Origin; }
            set
            {
                if (value.Equals(m_MotorGalvanic3Origin))
                {
                    return;
                }
                m_MotorGalvanic3Origin = value;
                RaisePropertyChanged(() => MotorGalvanic3Origin);
            }
        }

        public double MotorGalvanic4Origin
        {
            get { return m_MotorGalvanic4Origin; }
            set
            {
                if (value.Equals(m_MotorGalvanic4Origin))
                {
                    return;
                }
                m_MotorGalvanic4Origin = value;
                RaisePropertyChanged(() => MotorGalvanic4Origin);
            }
        }

        public double MotorGalvanic5Origin
        {
            get { return m_MotorGalvanic5Origin; }
            set
            {
                if (value.Equals(m_MotorGalvanic5Origin))
                {
                    return;
                }
                m_MotorGalvanic5Origin = value;
                RaisePropertyChanged(() => MotorGalvanic5Origin);
            }
        }

        public double MotorGalvanic6Origin
        {
            get { return m_MotorGalvanic6Origin; }
            set
            {
                if (value.Equals(m_MotorGalvanic6Origin))
                {
                    return;
                }
                m_MotorGalvanic6Origin = value;
                RaisePropertyChanged(() => MotorGalvanic6Origin);
            }
        }

        public double NetPressOrigin
        {
            get { return m_NetPressOrigin; }
            set
            {
                if (value.Equals(m_NetPressOrigin))
                {
                    return;
                }
                m_NetPressOrigin = value;
                RaisePropertyChanged(() => NetPressOrigin);
            }
        }

        public double Frame1MOtorVoltageOther
        {
            get { return m_Frame1MOtorVoltageOther; }
            set
            {
                if (value.Equals(m_Frame1MOtorVoltageOther))
                {
                    return;
                }
                m_Frame1MOtorVoltageOther = value;
                RaisePropertyChanged(() => Frame1MOtorVoltageOther);
            }
        }

        public double Frame2MotorVoltageOther
        {
            get { return m_Frame2MotorVoltageOther; }
            set
            {
                if (value.Equals(m_Frame2MotorVoltageOther))
                {
                    return;
                }
                m_Frame2MotorVoltageOther = value;
                RaisePropertyChanged(() => Frame2MotorVoltageOther);
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

        public double MotorGalvanic1Other
        {
            get { return m_MotorGalvanic1Other; }
            set
            {
                if (value.Equals(m_MotorGalvanic1Other))
                {
                    return;
                }
                m_MotorGalvanic1Other = value;
                RaisePropertyChanged(() => MotorGalvanic1Other);
            }
        }

        public double MotorGalvanic2Other
        {
            get { return m_MotorGalvanic2Other; }
            set
            {
                if (value.Equals(m_MotorGalvanic2Other))
                {
                    return;
                }
                m_MotorGalvanic2Other = value;
                RaisePropertyChanged(() => MotorGalvanic2Other);
            }
        }

        public double MotorGalvanic3Other
        {
            get { return m_MotorGalvanic3Other; }
            set
            {
                if (value.Equals(m_MotorGalvanic3Other))
                {
                    return;
                }
                m_MotorGalvanic3Other = value;
                RaisePropertyChanged(() => MotorGalvanic3Other);
            }
        }

        public double MotorGalvanic4Other
        {
            get { return m_MotorGalvanic4Other; }
            set
            {
                if (value.Equals(m_MotorGalvanic4Other))
                {
                    return;
                }
                m_MotorGalvanic4Other = value;
                RaisePropertyChanged(() => MotorGalvanic4Other);
            }
        }

        public double MotorGalvanic5Other
        {
            get { return m_MotorGalvanic5Other; }
            set
            {
                if (value.Equals(m_MotorGalvanic5Other))
                {
                    return;
                }
                m_MotorGalvanic5Other = value;
                RaisePropertyChanged(() => MotorGalvanic5Other);
            }
        }

        public double MotorGalvanic6Other
        {
            get { return m_MotorGalvanic6Other; }
            set
            {
                if (value.Equals(m_MotorGalvanic6Other))
                {
                    return;
                }
                m_MotorGalvanic6Other = value;
                RaisePropertyChanged(() => MotorGalvanic6Other);
            }
        }

        public double NetPressOther
        {
            get { return m_NetPressOther; }
            set
            {
                if (value.Equals(m_NetPressOther))
                {
                    return;
                }
                m_NetPressOther = value;
                RaisePropertyChanged(() => NetPressOther);
            }
        }
    }
}
