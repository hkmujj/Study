using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Model.Constant;

namespace Urban.GuiYang.DDU.Model.Train.CarPart
{
    public class ValidateFloatStateItem : NotificationObject
    {
        private bool m_GeneralState;
        private float m_Value;
        private BatteryChargerState m_BatteryChargerState;

        public BatteryChargerState BatteryChargerState
        {
            get { return m_BatteryChargerState; }
            set
            {
                if (value.Equals(m_BatteryChargerState))
                {
                    return;
                }
                m_BatteryChargerState = value;
                RaisePropertyChanged(() => BatteryChargerState);
            }
        }
        public bool GeneralState
        {
            get { return m_GeneralState; }
            set
            {
                if (value.Equals(m_GeneralState))
                {
                    return;
                }
                m_GeneralState = value;
                RaisePropertyChanged(() => GeneralState);
            }
        }

        public float Value
        {
            get { return m_Value; }
            set
            {
                if (value.Equals(m_Value))
                {
                    return;
                }
                m_Value = value;
                RaisePropertyChanged(() => Value);
            }
        }
    }
}