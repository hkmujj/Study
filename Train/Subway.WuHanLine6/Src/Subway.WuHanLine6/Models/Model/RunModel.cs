using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Subway.WuHanLine6.Models.States;
using Subway.WuHanLine6.Models.Units;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 运行界面Model
    /// </summary>
    [Export]
    public class RunModel : ModelBase
    {
        private WorkPowerState m_WorkPowerStateTwo;
        private WorkPowerState m_WorkPowerStateOne;
        private HighSpeedState m_HighSpeedStateFour;
        private HighSpeedState m_HighSpeedStateThree;
        private HighSpeedState m_HighSpeedStateTwo;
        private HighSpeedState m_HighSpeedStateOne;
        private TractionState m_TractionStateF005;
        private TractionState m_TractionStateF004;
        private TractionState m_TractionStateF003;
        private TractionState m_TractionStateF002;
        private AssistACState m_AssistACStateF001One;
        private AssistACState m_AssistACStateF001Two;
        private AssistACState m_AssistACStateF006One;
        private AssistACState m_AssistACStateF006Two;
        private bool m_ExtentionPower;
        private ObservableCollection<BrakeUnit> m_BrakeUnits;
        private AirPumpState m_AirPumpStateF003;
        private AirPumpState m_AirPumpStateF004;
        private ObservableCollection<AirConditionUnit> m_AirConditionUnits;
        private double m_Traction;
        private double m_Brake;
        private ObservableCollection<DoorUnit> m_AllDoorStates;
        private IEnumerable<RunModelUnit> m_CurrentModel;
        private Visibility m_BypassVisibility;
        private Visibility m_EmergencyTalkVisibility;
        private Visibility m_SmokeVisibility;
        private DriveDoorState m_Drive2Door2;
        private DriveDoorState m_Drive2Door1;
        private DriveDoorState m_Drive1Door2;
        private DriveDoorState m_Drive1Door1;

        /// <summary>
        ///
        /// </summary>
        public RunModel()
        {
            BrakeUnits = new ObservableCollection<BrakeUnit>(GlobalParams.Instance.BrakeUnits);
            AirConditionUnits = new ObservableCollection<AirConditionUnit>(GlobalParams.Instance.AirConditionUnits);
            AllDoorStates = new ObservableCollection<DoorUnit>(GlobalParams.Instance.DoorUnits);
            SmokeVisibility = Visibility.Hidden;
            EmergencyTalkVisibility = Visibility.Hidden;
            BypassVisibility = Visibility.Hidden;
        }

        /// <summary>
        /// F002高速断路器状态
        /// </summary>
        public HighSpeedState HighSpeedStateOne
        {
            get { return m_HighSpeedStateOne; }
            set
            {
                if (value == m_HighSpeedStateOne)
                {
                    return;
                }
                m_HighSpeedStateOne = value;
                RaisePropertyChanged(() => HighSpeedStateOne);
            }
        }

        /// <summary>
        /// F003高速断路器状态
        /// </summary>
        public HighSpeedState HighSpeedStateTwo
        {
            get { return m_HighSpeedStateTwo; }
            set
            {
                if (value == m_HighSpeedStateTwo)
                {
                    return;
                }
                m_HighSpeedStateTwo = value;
                RaisePropertyChanged(() => HighSpeedStateTwo);
            }
        }

        /// <summary>
        /// F004高速断路器状态
        /// </summary>
        public HighSpeedState HighSpeedStateThree
        {
            get { return m_HighSpeedStateThree; }
            set
            {
                if (value == m_HighSpeedStateThree)
                {
                    return;
                }
                m_HighSpeedStateThree = value;
                RaisePropertyChanged(() => HighSpeedStateThree);
            }
        }

        /// <summary>
        /// F005高速断路器状态
        /// </summary>
        public HighSpeedState HighSpeedStateFour
        {
            get { return m_HighSpeedStateFour; }
            set
            {
                if (value == m_HighSpeedStateFour)
                {
                    return;
                }
                m_HighSpeedStateFour = value;
                RaisePropertyChanged(() => HighSpeedStateFour);
            }
        }

        /// <summary>
        /// F002车间电源状态
        /// </summary>
        public WorkPowerState WorkPowerStateOne
        {
            get { return m_WorkPowerStateOne; }
            set
            {
                if (value == m_WorkPowerStateOne)
                {
                    return;
                }
                m_WorkPowerStateOne = value;
                RaisePropertyChanged(() => WorkPowerStateOne);
            }
        }

        /// <summary>
        /// F005车间电源状态
        /// </summary>
        public WorkPowerState WorkPowerStateTwo
        {
            get { return m_WorkPowerStateTwo; }
            set
            {
                if (value == m_WorkPowerStateTwo)
                {
                    return;
                }
                m_WorkPowerStateTwo = value;
                RaisePropertyChanged(() => WorkPowerStateTwo);
            }
        }

        /// <summary>
        /// F002车 牵引状态
        /// </summary>
        public TractionState TractionStateF002
        {
            get { return m_TractionStateF002; }
            set
            {
                if (value == m_TractionStateF002)
                {
                    return;
                }
                m_TractionStateF002 = value;
                RaisePropertyChanged(() => TractionStateF002);
            }
        }

        /// <summary>
        /// F003车 牵引状态
        /// </summary>
        public TractionState TractionStateF003
        {
            get { return m_TractionStateF003; }
            set
            {
                if (value == m_TractionStateF003)
                {
                    return;
                }
                m_TractionStateF003 = value;
                RaisePropertyChanged(() => TractionStateF003);
            }
        }

        /// <summary>
        /// F004车 牵引状态
        /// </summary>
        public TractionState TractionStateF004
        {
            get { return m_TractionStateF004; }
            set
            {
                if (value == m_TractionStateF004)
                {
                    return;
                }
                m_TractionStateF004 = value;
                RaisePropertyChanged(() => TractionStateF004);
            }
        }

        /// <summary>
        /// F005车 牵引状态
        /// </summary>
        public TractionState TractionStateF005
        {
            get { return m_TractionStateF005; }
            set
            {
                if (value == m_TractionStateF005)
                {
                    return;
                }
                m_TractionStateF005 = value;
                RaisePropertyChanged(() => TractionStateF005);
            }
        }

        /// <summary>
        /// 辅助系统 F001 AC辅助电源1
        /// </summary>
        public AssistACState AssistACStateF001One
        {
            get { return m_AssistACStateF001One; }
            set
            {
                if (value == m_AssistACStateF001One)
                {
                    return;
                }
                m_AssistACStateF001One = value;
                RaisePropertyChanged(() => AssistACStateF001One);
            }
        }

        /// <summary>
        /// 辅助系统 F001 AC辅助电源2
        /// </summary>
        public AssistACState AssistACStateF001Two
        {
            get { return m_AssistACStateF001Two; }
            set
            {
                if (value == m_AssistACStateF001Two)
                {
                    return;
                }
                m_AssistACStateF001Two = value;
                RaisePropertyChanged(() => AssistACStateF001Two);
            }
        }

        /// <summary>
        /// 辅助系统 F006 AC辅助电源1
        /// </summary>
        public AssistACState AssistACStateF006One
        {
            get { return m_AssistACStateF006One; }
            set
            {
                if (value == m_AssistACStateF006One)
                {
                    return;
                }
                m_AssistACStateF006One = value;
                RaisePropertyChanged(() => AssistACStateF006One);
            }
        }

        /// <summary>
        /// 辅助系统 F006 AC辅助电源2
        /// </summary>
        public AssistACState AssistACStateF006Two
        {
            get { return m_AssistACStateF006Two; }
            set
            {
                if (value == m_AssistACStateF006Two)
                {
                    return;
                }
                m_AssistACStateF006Two = value;
                RaisePropertyChanged(() => AssistACStateF006Two);
            }
        }

        /// <summary>
        /// 扩展供电
        /// </summary>
        public bool ExtentionPower
        {
            get { return m_ExtentionPower; }
            set
            {
                if (value == m_ExtentionPower)
                {
                    return;
                }
                m_ExtentionPower = value;
                RaisePropertyChanged(() => ExtentionPower);
            }
        }

        /// <summary>
        /// 制动
        /// </summary>
        public ObservableCollection<BrakeUnit> BrakeUnits
        {
            get { return m_BrakeUnits; }
            set
            {
                if (Equals(value, m_BrakeUnits))
                {
                    return;
                }
                m_BrakeUnits = value;
                RaisePropertyChanged(() => BrakeUnits);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public BrakeUnit BrakeUnitF0011 { get { return BrakeUnits.FirstOrDefault(f => f.Car == 1 && f.Location == 1); } }

        /// <summary>
        ///
        /// </summary>
        public BrakeUnit BrakeUnitF0012 { get { return BrakeUnits.FirstOrDefault(f => f.Car == 1 && f.Location == 2); } }

        /// <summary>
        ///
        /// </summary>
        public BrakeUnit BrakeUnitF0021 { get { return BrakeUnits.FirstOrDefault(f => f.Car == 2 && f.Location == 1); } }

        /// <summary>
        ///
        /// </summary>
        public BrakeUnit BrakeUnitF0022 { get { return BrakeUnits.FirstOrDefault(f => f.Car == 2 && f.Location == 2); } }

        /// <summary>
        ///
        /// </summary>
        public BrakeUnit BrakeUnitF0031 { get { return BrakeUnits.FirstOrDefault(f => f.Car == 3 && f.Location == 1); } }

        /// <summary>
        ///
        /// </summary>
        public BrakeUnit BrakeUnitF0032 { get { return BrakeUnits.FirstOrDefault(f => f.Car == 3 && f.Location == 2); } }

        /// <summary>
        ///
        /// </summary>
        public BrakeUnit BrakeUnitF0041 { get { return BrakeUnits.FirstOrDefault(f => f.Car == 4 && f.Location == 1); } }

        /// <summary>
        ///
        /// </summary>
        public BrakeUnit BrakeUnitF0042 { get { return BrakeUnits.FirstOrDefault(f => f.Car == 4 && f.Location == 2); } }

        /// <summary>
        ///
        /// </summary>
        public BrakeUnit BrakeUnitF0051 { get { return BrakeUnits.FirstOrDefault(f => f.Car == 5 && f.Location == 1); } }

        /// <summary>
        ///
        /// </summary>
        public BrakeUnit BrakeUnitF0052 { get { return BrakeUnits.FirstOrDefault(f => f.Car == 5 && f.Location == 2); } }

        /// <summary>
        ///
        /// </summary>
        public BrakeUnit BrakeUnitF0061 { get { return BrakeUnits.FirstOrDefault(f => f.Car == 6 && f.Location == 1); } }

        /// <summary>
        ///
        /// </summary>
        public BrakeUnit BrakeUnitF0062 { get { return BrakeUnits.FirstOrDefault(f => f.Car == 6 && f.Location == 2); } }

        /// <summary>
        /// F003空压机
        /// </summary>
        public AirPumpState AirPumpStateF003
        {
            get { return m_AirPumpStateF003; }
            set
            {
                if (value == m_AirPumpStateF003)
                {
                    return;
                }
                m_AirPumpStateF003 = value;
                RaisePropertyChanged(() => AirPumpStateF003);
            }
        }

        /// <summary>
        /// F004空压机
        /// </summary>
        public AirPumpState AirPumpStateF004
        {
            get { return m_AirPumpStateF004; }
            set
            {
                if (value == m_AirPumpStateF004)
                {
                    return;
                }
                m_AirPumpStateF004 = value;
                RaisePropertyChanged(() => AirPumpStateF004);
            }
        }

        /// <summary>
        /// 所有空调
        /// </summary>
        public ObservableCollection<AirConditionUnit> AirConditionUnits
        {
            get { return m_AirConditionUnits; }
            set
            {
                if (Equals(value, m_AirConditionUnits))
                {
                    return;
                }
                m_AirConditionUnits = value;
                RaisePropertyChanged(() => AirConditionUnits);
            }
        }

        /// <summary>
        ///
        /// </summary>
        public AirConditionUnit AirConditionUnitF0011 { get { return AirConditionUnits.FirstOrDefault(f => f.Car == 1 && f.Location == 1); } }

        /// <summary>
        ///
        /// </summary>
        public AirConditionUnit AirConditionUnitF0012 { get { return AirConditionUnits.FirstOrDefault(f => f.Car == 1 && f.Location == 2); } }

        /// <summary>
        ///
        /// </summary>
        public AirConditionUnit AirConditionUnitF0021 { get { return AirConditionUnits.FirstOrDefault(f => f.Car == 2 && f.Location == 1); } }

        /// <summary>
        ///
        /// </summary>
        public AirConditionUnit AirConditionUnitF0022 { get { return AirConditionUnits.FirstOrDefault(f => f.Car == 2 && f.Location == 2); } }

        /// <summary>
        ///
        /// </summary>
        public AirConditionUnit AirConditionUnitF0031 { get { return AirConditionUnits.FirstOrDefault(f => f.Car == 3 && f.Location == 1); } }

        /// <summary>
        ///
        /// </summary>
        public AirConditionUnit AirConditionUnitF0032 { get { return AirConditionUnits.FirstOrDefault(f => f.Car == 3 && f.Location == 2); } }

        /// <summary>
        ///
        /// </summary>
        public AirConditionUnit AirConditionUnitF0041 { get { return AirConditionUnits.FirstOrDefault(f => f.Car == 4 && f.Location == 1); } }

        /// <summary>
        ///
        /// </summary>
        public AirConditionUnit AirConditionUnitF0042 { get { return AirConditionUnits.FirstOrDefault(f => f.Car == 4 && f.Location == 2); } }

        /// <summary>
        ///
        /// </summary>
        public AirConditionUnit AirConditionUnitF0051 { get { return AirConditionUnits.FirstOrDefault(f => f.Car == 5 && f.Location == 1); } }

        /// <summary>
        ///
        /// </summary>
        public AirConditionUnit AirConditionUnitF0052 { get { return AirConditionUnits.FirstOrDefault(f => f.Car == 5 && f.Location == 2); } }

        /// <summary>
        ///
        /// </summary>
        public AirConditionUnit AirConditionUnitF0061 { get { return AirConditionUnits.FirstOrDefault(f => f.Car == 6 && f.Location == 1); } }

        /// <summary>
        ///
        /// </summary>
        public AirConditionUnit AirConditionUnitF0062 { get { return AirConditionUnits.FirstOrDefault(f => f.Car == 6 && f.Location == 2); } }

        /// <summary>
        /// 牵引百分比
        /// </summary>
        public double Traction
        {
            get { return m_Traction; }
            set
            {
                if (value.Equals(m_Traction))
                {
                    return;
                }
                m_Traction = value;
                RaisePropertyChanged(() => Traction);
            }
        }

        /// <summary>
        /// 制动百分比
        /// </summary>
        public double Brake
        {
            get { return m_Brake; }
            set
            {
                if (value.Equals(m_Brake))
                {
                    return;
                }
                m_Brake = value;
                RaisePropertyChanged(() => Brake);
            }
        }

        /// <summary>
        /// 所有门状态
        /// </summary>
        public ObservableCollection<DoorUnit> AllDoorStates
        {
            get { return m_AllDoorStates; }
            set
            {
                if (Equals(value, m_AllDoorStates)) return;
                m_AllDoorStates = value;
                RaisePropertyChanged(() => AllDoorStates);
            }
        }

        /// <summary>
        /// F001车上方门状态
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnitF001Top
        {
            get
            {
                return AllDoorStates.Where(w => w.Car == 1 && w.Location % 2 == 0).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        /// F001车下方门状态
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnitF001Bottom
        {
            get
            {
                return AllDoorStates.Where(w => w.Car == 1 && w.Location % 2 != 0).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        /// F002车上方门状态
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnitF002Top
        {
            get
            {
                return AllDoorStates.Where(w => w.Car == 2 && w.Location % 2 == 0).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        /// F002车下方门状态
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnitF002Bottom
        {
            get
            {
                return AllDoorStates.Where(w => w.Car == 2 && w.Location % 2 != 0).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        /// F003车上方门状态
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnitF003Top
        {
            get
            {
                return AllDoorStates.Where(w => w.Car == 3 && w.Location % 2 == 0).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        /// F003车下方门状态
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnitF003Bottom
        {
            get
            {
                return AllDoorStates.Where(w => w.Car == 3 && w.Location % 2 != 0).OrderBy(o => o.Location);
            }
        }

        /// <summary>
        /// F004车上方门状态
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnitF004Top
        {
            get
            {
                return AllDoorStates.Where(w => w.Car == 4 && w.Location % 2 != 0).OrderByDescending(o => o.Location);
            }
        }

        /// <summary>
        /// F004车下方门状态
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnitF004Bottom
        {
            get
            {
                return AllDoorStates.Where(w => w.Car == 4 && w.Location % 2 == 0).OrderByDescending(o => o.Location);
            }
        }

        /// <summary>
        /// F005车上方门状态
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnitF005Top
        {
            get
            {
                return AllDoorStates.Where(w => w.Car == 5 && w.Location % 2 != 0).OrderByDescending(o => o.Location);
            }
        }

        /// <summary>
        /// F005车下方门状态
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnitF005Bottom
        {
            get
            {
                return AllDoorStates.Where(w => w.Car == 5 && w.Location % 2 == 0).OrderByDescending(o => o.Location);
            }
        }

        /// <summary>
        /// F006车上方门状态
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnitF006Top
        {
            get
            {
                return AllDoorStates.Where(w => w.Car == 6 && w.Location % 2 != 0).OrderByDescending(o => o.Location);
            }
        }

        /// <summary>
        /// F006车下方门状态
        /// </summary>
        public IEnumerable<DoorUnit> DoorUnitF006Bottom
        {
            get
            {
                return AllDoorStates.Where(w => w.Car == 6 && w.Location % 2 == 0).OrderByDescending(o => o.Location);
            }
        }

        /// <summary>
        /// CurrentModel
        /// </summary>
        public IEnumerable<RunModelUnit> CurrentModel
        {
            get { return m_CurrentModel; }
            set
            {
                if (Equals(value, m_CurrentModel))
                {
                    return;
                }
                m_CurrentModel = value;
                RaisePropertyChanged(() => CurrentModel);
            }
        }

        /// <summary>
        /// 烟火报警显示
        /// </summary>
        public Visibility SmokeVisibility
        {
            get { return m_SmokeVisibility; }
            set
            {
                if (value == m_SmokeVisibility)
                {
                    return;
                }
                m_SmokeVisibility = value;
                RaisePropertyChanged(() => SmokeVisibility);
            }
        }

        /// <summary>
        /// 乘客报警显示
        /// </summary>
        public Visibility EmergencyTalkVisibility
        {
            get { return m_EmergencyTalkVisibility; }
            set
            {
                if (value == m_EmergencyTalkVisibility)
                {
                    return;
                }
                m_EmergencyTalkVisibility = value;
                RaisePropertyChanged(() => EmergencyTalkVisibility);
            }
        }

        /// <summary>
        /// 旁路报警显示
        /// </summary>
        public Visibility BypassVisibility
        {
            get { return m_BypassVisibility; }
            set
            {
                if (value == m_BypassVisibility)
                {
                    return;
                }
                m_BypassVisibility = value;
                RaisePropertyChanged(() => BypassVisibility);
            }
        }

        /// <summary>
        /// 司机室1门1状态
        /// </summary>
        public DriveDoorState Drive1Door1
        {
            get { return m_Drive1Door1; }
            set
            {
                if (value == m_Drive1Door1)
                {
                    return;
                }
                m_Drive1Door1 = value;
                RaisePropertyChanged(() => Drive1Door1);
            }
        }

        /// <summary>
        /// 司机室1门2状态
        /// </summary>
        public DriveDoorState Drive1Door2
        {
            get { return m_Drive1Door2; }
            set
            {
                if (value == m_Drive1Door2)
                {
                    return;
                }
                m_Drive1Door2 = value;
                RaisePropertyChanged(() => Drive1Door2);
            }
        }

        /// <summary>
        /// 司机室2门1状态
        /// </summary>
        public DriveDoorState Drive2Door1
        {
            get { return m_Drive2Door1; }
            set
            {
                if (value == m_Drive2Door1)
                {
                    return;
                }
                m_Drive2Door1 = value;
                RaisePropertyChanged(() => Drive2Door1);
            }
        }

        /// <summary>
        /// 司机室2门2状态
        /// </summary>
        public DriveDoorState Drive2Door2
        {
            get { return m_Drive2Door2; }
            set
            {
                if (value == m_Drive2Door2)
                {
                    return;
                }
                m_Drive2Door2 = value;
                RaisePropertyChanged(() => Drive2Door2);
            }
        }
    }
}