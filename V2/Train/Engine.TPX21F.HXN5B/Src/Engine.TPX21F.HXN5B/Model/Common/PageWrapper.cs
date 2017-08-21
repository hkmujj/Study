using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.Model.Common
{
    [DebuggerDisplay("PageCount={PageCount}, CurrentPageIndex={CurrentPageIndex}")]
    public class PageWrapper<T> : NotificationObject  where T : class 
    {
        private IList<T> m_RawData;
        private int m_CurrentPageIndex;
        private int m_PageCount;
        private ObservableCollection<T> m_Items;

        private readonly bool m_AddNullToFillPage;

        [DebuggerStepThrough]
        public PageWrapper(int countPerPage, Func<T, bool> predicate = null, bool addNullToFillPage = false)
        {
            CountPerPage = countPerPage;
            m_AddNullToFillPage = addNullToFillPage;
            if (predicate == null)
            {
                predicate = arg => true;
            }
            Predicate = predicate;
            UpdateItems();
        }

        public int CountPerPage { get; private set; }

        public Func<T, bool> Predicate { private set; get; }

        public ObservableCollection<T> Items
        {
            private set
            {
                if (Equals(value, m_Items))
                {
                    return;
                }

                m_Items = value;
                RaisePropertyChanged(() => Items);
            }
            get { return m_Items; }
        }

        public int PageCount
        {
            private set
            {
                if (value == m_PageCount)
                {
                    return;
                }

                m_PageCount = value;
                UpdateItems();
                RaisePropertyChanged(() => PageCount);
                RaisePropertyChanged(() => Items);
            }
            get { return m_PageCount; }
        }

        public int CurrentPageIndex
        {
            private set
            {
                if (value == m_CurrentPageIndex)
                {
                    return;
                }

                m_CurrentPageIndex = value;
                UpdateItems();
                RaisePropertyChanged(() => CurrentPageIndex);
                RaisePropertyChanged(() => Items);
            }
            get { return m_CurrentPageIndex; }
        }

        public void GotoNextPage()
        {
            CurrentPageIndex = Math.Min(CurrentPageIndex + 1, PageCount - 1);
        }

        public void GotoPrePage()
        {
            CurrentPageIndex = Math.Max(CurrentPageIndex - 1, 0);
        }

        private void UpdateItems()
        {
            if (m_RawData != null)
            {
                Items =
                    new ObservableCollection<T>(
                        m_RawData.Where(Predicate).Skip(CurrentPageIndex*CountPerPage).Take(CountPerPage));

                if (m_AddNullToFillPage && Items.Count < CountPerPage )
                {
                    for (int i = Items.Count; i < CountPerPage; ++i)
                    {
                        Items.Add(null);
                    }
                }
            }
        }

        public void Reset(IList<T> rawData)
        {
            Contract.Assert(rawData != null);

            m_RawData = rawData;

            CurrentPageIndex = 0;

            var cnt = rawData.Where(Predicate).Count();

            PageCount = cnt/CountPerPage + (cnt%CountPerPage != 0 ? 1 : 0);

            UpdateItems();

            RaisePropertyChanged(() => Items);
        }
    }
}