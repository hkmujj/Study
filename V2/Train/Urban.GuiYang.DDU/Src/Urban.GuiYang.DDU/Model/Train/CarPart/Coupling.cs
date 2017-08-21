using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class Coupling : CarPartBase
    {
        private CouplingState m_CouplingState;

        public CouplingState CouplingState
        {
            get { return m_CouplingState; }
            set
            {
                if (value == m_CouplingState)
                {
                    return;
                }

                m_CouplingState = value;
                RaisePropertyChanged(() => CouplingState);
            }
        }
    }
}