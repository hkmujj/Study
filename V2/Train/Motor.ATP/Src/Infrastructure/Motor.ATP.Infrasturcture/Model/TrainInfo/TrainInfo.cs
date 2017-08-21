using System.Collections.ObjectModel;
using System.Linq;
using Motor.ATP.Infrasturcture.Interface;

// ReSharper disable once CheckNamespace
namespace Motor.ATP.Infrasturcture.Model
{
    public class TrainInfo : ATPPartialBase, ITrainInfo
    {
        /// <summary>
        /// 最大车长个数
        /// </summary>
        public const int MaxTrainLenghtCount = 2;

        private Speed m_Speed;
        private Brake m_Brake;
        private Driver m_Driver;
        private KilometerPost m_KilometerPost;
        private ConnectState m_ConnectState;
        private Station m_Station;
        private ObservableCollection<float> m_TrainLegth;
        private int m_CurrentTrainGroupCount;

        public TrainInfo(ATPDomain parent)
            : base(parent)
        {
            TrainLegth =
                new ObservableCollection<float>(Enumerable.Repeat<float>(float.NaN, MaxTrainLenghtCount));
        }

        /// <summary>
        /// 当前列车个数
        /// </summary>
        public int CurrentTrainGroupCount
        {
            get { return m_CurrentTrainGroupCount; }
            set
            {
                if (value == m_CurrentTrainGroupCount)
                {
                    return;
                }

                if (value < 0)
                {
                    value = 0;
                }

                if (value > MaxTrainLenghtCount)
                {
                    value = MaxTrainLenghtCount;
                }

                m_CurrentTrainGroupCount = value;
                RaisePropertyChanged(() => CurrentTrainGroupCount);
            }
        }

        public ObservableCollection<float> TrainLegth
        {
            get { return m_TrainLegth; }
            private set
            {
                if (value.Equals(m_TrainLegth))
                {
                    return;
                }
                m_TrainLegth = value;
                RaisePropertyChanged(() => TrainLegth);
            }
        }

        public Speed Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; RaisePropertyChanged(() => Speed); }
        }

        public Brake Brake
        {
            get { return m_Brake; }
            set { m_Brake = value; RaisePropertyChanged(() => Brake); }
        }

        public Driver Driver
        {
            get { return m_Driver; }
            set { m_Driver = value; RaisePropertyChanged(() => Driver); }
        }

        public Station Station
        {
            get { return m_Station; }
            set
            {
                if (Equals(value, m_Station))
                {
                    return;
                }
                m_Station = value;
                RaisePropertyChanged(() => Station);
            }
        }

        public KilometerPost KilometerPost
        {
            get { return m_KilometerPost; }
            set { m_KilometerPost = value; RaisePropertyChanged(() => KilometerPost); }
        }

        public ConnectState ConnectState
        {
            get { return m_ConnectState; }
            set
            {
                if (Equals(value, m_ConnectState))
                {
                    return;
                }
                m_ConnectState = value;
                RaisePropertyChanged(() => ConnectState);
            }
        }

        IKilometerPost ITrainInfo.KilometerPost
        {
            get { return this.KilometerPost; }
        }

        IBrake ITrainInfo.Brake
        {
            get { return this.Brake; }
        }

        ISpeed ITrainInfo.Speed
        {
            get { return this.Speed; }
        }

        IDriver ITrainInfo.Driver
        {
            get { return this.Driver; }
        }

        IStation ITrainInfo.Station
        {
            get { return this.Station; }
        }
        IConnectState ITrainInfo.ConnectState
        {
            get { return this.ConnectState; }
        }
    }
}
