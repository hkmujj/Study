using System.Windows;
using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class RegionFViewModel : ViewModelBase
    {
        private string m_EnglishInfo;
        private string m_ChinaInfo;
        private InfoLevl m_InfoLevl;
        private Visibility m_Visibility;
        private double m_ChinaFontSize;
        private double m_EnglishFontSize;

        public RegionFViewModel()
        {
            m_ChinaInfo = "确认释放速度\r\n以低速接近";
            EnglishInfo = "confirm Release speed";
            InfoLevl = InfoLevl.Yellow;
            Visibility = Visibility.Hidden;
        }

        public Visibility Visibility
        {
            get { return m_Visibility; }
            set
            {
                if (value == m_Visibility)
                {
                    return;
                }
                m_Visibility = value;
                RaisePropertyChanged(() => Visibility);
            }
        }

        public InfoLevl InfoLevl
        {
            get { return m_InfoLevl; }
            set
            {
                if (value == m_InfoLevl)
                {
                    return;
                }
                m_InfoLevl = value;
                RaisePropertyChanged(() => InfoLevl);
            }
        }

        public string ChinaInfo
        {
            get { return m_ChinaInfo; }
            set
            {
                if (value == m_ChinaInfo)
                {
                    return;
                }
                m_ChinaInfo = value;
                SetVisibility();
                RaisePropertyChanged(() => ChinaInfo);
            }
        }

        private void SetVisibility()
        {
            if (string.IsNullOrEmpty(m_ChinaInfo) && string.IsNullOrEmpty(m_EnglishInfo))
            {
                Visibility = Visibility.Hidden;
            }
            else
            {
                Visibility = Visibility.Visible;
            }
        }

        public string EnglishInfo
        {
            get { return m_EnglishInfo; }
            set
            {
                if (value == m_EnglishInfo)
                {
                    return;
                }
                m_EnglishInfo = value;
                SetVisibility();
                RaisePropertyChanged(() => EnglishInfo);
            }
        }

        public double ChinaFontSize
        {
            get { return m_ChinaFontSize; }
            set
            {
                if (value.Equals(m_ChinaFontSize))
                {
                    return;
                }
                m_ChinaFontSize = value;
                RaisePropertyChanged(() => ChinaFontSize);
            }
        }

        public double EnglishFontSize
        {
            get { return m_EnglishFontSize; }
            set
            {
                if (value.Equals(m_EnglishFontSize))
                {
                    return;
                }
                m_EnglishFontSize = value;
                RaisePropertyChanged(() => EnglishFontSize);
            }
        }
    }
}