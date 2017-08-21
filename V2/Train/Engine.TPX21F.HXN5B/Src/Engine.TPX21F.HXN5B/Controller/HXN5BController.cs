using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TPX21F.HXN5B.Extension;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.Model.BtnStragy;
using Engine.TPX21F.HXN5B.Model.Interface;
using Engine.TPX21F.HXN5B.Resources.Keys;
using Engine.TPX21F.HXN5B.ViewModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TPX21F.HXN5B.Controller
{
    [Export]
    [Export(typeof(IResetSupport))]
    public class HXN5BController : ControllerBase<Lazy<HXN5BViewModel>>, IResetSupport
    {
        public IStateInterfaceFactory StateInterfaceFactory { private set; get; }

        private StateInterfaceKey m_CurrentRootStateKey;

        private readonly IRegionManager m_RegionManager;

        private readonly IEventAggregator m_EventAggregator;

        private readonly Stack<StateInterfaceKey> m_NavigateHistory;

        public DelegateCommand<CommandParameter> LoadedCommand { private set; get; }

        [ImportingConstructor]
        public HXN5BController(Lazy<HXN5BViewModel> viewModel, IEventAggregator eventAggregator, IRegionManager regionManager, IStateInterfaceFactory stateInterfaceFactory) : base(viewModel)
        {
            m_CurrentRootStateKey = StateInterfaceKey.Parser(StateKeys.Root);
            m_EventAggregator = eventAggregator;
            m_RegionManager = regionManager;
            StateInterfaceFactory = stateInterfaceFactory;
            m_NavigateHistory = new Stack<StateInterfaceKey>();
            LoadedCommand = new DelegateCommand<CommandParameter>(OnLoaded);
        }

        private void OnLoaded(CommandParameter obj)
        {
            NavigateToRoot();
        }

        public void NavigateToRoot()
        {
            NavigateTo(StateKeys.Root);
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

        public void Reset()
        {
            NavigateToRoot();
            var ws = GlobalParam.Instance.InitParam.CommunicationDataService.WritableReadService;
            ws.ChangedInBoolOf(InBKeys.MMI亮屏, false);
            ViewModel.Value.Model.IsVisble = false;
        }
    }
}