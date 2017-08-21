using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Engine.TPX21F.HXN5B.Model.Interface;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Test.Detail
{
    [Export]
    public class TestSelfItemsModel : NotificationObject, IResetSupport
    {
        private string m_SelectedGroupName;
        private TestSelfItem m_SelectedItem;
        private SelfTestCondition m_SelectedCondition;
        private ObservableCollection<TestSelfItem> m_GroupItems;
        public Lazy<ObservableCollection<TestSelfItem>> Items { get; set; }

        public Lazy<List<string>> GroupNames { set; get; }

        public SelfTestCondition SelectedCondition
        {
            get { return m_SelectedCondition; }
            set
            {
                if (value == m_SelectedCondition)
                {
                    return;
                }

                m_SelectedCondition = value;
                RaisePropertyChanged(() => SelectedCondition);
            }
        }

        public string SelectedGroupName
        {
            set
            {
                if (value == m_SelectedGroupName)
                {
                    return;
                }

                m_SelectedGroupName = value;
                GroupItems = new ObservableCollection<TestSelfItem>(Items.Value.Where(w => w.ItemConfig.GroupName == value));
                SelectedItem = GroupItems.FirstOrDefault();
                RaisePropertyChanged(() => SelectedGroupName);
            }
            get { return m_SelectedGroupName; }
        }

        public ObservableCollection<TestSelfItem> GroupItems
        {
            get { return m_GroupItems; }
            private set
            {
                if (Equals(value, m_GroupItems))
                {
                    return;
                }

                m_GroupItems = value;
                RaisePropertyChanged(() => GroupItems);
            }
        }

        public TestSelfItem SelectedItem
        {
            get { return m_SelectedItem; }
            set
            {
                if (Equals(value, m_SelectedItem))
                {
                    return;
                }

                m_SelectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public void Reset()
        {
            SelectedGroupName = GroupNames.Value.FirstOrDefault();
        }
    }

    
}