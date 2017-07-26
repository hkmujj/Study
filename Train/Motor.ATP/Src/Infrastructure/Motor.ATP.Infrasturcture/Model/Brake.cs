using Motor.ATP.Infrasturcture.Interface;

namespace Motor.ATP.Infrasturcture.Model
{
    public class Brake : TrainInfoPartialBase, IBrake
    {
        private BrakeType m_BrakeType;
        private bool m_Visible;


        public BrakeType BrakeType
        {
            get { return m_BrakeType; }
            set
            {
                if (value == m_BrakeType)
                {
                    return;
                }
                m_BrakeType = value;
                RaisePropertyChanged(() => BrakeType);
            }
        }

        public Brake(ITrainInfo parent)
            : base(parent)
        {
            Visible = false;
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
