using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model
{
    public class Driver : TrainInfoPartialBase, IDriver
    {
        private bool m_TrainIdVisible;
        private string m_DriverId;
        private string m_TrainId;
        private string m_InputtinTrainId;
        private bool m_IsInputtingTrainId;
        private bool m_IsInputtingDriverId;
        private string m_InputtinDriverId;


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

        /// <summary>
        /// 正在输入的司机号
        /// </summary>
        public string InputtingDriverId
        {
            get { return m_InputtinDriverId; }
            set
            {
                if (value == m_InputtinDriverId)
                    return;

                m_InputtinDriverId = value;
                RaisePropertyChanged(() => InputtingDriverId);
            }
        }

        /// <summary>
        /// 是否正在输入司机号
        /// </summary>
        public bool IsInputtingDriverId
        {
            get { return m_IsInputtingDriverId; }
            set
            {
                if (value == m_IsInputtingDriverId)
                    return;

                m_IsInputtingDriverId = value;
                RaisePropertyChanged(() => IsInputtingDriverId);
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

        /// <summary>
        /// 正在输入的车次号
        /// </summary>
        public string InputtingTrainId
        {
            get { return m_InputtinTrainId; }
            set
            {
                if (value == m_InputtinTrainId)
                    return;

                m_InputtinTrainId = value;
                RaisePropertyChanged(() => InputtingTrainId);
            }
        }

        /// <summary>
        /// 是否正在输入车次号
        /// </summary>
        public bool IsInputtingTrainId
        {
            get { return m_IsInputtingTrainId; }
            set
            {
                if (value == m_IsInputtingTrainId)
                    return;

                m_IsInputtingTrainId = value;
                RaisePropertyChanged(() => IsInputtingTrainId);
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
