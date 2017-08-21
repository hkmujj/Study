using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Tram.CBTC.DataAdapter;
using Tram.CBTC.Infrasturcture.Events;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Monitor;
using Tram.CBTC.Infrasturcture.Service;
using Tram.CBTC.Infrasturcture.ViewModel.Monitor;
using Tram.CBTC.NRIET.Adapter;
using Tram.CBTC.NRIET.Model;
using Tram.CBTC.NRIET.View.Shell;
using Tram.CBTC.NRIET.ViewModel;

namespace Tram.CBTC.NRIET
{
    [SubsystemExport(typeof(CBTCNrietSubystem))]
    public class CBTCNrietSubystem : ISubsystem
    {
        private DomainAdapter m_DomainAdapter;

        private readonly Action<DataAdapterBase> m_ClearDataOnCoursrStopAction = adpt => { adpt.ClearDataOnCourseStop(); };

        private PushOperatorToUIThreadEvent m_PushOperatorToUIThreadEvent;

        private IEventAggregator m_EventAggregator;

        [ImportingConstructor]
        public CBTCNrietSubystem(IEventAggregator eventAggregator)
        {
               m_EventAggregator = eventAggregator;
            m_PushOperatorToUIThreadEvent = eventAggregator.GetEvent<PushOperatorToUIThreadEvent>();
        }

        /// <summary>
        /// </summary>
        /// <param name="initParam"></param>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParam.Instance.Initalize(initParam);

            InitalizeServices(initParam);

            var serviceManager = initParam.DataPackage.ServiceManager;

            var form = new NrietForm();

            var viewService = serviceManager.GetService<IViewService>();
            viewService.Regist(initParam.AppConfig.AppName, form);

            var domainVm = ServiceLocator.Current.GetInstance<DomainViewModel>();
            var adapter =
                DataAdapterFactory.Instance.CreateDataAdapter(domainVm.CBTC);
            initParam.DataPackage.ServiceManager.GetService<IDataChangeListenService>()
                .RegistListener(adapter, initParam.AppConfig);

            m_PushOperatorToUIThreadEvent.Subscribe(OnPushOperatorToUIThreadEvent, ThreadOption.UIThread, true,
                args => args.ScreenIdentity == domainVm.CBTC.Identity);

            var coursService = serviceManager.GetService<ICourseService>();
            coursService.CourseStateChanged += (sender, args) =>
            {
                if (args.CourseService.CurrentCourseState == CourseState.Stoped)
                {
                    m_PushOperatorToUIThreadEvent.Publish(
                        new PushOperatorToUIThreadEvent.Args(m_ClearDataOnCoursrStopAction, domainVm.CBTC.Identity,
                            adapter));
                }
            };

            if (initParam.DataPackage.Config.SystemConfig.IsDebugModel)
            {
                var domainMonitor =
                    new DomainMonitor(
                        new MonitorViewModel<DomainViewModel>(domainVm));
                var debugViewService = serviceManager.GetService<IDebugViewService>();
                domainMonitor.Top = 10;
                domainMonitor.Left = 200;
                domainMonitor.Width = 640;
                domainMonitor.Height = 480;
                debugViewService.DebugFormCollection.Add(domainMonitor);
            }

            SetValueWhenDebug(initParam);

            initParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
        }

        private void OnPushOperatorToUIThreadEvent(PushOperatorToUIThreadEvent.Args args)
        {
            args.Operate.DynamicInvoke(args.Params);
        }

        private void InitalizeServices(SubsystemInitParam initParam)
        {
            var serviceManager = initParam.DataPackage.ServiceManager;

            var signalType = SignalType.NRIET.ToString();

            var interfaceService = new InterfaceAdapterService();
            interfaceService.Initalize(
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig)));

            serviceManager.RegistService<IInterfaceAdapterService>(signalType, interfaceService);
        }

        private void SetValueWhenDebug(SubsystemInitParam initParam)
        {
        }
    }
}