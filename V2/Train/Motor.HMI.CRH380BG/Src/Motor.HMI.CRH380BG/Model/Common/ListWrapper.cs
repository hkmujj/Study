using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Microsoft.Practices.Prism.ViewModel;

namespace Motor.HMI.CRH380BG.Model.Common
{
    [DebuggerDisplay("CurrentPageIndex={CurrentListIndex}, ListItemCount={ListItemCount}")]
    public class ListWrapper<T> : NotificationObject where T : class
    {
        private IList<T> m_RawData;
        private int m_CurrentListIndex;
        private int m_ItemsIndex;
        private bool m_IsfaultsTate;
        private bool m_AllFaultReadState;

        public bool AllFaultReadState
        {
            get { return m_AllFaultReadState; }
            set
            {
                if (value == m_AllFaultReadState)
                    return;
                m_AllFaultReadState = value;
                RaisePropertyChanged(() => AllFaultReadState);
            }
        }


        public int ItemsIndex
        {
            get { return m_ItemsIndex; }
            set
            {
                if (value == m_ItemsIndex)
                    return;
                m_ItemsIndex = value;
                RaisePropertyChanged(() => ItemsIndex);
            }
        }


        private int m_ListItemCount;
        private ObservableCollection<T> m_Items;

        private readonly bool m_AddNullToFillPage;

        [DebuggerStepThrough]
        public ListWrapper(Func<T, bool> predicate = null, bool addNullToFillPage = false)
        {
            ItemNumber = 20;
            SkipCount = 0;
            CurrentListIndex = 0;
            m_AddNullToFillPage = addNullToFillPage;
            ItemsIndex = 0;
            AllFaultReadState = false;
            if (predicate == null)
            {
                predicate = it => true;
                //predicate =(x,idx)=> true;
            }
            Predicate = predicate;
            UpdateItems();
        }

        public int ItemNumber { get; set; }
        public int SkipCount { get; set; }

        

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


        public int CurrentListIndex
        {
            get { return m_CurrentListIndex; }
            set
            {
                if (value == m_CurrentListIndex)
                    return;
                m_CurrentListIndex = value;
                
                RaisePropertyChanged(() => CurrentListIndex);
                
                RaisePropertyChanged(() => Items);
                UpdateItems();
            }
        }

       


        public int ListItemCount
        {
            get { return m_ListItemCount; }
            set
            {
                if (value == m_ListItemCount)
                    return;
                m_ListItemCount = value;
               
                RaisePropertyChanged(() => ListItemCount);
                RaisePropertyChanged(() => Items);
                UpdateItems();
            }
        }

        public bool IsfaultsTate
        {
            get { return m_IsfaultsTate; }
            set
            {
                if (value == m_IsfaultsTate)
                    return;
                m_IsfaultsTate = value;
                RaisePropertyChanged(() => IsfaultsTate);
            }
        }


        public void UpdateItems()
        {
            if (m_RawData != null)
            {
                Items =
                    new ObservableCollection<T>(
                        m_RawData.Where(Predicate).Skip(SkipCount).Take(ItemNumber));


            }

        }

        //public int ItemsShowItemNumber()



        public void Reset(IList<T> rawData)
        {
            Contract.Assert(rawData != null);

            m_RawData = rawData;

            var cnt = rawData.Where(Predicate).Count();

            ListItemCount = cnt;

            UpdateItems();

            IsIncludedFaultState();
            RaisePropertyChanged(() => Items);

        }

        public void IsIncludedFaultState()
        {
            if (ListItemCount <= 0)
            {
                IsfaultsTate = false;
            }
            else
            {
                IsfaultsTate = true;
            }
        }
    }
}
