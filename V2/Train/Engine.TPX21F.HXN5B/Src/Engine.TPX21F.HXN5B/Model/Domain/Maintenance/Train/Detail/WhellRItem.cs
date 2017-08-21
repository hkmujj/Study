using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Maintenance.Train.Detail
{
    public class WhellRItem : NotificationObject
    {
        private float m_Current;
        private bool m_IsSetting;

        public WhellRItem(string name, float @default, float min, float max)
        {
            Name = name;
            Default = @default;
            Min = min;
            Max = max;
        }

        public string Name { get; private set; }

        public float Default { get; private set; }

        public float Min { get; private set; }

        public float Max { get; private set; }

        public float Current
        {
            get { return m_Current; }
            set
            {
                if (value.Equals(m_Current))
                {
                    return;
                }

                m_Current = value;
                RaisePropertyChanged(() => Current);
            }
        }

        public bool IsSetting
        {
            get { return m_IsSetting; }
            set
            {
                if (value == m_IsSetting)
                {
                    return;
                }

                m_IsSetting = value;
                RaisePropertyChanged(() => IsSetting);
            }
        }
    }
}