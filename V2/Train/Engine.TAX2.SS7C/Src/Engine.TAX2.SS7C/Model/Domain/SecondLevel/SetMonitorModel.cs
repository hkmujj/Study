using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Model.Domain.SecondLevel.Details;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.Domain.SecondLevel
{
    [Export]
    public class SetMonitorModel : NotificationObject
    {
        private MonitorItem m_SelectedMonitorItem;
        private Lazy<ReadOnlyCollection<MonitorItem>> m_MonitorItemCollection;
        private MonitorItem m_SuredSelectedMonitorItem;

        public Lazy<ReadOnlyCollection<MonitorItem>> MonitorItemCollection
        {
            get { return m_MonitorItemCollection; }
            set
            {
                if (Equals(value, m_MonitorItemCollection))
                {
                    return;
                }

                m_MonitorItemCollection = value;
                RaisePropertyChanged(() => MonitorItemCollection);
            }
        }

        public MonitorItem SelectedMonitorItem
        {
            get { return m_SelectedMonitorItem; }
            set
            {
                if (Equals(value, m_SelectedMonitorItem))
                {
                    return;
                }

                m_SelectedMonitorItem = value;
                RaisePropertyChanged(() => SelectedMonitorItem);
            }
        }

        public MonitorItem SuredSelectedMonitorItem
        {
            get { return m_SuredSelectedMonitorItem; }
            set
            {
                if (Equals(value, m_SuredSelectedMonitorItem))
                {
                    return;
                }

                m_SuredSelectedMonitorItem = value;
                RaisePropertyChanged(() => SuredSelectedMonitorItem);
            }
        }
    }
}