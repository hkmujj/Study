using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.Model.BtnStragy;
using Engine.TCMS.HXD3.Model.Interface;
using Engine.TCMS.HXD3.Resource.Keys;
using Engine.TCMS.HXD3.ViewModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TCMS.HXD3.Controller
{
    [Export]
    public class HXD3TCMSController : ControllerBase<HXD3TCMSViewModel>
    {
        [Import]
        public IStateInterfaceFactory StateInterfaceFactory { private set; get; }

        public IDataSender DataSender { set; get; }

        public DelegateCommand<CommandParameter> LoadedCommand { private set; get; }

        public ICommand NavigateToCommand { get; private set; }

        private StateInterfaceKey m_CurrentRootStateKey;

        private readonly IRegionManager m_RegionManager;

        private readonly IEventAggregator m_EventAggregator;

        private readonly Stack<StateInterfaceKey> m_NavigateHistory;

        [ImportingConstructor]
        public HXD3TCMSController(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            m_CurrentRootStateKey = StateInterfaceKey.Parser(StateKeys.Root2);
            m_RegionManager = regionManager;
            m_EventAggregator = eventAggregator;
            m_NavigateHistory = new Stack<StateInterfaceKey>();
            LoadedCommand = new DelegateCommand<CommandParameter>(OnLoaded);
            NavigateToCommand = new DelegateCommand<string>(NavigateTo);

            m_EventAggregator.GetEvent<CompositePresentationEvent<CourseState>>().Subscribe(s =>
            {
                if (s == CourseState.Started)
                {
                    NavigateToRoot();
                }
            }, ThreadOption.UIThread);

            if (GlobalParam.Instance.InitParam != null)
            {
                GlobalParam.Instance.InitParam.DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged += OnCourseStateChanged;
            }
        }

        private void OnCourseStateChanged(object sender, CourseStateChangedArgs courseStateChangedArgs)
        {
            if (courseStateChangedArgs.CourseService.CurrentCourseState == CourseState.Started)
            {
                m_EventAggregator.GetEvent<CompositePresentationEvent<CourseState>>().Publish(CourseState.Started);
            }
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

        public void NavigateToRoot()
        {
            NavigateTo(m_CurrentRootStateKey);
        }

        private void OnLoaded(CommandParameter obj)
        {
            NavigateTo(StateKeys.Root2);
        }

    }
}