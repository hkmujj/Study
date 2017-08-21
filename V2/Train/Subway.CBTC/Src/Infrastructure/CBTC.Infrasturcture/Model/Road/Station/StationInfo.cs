using CBTC.Infrasturcture.Model.Road.Station.PSD;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Road.Station
{
    public class StationInfo : NotificationObject
    {
        private string m_EndStation;
        private string m_StartStation;
        private string m_NextStation;
        private string m_CurrentStation;
        private PSDInfo m_PSD;

        public StationInfo()
        {
            PSD = new PSDInfo();
        }

        public PSDInfo PSD
        {
            get { return m_PSD; }
            protected set
            {
                if (Equals(value, m_PSD))
                    return;

                m_PSD = value;
                RaisePropertyChanged(() => PSD);
            }
        }

        public string CurrentStation
        {
            get { return m_CurrentStation; }
            set
            {
                if (value == m_CurrentStation)
                    return;

                m_CurrentStation = value;
                RaisePropertyChanged(() => CurrentStation);
            }
        }

        public string NextStation
        {
            get { return m_NextStation; }
            set
            {
                if (value == m_NextStation)
                    return;

                m_NextStation = value;
                RaisePropertyChanged(() => NextStation);
            }
        }

        public string StartStation
        {
            get { return m_StartStation; }
            set
            {
                if (value == m_StartStation)
                    return;

                m_StartStation = value;
                RaisePropertyChanged(() => StartStation);
            }
        }

        public string EndStation
        {
            get { return m_EndStation; }
            set
            {
                if (value == m_EndStation)
                    return;

                m_EndStation = value;
                RaisePropertyChanged(() => EndStation);
            }
        }
    }
}