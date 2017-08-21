using CBTC.Infrasturcture.Model.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace CBTC.Infrasturcture.Model.Train.Carriage
{
    /// <summary>
    /// 司机室
    /// </summary>
    public class Cab : NotificationObject
    {
        private CabState m_State;
        /// <summary>
        /// 司机室状态
        /// </summary>
        public CabState State
        {
            get { return m_State; }
            set
            {
                if (value == m_State)
                    return;

                m_State = value;
                RaisePropertyChanged(() => State);
            }
        }
    }
}