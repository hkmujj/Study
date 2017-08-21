using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.Model.Domain.TrainState.Detail
{
    public class TruckState : NotificationObject
    {
        private float m_PowerSupplyA;
        private float m_PowerSupplyV;
        private float m_V;
        private float m_UM1;
        private float m_IF1;
        private float m_IM3;
        private float m_IM2;
        private float m_IM1;

        public float IM1
        {
            get { return m_IM1; }
            set
            {
                if (value.Equals(m_IM1))
                {
                    return;
                }

                m_IM1 = value;
                RaisePropertyChanged(() => IM1);
            }
        }

        public float IM2
        {
            get { return m_IM2; }
            set
            {
                if (value.Equals(m_IM2))
                {
                    return;
                }

                m_IM2 = value;
                RaisePropertyChanged(() => IM2);
            }
        }

        public float IM3
        {
            get { return m_IM3; }
            set
            {
                if (value.Equals(m_IM3))
                {
                    return;
                }

                m_IM3 = value;
                RaisePropertyChanged(() => IM3);
            }
        }

        public float IF1
        {
            get { return m_IF1; }
            set
            {
                if (value.Equals(m_IF1))
                {
                    return;
                }

                m_IF1 = value;
                RaisePropertyChanged(() => IF1);
            }
        }

        public float UM1
        {
            get { return m_UM1; }
            set
            {
                if (value.Equals(m_UM1))
                {
                    return;
                }

                m_UM1 = value;
                RaisePropertyChanged(() => UM1);
            }
        }

        public float V
        {
            get { return m_V; }
            set
            {
                if (value.Equals(m_V))
                {
                    return;
                }

                m_V = value;
                RaisePropertyChanged(() => V);
            }
        }

        public float PowerSupplyV
        {
            get { return m_PowerSupplyV; }
            set
            {
                if (value.Equals(m_PowerSupplyV))
                {
                    return;
                }

                m_PowerSupplyV = value;
                RaisePropertyChanged(() => PowerSupplyV);
            }
        }

        public float PowerSupplyA
        {
            get { return m_PowerSupplyA; }
            set
            {
                if (value.Equals(m_PowerSupplyA))
                {
                    return;
                }

                m_PowerSupplyA = value;
                RaisePropertyChanged(() => PowerSupplyA);
            }
        }
    }
}