using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;

namespace MMITool.Addin.MMIDataDebugger.Model.Base
{
    public class ValueItem<T> : NotificationObject
    {
        private T m_Value;

        [DebuggerStepThrough]
        public ValueItem(string name, int index)
        {
            Description = name;
            Index = index;
        }

        public string Description { get; private set; }

        public int Index { get; private set; }

        public T Value
        {
            get { return m_Value; }
            set
            {
                if (Equals(value, m_Value))
                    return;

                m_Value = value;
                RaisePropertyChanged(() => Value);
            }
        }
    }
}