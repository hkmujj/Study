using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model
{
    public class ConnectState : TrainInfoPartialBase, IConnectState
    {
        private GSMRState m_GSMRState;
        private RBCConnectState m_RBCConnectState;
        private bool m_Visible;
        private string m_RBCID;
        private string m_TelNumber;

        public ConnectState(ITrainInfo parent)
            : base(parent)
        {
            Visible = true;
        }

        public GSMRState GSMRState
        {
            get { return m_GSMRState; }
            set
            {
                if (value == m_GSMRState)
                {
                    return;
                }
                m_GSMRState = value;
                RaisePropertyChanged(() => GSMRState);
            }
        }

        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (value == m_Visible)
                {
                    return;
                }
                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
        }

        public RBCConnectState RBCConnectState
        {
            get { return m_RBCConnectState; }
            set
            {
                if (value == m_RBCConnectState)
                {
                    return;
                }
                m_RBCConnectState = value;
                RaisePropertyChanged(() => RBCConnectState);
            }
        }

        public string RBCID
        {
            get { return m_RBCID; }
            set
            {
                if (value == m_RBCID)
                {
                    return;
                }

                m_RBCID = value;
                RaisePropertyChanged(() => RBCID);
            }
        }

        public string TelNumber
        {
            get { return m_TelNumber; }
            set
            {
                if (value == m_TelNumber)
                {
                    return;
                }

                m_TelNumber = value;
                RaisePropertyChanged(() => TelNumber);
            }
        }
    }
}