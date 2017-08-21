using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Road
{
    public class ReturnIndicateInfo : NotificationObject
    {
        private ReturnIndicate m_ReturnIndicate;
        private ulong m_StationID;
       
        
        public ReturnIndicateInfo()
        {
           
        }

        /// <summary>
        /// 折返指示
        /// </summary>
        public ReturnIndicate ReturnIndicate
        {
            get { return m_ReturnIndicate; }
            set
            {
                if (value == m_ReturnIndicate)
                    return;

                m_ReturnIndicate = value;
                RaisePropertyChanged(() => ReturnIndicate);
            }
        }


        /// <summary>
        /// 车站ID
        /// </summary>
        public ulong StationID
        {
            get { return m_StationID; }
            set
            {
                if (value == m_StationID)
                    return;

                m_StationID = value;
                RaisePropertyChanged(() => StationID);
            }
        }
    }
}