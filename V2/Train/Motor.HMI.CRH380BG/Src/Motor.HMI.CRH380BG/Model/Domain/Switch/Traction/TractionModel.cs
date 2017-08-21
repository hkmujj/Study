using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380BG.Model.Domain.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.Model.Domain.Switch.Traction
{
    [Export]
    public class TractionModel : NotificationObject
    {
        private PantographState m_Car2PantographState;
        private PantographState m_Car7PantographState;
        private MainBreakState m_Car2MainBreakState;
        private MainBreakState m_Car7MainBreakState;
        private RoofIsolationState m_Car2RoofIsolationState;
        private RoofIsolationState m_Car7RoofIsolationState;
        private MainBreakState m_Car1TractionConverterState;
        private MainBreakState m_Car3TractionConverterState;
        private MainBreakState m_Car6TractionConverterState;
        private MainBreakState m_Car8TractionConverterState;
        private VoltageChargerState m_Car2AssistPoweredUnitState;
        private VoltageChargerState m_Car4AssistPoweredUnit1State;
        private VoltageChargerState m_Car4AssistPoweredUnit2State;
        private VoltageChargerState m_Car5AssistPoweredUnit1State;
        private VoltageChargerState m_Car5AssistPoweredUnit2State;
        private VoltageChargerState m_Car7AssistPoweredUnitState;
        private bool m_AllIsSelect;
        private bool m_Car2PantographIsSelect;
        private bool m_Car7PantographIsSelect;
        private bool m_Car2MainBreakIsSelect;
        private bool m_Car7MainBreakIsSelect;
        private bool m_Car2RoofIsolationIsSelect;
        private bool m_Car7RoofIsolationIsSelect;
        private bool m_Car1TractionConverterIsSelect;
        private bool m_Car3TractionConverterIsSelect;
        private bool m_Car6TractionConverterIsSelect;
        private bool m_Car8TractionConverterIsSelect;
        private int m_SelectNum;

        public TractionModel()
        {
            IsSelect();
            SelectNum = 0;
        }

        public int SelectNum
        {
            get { return m_SelectNum; }
            set
            {
                if (value == m_SelectNum)
                {
                    return;
                }
                m_SelectNum = value;
                IsSelect();
                RaisePropertyChanged(() => SelectNum);
            }
        }
        public PantographState Car2PantographState
        {
            set
            {
                if (value == m_Car2PantographState)
                {
                    return;
                }

                m_Car2PantographState = value;
                RaisePropertyChanged(() => Car2PantographState);
            }
            get { return m_Car2PantographState; }
        }

        public PantographState Car7PantographState
        {
            set
            {
                if (value == m_Car7PantographState)
                {
                    return;
                }

                m_Car7PantographState = value;
                RaisePropertyChanged(() => Car7PantographState);
            }
            get { return m_Car7PantographState; }
        }
        public MainBreakState Car2MainBreakState
        {
            set
            {
                if (value == m_Car2MainBreakState)
                {
                    return;
                }

                m_Car2MainBreakState = value;
                RaisePropertyChanged(() => Car2MainBreakState);
            }
            get { return m_Car2MainBreakState; }
        }
        public MainBreakState Car7MainBreakState
        {
            set
            {
                if (value == m_Car7MainBreakState)
                {
                    return;
                }

                m_Car7MainBreakState = value;
                RaisePropertyChanged(() => Car7MainBreakState);
            }
            get { return m_Car7MainBreakState; }
        }
        public RoofIsolationState Car2RoofIsolationState
        {
            set
            {
                if (value == m_Car2RoofIsolationState)
                {
                    return;
                }

                m_Car2RoofIsolationState = value;
                RaisePropertyChanged(() => Car2RoofIsolationState);
            }
            get { return m_Car2RoofIsolationState; }
        }
        public RoofIsolationState Car7RoofIsolationState
        {
            set
            {
                if (value == m_Car7RoofIsolationState)
                {
                    return;
                }

                m_Car7RoofIsolationState = value;
                RaisePropertyChanged(() => Car7RoofIsolationState);
            }
            get { return m_Car7RoofIsolationState; }
        }

        public MainBreakState Car1TractionConverterState
        {
            set
            {
                if (value == m_Car1TractionConverterState)
                {
                    return;
                }

                m_Car1TractionConverterState = value;
                RaisePropertyChanged(() => Car1TractionConverterState);
            }
            get { return m_Car1TractionConverterState; }
        }
        public MainBreakState Car3TractionConverterState
        {
            set
            {
                if (value == m_Car3TractionConverterState)
                {
                    return;
                }

                m_Car3TractionConverterState = value;
                RaisePropertyChanged(() => Car3TractionConverterState);
            }
            get { return m_Car3TractionConverterState; }
        }
        public MainBreakState Car6TractionConverterState
        {
            set
            {
                if (value == m_Car6TractionConverterState)
                {
                    return;
                }

                m_Car6TractionConverterState = value;
                RaisePropertyChanged(() => Car6TractionConverterState);
            }
            get { return m_Car6TractionConverterState; }
        }
        public MainBreakState Car8TractionConverterState
        {
            set
            {
                if (value == m_Car8TractionConverterState)
                {
                    return;
                }

                m_Car8TractionConverterState = value;
                RaisePropertyChanged(() => Car8TractionConverterState);
            }
            get { return m_Car8TractionConverterState; }
        }

        public VoltageChargerState Car2AssistPoweredUnitState
        {
            set
            {
                if (value == m_Car2AssistPoweredUnitState)
                {
                    return;
                }

                m_Car2AssistPoweredUnitState = value;
                RaisePropertyChanged(() => Car2AssistPoweredUnitState);
            }
            get { return m_Car2AssistPoweredUnitState; }
        }

        public VoltageChargerState Car4AssistPoweredUnit1State
        {
            set
            {
                if (value == m_Car4AssistPoweredUnit1State)
                {
                    return;
                }

                m_Car4AssistPoweredUnit1State = value;
                RaisePropertyChanged(() => Car4AssistPoweredUnit1State);
            }
            get { return m_Car4AssistPoweredUnit1State; }
        }

        public VoltageChargerState Car4AssistPoweredUnit2State
        {
            set
            {
                if (value == m_Car4AssistPoweredUnit2State)
                {
                    return;
                }

                m_Car4AssistPoweredUnit2State = value;
                RaisePropertyChanged(() => Car4AssistPoweredUnit2State);
            }
            get { return m_Car4AssistPoweredUnit2State; }
        }
        public VoltageChargerState Car5AssistPoweredUnit1State
        {
            set
            {
                if (value == m_Car5AssistPoweredUnit1State)
                {
                    return;
                }

                m_Car5AssistPoweredUnit1State = value;
                RaisePropertyChanged(() => Car5AssistPoweredUnit1State);
            }
            get { return m_Car5AssistPoweredUnit1State; }
        }
        public VoltageChargerState Car5AssistPoweredUnit2State
        {
            set
            {
                if (value == m_Car5AssistPoweredUnit2State)
                {
                    return;
                }

                m_Car5AssistPoweredUnit2State = value;
                RaisePropertyChanged(() => Car5AssistPoweredUnit2State);
            }
            get { return m_Car5AssistPoweredUnit2State; }
        }
        public VoltageChargerState Car7AssistPoweredUnitState
        {
            set
            {
                if (value == m_Car7AssistPoweredUnitState)
                {
                    return;
                }

                m_Car7AssistPoweredUnitState = value;
                RaisePropertyChanged(() => Car7AssistPoweredUnitState);
            }
            get { return m_Car7AssistPoweredUnitState; }
        }
        public bool AllIsSelect
        {
            get { return m_AllIsSelect; }
            set
            {
                if (value == m_AllIsSelect)
                {
                    return;
                }

                m_AllIsSelect = value;
                RaisePropertyChanged(() => AllIsSelect);
            }
        }
         public bool Car2PantographIsSelect
        {
            get { return m_Car2PantographIsSelect; }
            set
            {
                if (value == m_Car2PantographIsSelect)
                {
                    return;
                }

                m_Car2PantographIsSelect = value;
                RaisePropertyChanged(() => Car2PantographIsSelect);
            }
        }
         public bool Car7PantographIsSelect
        {
            get { return m_Car7PantographIsSelect; }
            set
            {
                if (value == m_Car7PantographIsSelect)
                {
                    return;
                }

                m_Car7PantographIsSelect = value;
                RaisePropertyChanged(() => Car7PantographIsSelect);
            }
        }
         public bool Car2MainBreakIsSelect
        {
            get { return m_Car2MainBreakIsSelect; }
            set
            {
                if (value == m_Car2MainBreakIsSelect)
                {
                    return;
                }

                m_Car2MainBreakIsSelect = value;
                RaisePropertyChanged(() => Car2MainBreakIsSelect);
            }
        }
         public bool Car7MainBreakIsSelect
        {
            get { return m_Car7MainBreakIsSelect; }
            set
            {
                if (value == m_Car7MainBreakIsSelect)
                {
                    return;
                }

                m_Car7MainBreakIsSelect = value;
                RaisePropertyChanged(() => Car7MainBreakIsSelect);
            }
        }
         public bool Car2RoofIsolationIsSelect
        {
            get { return m_Car2RoofIsolationIsSelect; }
            set
            {
                if (value == m_Car2RoofIsolationIsSelect)
                {
                    return;
                }

                m_Car2RoofIsolationIsSelect = value;
                RaisePropertyChanged(() => Car2RoofIsolationIsSelect);
            }
        }
         public bool Car7RoofIsolationIsSelect
         {
             get { return m_Car7RoofIsolationIsSelect; }
             set
             {
                 if (value == m_Car7RoofIsolationIsSelect)
                 {
                     return;
                 }

                 m_Car7RoofIsolationIsSelect = value;
                 RaisePropertyChanged(() => Car7RoofIsolationIsSelect);
             }
         }
         public bool Car1TractionConverterIsSelect
        {
            get { return m_Car1TractionConverterIsSelect; }
            set
            {
                if (value == m_Car1TractionConverterIsSelect)
                {
                    return;
                }

                m_Car1TractionConverterIsSelect = value;
                RaisePropertyChanged(() => Car1TractionConverterIsSelect);
            }
        }
         public bool Car3TractionConverterIsSelect
        {
            get { return m_Car3TractionConverterIsSelect; }
            set
            {
                if (value == m_Car3TractionConverterIsSelect)
                {
                    return;
                }

                m_Car3TractionConverterIsSelect = value;
                RaisePropertyChanged(() => Car3TractionConverterIsSelect);
            }
        }
         public bool Car6TractionConverterIsSelect
        {
            get { return m_Car6TractionConverterIsSelect; }
            set
            {
                if (value == m_Car6TractionConverterIsSelect)
                {
                    return;
                }

                m_Car6TractionConverterIsSelect = value;
                RaisePropertyChanged(() => Car6TractionConverterIsSelect);
            }
        }
         public bool Car8TractionConverterIsSelect
        {
            get { return m_Car8TractionConverterIsSelect; }
            set
            {
                if (value == m_Car8TractionConverterIsSelect)
                {
                    return;
                }

                m_Car8TractionConverterIsSelect = value;
                RaisePropertyChanged(() => Car8TractionConverterIsSelect);
            }
        }
         private void IsSelect()
         {
             if (SelectNum == 0)
             {
                 AllIsSelect = true;
             }
             else
             {
                 AllIsSelect = false;
             }
             if (SelectNum == 1)
             {
                 Car7PantographIsSelect = true;
             }
             else
             {
                 Car7PantographIsSelect = false;
             }
             if (SelectNum == 2)
             {
                 Car2PantographIsSelect = true;
             }
             else
             {
                 Car2PantographIsSelect = false;
             }
             if (SelectNum == 3)
             {
                 Car7MainBreakIsSelect = true;
             }
             else
             {
                 Car7MainBreakIsSelect = false;
             }
             if (SelectNum == 4)
             {
                 Car2MainBreakIsSelect = true;
             }
             else
             {
                 Car2MainBreakIsSelect = false;
             }
             if (SelectNum == 5)
             {
                 Car7RoofIsolationIsSelect = true;
             }
             else
             {
                 Car7RoofIsolationIsSelect = false;
             }
             if (SelectNum == 6)
             {
                 Car2RoofIsolationIsSelect = true;
             }
             else
             {
                 Car2RoofIsolationIsSelect = false;
             }
             if (SelectNum == 7)
             {
                 Car8TractionConverterIsSelect = true;
             }
             else
             {
                 Car8TractionConverterIsSelect = false;
             }
             if (SelectNum == 8)
             {
                 Car6TractionConverterIsSelect = true;
             }
             else
             {
                 Car6TractionConverterIsSelect = false;
             }
             if (SelectNum == 9)
             {
                 Car3TractionConverterIsSelect = true;
             }
             else
             {
                 Car3TractionConverterIsSelect = false;
             }
             if (SelectNum == 10)
             {
                 Car1TractionConverterIsSelect = true;
             }
             else
             {
                 Car1TractionConverterIsSelect = false;
             }
             
         }
    }
}
