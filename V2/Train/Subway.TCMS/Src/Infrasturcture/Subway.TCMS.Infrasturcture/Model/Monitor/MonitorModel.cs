using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.Infrasturcture.Model.Monitor.Detail;

namespace Subway.TCMS.Infrasturcture.Model.Monitor
{
    public class MonitorModel : NotificationObject
    {
        private SendMonitor m_SendMonitor;
        private ReciveMonitor m_ReciveMonitor;

        public SendMonitor SendMonitor
        {
            get { return m_SendMonitor; }
            set
            {
                if (Equals(value, m_SendMonitor))
                    return;

                m_SendMonitor = value;
                RaisePropertyChanged(() => SendMonitor);
            }
        }

        public ReciveMonitor ReciveMonitor
        {
            get { return m_ReciveMonitor; }
            set
            {
                if (Equals(value, m_ReciveMonitor))
                    return;

                m_ReciveMonitor = value;
                RaisePropertyChanged(() => ReciveMonitor);
            }
        }
    }
}
