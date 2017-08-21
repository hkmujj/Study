using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Monitor.Detail
{
    public class MonitorPage : MonitorPage<MonitorItem>
    {
        [DebuggerStepThrough]
        public MonitorPage(ReadOnlyCollection<MonitorItem> allItems, int groupCount = 8) : base(allItems, groupCount)
        {
        }
    }

    public class MonitorPage<T> : NotificationObject where T : MonitorItem
    {
        private int m_GroupCount;

        [DebuggerStepThrough]
        public MonitorPage(ReadOnlyCollection<T> allItems, int groupCount = 8)
        {
            AllItems = allItems;
            GroupCount = groupCount;
        }

        public ReadOnlyCollection<T> AllItems { private set; get; }

        public int GroupCount
        {
            get { return m_GroupCount; }
            set
            {
                if (value == m_GroupCount)
                {
                    return;
                }

                m_GroupCount = value;
                RaisePropertyChanged(() => GroupCount);
                RaisePropertyChanged(() => Group1);
                RaisePropertyChanged(() => Group2);
                RaisePropertyChanged(() => Group3);
            }
        }

        public IEnumerable<T> Group1
        {
            get { return AllItems.Take(GroupCount); }
        }

        public IEnumerable<T> Group2
        {
            get { return AllItems.Skip(GroupCount).Take(GroupCount); }
        }

        public IEnumerable<T> Group3
        {
            get { return AllItems.Skip(GroupCount * 2).Take(GroupCount); }
        }
    }
}