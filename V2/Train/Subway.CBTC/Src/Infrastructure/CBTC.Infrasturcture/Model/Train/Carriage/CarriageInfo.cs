using CBTC.Infrasturcture.Model.Constant;
using Microsoft.Practices.Prism.ViewModel;


namespace CBTC.Infrasturcture.Model.Train.Carriage
{
    /// <summary>
    /// 车厢信息
    /// </summary>
    public class CarriageInfo : NotificationObject
    {
        private Cab m_CurrentCab;
        private Cab m_RemoteCab;
        private VOBCState m_VOBCState;

        public CarriageInfo()
        {
            CurrentCab = new Cab();
            RemoteCab = new Cab();
        }

        /// <summary>
        /// 车首OBCU
        /// </summary>
        public Cab CurrentCab
        {
            get { return m_CurrentCab; }
            set
            {
                if (value == m_CurrentCab)
                    return;

                m_CurrentCab = value;
                RaisePropertyChanged(() => CurrentCab);
            }
        }

        /// <summary>
        /// 车尾OBCU
        /// </summary>
        public Cab RemoteCab
        {
            get { return m_RemoteCab; }
            set
            {
                if (value == m_RemoteCab)
                    return;

                m_RemoteCab = value;
                RaisePropertyChanged(() => RemoteCab);
            }
        }

        /// <summary>
        /// VOBC状态
        /// </summary>
        public VOBCState VOBCState
        {
            get { return m_VOBCState; }
            set
            {
                if (value == m_VOBCState)
                    return;

                m_VOBCState = value;
                RaisePropertyChanged(() => VOBCState);
            }
        }
    }
}