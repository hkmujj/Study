using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.Domain.Details
{
    public class PowerSupplyUnit : NotificationObject
    {
        private float m_Current;
        private float m_Voltage;

        public float Voltage
        {
            get { return m_Voltage; }
            set
            {
                if (value.Equals(m_Voltage))
                {
                    return;
                }

                m_Voltage = value;
                RaisePropertyChanged(() => Voltage);
            }
        }

        public float Current
        {
            get { return m_Current; }
            set
            {
                if (value.Equals(m_Current))
                {
                    return;
                }

                m_Current = value;
                RaisePropertyChanged(() => Current);
            }
        }
    }
}