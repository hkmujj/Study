using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.TrainSetting.Detail
{
    [Export]
    public class TrainAutoStartModel : NotificationObject
    {
        private Lazy<ReadOnlyCollection<AutoStartItem>> m_StartSystemItems;
        private int m_StartPerDay;

        public Lazy<ReadOnlyCollection<AutoStartItem>> StartSystemItems
        {
            get { return m_StartSystemItems; }
            set
            {
                if (Equals(value, m_StartSystemItems))
                {
                    return;
                }

                m_StartSystemItems = value;
                RaisePropertyChanged(() => StartSystemItems);
            }
        }

        public int StartPerDay
        {
            get { return m_StartPerDay; }
            set
            {
                if (value == m_StartPerDay)
                {
                    return;
                }

                m_StartPerDay = value;
                RaisePropertyChanged(() => StartPerDay);
            }
        }
    }
}