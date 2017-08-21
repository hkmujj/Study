using Motor.ATP.Infrasturcture.Interface;

// ReSharper disable once CheckNamespace
namespace Motor.ATP.Infrasturcture.Model
{
    public class KilometerPost : TrainInfoPartialBase, IKilometerPost
    {
        private double m_Kilometer;
        private double m_Meter;
        private bool m_Visible;


        public KilometerPost(ITrainInfo parent)
            : base(parent)
        {
            Visible = true;
        }


        public double Kilometer
        {
            get { return m_Kilometer; }
            set
            {
                if (value.Equals(m_Kilometer))
                {
                    return;
                }

                m_Kilometer = value;
                RaisePropertyChanged(() => Kilometer);
            }
        }

        public double Meter
        {
            get { return m_Meter; }
            set
            {
                if (value.Equals(m_Meter))
                {
                    return;
                }
                m_Meter = value;
                RaisePropertyChanged(() => Meter);
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
    }
}