using Microsoft.Practices.Prism.ViewModel;
using System.Drawing;

namespace Urban.ATC.Siemens.Model.ViewModel
{
    public class BrakeDetailsViewModel : NotificationObject
    {
        private Color m_BackGroundColor;

        public Color BackGroundColor
        {
            get { return m_BackGroundColor; }
            set
            {
                if (value.Equals(m_BackGroundColor))
                {
                    return;
                }
                m_BackGroundColor = value;
                RaisePropertyChanged(() => BackGroundColor);
            }
        }
    }
}