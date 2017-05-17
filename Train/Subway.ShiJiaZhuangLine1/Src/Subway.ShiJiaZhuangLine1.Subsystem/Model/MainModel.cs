using System.Windows;
using System.Windows.Media;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;
using MMI.Facility.WPFInfrastructure.Event;
using Subway.ShiJiaZhuangLine1.Interface.Enum;
using Subway.ShiJiaZhuangLine1.Interface.Resouce;
using Subway.ShiJiaZhuangLine1.Subsystem.Extension;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class MainModel : NotificationObject
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
        private DriverDoorState m_DriverOneDoorStateRight;
        private EvacuateDoorState m_EvacuateSixDoorState;
        private DriverDoorState m_DriverSixDoorStateLeft;
        private IntervalDoorState m_IntervalOneDoorState;
        private IntervalDoorState m_IntervalSixDoorState;
        private ShiJiaZhuangLine1.Interface.Enum.BrakeModel m_BrakeModel;
        private DriverDoorState m_DriverSixDoorStateRight;
        private DriverDoorState m_DriverOneDoorStateLeft;
        private bool m_IsAutoModelGreen;


        public MainModel()
        {
            DriverSixVisibility = Visibility.Hidden;
            DriverOneVisibility = Visibility.Hidden;
            BrakeVisibility = Visibility.Hidden;
            TractionVisibility = Visibility.Hidden;

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

        public IntervalDoorState IntervalOneDoorState
        {
            set
            {
                if (value == m_IntervalOneDoorState)
                {
                    return;
                }
                m_IntervalOneDoorState = value;
                RaisePropertyChanged(() => IntervalOneDoorState);
            }
            get { return m_IntervalOneDoorState; }

        }
        public IntervalDoorState IntervalSixDoorState
        {
            set
            {
                if (value == m_IntervalSixDoorState)
                {
                    return;
                }
                m_IntervalSixDoorState = value;
                RaisePropertyChanged(() => IntervalSixDoorState);
            }
            get { return m_IntervalSixDoorState; }

        }

        public DriverDoorState DriverOneDoorStateRight
        {
            set
            {
                if (value == m_DriverOneDoorStateRight)
                {
                    return;
                }
                m_DriverOneDoorStateRight = value;
                RaisePropertyChanged(() => DriverOneDoorStateRight);
            }
            get { return m_DriverOneDoorStateRight; }
        }

        public DriverDoorState DriverOneDoorStateLeft
        {
            get { return m_DriverOneDoorStateLeft; }
            set
            {
                if (value == m_DriverOneDoorStateLeft)
                {
                    return;
                }
                m_DriverOneDoorStateLeft = value;
                RaisePropertyChanged(() => DriverOneDoorStateLeft);
            }
        }

        public DriverDoorState DriverSixDoorStateRight
        {
            get { return m_DriverSixDoorStateRight; }
            set
            {
                if (value == m_DriverSixDoorStateRight)
                {
                    return;
                }
                m_DriverSixDoorStateRight = value;
                RaisePropertyChanged(() => DriverSixDoorStateRight);
            }
        }

        public DriverDoorState DriverSixDoorStateLeft
        {
            set
            {
                if (value == m_DriverSixDoorStateLeft)
                {
                    return;
                }
                m_DriverSixDoorStateLeft = value;
                RaisePropertyChanged(() => DriverSixDoorStateLeft);
            }
            get { return m_DriverSixDoorStateLeft; }
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

        public ShiJiaZhuangLine1.Interface.Enum.BrakeModel BrakeModel
        {
            get { return m_BrakeModel; }
            set
            {
                if (value == m_BrakeModel)
                {
                    return;
                }
                m_BrakeModel = value;
                RaisePropertyChanged(() => BrakeModel);
            }
        }

        public SolidColorBrush EmergencyBrush { get; set; }
        /// <summary>
        /// 自动控制绿色
        /// </summary>
        public bool IsAutoModelGreen
        {
            get { return m_IsAutoModelGreen; }
            set
            {
                if (value == m_IsAutoModelGreen)
                    return;
                m_IsAutoModelGreen = value;
                RaisePropertyChanged(() => IsAutoModelGreen);
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

            args.UpdateIfContains(InBoolKeys.Inb列车运行方向_6号车_, b => TrainRunRight = b);


            args.UpdateIfContains(InBoolKeys.Inb紧急制动未施加, (() => BrakeModel = ShiJiaZhuangLine1.Interface.Enum.BrakeModel.EmergenctUnInfliction));
            args.UpdateIfContains(InBoolKeys.Inb紧急制动施加_红_, () => BrakeModel = ShiJiaZhuangLine1.Interface.Enum.BrakeModel.EmergencyInfliction);
            args.UpdateIfContains(InBoolKeys.Inb停放制动施加, (() => BrakeModel = ShiJiaZhuangLine1.Interface.Enum.BrakeModel.ParkingFliction));
            args.UpdateIfContains(InBoolKeys.Inb停放制动未施加, (() => BrakeModel = ShiJiaZhuangLine1.Interface.Enum.BrakeModel.ParkingUninfliction));
            args.UpdateIfContains(InBoolKeys.Inb保持制动, (() => BrakeModel = ShiJiaZhuangLine1.Interface.Enum.BrakeModel.HoldBrake));
            args.UpdateIfContains(InBoolKeys.Inb快速制动, (() => BrakeModel = ShiJiaZhuangLine1.Interface.Enum.BrakeModel.FastBrake));
            args.UpdateIfContains(InBoolKeys.Inb牵引, (() => BrakeModel = ShiJiaZhuangLine1.Interface.Enum.BrakeModel.Traction));
            args.UpdateIfContains(InBoolKeys.Inb惰行, (() => BrakeModel = ShiJiaZhuangLine1.Interface.Enum.BrakeModel.Coasting));
            args.UpdateIfContains(InBoolKeys.Inb常用制动, (() => BrakeModel = ShiJiaZhuangLine1.Interface.Enum.BrakeModel.Braking));

            args.UpdateIfContains(InBoolKeys.Inb列车驾驶模式_未知, () => ControlModel = ControlModel.UnKnow);
            args.UpdateIfContains(InBoolKeys.Inb列车驾驶模式_AR_, () => ControlModel = ControlModel.AR);
            args.UpdateIfContains(InBoolKeys.Inb列车驾驶模式_PM, () => ControlModel = ControlModel.PM);
            args.UpdateIfContains(InBoolKeys.Inb列车驾驶模式_ATO_, () => ControlModel = ControlModel.ATO);
            args.UpdateIfContains(InBoolKeys.Inb列车驾驶模式_SM_, () => ControlModel = ControlModel.SM);
            args.UpdateIfContains(InBoolKeys.Inb列车驾驶模式_RM_, () => ControlModel = ControlModel.RM);
            args.UpdateIfContains(InBoolKeys.Inb列车驾驶模式_URM_, () => ControlModel = ControlModel.URM);
            args.UpdateIfContains(InBoolKeys.Inb驾驶模式ATP切除, () => ControlModel = ControlModel.ATPCut);

            args.UpdateIfContains(InBoolKeys.Inb自动控制绿色, (b) => { IsAutoModelGreen = b; });

            args.UpdateIfContains(InBoolKeys.InB一端司机室右侧门打开, (b) => { if (b) { DriverOneDoorStateRight = DriverDoorState.UnlockOrOpen; } });
            args.UpdateIfContains(InBoolKeys.InB一端司机室右侧门锁闭, (b) => { if (b) { DriverOneDoorStateRight = DriverDoorState.Locked; } });
            args.UpdateIfContains(InBoolKeys.Inb一端司机室右侧门状态未知, (b) => { if (b) { DriverOneDoorStateRight = DriverDoorState.UnKnow; } });


            args.UpdateIfContains(InBoolKeys.InB一端司机室左侧门打开, (b) => { if (b) { DriverOneDoorStateLeft = DriverDoorState.UnlockOrOpen; } });
            args.UpdateIfContains(InBoolKeys.InB一端司机室左侧门锁闭, (b) => { if (b) { DriverOneDoorStateLeft = DriverDoorState.Locked; } });
            args.UpdateIfContains(InBoolKeys.Inb一端司机室左侧门状态未知, (b) => { if (b) { DriverOneDoorStateLeft = DriverDoorState.UnKnow; } });


            args.UpdateIfContains(InBoolKeys.InB二端司机室右侧门打开, (b) => { if (b) { DriverSixDoorStateRight = DriverDoorState.UnlockOrOpen; } });
            args.UpdateIfContains(InBoolKeys.InB二端司机室右侧门锁闭, (b) => { if (b) { DriverSixDoorStateRight = DriverDoorState.Locked; } });
            args.UpdateIfContains(InBoolKeys.Inb二端司机室右侧门状态未知, (b) => { if (b) { DriverSixDoorStateRight = DriverDoorState.UnKnow; } });


            args.UpdateIfContains(InBoolKeys.InB二端司机室左侧门打开, (b) => { if (b) { DriverSixDoorStateLeft = DriverDoorState.UnlockOrOpen; } });
            args.UpdateIfContains(InBoolKeys.InB二端司机室左侧门锁闭, (b) => { if (b) { DriverSixDoorStateLeft = DriverDoorState.Locked; } });
            args.UpdateIfContains(InBoolKeys.Inb二端司机室左侧门状态未知, (b) => { if (b) { DriverSixDoorStateLeft = DriverDoorState.UnKnow; } });

            //EvacuateSixDoorState = EvacuateDoorState.Locked;
            args.UpdateIfContains(InBoolKeys.Inb车1号疏散门未锁_打开_闪烁_, b =>
            {
                if (b) { EvacuateOneDoorState = EvacuateDoorState.UnlockOrOpen; }
            });
            args.UpdateIfContains(InBoolKeys.Inb车1号疏散门锁上, b =>
            {
                if (b) { EvacuateOneDoorState = EvacuateDoorState.Locked; }
            });
            args.UpdateIfContains(InBoolKeys.Inb1号车疏散门状态未知, b =>
             {
                 if (b) { EvacuateOneDoorState = EvacuateDoorState.UnKnow; }
             });

            args.UpdateIfContains(InBoolKeys.Inb车6号疏散门未锁_打开_闪烁_, b =>
            {
                if (b) { EvacuateSixDoorState = EvacuateDoorState.UnlockOrOpen; }
            });
            args.UpdateIfContains(InBoolKeys.Inb车6号疏散门锁上, b =>
            {
                if (b)
                {
                    EvacuateSixDoorState = EvacuateDoorState.Locked;
                }
            });
            args.UpdateIfContains(InBoolKeys.Inb6号车疏散门状态未知, b =>
            {
                if (b) { EvacuateSixDoorState = EvacuateDoorState.UnKnow; }
            });

            args.UpdateIfContains(InBoolKeys.Inb车1号间隔门未锁_打开_闪烁_, b =>
            {
                if (b) { IntervalOneDoorState = IntervalDoorState.UnlockOrOpen; }
            });
            args.UpdateIfContains(InBoolKeys.Inb车1号间隔门锁上, b =>
                {
                    if (b)
                    {
                        IntervalOneDoorState = IntervalDoorState.Locked;
                    }
                });
            args.UpdateIfContains(InBoolKeys.Inb1号车间隔门状态未知, b => { if (b) { IntervalOneDoorState = IntervalDoorState.UnKnow; } });

            args.UpdateIfContains(InBoolKeys.Inb车6号间隔门未锁_打开_闪烁_, b =>
            {
                if (b)
                {
                    IntervalSixDoorState = IntervalDoorState.UnlockOrOpen;
                }
            });
            args.UpdateIfContains(InBoolKeys.Inb车6号间隔门锁上, b =>
            {
                if (b)
                {
                    IntervalSixDoorState = IntervalDoorState.Locked;
                }
            });
            args.UpdateIfContains(InBoolKeys.Inb6号车间隔门状态未知, b => { if (b) { IntervalSixDoorState = IntervalDoorState.UnKnow; } });
        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {
            args.UpdateIfContains(InFloatKeys.限速, f => LimitSpeed = f);
            args.UpdateIfContains(InFloatKeys.速度, f => Speed = f);
            args.UpdateIfContains(InFloatKeys.牵引百分比, f => TractionEffective = f);
            args.UpdateIfContains(InFloatKeys.制动百分比, f => BrakeEffective = f);
        }
    }
}