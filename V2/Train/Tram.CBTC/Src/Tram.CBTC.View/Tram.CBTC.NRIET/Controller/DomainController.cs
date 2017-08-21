using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Tram.CBTC.Infrasturcture.Common;
using Tram.CBTC.Infrasturcture.Events;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Msg.Details;
using Tram.CBTC.Infrasturcture.Model.Send;
using Tram.CBTC.NRIET.Controller.Navigator;
using Tram.CBTC.NRIET.Model;
using Tram.CBTC.NRIET.Model.BtnStragy;
using Tram.CBTC.NRIET.Resources.Keys;
using Tram.CBTC.NRIET.ViewModel;

namespace Tram.CBTC.NRIET.Controller
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

        public DelegateCommand<CommandParameter> LoadedCommand { get; private set; }

        public NavigatorController Navigator { get; private set; }

        public RunningController RunningController { get; private set; }


        //更新最新时间定时器
        private /*readonly*/ DispatcherTimer m_GetNowTimeTimer;


        private bool m_bOpenKeyboard = false;


        private ICommand m_ReentryWindowConfirmCommand;

        private ICommand m_OpenRadarCommand;


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
            ReentryWindowConfirmCommand = new DelegateCommand(ReentryWindowConfirm);
            OpenRadarCommand = new DelegateCommand(OpenRadar);
            m_EventAggregator.GetEvent<PowerStateChangedEvent>().Subscribe(OnPowerChanged, ThreadOption.UIThread);
        }

        private void OnLoaded(CommandParameter obj)
        {
            ViewModel.Value.Domain.Initalize();

            RunningController.Initalize(ViewModel.Value);

            Navigator.Initalize(this);

            ViewModel.Value.CBTC.TrainInfo.CarriageInfo.CurrentCab.OBCUStatusChangedEvent += TrainInfo_OnOBCUStatusChangedEvent;
            ViewModel.Value.CBTC.TrainInfo.CarriageInfo.RemoteCab.OBCUStatusChangedEvent += TrainInfo_OnOBCUStatusChangedEvent;

            if (GlobalParam.Instance.IsDebug)
            {
                SetValueWhenDebug();
            }

            m_GetNowTimeTimer = new DispatcherTimer();

            m_GetNowTimeTimer.Tick += (sender, args) =>
            {
                if (ViewModel.Value.Model != null)
                {
                    ViewModel.Value.Model.CurrentTime = DateTime.Now;
                }
            };

            m_GetNowTimeTimer.Interval = new TimeSpan(0, 0, 1);
            m_GetNowTimeTimer.Start();
        }

        private void SetValueWhenDebug()
        {
            //GlobalTimer.Instance.Timer500MS +=
            //    (sender, args) => { ViewModel.Value.Domain.Other.NowInCBTC = DateTime.Now; };

            ViewModel.Value.CBTC.TrainInfo.PowerState = PowerState.Started;

            //ViewModel.Value.Domain.Message.InformationItems.Add(
            //    new InformationItem(new InformationContent(1, "", "确认前方信号开放，以低速接近")));
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


        private void ReentryWindowConfirm()
        {
            if (ViewModel.Value != null)
            {
                ViewModel.Value.CBTC.RoadInfo.ReturnState = ReturnInfo.None;
            }
        }

        private void OpenRadar()
        {
            if (ViewModel.Value != null)
            {
                ViewModel.Value.CBTC.SendInterface.OpenRadar(new SendModel<bool>(!ViewModel.Value.CBTC.FaultInfo.OpenRadar));
            }
        }

        private void TrainInfo_OnOBCUStatusChangedEvent()
        {
            if (ViewModel.Value.CBTC.TrainInfo.PowerState != PowerState.Stopped)
            {
                var CurCabStatus = ViewModel.Value.CBTC.TrainInfo.CarriageInfo.CurCabStatus;
                var CurrentCab = ViewModel.Value.CBTC.TrainInfo.CarriageInfo.CurrentCab;
                var RemoteCab = ViewModel.Value.CBTC.TrainInfo.CarriageInfo.RemoteCab;

                if (CurCabStatus == 0)
                {
                    if (CurrentCab.State == OBCUStatus.Standby)
                    {
                        Navigator.ToTrainNoActiveViewCommand.Execute(null);
                    }
                    else
                    {
                        Navigator.ToMainViewCommand.Execute(null);
                    }
                }
                else if (CurCabStatus == 1)
                {
                    if (RemoteCab.State == OBCUStatus.Standby)
                    {
                        Navigator.ToTrainNoActiveViewCommand.Execute(null);
                    }
                    else
                    {
                        Navigator.ToMainViewCommand.Execute(null);
                    }
                }
                else if (CurCabStatus == 2)
                {
                    
                }
            }
        }


        private void OnPowerChanged(PowerStateChangedEvent.Args obj)
        {
            if (obj.CurrentState == PowerState.Started)
            {
                Navigator.ToStartViewCommand.Execute(null);
            }
        }


        /// <summary>
        /// 是否打开键盘
        /// </summary>
        public bool IsOpenKeyboard
        {
            get { return m_bOpenKeyboard; }
            set
            {
                if (value == m_bOpenKeyboard)
                {
                    return;
                }

                m_bOpenKeyboard = value;

                RaisePropertyChanged(() => IsOpenKeyboard);
            }
        }



        /// <summary>
        /// 折返界面确认命令
        /// </summary>
        public ICommand ReentryWindowConfirmCommand
        {
            get { return m_ReentryWindowConfirmCommand; }
            private set
            {
                if (value.Equals(m_ReentryWindowConfirmCommand))
                {
                    return;
                }

                m_ReentryWindowConfirmCommand = value;

                RaisePropertyChanged(() => ReentryWindowConfirmCommand);
            }
        }

        /// <summary>
        /// 开启／关闭雷达
        /// </summary>
        public ICommand OpenRadarCommand
        {

            get { return m_OpenRadarCommand; }
            private set
            {
                if (value.Equals(m_OpenRadarCommand))
                {
                    return;
                }

                m_OpenRadarCommand = value;

                RaisePropertyChanged(() => OpenRadarCommand);
            }
        }
    }
}