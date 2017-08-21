using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TPX21F.HXN5B.Event;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.Model.Domain.Monitor.Detail;
using Engine.TPX21F.HXN5B.ViewModel.Domain;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TPX21F.HXN5B.Controller.Domain.Monitor
{
    [Export]
    public class MonitorControlller : ControllerBase<MonitorViewModel>
    {

        private IEventAggregator m_EventAggregator;

        [ImportingConstructor]
        public MonitorControlller(IEventAggregator eventAggregator)
        {
            m_EventAggregator = eventAggregator;
        }

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.SelfChargingPage =
                new Lazy<MonitorPage>(
                    () =>
                    {
                        var d = new MonitorPage(
                            GlobalParam.Instance.MonitorSelfChargingItems.Value.Select(s => new MonitorItem(s))
                                .ToList()
                                .AsReadOnly());
                        m_EventAggregator.GetEvent<LazyValueCreatedEvent>().Publish(null);
                        return d;
                    });

            ViewModel.Model.OilEngineStartPage =
                new Lazy<MonitorPage>(
                    () =>
                    {
                        var d =
                            new MonitorPage(
                                GlobalParam.Instance.MonitorOilEnginStartItems.Value.Select(s => new MonitorItem(s))
                                    .ToList()
                                    .AsReadOnly());

                        m_EventAggregator.GetEvent<LazyValueCreatedEvent>().Publish(null);
                        return d;
                    })

                ;

            ViewModel.Model.DCUPageCollection = new Lazy<ReadOnlyCollection<MonitorPage>>(() =>
            {
                var mis = GlobalParam.Instance.MonitorDCUItemConfigs.Value.Select(s => new MonitorItem(s)).ToList();

                const int groupCount = 7;
                const int groupCount2 = 6;

                var ps = new List<MonitorPage>()
                {
                    new MonitorPage(
                        new ReadOnlyCollection<MonitorItem>(new List<MonitorItem>(mis.Take(groupCount*3))), groupCount),

                    new MonitorPage(
                        new ReadOnlyCollection<MonitorItem>(
                            new List<MonitorItem>(mis.Skip(groupCount*3).Take(groupCount2*3))), groupCount2),

                    new MonitorPage(
                        new ReadOnlyCollection<MonitorItem>(
                            new List<MonitorItem>(mis.Skip(groupCount*3 + groupCount2*3).Take(groupCount2*3))),
                        groupCount2),
                };
                m_EventAggregator.GetEvent<LazyValueCreatedEvent>().Publish(null);
                return ps.AsReadOnly();
            });

            ViewModel.Model.ECUPage =
                new Lazy<MonitorPage>(
                    () =>
                    {
                        var d = new MonitorPage(
                            GlobalParam.Instance.MonitorECUItemConfigs.Value.Select(s => new MonitorItem(s))
                                .ToList()
                                .AsReadOnly());
                        m_EventAggregator.GetEvent<LazyValueCreatedEvent>().Publish(null);
                        return d;
                    });


            ViewModel.Model.PhaseControlPage =
                new Lazy<MonitorPage>(
                    () =>
                    {
                        var d = new MonitorPage(
                            GlobalParam.Instance.MonitorPhaseControlItemConfigs.Value.Select(s => new MonitorItem(s))
                                .ToList()
                                .AsReadOnly());
                        m_EventAggregator.GetEvent<LazyValueCreatedEvent>().Publish(null);
                        return d;
                    });

            ViewModel.Model.TowPage =
                new Lazy<MonitorPage>(
                    () =>
                    {
                        var d = new MonitorPage(
                            GlobalParam.Instance.MonitorTowItemConfigs.Value.Select(s => new MonitorItem(s))
                                .ToList()
                                .AsReadOnly());
                        m_EventAggregator.GetEvent<LazyValueCreatedEvent>().Publish(null);
                        return d;
                    });

            ViewModel.Model.MonitorAssistMachinePage =
                new Lazy<MonitorPage<AssistMachineMonitorItem>>(
                    () =>
                    {
                        var d = new MonitorPage<AssistMachineMonitorItem>(
                            GlobalParam.Instance.MonitorAssistMachineItemConfigs.Value.Select(
                                s => new AssistMachineMonitorItem(s))
                                .ToList()
                                .AsReadOnly());
                        m_EventAggregator.GetEvent<LazyValueCreatedEvent>().Publish(null);
                        return d;
                    });

            ViewModel.Model.EnginePageCollection = new Lazy<ReadOnlyCollection<MonitorPage>>(() =>
            {
                var mis = GlobalParam.Instance.MonitorEngineItemConfigs.Value.Select(s => new MonitorItem(s)).ToList();

                const int groupCount = 8;

                var ps = new List<MonitorPage>()
                {
                    new MonitorPage(
                        new ReadOnlyCollection<MonitorItem>(new List<MonitorItem>(mis.Take(groupCount*3))), groupCount),

                    new MonitorPage(
                        new ReadOnlyCollection<MonitorItem>(
                            new List<MonitorItem>(mis.Skip(groupCount*3).Take(groupCount*3))), groupCount),

                    new MonitorPage(
                        new ReadOnlyCollection<MonitorItem>(
                            new List<MonitorItem>(mis.Skip(groupCount*3*2).Take(groupCount*3))), groupCount),
                };

                m_EventAggregator.GetEvent<LazyValueCreatedEvent>().Publish(null);

                return ps.AsReadOnly();
            });

        }
    }
}