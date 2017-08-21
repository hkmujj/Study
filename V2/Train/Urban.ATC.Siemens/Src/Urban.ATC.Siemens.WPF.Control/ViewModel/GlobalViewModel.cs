using Microsoft.Practices.Prism.ViewModel;
using System.Drawing;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class GlobalViewModel : NotificationObject
    {
        public GlobalViewModel()
        {
            BackColor = GDICommon.BacgroundColor;
            LightGreyColor = GDICommon.LightGreyColor;
            LogoColor = GDICommon.LogColor;
        }

        private Color m_BackColor;
        private Color m_LightGreyColor;
        private Color m_LogoColor;

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

        public Color BackColor
        {
            get { return m_BackColor; }
            set
            {
                if (value.Equals(m_BackColor))
                {
                    return;
                }
                m_BackColor = value;
                RaisePropertyChanged(() => BackColor);
            }
        }

        public Color LightGreyColor
        {
            get { return m_LightGreyColor; }
            set
            {
                if (value.Equals(m_LightGreyColor))
                {
                    return;
                }
                m_LightGreyColor = value;
                RaisePropertyChanged(() => LightGreyColor);
            }
        }
    }
}