using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.Model.Interface;
using Engine.TCMS.HXD3.Model.TCMS.Domain;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class SwitchStateViewModel : NotificationObject, IMutiPageProvider
    {
        private int m_ShowingPageIndex;

        public List<SwitchItem> SwitchItems { private set; get; }

        public List<SwitchItem> ShowingPage
        {
            get { return ShowingPageIndex>=0 && ShowingPageIndex < Pages.Count ? Pages[ShowingPageIndex] : null; }
        }

        public int ShowingPageIndex
        {
            set
            {
                if (value == m_ShowingPageIndex)
                {
                    return;
                }

                m_ShowingPageIndex = value;
                RaisePropertyChanged(() => ShowingPageIndex);
                RaisePropertyChanged(() => ShowingPage);
                RaisePropertyChanged(() => CanNavigateBack);
                RaisePropertyChanged(() => CanNavigateForward);
            }
            get { return m_ShowingPageIndex; }
        }

        public DelegateCommand NextPageCommand { private set; get; }

        public DelegateCommand PrePageCommand { private set; get; }

        public List<List<SwitchItem>> Pages { private set; get; }

        public SwitchStateViewModel()
        {
            PrePageCommand = new DelegateCommand(OnPrePageExcute);
            NextPageCommand = new DelegateCommand(OnNextPageExcute);

            if (GlobalParam.Instance.SwitchStateConfigs.Any())
            {
                SwitchItems = GlobalParam.Instance.SwitchStateConfigs.Select(s => new SwitchItem(s)).ToList();
                Pages = new List<List<SwitchItem>>()
                {
                    new List<SwitchItem>(SwitchItems.Take(16)),
                    new List<SwitchItem>(SwitchItems.Skip(16).Take(32)),
                    new List<SwitchItem>(SwitchItems.Skip(16+32).Take(32)),
                };
                NavagateDefault();
            }
        }

        private void OnNextPageExcute()
        {
            if (CanNavigateForward)
            {
                NavigateForward();
            }
        }

        private void OnPrePageExcute()
        {
            if (CanNavigateBack)
            {
                NavigateBack();
            }
        }


        /// <summary>
        /// 向后
        /// </summary>
        public bool CanNavigateBack
        {
            get { return ShowingPageIndex > 0; }
        }

        /// <summary>
        /// 向前
        /// </summary>
        public bool CanNavigateForward
        {
            get { return ShowingPageIndex < Pages.Count - 1; }
        }

        public void NavigateBack()
        {
            --ShowingPageIndex;
        }

        public void NavigateForward()
        {
            ++ ShowingPageIndex;
        }

        public void NavagateDefault()
        {
            ShowingPageIndex = 0;
        }
    }
}