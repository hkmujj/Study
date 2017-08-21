using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Utils;

namespace Subway.TCMS.LanZhou.Model.Domain.Fault
{
    [Export]
    public class FaultModel: NotificationObject
    {
        private int m_CreateFaultIndex;
        public ObservableCollection<FaultItem> AllFaults { get; private set; }

        public int CreateFaultIndex
        {
            get { return m_CreateFaultIndex; }
            set
            {
                if (value == m_CreateFaultIndex)
                {
                    return;
                }
                m_CreateFaultIndex = value;
                RaisePropertyChanged(() => CreateFaultIndex);
            }
        }

        public FaultModel()
        {
            AllFaults = new ObservableCollection<FaultItem>();

            CurrentShowingFaultModel = new ShowingFaultModel();

            HistoryShowingFaultModel = new ShowingFaultModel();
        }


        public ShowingFaultModel CurrentShowingFaultModel { get; private set; }

        public ShowingFaultModel HistoryShowingFaultModel { get; private set; }
    }


    public class FaultItem : NotificationObject
    {
        public FaultItem(FaultInfo faultInfo, DateTime startTime)
        {
            FaultInfo = faultInfo;
            StartTime = startTime;
        }

        public FaultInfo FaultInfo { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime EndTime { get; set; }

    }
}