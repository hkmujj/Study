using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using Subway.ShenZhenLine7.Config;
using Subway.ShenZhenLine7.Controller.ViewModelController;
using Subway.ShenZhenLine7.Interfaces;
using Subway.ShenZhenLine7.Models;
using Subway.ShenZhenLine7.Models.Units;

namespace Subway.ShenZhenLine7.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    [Export(typeof(ICourceStatusChanged))]
    public class MasterPageViewModel : ViewModelBase
    {
        private ObservableCollection<BorderCastItem> m_AllBorderCastItems;
        private string m_PageInfo;
        private BorderCastItem m_SelectItem;
        private IEnumerable<BorderCastItem> m_DisplayItems;
        private string m_Speed;
        private string m_LimitSpeed;
        private WorkState m_WorkState;
        private SignalModel m_SignalModel;
        private ControlModel m_ControlModel;
        private StationModel m_StationModel;
        private double m_Traction;
        private double m_Brake;
        private bool m_IsActiveCar1;
        private bool m_IsActiveCar6;
        private bool m_IsRunCar6;
        private bool m_IsRunCar1;
        private bool m_IsLeftOpenDoor;
        private bool m_IsRightOpenDoor;
        private BrakeStatus m_BrakeStatus;
        private RunModel m_RunModel;

        /// <summary>
        /// 
        /// </summary>
        public MasterPageController Controller { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        [ImportingConstructor]
        public MasterPageViewModel(MasterPageController controller)
        {
            Controller = controller;
            Controller.ViewModel = this;
            BrakeStatus = BrakeStatus.EmergencyBrakeUnInfliction;
            AllBorderCastItems = new ObservableCollection<BorderCastItem>(GlobalParam.Instance.BorderCastConfig.BorderCastItems);
        }

        /// <summary>
        /// 初始化运行数据
        /// </summary>
        public override void Start()
        {
            Controller.Initalize();
        }

        /// <summary>
        /// DoorViewModel
        /// </summary>
        [Import]
        public DoorViewModel DoorViewModel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        [Import]
        public AirConditionViewModel AirConditionViewModel { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        [Import]
        public AssistViewModel AssistViewModel { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        [Import]
        public EmergencyTalkViewModel EmergencyTalkViewModel { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        [Import]
        public BrakeViewModel BrakeViewModel { get; private set; }
        /// <summary>
        /// 牵引
        /// </summary>
        [Import]
        public TractionViewModel TractionViewModel { get; private set; }
        /// <summary>
        /// 烟火报警
        /// </summary>
        [Import]
        public SmokeViewModel SmokeViewModel { get; private set; }

        /// <summary>
        /// 空压机
        /// </summary>
        [Import]
        public AirPumpViewModel AirPumpViewModel { get; private set; }
        /// <summary>
        /// HSCB
        /// </summary>
        [Import]
        public HSCBViewModel HSCBViewModel { get; private set; }

        /// <summary>
        /// 主界面按钮
        /// </summary>
        [Import]
        public MainContentBrnViewModel MainContentBrnViewModel { get; private set; }

        /// <summary>
        /// 页面信息
        /// </summary>
        public string PageInfo
        {
            get { return m_PageInfo; }
            set
            {
                if (value == m_PageInfo)
                {
                    return;
                }
                m_PageInfo = value;
                RaisePropertyChanged(() => PageInfo);
            }
        }

        /// <summary>
        /// 选中项
        /// </summary>
        public BorderCastItem SelectItem
        {
            get { return m_SelectItem; }
            set
            {
                if (Equals(value, m_SelectItem))
                {
                    return;
                }
                m_SelectItem = value;
                RaisePropertyChanged(() => SelectItem);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BorderCastItem> DisplayItems
        {
            get { return m_DisplayItems; }
            set
            {
                if (Equals(value, m_DisplayItems))
                {
                    return;
                }
                m_DisplayItems = value;
                RaisePropertyChanged(() => DisplayItems);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<BorderCastItem> AllBorderCastItems
        {
            get { return m_AllBorderCastItems; }
            private set
            {
                if (Equals(value, m_AllBorderCastItems))
                {
                    return;
                }
                m_AllBorderCastItems = value;
                RaisePropertyChanged(() => AllBorderCastItems);
            }
        }

        /// <summary>
        /// 制动
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
        /// 牵引
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
        /// 制动状态
        /// </summary>
        public BrakeStatus BrakeStatus
        {
            get { return m_BrakeStatus; }
            set
            {
                if (value == m_BrakeStatus)
                    return;
                m_BrakeStatus = value;
                RaisePropertyChanged(() => BrakeStatus);
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
                    return;
                m_RunModel = value;
                RaisePropertyChanged(() => RunModel);
            }
        }

        /// <summary>
        /// 报站模式
        /// </summary>
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
        /// 信号模式
        /// </summary>
        public SignalModel SignalModel
        {
            get { return m_SignalModel; }
            set
            {
                if (value == m_SignalModel)
                {
                    return;
                }
                m_SignalModel = value;
                RaisePropertyChanged(() => SignalModel);
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
        /// 限制速度
        /// </summary>
        public string LimitSpeed
        {
            get { return m_LimitSpeed; }
            set
            {
                if (value == m_LimitSpeed)
                {
                    return;
                }
                m_LimitSpeed = value;
                RaisePropertyChanged(() => LimitSpeed);
            }
        }

        /// <summary>
        /// 速度
        /// </summary>
        public string Speed
        {
            get { return m_Speed; }
            set
            {
                if (value == m_Speed)
                {
                    return;
                }
                m_Speed = value;
                RaisePropertyChanged(() => Speed);
            }
        }

        /// <summary>
        /// 激活司机室1
        /// </summary>
        public bool IsActiveCar1
        {
            get { return m_IsActiveCar1; }
            set
            {
                if (value == m_IsActiveCar1)
                {
                    return;
                }
                m_IsActiveCar1 = value;
                RaisePropertyChanged(() => IsActiveCar1);
            }
        }

        /// <summary>
        /// 激活司机室1
        /// </summary>
        public bool IsActiveCar6
        {
            get { return m_IsActiveCar6; }
            set
            {
                if (value == m_IsActiveCar6)
                {
                    return;
                }
                m_IsActiveCar6 = value;
                RaisePropertyChanged(() => IsActiveCar6);
            }
        }

        /// <summary>
        /// 死机运行方向1
        /// </summary>
        public bool IsRunCar1
        {
            get { return m_IsRunCar1; }
            set
            {
                if (value == m_IsRunCar1)
                {
                    return;
                }
                m_IsRunCar1 = value;
                RaisePropertyChanged(() => IsRunCar1);
            }
        }

        /// <summary>
        /// 死机运行方向1
        /// </summary>
        public bool IsRunCar6
        {
            get { return m_IsRunCar6; }
            set
            {
                if (value == m_IsRunCar6)
                {
                    return;
                }
                m_IsRunCar6 = value;
                RaisePropertyChanged(() => IsRunCar6);
            }
        }

        /// <summary>
        /// 列车开左门
        /// </summary>
        public bool IsLeftOpenDoor
        {
            get { return m_IsLeftOpenDoor; }
            set
            {
                if (value == m_IsLeftOpenDoor)
                {
                    return;
                }
                m_IsLeftOpenDoor = value;
                RaisePropertyChanged(() => IsLeftOpenDoor);
            }
        }

        /// <summary>
        /// 列车开右门
        /// </summary>
        public bool IsRightOpenDoor
        {
            get { return m_IsRightOpenDoor; }
            set
            {
                if (value == m_IsRightOpenDoor)
                {
                    return;
                }
                m_IsRightOpenDoor = value;
                RaisePropertyChanged(() => IsRightOpenDoor);
            }
        }
    }
}