using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class Fire : CarPartBase
    {
        private FireAlert m_FireAlert;

        public FireAlert FireAlert
        {
            get { return m_FireAlert; }
            set
            {
                if (value == m_FireAlert)
                {
                    return;
                }

                m_FireAlert = value;
                RaisePropertyChanged(() => FireAlert);
            }
        }
    }
}