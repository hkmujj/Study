using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Domain.Test.Detail;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Test
{
    [Export]
    public class TestModel :NotificationObject
    {
        private Lazy<ReadOnlyCollection<TestItem>> m_TestSpeedConditions;

        private float m_SimSpeed;

        [ImportingConstructor]
        public TestModel(TestVigilantModel vigilantModel, TestSelfItemsModel testSelfItemsModel)
        {
            VigilantModel = vigilantModel;
            TestSelfItemsModel = testSelfItemsModel;
        }

        public TestSelfItemsModel TestSelfItemsModel { get; private set; }

        public float SimSpeed
        {
            set
            {
                if (value.Equals(m_SimSpeed))
                {
                    return;
                }

                m_SimSpeed = value;
                RaisePropertyChanged(() => SimSpeed);
            }
            get { return m_SimSpeed; }
        }

        public Lazy<ReadOnlyCollection<TestItem>> TestSpeedConditions
        {
            set
            {
                if (Equals(value, m_TestSpeedConditions))
                {
                    return;
                }

                m_TestSpeedConditions = value;
                RaisePropertyChanged(() => TestSpeedConditions);
            }
            get { return m_TestSpeedConditions; }
        }

        public TestVigilantModel VigilantModel { get; private set; }


    }
}