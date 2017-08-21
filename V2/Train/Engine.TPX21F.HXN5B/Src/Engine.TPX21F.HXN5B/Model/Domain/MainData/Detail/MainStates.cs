using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.MainData.Detail
{
    [Export]
    public class MainStates : NotificationObject
    {
        private VigilantState m_VigilantState;
        private TrainDirection m_Direction;
        private StorageBatteryState m_StorageBatteryState;
        private SonaState m_SonaState;
        private SingleBrakeAlert m_SingleBrakeAlert;
        private SandState m_SandState;
        private ReconnexionAlertState m_ReconnexionAlertState;
        private PenaltyBrake m_PenaltyBrake;
        private PCSState m_PCSState;
        private ParkingBrakeState m_ParkingBrakeState;
        private OtherTrainAlert m_OtherTrainAlert;
        private OperationConsle m_OperationConsle;
        private OilLowAlertState m_OilLowAlertState;
        private NormalBrake m_NormalBrake;
        private EmergBrakeState m_EmergBrakeState;
        private BcCutState m_Bc2CutState;
        private BcCutState m_Bc1CutState;
        private RacingState m_RacingState;
        private WorkState m_WorkState;
        private HandlLevel m_HandlLevel;

        public HandlLevel HandlLevel
        {
            set
            {
                if (value == m_HandlLevel)
                {
                    return;
                }

                m_HandlLevel = value;
                RaisePropertyChanged(() => HandlLevel);
            }
            get { return m_HandlLevel; }
        }

        public WorkState WorkState
        {
            set
            {
                if (value == m_WorkState)
                {
                    return;
                }

                m_WorkState = value;
                RaisePropertyChanged(() => WorkState);
            }
            get { return m_WorkState; }
        }

        public RacingState RacingState
        {
            set
            {
                if (value == m_RacingState)
                {
                    return;
                }

                m_RacingState = value;
                RaisePropertyChanged(() => RacingState);
            }
            get { return m_RacingState; }
        }

        public BcCutState Bc1CutState
        {
            set
            {
                if (value == m_Bc1CutState)
                {
                    return;
                }

                m_Bc1CutState = value;
                RaisePropertyChanged(() => Bc1CutState);
            }
            get { return m_Bc1CutState; }
        }

        public BcCutState Bc2CutState
        {
            set
            {
                if (value == m_Bc2CutState)
                {
                    return;
                }

                m_Bc2CutState = value;
                RaisePropertyChanged(() => Bc2CutState);
            }
            get { return m_Bc2CutState; }
        }

        public EmergBrakeState EmergBrakeState
        {
            set
            {
                if (value == m_EmergBrakeState)
                {
                    return;
                }

                m_EmergBrakeState = value;
                RaisePropertyChanged(() => EmergBrakeState);
            }
            get { return m_EmergBrakeState; }
        }

        public NormalBrake NormalBrake
        {
            get { return m_NormalBrake; }
            set
            {
                if (value == m_NormalBrake)
                {
                    return;
                }

                m_NormalBrake = value;
                RaisePropertyChanged(() => NormalBrake);
            }
        }

        public OilLowAlertState OilLowAlertState
        {
            get { return m_OilLowAlertState; }
            set
            {
                if (value == m_OilLowAlertState)
                {
                    return;
                }

                m_OilLowAlertState = value;
                RaisePropertyChanged(() => OilLowAlertState);
            }
        }

        public OperationConsle OperationConsle
        {
            get { return m_OperationConsle; }
            set
            {
                if (value == m_OperationConsle)
                {
                    return;
                }

                m_OperationConsle = value;
                RaisePropertyChanged(() => OperationConsle);
            }
        }

        public OtherTrainAlert OtherTrainAlert
        {
            get { return m_OtherTrainAlert; }
            set
            {
                if (value == m_OtherTrainAlert)
                {
                    return;
                }

                m_OtherTrainAlert = value;
                RaisePropertyChanged(() => OtherTrainAlert);
            }
        }

        public ParkingBrakeState ParkingBrakeState
        {
            get { return m_ParkingBrakeState; }
            set
            {
                if (value == m_ParkingBrakeState)
                {
                    return;
                }

                m_ParkingBrakeState = value;
                RaisePropertyChanged(() => ParkingBrakeState);
            }
        }

        public PCSState PCSState
        {
            get { return m_PCSState; }
            set
            {
                if (value == m_PCSState)
                {
                    return;
                }

                m_PCSState = value;
                RaisePropertyChanged(() => PCSState);
            }
        }

        public PenaltyBrake PenaltyBrake
        {
            get { return m_PenaltyBrake; }
            set
            {
                if (value == m_PenaltyBrake)
                {
                    return;
                }

                m_PenaltyBrake = value;
                RaisePropertyChanged(() => PenaltyBrake);
            }
        }

        public ReconnexionAlertState ReconnexionAlertState
        {
            get { return m_ReconnexionAlertState; }
            set
            {
                if (value == m_ReconnexionAlertState)
                {
                    return;
                }

                m_ReconnexionAlertState = value;
                RaisePropertyChanged(() => ReconnexionAlertState);
            }
        }

        public SandState SandState
        {
            get { return m_SandState; }
            set
            {
                if (value == m_SandState)
                {
                    return;
                }

                m_SandState = value;
                RaisePropertyChanged(() => SandState);
            }
        }

        public SingleBrakeAlert SingleBrakeAlert
        {
            get { return m_SingleBrakeAlert; }
            set
            {
                if (value == m_SingleBrakeAlert)
                {
                    return;
                }

                m_SingleBrakeAlert = value;
                RaisePropertyChanged(() => SingleBrakeAlert);
            }
        }

        public SonaState SonaState
        {
            get { return m_SonaState; }
            set
            {
                if (value == m_SonaState)
                {
                    return;
                }

                m_SonaState = value;
                RaisePropertyChanged(() => SonaState);
            }
        }

        public StorageBatteryState StorageBatteryState
        {
            get { return m_StorageBatteryState; }
            set
            {
                if (value == m_StorageBatteryState)
                {
                    return;
                }

                m_StorageBatteryState = value;
                RaisePropertyChanged(() => StorageBatteryState);
            }
        }

        public TrainDirection Direction
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

        public VigilantState VigilantState
        {
            get { return m_VigilantState; }
            set
            {
                if (value == m_VigilantState)
                {
                    return;
                }

                m_VigilantState = value;
                RaisePropertyChanged(() => VigilantState);
            }
        }
    }
}