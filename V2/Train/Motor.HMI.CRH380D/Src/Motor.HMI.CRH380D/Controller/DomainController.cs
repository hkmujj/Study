using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380D.Event;
using Motor.HMI.CRH380D.Models;
using Motor.HMI.CRH380D.Models.BtnStragy;
using Motor.HMI.CRH380D.Resources.Keys;
using Motor.HMI.CRH380D.ViewModel;

namespace Motor.HMI.CRH380D.Controller
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

        public AnsyncNavigateViewByKeyEvent AnsyncNavigateViewKeyEvent { get; private set; }

        [ImportingConstructor]
        public DomainController(Lazy<DomainViewModel> viewModel, IEventAggregator eventAggregator,
            IRegionManager regionManager, IStateInterfaceFactory stateInterfaceFactory) : base(viewModel)
        {
            m_CurrentRootStateKey = StateInterfaceKey.Parser(StateKeys.Root_主菜单界面按键);
            m_EventAggregator = eventAggregator;
            m_RegionManager = regionManager;
            StateInterfaceFactory = stateInterfaceFactory;
            m_NavigateHistory = new Stack<StateInterfaceKey>();
            AnsyncNavigateViewKeyEvent = m_EventAggregator.GetEvent<AnsyncNavigateViewByKeyEvent>();

            AnsyncNavigateViewKeyEvent.Subscribe(s => NavigateTo(s.StateKey), ThreadOption.UIThread, true);
        }

        public override void Initalize()
        {
            NavigateToRoot();

            //ViewModel.Value.Model.PropertyChanged += (sender, args) =>
            //{
            //    if (args.PropertyName == PropertySupport.ExtractPropertyName<DomainModel, bool>(s => s.IsStart))
            //    {
            //        if (ViewModel.Value.Model.IsStart)
            //        {
            //            Task.Factory.StartNew(s =>
            //            {
            //                Thread.Sleep(TimeSpan.FromSeconds(5));

            //                AnsyncNavigateViewKeyEvent.Publish(new AnsyncNavigateViewByKeyEvent.Args(StateKeys.Root_主界面按键));

            //                AnsyncNavigateViewKeyEvent.Publish(new AnsyncNavigateViewByKeyEvent.Args(StateKeys.Root_运行界面按键));

            //            }, null);
            //        }
            //        else
            //        {

            //            AnsyncNavigateViewKeyEvent.Publish(new AnsyncNavigateViewByKeyEvent.Args(StateKeys.Root_启动界面按键));
            //        }
            //    }
            //};
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
            var StateInterface = StateInterfaceFactory.GetOrCreate(key,
                GlobalParam.Instance.StateInterfaceCollection);

            //if (!StateInterface.ContentViewTitle.IsNullOrWhiteSpace())
            //{
            //    ViewModel.Value.TitleViewModel.Model.CurViewTitle = StateInterface.ContentViewTitle;
            //}

            ViewModel.Value.Model.UpdateState(StateInterface);
        }

        public void NavigateToRoot()
        {
            NavigateTo(StateKeys.Root_主菜单界面按键);
        }
    }
}