using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Model.Fault;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Model
{
    [Export]
    public class FaultModel : NotificationObject
    {
        private FaultInfo m_DispkayFault1;
        private FaultInfo m_DisplayFault2;

        public FaultModel()
        {
            AllFault = GlobalParam.Instance.FaultInfos.ToDictionary(t => t.LogicNumber, t => t);
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<DataServiceDataChangedEvent>()
                .Subscribe(DataChanged, ThreadOption.UIThread);
            CurrentFault = new List<FaultInfo>();
        }

        private void DataChanged(DataServiceDataChangedEvent.Args obj)
        {
            foreach (var b in obj.DataChangedArgs.ChangedBools.NewValue)
            {
                if (AllFault.ContainsKey(b.Key))
                {
                    if (b.Value)
                    {
                        var tmp = AllFault[b.Key].Clone();
                        tmp.HappenTime = DateTime.Now;
                        CurrentFault.Add(tmp);
                        OnFaultChanged();
                    }
                    else
                    {
                        CurrentFault.Remove(CurrentFault.Find(f => f.LogicNumber == b.Key));
                        OnFaultChanged();
                    }
                }
            }
        }

        private void OnFaultChanged()
        {
            if (CurrentFault.Count >= 2)
            {
                DispkayFault1 = CurrentFault.Last();
                DisplayFault2 = CurrentFault[CurrentFault.Count - 2];
            }
            else
            {
                DispkayFault1 = CurrentFault.FirstOrDefault();
                DisplayFault2 = default(FaultInfo);
            }
        }
        public FaultInfo DispkayFault1
        {
            get { return m_DispkayFault1; }
            set
            {
                if (Equals(value, m_DispkayFault1))
                    return;
                m_DispkayFault1 = value;
                RaisePropertyChanged(() => DispkayFault1);
            }
        }

        public FaultInfo DisplayFault2
        {
            get { return m_DisplayFault2; }
            set
            {
                if (Equals(value, m_DisplayFault2))
                    return;
                m_DisplayFault2 = value;
                RaisePropertyChanged(() => DisplayFault2);
            }
        }

        public Dictionary<int, FaultInfo> AllFault { get; private set; }
        public List<FaultInfo> CurrentFault { get; private set; }
    }
}
