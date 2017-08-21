using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using CommonUtil.Util;
using LightRail.HMI.GZYGDC.Event;
using LightRail.HMI.GZYGDC.Model;
using LightRail.HMI.GZYGDC.Model.BtnStragy;
using LightRail.HMI.GZYGDC.Resources.Keys;
using LightRail.HMI.GZYGDC.ViewModel;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace LightRail.HMI.GZYGDC.Controller
{
    [Export]
    public class DomainController : ControllerBase<Lazy<DomainViewModel>>
    {
        public IStateInterfaceFactory StateInterfaceFactory { private set; get; }

        //public ICommand NavigateToCommand { get; private set; }
        
        private StateInterfaceKey m_CurrentRootStateKey;

        private readonly IRegionManager m_RegionManager;

        private readonly IEventAggregator m_EventAggregator;

        private readonly Stack<StateInterfaceKey> m_NavigateCenterHistory;

        private readonly Stack<StateInterfaceKey> m_NavigateBottomHistory;

        public AnsyncNavigateViewByKeyEvent AnsyncNavigateViewKeyEvent { get; private set; }

        //更新最新时间定时器
        private /*readonly*/ DispatcherTimer m_GetNowTimeTimer;

        [ImportingConstructor]
        public DomainController(Lazy<DomainViewModel> viewModel, IEventAggregator eventAggregator, IRegionManager regionManager, IStateInterfaceFactory stateInterfaceFactory) : base(viewModel)
        {
            m_CurrentRootStateKey = StateInterfaceKey.Parser(StateKeys.Root_主界面底部按键);
            m_EventAggregator = eventAggregator;
            m_RegionManager = regionManager;
            StateInterfaceFactory = stateInterfaceFactory;
            m_NavigateCenterHistory = new Stack<StateInterfaceKey>();
            m_NavigateBottomHistory = new Stack<StateInterfaceKey>();

            AnsyncNavigateViewKeyEvent = m_EventAggregator.GetEvent<AnsyncNavigateViewByKeyEvent>();

            AnsyncNavigateViewKeyEvent.Subscribe(s => NavigateCenterTo(s.StateKey), ThreadOption.UIThread, true);
        }


        public override void Initalize()
        {
            m_GetNowTimeTimer = new DispatcherTimer();

            m_GetNowTimeTimer.Tick += (sender, args) =>
            {
                if (ViewModel.Value.Model != null)
                {
                    ViewModel.Value.Model.CurrentTime = DateTime.Now;
                }
            };

            m_GetNowTimeTimer.Interval = new TimeSpan(0,0,1);
            m_GetNowTimeTimer.Start();

            NavigateCenterToRoot();
            NavigateBottomToRoot();

            ViewModel.Value.Model.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == PropertySupport.ExtractPropertyName<DomainModel, bool>(s => s.IsStart))
                {
                    if (ViewModel.Value.Model.IsStart)
                    {
                        Task.Factory.StartNew(s =>
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(5));

                                AnsyncNavigateViewKeyEvent.Publish(new AnsyncNavigateViewByKeyEvent.Args(StateKeys.Root_主界面按键));

                                AnsyncNavigateViewKeyEvent.Publish(new AnsyncNavigateViewByKeyEvent.Args(StateKeys.Root_运行界面按键));

                            }, null);
                    }
                    else
                    {

                        AnsyncNavigateViewKeyEvent.Publish(new AnsyncNavigateViewByKeyEvent.Args(StateKeys.Root_启动界面按键));
                    }
                }
            };
        }

        public void NavigateCenterTo(string stateKey)
        {
            var key = StateInterfaceKey.Parser(stateKey);
            if (key.Paths.Count == 1)
            {
                m_CurrentRootStateKey = key;
                m_NavigateCenterHistory.Clear();
                m_NavigateCenterHistory.Push(key);
            }
            else if (m_NavigateCenterHistory.Any())
            {
                var last = m_NavigateCenterHistory.Peek();
                if (last != key.Parent && key.Parent.Paths.Count > 1)
                {
                    m_NavigateCenterHistory.Push(key.Parent);
                }
            }
            else
            {
                m_NavigateCenterHistory.Push(m_CurrentRootStateKey);
            }

            NavigateCenterTo(key);
        }

        public void NavigateCenterBack()
        {
            if (m_NavigateCenterHistory.Any())
            {
                NavigateCenterTo(m_NavigateCenterHistory.Pop());
            }
        }

        private void NavigateCenterTo(StateInterfaceKey key)
        {
            var StateInterface = StateInterfaceFactory.GetOrCreate(key,
                GlobalParam.Instance.CenterStateInterfaceCollection);

            if (!StateInterface.ContentViewTitle.IsNullOrWhiteSpace())
            {
                ViewModel.Value.TitleViewModel.Model.CurViewTitle = StateInterface.ContentViewTitle;
            }

            ViewModel.Value.Model.UpdateCenterState(StateInterface);
        }

        public void NavigateCenterToRoot()
        {
            NavigateCenterTo(StateKeys.Root_运行界面按键);
        }

        public void NavigateBottomTo(string stateKey)
        {
            var key = StateInterfaceKey.Parser(stateKey);
            if (key.Paths.Count == 1)
            {
                m_CurrentRootStateKey = key;
                m_NavigateBottomHistory.Clear();
                m_NavigateBottomHistory.Push(key);
            }
            else if (m_NavigateBottomHistory.Any())
            {
                var last = m_NavigateBottomHistory.Peek();
                if (last != key.Parent && key.Parent.Paths.Count > 1)
                {
                    m_NavigateBottomHistory.Push(key.Parent);
                }
            }
            else
            {
                m_NavigateBottomHistory.Push(m_CurrentRootStateKey);
            }

            NavigateBottomTo(key);
        }

        public void NavigateBottomBack()
        {
            if (m_NavigateBottomHistory.Any())
            {
                NavigateBottomTo(m_NavigateBottomHistory.Pop());
            }
        }

        private void NavigateBottomTo(StateInterfaceKey key)
        {
            ViewModel.Value.Model.UpdateBottomState(StateInterfaceFactory.GetOrCreate(key, GlobalParam.Instance.BottomStateInterfaceCollection));
        }

        public void NavigateBottomToRoot()
        {
            NavigateBottomTo(StateKeys.Root_主界面底部按键);
        }
    }
}