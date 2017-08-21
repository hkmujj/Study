using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Tram.CBTC.Infrasturcture.Model.Road
{
    public class TimeTableInfo : NotificationObject
    {

        private ReadOnlyCollection<TimeTableItem> m_TimeTables;

        public TimeTableInfo()
        {
            
        }

 
        /// <summary>
        /// 时刻表
        /// </summary>
        public ReadOnlyCollection<TimeTableItem> TimeTables
        {
            get { return m_TimeTables; }
            set
            {
                if (Equals(value, m_TimeTables))
                    return;
                m_TimeTables = value;
                RaisePropertyChanged(() => TimeTables);
            }
        }
    }
}