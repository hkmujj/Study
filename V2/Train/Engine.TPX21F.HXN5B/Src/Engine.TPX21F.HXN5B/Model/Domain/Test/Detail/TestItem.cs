using System.Diagnostics;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Engine.TPX21F.HXN5B.Model.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Test.Detail
{
    public class TestItem : NotificationObject, IPassableItem
    {
        private bool m_Passed;
        private float m_Value;

        [DebuggerStepThrough]
        public TestItem(TestItemConfig itemConfig)
        {
            ItemConfig = itemConfig;
        }

        public TestItemConfig ItemConfig { private set; get; }

        public bool Passed
        {
            set
            {
                if (value == m_Passed)
                {
                    return;
                }

                m_Passed = value;
                RaisePropertyChanged(() => Passed);
            }
            get { return m_Passed; }
        }

        public float Value
        {
            set
            {
                if (value.Equals(m_Value))
                {
                    return;
                }

                m_Value = value;
                RaisePropertyChanged(() => Value);
            }
            get { return m_Value; }
        }

        IPassableItemConfig IPassableItem.ItemConfig
        {
            get { return ItemConfig; }
        }
    }
}