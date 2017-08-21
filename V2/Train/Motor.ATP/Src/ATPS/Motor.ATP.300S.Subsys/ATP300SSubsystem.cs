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
using Motor.ATP.Infrasturcture.Model.Service;
using Motor.ATP.Infrasturcture.Model.UserAction;
using Motor.ATP.Infrasturcture.Monitor;
using Motor.ATP.Infrasturcture.ViewModel;
using Motor.ATP._300S.Subsys.Control;
using Motor.ATP._300S.Subsys.Model;
using Motor.ATP._300S.Subsys.Service;
using Motor.ATP._300S.Subsys.WPFView.Shell;

namespace Motor.ATP._300S.Subsys
{

    [SubsystemExport(typeof(ATP300SSubsystem))]
    public class ATP300SSubsystem : ISubsystem
    {
        private ATP300SDriverInterfaceController m_DriverController;

        private ATP300SDomainModel m_ATP300SDomainModel;

        private MonitorViewModel m_MonitorViewModel;

        private SubsystemInitParam m_InitParam;

        private DispatcherTimer m_UpdateNowTimer;

        private Action<DataAdapterBase> ClearDataOnCoursrStopAction = adpt => { adpt.ClearDataOnCourseStop(); };

        private PushOperatorToUIThreadEvent m_PushOperatorToUIThreadEvent;

        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            AppLog.Info(" ----------------- ATP300SSubsystem started! -----------------");
            m_InitParam = initParam;

            var serviceManager = initParam.DataPackage.ServiceManager;

            var eventService = new DriverInputEventService();
            serviceManager.RegistService<INotifyableDriverInputEventService>(ATPType.ATP300S.ToString(), eventService);
            serviceManager.RegistService<IDriverInputEventService>(ATPType.ATP300S.ToString(), eventService);
            serviceManager.RegistService<IEventAggregatorProvider>(EventAggregatorProvider.Instance);

            var interfaceService = new InterfaceAdapterService();
            interfaceService.Initalize(
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig)));
            serviceManager.RegistService<IInterfaceAdapterService>(ATPType.ATP300S.ToString(), interfaceService);

            m_ATP300SDomainModel = new ATP300SDomainModel(initParam);
            m_ATP300SDomainModel.PropertyChanged += DomainModelModelOnPropertyChanged;

            m_PushOperatorToUIThreadEvent = ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<PushOperatorToUIThreadEvent>();

            m_PushOperatorToUIThreadEvent.Subscribe(OnPushOperatorToUIThreadEvent, ThreadOption.UIThread, true,
                    args => args.ScreenIdentity == m_ATP300SDomainModel.Identity);

            var infomationService = new InfomationService(m_ATP300SDomainModel,
                new InfomationCreater("ATP300S消息通知表-E和F区.xls", GlobalParam.Instance));
            serviceManager.RegistService<IInfomationService>(ATPType.ATP300S.ToString(), infomationService);
            infomationService.Initalize();

            var appName = initParam.AppConfig.AppName;
            var opaqueLayer = new OpaqueLayerService
            {
                Alpha = 0
            };
            serviceManager.RegistService<IOpaqueLayerService>(ATPType.ATP300S.ToString(), opaqueLayer);
            var mainModel = new ATP300SMainViewModel() {SpeedDialPlate = new ATP300SSpeedDialPlate()};

            var pvs = ServiceLocator.Current.GetInstance<WPFPopupViewService>();
            serviceManager.RegistService<IPopupViewService>(ATPType.ATP300S.ToString(), pvs);
            var viewService = serviceManager.GetService<IViewService>();

            viewService.Regist(appName, new ShellForm(initParam, m_ATP300SDomainModel));

            var factory = new ATP300SDriverInterfaceFactory(m_ATP300SDomainModel);

            serviceManager.RegistService<IDriverInterfaceFactory>(ATPType.ATP300S.ToString(), factory);


            m_DriverController = new ATP300SDriverInterfaceController(m_ATP300SDomainModel, factory, null);
            serviceManager.RegistService<IDriverInterfaceController>(ATPType.ATP300S.ToString(), m_DriverController);


            m_ATP300SDomainModel.Initalize(new ScreenIdentity("ATP300S", new Rect(), new RegionModel()));
            m_ATP300SDomainModel.ATPHardwareButton.HardwareButtonViewModel.ButtonEvent +=
                HardwareButtonViewModelOnButtonEvent;
            foreach (var hardwareButton in m_ATP300SDomainModel.ATPHardwareButton.HardwareButtonCollection)
            {
                hardwareButton.PropertyChanged += HardwareButtonOnPropertyChanged;
            }

            m_DriverController.UpdateDriverInterface(factory.GetOrCreateDriverInterface(DriverInterfaceKey.Root));

            var dataAdptFacotyr = new DataAdapterFactory();
            var dataAdpt = dataAdptFacotyr.CreateDataAdapter(m_ATP300SDomainModel);

            var dataService = initParam.CommunicationDataService;

            initParam.DataPackage.ServiceManager.GetService<IDataChangeListenService>()
                .RegistListener(dataAdpt, initParam.AppConfig);

            dataService.ReadService.RaiseAllDataChanged();

            var coursService = serviceManager.GetService<ICourseService>();
            coursService.CourseStateChanged += (sender, args) =>
            {
                if (args.CourseService.CurrentCourseState == CourseState.Stoped)
                {
                    m_PushOperatorToUIThreadEvent.Publish(new PushOperatorToUIThreadEvent.Args(ClearDataOnCoursrStopAction, m_ATP300SDomainModel.Identity, dataAdpt));
                }
            };

            m_MonitorViewModel = new MonitorViewModel(m_ATP300SDomainModel);

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
                        .PublishLater(new ButtonResponseCompletedEvent.Args(ATPType.ATP300S));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DomainModelModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
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
                            .PublishLater(new ButtonResponseCompletedEvent.Args(ATPType.ATP300S));
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

            m_UpdateNowTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 1)};
            m_UpdateNowTimer.Tick += (sender, args) => m_ATP300SDomainModel.Other.NowInATP = DateTime.Now;
            m_UpdateNowTimer.Start();

            var idxs =
                m_InitParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(m_InitParam.AppConfig));


            m_InitParam.CommunicationDataService.WritableReadService.ChangeBool(
                idxs.InBoolDescriptionDictionary[InBoolKeys.车载设备自检状态], true);
        }
    }
}
