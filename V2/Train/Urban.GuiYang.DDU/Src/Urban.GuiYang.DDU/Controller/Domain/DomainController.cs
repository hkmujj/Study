using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.ActionResponser;
using Urban.GuiYang.DDU.Events;
using Urban.GuiYang.DDU.Model.BtnStragy;
using Urban.GuiYang.DDU.Resources.Keys;
using Urban.GuiYang.DDU.View.Contents.Contents;
using Urban.GuiYang.DDU.View.Contents.Help;
using Urban.GuiYang.DDU.ViewModel;

namespace Urban.GuiYang.DDU.Controller.Domain
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
        public ICommand GoToBypass { get; private set; }
        public ICommand GoToLast { get; private set; }
        public ICommand CloseHelp { get; private set; }
        public ICommand GoToHelpResponse { get; private set; }
        public ICommand GoToHelp { get; private set; }
        public ICommand GoToFault { get; private set; }
        [Import]
        public Lazy<HelpResponse> HelpResponse { get; private set; }
        [Import]
        public Lazy<BypassResponse> BypassResponse { get; private set; }
        [Import]
        public Lazy<FaultResponse> FaultResponse { get; private set; }
        
        [ImportingConstructor]
        public DomainController(IEventAggregator eventAggregator, IRegionManager regionManager, IStateInterfaceFactory stateInterfaceFactory)
        {
            m_CurrentRootStateKey = StateInterfaceKey.Parser(StateKeys.Root);
            m_EventAggregator = eventAggregator;
            m_RegionManager = regionManager;
            StateInterfaceFactory = stateInterfaceFactory;
            m_NavigateHistory = new Stack<StateInterfaceKey>();

            m_EventAggregator.GetEvent<ChangeToInitalizeViewEvent>().Subscribe(s =>
            {
                NavigateTo(StateInterfaceKey.Root);
                ViewModel.Model.CurrentStateInterface.BtnB1.StateProvider.IsSelected = true;
                ViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser.ResponseClick();
            });
            GoToBypass = new DelegateCommand<string>(NavigatorToView);
            GoToLast = new DelegateCommand(() =>
            {
                var sd = ViewModel.Model.CurrentStateInterface.BtnB1.ActionResponser as OrdinaryActionResponser;
                if (sd != null)
                {
                    sd.GoToLast();
                }
            });
            GoToHelp = new DelegateCommand<string>((fullName) =>
            {
                var type = Type.GetType(fullName).GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault() as ViewExportAttribute;
                if (type != null)
                {
                    var region = type.RegionName;
                    m_RegionManager.RequestNavigate(region, fullName);
                }
            });
            CloseHelp = new DelegateCommand(() =>
            {
                m_RegionManager.RequestNavigate(RegionNames.HelpRegion, typeof(HelpNullView).FullName);
            });

            GoToFault = new DelegateCommand(() => { FaultResponse.Value.ResponseClick(); });

            GoToHelpResponse = new DelegateCommand(() => { HelpResponse.Value.GoToHelp(); });
        }

        public void NavigatorToView(string fullName)
        {
            if (fullName == typeof(MainPageByPass1ContentView).FullName)
            {
                BypassResponse.Value.GoToBypass1();
            }
            else
            {
                BypassResponse.Value.GoToBypass2();
            }
            CloseHelp.Execute(null);
        }
        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.LoadedCommand = new DevExpress.Mvvm.DelegateCommand<CommandParameter>(OnLoaded);
            ViewModel.Model.PropertyChanged += ModelOnPropertyChanged;
        }

        private void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Visible")
            {
                if (ViewModel.Model.Visible)
                {
                    m_EventAggregator.GetEvent<ChangeToInitalizeViewEvent>().Publish(new ChangeToInitalizeViewEvent.Args());
                }
            }
        }

        private void OnLoaded(CommandParameter commandParameter)
        {
            ViewModel.DebuggerViewModel.Initalize();
            m_EventAggregator.GetEvent<ChangeToInitalizeViewEvent>().Publish(new ChangeToInitalizeViewEvent.Args());
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
            CloseHelp.Execute(null);
        }
    }
}