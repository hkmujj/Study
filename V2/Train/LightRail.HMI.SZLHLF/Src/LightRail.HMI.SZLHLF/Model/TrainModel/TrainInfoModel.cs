using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using System.Linq;
using LightRail.HMI.SZLHLF.Model.TrainModel;
using LightRail.HMI.SZLHLF.Model.Units;

namespace LightRail.HMI.SZLHLF.Model
{
    [Export]
    public class TrainInfoModel : ModelBase
    {
        private ObservableCollection<DoorUnit> m_DoorUnits;
        private ObservableCollection<BatteryUnit> m_BatteryUnits;
        private ObservableCollection<BrakeUnit> m_BrakeUnits;
        private ObservableCollection<TractionUnit> m_TractionUnits;

        private float m_TractionPercent;
        private float m_BrakePercent;
        private float m_RemainElectricQuantity1;
        private float m_RemainElectricQuantity2;
        private float m_RemainElectricQuantity3;
        private float m_PowerSourceTemperature1;
        private float m_PowerSourceTemperature2;
        private float m_PowerSourceTemperature3;
        private float m_SpeedLimit;
        private float m_ResidueMileage;
        private AssistPowerSource m_AssistPowerSource;
        private HighSpeedBreaker m_HighSpeedBreaker;
        private Pantograph m_Pantograph;
        private BatteryCharger m_BatteryCharger;
        private DirectionArrow m_DirectionArrow;
        private Drivers m_MC1Drivers;
        private Drivers m_MC2Drivers;
        private TurnLight m_TurnLeftLight;
        private TurnLight m_TurnRightLight;
        private RunModel m_RunModel;
        private WorkState m_WorkState;
        private bool m_Fire1;
        private bool m_Fire2;
        private bool m_Fire3;
        private bool m_Fire4;
        private bool m_BogieApron;

        public TrainInfoModel()
        {
            DoorUnits = new ObservableCollection<DoorUnit>(GlobalParam.Instance.DoorUnits);
            BatteryUnits = new ObservableCollection<BatteryUnit>(GlobalParam.Instance.BatteryUnits);
            BrakeUnits = new ObservableCollection<BrakeUnit>(GlobalParam.Instance.BrakeUnits);
            TractionUnits = new ObservableCollection<TractionUnit>(GlobalParam.Instance.TractionUnits);
        }

