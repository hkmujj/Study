using Motor.ATP.Domain.Interface;

namespace Motor.ATP.Domain.Model
{
    public class Driver : TrainInfoPartialBase, IDriver
    {
        private bool m_TrainIdVisible;
        private string m_DriverId;
        private string m_TrainId;


        public Driver(ITrainInfo parent)
            : base(parent)
        {
            DriverId = "ABCD123";
            TrainId = "A12345";
        }

        public string DriverId
        {
            get { return m_DriverId; }
            set
            {
                if (value == m_DriverId)
                {
                    return;
                }
                m_DriverId = value;
                RaisePropertyChanged(() => DriverId);
            }
        }

        public string TrainId
        {
            get { return m_TrainId; }
            set
            {
                if (value == m_TrainId)
                {
                    return;
                }
                m_TrainId = value;
                RaisePropertyChanged(() => TrainId);
            }
        }

        public bool TrainIdVisible
        {
            get { return m_TrainIdVisible; }
            set
            {
                if (value == m_TrainIdVisible)
                {
                    return;
                }
                m_TrainIdVisible = value;
                RaisePropertyChanged(() => TrainIdVisible);
            }
        }
    }
}
