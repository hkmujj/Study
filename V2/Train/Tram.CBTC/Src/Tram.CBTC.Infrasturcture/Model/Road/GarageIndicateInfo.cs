using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Road
{
    public class GarageIndicateInfo : NotificationObject
    {
        private GarageIndicate m_GarageIndicate;
        private ulong m_StationID;
       
        
        public GarageIndicateInfo()
        {
           
        }

        /// <summary>
        /// 进出车库指示
        /// </summary>
        public GarageIndicate GarageIndicate
        {
            get { return m_GarageIndicate; }
            set
            {
                if (value == m_GarageIndicate)
                    return;

                m_GarageIndicate = value;
                RaisePropertyChanged(() => GarageIndicate);
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