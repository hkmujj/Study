using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using CommonUtil.Util;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.Controller;
using Subway.ShenZhenLine11.Enum;
using Subway.ShenZhenLine11.Extention;
using Subway.ShenZhenLine11.Views.CommonViews;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export]
    public class HelpViewModel : SubViewModelBase
    {
        private ObservableCollection<HelpUnit> m_PantographTwo;
        private ObservableCollection<HelpUnit> m_PantographOne;
        private ObservableCollection<HelpUnit> m_HighSpeed;
        private ObservableCollection<HelpUnit> m_AirPump;
        private ObservableCollection<HelpUnit> m_Smoke;
        private ObservableCollection<HelpUnit> m_Traction;
        private ObservableCollection<HelpUnit> m_BrakeTwo;
        private ObservableCollection<HelpUnit> m_BrakeOne;
        private ObservableCollection<HelpUnit> m_EmergncyTalk;
        private ObservableCollection<HelpUnit> m_Door;
        private ObservableCollection<HelpUnit> m_AssistPowerDC;
        private ObservableCollection<HelpUnit> m_AssistPowerAC;
        private ObservableCollection<HelpUnit> m_AirConditionTwo;
        private ObservableCollection<HelpUnit> m_AirConditionOne;
        private ObservableCollection<HelpUnit> m_EscapeDoor;
        private ObservableCollection<HelpUnit> m_DriveDoor;
        private ObservableCollection<HelpUnit> m_AirportDoor;
        public static HelpViewModel Instance { get; private set; }
        [ImportingConstructor]
        public HelpViewModel(HelpViewContrller contrller)
        {
            Contrller = contrller;
            InitModelValue();
            Instance = this;
        }

        private void InitModelValue()
        {
            AssistPowerAC = new ObservableCollection<HelpUnit>(typeof(AssistPowerAC).GetUnits<AssistPowerAC>());
            AssistPowerDC = new ObservableCollection<HelpUnit>(typeof(AssistPowerDC).GetUnits<AssistPowerDC>());
            Door = new ObservableCollection<HelpUnit>(typeof(Door).GetUnits<Door>());
            EmergncyTalk = new ObservableCollection<HelpUnit>(typeof(EmergncyTalk).GetUnits<EmergncyTalk>());
            Traction = new ObservableCollection<HelpUnit>(typeof(Traction).GetUnits<Traction>());
            Smoke = new ObservableCollection<HelpUnit>(typeof(Smoke).GetUnits<Smoke>());
            AirPump = new ObservableCollection<HelpUnit>(typeof(AirPump).GetUnits<AirPump>());
            HighSpeed = new ObservableCollection<HelpUnit>(typeof(HighSpeed).GetUnits<HighSpeed>());
            EscapeDoor = new ObservableCollection<HelpUnit>(typeof(EscapeDoorState).GetUnits<EscapeDoorState>());
            DriveDoor = new ObservableCollection<HelpUnit>(typeof(DriveDoorState).GetUnits<DriveDoorState>());
            AirportDoor = new ObservableCollection<HelpUnit>(typeof(AirportDoorState).GetUnits<AirportDoorState>());
            var tmp = new List<AirCondition>()
            {
                AirCondition.Falut,
                AirCondition.Warn,
                AirCondition.EmergencyAir,
                AirCondition.Air,
            };
            AirConditionOne = new ObservableCollection<HelpUnit>(tmp.GetUnit());
            tmp = new List<AirCondition>()
            {
                AirCondition.Limit,
                AirCondition.Run,
                AirCondition.Off
            };
            AirConditionTwo = new ObservableCollection<HelpUnit>(tmp.GetUnit());
            var tmp1 = new List<Brake>()
            {
                Brake.Park,
                Brake.SeverityFault,
                Brake.Cut,
                Brake.BarkeRemit
            };
            BrakeOne = new ObservableCollection<HelpUnit>(tmp1.GetUnit());
            tmp1 = new List<Brake>()
            {
                Brake.ParkRemit,
                Brake.MediumFaulr,
                Brake.BrakeInfliction
            };
            BrakeTwo = new ObservableCollection<HelpUnit>(tmp1.GetUnit());
            var tmp3 = new List<Pantograph>()
            {
                Pantograph.ConnectPower,
                Pantograph.ConnectUnPower
            };
            PantographOne = new ObservableCollection<HelpUnit>(tmp3.GetUnit());
            tmp3 = new List<Pantograph>()
            {
                Pantograph.PantographSeparate,
                Pantograph.PantographUpFault,
                Pantograph.PantographDownFault,
                Pantograph.PantographUp,
                Pantograph.PantographNetPressure,
                Pantograph.PantographDown
            };
            PantographTwo = new ObservableCollection<HelpUnit>(tmp3.GetUnit());
        }
        public HelpViewContrller Contrller { get; private set; }
        public ObservableCollection<HelpUnit> AirConditionOne
        {
            get { return m_AirConditionOne; }
            set
            {
                if (Equals(value, m_AirConditionOne))
                {
                    return;
                }
                m_AirConditionOne = value;
                RaisePropertyChanged(() => AirConditionOne);
            }
        }

        public ObservableCollection<HelpUnit> AirConditionTwo
        {
            get { return m_AirConditionTwo; }
            set
            {
                if (Equals(value, m_AirConditionTwo))
                {
                    return;
                }
                m_AirConditionTwo = value;
                RaisePropertyChanged(() => AirConditionTwo);
            }
        }

        public ObservableCollection<HelpUnit> AssistPowerAC
        {
            get { return m_AssistPowerAC; }
            set
            {
                if (Equals(value, m_AssistPowerAC))
                {
                    return;
                }
                m_AssistPowerAC = value;
                RaisePropertyChanged(() => AssistPowerAC);
            }
        }

        public ObservableCollection<HelpUnit> AssistPowerDC
        {
            get { return m_AssistPowerDC; }
            set
            {
                if (Equals(value, m_AssistPowerDC))
                {
                    return;
                }
                m_AssistPowerDC = value;
                RaisePropertyChanged(() => AssistPowerDC);
            }
        }

        public ObservableCollection<HelpUnit> Door
        {
            get { return m_Door; }
            set
            {
                if (Equals(value, m_Door))
                {
                    return;
                }
                m_Door = value;
                RaisePropertyChanged(() => Door);
            }
        }

        public ObservableCollection<HelpUnit> EmergncyTalk
        {
            get { return m_EmergncyTalk; }
            set
            {
                if (Equals(value, m_EmergncyTalk))
                {
                    return;
                }
                m_EmergncyTalk = value;
                RaisePropertyChanged(() => EmergncyTalk);
            }
        }

        public ObservableCollection<HelpUnit> BrakeOne
        {
            get { return m_BrakeOne; }
            set
            {
                if (Equals(value, m_BrakeOne))
                {
                    return;
                }
                m_BrakeOne = value;
                RaisePropertyChanged(() => BrakeOne);
            }
        }

        public ObservableCollection<HelpUnit> BrakeTwo
        {
            get { return m_BrakeTwo; }
            set
            {
                if (Equals(value, m_BrakeTwo))
                {
                    return;
                }
                m_BrakeTwo = value;
                RaisePropertyChanged(() => BrakeTwo);
            }
        }

        public ObservableCollection<HelpUnit> Traction
        {
            get { return m_Traction; }
            set
            {
                if (Equals(value, m_Traction))
                {
                    return;
                }
                m_Traction = value;
                RaisePropertyChanged(() => Traction);
            }
        }

        public ObservableCollection<HelpUnit> Smoke
        {
            get { return m_Smoke; }
            set
            {
                if (Equals(value, m_Smoke))
                {
                    return;
                }
                m_Smoke = value;
                RaisePropertyChanged(() => Smoke);
            }
        }

        public ObservableCollection<HelpUnit> AirPump
        {
            get { return m_AirPump; }
            set
            {
                if (Equals(value, m_AirPump))
                {
                    return;
                }
                m_AirPump = value;
                RaisePropertyChanged(() => AirPump);
            }
        }

        public ObservableCollection<HelpUnit> HighSpeed
        {
            get { return m_HighSpeed; }
            set
            {
                if (Equals(value, m_HighSpeed))
                {
                    return;
                }
                m_HighSpeed = value;
                RaisePropertyChanged(() => HighSpeed);
            }
        }

        public ObservableCollection<HelpUnit> PantographOne
        {
            get { return m_PantographOne; }
            set
            {
                if (Equals(value, m_PantographOne))
                {
                    return;
                }
                m_PantographOne = value;
                RaisePropertyChanged(() => PantographOne);
            }
        }

        public ObservableCollection<HelpUnit> PantographTwo
        {
            get { return m_PantographTwo; }
            set
            {
                if (Equals(value, m_PantographTwo))
                {
                    return;
                }
                m_PantographTwo = value;
                RaisePropertyChanged(() => PantographTwo);
            }
        }

        public ObservableCollection<HelpUnit> EscapeDoor
        {
            get { return m_EscapeDoor; }
            set
            {
                if (Equals(value, m_EscapeDoor))
                {
                    return;
                }
                m_EscapeDoor = value;
                RaisePropertyChanged(() => EscapeDoor);
            }
        }

        public ObservableCollection<HelpUnit> DriveDoor
        {
            get { return m_DriveDoor; }
            set
            {
                if (Equals(value, m_DriveDoor))
                {
                    return;
                }
                m_DriveDoor = value;
                RaisePropertyChanged(() => DriveDoor);
            }
        }

        public ObservableCollection<HelpUnit> AirportDoor
        {
            get { return m_AirportDoor; }
            set
            {
                if (Equals(value, m_AirportDoor))
                {
                    return;
                }
                m_AirportDoor = value;
                RaisePropertyChanged(() => AirportDoor);
            }
        }
    }
}