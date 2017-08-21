using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.Interface.Data;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Tram.CBTC.Casco.Constant;
using Tram.CBTC.Casco.Controller.Navigator;
using Tram.CBTC.Casco.Model;
using Tram.CBTC.Casco.Model.BtnStragy;
using Tram.CBTC.Casco.Model.TempModel;
using Tram.CBTC.Casco.Resources.Keys;
using Tram.CBTC.Casco.View.Contents.RegionPop;
using Tram.CBTC.Casco.ViewModel;
using Tram.CBTC.Infrasturcture.Common;
using Tram.CBTC.Infrasturcture.Events;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Send;

namespace Tram.CBTC.Casco.Controller
{
    [Export]
    public class DomainController : ControllerBase<Lazy<DomainViewModel>>
    {
        public IStateInterfaceFactory StateInterfaceFactory { private set; get; }

        //public ICommand NavigateToCommand { get; private set; }

        private StateInterfaceKey m_CurrentRootStateKey;

        private readonly IRegionManager m_RegionManager;

        private readonly IEventAggregator m_EventAggregator;

        private readonly Stack<StateInterfaceKey> m_NavigateHistory;

        private ISendInterface SendInterface { get { return ViewModel.Value.Domain.SendInterface; } }

        private RunningModel RunningModel { get { return ViewModel.Value.Model.RunningModel; } }

        public DelegateCommand<CommandParameter> LoadedCommand { get; private set; }

        public NavigatorController Navigator { get; private set; }

        public RunningController RunningController { get; private set; }
        /// <summary>
        /// 版本和音量Pop
        /// </summary>
        public ICommand VersionAndVolumeCommand { get; private set; }
        /// <summary>
        /// 车载运行界面
        /// </summary>
        public ICommand VehicleOperationSelectionCommand { get; private set; }
        /// <summary>
        /// 车载运行界面
        /// </summary>
        public ICommand DriverIDInputPop { get; private set; }
        /// <summary>
        /// 计划号
        /// </summary>
        public ICommand PlanInfoCommand { get; private set; }
        /// <summary>
        /// 终点站
        /// </summary>
        public ICommand EndStationSelecCommand { get; private set; }
        /// <summary>
        /// Pop关闭命令
        /// </summary>
        public ICommand ClosePop { get; private set; }
        /// <summary>
        /// 打开信息查询界面
        /// </summary>
        public ICommand OpenInfoSerch { get; private set; }
        /// <summary>
        /// 打开或者关闭声音
        /// </summary>
        public ICommand OpenCloseSound { get; private set; }

        public DelegateCommand<InputWordEvent.EventArgs> InputDriverId { get; private set; }

        [ImportingConstructor]
        public DomainController(Lazy<DomainViewModel> viewModel, IEventAggregator eventAggregator,
            IRegionManager regionManager, IStateInterfaceFactory stateInterfaceFactory, NavigatorController navigator, RunningController runningController) : base(viewModel)
        {
            m_CurrentRootStateKey = StateInterfaceKey.Parser(StateKeys.Root);
            m_EventAggregator = eventAggregator;
            m_RegionManager = regionManager;
            StateInterfaceFactory = stateInterfaceFactory;
            Navigator = navigator;
            RunningController = runningController;
            m_NavigateHistory = new Stack<StateInterfaceKey>();

            LoadedCommand = new DelegateCommand<CommandParameter>(OnLoaded);
            InputDriverId = new DelegateCommand<InputWordEvent.EventArgs>(OnInputDriverId);
            m_Time = new DispatcherTimer();

            VersionAndVolumeCommand = new DelegateCommand((() => { NavigatorToPop(typeof(VersionAndVolumeView)); }));
            VehicleOperationSelectionCommand = new DelegateCommand((() => { NavigatorToPop(typeof(VehicleOperationSelectionView)); }));
            PlanInfoCommand = new DelegateCommand((() => { NavigatorToPop(typeof(PlanInfoView)); }));
            EndStationSelecCommand = new DelegateCommand((() => { NavigatorToPop(typeof(EndStationSelectView)); }));
            DriverIDInputPop = new DelegateCommand(() => { NavigatorToPop(typeof(DriveIDInputPopView)); });
            OpenCloseSound = new DelegateCommand(() =>
                {
                    ViewModel.Value.CBTC.SendInterface.OpenSound(new SendModel<bool>(!ViewModel.Value.CBTC.TrainInfo
                        .System.IsSoundOpen));
                });
            OpenInfoSerch = new DelegateCommand(() => { NavigatorToPop(typeof(InfoSerchView)); });
            ClosePop = new DelegateCommand((() => { NavigatorToPop(typeof(PopNullView)); }));
        }

