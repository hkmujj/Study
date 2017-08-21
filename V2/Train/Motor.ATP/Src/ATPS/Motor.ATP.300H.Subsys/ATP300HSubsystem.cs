using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using CommonUtil.Controls;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using MMI.Facility.WPFInfrastructure.Extension;
using Motor.ATP.DataAdapter;
using Motor.ATP.DataAdapter.Resource.Keys;
using Motor.ATP.Infrasturcture.Control.Infomation;
using Motor.ATP.Infrasturcture.Control.Service;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Events;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.Service;
using Motor.ATP.Infrasturcture.Model.UserAction;
using Motor.ATP.Infrasturcture.Monitor;
using Motor.ATP.Infrasturcture.ViewModel;
using Motor.ATP._300H.Subsys.Control;
using Motor.ATP._300H.Subsys.Model;
using Motor.ATP._300H.Subsys.Service;
using Motor.ATP._300H.Subsys.WPFView.Shell;

namespace Motor.ATP._300H.Subsys
{
    [SubsystemExport(typeof(ATP300HSubsystem))]
    public class ATP300HSubsystem : ISubsystem
    {
        private ATP300HDriverInterfaceController m_DriverController;

        private ATP300HDomainModel m_ATP300HDomain;

        private SubsystemInitParam m_InitParam;

        private DispatcherTimer m_UpdateNowTimer;

        private readonly Action<DataAdapterBase> m_ClearDataOnCoursrStopAction = adpt => { adpt.ClearDataOnCourseStop(); };

        private PushOperatorToUIThreadEvent m_PushOperatorToUIThreadEvent;
        private MonitorViewModel m_MonitorViewModel;

        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            AppLog.Info(" ----------------- ATP300SSubsystem started! -----------------");
            m_InitParam = initParam;
            var serviceManager = initParam.DataPackage.ServiceManager;

            var eventService = new DriverInputEventService();
            serviceManager.RegistService<INotifyableDriverInputEventService>(ATPType.ATP300H.ToString(), eventService);
            serviceManager.RegistService<IDriverInputEventService>(ATPType.ATP300H.ToString(), eventService);

            serviceManager.RegistService<IEventAggregatorProvider>(EventAggregatorProvider.Instance);

