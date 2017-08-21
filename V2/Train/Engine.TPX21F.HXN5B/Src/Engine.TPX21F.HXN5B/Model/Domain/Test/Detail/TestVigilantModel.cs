using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Test.Detail
{
    [Export]
    public class TestVigilantModel :NotificationObject
    {
        private Lazy<ReadOnlyCollection<TestItem>> m_TestItems;
        private Lazy<ReadOnlyCollection<TestItem>> m_Conditions;
        private Lazy<ReadOnlyCollection<TestItem>> m_Testings;

        public Lazy<ReadOnlyCollection<TestItem>> TestItems
        {
            set
            {
                if (Equals(value, m_TestItems))
                {
                    return;
                }

                m_TestItems = value;
                RaisePropertyChanged(() => TestItems);
            }
            get { return m_TestItems; }
        }

        public Lazy<ReadOnlyCollection<TestItem>> Conditions
        {
            get { return m_Conditions; }
            set
            {
                if (Equals(value, m_Conditions))
                {
                    return;
                }

                m_Conditions = value;
                RaisePropertyChanged(() => Conditions);
            }
        }

        public Lazy<ReadOnlyCollection<TestItem>> Testings
        {
            get { return m_Testings; }
            set
            {
                if (Equals(value, m_Testings))
                {
                    return;
                }

                m_Testings = value;
                RaisePropertyChanged(() => Testings);
            }
        }
    }
}