        private void NavigatorToPop(Type type)
        {
            m_RegionManager.RequestNavigate(RegionNames.RegionPop, type.FullName);
        }

        private void OnInputDriverId(InputWordEvent.EventArgs args)
        {
            switch (args.Control)
            {
                case InputWordEvent.Type.InputString:
                    if (RunningModel.InputtingDriverId == null)
                    {
                        RunningModel.InputtingDriverId = args.Content;
                    }
                    if (RunningModel.InputtingDriverId != null && RunningModel.DriverIdMaxLength >= RunningModel.InputtingDriverId.Length)
                    {
                        RunningModel.InputtingDriverId += args.Content;
                    }
                    break;
                case InputWordEvent.Type.OK:
                    SendInterface.InputDriverId(
                        new SendModel<string>(RunningModel.InputtingDriverId));
                    RunningModel.InputtingDriverId = string.Empty;
                    break;
                case InputWordEvent.Type.Delete:
                    if (!string.IsNullOrWhiteSpace(RunningModel.InputtingDriverId))
                    {
                        RunningModel.InputtingDriverId = RunningModel.InputtingDriverId.Remove(
                            RunningModel.InputtingDriverId.Length - 1, 1);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnLoaded(CommandParameter obj)
        {
            ViewModel.Value.Domain.Initalize();

            RunningController.Initalize(ViewModel.Value);

            Navigator.Initalize(this);
            if (GlobalParam.Instance.InitParam != null)
            {
                if (GlobalParam.Instance.IsDebug)
                {
                    if (GlobalParam.Instance.InitParam.DataPackage.Config.SystemConfig.StartModel == StartModel.Edit)
                    {
                        SetValueWhenDebug();
                    }

                }
            }

        }

        private void SetValueWhenDebug()
        {
            GlobalTimer.Instance.Timer500MS +=
                (sender, args) => { ViewModel.Value.Domain.Other.NowInCBTC = DateTime.Now; };

            //ViewModel.Value.CBTC.RunningInfo.VehicleRunningModel = VehicleRunningModel.VehicleOnlineEqualinterval;
            //ViewModel.Value.CBTC.RunningInfo.VehicleOnlineEqualintervalTime = "7分11秒";
            //ViewModel.Value.CBTC.RunningInfo.TrainSoonerOrLaterStatus = TrainSoonerOrLaterStatus.TrainLater;
            //ViewModel.Value.CBTC.RunningInfo.TrainSoonerOrLaterTime = "52秒";
            //ViewModel.Value.CBTC.RoadInfo.StationInfo.CurrentStation = "成都西站";
            //ViewModel.Value.CBTC.RoadInfo.StationInfo.NextStation = "联工站";
            //ViewModel.Value.CBTC.RoadInfo.StationInfo.EndStation = "郫县西站";
            //ViewModel.Value.CBTC.RoadInfo.StationInfo.PSD.ArriveTime = DateTime.Now;
            //ViewModel.Value.CBTC.RoadInfo.StationInfo.PSD.DepartTime = DateTime.Now;
            //ViewModel.Value.CBTC.RoadInfo.PlanID = "无计划";
            //ViewModel.Value.CBTC.SignalInfo.ForwardDistanceAndTime = "589 米     31秒";
            //ViewModel.Value.CBTC.SignalInfo.AfterDistanceAndTime = "941 米     54秒";
            //ViewModel.Value.CBTC.RoadInfo.DriverID = "666";
            //ViewModel.Value.CBTC.RoadInfo.TrainID = "5";
            //ViewModel.Value.CBTC.RoadInfo.ReturnState = ReturnInfo.CurrentStationReturn;
            //ViewModel.Value.CBTC.RoadInfo.StationInfo.PSD.StationControlCarStatus = StationControlCarStatus.JumStop;
            //ViewModel.Value.CBTC.TrainInfo.BrakeInfo.SignalBrakeStatus = SignalBrakeStatus.EmergentBrake;
            //ViewModel.Value.CBTC.SignalInfo.ATPProtectionControlStatus = ATPProtectionControlStatus.ATPProtectActive;
            //ViewModel.Value.CBTC.SignalInfo.ATPProhibitStatus = ATPProhibitStatus.Enable;
            //ViewModel.Value.CBTC.SignalInfo.BeaconStatus = BeaconStatus.BeaconMissingOrMissedRead;


            //ViewModel.Value.CBTC.SignalInfo.Alram.CurrentLimitSpeedAlram.Text = "当前限速";
            //ViewModel.Value.CBTC.SignalInfo.Alram.CurrentLimitSpeedAlram.Value = 42;
            //ViewModel.Value.CBTC.SignalInfo.Alram.CurrentLimitSpeedAlram.Distance = 0;
            //ViewModel.Value.CBTC.SignalInfo.Alram.CurrentLimitSpeedAlram.Visible = true;

            //ViewModel.Value.CBTC.SignalInfo.Alram.ForwardLimitSppedAlram.Text = "前方限速";
            //ViewModel.Value.CBTC.SignalInfo.Alram.ForwardLimitSppedAlram.Value = 55;
            //ViewModel.Value.CBTC.SignalInfo.Alram.ForwardLimitSppedAlram.Distance = 14;
            //ViewModel.Value.CBTC.SignalInfo.Alram.ForwardLimitSppedAlram.Visible = true;

            //ViewModel.Value.CBTC.SignalInfo.Alram.SemaphoreAlram.Text = "前方信号";
            //ViewModel.Value.CBTC.SignalInfo.Alram.SemaphoreAlram.Value = SemaphoreStaus.Green;
            //ViewModel.Value.CBTC.SignalInfo.Alram.SemaphoreAlram.Distance = 300;
            //ViewModel.Value.CBTC.SignalInfo.Alram.SemaphoreAlram.Visible = true;

            //ViewModel.Value.CBTC.SignalInfo.Alram.RoadRequestAlram.Text = "";
            //ViewModel.Value.CBTC.SignalInfo.Alram.RoadRequestAlram.Value = 42;
            //ViewModel.Value.CBTC.SignalInfo.Alram.RoadRequestAlram.Distance = 200;
            //ViewModel.Value.CBTC.SignalInfo.Alram.RoadRequestAlram.Visible = true;

            //ViewModel.Value.CBTC.SignalInfo.Speed.RunSpeed.Value = 10;
            //ViewModel.Value.CBTC.SignalInfo.Speed.RunSpeed.Visible = true;
            //ViewModel.Value.CBTC.SignalInfo.Speed.RunSpeed.SpeedColor = CBTCColor.Green;


            //ViewModel.Value.CBTC.SignalInfo.Speed.TargetSpeed.Value = 30;
            //ViewModel.Value.CBTC.SignalInfo.Speed.TargetSpeed.Visible = true;
            //ViewModel.Value.CBTC.SignalInfo.Speed.TargetSpeed.SpeedColor = CBTCColor.Green;

            //ViewModel.Value.CBTC.SignalInfo.Speed.AlarmSpeed.Value = 30;
            //ViewModel.Value.CBTC.SignalInfo.Speed.AlarmSpeed.Visible = true;
            //ViewModel.Value.CBTC.SignalInfo.Speed.AlarmSpeed.SpeedColor = CBTCColor.Green;
        }

        private readonly DispatcherTimer m_Time;


        public void NavigateTo(string stateKey)
        {
            var key = StateInterfaceKey.Parser(stateKey);
            if (key.Paths.Count == 1)
            {
                m_CurrentRootStateKey = key;
                m_NavigateHistory.Clear();
                m_NavigateHistory.Push(key);
            }
            else if (m_NavigateHistory.Any())
            {
                var last = m_NavigateHistory.Peek();
                if (last != key.Parent && key.Parent.Paths.Count > 1)
                {
                    m_NavigateHistory.Push(key.Parent);
                }
            }
            else
            {
                m_NavigateHistory.Push(m_CurrentRootStateKey);
            }

            NavigateTo(key);
        }

        public void NavigateBack()
        {
            if (m_NavigateHistory.Any())
            {
                NavigateTo(m_NavigateHistory.Pop());
            }
        }

        private void NavigateTo(StateInterfaceKey key)
        {
            ViewModel.Value.Model.UpdateCurrentState(StateInterfaceFactory.GetOrCreate(key));
        }
    }
}