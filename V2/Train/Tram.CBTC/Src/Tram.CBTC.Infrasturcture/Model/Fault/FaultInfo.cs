using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Fault
{
    public class FaultInfo : NotificationObject
    {
        private bool m_openRadar = true;
        private bool m_LostGPS = false;


        public FaultInfo()
        {
            RadarInfo = new RadarInfo();
        }


        /// <summary>
        /// 开启／关闭雷达
        /// </summary>
        public bool OpenRadar
        {
            get { return m_openRadar; }
            set
            {
                if (value.Equals(m_openRadar))
                    return;
                m_openRadar = value;
                RaisePropertyChanged(() => OpenRadar);
            }
        }

        /// <summary>
        /// 雷达信息
        /// </summary>
        public RadarInfo RadarInfo { get; protected set; }


        /// <summary>
        /// 失去定位
        /// </summary>
        public bool LostGPS
        {
            get { return m_LostGPS; }
            set
            {
                if (value.Equals(m_LostGPS))
                    return;
                m_LostGPS = value;
                RaisePropertyChanged(() => LostGPS);
            }
        }
    }
}