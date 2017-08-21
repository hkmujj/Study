using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Model.Domain.Constant;

namespace Subway.TCMS.LanZhou.Model.Domain.Car
{
    public class CarTowBrakePercentData : NotificationObject
    {      
        private float m_TowBrakePercentValue;
        private CarTowBrakeStatus m_CarTowBrakeStatus;

        public float TowBrakePercentValue
        {
            get { return m_TowBrakePercentValue; }
            set
            {
                if (value.Equals(m_TowBrakePercentValue))
                {
                    return;
                }
                m_TowBrakePercentValue = value;
                RaisePropertyChanged(() => TowBrakePercentValue);
            }
        }

        public CarTowBrakeStatus Status
        {
            get { return m_CarTowBrakeStatus; }
            set
            {
                if (value == m_CarTowBrakeStatus)
                {
                    return;
                }
                m_CarTowBrakeStatus = value;
                RaisePropertyChanged(() => Status);
            }
        }
    }
}
