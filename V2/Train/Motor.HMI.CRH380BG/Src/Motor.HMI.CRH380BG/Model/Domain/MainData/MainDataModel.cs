using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Model.Domain.MainData
{
    [Export]
    public class MainDataModel : NotificationObject
    {
        private double m_ContactNetVoltage;
        private double m_NetworkSideCurrent;
        private double m_BatteryVoltage1;
        private double m_BatteryVoltage2;
        private double m_EVCSpeed;
        private double m_TrctionBrakeActualPercent1;
        private double m_TrctionBrakeActualPercent2;
        private double m_TrctionBrakeActualPercent3;
        private double m_TrctionBrakeActualPercent4;
        private double m_TrctionBrakeSetPercent1;
        private double m_TrctionBrakeSetPercent2;
        private double m_TrctionBrakeSetPercent3;
        private double m_TrctionBrakeSetPercent4;
        private bool m_EVCIsDisplay;
        private bool m_RedTriangleVisible;
        private bool m_NetCurrent300AVisible;
        private bool m_NetCurrent400AVisible;
        private bool m_NetCurrent500AVisible;
        private bool m_NetCurrent600AVisible;
        private bool m_WelcomVisible;
        private bool m_TimeVisible;
        private bool m_TrainConfigVisible;
        private bool m_ConfirmConfigVisible;
        private PantographState m_PantographState1;
        private PantographState m_PantographState2;
        private MainBreakState m_MainBreakState1;
        private MainBreakState m_MainBreakState2;
        private VoltageChargerState m_VoltageChargerState1;
        private VoltageChargerState m_VoltageChargerState2;
        private ChargerState m_ChargerState1;
        private ChargerState m_ChargerState2;
        private AutoSpeedControlType m_AutoSpeedControlType;
        private TractionBrakeType m_TractionBrakeType;

       


        public double ContactNetVoltage
        {
            get { return m_ContactNetVoltage; }
            set
            {
                if (value.Equals(m_ContactNetVoltage))
                {
                    return;
                }

                m_ContactNetVoltage = value;
                RaisePropertyChanged(() => ContactNetVoltage);
            }
        }

        public double NetworkSideCurrent
        {
            get { return m_NetworkSideCurrent; }
            set
            {
                if (value.Equals(m_NetworkSideCurrent))
                {
                    return;
                }

                m_NetworkSideCurrent = value;
                RaisePropertyChanged(() => NetworkSideCurrent);
            }
        }

        public double BatteryVoltage1
        {
            get { return m_BatteryVoltage1; }
            set
            {
                if (value.Equals(m_BatteryVoltage1))
                {
                    return;
                }

                m_BatteryVoltage1 = value;
                RaisePropertyChanged(() => BatteryVoltage1);
            }
        }

        public double BatteryVoltage2
        {
            get { return m_BatteryVoltage2; }
            set
            {
                if (value.Equals(m_BatteryVoltage2))
                {
                    return;
                }

                m_BatteryVoltage2 = value;
                RaisePropertyChanged(() => BatteryVoltage2);
            }
        }

        public double EVCSpeed
        {
            get { return m_EVCSpeed; }
            set
            {
                if (value.Equals(m_EVCSpeed))
                {
                    return;
                }

                m_EVCSpeed = value;
                RaisePropertyChanged(() => EVCSpeed);
            }
        }

        public bool EVCIsDisplay
        {
            get { return m_EVCIsDisplay; }
            set
            {
                if (value == m_EVCIsDisplay)
                {
                    return;
                }

                m_EVCIsDisplay = value;
                RaisePropertyChanged(() => EVCIsDisplay);
            }
        }

        public bool RedTriangleVisible
        {
            get { return m_RedTriangleVisible; }
            set
            {
                if (value == m_RedTriangleVisible)
                {
                    return;
                }

                m_RedTriangleVisible = value;
                RaisePropertyChanged(() => RedTriangleVisible);
            }
        }

        public bool NetCurrent300AVisible
        {
            get { return m_NetCurrent300AVisible; }
            set
            {
                if (value == m_NetCurrent300AVisible)
                {
                    return;
                }

                m_NetCurrent300AVisible = value;
                RaisePropertyChanged(() => NetCurrent300AVisible);
            }
        }

        public bool NetCurrent400AVisible
        {
            get { return m_NetCurrent400AVisible; }
            set
            {
                if (value == m_NetCurrent400AVisible)
                {
                    return;
                }

                m_NetCurrent400AVisible = value;
                RaisePropertyChanged(() => NetCurrent400AVisible);
            }
        }

        public bool NetCurrent500AVisible
        {
            get { return m_NetCurrent500AVisible; }
            set
            {
                if (value == m_NetCurrent500AVisible)
                {
                    return;
                }

                m_NetCurrent500AVisible = value;
                RaisePropertyChanged(() => NetCurrent500AVisible);
            }
        }

        public bool NetCurrent600AVisible
        {
            get { return m_NetCurrent600AVisible; }
            set
            {
                if (value == m_NetCurrent600AVisible)
                {
                    return;
                }

                m_NetCurrent600AVisible = value;
                RaisePropertyChanged(() => NetCurrent600AVisible);
            }
        }

        public bool WelcomVisible
        {
            get { return m_WelcomVisible; }
            set
            {
                if (value == m_WelcomVisible)
                {
                    return;
                }

                m_WelcomVisible = value;
                RaisePropertyChanged(() => WelcomVisible);
            }
        }

        public bool TimeVisible
        {
            get { return m_TimeVisible; }
            set
            {
                if (value == m_TimeVisible)
                {
                    return;
                }

                m_TimeVisible = value;
                RaisePropertyChanged(() => TimeVisible);
            }
        }

        public bool TrainConfigVisible
        {
            get { return m_TrainConfigVisible; }
            set
            {
                if (value == m_TrainConfigVisible)
                {
                    return;
                }

                m_TrainConfigVisible = value;
                RaisePropertyChanged(() => TrainConfigVisible);
            }
        }

        public bool ConfirmConfigVisible
        {
            get { return m_ConfirmConfigVisible; }
            set
            {
                if (value == m_ConfirmConfigVisible)
                {
                    return;
                }

                m_ConfirmConfigVisible = value;
                RaisePropertyChanged(() => ConfirmConfigVisible);
            }
        }


        public double TrctionBrakeActualPercent1
        {
            get { return m_TrctionBrakeActualPercent1; }
            set
            {
                if (value.Equals(m_TrctionBrakeActualPercent1))
                {
                    return;
                }

                m_TrctionBrakeActualPercent1 = value;
                RaisePropertyChanged(() => TrctionBrakeActualPercent1);
            }
        }

        public double TrctionBrakeActualPercent2
        {
            get { return m_TrctionBrakeActualPercent2; }
            set
            {
                if (value.Equals(m_TrctionBrakeActualPercent2))
                {
                    return;
                }

                m_TrctionBrakeActualPercent2 = value;
                RaisePropertyChanged(() => TrctionBrakeActualPercent2);
            }
        }

        public double TrctionBrakeActualPercent3
        {
            get { return m_TrctionBrakeActualPercent3; }
            set
            {
                if (value.Equals(m_TrctionBrakeActualPercent3))
                {
                    return;
                }

                m_TrctionBrakeActualPercent3 = value;
                RaisePropertyChanged(() => TrctionBrakeActualPercent3);
            }
        }

        public double TrctionBrakeActualPercent4
        {
            get { return m_TrctionBrakeActualPercent4; }
            set
            {
                if (value.Equals(m_TrctionBrakeActualPercent4))
                {
                    return;
                }

                m_TrctionBrakeActualPercent4 = value;
                RaisePropertyChanged(() => TrctionBrakeActualPercent4);
            }
        }

        public double TrctionBrakeSetPercent1
        {
            get { return m_TrctionBrakeSetPercent1; }
            set
            {
                if (value.Equals(m_TrctionBrakeSetPercent1))
                {
                    return;
                }

                m_TrctionBrakeSetPercent1 = value;
                RaisePropertyChanged(() => TrctionBrakeSetPercent1);
            }
        }

        public double TrctionBrakeSetPercent2
        {
            get { return m_TrctionBrakeSetPercent2; }
            set
            {
                if (value.Equals(m_TrctionBrakeSetPercent2))
                {
                    return;
                }

                m_TrctionBrakeSetPercent2 = value;
                RaisePropertyChanged(() => TrctionBrakeSetPercent2);
            }
        }

        public double TrctionBrakeSetPercent3
        {
            get { return m_TrctionBrakeSetPercent3; }
            set
            {
                if (value.Equals(m_TrctionBrakeSetPercent3))
                {
                    return;
                }

                m_TrctionBrakeSetPercent3 = value;
                RaisePropertyChanged(() => TrctionBrakeSetPercent3);
            }
        }

        public double TrctionBrakeSetPercent4
        {
            get { return m_TrctionBrakeSetPercent4; }
            set
            {
                if (value.Equals(m_TrctionBrakeSetPercent4))
                {
                    return;
                }

                m_TrctionBrakeSetPercent4 = value;
                RaisePropertyChanged(() => TrctionBrakeSetPercent4);
            }
        }

        public PantographState PantographState1
        {
            set
            {
                if (value == m_PantographState1)
                {
                    return;
                }

                m_PantographState1 = value;
                RaisePropertyChanged(() => PantographState1);
            }
            get { return m_PantographState1; }
        }

        public PantographState PantographState2
        {
            set
            {
                if (value == m_PantographState2)
                {
                    return;
                }

                m_PantographState2 = value;
                RaisePropertyChanged(() => PantographState2);
            }
            get { return m_PantographState2; }
        }
        public MainBreakState MainBreakState1
        {
            set
            {
                if (value == m_MainBreakState1)
                {
                    return;
                }

                m_MainBreakState1 = value;
                RaisePropertyChanged(() => MainBreakState1);
            }
            get { return m_MainBreakState1; }
        }

        public MainBreakState MainBreakState2
        {
            set
            {
                if (value == m_MainBreakState2)
                {
                    return;
                }

                m_MainBreakState2 = value;
                RaisePropertyChanged(() => MainBreakState2);
            }
            get { return m_MainBreakState2; }
        }

        public VoltageChargerState VoltageChargerState1
        {
            set
            {
                if (value == m_VoltageChargerState1)
                {
                    return;
                }

                m_VoltageChargerState1 = value;
                RaisePropertyChanged(() => VoltageChargerState1);
            }
            get { return m_VoltageChargerState1; }
        }
        public VoltageChargerState VoltageChargerState2
        {
            set
            {
                if (value == m_VoltageChargerState2)
                {
                    return;
                }

                m_VoltageChargerState2 = value;
                RaisePropertyChanged(() => VoltageChargerState2);
            }
            get { return m_VoltageChargerState2; }
        }
        public ChargerState ChargerState1
        {
            set
            {
                if (value == m_ChargerState1)
                {
                    return;
                }

                m_ChargerState1 = value;
                RaisePropertyChanged(() => ChargerState1);
            }
            get { return m_ChargerState1; }
        }

        public ChargerState ChargerState2
        {
            set
            {
                if (value == m_ChargerState2)
                {
                    return;
                }

                m_ChargerState2 = value;
                RaisePropertyChanged(() => ChargerState2);
            }
            get { return m_ChargerState2; }
        }

        public AutoSpeedControlType AutoSpeedControlType
        {
            set
            {
                if (value == m_AutoSpeedControlType)
                {
                    return;
                }

                m_AutoSpeedControlType = value;
                RaisePropertyChanged(() => AutoSpeedControlType);
            }
            get { return m_AutoSpeedControlType; }
        }

        public TractionBrakeType TractionBrakeType
        {
            set
            {
                if (value == m_TractionBrakeType)
                {
                    return;
                }

                m_TractionBrakeType = value;
                RaisePropertyChanged(() => TractionBrakeType);
            }
            get { return m_TractionBrakeType; }
        }
    }
}