        /// <summary>
        /// 所有门单元
        /// </summary>
        public ObservableCollection<DoorUnit> DoorUnits
        {
            get { return m_DoorUnits; }
            private set
            {
                if (Equals(value, m_DoorUnits))
                {
                    return;
                }
                m_DoorUnits = value;
                RaisePropertyChanged(() => DoorUnits);
                RaisePropertyChanged(() => Car1Door1);
                RaisePropertyChanged(() => Car1Door2);
                RaisePropertyChanged(() => Car1Door3);
                RaisePropertyChanged(() => Car2Door4);
                RaisePropertyChanged(() => Car2Door5);
                RaisePropertyChanged(() => Car3Door6);
                RaisePropertyChanged(() => Car3Door7);
                RaisePropertyChanged(() => Car4Door8);
                RaisePropertyChanged(() => Car4Door9);
                RaisePropertyChanged(() => Car4Door10);
            }
        }
        /// <summary>
        /// 1车1门
        /// </summary>
        public DoorUnit Car1Door1 { get { return DoorUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 1车2门
        /// </summary>
        public DoorUnit Car1Door2 { get { return DoorUnits.Where(w => w.Car == 1 && w.Location == 2).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 1车3门
        /// </summary>
        public DoorUnit Car1Door3 { get { return DoorUnits.Where(w => w.Car == 1 && w.Location == 3).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2车4门
        /// </summary>
        public DoorUnit Car2Door4 { get { return DoorUnits.Where(w => w.Car == 2 && w.Location == 4).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2车5门
        /// </summary>
        public DoorUnit Car2Door5 { get { return DoorUnits.Where(w => w.Car == 2 && w.Location == 5).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车6门
        /// </summary>
        public DoorUnit Car3Door6 { get { return DoorUnits.Where(w => w.Car == 3 && w.Location == 6).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车7门
        /// </summary>
        public DoorUnit Car3Door7 { get { return DoorUnits.Where(w => w.Car == 3 && w.Location == 7).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4车8门
        /// </summary>
        public DoorUnit Car4Door8 { get { return DoorUnits.Where(w => w.Car == 4 && w.Location == 8).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4车9门
        /// </summary>
        public DoorUnit Car4Door9 { get { return DoorUnits.Where(w => w.Car == 4 && w.Location == 9).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4车10门
        /// </summary>
        public DoorUnit Car4Door10 { get { return DoorUnits.Where(w => w.Car == 4 && w.Location == 10).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 所有电池单元
        /// </summary>
        public ObservableCollection<BatteryUnit> BatteryUnits
        {
            get { return m_BatteryUnits; }
            private set
            {
                if (Equals(value, m_BatteryUnits))
                {
                    return;
                }
                m_BatteryUnits = value;
                RaisePropertyChanged(() => BatteryUnits);
                RaisePropertyChanged(() => Car1BatteryUnit);
                RaisePropertyChanged(() => Car2BatteryUnit);
                RaisePropertyChanged(() => Car4BatteryUnit);

            }
        }

        /// <summary>
        /// 1车
        /// </summary>
        public BatteryUnit Car1BatteryUnit { get { return BatteryUnits.Where(w => w.Car == 1).OrderBy(o => o.Car).FirstOrDefault(); } }

        /// <summary>
        /// 2车
        /// </summary>
        public BatteryUnit Car2BatteryUnit { get { return BatteryUnits.Where(w => w.Car == 2).OrderBy(o => o.Car).FirstOrDefault(); } }

        /// <summary>
        /// 4车
        /// </summary>
        public BatteryUnit Car4BatteryUnit { get { return BatteryUnits.Where(w => w.Car == 4).OrderBy(o => o.Car).FirstOrDefault(); } }

        /// <summary>
        /// 所有制动单元
        /// </summary>
        public ObservableCollection<BrakeUnit> BrakeUnits
        {
            get { return m_BrakeUnits; }
            private set
            {
                if (Equals(value, m_BrakeUnits))
                {
                    return;
                }
                m_BrakeUnits = value;
                RaisePropertyChanged(() => BrakeUnits);
                RaisePropertyChanged(() => Car1Location1BrakeUnit);
                RaisePropertyChanged(() => Car2Location1BrakeUnit);
                RaisePropertyChanged(() => Car3Location1BrakeUnit);
                RaisePropertyChanged(() => Car4Location1BrakeUnit);
            }
        }

        /// <summary>
        /// 1车1位置
        /// </summary>
        public BrakeUnit Car1Location1BrakeUnit { get { return BrakeUnits.Where(w => w.Car == 1 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 2车1位置
        /// </summary>
        public BrakeUnit Car2Location1BrakeUnit { get { return BrakeUnits.Where(w => w.Car == 2 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 3车1位置
        /// </summary>
        public BrakeUnit Car3Location1BrakeUnit { get { return BrakeUnits.Where(w => w.Car == 3 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 4车1位置
        /// </summary>
        public BrakeUnit Car4Location1BrakeUnit { get { return BrakeUnits.Where(w => w.Car == 4 && w.Location == 1).OrderBy(o => o.Location).FirstOrDefault(); } }

        /// <summary>
        /// 所有牵引单元
        /// </summary>
        public ObservableCollection<TractionUnit> TractionUnits
        {
            get { return m_TractionUnits; }
            private set
            {
                if (Equals(value, m_TractionUnits))
                {
                    return;
                }
                m_TractionUnits = value;
                RaisePropertyChanged(() => TractionUnits);
                RaisePropertyChanged(() => Car1TractionUnit);
                RaisePropertyChanged(() => Car3TractionUnit);
                RaisePropertyChanged(() => Car4TractionUnit);

            }
        }

        /// <summary>
        /// 1车
        /// </summary>
        public TractionUnit Car1TractionUnit { get { return TractionUnits.Where(w => w.Car == 1).OrderBy(o => o.Car).FirstOrDefault(); } }

        /// <summary>
        /// 3车
        /// </summary>
        public TractionUnit Car3TractionUnit { get { return TractionUnits.Where(w => w.Car == 3).OrderBy(o => o.Car).FirstOrDefault(); } }

        /// <summary>
        /// 4车
        /// </summary>
        public TractionUnit Car4TractionUnit { get { return TractionUnits.Where(w => w.Car == 4).OrderBy(o => o.Car).FirstOrDefault(); } }

        /// <summary>
        /// 牵引百分比
        /// </summary>
        public float TractionPercent
        {
            get { return m_TractionPercent; }
            set
            {
                if(value.Equals(m_TractionPercent))
                {
                    return;
                }

                m_TractionPercent = value;
                RaisePropertyChanged(() => TractionPercent);
            }
        }

        /// <summary>
        /// 制动百分比
        /// </summary>
        public float BrakePercent
        {
            get { return m_BrakePercent; }
            set
            {
                if(value.Equals(m_BrakePercent))
                {
                    return;
                }

                m_BrakePercent = value;
                RaisePropertyChanged(() => BrakePercent);
            }
        }

        /// <summary>
        /// 剩余电量1
        /// </summary>
        public float RemainElectricQuantity1
        {
            get { return m_RemainElectricQuantity1; }
            set
            {
                if (value.Equals(m_RemainElectricQuantity1))
                {
                    return;
                }

                m_RemainElectricQuantity1 = value;
                RaisePropertyChanged(() => RemainElectricQuantity1);
            }
        }

        /// <summary>
        /// 剩余电量2
        /// </summary>
        public float RemainElectricQuantity2
        {
            get { return m_RemainElectricQuantity2; }
            set
            {
                if (value.Equals(m_RemainElectricQuantity2))
                {
                    return;
                }

                m_RemainElectricQuantity2 = value;
                RaisePropertyChanged(() => RemainElectricQuantity2);
            }
        }

        /// <summary>
        /// 剩余电量3
        /// </summary>
        public float RemainElectricQuantity3
        {
            get { return m_RemainElectricQuantity3; }
            set
            {
                if (value.Equals(m_RemainElectricQuantity3))
                {
                    return;
                }

                m_RemainElectricQuantity3 = value;
                RaisePropertyChanged(() => RemainElectricQuantity3);
            }
        }

        /// <summary>
        /// 电源温度1
        /// </summary>
        public float PowerSourceTemperature1
        {
            get { return m_PowerSourceTemperature1; }
            set
            {
                if (value.Equals(m_PowerSourceTemperature1))
                {
                    return;
                }

                m_PowerSourceTemperature1 = value;
                RaisePropertyChanged(() => PowerSourceTemperature1);
            }
        }

        /// <summary>
        /// 电源温度2
        /// </summary>
        public float PowerSourceTemperature2
        {
            get { return m_PowerSourceTemperature2; }
            set
            {
                if (value.Equals(m_PowerSourceTemperature2))
                {
                    return;
                }

                m_PowerSourceTemperature2 = value;
                RaisePropertyChanged(() => PowerSourceTemperature2);
            }
        }

        /// <summary>
        /// 电源温度3
        /// </summary>
        public float PowerSourceTemperature3
        {
            get { return m_PowerSourceTemperature3; }
            set
            {
                if (value.Equals(m_PowerSourceTemperature3))
                {
                    return;
                }

                m_PowerSourceTemperature3 = value;
                RaisePropertyChanged(() => PowerSourceTemperature3);
            }
        }

        /// <summary>
        /// 限速
        /// </summary>
        public float SpeedLimit
        {
            get { return m_SpeedLimit; }
            set
            {
                if (value.Equals(m_SpeedLimit))
                {
                    return;
                }

                m_SpeedLimit = value;
                RaisePropertyChanged(() => SpeedLimit);
            }
        }

        /// <summary>
        /// 剩余里程数
        /// </summary>
        public float ResidueMileage
        {
            get { return m_ResidueMileage; }
            set
            {
                if (value.Equals(m_ResidueMileage))
                {
                    return;
                }

                m_ResidueMileage = value;
                RaisePropertyChanged(() => ResidueMileage);
            }
        }
     
        public AssistPowerSource AssistPowerSource
        {
            get { return m_AssistPowerSource; }
            set
            {
                if (value == m_AssistPowerSource)
                {
                    return;
                }

                m_AssistPowerSource = value;
                RaisePropertyChanged(() => AssistPowerSource);
            }
        }

        public HighSpeedBreaker HighSpeedBreaker
        {
            get { return m_HighSpeedBreaker; }
            set
            {
                if (value == m_HighSpeedBreaker)
                {
                    return;
                }

                m_HighSpeedBreaker = value;
                RaisePropertyChanged(() => HighSpeedBreaker);
            }
        }

        public Pantograph Pantograph
        {
            get { return m_Pantograph; }
            set
            {
                if (value == m_Pantograph)
                {
                    return;
                }

                m_Pantograph = value;
                RaisePropertyChanged(() => Pantograph);
            }
        }

        public BatteryCharger BatteryCharger
        {
            get { return m_BatteryCharger; }
            set
            {
                if (value == m_BatteryCharger)
                {
                    return;
                }

                m_BatteryCharger = value;
                RaisePropertyChanged(() => BatteryCharger);
            }
        }

        public DirectionArrow DirectionArrow
        {
            get { return m_DirectionArrow; }
            set
            {
                if (value == m_DirectionArrow)
                {
                    return;
                }

                m_DirectionArrow = value;
                RaisePropertyChanged(() => DirectionArrow);
            }
        }

        public Drivers MC1Drivers
        {
            get { return m_MC1Drivers; }
            set
            {
                if (value == m_MC1Drivers)
                {
                    return;
                }

                m_MC1Drivers = value;
                RaisePropertyChanged(() => MC1Drivers);
            }
        }

        public Drivers MC2Drivers
        {
            get { return m_MC2Drivers; }
            set
            {
                if (value == m_MC2Drivers)
                {
                    return;
                }

                m_MC2Drivers = value;
                RaisePropertyChanged(() => MC2Drivers);
            }
        }

        public TurnLight TurnLeftLight
        {
            get { return m_TurnLeftLight; }
            set
            {
                if (value == m_TurnLeftLight)
                {
                    return;
                }

                m_TurnLeftLight = value;
                RaisePropertyChanged(() => TurnLeftLight);
            }
        }

        public TurnLight TurnRightLight
        {
            get { return m_TurnRightLight; }
            set
            {
                if (value == m_TurnRightLight)
                {
                    return;
                }

                m_TurnRightLight = value;
                RaisePropertyChanged(() => TurnRightLight);
            }
        }

        /// <summary>
        /// 运行模式
        /// </summary>
        public RunModel RunModel
        {
            get { return m_RunModel; }
            set
            {
                if (value == m_RunModel)
                {
                    return;
                }

                m_RunModel = value;
                RaisePropertyChanged(() => RunModel);
            }
        }

        /// <summary>
        /// 工况
        /// </summary>
        public WorkState WorkState
        {
            get { return m_WorkState; }
            set
            {
                if (value == m_WorkState)
                {
                    return;
                }

                m_WorkState = value;
                RaisePropertyChanged(() => WorkState);
            }
        }

        /// <summary>
        /// 车1火灾
        /// </summary>
        public bool Fire1
        {
            get { return m_Fire1; }
            set
            {
                if (value == m_Fire1)
                {
                    return;
                }

                m_Fire1 = value;
                RaisePropertyChanged(() => Fire1);
            }
        }

        /// <summary>
        /// 车2火灾
        /// </summary>
        public bool Fire2
        {
            get { return m_Fire2; }
            set
            {
                if (value == m_Fire2)
                {
                    return;
                }

                m_Fire2 = value;
                RaisePropertyChanged(() => Fire2);
            }
        }

        /// <summary>
        /// 车3火灾
        /// </summary>
        public bool Fire3
        {
            get { return m_Fire3; }
            set
            {
                if (value == m_Fire3)
                {
                    return;
                }

                m_Fire3 = value;
                RaisePropertyChanged(() => Fire3);
            }
        }

        /// <summary>
        /// 车4火灾
        /// </summary>
        public bool Fire4
        {
            get { return m_Fire4; }
            set
            {
                if (value == m_Fire4)
                {
                    return;
                }

                m_Fire4 = value;
                RaisePropertyChanged(() => Fire4);
            }
        }

        /// <summary>
        /// 转向架裙板故障
        /// </summary>
        public bool BogieApron
        {
            get { return m_BogieApron; }
            set {
                if (value == m_BogieApron)
                {
                    return;
                }
                m_BogieApron = value;
                RaisePropertyChanged(()=>BogieApron);
            }
        }
    }
}
