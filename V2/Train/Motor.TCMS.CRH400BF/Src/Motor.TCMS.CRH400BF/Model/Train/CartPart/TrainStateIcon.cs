using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.Model.Constant;

namespace Motor.TCMS.CRH400BF.Model.Train.CartPart
{
    public class TrainStateIcon : NotificationObject
    {
        private DoorStateIcon m_DoorStateIcon;
        private MainBreakerStateIcon m_MainBreakerStateIcon;
        private ChangePortIcon m_ChangePortIcon;
        private ShowStateIcon m_ExternalPower;
        private ShowStateIcon m_PassengerCall;
        private ShowStateIcon m_AutoSafetyEquip;
        private ShowStateIcon m_FireCall;
        private ShowStateIcon m_EmptyRun;
        private AssistInvertorStateIcon m_OneOrThreeAssistInvertor;
        private AssistInvertorStateIcon m_SixOrEightAssistInvertor;
        private BatteryChargerStateIcon m_OneOrEightBatteryCharger;
        private BatteryChargerStateIcon m_EightBatteryCharger;


        public DoorStateIcon DoorStateIcon
        {
            get { return m_DoorStateIcon; }
            set
            {
                if (value == m_DoorStateIcon)
                {
                    return;
                }

                m_DoorStateIcon = value;
                RaisePropertyChanged(() => DoorStateIcon);
            }
        }

        public MainBreakerStateIcon MainBreakerStateIcon
        {
            get { return m_MainBreakerStateIcon; }
            set
            {
                if (value == m_MainBreakerStateIcon)
                {
                    return;
                }

                m_MainBreakerStateIcon = value;
                RaisePropertyChanged(() => MainBreakerStateIcon);
            }
        }


        public ChangePortIcon ChangePortIcon
        {
            get { return m_ChangePortIcon; }
            set
            {
                if (value == m_ChangePortIcon)
                {
                    return;
                }

                m_ChangePortIcon = value;
                RaisePropertyChanged(() => ChangePortIcon);
            }
        }

        public ShowStateIcon ExternalPower
        {
            get { return m_ExternalPower; }
            set
            {
                if (value == m_ExternalPower)
                {
                    return;
                }

                m_ExternalPower = value;
                RaisePropertyChanged(() => ExternalPower);
            }
        }

        public ShowStateIcon PassengerCall
        {
            get { return m_PassengerCall; }
            set
            {
                if (value == m_PassengerCall)
                {
                    return;
                }

                m_PassengerCall = value;
                RaisePropertyChanged(() => PassengerCall);
            }
        }

        public ShowStateIcon AutoSafetyEquip
        {
            get { return m_AutoSafetyEquip; }
            set
            {
                if (value == m_AutoSafetyEquip)
                {
                    return;
                }

                m_AutoSafetyEquip = value;
                RaisePropertyChanged(() => AutoSafetyEquip);
            }
        }

        public ShowStateIcon FireCall
        {
            get { return m_FireCall; }
            set
            {
                if (value == m_FireCall)
                {
                    return;
                }

                m_FireCall = value;
                RaisePropertyChanged(() => FireCall);
            }
        }

        public ShowStateIcon EmptyRun
        {
            get { return m_EmptyRun; }
            set
            {
                if (value == m_EmptyRun)
                {
                    return;
                }

                m_EmptyRun = value;
                RaisePropertyChanged(() => EmptyRun);
            }
        }

        public AssistInvertorStateIcon OneOrThreeAssistInvertor
        {
            get { return m_OneOrThreeAssistInvertor; }
            set
            {
                if (value == m_OneOrThreeAssistInvertor)
                {
                    return;
                }

                m_OneOrThreeAssistInvertor = value;
                RaisePropertyChanged(() => OneOrThreeAssistInvertor);
            }
        }

        public AssistInvertorStateIcon SixOrEightAssistInvertor
        {
            get { return m_SixOrEightAssistInvertor; }
            set
            {
                if (value == m_SixOrEightAssistInvertor)
                {
                    return;
                }

                m_SixOrEightAssistInvertor = value;
                RaisePropertyChanged(() => SixOrEightAssistInvertor);
            }
        }

        public BatteryChargerStateIcon OneOrEightBatteryCharger
        {
            get { return m_OneOrEightBatteryCharger; }
            set
            {
                if (value == m_OneOrEightBatteryCharger)
                {
                    return;
                }

                m_OneOrEightBatteryCharger = value;
                RaisePropertyChanged(() => OneOrEightBatteryCharger);
            }
        }

        public BatteryChargerStateIcon EightBatteryCharger
        {
            get { return m_EightBatteryCharger; }
            set
            {
                if (value == m_EightBatteryCharger)
                    return;

                m_EightBatteryCharger = value;
                RaisePropertyChanged(() => EightBatteryCharger);
            }
        }
    }

}
