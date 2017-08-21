using System;
using System.ComponentModel.Composition;
using CBTC.DataAdapter;
using CBTC.Infrasturcture.Events;
using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Monitor;
using CBTC.Infrasturcture.Service;
using CBTC.Infrasturcture.ViewModel.Monitor;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.CBTC.Casco.Adapter;
using Subway.CBTC.Casco.Model;
using Subway.CBTC.Casco.View.Shell;
using Subway.CBTC.Casco.ViewModel;

namespace Subway.CBTC.Casco
{
    [SubsystemExport(typeof(CBTCCascoSubystem))]
    public class CBTCCascoSubystem : ISubsystem
    {
        private DomainAdapter m_DomainAdapter;

        private readonly Action<DataAdapterBase> m_ClearDataOnCoursrStopAction = adpt => { adpt.ClearDataOnCourseStop(); };

        private PushOperatorToUIThreadEvent m_PushOperatorToUIThreadEvent;

        private IEventAggregator m_EventAggregator;

        [ImportingConstructor]
        public CBTCCascoSubystem(IEventAggregator eventAggregator)
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

            var form = new CascoForm();

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

            var signalType = SignalType.CASCO.ToString();

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