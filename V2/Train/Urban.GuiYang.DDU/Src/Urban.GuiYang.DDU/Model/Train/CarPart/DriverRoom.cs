using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class DriverRoom : CarPartBase
    {
        private DriverRoomState m_DriverRoomState;

        public DriverRoomState DriverRoomState
        {
            get { return m_DriverRoomState; }
            set
            {
                if (value == m_DriverRoomState)
                {
                    return;
                }

                m_DriverRoomState = value;
                RaisePropertyChanged(() => DriverRoomState);
            }
        }
    }
}