            var interfaceService = new InterfaceAdapterService();
            interfaceService.Initalize(
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig)));
            serviceManager.RegistService<IInterfaceAdapterService>(ATPType.ATP300H.ToString(), interfaceService);

            m_ATP300HDomain = new ATP300HDomainModel(initParam);
            m_ATP300HDomain.PropertyChanged += DomainModelOnPropertyChanged;

            var infomationService = new InfomationService(m_ATP300HDomain,
                new InfomationCreater("ATP300H消息通知表-E和F区.xls", GlobalParam.Instance));
            serviceManager.RegistService<IInfomationService>(ATPType.ATP300H.ToString(), infomationService);
            infomationService.Initalize();

            var appName = initParam.AppConfig.AppName;
            var opaqueLayer = new OpaqueLayerService
            {
                //BackColor = ATP300SGdiResoures.BackgrourdColor,
                Alpha = 0
            };
            serviceManager.RegistService<IOpaqueLayerService>(ATPType.ATP300H.ToString(), opaqueLayer);
            //var mainModel = new ATP300SMainViewModel() {SpeedDialPlate = new ATP300SSpeedDialPlate()};
            var pvs = ServiceLocator.Current.GetInstance<WPFPopupViewService>();
            serviceManager.RegistService<IPopupViewService>(ATPType.ATP300H.ToString(), pvs);
            var viewService = serviceManager.GetService<IViewService>();
            //viewService.Regist(appName, m_View);
            viewService.Regist(appName, new ShellForm(initParam, m_ATP300HDomain));

            //viewService.Active(appName);

            var factory = new ATP300HDriverInterfaceFactory(m_ATP300HDomain);

            serviceManager.RegistService<IDriverInterfaceFactory>(ATPType.ATP300H.ToString(), factory);


            m_DriverController = new ATP300HDriverInterfaceController(m_ATP300HDomain, factory,
                null);
            serviceManager.RegistService<IDriverInterfaceController>(ATPType.ATP300H.ToString(), m_DriverController);


            m_ATP300HDomain.Initalize(new ScreenIdentity("ATP300H", new Rect(), new RegionModel()));
            m_ATP300HDomain.ATPHardwareButton.HardwareButtonViewModel.ButtonEvent +=
                HardwareButtonViewModelOnButtonEvent;

            m_PushOperatorToUIThreadEvent = ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<PushOperatorToUIThreadEvent>();
            m_PushOperatorToUIThreadEvent.Subscribe(OnPushOperatorToUIThreadEvent, ThreadOption.UIThread, true,
                args => args.ScreenIdentity == m_ATP300HDomain.Identity);

            foreach (var hardwareButton in m_ATP300HDomain.ATPHardwareButton.HardwareButtonCollection)
            {
                hardwareButton.PropertyChanged += HardwareButtonOnPropertyChanged;
            }

            m_DriverController.UpdateDriverInterface(factory.GetOrCreateDriverInterface(DriverInterfaceKey.Root));

            var dataAdptFacotyr = new DataAdapterFactory();
            var dataAdpt = dataAdptFacotyr.CreateDataAdapter(m_ATP300HDomain);

            var dataService = initParam.CommunicationDataService;

            initParam.DataPackage.ServiceManager.GetService<IDataChangeListenService>()
                .RegistListener(dataAdpt, initParam.AppConfig);

            dataService.ReadService.RaiseAllDataChanged();

            var coursService = serviceManager.GetService<ICourseService>();
            coursService.CourseStateChanged += (sender, args) =>
            {
                if (args.CourseService.CurrentCourseState == CourseState.Stoped)
                {
                    m_PushOperatorToUIThreadEvent.Publish(
                        new PushOperatorToUIThreadEvent.Args(m_ClearDataOnCoursrStopAction, m_ATP300HDomain.Identity,
                            dataAdpt));
                }
            };
            m_MonitorViewModel = new MonitorViewModel(m_ATP300HDomain);
            var domainMonitor = new DoaminMonitorMain(m_MonitorViewModel);
            var debugViewService = serviceManager.GetService<IDebugViewService>();
            domainMonitor.Top = 10;
            domainMonitor.Left = 200;
            debugViewService.DebugFormCollection.Add(domainMonitor);

            InitalizeValuesWhenDebug();

            AppLog.Info(" ================== ATP300SSubsystem completed! ==================");
        }

        private void OnPushOperatorToUIThreadEvent(PushOperatorToUIThreadEvent.Args args)
        {
            args.Operate.DynamicInvoke(args.Params);
        }

        private void HardwareButtonViewModelOnButtonEvent(UserActionType userActionType,
            MouseButtonState mouseButtonState)
        {
            var item = m_DriverController.CurrentInterface.DriverSelectable.SelectableItems.First(
                f => (UserActionType) f.UserActionType == userActionType);
            if (!item.Enabled || !item.Visible)
            {
                return;
            }

            switch (mouseButtonState)
            {
                case MouseButtonState.Pressed:
                    item.ActionResponser.ResponseMouseDown();
                    break;
                case MouseButtonState.Released:
                    item.ActionResponser.ResponseMouseUp();
                    item.ActionResponser.ResponseMouseClick();

                    ServiceLocator.Current.GetInstance<IEventAggregator>()
                        .GetEvent<ButtonResponseCompletedEvent>()
                        .PublishLater(new ButtonResponseCompletedEvent.Args(ATPType.ATP300H));

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DomainModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
        }


        private void HardwareButtonOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName ==
                PropertySupport.ExtractPropertyName<IHardwareButton, MouseState>(a => a.MouseState))
            {
                var btn = (IHardwareButton) sender;
                var item = m_DriverController.CurrentInterface.DriverSelectable.SelectableItems.First(
                    f => (UserActionType) f.UserActionType == btn.Type);

                if (!item.Enabled || !item.Visible)
                {
                    return;
                }

                switch (btn.MouseState)
                {
                    case MouseState.MouseDown:
                        item.ActionResponser.ResponseMouseDown();
                        break;
                    case MouseState.MouseUp:
                        item.ActionResponser.ResponseMouseUp();
                        item.ActionResponser.ResponseMouseClick();

                            ServiceLocator.Current.GetInstance<IEventAggregator>()
                                .GetEvent<ButtonResponseCompletedEvent>()
                                .PublishLater(new ButtonResponseCompletedEvent.Args(ATPType.ATP300H));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void InitalizeValuesWhenDebug()
        {
            if (m_InitParam.DataPackage.Config.SystemConfig.StartModel != StartModel.Edit)
            {
                return;
            }

            m_UpdateNowTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(1000), DispatcherPriority.Background,
                (sender, args) => m_ATP300HDomain.Other.NowInATP = DateTime.Now, Dispatcher.CurrentDispatcher);
            m_UpdateNowTimer.Start();

            var idxs =
                m_InitParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(m_InitParam.AppConfig));


            m_InitParam.CommunicationDataService.WritableReadService.ChangeBool(
                idxs.InBoolDescriptionDictionary[InBoolKeys.车载设备自检状态], true);

            m_ATP300HDomain.DriverInterfaceController.UpdateDriverInterface(
                DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_输入电话号码));
        }
    }
}
