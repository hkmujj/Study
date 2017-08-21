using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Model.Domain.Title
{
    [Export]
    public class TitleModel : NotificationObject
    {
        private AscSettingState m_AscSettingState;
        private PrepareRunActivationState m_PrepareRunActivationState;
        private InfoMainBreakState m_InfoMainBreakState;
        private SettingPlacedInZeroType m_SettingPlacedInZeroType;
        private TrainIsReadyState m_TrainIsReadyState;
        private InfoLightState m_InfoLightState;
        private AutoSafeDeviceState m_AutoSafeDeviceState;
        private DriverRoomChangeModeState m_DriverRoomChangeModeState;
        private InfoDoorState m_InfoDoorState;
        private FireState m_FireState;
        private InfoDriverRoomDoorState m_InfoDriverRoomDoorState;
        private TrainPipeCutoffState m_TrainPipeCutoffState;
        private SpeedLimitState m_SpeedLimitState;
        private AtLeastOneDoorOpenState m_AtLeastOneDoorOpenState;
        private float m_Speed;
        private float m_OutTemperature;
        private float m_HighestSpeed;


        public float Speed
        {
            set
            {
                if (value == m_Speed)
                {
                    return;
                }

                m_Speed = value;
                RaisePropertyChanged(() => Speed);
            }
            get { return m_Speed; }
        }

        public float OutTemperature
        {
            set
            {
                if (value == m_OutTemperature)
                {
                    return;
                }

                m_OutTemperature = value;
                RaisePropertyChanged(() => OutTemperature);
            }
            get { return m_OutTemperature; }
        }

        public float HighestSpeed
        {
            set
            {
                if (value == m_HighestSpeed)
                {
                    return;
                }

                m_HighestSpeed = value;
                RaisePropertyChanged(() => HighestSpeed);
            }
            get { return m_HighestSpeed; }
        }

        public AscSettingState AscSettingState
        {
            set
            {
                if (value == m_AscSettingState)
                {
                    return;
                }

                m_AscSettingState = value;
                RaisePropertyChanged(() => AscSettingState);
            }
            get { return m_AscSettingState; }
        }

        public PrepareRunActivationState PrepareRunActivationState
        {
            set
            {
                if (value == m_PrepareRunActivationState)
                {
                    return;
                }

                m_PrepareRunActivationState = value;
                RaisePropertyChanged(() => PrepareRunActivationState);
            }
            get { return m_PrepareRunActivationState; }
        }

        public InfoMainBreakState InfoMainBreakState
        {
            set
            {
                if (value == m_InfoMainBreakState)
                {
                    return;
                }

                m_InfoMainBreakState = value;
                RaisePropertyChanged(() => InfoMainBreakState);
            }
            get { return m_InfoMainBreakState; }
        }

        public SettingPlacedInZeroType SettingPlacedInZeroType
        {
            set
            {
                if (value == m_SettingPlacedInZeroType)
                {
                    return;
                }

                m_SettingPlacedInZeroType = value;
                RaisePropertyChanged(() => SettingPlacedInZeroType);
            }
            get { return m_SettingPlacedInZeroType; }
        }

        public TrainIsReadyState TrainIsReadyState
        {
            set
            {
                if (value == m_TrainIsReadyState)
                {
                    return;
                }

                m_TrainIsReadyState = value;
                RaisePropertyChanged(() => TrainIsReadyState);
            }
            get { return m_TrainIsReadyState; }
        }

        public InfoLightState InfoLightState
        {
            set
            {
                if (value == m_InfoLightState)
                {
                    return;
                }

                m_InfoLightState = value;
                RaisePropertyChanged(() => InfoLightState);
            }
            get { return m_InfoLightState; }
        }

        public AutoSafeDeviceState AutoSafeDeviceState
        {
            set
            {
                if (value == m_AutoSafeDeviceState)
                {
                    return;
                }

                m_AutoSafeDeviceState = value;
                RaisePropertyChanged(() => AutoSafeDeviceState);
            }
            get { return m_AutoSafeDeviceState; }
        }

        public DriverRoomChangeModeState DriverRoomChangeModeState
        {
            set
            {
                if (value == m_DriverRoomChangeModeState)
                {
                    return;
                }

                m_DriverRoomChangeModeState = value;
                RaisePropertyChanged(() => DriverRoomChangeModeState);
            }
            get { return m_DriverRoomChangeModeState; }
        }

        public InfoDoorState InfoDoorState
        {
            set
            {
                if (value == m_InfoDoorState)
                {
                    return;
                }

                m_InfoDoorState = value;
                RaisePropertyChanged(() => InfoDoorState);
            }
            get { return m_InfoDoorState; }
        }

        public FireState FireState
        {
            set
            {
                if (value == m_FireState)
                {
                    return;
                }

                m_FireState = value;
                RaisePropertyChanged(() => FireState);
            }
            get { return m_FireState; }
        }

        public InfoDriverRoomDoorState InfoDriverRoomDoorState
        {
            set
            {
                if (value == m_InfoDriverRoomDoorState)
                {
                    return;
                }

                m_InfoDriverRoomDoorState = value;
                RaisePropertyChanged(() => InfoDriverRoomDoorState);
            }
            get { return m_InfoDriverRoomDoorState; }
        }

        public TrainPipeCutoffState TrainPipeCutoffState
        {
            set
            {
                if (value == m_TrainPipeCutoffState)
                {
                    return;
                }

                m_TrainPipeCutoffState = value;
                RaisePropertyChanged(() => TrainPipeCutoffState);
            }
            get { return m_TrainPipeCutoffState; }
        }
        public SpeedLimitState SpeedLimitState
        {
            set
            {
                if (value == m_SpeedLimitState)
                {
                    return;
                }

                m_SpeedLimitState = value;
                RaisePropertyChanged(() => SpeedLimitState);
            }
            get { return m_SpeedLimitState; }
        }

        public AtLeastOneDoorOpenState AtLeastOneDoorOpenState
        {
            set
            {
                if (value == m_AtLeastOneDoorOpenState)
                {
                    return;
                }

                m_AtLeastOneDoorOpenState = value;
                RaisePropertyChanged(() => AtLeastOneDoorOpenState);
            }
            get { return m_AtLeastOneDoorOpenState; }
        }
    }
}
