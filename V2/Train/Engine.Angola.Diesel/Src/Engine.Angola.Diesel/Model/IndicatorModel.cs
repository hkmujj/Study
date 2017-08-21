using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.Angola.Diesel.Model
{
    [Export]
    public class IndicatorModel : NotificationObject
    {
        private float m_LedData;
        private NotificationRectange m_IndicatorRectange;
        private bool m_State14;
        private bool m_State13;
        private bool m_State12;
        private bool m_State11;
        private bool m_State10;
        private bool m_State04;
        private bool m_State03;
        private bool m_State02;
        private bool m_State01;
        private bool m_State00;

        public float LedData
        {
            set
            {
                if (value.Equals(m_LedData))
                {
                    return;
                }

                m_LedData = value;
                RaisePropertyChanged(() => LedData);
            }
            get { return m_LedData; }
        }

        public bool State00
        {
            set
            {
                if (value == m_State00)
                {
                    return;
                }

                m_State00 = value;
                RaisePropertyChanged(() => State00);
            }
            get { return m_State00; }
        }

        public bool State01
        {
            set
            {
                if (value == m_State01)
                {
                    return;
                }

                m_State01 = value;
                RaisePropertyChanged(() => State01);
            }
            get { return m_State01; }
        }

        public bool State02
        {
            set
            {
                if (value == m_State02)
                {
                    return;
                }

                m_State02 = value;
                RaisePropertyChanged(() => State02);
            }
            get { return m_State02; }
        }

        public bool State03
        {
            set
            {
                if (value == m_State03)
                {
                    return;
                }

                m_State03 = value;
                RaisePropertyChanged(() => State03);
            }
            get { return m_State03; }
        }

        public bool State04
        {
            set
            {
                if (value == m_State04)
                {
                    return;
                }

                m_State04 = value;
                RaisePropertyChanged(() => State04);
            }
            get { return m_State04; }
        }

        public bool State10
        {
            set
            {
                if (value == m_State10)
                {
                    return;
                }

                m_State10 = value;
                RaisePropertyChanged(() => State10);
            }
            get { return m_State10; }
        }

        public bool State11
        {
            set
            {
                if (value == m_State11)
                {
                    return;
                }

                m_State11 = value;
                RaisePropertyChanged(() => State11);
            }
            get { return m_State11; }
        }

        public bool State12
        {
            set
            {
                if (value == m_State12)
                {
                    return;
                }

                m_State12 = value;
                RaisePropertyChanged(() => State12);
            }
            get { return m_State12; }
        }

        public bool State13
        {
            set
            {
                if (value == m_State13)
                {
                    return;
                }

                m_State13 = value;
                RaisePropertyChanged(() => State13);
            }
            get { return m_State13; }
        }

        public bool State14
        {
            set
            {
                if (value == m_State14)
                {
                    return;
                }

                m_State14 = value;
                RaisePropertyChanged(() => State14);
            }
            get { return m_State14; }
        }

        public NotificationRectange IndicatorRectange
        {
            set
            {
                if (Equals(value, m_IndicatorRectange))
                {
                    return;
                }

                m_IndicatorRectange = value;
                RaisePropertyChanged(() => IndicatorRectange);
            }
            get { return m_IndicatorRectange; }
        }
    }
}