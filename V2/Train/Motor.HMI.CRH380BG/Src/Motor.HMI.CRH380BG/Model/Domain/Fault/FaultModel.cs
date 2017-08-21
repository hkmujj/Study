using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380BG.Model.Common;
using Motor.HMI.CRH380BG.Model.Domain.Fault.Detail;

namespace Motor.HMI.CRH380BG.Model.Domain.Fault
{
    [Export]
    public class FaultModel : NotificationObject
    {
        public FaultModel()
        {
            AllItems = new List<FaultItem>();
            AllPagedItems = new ListWrapper<FaultItem>(null
                , true);
        }

        public List<FaultItem> AllItems { private set; get; }
        

        public ListWrapper<FaultItem> AllPagedItems
        {
            get { return m_AllPagedItems; }
            set
            {
                if (Equals(value, m_AllPagedItems))
                {
                    return;
                }
                m_AllPagedItems = value;
                RaisePropertyChanged(() => AllPagedItems);
            }
        }

        private ListWrapper<FaultItem> m_AllPagedItems;



        private FaultItem m_CurrentSelectedItem;

        private FaultItem m_CurrentFlickingFault;

        public FaultItem CurrentFlickingFault
        {
            get { return m_CurrentFlickingFault; }
            set
            {
                if (Equals(value, m_CurrentFlickingFault))
                    return;
                m_CurrentFlickingFault = value;
                RaisePropertyChanged(() => CurrentFlickingFault);
            }
        }

        public FaultItem CurrentSelectedItem
        {
            get { return m_CurrentSelectedItem; }
            set
            {
                if (Equals(value, m_CurrentSelectedItem))
                    return;
                m_CurrentSelectedItem = value;
                RaisePropertyChanged(() => CurrentSelectedItem);
            }
        }

        


        

    }
}
