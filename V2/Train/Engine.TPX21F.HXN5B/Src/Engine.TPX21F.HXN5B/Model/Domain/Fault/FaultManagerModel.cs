using System.Collections.Generic;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Model.Common;
using Engine.TPX21F.HXN5B.Model.ConfigModel;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Engine.TPX21F.HXN5B.Model.Domain.Fault.Detail;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Fault
{
    [Export]
    public class FaultManagerModel : NotificationObject
    {
        private PageWrapper<FaultItem> m_CurrentFaultDetails;

        public FaultManagerModel()
        {
            AllItems = new List<FaultItem>();
            RootViewItems = new PageWrapper<FaultItem>(6,
                it => it.InfoConfig.InfoType == NotifyInfoType.Fault && it.FaultState == FaultState.Occursed);
            NotifyItems = new PageWrapper<FaultItem>(3,
                it => it.InfoConfig.InfoType == NotifyInfoType.Notification && it.FaultState == FaultState.Occursed);
            CurrentFaultItems = new PageWrapper<FaultItem>(15, it => it.FaultState == FaultState.Occursed, true);
            TodayFaultItems = new PageWrapper<FaultItem>(6, null, true);
            AllPagedItems = new PageWrapper<FaultItem>(6, null, true);
        }

        public List<FaultItem> AllItems { private set; get; }
        
        public PageWrapper<FaultItem> RootViewItems { private set; get; }

        public PageWrapper<FaultItem> CurrentFaultItems { get; private set; }

        public PageWrapper<FaultItem> TodayFaultItems { get; private set; }

        public PageWrapper<FaultItem> AllPagedItems { get; private set; }

        public PageWrapper<FaultItem> NotifyItems { get; private set; }

        public PageWrapper<FaultItem> CurrentFaultDetails
        {
            set
            {
                if (Equals(value, m_CurrentFaultDetails))
                {
                    return;
                }

                m_CurrentFaultDetails = value;
                RaisePropertyChanged(() => CurrentFaultDetails);
            }
            get { return m_CurrentFaultDetails; }
        }
    }
}