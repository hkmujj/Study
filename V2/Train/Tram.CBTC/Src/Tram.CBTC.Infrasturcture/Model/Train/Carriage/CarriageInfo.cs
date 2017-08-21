using System;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Train.Carriage
{
    /// <summary>
    /// 车厢信息
    /// </summary>
    public class CarriageInfo : NotificationObject
    {
        private int m_CurCabStatus;
        private Cab m_CurrentCab;
        private Cab m_RemoteCab;
        private VOBCOnBoardHostStatus m_VOBCState;

        public CarriageInfo()
        {
            CurrentCab = new Cab();
            RemoteCab = new Cab();
        }

        /// <summary>
        /// 当前为车首（本端）：0，车尾（远端）：1，两端：2
        /// </summary>
        public int CurCabStatus
        {
            get { return m_CurCabStatus; }
            set
            {
                if (value == m_CurCabStatus)
                    return;

                m_CurCabStatus = value;

                CurrentCab.OnOBCUStatusChangedEvent();
                RemoteCab.OnOBCUStatusChangedEvent();

                RaisePropertyChanged(() => CurCabStatus);
            }
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
        /// VOBC 车载主机状态
        /// </summary>
        public VOBCOnBoardHostStatus VOBCState
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