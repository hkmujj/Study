using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class Pantograph : CarPartBase
    {
        private PantographState m_PantographState;
        private VidioState m_VidioState;

        public PantographState PantographState
        {
            get { return m_PantographState; }
            set
            {
                if (value == m_PantographState)
                {
                    return;
                }

                m_PantographState = value;
                RaisePropertyChanged(() => PantographState);
            }
        }

        public VidioState VidioState
        {
            get { return m_VidioState; }
            set
            {
                if (value == m_VidioState)
                {
                    return;
                }
                m_VidioState = value;
                RaisePropertyChanged(() => VidioState);
            }
        }
    }
}