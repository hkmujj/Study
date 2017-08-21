using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Engine._6A.Enums;
using Engine._6A.Interface;
using Engine._6A.Interface.ViewModel;
using Microsoft.Practices.Prism.Commands;

namespace Engine._6A.ViewModel.Common
{
    [Export(typeof(IFaultViewModel))]
    public class FaultViewModel : ViewModelBase, IFaultViewModel
    {
        private Visibility m_ButtonVisibility;
        private IList<IFaultInfo> m_CurrentFalutInfos;
        private string m_PageInfo;

        public FaultViewModel()
        {
            NextPage = new DelegateCommand(NextPageMethod);
            LastPage = new DelegateCommand(LastPageMethod);
            Reset = new DelegateCommand<string>(ResetMethod);
            CurrentFalutInfos = new List<IFaultInfo>();
            ButtonVisibility = Visibility.Hidden;
        }

        private void ResetMethod(string obj)
        {
            FaultType type;
            System.Enum.TryParse(obj, false, out type);
            FaultManage.Reset(type);
            CurrentFalutInfos = FaultManage.GetCurrent();
        }

        private void LastPageMethod()
        {
            FaultManage.LastPage();
            CurrentFalutInfos = FaultManage.GetCurrent();
        }

        private void NextPageMethod()
        {
            FaultManage.NextPage();
            CurrentFalutInfos = FaultManage.GetCurrent();
        }

        public string PageInfo
        {
            get { return m_PageInfo; }
            set
            {
                if (value == m_PageInfo)
                {
                    return;
                }
                m_PageInfo = value;
                RaisePropertyChanged(() => PageInfo);
            }
        }

        public IList<IFaultInfo> CurrentFalutInfos
        {
            get { return m_CurrentFalutInfos; }
            set
            {
                if (Equals(value, m_CurrentFalutInfos))
                {
                    return;
                }
                m_CurrentFalutInfos = value;
                RaisePropertyChanged(() => CurrentFalutInfos);
            }
        }

        public ICommand NextPage { get; private set; }
        public ICommand LastPage { get; private set; }
        public ICommand Reset { get; private set; }
        public IFaultManage FaultManage { get; set; }

        public Visibility ButtonVisibility
        {
            get { return m_ButtonVisibility; }
            set
            {
                if (value == m_ButtonVisibility)
                {
                    return;
                }
                m_ButtonVisibility = value;
                RaisePropertyChanged(() => ButtonVisibility);
            }
        }
    }
}