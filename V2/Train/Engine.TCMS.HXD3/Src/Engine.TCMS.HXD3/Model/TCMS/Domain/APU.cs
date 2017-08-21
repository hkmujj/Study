using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model.TCMS.Domain
{
    public class APU : NotificationObject
    {
        private float m_OutputFreq;
        private float m_OutputCurrent;
        private float m_OutputVoltage;
        private APUWorkState m_State;

        public APUWorkState State
        {
            set
            {
                if (value == m_State)
                {
                    return;
                }

                m_State = value;
                RaisePropertyChanged(() => State);
            }
            get { return m_State; }
        }

        public float OutputVoltage
        {
            set
            {
                if (value.Equals(m_OutputVoltage))
                {
                    return;
                }

                m_OutputVoltage = value;
                RaisePropertyChanged(() => OutputVoltage);
            }
            get { return m_OutputVoltage; }
        }

        public float OutputCurrent
        {
            set
            {
                if (value.Equals(m_OutputCurrent))
                {
                    return;
                }

                m_OutputCurrent = value;
                RaisePropertyChanged(() => OutputCurrent);
            }
            get { return m_OutputCurrent; }
        }

        public float OutputFreq
        {
            set
            {
                if (value.Equals(m_OutputFreq))
                {
                    return;
                }

                m_OutputFreq = value;
                RaisePropertyChanged(() => OutputFreq);
            }
            get { return m_OutputFreq; }
        }

        public void CopyDataFrom(APU other)
        {
            if (other == null)
            {
                return;
            }

            State = other.State;
            OutputVoltage = other.OutputVoltage;
            OutputCurrent = other.OutputCurrent;
            OutputFreq = other.OutputFreq;
        }
    }
}