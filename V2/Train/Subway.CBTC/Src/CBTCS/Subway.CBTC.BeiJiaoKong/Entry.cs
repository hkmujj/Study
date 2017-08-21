using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Service;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Subway.CBTC.BeiJiaoKong.Models;
using Subway.CBTC.BeiJiaoKong.ViewModel;
using Subway.CBTC.BeiJiaoKong.Views.App;
using CBTC.DataAdapter;
using CBTC.Infrasturcture.Events;
using Microsoft.Practices.Prism.Events;
using Subway.CBTC.BeiJiaoKong.Interfaces;

namespace Subway.CBTC.BeiJiaoKong
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    [SubsystemExport(typeof(Entry))]
    public class Entry : ISubsystem
    {
        private readonly Action<DataAdapterBase> m_ClearDataOnCoursrStopAction = adpt => { adpt.ClearDataOnCourseStop(); };
        private PushOperatorToUIThreadEvent m_PushOperatorToUIThreadEvent;
        private IEventAggregator m_EventAggregator;

        [ImportingConstructor]
        public Entry(IEventAggregator eventAggregator)
        {
            m_EventAggregator = eventAggregator;
            m_PushOperatorToUIThreadEvent = eventAggregator.GetEvent<PushOperatorToUIThreadEvent>();
        }

        /// <summary/>
        /// <param name="initParam"/>
        public void Initalize(SubsystemInitParam initParam)
        {
            GlobalParams.Instance.Initialize(initParam);

            InitalizeServices(initParam);

            var viewService = initParam.DataPackage.ServiceManager.GetService<IViewService>();
            var frm = new WpfHostForm(initParam);
            viewService.Regist(frm.AppName, frm);
            viewService.Active(frm.AppName);
            var domainVm = ServiceLocator.Current.GetInstance<BeiJiaoKongViewModel>();
            var adapter =
                DataAdapterFactory.Instance.CreateDataAdapter(domainVm.CBTC);
            initParam.DataPackage.ServiceManager.GetService<IDataChangeListenService>()
                .RegistListener(adapter, initParam.AppConfig);

            m_PushOperatorToUIThreadEvent.Subscribe(OnPushOperatorToUIThreadEvent, ThreadOption.UIThread, true,
                args => args.ScreenIdentity == domainVm.CBTC.Identity);
            
            var coursService = initParam.DataPackage.ServiceManager.GetService<ICourseService>();
            coursService.CourseStateChanged += (sender, args) =>
            {
                if (args.CourseService.CurrentCourseState == CourseState.Stoped)
                {
                    OnClear();

                    m_PushOperatorToUIThreadEvent.Publish(
                        new PushOperatorToUIThreadEvent.Args(m_ClearDataOnCoursrStopAction, domainVm.CBTC.Identity,
                            adapter));

                    initParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
                }
            };

            GlobalParams.Instance.InitParam.CommunicationDataService.ReadService.RaiseAllDataChanged();
        }

        private void OnClear()
        {

            BeiJiaoKongViewModel.Domain.TestInfo.ButtonDownBrakeTest = false;
            BeiJiaoKongViewModel.Domain.TestInfo.ButtonDownBroadcastTest = false;
            BeiJiaoKongViewModel.Domain.TestInfo.ButtonDownLightTest = false;
            BeiJiaoKongViewModel.Domain.TestInfo.ButtonDownRemit = false;
            BeiJiaoKongViewModel.Domain.Other.Light = 100;
            BeiJiaoKongViewModel.Domain.Other.Volumne = 50;

            BeiJiaoKongViewModel.DoMainViewModel.InputKeyBoard.ClearCommand.Execute(string.Empty);
            BeiJiaoKongViewModel.DoMainViewModel.InputScreen.Input.Execute(string.Empty);
            BeiJiaoKongViewModel.DoMainViewModel.InputScreen.Submit.Execute(string.Empty);
        }

        private void OnPushOperatorToUIThreadEvent(PushOperatorToUIThreadEvent.Args args)
        {
            args.Operate.DynamicInvoke(args.Params);
        }

        private void InitalizeServices(SubsystemInitParam initParam)
        {
            var serviceManager = initParam.DataPackage.ServiceManager;

            var signalType = SignalType.TCT.ToString();

            var interfaceService = new InterfaceAdapterService();
            interfaceService.Initalize(
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(initParam.AppConfig)));

            serviceManager.RegistService<IInterfaceAdapterService>(signalType, interfaceService);
        }
    }
}