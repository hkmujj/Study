using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.Interface.Data;
using Subway.XiaMenLine1.Interface.Enum;
using Subway.XiaMenLine1.Subsystem.Constant;
using Subway.XiaMenLine1.Interface.Resouce;
using Subway.XiaMenLine1.Subsystem.Extension;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    public class MainModel : ViewModelBase
    {
        private bool m_TrainRunRight;
        private bool m_TrainRightDoorOpen;
        private bool m_TrainRunLeft;
        private bool m_TrainLeftDoorOpen;
        private ControlModel m_ControlModel;
        private Visibility m_HighSpeedVisibility;
        private Visibility m_DriverSixVisibility;
        private Visibility m_DriverOneVisibility;
        private StationModel m_StationModel;
        private double m_LimitSpeed;
        private double m_Speed;
        private double m_TractionEffective;
        private double m_BrakeEffective;
        private Visibility m_TractionVisibility;
        private Visibility m_BrakeVisibility;
        private Visibility m_DoorNumVisibility;
        private bool m_EnmergencyBrake;
        private EvacuateDoorState m_EvacuateOneDoorState;
        private DriverDoorState m_DriverOneDoorDownState;
        private EvacuateDoorState m_EvacuateSixDoorState;
        private DriverDoorState m_DriverSixDownDoorState;
        private bool m_Clouping;
        private DriverDoorState m_DriverOneDoorUpState;
        private DriverDoorState m_DriverSixDoorUpState;
        private WorkModel m_WorkModel;
        private bool m_UnitClouping;
        private bool m_PantographOne;
        private bool m_PantographTwo;
        private bool m_IsAutoControl;
        private bool m_TrainRunRightBack;
        private bool m_TrainRunLeftBack;
        private IntervalDoorState m_IntervalDoorStateSix;
        private IntervalDoorState m_IntervalDoorStateOne;
        public ICommand JumpEnmergencyPage { get; private set; }

        public MainModel()
        {
            DriverSixVisibility = Visibility.Hidden;
            DriverOneVisibility = Visibility.Hidden;
            BrakeVisibility = Visibility.Hidden;
            TractionVisibility = Visibility.Hidden;
            JumpEnmergencyPage = new DelegateCommand(() =>
            {
                RegionManager.RequestNavigate(RegionNames.ShellContentRegion, ViewNames.EmergencyCauseView);
            }, () => WorkModel == WorkModel.EB);

        }

        public Visibility DoorNumVisibility
        {
            get { return m_DoorNumVisibility; }
            set
            {
                if (value == m_DoorNumVisibility)
                {
                    return;
                }
                m_DoorNumVisibility = value;
                RaisePropertyChanged(() => DoorNumVisibility);
            }
        }

        public EvacuateDoorState EvacuateOneDoorState
        {
            set
            {
                if (value == m_EvacuateOneDoorState)
                {
                    return;
                }
                m_EvacuateOneDoorState = value;
                RaisePropertyChanged(() => EvacuateOneDoorState);
            }
            get { return m_EvacuateOneDoorState; }
        }

        public EvacuateDoorState EvacuateSixDoorState
        {
            set
            {
                if (value == m_EvacuateSixDoorState)
                {
                    return;
                }
                m_EvacuateSixDoorState = value;
                RaisePropertyChanged(() => EvacuateSixDoorState);
            }
            get { return m_EvacuateSixDoorState; }
        }

        public IntervalDoorState IntervalDoorStateOne
        {
            get { return m_IntervalDoorStateOne; }
            set
            {
                if (value == m_IntervalDoorStateOne)
                    return;

                m_IntervalDoorStateOne = value;
                RaisePropertyChanged(() => IntervalDoorStateOne);
            }
        }

        public IntervalDoorState IntervalDoorStateSix
        {
            get { return m_IntervalDoorStateSix; }
            set
            {
                if (value == m_IntervalDoorStateSix)
                    return;

                m_IntervalDoorStateSix = value;
                RaisePropertyChanged(() => IntervalDoorStateSix);
            }
        }

        public DriverDoorState DriverOneDoorUpState
        {
            get { return m_DriverOneDoorUpState; }
            set
            {
                if (value == m_DriverOneDoorUpState)
                {
                    return;
                }
                m_DriverOneDoorUpState = value;
                RaisePropertyChanged(() => DriverOneDoorUpState);
            }
        }

        public DriverDoorState DriverOneDoorDownState
        {
            set
            {
                if (value == m_DriverOneDoorDownState)
                {
                    return;
                }
                m_DriverOneDoorDownState = value;
                RaisePropertyChanged(() => DriverOneDoorDownState);
            }
            get { return m_DriverOneDoorDownState; }
        }

        public DriverDoorState DriverSixDownDoorState
        {
            set
            {
                if (value == m_DriverSixDownDoorState)
                {
                    return;
                }
                m_DriverSixDownDoorState = value;
                RaisePropertyChanged(() => DriverSixDownDoorState);
            }
            get { return m_DriverSixDownDoorState; }
        }

        public DriverDoorState DriverSixDoorUpState
        {
            get { return m_DriverSixDoorUpState; }
            set
            {
                if (value == m_DriverSixDoorUpState)
                {
                    return;
                }
                m_DriverSixDoorUpState = value;
                RaisePropertyChanged(() => DriverSixDoorUpState);
            }
        }

        public bool UnitClouping
        {
            get { return m_UnitClouping; }
            set
            {
                if (value == m_UnitClouping)
                {
                    return;
                }
                m_UnitClouping = value;
                RaisePropertyChanged(() => UnitClouping);
            }
        }

        public bool PantographOne
        {
            get { return m_PantographOne; }
            set
            {
                if (value == m_PantographOne)
                {
                    return;
                }
                m_PantographOne = value;
                RaisePropertyChanged(() => PantographOne);
            }
        }

        public bool PantographTwo
        {
            get { return m_PantographTwo; }
            set
            {
                if (value == m_PantographTwo)
                {
                    return;
                }
                m_PantographTwo = value;
                RaisePropertyChanged(() => PantographTwo);
            }
        }

        public bool Clouping
        {
            get { return m_Clouping; }
            set
            {
                if (value == m_Clouping)
                {
                    return;
                }
                m_Clouping = value;
                RaisePropertyChanged(() => Clouping);
            }
        }

        /// <summary>
        /// 司机室激活1端
        /// </summary>
        public Visibility DriverOneVisibility
        {
            get { return m_DriverOneVisibility; }
            set
            {
                if (value == m_DriverOneVisibility)
                {
                    return;
                }
                m_DriverOneVisibility = value;
                RaisePropertyChanged(() => DriverOneVisibility);
            }
        }
        /// <summary>
        /// 司机室激活6端
        /// </summary>
        public Visibility DriverSixVisibility
        {
            get { return m_DriverSixVisibility; }
            set
            {
                if (value == m_DriverSixVisibility)
                {
                    return;
                }
                m_DriverSixVisibility = value;
                RaisePropertyChanged(() => DriverSixVisibility);
            }
        }

        /// <summary>
        /// 制动有效率
        /// </summary>
        public double BrakeEffective
        {
            get { return m_BrakeEffective; }
            set
            {
                if (value.Equals(m_BrakeEffective))
                {
                    return;
                }
                m_BrakeEffective = (int)value;
                BrakeVisibility = m_BrakeEffective > float.Epsilon ? Visibility.Visible : Visibility.Hidden;
                RaisePropertyChanged(() => BrakeEffective);
            }
        }

        /// <summary>
        /// 牵引有效率
        /// </summary>
        public double TractionEffective
        {
            get { return m_TractionEffective; }
            set
            {
                if (value.Equals(m_TractionEffective))
                {
                    return;
                }
                m_TractionEffective = (int)value;
                TractionVisibility = m_TractionEffective > float.Epsilon ? Visibility.Visible : Visibility.Hidden;
                RaisePropertyChanged(() => TractionEffective);
            }
        }

        public Visibility BrakeVisibility
        {
            get { return m_BrakeVisibility; }
            set
            {
                if (value == m_BrakeVisibility)
                {
                    return;
                }
                m_BrakeVisibility = value;
                RaisePropertyChanged(() => BrakeVisibility);
            }
        }

        public Visibility TractionVisibility
        {
            get { return m_TractionVisibility; }
            set
            {
                if (value == m_TractionVisibility)
                {
                    return;
                }
                m_TractionVisibility = value;
                RaisePropertyChanged(() => TractionVisibility);
            }
        }

        /// <summary>
        /// 高速断路器
        /// </summary>
        public Visibility HighSpeedVisibility
        {
            get { return m_HighSpeedVisibility; }
            set
            {
                if (value == m_HighSpeedVisibility)
                {
                    return;
                }
                m_HighSpeedVisibility = value;
                RaisePropertyChanged(() => HighSpeedVisibility);
            }
        }

        /// <summary>
        /// 紧急制动施加
        /// </summary>
        public bool EnmergencyBrake
        {
            get { return m_EnmergencyBrake; }
            set
            {
                if (value == m_EnmergencyBrake)
                {
                    return;
                }
                m_EnmergencyBrake = value;
                RaisePropertyChanged(() => EnmergencyBrake);
            }
        }

        public SolidColorBrush EmergencyBrush { get; set; }

        public WorkModel WorkModel
        {
            get { return m_WorkModel; }
            set
            {
                if (value == m_WorkModel)
                {
                    return;
                }
                m_WorkModel = value;
                RaisePropertyChanged(() => WorkModel);
            }
        }

        /// <summary>
        /// 控制模式
        /// </summary>
        public ControlModel ControlModel
        {
            get { return m_ControlModel; }
            set
            {
                if (value == m_ControlModel)
                {
                    return;
                }
                m_ControlModel = value;
                RaisePropertyChanged(() => ControlModel);
            }
        }
        /// <summary>
        /// 左侧门开启
        /// </summary>
        public bool TrainLeftDoorOpen
        {
            get { return m_TrainLeftDoorOpen; }
            set
            {
                if (value == m_TrainLeftDoorOpen)
                {
                    return;
                }
                m_TrainLeftDoorOpen = value;
                RaisePropertyChanged(() => TrainLeftDoorOpen);
            }
        }
        /// <summary>
        /// 左侧运行方向
        /// </summary>
        public bool TrainRunLeft
        {
            get { return m_TrainRunLeft; }
            set
            {
                if (value == m_TrainRunLeft)
                {
                    return;
                }
                m_TrainRunLeft = value;
                RaisePropertyChanged(() => TrainRunLeft);
            }
        }
        /// <summary>
        /// 右侧车门开启
        /// </summary>
        public bool TrainRightDoorOpen
        {
            get { return m_TrainRightDoorOpen; }
            set
            {
                if (value == m_TrainRightDoorOpen)
                {
                    return;
                }
                m_TrainRightDoorOpen = value;
                RaisePropertyChanged(() => TrainRightDoorOpen);
            }
        }
        /// <summary>
        /// 运行方向右侧
        /// </summary>
        public bool TrainRunRight
        {
            get { return m_TrainRunRight; }
            set
            {
                if (value == m_TrainRunRight)
                {
                    return;
                }
                m_TrainRunRight = value;
                RaisePropertyChanged(() => TrainRunRight);
            }
        }

        /// <summary>
        /// 运行方向右侧向后
        /// </summary>
        public bool TrainRunRightBack
        {
            get { return m_TrainRunRightBack; }
            set
            {
                if (value == m_TrainRunRightBack)
                {
                    return;
                }
                m_TrainRunRightBack = value;
                RaisePropertyChanged(() => TrainRunRightBack);
            }
        }

        /// <summary>
        /// 运行方向左侧向后
        /// </summary>
        public bool TrainRunLeftBack
        {
            get { return m_TrainRunLeftBack; }
            set
            {
                if (value == m_TrainRunLeftBack)
                {
                    return;
                }
                m_TrainRunLeftBack = value;
                RaisePropertyChanged(() => TrainRunLeftBack);
            }
        }

        public double LimitSpeed
        {
            get { return m_LimitSpeed; }
            set
            {
                if (value.Equals(m_LimitSpeed))
                {
                    return;
                }
                m_LimitSpeed = value;
                RaisePropertyChanged(() => LimitSpeed);
            }
        }

        public StationModel StationModel
        {
            get { return m_StationModel; }
            set
            {
                if (value == m_StationModel)
                {
                    return;
                }
                m_StationModel = value;
                RaisePropertyChanged(() => StationModel);
            }
        }

        public double Speed
        {
            get { return m_Speed; }
            set
            {
                if (value.Equals(m_Speed))
                {
                    return;
                }
                m_Speed = value;
                RaisePropertyChanged(() => Speed);
            }
        }

        public bool IsAutoControl
        {
            get { return m_IsAutoControl; }
            set
            {
                if (value == m_IsAutoControl)
                {
                    return;
                }
                m_IsAutoControl = value;
                RaisePropertyChanged(() => IsAutoControl);
            }
        }

        public void ChangeStatus(object sender, CommunicationDataChangedArgs e)
        {
            ChangeBools(e.ChangedBools);
            ChangeFloats(e.ChangedFloats);
        }

        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {
            args.UpdateIfContains(InBoolKeys.Inb报站模式_自动_, () => StationModel = StationModel.Auto);
            args.UpdateIfContains(InBoolKeys.Inb报站模式_人工_, () => StationModel = StationModel.Manual);
            args.UpdateIfContains(InBoolKeys.Inb报站模式_半自动_, () => StationModel = StationModel.HalfAuto);

            args.UpdateIfContains(InBoolKeys.Inb列车左侧预开门, b => TrainLeftDoorOpen = b);
            args.UpdateIfContains(InBoolKeys.Inb列车右侧预开门, b => TrainRightDoorOpen = b);
            args.UpdateIfContains(InBoolKeys.Inb车1号司机台激活, b => DriverOneVisibility = b ? Visibility.Visible : Visibility.Hidden);
            args.UpdateIfContains(InBoolKeys.Inb车6号司机台激活, b => DriverSixVisibility = b ? Visibility.Visible : Visibility.Hidden);
            args.UpdateIfContains(InBoolKeys.Inb列车运行方向_1号车_, b => TrainRunLeft = b);
            args.UpdateIfContains(InBoolKeys.Inb列车运行方向1号车向后, b => TrainRunLeftBack = b);
            args.UpdateIfContains(InBoolKeys.Inb列车运行方向6号车向后, b => TrainRunRightBack = b);
            args.UpdateIfContains(InBoolKeys.Inb列车运行方向_6号车_, b => TrainRunRight = b);
            args.UpdateIfContains(InBoolKeys.Inb列车紧急, b => EnmergencyBrake = b);
            args.UpdateIfContains(InBoolKeys.Inb两列车连挂好, () => { Clouping = true; }, () => { Clouping = false; });
            args.UpdateIfContains(InBoolKeys.Inb两单元连挂好, () => { UnitClouping = true; }, () => { UnitClouping = false; });
            //TODO
            // DriverSixDownDoorState = DriverDoorState.Locked;
            args.UpdateIfContains(InBoolKeys.Inb车载信号系统_自动控制_, () => IsAutoControl = true, () => IsAutoControl = false);

            args.UpdateIfContains(InBoolKeys.Inb车1号疏散门未锁_打开_闪烁_, () => { EvacuateOneDoorState = EvacuateDoorState.UnlockOrOpen; });
            args.UpdateIfContains(InBoolKeys.Inb车1号疏散门锁上, () => { EvacuateOneDoorState = EvacuateDoorState.Locked; });
            args.UpdateIfContains(InBoolKeys.Inb车1号疏散门未知, () => { EvacuateOneDoorState = EvacuateDoorState.Unknow; });
            args.UpdateIfContains(InBoolKeys.Inb车6号疏散门未锁_打开_闪烁_, () => { EvacuateSixDoorState = EvacuateDoorState.UnlockOrOpen; });
            args.UpdateIfContains(InBoolKeys.Inb车6号疏散门锁上, () => { EvacuateSixDoorState = EvacuateDoorState.Locked; });
            args.UpdateIfContains(InBoolKeys.Inb车6号疏散门未知, () => { EvacuateSixDoorState = EvacuateDoorState.Unknow; });

            args.UpdateIfContains(InBoolKeys.Inb车1号间隔门未锁_打开_闪烁_, () => { IntervalDoorStateOne = IntervalDoorState.UnlockOrOpen; });
            args.UpdateIfContains(InBoolKeys.Inb车1号间隔门锁上, () => { IntervalDoorStateOne = IntervalDoorState.Locked; });
            args.UpdateIfContains(InBoolKeys.Inb车1号间隔门未知, () => { IntervalDoorStateOne = IntervalDoorState.UnKown; });
            args.UpdateIfContains(InBoolKeys.Inb车6号间隔门未锁_打开_闪烁_, () => { IntervalDoorStateSix = IntervalDoorState.UnlockOrOpen; });
            args.UpdateIfContains(InBoolKeys.Inb车6号间隔门锁上, () => { IntervalDoorStateSix = IntervalDoorState.Locked; });
            args.UpdateIfContains(InBoolKeys.Inb车6号间隔门未知, () => { IntervalDoorStateSix = IntervalDoorState.UnKown; });

            args.UpdateIfContains(InBoolKeys.Inb车1司机室门1打开, () => { DriverOneDoorUpState = DriverDoorState.UnlockOrOpen; });
            args.UpdateIfContains(InBoolKeys.Inb车1司机室门1锁闭, () => { DriverOneDoorUpState = DriverDoorState.Locked; });
            args.UpdateIfContains(InBoolKeys.Inb车1司机室门1未知, () => { DriverOneDoorUpState = DriverDoorState.UnKnow; });

            args.UpdateIfContains(InBoolKeys.Inb车1司机室门2打开, () => { DriverOneDoorDownState = DriverDoorState.UnlockOrOpen; });
            args.UpdateIfContains(InBoolKeys.Inb车1司机室门2锁闭, () => { DriverOneDoorDownState = DriverDoorState.Locked; });
            args.UpdateIfContains(InBoolKeys.Inb车1司机室门2未知, () => { DriverOneDoorDownState = DriverDoorState.UnKnow; });

            args.UpdateIfContains(InBoolKeys.Inb车6司机室门1打开, () => { DriverSixDoorUpState = DriverDoorState.UnlockOrOpen; });
            args.UpdateIfContains(InBoolKeys.Inb车6司机室门1锁闭, () => { DriverSixDoorUpState = DriverDoorState.Locked; });
            args.UpdateIfContains(InBoolKeys.Inb车6司机室门1未知, () => { DriverSixDoorUpState = DriverDoorState.UnKnow; });

            args.UpdateIfContains(InBoolKeys.Inb车6司机室门2打开, () => { DriverSixDownDoorState = DriverDoorState.UnlockOrOpen; });
            args.UpdateIfContains(InBoolKeys.Inb车6司机室门2锁闭, () => { DriverSixDownDoorState = DriverDoorState.Locked; });
            args.UpdateIfContains(InBoolKeys.Inb车6司机室门2未知, () => { DriverSixDownDoorState = DriverDoorState.UnKnow; });
        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {
            args.UpdateIfContains(InFloatKeys.限速, f => LimitSpeed = f);
            args.UpdateIfContains(InFloatKeys.速度, f => Speed = f);
            args.UpdateIfContains(InFloatKeys.牵引百分比, f => TractionEffective = f);
            args.UpdateIfContains(InFloatKeys.制动百分比, f => BrakeEffective = f);
            args.UpdateIfContains(InFloatKeys.运行模式, f => ControlModel = (ControlModel)f);
            args.UpdateIfContains(InFloatKeys.运行工况, f => WorkModel = (WorkModel)f);
        }
    }
}