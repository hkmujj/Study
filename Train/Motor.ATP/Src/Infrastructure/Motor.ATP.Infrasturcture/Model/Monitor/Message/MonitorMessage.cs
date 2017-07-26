using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP.Infrasturcture.Model.Monitor.Message
{
    public class MonitorMessage : NotificationObject
    {
        private int m_MessageId;
        private InformationUpdateType m_UpdateType;

        public int MessageId
        {
            get { return m_MessageId; }
            set
            {
                if (value == m_MessageId)
                {
                    return;
                }

                m_MessageId = value;
                RaisePropertyChanged(() => MessageId);
            }
        }

        public InformationUpdateType UpdateType
        {
            get { return m_UpdateType; }
            set
            {
                if (value == m_UpdateType)
                {
                    return;
                }

                m_UpdateType = value;
                RaisePropertyChanged(() => UpdateType);
            }
        }
    }
}