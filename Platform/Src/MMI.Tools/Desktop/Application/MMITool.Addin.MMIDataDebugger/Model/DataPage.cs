using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.ViewModel;
using MMITool.Addin.MMIDataDebugger.Model.Base;

namespace MMITool.Addin.MMIDataDebugger.Model
{
    public class DataPage : NotificationObject
    {
        private Lazy<ObservableCollection<BoolItem>> m_BoolItems;
        private Lazy<ObservableCollection<FloatItem>> m_FloatItems;

        public Lazy<ObservableCollection<BoolItem>> BoolItems
        {
            get { return m_BoolItems; }
            set
            {
                if (Equals(value, m_BoolItems))
                    return;

                m_BoolItems = value;
                RaisePropertyChanged(() => BoolItems);
            }
        }

        public Lazy<ObservableCollection<FloatItem>> FloatItems
        {
            get { return m_FloatItems; }
            set
            {
                if (Equals(value, m_FloatItems))
                    return;

                m_FloatItems = value;
                RaisePropertyChanged(() => FloatItems);
            }
        }
    }
}