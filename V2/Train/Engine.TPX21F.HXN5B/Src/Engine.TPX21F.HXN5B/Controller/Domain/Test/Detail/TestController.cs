using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TPX21F.HXN5B.Event;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.Model.Domain.Test.Detail;
using Engine.TPX21F.HXN5B.Model.Interface;
using Engine.TPX21F.HXN5B.ViewModel.Domain;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TPX21F.HXN5B.Controller.Domain.Test.Detail
{
    [Export]
    [Export(typeof(IResetSupport))]
    public class TestController : ControllerBase<TestViewModel>, IResetSupport
    {
        private readonly IEventAggregator m_EventAggregator;

        [ImportingConstructor]
        public TestController(IEventAggregator eventAggregator)
        {
            m_EventAggregator = eventAggregator;
        }

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.TestSpeedConditions = new Lazy<ReadOnlyCollection<TestItem>>(() =>
            {
                var its =
                    GlobalParam.Instance.TestSpeedConditionItemConfigs.Value.Select(s => new TestItem(s))
                        .ToList()
                        .AsReadOnly();
                m_EventAggregator.GetEvent<LazyValueCreatedEvent>().Publish(null);
                return its;
            });
            ViewModel.Model.VigilantModel.TestItems = new Lazy<ReadOnlyCollection<TestItem>>(() =>
            {
                var its =
                    GlobalParam.Instance.TestVigilantConditionItemConfigs.Value.Select(s => new TestItem(s))
                        .ToList()
                        .AsReadOnly();
                m_EventAggregator.GetEvent<LazyValueCreatedEvent>().Publish(null);
                return its;
            });

            ViewModel.Model.VigilantModel.Conditions =
                new Lazy<ReadOnlyCollection<TestItem>>(
                    () =>
                    {
                        var its = ViewModel.Model.VigilantModel.TestItems.Value.Take(5).ToList().AsReadOnly();
                        m_EventAggregator.GetEvent<LazyValueCreatedEvent>().Publish(null);
                        return its;
                    });


            ViewModel.Model.VigilantModel.Testings =
                new Lazy<ReadOnlyCollection<TestItem>>(
                    () =>
                    {
                        var its = ViewModel.Model.VigilantModel.TestItems.Value.Skip(5).ToList().AsReadOnly();
                        m_EventAggregator.GetEvent<LazyValueCreatedEvent>().Publish(null);
                        return its;
                    });


        }

        public void Reset()
        {
            if (ViewModel.Model.TestSpeedConditions.IsValueCreated)
            {
                foreach (var it in ViewModel.Model.TestSpeedConditions.Value)
                {
                    it.Passed = false;
                }
            }

            if (ViewModel.Model.VigilantModel.TestItems.IsValueCreated)
            {
                foreach (var it in ViewModel.Model.VigilantModel.TestItems.Value)
                {
                    it.Passed = false;
                }
            }
        }
    }
}