using Microsoft.Practices.Prism.ViewModel;
using System.Drawing;

namespace Urban.ATC.Siemens.Model.ViewModel
{
    public class RegionEViewModel : NotificationObject
    {
        private string m_Logo;
        private string m_CurrentTime;
        private Color m_LogoColor;
        private Color m_TimeColor;

        public Color LogoColor
        {
            get { return m_LogoColor; }
            set
            {
                if (value.Equals(m_LogoColor))
                {
                    return;
                }
                m_LogoColor = value;
                RaisePropertyChanged(() => LogoColor);
            }
        }

        public Color TimeColor
        {
            get { return m_TimeColor; }
            set
            {
                if (value.Equals(m_TimeColor))
                {
                    return;
                }
                m_TimeColor = value;
                RaisePropertyChanged(() => TimeColor);
            }
        }

        public string Logo
        {
            get { return m_Logo; }
            set
            {
                if (value == m_Logo)
                {
                    return;
                }
                m_Logo = value;
                RaisePropertyChanged(() => Logo);
            }
        }

        public string CurrentTime
        {
            get { return m_CurrentTime; }
            set
            {
                if (value == m_CurrentTime)
                {
                    return;
                }
                m_CurrentTime = value;
                RaisePropertyChanged(() => CurrentTime);
            }
        }
    }
}