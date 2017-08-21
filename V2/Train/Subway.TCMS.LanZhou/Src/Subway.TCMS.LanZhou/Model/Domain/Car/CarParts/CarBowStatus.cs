using Subway.TCMS.LanZhou.Model.Domain.Constant;

namespace Subway.TCMS.LanZhou.Model.Domain.Car.CarParts
{
    public class CarBowStatus:CarPartBase
    {
        private BowStatus m_BowStatus;

        public BowStatus TrainBowStatus
        {
            get { return m_BowStatus; }
            set
            {
                if (value == m_BowStatus)
                {
                    return;
                }

                m_BowStatus = value;
                RaisePropertyChanged(() => TrainBowStatus);
            }
        }
    }
}
