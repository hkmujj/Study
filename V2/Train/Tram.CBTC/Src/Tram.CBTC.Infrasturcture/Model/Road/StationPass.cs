using Microsoft.Practices.Prism.ViewModel;

namespace Tram.CBTC.Infrasturcture.Model.Road
{
    public class StationPass : NotificationObject
    {
        private bool m_Passed;
        private bool m_IsStation;
        private bool m_Flicker;
        private string m_StationName;
        /// <summary>
        /// 如果是站点  需要ID
        /// </summary>
        public ulong ID { get; set; }

        /// <summary>
        /// 通过
        /// </summary>
        public bool Passed
        {
            get { return m_Passed; }
            set
            {
                if (value == m_Passed)
                    return;
                m_Passed = value;
                RaisePropertyChanged(() => Passed);
            }
        }

        /// <summary>
        /// 是否是站点
        /// </summary>
        public bool IsStation
        {
            get { return m_IsStation; }
            set
            {
                if (value == m_IsStation)
                    return;
                m_IsStation = value;
                RaisePropertyChanged(() => IsStation);
            }
        }

        /// <summary>
        /// 是否闪烁
        /// </summary>
        public bool Flicker
        {
            get { return m_Flicker; }
            set
            {
                if (value == m_Flicker)
                    return;
                m_Flicker = value;
                RaisePropertyChanged(() => Flicker);
            }
        }

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
    }
}
