using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using DevExpress.Mvvm;
using Engine.TCMS.HXD3.Constant;
using Engine.TCMS.HXD3.Model.ConfigModel;
using Engine.TCMS.HXD3.Model.Interface;
using Engine.TCMS.HXD3.View.Contents.Fault;
using Engine.TCMS.HXD3.ViewModel.Domain.Detail;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using PropertySupport = CommonUtil.Util.PropertySupport;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class FaultViewModel : NotificationObject, IResetSupport
    {

        private readonly IRegionManager m_RegionManager;
        private ObservableCollection<FaultItem> m_FaultItemCollection;
        private int m_SelectedIndex;
        private int m_CurrentShowingFirstIndex;
        private bool m_CanGoPreItem;
        private bool m_CanGoNextItem;
        private bool m_CanGoPrePage;
        private bool m_CanGoNextPage;
        private ObservableCollection<string> m_PopingFaultItemNames;

        public const int MaxShowingCount = 12;

        public DelegateCommand NavigateToDescriptionCommand { private set; get; }

        public DelegateCommand NextPageCommand { private set; get; }

        public DelegateCommand PrePageCommand { private set; get; }

        public DelegateCommand NextItemCommand { private set; get; }

        public DelegateCommand PreItemCommand { private set; get; }

        public bool CanGoPreItem
        {
            private set
            {
                if (value == m_CanGoPreItem)
                {
                    return;
                }

                m_CanGoPreItem = value;
                RaisePropertyChanged(() => CanGoPreItem);
            }
            get { return m_CanGoPreItem; }
        }

        public bool CanGoNextItem
        {
            private set
            {
                if (value == m_CanGoNextItem)
                {
                    return;
                }

                m_CanGoNextItem = value;
                RaisePropertyChanged(() => CanGoNextItem);
            }
            get { return m_CanGoNextItem; }
        }

        public bool CanGoPrePage
        {
            private set
            {
                if (value == m_CanGoPrePage)
                {
                    return;
                }

                m_CanGoPrePage = value;
                RaisePropertyChanged(() => CanGoPrePage);
            }
            get { return m_CanGoPrePage; }
        }

        public bool CanGoNextPage
        {
            private set
            {
                if (value == m_CanGoNextPage)
                {
                    return;
                }

                m_CanGoNextPage = value;
                RaisePropertyChanged(() => CanGoNextPage);
            }
            get { return m_CanGoNextPage; }
        }

        public ObservableCollection<FaultItem> FaultItemCollection
        {
            private set
            {
                if (Equals(value, m_FaultItemCollection))
                {
                    return;
                }

                m_FaultItemCollection = value;
                RaisePropertyChanged(() => FaultItemCollection);
                RaisePropertyChanged(() => ShowingItems);
                RaisePropertyChanged(() => PopingFaultItemNames);
                RaisePropertyChanged(() => PopingFaultItemNamesVisible);
            }
            get { return m_FaultItemCollection; }
        }

        public IEnumerable<FaultItem> ShowingItems
        {
            get { return FaultItemCollection.Skip(CurrentShowingFirstIndex).Take(MaxShowingCount); }
        }

        public ObservableCollection<string> PopingFaultItemNames
        {
            private set
            {
                if (Equals(value, m_PopingFaultItemNames))
                {
                    return;
                }

                m_PopingFaultItemNames = value;
                RaisePropertyChanged(() => PopingFaultItemNames);
                RaisePropertyChanged(() => PopingFaultItemNamesVisible);
            }
            get { return m_PopingFaultItemNames; }
        }

        public bool PopingFaultItemNamesVisible
        {
            get { return PopingFaultItemNames != null && PopingFaultItemNames.Any(); }
        }


        private int CurrentShowingFirstIndex
        {
            set
            {
                if (value == m_CurrentShowingFirstIndex)
                {
                    return;
                }

                m_CurrentShowingFirstIndex = value;
                RaisePropertyChanged(() => CurrentShowingFirstIndex);
                RaisePropertyChanged(() => ShowingItems);
                RaisePropertyChanged(() => SelectedIndex);
                NextPageCommand.RaiseCanExecuteChanged();
                PrePageCommand.RaiseCanExecuteChanged();
                NextItemCommand.RaiseCanExecuteChanged();
                PreItemCommand.RaiseCanExecuteChanged();
            }
            get { return m_CurrentShowingFirstIndex; }
        }

        public int SelectedIndex
        {
            set
            {
                if (value == m_SelectedIndex)
                {
                    return;
                }

                m_SelectedIndex = value;
                RaisePropertyChanged(() => SelectedIndex);
                NextPageCommand.RaiseCanExecuteChanged();
                PrePageCommand.RaiseCanExecuteChanged();
                NextItemCommand.RaiseCanExecuteChanged();
                PreItemCommand.RaiseCanExecuteChanged();
            }
            get { return m_SelectedIndex; }
        }


        [ImportingConstructor]
        public FaultViewModel(IRegionManager regionManager)
        {
            m_RegionManager = regionManager;
            PopingFaultItemNames = new ObservableCollection<string>();
            FaultItemCollection = new ObservableCollection<FaultItem>(
                //Enumerable.Range(0, 20).Select(s =>
                //new FaultItem(new FaultItemConfig(121, "内容1121", 2))
                //{
                //    OccuseDateTime = DateTime.Now,
                //    ResumeDateTime = DateTime.Now,
                //    Speed = s*10,
                //    Level = s%10,
                //    Votage = (float) s%10 + (float) s/100,
                //})
                );
            CurrentShowingFirstIndex = 0;
            FaultItemCollection.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    ListenResume(args.NewItems.Cast<FaultItem>());
                }

                if (FaultItemCollection.Any())
                {
                    if (CurrentShowingFirstIndex == -1)
                    {
                        CurrentShowingFirstIndex = 0;
                    }
                }
                else
                {
                    CurrentShowingFirstIndex = -1;
                }
                UpdatePopingFaultItems();
                RaisePropertyChanged(() => ShowingItems);

                PrePageCommand.RaiseCanExecuteChanged();
                PreItemCommand.RaiseCanExecuteChanged();
                NextItemCommand.RaiseCanExecuteChanged();
                NextPageCommand.RaiseCanExecuteChanged();
            };
            NavigateToDescriptionCommand = new DelegateCommand(OnNavigateToDescriptionExcute);
            PrePageCommand = new DelegateCommand(OnPrePage, CanPrePage);
            NextPageCommand = new DelegateCommand(OnNextPage, CanNextPage);
            PreItemCommand = new DelegateCommand(OnPreItem, CanPreItem);
            NextItemCommand = new DelegateCommand(OnNextItem, CanNextItem);
            SelectedIndex = -1;
        }

        private void ListenResume(IEnumerable<FaultItem> faultItems)
        {
            foreach (var item in faultItems)
            {
                item.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == PropertySupport.ExtractPropertyName<FaultItem, DateTime>(f => f.ResumeDateTime))
                    {
                        UpdatePopingFaultItems();
                    }
                };
            }
        }

        private void UpdatePopingFaultItems()
        {
            PopingFaultItemNames =
                new ObservableCollection<string>(
                    FaultItemCollection.Where(w => w.ResumeDateTime == DateTime.MinValue)
                        .OrderByDescending(o => o.OccuseDateTime)
                        .Select(s => s.ItemConfig.NameAndContent));
        }

        private void OnNextItem()
        {
            if (SelectedIndex == MaxShowingCount - 1 &&
                CurrentShowingFirstIndex + MaxShowingCount < FaultItemCollection.Count)
            {
                CurrentShowingFirstIndex += 1;
                SelectedIndex = MaxShowingCount - 1;
            }
            else
            {
                SelectedIndex += 1;
            }
        }

        private bool CanNextItem()
        {
            CanGoNextItem = UpdateCanNextItem();

            return CanGoNextItem;
        }

        private bool UpdateCanNextItem()
        {
            if (!ShowingItems.Any())
            {
                return false;
            }

            if (SelectedIndex < 0)
            {
                return true;
            }

            var last = ShowingItems.ElementAt(SelectedIndex);

            if (last != FaultItemCollection.LastOrDefault())
            {
                return true;
            }

            return false;
        }

        private void OnPreItem()
        {
            if (SelectedIndex <= 0 && CurrentShowingFirstIndex > 0)
            {
                CurrentShowingFirstIndex -= 1;
                SelectedIndex = 0;
            }
            else
            {
                SelectedIndex -= 1;
            }
        }

        private bool CanPreItem()
        {
            CanGoPreItem = UpdateCanGoPreItem();
            return CanGoPreItem;
        }

        private bool UpdateCanGoPreItem()
        {
            if (!ShowingItems.Any())
            {
                return false;
            }

            return CurrentShowingFirstIndex > 0 || SelectedIndex > 0;
        }

        private void OnNextPage()
        {
            CurrentShowingFirstIndex += MaxShowingCount;
        }

        private bool CanNextPage()
        {
            CanGoNextPage = UpdateCanGoCanNextPage();
            return CanGoNextPage;
        }

        private bool UpdateCanGoCanNextPage()
        {
            if (!ShowingItems.Any())
            {
                return false;
            }

            return CurrentShowingFirstIndex + MaxShowingCount < FaultItemCollection.Count;
        }

        private void OnPrePage()
        {
            CurrentShowingFirstIndex = Math.Max(0, CurrentShowingFirstIndex - MaxShowingCount);
        }

        private bool CanPrePage()
        {
            CanGoPrePage = UpdateCanGoPrePage();
            return CanGoPrePage;
        }

        private bool UpdateCanGoPrePage()
        {
            if (!ShowingItems.Any())
            {
                return false;
            }

            return CurrentShowingFirstIndex > 0;
        }

        private void OnNavigateToDescriptionExcute()
        {
            m_RegionManager.RequestNavigate(RegionNames.RegionFaultContent, typeof(FaultHandlerDescriptionView).FullName);
        }

        public void Reset()
        {
            SelectedIndex = -1;
            CurrentShowingFirstIndex = 0;
            PopingFaultItemNames.Clear();
            FaultItemCollection.Clear();
        }
    }
}