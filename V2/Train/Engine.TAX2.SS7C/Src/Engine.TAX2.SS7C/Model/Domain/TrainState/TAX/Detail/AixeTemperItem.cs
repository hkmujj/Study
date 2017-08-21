using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.Domain.TrainState.TAX.Detail
{
    public class AixeTemperItem :NotificationObject
    {
        private float m_Bit1;
        private float m_Bit6;
        private float m_Bit5;
        private float m_Bit4;
        private float m_Bit3;
        private float m_Bit2;

        public float Bit1
        {
            get { return m_Bit1; }
            set
            {
                if (value.Equals(m_Bit1))
                {
                    return;
                }

                m_Bit1 = value;
                RaisePropertyChanged(() => Bit1);
            }
        }

        public float Bit2
        {
            get { return m_Bit2; }
            set
            {
                if (value.Equals(m_Bit2))
                {
                    return;
                }

                m_Bit2 = value;
                RaisePropertyChanged(() => Bit2);
            }
        }

        public float Bit3
        {
            get { return m_Bit3; }
            set
            {
                if (value.Equals(m_Bit3))
                {
                    return;
                }

                m_Bit3 = value;
                RaisePropertyChanged(() => Bit3);
            }
        }

        public float Bit4
        {
            get { return m_Bit4; }
            set
            {
                if (value.Equals(m_Bit4))
                {
                    return;
                }

                m_Bit4 = value;
                RaisePropertyChanged(() => Bit4);
            }
        }

        public float Bit5
        {
            get { return m_Bit5; }
            set
            {
                if (value.Equals(m_Bit5))
                {
                    return;
                }

                m_Bit5 = value;
                RaisePropertyChanged(() => Bit5);
            }
        }

        public float Bit6
        {
            get { return m_Bit6; }
            set
            {
                if (value.Equals(m_Bit6))
                {
                    return;
                }

                m_Bit6 = value;
                RaisePropertyChanged(() => Bit6);
            }
        }
    }
}