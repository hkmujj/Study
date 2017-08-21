using System.ComponentModel.Composition;

namespace Engine.HMI.SS3B.View.ViewModel.KunMing
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AssistSysytemPageViewModel : ViewModelBase
    {
        private double m_CompressTouchOther;
        private double m_CameraRelayOpenOther;
        private double m_Camera2TouchOther;
        private double m_Camera1TouchOther;
        private double m_OilPumpRelayOther;
        private double m_TransformerFanOther;
        private double m_Brake2FanTouchOther;
        private double m_Brake1FanTouchOther;
        private double m_Traction4FanTouchOther;
        private double m_Traction3FanTouchOther;
        private double m_Traction2FanTouchOther;
        private double m_Traction1FanTouchOther;
        private double m_SpurtWheelOther;
        private double m_DesiccationOther;
        private double m_CompressOther;
        private double m_CameraRelayOther;
        private double m_Camera2Other;
        private double m_Camera1Other;
        private double m_OilPumpOther;
        private double m_TransformerFanOpenOther;
        private double m_BrakeFan2Other;
        private double m_BrakeFan1Other;
        private double m_TractionFan4Other;
        private double m_TractionFan3Other;
        private double m_TractionFan2Other;
        private double m_TractionFan1Other;
        private double m_CompressTouchOrigin;
        private double m_CameraRelayOpenOrigin;
        private double m_Camera2TouchOrigin;
        private double m_Camera1TouchOrigin;
        private double m_OilPumpRelayOrigin;
        private double m_TransformerFanOrigin;
        private double m_Brake2FanTouchOrigin;
        private double m_Brake1FanTouchOrigin;
        private double m_Traction4FanTouchOrigin;
        private double m_Traction3FanTouchOrigin;
        private double m_Traction2FanTouchOrigin;
        private double m_Traction1FanTouchOrigin;
        private double m_SpurtWheelOrigin;
        private double m_DesiccationOrigin;
        private double m_CompressOrigin;
        private double m_CameraRelayOrigin;
        private double m_Camera2Origin;
        private double m_Camera1Origin;
        private double m_OilPumpOrigin;
        private double m_TransformerFanOpenOrigin;
        private double m_BrakeFan2Origin;
        private double m_BrakeFan1Origin;
        private double m_TractionFan4Origin;
        private double m_TractionFan3Origin;
        private double m_TractionFan2Origin;
        private double m_TractionFan1Origin;

        public double TractionFan1Origin
        {
            get { return m_TractionFan1Origin; }
            set
            {
                if (value.Equals(m_TractionFan1Origin))
                {
                    return;
                }
                m_TractionFan1Origin = value;
                RaisePropertyChanged(() => TractionFan1Origin);
            }
        }

        public double TractionFan2Origin
        {
            get { return m_TractionFan2Origin; }
            set
            {
                if (value.Equals(m_TractionFan2Origin))
                {
                    return;
                }
                m_TractionFan2Origin = value;
                RaisePropertyChanged(() => TractionFan2Origin);
            }
        }

        public double TractionFan3Origin
        {
            get { return m_TractionFan3Origin; }
            set
            {
                if (value.Equals(m_TractionFan3Origin))
                {
                    return;
                }
                m_TractionFan3Origin = value;
                RaisePropertyChanged(() => TractionFan3Origin);
            }
        }

        public double TractionFan4Origin
        {
            get { return m_TractionFan4Origin; }
            set
            {
                if (value.Equals(m_TractionFan4Origin))
                {
                    return;
                }
                m_TractionFan4Origin = value;
                RaisePropertyChanged(() => TractionFan4Origin);
            }
        }

        public double BrakeFan1Origin
        {
            get { return m_BrakeFan1Origin; }
            set
            {
                if (value.Equals(m_BrakeFan1Origin))
                {
                    return;
                }
                m_BrakeFan1Origin = value;
                RaisePropertyChanged(() => BrakeFan1Origin);
            }
        }

        public double BrakeFan2Origin
        {
            get { return m_BrakeFan2Origin; }
            set
            {
                if (value.Equals(m_BrakeFan2Origin))
                {
                    return;
                }
                m_BrakeFan2Origin = value;
                RaisePropertyChanged(() => BrakeFan2Origin);
            }
        }

        public double TransformerFanOpenOrigin
        {
            get { return m_TransformerFanOpenOrigin; }
            set
            {
                if (value.Equals(m_TransformerFanOpenOrigin))
                {
                    return;
                }
                m_TransformerFanOpenOrigin = value;
                RaisePropertyChanged(() => TransformerFanOpenOrigin);
            }
        }

        public double OilPumpOrigin
        {
            get { return m_OilPumpOrigin; }
            set
            {
                if (value.Equals(m_OilPumpOrigin))
                {
                    return;
                }
                m_OilPumpOrigin = value;
                RaisePropertyChanged(() => OilPumpOrigin);
            }
        }

        public double Camera1Origin
        {
            get { return m_Camera1Origin; }
            set
            {
                if (value.Equals(m_Camera1Origin))
                {
                    return;
                }
                m_Camera1Origin = value;
                RaisePropertyChanged(() => Camera1Origin);
            }
        }

        public double Camera2Origin
        {
            get { return m_Camera2Origin; }
            set
            {
                if (value.Equals(m_Camera2Origin))
                {
                    return;
                }
                m_Camera2Origin = value;
                RaisePropertyChanged(() => Camera2Origin);
            }
        }

        public double CameraRelayOrigin
        {
            get { return m_CameraRelayOrigin; }
            set
            {
                if (value.Equals(m_CameraRelayOrigin))
                {
                    return;
                }
                m_CameraRelayOrigin = value;
                RaisePropertyChanged(() => CameraRelayOrigin);
            }
        }

        public double CompressOrigin
        {
            get { return m_CompressOrigin; }
            set
            {
                if (value.Equals(m_CompressOrigin))
                {
                    return;
                }
                m_CompressOrigin = value;
                RaisePropertyChanged(() => CompressOrigin);
            }
        }

        public double DesiccationOrigin
        {
            get { return m_DesiccationOrigin; }
            set
            {
                if (value.Equals(m_DesiccationOrigin))
                {
                    return;
                }
                m_DesiccationOrigin = value;
                RaisePropertyChanged(() => DesiccationOrigin);
            }
        }

        public double SpurtWheelOrigin
        {
            get { return m_SpurtWheelOrigin; }
            set
            {
                if (value.Equals(m_SpurtWheelOrigin))
                {
                    return;
                }
                m_SpurtWheelOrigin = value;
                RaisePropertyChanged(() => SpurtWheelOrigin);
            }
        }

        public double Traction1FanTouchOrigin
        {
            get { return m_Traction1FanTouchOrigin; }
            set
            {
                if (value.Equals(m_Traction1FanTouchOrigin))
                {
                    return;
                }
                m_Traction1FanTouchOrigin = value;
                RaisePropertyChanged(() => Traction1FanTouchOrigin);
            }
        }

        public double Traction2FanTouchOrigin
        {
            get { return m_Traction2FanTouchOrigin; }
            set
            {
                if (value.Equals(m_Traction2FanTouchOrigin))
                {
                    return;
                }
                m_Traction2FanTouchOrigin = value;
                RaisePropertyChanged(() => Traction2FanTouchOrigin);
            }
        }

        public double Traction3FanTouchOrigin
        {
            get { return m_Traction3FanTouchOrigin; }
            set
            {
                if (value.Equals(m_Traction3FanTouchOrigin))
                {
                    return;
                }
                m_Traction3FanTouchOrigin = value;
                RaisePropertyChanged(() => Traction3FanTouchOrigin);
            }
        }

        public double Traction4FanTouchOrigin
        {
            get { return m_Traction4FanTouchOrigin; }
            set
            {
                if (value.Equals(m_Traction4FanTouchOrigin))
                {
                    return;
                }
                m_Traction4FanTouchOrigin = value;
                RaisePropertyChanged(() => Traction4FanTouchOrigin);
            }
        }

        public double Brake1FanTouchOrigin
        {
            get { return m_Brake1FanTouchOrigin; }
            set
            {
                if (value.Equals(m_Brake1FanTouchOrigin))
                {
                    return;
                }
                m_Brake1FanTouchOrigin = value;
                RaisePropertyChanged(() => Brake1FanTouchOrigin);
            }
        }

        public double Brake2FanTouchOrigin
        {
            get { return m_Brake2FanTouchOrigin; }
            set
            {
                if (value.Equals(m_Brake2FanTouchOrigin))
                {
                    return;
                }
                m_Brake2FanTouchOrigin = value;
                RaisePropertyChanged(() => Brake2FanTouchOrigin);
            }
        }

        public double TransformerFanOrigin
        {
            get { return m_TransformerFanOrigin; }
            set
            {
                if (value.Equals(m_TransformerFanOrigin))
                {
                    return;
                }
                m_TransformerFanOrigin = value;
                RaisePropertyChanged(() => TransformerFanOrigin);
            }
        }

        public double OilPumpRelayOrigin
        {
            get { return m_OilPumpRelayOrigin; }
            set
            {
                if (value.Equals(m_OilPumpRelayOrigin))
                {
                    return;
                }
                m_OilPumpRelayOrigin = value;
                RaisePropertyChanged(() => OilPumpRelayOrigin);
            }
        }

        public double Camera1TouchOrigin
        {
            get { return m_Camera1TouchOrigin; }
            set
            {
                if (value.Equals(m_Camera1TouchOrigin))
                {
                    return;
                }
                m_Camera1TouchOrigin = value;
                RaisePropertyChanged(() => Camera1TouchOrigin);
            }
        }

        public double Camera2TouchOrigin
        {
            get { return m_Camera2TouchOrigin; }
            set
            {
                if (value.Equals(m_Camera2TouchOrigin))
                {
                    return;
                }
                m_Camera2TouchOrigin = value;
                RaisePropertyChanged(() => Camera2TouchOrigin);
            }
        }

        public double CameraRelayOpenOrigin
        {
            get { return m_CameraRelayOpenOrigin; }
            set
            {
                if (value.Equals(m_CameraRelayOpenOrigin))
                {
                    return;
                }
                m_CameraRelayOpenOrigin = value;
                RaisePropertyChanged(() => CameraRelayOpenOrigin);
            }
        }

        public double CompressTouchOrigin
        {
            get { return m_CompressTouchOrigin; }
            set
            {
                if (value.Equals(m_CompressTouchOrigin))
                {
                    return;
                }
                m_CompressTouchOrigin = value;
                RaisePropertyChanged(() => CompressTouchOrigin);
            }
        }

        public double TractionFan1Other
        {
            get { return m_TractionFan1Other; }
            set
            {
                if (value.Equals(m_TractionFan1Other))
                {
                    return;
                }
                m_TractionFan1Other = value;
                RaisePropertyChanged(() => TractionFan1Other);
            }
        }

        public double TractionFan2Other
        {
            get { return m_TractionFan2Other; }
            set
            {
                if (value.Equals(m_TractionFan2Other))
                {
                    return;
                }
                m_TractionFan2Other = value;
                RaisePropertyChanged(() => TractionFan2Other);
            }
        }

        public double TractionFan3Other
        {
            get { return m_TractionFan3Other; }
            set
            {
                if (value.Equals(m_TractionFan3Other))
                {
                    return;
                }
                m_TractionFan3Other = value;
                RaisePropertyChanged(() => TractionFan3Other);
            }
        }

        public double TractionFan4Other
        {
            get { return m_TractionFan4Other; }
            set
            {
                if (value.Equals(m_TractionFan4Other))
                {
                    return;
                }
                m_TractionFan4Other = value;
                RaisePropertyChanged(() => TractionFan4Other);
            }
        }

        public double BrakeFan1Other
        {
            get { return m_BrakeFan1Other; }
            set
            {
                if (value.Equals(m_BrakeFan1Other))
                {
                    return;
                }
                m_BrakeFan1Other = value;
                RaisePropertyChanged(() => BrakeFan1Other);
            }
        }

        public double BrakeFan2Other
        {
            get { return m_BrakeFan2Other; }
            set
            {
                if (value.Equals(m_BrakeFan2Other))
                {
                    return;
                }
                m_BrakeFan2Other = value;
                RaisePropertyChanged(() => BrakeFan2Other);
            }
        }

        public double TransformerFanOpenOther
        {
            get { return m_TransformerFanOpenOther; }
            set
            {
                if (value.Equals(m_TransformerFanOpenOther))
                {
                    return;
                }
                m_TransformerFanOpenOther = value;
                RaisePropertyChanged(() => TransformerFanOpenOther);
            }
        }

        public double OilPumpOther
        {
            get { return m_OilPumpOther; }
            set
            {
                if (value.Equals(m_OilPumpOther))
                {
                    return;
                }
                m_OilPumpOther = value;
                RaisePropertyChanged(() => OilPumpOther);
            }
        }

        public double Camera1Other
        {
            get { return m_Camera1Other; }
            set
            {
                if (value.Equals(m_Camera1Other))
                {
                    return;
                }
                m_Camera1Other = value;
                RaisePropertyChanged(() => Camera1Other);
            }
        }

        public double Camera2Other
        {
            get { return m_Camera2Other; }
            set
            {
                if (value.Equals(m_Camera2Other))
                {
                    return;
                }
                m_Camera2Other = value;
                RaisePropertyChanged(() => Camera2Other);
            }
        }

        public double CameraRelayOther
        {
            get { return m_CameraRelayOther; }
            set
            {
                if (value.Equals(m_CameraRelayOther))
                {
                    return;
                }
                m_CameraRelayOther = value;
                RaisePropertyChanged(() => CameraRelayOther);
            }
        }

        public double CompressOther
        {
            get { return m_CompressOther; }
            set
            {
                if (value.Equals(m_CompressOther))
                {
                    return;
                }
                m_CompressOther = value;
                RaisePropertyChanged(() => CompressOther);
            }
        }

        public double DesiccationOther
        {
            get { return m_DesiccationOther; }
            set
            {
                if (value.Equals(m_DesiccationOther))
                {
                    return;
                }
                m_DesiccationOther = value;
                RaisePropertyChanged(() => DesiccationOther);
            }
        }

        public double SpurtWheelOther
        {
            get { return m_SpurtWheelOther; }
            set
            {
                if (value.Equals(m_SpurtWheelOther))
                {
                    return;
                }
                m_SpurtWheelOther = value;
                RaisePropertyChanged(() => SpurtWheelOther);
            }
        }

        public double Traction1FanTouchOther
        {
            get { return m_Traction1FanTouchOther; }
            set
            {
                if (value.Equals(m_Traction1FanTouchOther))
                {
                    return;
                }
                m_Traction1FanTouchOther = value;
                RaisePropertyChanged(() => Traction1FanTouchOther);
            }
        }

        public double Traction2FanTouchOther
        {
            get { return m_Traction2FanTouchOther; }
            set
            {
                if (value.Equals(m_Traction2FanTouchOther))
                {
                    return;
                }
                m_Traction2FanTouchOther = value;
                RaisePropertyChanged(() => Traction2FanTouchOther);
            }
        }

        public double Traction3FanTouchOther
        {
            get { return m_Traction3FanTouchOther; }
            set
            {
                if (value.Equals(m_Traction3FanTouchOther))
                {
                    return;
                }
                m_Traction3FanTouchOther = value;
                RaisePropertyChanged(() => Traction3FanTouchOther);
            }
        }

        public double Traction4FanTouchOther
        {
            get { return m_Traction4FanTouchOther; }
            set
            {
                if (value.Equals(m_Traction4FanTouchOther))
                {
                    return;
                }
                m_Traction4FanTouchOther = value;
                RaisePropertyChanged(() => Traction4FanTouchOther);
            }
        }

        public double Brake1FanTouchOther
        {
            get { return m_Brake1FanTouchOther; }
            set
            {
                if (value.Equals(m_Brake1FanTouchOther))
                {
                    return;
                }
                m_Brake1FanTouchOther = value;
                RaisePropertyChanged(() => Brake1FanTouchOther);
            }
        }

        public double Brake2FanTouchOther
        {
            get { return m_Brake2FanTouchOther; }
            set
            {
                if (value.Equals(m_Brake2FanTouchOther))
                {
                    return;
                }
                m_Brake2FanTouchOther = value;
                RaisePropertyChanged(() => Brake2FanTouchOther);
            }
        }

        public double TransformerFanOther
        {
            get { return m_TransformerFanOther; }
            set
            {
                if (value.Equals(m_TransformerFanOther))
                {
                    return;
                }
                m_TransformerFanOther = value;
                RaisePropertyChanged(() => TransformerFanOther);
            }
        }

        public double OilPumpRelayOther
        {
            get { return m_OilPumpRelayOther; }
            set
            {
                if (value.Equals(m_OilPumpRelayOther))
                {
                    return;
                }
                m_OilPumpRelayOther = value;
                RaisePropertyChanged(() => OilPumpRelayOther);
            }
        }

        public double Camera1TouchOther
        {
            get { return m_Camera1TouchOther; }
            set
            {
                if (value.Equals(m_Camera1TouchOther))
                {
                    return;
                }
                m_Camera1TouchOther = value;
                RaisePropertyChanged(() => Camera1TouchOther);
            }
        }

        public double Camera2TouchOther
        {
            get { return m_Camera2TouchOther; }
            set
            {
                if (value.Equals(m_Camera2TouchOther))
                {
                    return;
                }
                m_Camera2TouchOther = value;
                RaisePropertyChanged(() => Camera2TouchOther);
            }
        }

        public double CameraRelayOpenOther
        {
            get { return m_CameraRelayOpenOther; }
            set
            {
                if (value.Equals(m_CameraRelayOpenOther))
                {
                    return;
                }
                m_CameraRelayOpenOther = value;
                RaisePropertyChanged(() => CameraRelayOpenOther);
            }
        }

        public double CompressTouchOther
        {
            get { return m_CompressTouchOther; }
            set
            {
                if (value.Equals(m_CompressTouchOther))
                {
                    return;
                }
                m_CompressTouchOther = value;
                RaisePropertyChanged(() => CompressTouchOther);
            }
        }
    }
}
