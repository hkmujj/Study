using System;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Infrasturcture.Model.Constant;

namespace Tram.CBTC.Infrasturcture.Model.Train.Carriage
{
    /// <summary>
    /// 司机室
    /// </summary>
    public class Cab : NotificationObject
    {
        private OBCUStatus m_State;
        /// <summary>
        /// 司机室状态
        /// </summary>
        public OBCUStatus State
        {
            get { return m_State; }
            set
            {
                if (value == m_State)
                    return;

                m_State = value;

                OnOBCUStatusChangedEvent();

                RaisePropertyChanged(() => State);
            }
        }


        public void OnOBCUStatusChangedEvent()
        {
            var handler = OBCUStatusChangedEvent;
            if (handler != null)
            {
                handler();
            }
        }

        /// <summary>
        /// 司机室状态使能通知事件
        /// 0：车首OBCU，1：车尾OBCU，2：车首尾OBCU
        /// </summary>
        public event Action OBCUStatusChangedEvent;
    }
}