using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommonUtil.Annotations;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.Enum;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IFaultRecordVIewModel))]
    public class FaultRecordViewModel : ViewModelBase, IFaultRecordVIewModel
    {
        private int m_CurrentPage;
        private int m_MaxPage;
        private string m_PageInfo;
        private IList<IFaultInfo> m_CurrentInfo;
        private readonly IFaultInfo m_NullInfo;
        private IFaultInfo m_SelectInfo;
        private Visibility m_Visibility;
        private Visibility m_FaultVisibility;
        private string m_StrPngType;//图标名称
        private bool m_HasFault;

        public FaultRecordViewModel()
        {
            m_NullInfo = new FaultInfo
            {
                Code = -1,
                Logic = -1,
                Time = default(DateTime),
                Description = string.Empty
            };
            PageInfo = "1/1";
            Visibility = Visibility.Hidden;
            NexPage = new DelegateCommand(NexPageAction);
            LastPage = new DelegateCommand(LastPageAction);
            Select = new DelegateCommand<IFaultInfo>(SelectAction);
            CurrentInfo = new List<IFaultInfo>();
            QuitComand = new DelegateCommand(QuitComandAction);
            ChangedType = new DelegateCommand<string>(ChangedTypeAction);
            Manager = ServiceLocator.Current.GetInstance<IFaultManager>();
        
            Manager.MaxPageChanged += (args) =>
            {
                MaxPage = args.NewValue;
                SetInfo();
            };
            Manager.CurrentChanged += (args) =>
            {
                CurrentInfo = Manager.GetCurrent();
                SetInfo();
            };
            Manager.CurrentPageChanged += (args) =>
            {
                CurrentPage = args.NewValue;
                SetInfo();
            };
        }

        void SetInfo()
        {
            SetPageInfo();
            SetCurrentInfo();
            SetFaultIcoVisibility();
        }
        void SetCurrentInfo()
        {
            CurrentInfo = Manager.GetCurrent();
        }
        void SetPageInfo()
        {
            PageInfo = string.Format("{0}/{1}", Manager.CurrentPage, Manager.MaxPage);
            //PageInfo = $"{Manager.CurrentPage}/{Manager.MaxPage}";
        }

        public bool HasFault
        {
            get { return m_HasFault; }
            set
            {
                if (value == m_HasFault) return;
                m_HasFault = value;
                RaisePropertyChanged(() => HasFault);
            }
        }

        void SetFaultIcoVisibility()
        {
            HasFault = Manager.CurrentInfo.Count > 0;
            //StrPngType = Manager.CurrentInfo.Count == 0 ? ("../Resource/Image/YellowFault.png") : ("../Resource/Image/RedFault.png");
        }
        public override void Clear()
        {
            base.Clear();
            Visibility = Visibility.Hidden;

        }

        private void QuitComandAction()
        {
            var region = RegionManager.Regions[RegionNames.PopUpRegion];
            if (region.ActiveViews.Count() != 0)
            {
                Visibility = Visibility.Hidden;
            }
        }

        private void SelectAction(IFaultInfo obj)
        {
            if (obj != null)
            {
                SelectInfo = obj;
                RegionManager.RequestNavigate(RegionNames.PopUpRegion, ControlNames.FaultInfomation);
                Visibility = Visibility.Visible;
            }
        }

        private void ChangedTypeAction(string obj)
        {

            if (string.IsNullOrEmpty(obj))
            {
                return;
            }

            FaultType tmp;
            if (Enum.TryParse(obj, true, out tmp))
            {
                Manager.SetCurrentType(tmp);
            }
        }

        private void LastPageAction()
        {
            Manager.LastPage();
        }

        private void NexPageAction()
        {
            Manager.NextPage();
        }


        public ICommand NexPage { get; private set; }
        public ICommand LastPage { get; private set; }
        public ICommand ChangedType { get; private set; }
        public IFaultManager Manager { get; private set; }

        public int MaxPage
        {
            get { return m_MaxPage; }
            set
            {
                if (value == m_MaxPage)
                {
                    return;
                }
                m_MaxPage = value;
                RaisePropertyChanged(() => MaxPage);
            }
        }

        void SetNullData(IList<IFaultInfo> info)
        {
            while (info.Count < 7)
            {
                info.Add(m_NullInfo);
            }
        }
        public int CurrentPage
        {
            get { return m_CurrentPage; }
            set
            {
                if (value == m_CurrentPage)
                {
                    return;
                }

                m_CurrentPage = value;
                RaisePropertyChanged(() => CurrentPage);
            }
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

        public IList<IFaultInfo> CurrentInfo
        {
            get { return m_CurrentInfo; }
            set
            {
                if (Equals(value, m_CurrentInfo))
                {
                    return;
                }
                SetNullData(value);
                m_CurrentInfo = value;
                RaisePropertyChanged(() => CurrentInfo);
            }
        }

        public IFaultInfo SelectInfo
        {
            get { return m_SelectInfo; }
            set
            {
                if (Equals(value, m_SelectInfo))
                {
                    return;
                }
                m_SelectInfo = value;
                RaisePropertyChanged(() => SelectInfo);
            }
        }

        public ICommand Select { get; private set; }
        public ICommand QuitComand { get; private set; }

        public Visibility Visibility
        {
            get { return m_Visibility; }
            set
            {
                if (value == m_Visibility)
                {
                    return;
                }
                m_Visibility = value;
                RaisePropertyChanged(() => Visibility);
            }
        }

        public Visibility FaultVisibility
        {
            get { return m_FaultVisibility; }
            set
            {
                if (value == m_FaultVisibility)
                {
                    return;
                }
                m_FaultVisibility = value;
                RaisePropertyChanged(() => FaultVisibility);
            }
        }
        public string StrPngType
        {
            get { return m_StrPngType; }
            set
            {
                if (value == m_StrPngType)
                {
                    return;
                }
                m_StrPngType = value;
                RaisePropertyChanged(() => StrPngType);
            }
        }
    }
}