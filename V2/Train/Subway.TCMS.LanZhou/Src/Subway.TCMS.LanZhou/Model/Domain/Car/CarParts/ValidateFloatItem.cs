using Microsoft.Practices.Prism.ViewModel;

namespace Subway.TCMS.LanZhou.Model.Domain.Car.CarParts
{
    public class ValidateFloatItem : NotificationObject
    {
        private bool m_GeneralState;
        private float m_Value;

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
