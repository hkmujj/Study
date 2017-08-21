using System.ComponentModel.Composition;

namespace Engine.HMI.SS3B.View.ViewModel.LiuZhou
{

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class StatusViewModel:ViewModelBase
    {
        private double m_MasterFaultOther;
        private double m_BowFaultOther;
        private double m_EmptyTestSwitch2Other;
        private double m_EmptyTestSwitch1Other;
        private double m_StoreroomSwitch2Other;
        private double m_StoreroomSwitch1Other;
        private double m_ZeroPressFaultOther;
        private double m_OilPumpFaultOther;
        private double m_TransformerFanFaultOther;
        private double m_BrakeFanFault2Other;
        private double m_BrakeFanFault1Other;
        private double m_TractionFanFault4Other;
        private double m_TractionFanFault3Other;
        private double m_TractionFanFault2Other;
        private double m_TractionFanFault1Other;
        private double m_CompressFaultOther;
        private double m_CameraFault2Other;
        private double m_CameraFault1Other;
        private double m_FanSpeedRelayFault2Other;
        private double m_FanSpeedRelayFault1Other;
        private double m_LineTouchFault6Other;
        private double m_LineTouchFault5Other;
        private double m_LineTouchFault4Other;
        private double m_LineTouchFault3Other;
        private double m_LineTouchFault2Other;
        private double m_LineTouchFault1Other;
        private double m_MasterFaultOrigin;
        private double m_BowFaultOrigin;
        private double m_EmptyTestSwitch2Origin;
        private double m_EmptyTestSwitch1Origin;
        private double m_StoreroomSwitch2Origin;
        private double m_StoreroomSwitch1Origin;
        private double m_ZeroPressFaultOrigin;
        private double m_OilPumpFaultOrigin;
        private double m_TransformerFanFaultOrigin;
        private double m_BrakeFanFault2Origin;
        private double m_BrakeFanFault1Origin;
        private double m_TractionFanFault4Origin;
        private double m_TractionFanFault3Origin;
        private double m_TractionFanFault2Origin;
        private double m_TractionFanFault1Origin;
        private double m_CompressFaultOrigin;
        private double m_CameraFault2Origin;
        private double m_CameraFault1Origin;
        private double m_FanSpeedRelayFault2Origin;
        private double m_FanSpeedRelayFault1Origin;
        private double m_LineTouchFault6Origin;
        private double m_LineTouchFault5Origin;
        private double m_LineTouchFault4Origin;
        private double m_LineTouchFault3Origin;
        private double m_LineTouchFault2Origin;
        private double m_LineTouchFault1Origin;

        public double LineTouchFault1Origin
        {
            get { return m_LineTouchFault1Origin; }
            set
            {
                if (value.Equals(m_LineTouchFault1Origin))
                {
                    return;
                }
                m_LineTouchFault1Origin = value;
                RaisePropertyChanged(() => LineTouchFault1Origin);
            }
        }

        public double LineTouchFault2Origin
        {
            get { return m_LineTouchFault2Origin; }
            set
            {
                if (value.Equals(m_LineTouchFault2Origin))
                {
                    return;
                }
                m_LineTouchFault2Origin = value;
                RaisePropertyChanged(() => LineTouchFault2Origin);
            }
        }

        public double LineTouchFault3Origin
        {
            get { return m_LineTouchFault3Origin; }
            set
            {
                if (value.Equals(m_LineTouchFault3Origin))
                {
                    return;
                }
                m_LineTouchFault3Origin = value;
                RaisePropertyChanged(() => LineTouchFault3Origin);
            }
        }

        public double LineTouchFault4Origin
        {
            get { return m_LineTouchFault4Origin; }
            set
            {
                if (value.Equals(m_LineTouchFault4Origin))
                {
                    return;
                }
                m_LineTouchFault4Origin = value;
                RaisePropertyChanged(() => LineTouchFault4Origin);
            }
        }

        public double LineTouchFault5Origin
        {
            get { return m_LineTouchFault5Origin; }
            set
            {
                if (value.Equals(m_LineTouchFault5Origin))
                {
                    return;
                }
                m_LineTouchFault5Origin = value;
                RaisePropertyChanged(() => LineTouchFault5Origin);
            }
        }

        public double LineTouchFault6Origin
        {
            get { return m_LineTouchFault6Origin; }
            set
            {
                if (value.Equals(m_LineTouchFault6Origin))
                {
                    return;
                }
                m_LineTouchFault6Origin = value;
                RaisePropertyChanged(() => LineTouchFault6Origin);
            }
        }

        public double FanSpeedRelayFault1Origin
        {
            get { return m_FanSpeedRelayFault1Origin; }
            set
            {
                if (value.Equals(m_FanSpeedRelayFault1Origin))
                {
                    return;
                }
                m_FanSpeedRelayFault1Origin = value;
                RaisePropertyChanged(() => FanSpeedRelayFault1Origin);
            }
        }

        public double FanSpeedRelayFault2Origin
        {
            get { return m_FanSpeedRelayFault2Origin; }
            set
            {
                if (value.Equals(m_FanSpeedRelayFault2Origin))
                {
                    return;
                }
                m_FanSpeedRelayFault2Origin = value;
                RaisePropertyChanged(() => FanSpeedRelayFault2Origin);
            }
        }

        public double CameraFault1Origin
        {
            get { return m_CameraFault1Origin; }
            set
            {
                if (value.Equals(m_CameraFault1Origin))
                {
                    return;
                }
                m_CameraFault1Origin = value;
                RaisePropertyChanged(() => CameraFault1Origin);
            }
        }

        public double CameraFault2Origin
        {
            get { return m_CameraFault2Origin; }
            set
            {
                if (value.Equals(m_CameraFault2Origin))
                {
                    return;
                }
                m_CameraFault2Origin = value;
                RaisePropertyChanged(() => CameraFault2Origin);
            }
        }

        public double CompressFaultOrigin
        {
            get { return m_CompressFaultOrigin; }
            set
            {
                if (value.Equals(m_CompressFaultOrigin))
                {
                    return;
                }
                m_CompressFaultOrigin = value;
                RaisePropertyChanged(() => CompressFaultOrigin);
            }
        }

        public double TractionFanFault1Origin
        {
            get { return m_TractionFanFault1Origin; }
            set
            {
                if (value.Equals(m_TractionFanFault1Origin))
                {
                    return;
                }
                m_TractionFanFault1Origin = value;
                RaisePropertyChanged(() => TractionFanFault1Origin);
            }
        }

        public double TractionFanFault2Origin
        {
            get { return m_TractionFanFault2Origin; }
            set
            {
                if (value.Equals(m_TractionFanFault2Origin))
                {
                    return;
                }
                m_TractionFanFault2Origin = value;
                RaisePropertyChanged(() => TractionFanFault2Origin);
            }
        }

        public double TractionFanFault3Origin
        {
            get { return m_TractionFanFault3Origin; }
            set
            {
                if (value.Equals(m_TractionFanFault3Origin))
                {
                    return;
                }
                m_TractionFanFault3Origin = value;
                RaisePropertyChanged(() => TractionFanFault3Origin);
            }
        }

        public double TractionFanFault4Origin
        {
            get { return m_TractionFanFault4Origin; }
            set
            {
                if (value.Equals(m_TractionFanFault4Origin))
                {
                    return;
                }
                m_TractionFanFault4Origin = value;
                RaisePropertyChanged(() => TractionFanFault4Origin);
            }
        }

        public double BrakeFanFault1Origin
        {
            get { return m_BrakeFanFault1Origin; }
            set
            {
                if (value.Equals(m_BrakeFanFault1Origin))
                {
                    return;
                }
                m_BrakeFanFault1Origin = value;
                RaisePropertyChanged(() => BrakeFanFault1Origin);
            }
        }

        public double BrakeFanFault2Origin
        {
            get { return m_BrakeFanFault2Origin; }
            set
            {
                if (value.Equals(m_BrakeFanFault2Origin))
                {
                    return;
                }
                m_BrakeFanFault2Origin = value;
                RaisePropertyChanged(() => BrakeFanFault2Origin);
            }
        }

        public double TransformerFanFaultOrigin
        {
            get { return m_TransformerFanFaultOrigin; }
            set
            {
                if (value.Equals(m_TransformerFanFaultOrigin))
                {
                    return;
                }
                m_TransformerFanFaultOrigin = value;
                RaisePropertyChanged(() => TransformerFanFaultOrigin);
            }
        }

        public double OilPumpFaultOrigin
        {
            get { return m_OilPumpFaultOrigin; }
            set
            {
                if (value.Equals(m_OilPumpFaultOrigin))
                {
                    return;
                }
                m_OilPumpFaultOrigin = value;
                RaisePropertyChanged(() => OilPumpFaultOrigin);
            }
        }

        public double ZeroPressFaultOrigin
        {
            get { return m_ZeroPressFaultOrigin; }
            set
            {
                if (value.Equals(m_ZeroPressFaultOrigin))
                {
                    return;
                }
                m_ZeroPressFaultOrigin = value;
                RaisePropertyChanged(() => ZeroPressFaultOrigin);
            }
        }

        public double StoreroomSwitch1Origin
        {
            get { return m_StoreroomSwitch1Origin; }
            set
            {
                if (value.Equals(m_StoreroomSwitch1Origin))
                {
                    return;
                }
                m_StoreroomSwitch1Origin = value;
                RaisePropertyChanged(() => StoreroomSwitch1Origin);
            }
        }

        public double StoreroomSwitch2Origin
        {
            get { return m_StoreroomSwitch2Origin; }
            set
            {
                if (value.Equals(m_StoreroomSwitch2Origin))
                {
                    return;
                }
                m_StoreroomSwitch2Origin = value;
                RaisePropertyChanged(() => StoreroomSwitch2Origin);
            }
        }

        public double EmptyTestSwitch1Origin
        {
            get { return m_EmptyTestSwitch1Origin; }
            set
            {
                if (value.Equals(m_EmptyTestSwitch1Origin))
                {
                    return;
                }
                m_EmptyTestSwitch1Origin = value;
                RaisePropertyChanged(() => EmptyTestSwitch1Origin);
            }
        }

        public double EmptyTestSwitch2Origin
        {
            get { return m_EmptyTestSwitch2Origin; }
            set
            {
                if (value.Equals(m_EmptyTestSwitch2Origin))
                {
                    return;
                }
                m_EmptyTestSwitch2Origin = value;
                RaisePropertyChanged(() => EmptyTestSwitch2Origin);
            }
        }

        public double BowFaultOrigin
        {
            get { return m_BowFaultOrigin; }
            set
            {
                if (value.Equals(m_BowFaultOrigin))
                {
                    return;
                }
                m_BowFaultOrigin = value;
                RaisePropertyChanged(() => BowFaultOrigin);
            }
        }

        public double MasterFaultOrigin
        {
            get { return m_MasterFaultOrigin; }
            set
            {
                if (value.Equals(m_MasterFaultOrigin))
                {
                    return;
                }
                m_MasterFaultOrigin = value;
                RaisePropertyChanged(() => MasterFaultOrigin);
            }
        }

        public double LineTouchFault1Other
        {
            get { return m_LineTouchFault1Other; }
            set
            {
                if (value.Equals(m_LineTouchFault1Other))
                {
                    return;
                }
                m_LineTouchFault1Other = value;
                RaisePropertyChanged(() => LineTouchFault1Other);
            }
        }

        public double LineTouchFault2Other
        {
            get { return m_LineTouchFault2Other; }
            set
            {
                if (value.Equals(m_LineTouchFault2Other))
                {
                    return;
                }
                m_LineTouchFault2Other = value;
                RaisePropertyChanged(() => LineTouchFault2Other);
            }
        }

        public double LineTouchFault3Other
        {
            get { return m_LineTouchFault3Other; }
            set
            {
                if (value.Equals(m_LineTouchFault3Other))
                {
                    return;
                }
                m_LineTouchFault3Other = value;
                RaisePropertyChanged(() => LineTouchFault3Other);
            }
        }

        public double LineTouchFault4Other
        {
            get { return m_LineTouchFault4Other; }
            set
            {
                if (value.Equals(m_LineTouchFault4Other))
                {
                    return;
                }
                m_LineTouchFault4Other = value;
                RaisePropertyChanged(() => LineTouchFault4Other);
            }
        }

        public double LineTouchFault5Other
        {
            get { return m_LineTouchFault5Other; }
            set
            {
                if (value.Equals(m_LineTouchFault5Other))
                {
                    return;
                }
                m_LineTouchFault5Other = value;
                RaisePropertyChanged(() => LineTouchFault5Other);
            }
        }

        public double LineTouchFault6Other
        {
            get { return m_LineTouchFault6Other; }
            set
            {
                if (value.Equals(m_LineTouchFault6Other))
                {
                    return;
                }
                m_LineTouchFault6Other = value;
                RaisePropertyChanged(() => LineTouchFault6Other);
            }
        }

        public double FanSpeedRelayFault1Other
        {
            get { return m_FanSpeedRelayFault1Other; }
            set
            {
                if (value.Equals(m_FanSpeedRelayFault1Other))
                {
                    return;
                }
                m_FanSpeedRelayFault1Other = value;
                RaisePropertyChanged(() => FanSpeedRelayFault1Other);
            }
        }

        public double FanSpeedRelayFault2Other
        {
            get { return m_FanSpeedRelayFault2Other; }
            set
            {
                if (value.Equals(m_FanSpeedRelayFault2Other))
                {
                    return;
                }
                m_FanSpeedRelayFault2Other = value;
                RaisePropertyChanged(() => FanSpeedRelayFault2Other);
            }
        }

        public double CameraFault1Other
        {
            get { return m_CameraFault1Other; }
            set
            {
                if (value.Equals(m_CameraFault1Other))
                {
                    return;
                }
                m_CameraFault1Other = value;
                RaisePropertyChanged(() => CameraFault1Other);
            }
        }

        public double CameraFault2Other
        {
            get { return m_CameraFault2Other; }
            set
            {
                if (value.Equals(m_CameraFault2Other))
                {
                    return;
                }
                m_CameraFault2Other = value;
                RaisePropertyChanged(() => CameraFault2Other);
            }
        }

        public double CompressFaultOther
        {
            get { return m_CompressFaultOther; }
            set
            {
                if (value.Equals(m_CompressFaultOther))
                {
                    return;
                }
                m_CompressFaultOther = value;
                RaisePropertyChanged(() => CompressFaultOther);
            }
        }

        public double TractionFanFault1Other
        {
            get { return m_TractionFanFault1Other; }
            set
            {
                if (value.Equals(m_TractionFanFault1Other))
                {
                    return;
                }
                m_TractionFanFault1Other = value;
                RaisePropertyChanged(() => TractionFanFault1Other);
            }
        }

        public double TractionFanFault2Other
        {
            get { return m_TractionFanFault2Other; }
            set
            {
                if (value.Equals(m_TractionFanFault2Other))
                {
                    return;
                }
                m_TractionFanFault2Other = value;
                RaisePropertyChanged(() => TractionFanFault2Other);
            }
        }

        public double TractionFanFault3Other
        {
            get { return m_TractionFanFault3Other; }
            set
            {
                if (value.Equals(m_TractionFanFault3Other))
                {
                    return;
                }
                m_TractionFanFault3Other = value;
                RaisePropertyChanged(() => TractionFanFault3Other);
            }
        }

        public double TractionFanFault4Other
        {
            get { return m_TractionFanFault4Other; }
            set
            {
                if (value.Equals(m_TractionFanFault4Other))
                {
                    return;
                }
                m_TractionFanFault4Other = value;
                RaisePropertyChanged(() => TractionFanFault4Other);
            }
        }

        public double BrakeFanFault1Other
        {
            get { return m_BrakeFanFault1Other; }
            set
            {
                if (value.Equals(m_BrakeFanFault1Other))
                {
                    return;
                }
                m_BrakeFanFault1Other = value;
                RaisePropertyChanged(() => BrakeFanFault1Other);
            }
        }

        public double BrakeFanFault2Other
        {
            get { return m_BrakeFanFault2Other; }
            set
            {
                if (value.Equals(m_BrakeFanFault2Other))
                {
                    return;
                }
                m_BrakeFanFault2Other = value;
                RaisePropertyChanged(() => BrakeFanFault2Other);
            }
        }

        public double TransformerFanFaultOther
        {
            get { return m_TransformerFanFaultOther; }
            set
            {
                if (value.Equals(m_TransformerFanFaultOther))
                {
                    return;
                }
                m_TransformerFanFaultOther = value;
                RaisePropertyChanged(() => TransformerFanFaultOther);
            }
        }

        public double OilPumpFaultOther
        {
            get { return m_OilPumpFaultOther; }
            set
            {
                if (value.Equals(m_OilPumpFaultOther))
                {
                    return;
                }
                m_OilPumpFaultOther = value;
                RaisePropertyChanged(() => OilPumpFaultOther);
            }
        }

        public double ZeroPressFaultOther
        {
            get { return m_ZeroPressFaultOther; }
            set
            {
                if (value.Equals(m_ZeroPressFaultOther))
                {
                    return;
                }
                m_ZeroPressFaultOther = value;
                RaisePropertyChanged(() => ZeroPressFaultOther);
            }
        }

        public double StoreroomSwitch1Other
        {
            get { return m_StoreroomSwitch1Other; }
            set
            {
                if (value.Equals(m_StoreroomSwitch1Other))
                {
                    return;
                }
                m_StoreroomSwitch1Other = value;
                RaisePropertyChanged(() => StoreroomSwitch1Other);
            }
        }

        public double StoreroomSwitch2Other
        {
            get { return m_StoreroomSwitch2Other; }
            set
            {
                if (value.Equals(m_StoreroomSwitch2Other))
                {
                    return;
                }
                m_StoreroomSwitch2Other = value;
                RaisePropertyChanged(() => StoreroomSwitch2Other);
            }
        }

        public double EmptyTestSwitch1Other
        {
            get { return m_EmptyTestSwitch1Other; }
            set
            {
                if (value.Equals(m_EmptyTestSwitch1Other))
                {
                    return;
                }
                m_EmptyTestSwitch1Other = value;
                RaisePropertyChanged(() => EmptyTestSwitch1Other);
            }
        }

        public double EmptyTestSwitch2Other
        {
            get { return m_EmptyTestSwitch2Other; }
            set
            {
                if (value.Equals(m_EmptyTestSwitch2Other))
                {
                    return;
                }
                m_EmptyTestSwitch2Other = value;
                RaisePropertyChanged(() => EmptyTestSwitch2Other);
            }
        }

        public double BowFaultOther
        {
            get { return m_BowFaultOther; }
            set
            {
                if (value.Equals(m_BowFaultOther))
                {
                    return;
                }
                m_BowFaultOther = value;
                RaisePropertyChanged(() => BowFaultOther);
            }
        }

        public double MasterFaultOther
        {
            get { return m_MasterFaultOther; }
            set
            {
                if (value.Equals(m_MasterFaultOther))
                {
                    return;
                }
                m_MasterFaultOther = value;
                RaisePropertyChanged(() => MasterFaultOther);
            }
        }
    }
}
