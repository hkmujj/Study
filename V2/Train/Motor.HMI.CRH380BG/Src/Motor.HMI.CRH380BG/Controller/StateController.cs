using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380BG.Extension;
using Motor.HMI.CRH380BG.Model;
using Motor.HMI.CRH380BG.Model.BtnStragy;
using Motor.HMI.CRH380BG.Resources.Keys;
using Motor.HMI.CRH380BG.ViewModel;

namespace Motor.HMI.CRH380BG.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StateController : ControllerBase<StateViewModel>
    {
        public IStateInterfaceFactory StateInterfaceFactory { private set; get; }

        private StateInterfaceKey m_CurrentRootStateKey;

        private readonly IRegionManager m_RegionManager;

        private readonly IEventAggregator m_EventAggregator;

        private readonly Stack<StateInterfaceKey> m_NavigateHistory;
        //private DelegateCommand<CommandParameter> m_LoadedCommand;

        //public DelegateCommand<CommandParameter> LoadedCommand
        //{
        //    private set
        //    {
        //        if (Equals(value, m_LoadedCommand))
        //        {
        //            return;
        //        }
        //        m_LoadedCommand = value;
        //        RaisePropertyChanged(() => LoadedCommand);
        //    }
        //    get { return m_LoadedCommand; }
        //}

        [ImportingConstructor]
        public StateController(IEventAggregator eventAggregator, IRegionManager regionManager, IStateInterfaceFactory stateInterfaceFactory) 
        {
            m_CurrentRootStateKey = StateInterfaceKey.Parser(StateKeys.Root);
            m_EventAggregator = eventAggregator;
            m_RegionManager = regionManager;
            StateInterfaceFactory = stateInterfaceFactory;
            m_NavigateHistory = new Stack<StateInterfaceKey>();
            //LoadedCommand = new DelegateCommand<CommandParameter>(OnLoaded);
        }

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            NavigateToRoot();
        }

        public void OnLoaded(CommandParameter obj)
        {
            switch (ViewModel.Model.ViewLocation)
            {
                case ViewLocation.Main:
                    NavigateToRoot();
                    break;
                case ViewLocation.Reserve:
                    NavigateToBrake();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void NavigateToRoot()
        {
            NavigateTo(StateKeys.Root);
        }

        public void NavigateToBrake()
        {
            NavigateTo(StateKeys.Root_制动状态);
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
            ViewModel.Model.UpdateCurrentState(StateInterfaceFactory.GetOrCreate(key));
        }

        public void Reset()
        {
            NavigateToRoot();
            var ws = GlobalParam.Instance.InitParam.CommunicationDataService.WritableReadService;
            ws.ChangedInBoolOf(InBoolKeys.Inb制动屏黑屏, false);
            ws.ChangedInBoolOf(InBoolKeys.Inb非制动屏黑屏, false);
            ViewModel.Model.IsVisble = false;
        }
    }
}