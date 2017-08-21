using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Windows.Controls;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.LCDM.HDX2.Entity.Model.Domain
{
    [Export]
    public class LCDMModel : NotificationObject, IRaiseResourceChangedProvider, IResetable
    {
        private GateLevel m_SmallGateLevel;
        private GateLevel m_BitGateLevel;
        private double m_EleBrakeForce;
        private EleBrakeLevel m_EleBrakeLevel;
        private double m_TractiveForce;
        private TractiveLevel m_TractiveLevel;
        private double m_TrainSpeed;
        private double m_TrainTailPressure;
        private double m_ReducePressure;
        private double m_FlowRate;
        private double m_BalancePressure;
        private double m_TrainPipePressure;
        private double m_TotalCylinderPressure;
        private double m_BrakeCylinderPressure;
        private BrakeCylinder m_NoneOperBrakeCylinder;
        private BrakeCylinder m_OperBrakeCylinder;
        private TrainState m_TrainState;
        private BrakeState m_BrakeState;
        private SmallGateState m_SmallGateState;
        private BigGateState m_BigGateState;
        private VehicleCount m_VehicleCount;
        private TrainType m_TrainType;
        private TotalAirPresssureState m_TotalAirPresssureState;
        private PowerState m_PowerState;
        private string[] m_TrainId;
        private ReserveCommon m_ReserveCommon;
        private AirBrake m_AirBrake;
        private Other m_Other;
        private Message m_CurrentMessage;
        private MachineType m_MachineType;
        private EngineControlModel m_EngineControlModel;

        [ImportingConstructor]
        public LCDMModel(ISendInterface sendInterface)
        {
            SendInterface = sendInterface;
            NoneOperBrakeCylinder = new BrakeCylinder();
            OperBrakeCylinder = new BrakeCylinder();
            Other = new Other();
            WorkFlags = new WorkFlags();
            AirBrake = new AirBrake();
            ResetDatas();
        }

        public ISendInterface SendInterface { private set; get; }

        public MachineType MachineType
        {
            set
            {
                if (value == m_MachineType)
                {
                    return;
                }
                m_MachineType = value;
                RaisePropertyChanged(() => MachineType);
            }
            get { return m_MachineType; }
        }

        public EngineControlModel EngineControlModel
        {
            set
            {
                if (value == m_EngineControlModel)
                {
                    return;
                }
                m_EngineControlModel = value;
                RaisePropertyChanged(() => EngineControlModel);
            }
            get { return m_EngineControlModel; }
        }

        public string[] TrainId
        {
            set
            {
                if (Equals(value, m_TrainId))
                {
                    return;
                }
                m_TrainId = value;
                RaisePropertyChanged(() => TrainId);
            }
            get { return m_TrainId; }
        }

        public WorkFlags WorkFlags { private set; get; }

        public AirBrake AirBrake
        {
            private set
            {
                if (Equals(value, m_AirBrake))
                {
                    return;
                }
                m_AirBrake = value;
                RaisePropertyChanged(() => AirBrake);
            }
            get { return m_AirBrake; }
        }

        /// <summary>
        /// 电源状态
        /// </summary>
        public PowerState PowerState
        {
            set
            {
                if (value == m_PowerState)
                {
                    return;
                }
                m_PowerState = value;
                RaisePropertyChanged(() => PowerState);
            }
            get { return m_PowerState; }
        }

        /// <summary>
        /// 总风状态 
        /// </summary>
        public TotalAirPresssureState TotalAirPresssureState
        {
            set
            {
                if (value == m_TotalAirPresssureState)
                {
                    return;
                }
                m_TotalAirPresssureState = value;
                RaisePropertyChanged(() => TotalAirPresssureState);
            }
            get { return m_TotalAirPresssureState; }
        }

        /// <summary>
        /// 本机、补机
        /// </summary>
        public TrainType TrainType
        {
            set
            {
                if (value == m_TrainType)
                {
                    return;
                }
                m_TrainType = value;
                RaisePropertyChanged(() => TrainType);
            }
            get { return m_TrainType; }
        }

        /// <summary>
        /// 车辆个数
        /// </summary>
        public VehicleCount VehicleCount
        {
            set
            {
                if (value == m_VehicleCount)
                {
                    return;
                }
                m_VehicleCount = value;
                RaisePropertyChanged(() => VehicleCount);
            }
            get { return m_VehicleCount; }
        }

        /// <summary>
        /// 大闸状态
        /// </summary>
        public BigGateState BigGateState
        {
            set
            {
                if (value == m_BigGateState)
                {
                    return;
                }
                m_BigGateState = value;
                RaisePropertyChanged(() => BigGateState);
            }
            get { return m_BigGateState; }
        }

        /// <summary>
        /// 小闸状态
        /// </summary>
        public SmallGateState SmallGateState
        {
            set
            {
                if (value == m_SmallGateState)
                {
                    return;
                }
                m_SmallGateState = value;
                RaisePropertyChanged(() => SmallGateState);
            }
            get { return m_SmallGateState; }
        }

        /// <summary>
        /// 制动工况
        /// </summary>
        public BrakeState BrakeState
        {
            set
            {
                if (value == m_BrakeState)
                {
                    return;
                }
                m_BrakeState = value;
                RaisePropertyChanged(() => BrakeState);
            }
            get { return m_BrakeState; }
        }

        /// <summary>
        /// 机车工况
        /// </summary>
        public TrainState TrainState
        {
            set
            {
                if (value == m_TrainState)
                {
                    return;
                }
                m_TrainState = value;
                RaisePropertyChanged(() => TrainState);
            }
            get { return m_TrainState; }
        }

        /// <summary>
        /// 操作节
        /// </summary>
        public BrakeCylinder OperBrakeCylinder
        {
            set
            {
                if (Equals(value, m_OperBrakeCylinder))
                {
                    return;
                }
                m_OperBrakeCylinder = value;
                RaisePropertyChanged(() => OperBrakeCylinder);
            }
            get { return m_OperBrakeCylinder; }
        }

        /// <summary>
        /// 非操作节
        /// </summary>
        public BrakeCylinder NoneOperBrakeCylinder
        {
            set
            {
                if (Equals(value, m_NoneOperBrakeCylinder))
                {
                    return;
                }
                m_NoneOperBrakeCylinder = value;
                RaisePropertyChanged(() => NoneOperBrakeCylinder);
            }
            get { return m_NoneOperBrakeCylinder; }
        }

        /// <summary>
        /// 制动缸压力
        /// </summary>
        public double BrakeCylinderPressure
        {
            set
            {
                if (value.Equals(m_BrakeCylinderPressure))
                {
                    return;
                }
                m_BrakeCylinderPressure = value;
                RaisePropertyChanged(() => BrakeCylinderPressure);
            }
            get { return m_BrakeCylinderPressure; }
        }

        /// <summary>
        /// 总风缸压力
        /// </summary>
        public double TotalCylinderPressure
        {
            set
            {
                if (value.Equals(m_TotalCylinderPressure))
                {
                    return;
                }
                m_TotalCylinderPressure = value;
                RaisePropertyChanged(() => TotalCylinderPressure);
            }
            get { return m_TotalCylinderPressure; }
        }

        /// <summary>
        /// 列车管
        /// </summary>
        public double TrainPipePressure
        {
            set
            {
                if (value.Equals(m_TrainPipePressure))
                {
                    return;
                }
                m_TrainPipePressure = value;
                RaisePropertyChanged(() => TrainPipePressure);
            }
            get { return m_TrainPipePressure; }
        }

        /// <summary>
        /// 均衡
        /// </summary>
        public double BalancePressure
        {
            set
            {
                if (value.Equals(m_BalancePressure))
                {
                    return;
                }
                m_BalancePressure = value;
                RaisePropertyChanged(() => BalancePressure);
            }
            get { return m_BalancePressure; }
        }

        /// <summary>
        /// 流量
        /// </summary>
        public double FlowRate
        {
            set
            {
                if (value.Equals(m_FlowRate))
                {
                    return;
                }
                m_FlowRate = value;
                RaisePropertyChanged(() => FlowRate);
            }
            get { return m_FlowRate; }
        }

        /// <summary>
        /// 减压
        /// </summary>
        public double ReducePressure
        {
            set
            {
                if (value.Equals(m_ReducePressure))
                {
                    return;
                }
                m_ReducePressure = value;
                RaisePropertyChanged(() => ReducePressure);
            }
            get { return m_ReducePressure; }
        }

        /// <summary>
        /// 列尾压力
        /// </summary>
        public double TrainTailPressure
        {
            set
            {
                if (value.Equals(m_TrainTailPressure))
                {
                    return;
                }
                m_TrainTailPressure = value;
                RaisePropertyChanged(() => TrainTailPressure);
            }
            get { return m_TrainTailPressure; }
        }

        /// <summary>
        /// 速度
        /// </summary>
        public double TrainSpeed
        {
            set
            {
                if (value.Equals(m_TrainSpeed))
                {
                    return;
                }
                m_TrainSpeed = value;
                RaisePropertyChanged(() => TrainSpeed);
            }
            get { return m_TrainSpeed; }
        }

        public ReserveCommon ReserveCommon
        {
            set
            {
                if (value == m_ReserveCommon)
                {
                    return;
                }
                m_ReserveCommon = value;
                RaisePropertyChanged(() => ReserveCommon);
            }
            get { return m_ReserveCommon; }
        }

        /// <summary>
        /// 牵引级
        /// </summary>
        public TractiveLevel TractiveLevel
        {
            set
            {
                if (value == m_TractiveLevel)
                {
                    return;
                }
                m_TractiveLevel = value;
                RaisePropertyChanged(() => TractiveLevel);
            }
            get { return m_TractiveLevel; }
        }

        /// <summary>
        /// 牵引力
        /// </summary>
        public double TractiveForce
        {
            set
            {
                if (value.Equals(m_TractiveForce))
                {
                    return;
                }
                m_TractiveForce = value;
                RaisePropertyChanged(() => TractiveForce);
            }
            get { return m_TractiveForce; }
        }

        /// <summary>
        /// 电制级
        /// </summary>
        public EleBrakeLevel EleBrakeLevel
        {
            set
            {
                if (value == m_EleBrakeLevel)
                {
                    return;
                }
                m_EleBrakeLevel = value;
                RaisePropertyChanged(() => EleBrakeLevel);
            }
            get { return m_EleBrakeLevel; }
        }

        /// <summary>
        /// 电制力
        /// </summary>
        public double EleBrakeForce
        {
            set
            {
                if (value.Equals(m_EleBrakeForce))
                {
                    return;
                }
                m_EleBrakeForce = value;
                RaisePropertyChanged(() => EleBrakeForce);
            }
            get { return m_EleBrakeForce; }
        }

        /// <summary>
        /// 大闸级
        /// </summary>
        public GateLevel BitGateLevel
        {
            set
            {
                if (value == m_BitGateLevel)
                {
                    return;
                }
                m_BitGateLevel = value;
                RaisePropertyChanged(() => BitGateLevel);
            }
            get { return m_BitGateLevel; }
        }

        /// <summary>
        /// 小闸级
        /// </summary>
        public GateLevel SmallGateLevel
        {
            set
            {
                if (value == m_SmallGateLevel)
                {
                    return;
                }
                m_SmallGateLevel = value;
                RaisePropertyChanged(() => SmallGateLevel);
            }
            get { return m_SmallGateLevel; }
        }

        public Other Other
        {
            private set
            {
                if (Equals(value, m_Other))
                {
                    return;
                }
                m_Other = value;
                RaisePropertyChanged(() => Other);
            }
            get { return m_Other; }
        }

        public Message CurrentMessage
        {
            set
            {
                if (Equals(value, m_CurrentMessage))
                {
                    return;
                }
                m_CurrentMessage = value;
                RaisePropertyChanged(() => CurrentMessage);
            }
            get { return m_CurrentMessage; }
        }

        public void RaiseResourceChanged()
        {
            RaisePropertyChanged(() => MachineType);
            RaisePropertyChanged(() => CurrentMessage);
            RaisePropertyChanged(() => EngineControlModel);
        }

        public void ResetDatas()
        {
            Other.ResetDatas();
            AirBrake.ResetDatas();
            VehicleCount = VehicleCount.Multi;
            ReserveCommon = ReserveCommon.Common;
            TrainId = new[] { "1", "1", "1", "2", "A", };
        }
    }
}