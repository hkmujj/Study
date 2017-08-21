using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TAX2.SS7C.Model.BtnStragy;
using Engine.TAX2.SS7C.Resources.Keys;
using Engine.TAX2.SS7C.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TAX2.SS7C.Controller
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

        [ImportingConstructor]
        public DomainController(Lazy<DomainViewModel> viewModel, IEventAggregator eventAggregator, IRegionManager regionManager, IStateInterfaceFactory stateInterfaceFactory) : base(viewModel)
        {
            m_CurrentRootStateKey = StateInterfaceKey.Parser(StateKeys.Root);
            m_EventAggregator = eventAggregator;
            m_RegionManager = regionManager;
            StateInterfaceFactory = stateInterfaceFactory;
            m_NavigateHistory = new Stack<StateInterfaceKey>();
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