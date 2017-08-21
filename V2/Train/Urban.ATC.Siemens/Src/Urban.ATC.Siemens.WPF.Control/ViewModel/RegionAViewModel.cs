using System.Windows;
using Urban.ATC.Domain.Interface;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class RegionAViewModel : ViewModelBase
    {
        private BrakeDetailsViewModel m_BrakeDetails;
        private double m_TagertSpeed;
        private double m_TagertNum;
        private TargetBarType m_TargetBarType;
        private Visibility m_TagertNumVisibility;
        private Visibility m_TagertSpeedVisibility;

        public RegionAViewModel()
        {
            BrakeDetails = new BrakeDetailsViewModel();
            TagertSpeed = 0;
            TagertNum = 0.1;
            TargetBarType = TargetBarType.LightGreen;
        }

        public TargetBarType TargetBarType
        {
            get { return m_TargetBarType; }
            set
            {
                if (value == m_TargetBarType)
                {
                    return;
                }
                m_TargetBarType = value;
                RaisePropertyChanged(() => TargetBarType);
            }
        }

        public BrakeDetailsViewModel BrakeDetails
        {
            get { return m_BrakeDetails; }
            set
            {
                if (Equals(value, m_BrakeDetails))
                {
                    return;
                }
                m_BrakeDetails = value;
                RaisePropertyChanged(() => BrakeDetails);
            }
        }

        public Visibility TagertSpeedVisibility
        {
            get { return m_TagertSpeedVisibility; }
            set
            {
                if (value == m_TagertSpeedVisibility)
                {
                    return;
                }
                m_TagertSpeedVisibility = value;
                RaisePropertyChanged(() => TagertSpeedVisibility);
            }
        }

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
                RaisePropertyChanged(() => TagertSpeed);
            }
        }

        public Visibility TagertNumVisibility
        {
            get { return m_TagertNumVisibility; }
            set
            {
                if (value == m_TagertNumVisibility)
                {
                    return;
                }
                m_TagertNumVisibility = value;
                RaisePropertyChanged(() => TagertNumVisibility);
            }
        }

        public double TagertNum
        {
            get { return m_TagertNum; }
            set
            {
                if (value.Equals(m_TagertNum))
                {
                    return;
                }
                m_TagertNum = value;
                RaisePropertyChanged(() => TagertNum);
            }
        }
    }
}