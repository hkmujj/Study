using System;
using System.Windows;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class RegionBViewModel : ViewModelBase
    {
        public RegionBViewModel()
        {
            CurrentSpeed = 0;
            TagertSpeed = 0;
            EmergencySpeed = 0;
            CurrntAngle = -140;
            TagertAngle = -140;
            EmergencyAngle = -140;
        }

        private double m_CurrntAngle;
        private double m_CurrentSpeed;
        private double m_EmergencyAngle;
        private double m_TagertAngle;
        private double m_TagertSpeed;
        private double m_EmergencySpeed;
        private Visibility m_PointerVisibility;
        private Visibility m_YellowFlagVisibility;
        private Visibility m_RedFlagVisibility;

        public double TagertSpeed
        {
            get { return m_TagertSpeed; }
            set
            {
                if (value.Equals(m_TagertSpeed))
                {
                    return;
                }
                m_TagertSpeed = (int)value;
                ChangeAnge(value, Angele.Tagert);
                RaisePropertyChanged(() => TagertSpeed);
            }
        }

        public double EmergencySpeed
        {
            get { return m_EmergencySpeed; }
            set
            {
                if (value.Equals(m_EmergencySpeed))
                {
                    return;
                }
                m_EmergencySpeed = (int)value;
                ChangeAnge(value, Angele.Emergency);
                RaisePropertyChanged(() => EmergencySpeed);
            }
        }

        public double TagertAngle
        {
            get { return m_TagertAngle; }
            set
            {
                if (value.Equals(m_TagertAngle))
                {
                    return;
                }
                m_TagertAngle = value;
                RaisePropertyChanged(() => TagertAngle);
            }
        }

        public double EmergencyAngle
        {
            get { return m_EmergencyAngle; }
            set
            {
                if (value.Equals(m_EmergencyAngle))
                {
                    return;
                }
                m_EmergencyAngle = value;
                RaisePropertyChanged(() => EmergencyAngle);
            }
        }

        public double CurrntAngle
        {
            get { return m_CurrntAngle; }
            set
            {
                if (value.Equals(m_CurrntAngle))
                {
                    return;
                }
                m_CurrntAngle = value;
                RaisePropertyChanged(() => CurrntAngle);
            }
        }

        private enum Angele
        {
            Curren,
            Tagert,
            Emergency,
        }

        public Visibility RedFlagVisibility
        {
            get { return m_RedFlagVisibility; }
            set
            {
                if (value == m_RedFlagVisibility)
                {
                    return;
                }
                m_RedFlagVisibility = value;
                RaisePropertyChanged(() => RedFlagVisibility);
            }
        }

        public Visibility YellowFlagVisibility
        {
            get { return m_YellowFlagVisibility; }
            set
            {
                if (value == m_YellowFlagVisibility)
                {
                    return;
                }
                m_YellowFlagVisibility = value;
                RaisePropertyChanged(() => YellowFlagVisibility);
            }
        }

        private void ChangeAnge(double speed, Angele tyAngele)
        {
            switch (tyAngele)
            {
                case Angele.Curren:
                    CurrntAngle = DegreeScaleResource.Instance.ConvertToAngle(Math.Abs(speed) > 100 ? 100 : Math.Abs(speed));
                    break;

                case Angele.Tagert:
                    TagertAngle = DegreeScaleResource.Instance.ConvertToAngle(Math.Abs(speed) > 100 ? 100 : Math.Abs(speed));
                    break;

                case Angele.Emergency:
                    EmergencyAngle = DegreeScaleResource.Instance.ConvertToAngle(Math.Abs(speed) > 100 ? 100 : Math.Abs(speed));
                    break;

                default:
                    throw new ArgumentOutOfRangeException("tyAngele", tyAngele, null);
            }
        }

        public Visibility PointerVisibility
        {
            get { return m_PointerVisibility; }
            set
            {
                if (value == m_PointerVisibility)
                {
                    return;
                }
                m_PointerVisibility = value;
                RaisePropertyChanged(() => PointerVisibility);
            }
        }

        public double CurrentSpeed
        {
            get { return m_CurrentSpeed; }
            set
            {
                if (value.Equals(m_CurrentSpeed))
                {
                    return;
                }
                m_CurrentSpeed = (int)value;
                ChangeAnge(value, Angele.Curren);
                RaisePropertyChanged(() => CurrentSpeed);
            }
        }
    }
}