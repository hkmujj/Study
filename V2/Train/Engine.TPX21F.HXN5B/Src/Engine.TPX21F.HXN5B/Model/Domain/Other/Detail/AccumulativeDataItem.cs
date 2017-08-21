using System.Diagnostics;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Other.Detail
{
    [DebuggerDisplay("Content={ItemConfig.ShowContent}, Value={Value}")]
    public class AccumulativeDataItem : NotificationObject
    {
        private float m_Value;

        [DebuggerStepThrough]
        public AccumulativeDataItem(AccumulativeDataItemConfig itemConfig)
        {
            ItemConfig = itemConfig;
        }

        public AccumulativeDataItemConfig ItemConfig { private set; get; }

        public float Value
        {
            get { return m_Value; }
            set
            {
                if (value.Equals(m_Value))
                {
                    return;
                }

                m_Value = value;
                RaisePropertyChanged(() => Value);
            }
        }
    }
}