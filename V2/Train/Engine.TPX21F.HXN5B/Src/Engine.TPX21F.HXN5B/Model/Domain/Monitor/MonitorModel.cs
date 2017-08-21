using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Engine.TPX21F.HXN5B.Model.Domain.Monitor.Detail;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Domain.Monitor
{
    [Export]
    public class MonitorModel : NotificationObject
    {
        private int m_CurrentDCUIndex;
        private int m_CurrentEngineIndex;
        public ICommand LoadedCommand { set; get; }

        public Lazy<MonitorPage<AssistMachineMonitorItem>> MonitorAssistMachinePage { set; get; }

        public Lazy<ReadOnlyCollection<MonitorPage>> DCUPageCollection { set; get; }

        public MonitorPage CurrentDCUPage
        {
            get
            {
                if (DCUPageCollection.Value.Any() && CurrentDCUIndex >= 0 && CurrentDCUIndex < DCUPageCollection.Value.Count)
                {
                    return DCUPageCollection.Value[CurrentDCUIndex];
                }
                return null;
            }
        }

        public int CurrentDCUIndex
        {
            set
            {
                if (value == m_CurrentDCUIndex)
                {
                    return;
                }

                m_CurrentDCUIndex = value;
                RaisePropertyChanged(() => CurrentDCUIndex);
                RaisePropertyChanged(() => CurrentDCUPage);
            }
            get { return m_CurrentDCUIndex; }
        }

        public Lazy<MonitorPage> ECUPage { set; get; }

        public Lazy<MonitorPage> OilEngineStartPage { set; get; }

        public Lazy<ReadOnlyCollection<MonitorPage>> EnginePageCollection { set; get; }

        public MonitorPage CurrentEnginPage {  get
            {
                if (EnginePageCollection.Value.Any() && CurrentEngineIndex >= 0 && CurrentEngineIndex < EnginePageCollection.Value.Count)
                {
                    return EnginePageCollection.Value[CurrentEngineIndex];
                }
                return null;
            }
        }


        public int CurrentEngineIndex
        {
            set
            {
                if (value == m_CurrentEngineIndex)
                {
                    return;
                }

                m_CurrentEngineIndex = value;
                RaisePropertyChanged(() => CurrentEngineIndex);
                RaisePropertyChanged(() => CurrentEnginPage);
            }
            get { return m_CurrentEngineIndex; }
        }

        public Lazy<MonitorPage> PhaseControlPage { set; get; }

        public Lazy<MonitorPage> SelfChargingPage { set; get; }

        public Lazy<MonitorPage> TowPage { set; get; }
    }
}