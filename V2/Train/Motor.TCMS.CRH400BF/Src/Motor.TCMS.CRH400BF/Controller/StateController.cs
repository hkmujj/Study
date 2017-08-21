using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using DevExpress.Mvvm;
using Motor.TCMS.CRH400BF.Model.BtnStragy;
using Motor.TCMS.CRH400BF.Resources.Keys;
using Motor.TCMS.CRH400BF.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.TCMS.CRH400BF.Model;

namespace Motor.TCMS.CRH400BF.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class StateController : ControllerBase<StateViewModel>
    {
        public IStateInterfaceFactory StateInterfaceFactory { private set; get; }

        private StateInterfaceKey m_CurrentRootStateKey;

        private readonly IEventAggregator m_EventAggregator;

        private readonly Stack<StateInterfaceKey> m_NavigateHistory;

        public DelegateCommand<CommandParameter> LoadedCommand { private set; get; }

        [ImportingConstructor]
        public StateController(IEventAggregator eventAggregator, IRegionManager regionManager, IStateInterfaceFactory stateInterfaceFactory)
        {
            m_CurrentRootStateKey = StateInterfaceKey.Parser(StateKeys.Root);
            m_EventAggregator = eventAggregator;
            StateInterfaceFactory = stateInterfaceFactory;
            m_NavigateHistory = new Stack<StateInterfaceKey>();
            LoadedCommand = new DelegateCommand<CommandParameter>(OnLoaded);

        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsVisble")
            {
                if (ViewModel.DomainViewModel.Model.IsVisble && ViewModel.DomainViewModel.ViewLocation == ViewLocation.Reserve)
                {
                    NavigateTo(StateKeys.Root_制动界面);
                }

            }
        }

        /// <summary>Initalize</summary>
        private void OnLoaded(CommandParameter obj)
        {
            NavigateToRoot();
        }
        public override void Initalize()
        {
            NavigateToRoot();
            ViewModel.DomainViewModel.Model.PropertyChanged += Model_PropertyChanged;
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
            ViewModel.Model.UpdateCurrentState(StateInterfaceFactory.GetOrCreate(key), ViewModel.DomainViewModel);
        }
    }
}