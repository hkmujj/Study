using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.TCMS.LanZhou.Constant;
using Subway.TCMS.LanZhou.Event;
using Subway.TCMS.LanZhou.Model.BtnStragy;
using Subway.TCMS.LanZhou.Resources.Keys;
using Subway.TCMS.LanZhou.Resources.Strings;
using Subway.TCMS.LanZhou.View.Contents;
using Subway.TCMS.LanZhou.View.Contents.BreakDownInformation;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Controller
{
    [Export]
    public class DomainController : ControllerBase<DomainViewModel>
    {
        public IStateInterfaceFactory StateInterfaceFactory { private set; get; }

        //public ICommand NavigateToCommand { get; private set; }

        private StateInterfaceKey m_CurrentRootStateKey;

        private readonly IRegionManager m_RegionManager;

        private readonly IEventAggregator m_EventAggregator;

        private readonly Stack<StateInterfaceKey> m_NavigateHistory;

        public DelegateCommand LoadedCommand { get; private set; }

        [ImportingConstructor]
        public DomainController(IEventAggregator eventAggregator, IRegionManager regionManager, IStateInterfaceFactory stateInterfaceFactory) 
        {

            m_CurrentRootStateKey = StateInterfaceKey.Parser(StateKeys.Root);
            m_EventAggregator = eventAggregator;
            m_RegionManager = regionManager;
            StateInterfaceFactory = stateInterfaceFactory;
            m_NavigateHistory = new Stack<StateInterfaceKey>();
            LoadedCommand = new DelegateCommand(Initalize);
            ViewChangedEvent.Subscribe(OnViewChangedEvent);


        }

        private void OnViewChangedEvent(ViewChangedEvent.Args args)
        {
            ViewModel.Model.CurrentContentViewType = args.TargetViewType;

            ViewModel.Model.CurrentStateInterface.UpdateState();
        }

        /// <summary>
        /// Initalize
        /// </summary>
        public override void Initalize()
        {
            NavigateTo(StateInterfaceKeys.Root);

            ViewModel.Model.CurrentStateInterface.BtnB1.StateProvider.IsSelected = true;

            ViewChangedEvent.Publish(new ViewChangedEvent.Args(typeof(RunningView)));

            ViewModel.Model.TrainFaultPageReturnCommand = new DelegateCommand(() =>
            {
                NavigateTo(StateInterfaceKeys.Root_全部故障);

                ViewModel.Model.CurrentStateInterface.CurrentSelectedBtn = BtnType.B4;

                var target = typeof(BreakDownInformationList);

                m_EventAggregator.GetEvent<ViewChangedEvent>().Publish(new ViewChangedEvent.Args(target));
                m_RegionManager.RequestNavigate(RegionNames.ContentContent, target.FullName);
            });
        }

        private ViewChangedEvent ViewChangedEvent
        {
            get { return m_EventAggregator.GetEvent<ViewChangedEvent>(); }
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
    }
}