using System;
using Microsoft.Practices.Prism.ViewModel;

namespace Tram.CBTC.Infrasturcture.Model.Road
{
    public class TimeTableItem : NotificationObject
    {
        private string m_StationName;
        private string m_ArrieveTime;
        private string m_DepartureTime;


        /// <summary>
        /// 如果是站点  需要ID
        /// </summary>
        public ulong ID { get; set; }

 

        /// <summary>
        /// 站点名称
        /// </summary>
        public string StationName
        {
            get { return m_StationName; }
            set
            {
                if (value == m_StationName)
                    return;
                m_StationName = value;
                RaisePropertyChanged(() => StationName);
            }
        }


        /// <summary>
        /// 到站时间
        /// </summary>
        public string ArrieveTime
        {
            get { return m_ArrieveTime; }
            set
            {
                if (value == m_ArrieveTime)
                    return;
                m_ArrieveTime = value;
                RaisePropertyChanged(() => ArrieveTime);
            }
        }

        /// <summary>
        /// 发车时间
        /// </summary>
        public string DepartureTime
        {
            get { return m_DepartureTime; }
            set
            {
                if (value == m_DepartureTime)
                    return;
                m_DepartureTime = value;
                RaisePropertyChanged(() => DepartureTime);
            }
        }
    }
}
