using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MMI.Facility.DataType.Extension;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.TimeTable;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    public class TimeTableProviderService : ITimeTableProviderService
    {
        private ReadOnlyCollection<ITimeTableItem> m_TimeTableItemsDictionary;

        /// <summary>
        /// 时刻表字典
        /// </summary>
        public ReadOnlyCollection<ITimeTableItem> TimeTableItemsDictionary
        {
            get { return m_TimeTableItemsDictionary; }
            set
            {
                m_TimeTableItemsDictionary = value;
                OnTimeTableChanged();
            }
        }

        public event EventHandler TimeTableChanged;

        protected virtual void OnTimeTableChanged()
        {
            TimeTableChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateTimeTable(List<ITimeTableItem> item)
        {
            TimeTableItemsDictionary = new ReadOnlyCollection<ITimeTableItem>(item);
        }
    }
}
