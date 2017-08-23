using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Model.Fault;

namespace Urban.GuiYang.DDU.Model.Train
{
    [Export]
    public class FaultModel : NotificationObject
    {
        private int m_Count;
        private FaultInfo m_SelectItem;
        private List<FaultInfo> m_CurrentPageInfo;
        private string m_PageInfo;
        private FaultInfo m_CurrentFault;
        private Visibility m_Visibility;

        public FaultModel()
        {
            PageInfo = "1/1";
            Visibility = Visibility.Hidden;
            FaultManager = new FaultManager();
            FaultManager.CurrentInfoChanged += () =>
            {
                CurrentPageInfo = IsCurrent ? FaultManager.GetCurrentInfo() : FaultManager.GetHistoryInfo();
                CurrentFault = FaultManager.GetInfo();
            };
            FaultManager.CurrentPageChanged += () =>
            {
                PageInfo = FaultManager.CurrentPage + "/" + FaultManager.MaxPage;
            };
            FaultManager.MaxPageChanged += () =>
            {
                PageInfo = FaultManager.CurrentPage + "/" + FaultManager.MaxPage;
            };
        }

        public int Count
        {
            get { return m_Count; }
            set
            {
                if (value == m_Count)
                    return;

                m_Count = value;
                RaisePropertyChanged(() => Count);
            }
        }

        public string PageInfo
        {
            get { return m_PageInfo; }
            private set
            {
                if (value == m_PageInfo)
                    return;

                m_PageInfo = value;
                RaisePropertyChanged(() => PageInfo);
            }
        }

        public FaultManager FaultManager { get; private set; }

        public FaultInfo CurrentFault
        {
            get { return m_CurrentFault; }
            set
            {
                if (Equals(value, m_CurrentFault))
                    return;

                m_CurrentFault = value;
                Visibility = m_CurrentFault == default(FaultInfo) ? Visibility.Hidden : Visibility.Visible;
                RaisePropertyChanged(() => CurrentFault);
            }
        }

        public Visibility Visibility
        {
            get { return m_Visibility; }
            set
            {
                if (value == m_Visibility)
                    return;

                m_Visibility = value;
                RaisePropertyChanged(() => Visibility);
            }
        }

        public FaultInfo SelectItem
        {
            get { return m_SelectItem; }
            set
            {
                if (Equals(value, m_SelectItem))
                    return;

                m_SelectItem = value;
                RaisePropertyChanged(() => SelectItem);
            }
        }

        public List<FaultInfo> CurrentPageInfo
        {
            get { return m_CurrentPageInfo; }
            set
            {
                if (Equals(value, m_CurrentPageInfo))
                    return;

                m_CurrentPageInfo = value;
                RaisePropertyChanged(() => CurrentPageInfo);
            }
        }

        public bool IsCurrent { get; set; }
    }
}
