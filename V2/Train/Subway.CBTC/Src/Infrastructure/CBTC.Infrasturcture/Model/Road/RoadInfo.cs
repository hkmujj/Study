using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Road.Station;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Road
{
    public class RoadInfo : NotificationObject
    {
        private string m_DesID;
        private string m_TrainID;
        private string m_DriverID;
        private ReturnState m_ReturnState;
        private SpecialInfo m_SpecialInfo;

        public RoadInfo()
        {
            StationInfo = new StationInfo();
        }

        public StationInfo StationInfo { get; protected set; }

        /// <summary>
        /// 折返信息
        /// </summary>
        public ReturnState ReturnState
        {
            get { return m_ReturnState; }
            set
            {
                if (value == m_ReturnState)
                    return;

                m_ReturnState = value;
                RaisePropertyChanged(() => ReturnState);
            }
        }

        /// <summary>
        /// 司机号
        /// </summary>
        public string DriverID
        {
            get { return m_DriverID; }
            set
            {
                if (value == m_DriverID)
                    return;

                m_DriverID = value;
                RaisePropertyChanged(() => DriverID);
            }
        }

        /// <summary>
        /// 车次号
        /// </summary>
        public string TrainID
        {
            get { return m_TrainID; }
            set
            {
                if (value == m_TrainID)
                    return;

                m_TrainID = value;
                RaisePropertyChanged(() => TrainID);
            }
        }

        /// <summary>
        /// 目的地
        /// </summary>
        public string DesID
        {
            get { return m_DesID; }
            set
            {
                if (value == m_DesID)
                    return;

                m_DesID = value;
                RaisePropertyChanged(() => DesID);
            }
        }
        /// <summary>
        /// 特殊信息
        /// </summary>
        public SpecialInfo SpecialInfo
        {
            get { return m_SpecialInfo; }
            set
            {
                if (value == m_SpecialInfo)
                    return;
                m_SpecialInfo = value;
                RaisePropertyChanged(() => SpecialInfo);
            }
        }
    }
}