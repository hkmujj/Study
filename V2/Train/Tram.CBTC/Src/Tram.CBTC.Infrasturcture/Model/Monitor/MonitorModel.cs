using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Monitor.Detail;

namespace Tram.CBTC.Infrasturcture.Model.Monitor
{
    public class MonitorModel : NotificationObject
    {
        private SendeMonitor m_SendeMonitor;

        public MonitorModel()
        {
            MsgCreater = new MsgCreater();
        }

        public MsgCreater MsgCreater { get; set; }

        public SendeMonitor SendeMonitor
        {
            get { return m_SendeMonitor; }
            set
            {
                if (Equals(value, m_SendeMonitor))
                    return;

                m_SendeMonitor = value;
                RaisePropertyChanged(() => SendeMonitor);
            }
        }
    }
}