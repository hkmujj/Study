﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CBTC.Infrasturcture.Common;
using CBTC.Infrasturcture.Events;
using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Msg.Details;
using CBTC.Infrasturcture.Model.Send;
using Microsoft.Practices.Prism.Commands;
using Subway.CBTC.QuanLuTong.Model.BtnStragy;
using Subway.CBTC.QuanLuTong.Resources.Keys;
using Subway.CBTC.QuanLuTong.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.CBTC.QuanLuTong.Controller.Navigator;
using Subway.CBTC.QuanLuTong.Model;
using Subway.CBTC.QuanLuTong.Model.TempModel;

namespace Subway.CBTC.QuanLuTong.Controller
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

            if (GlobalParam.Instance.IsDebug)
            {
                SetValueWhenDebug();
            }
        }

        private void SetValueWhenDebug()
        {
            GlobalTimer.Instance.Timer500MS +=
                (sender, args) => { ViewModel.Value.Domain.Other.NowInCBTC = DateTime.Now; };

            ViewModel.Value.Domain.FaultInfo.FaultLocation= FaultLocation.Radar;
            ViewModel.Value.Domain.FaultInfo.HasFault = true;

            ViewModel.Value.Domain.SignalInfo.JumpStop = JumpStopDetainCar.JumpStop;
            ViewModel.Value.Domain.SignalInfo.ATPConnectState = ATPConnectState.Connected1AndConnecting1;
            ViewModel.Value.Domain.SignalInfo.WarningIntervention.TargetDistanceVisible = true;
            ViewModel.Value.Domain.SignalInfo.WarningIntervention.TargetDistance = 500;

            ViewModel.Value.Domain.SignalInfo.Speed.PermittedSpeed.Value = 49;
            ViewModel.Value.Domain.SignalInfo.Speed.PermittedSpeed.Visible = true;
            ViewModel.Value.Domain.SignalInfo.Speed.EmergencyBrakeInterventionSpeed.Value = 80;
            ViewModel.Value.Domain.SignalInfo.Speed.EmergencyBrakeInterventionSpeed.Visible = true;
            ViewModel.Value.Domain.SignalInfo.Speed.IsZeroSpeed = true;

            ViewModel.Value.Domain.SignalInfo.HighDirveModel = HighDirveModel.RM;

            ViewModel.Value.Domain.RoadInfo.DesID = "031";
            ViewModel.Value.Domain.RoadInfo.DriverID = "2122";
            ViewModel.Value.Domain.RoadInfo.TrainID = "1324";
            ViewModel.Value.Domain.RoadInfo.StationInfo.EndStation = "刘庄";
            ViewModel.Value.Domain.RoadInfo.StationInfo.NextStation = "宋庄";
            ViewModel.Value.Domain.RoadInfo.ReturnState = ReturnState.AutoReturn;

            ViewModel.Value.Domain.TrainInfo.WorkState = WorkState.Tow;
            ViewModel.Value.Domain.TrainInfo.CarriageInfo.CurrentCab.State = CabState.Actived;
            ViewModel.Value.Domain.TrainInfo.CarriageInfo.RemoteCab.State = CabState.Unactived;
            ViewModel.Value.Domain.TrainInfo.BrakeInfo.SignalBrake = BrakeType.CutTow;
            ViewModel.Value.Domain.TrainInfo.BrakeInfo.BrakeState = BrakeState.WheelSlip;
            ViewModel.Value.Domain.TrainInfo.DoorInfo.DoorAllowState = DoorAllowState.AllowBoth;
            ViewModel.Value.Domain.TrainInfo.DoorInfo.LeftDoorState = DoorState.Abnormal;
            ViewModel.Value.Domain.TrainInfo.DoorInfo.RightDoorState = DoorState.Closed;
            ViewModel.Value.Domain.TrainInfo.DoorInfo.LeftPSDState = DoorState.Closed;
            ViewModel.Value.Domain.TrainInfo.DoorInfo.RightPSDState = DoorState.Closed;
            ViewModel.Value.Domain.TrainInfo.DoorInfo.DoorControlMode = DoorControlMode.AOAC;
     

            ViewModel.Value.Domain.RunningInfo.OperatingGrade = OperatingGrade.CBTC;
            ViewModel.Value.Domain.RunningInfo.TrainOperatingMode = TrainOperatingMode.AM;
            ViewModel.Value.Domain.RunningInfo.TrainPosition = TrainPosition.CarDepot;
            ViewModel.Value.Domain.RunningInfo.ParkingAlignmentState = ParkingAlignmentState.AlignmentInStation;

            ViewModel.Value.Domain.Message.InformationItems.Add(
                new InformationItem(new InformationContent(1, "", "确认前方信号开放，以低速接近")));
        }


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