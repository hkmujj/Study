using Microsoft.Practices.Prism.ViewModel;
using System.Drawing;

namespace Urban.ATC.Siemens.Model.ViewModel
{
    public class MenuViewModel : NotificationObject
    {
        private string m_MenuContent;
        private Color m_TextColor;

        public string MenuContent
        {
            get { return m_MenuContent; }
            set
            {
                if (value == m_MenuContent)
                {
                    return;
                }
                m_MenuContent = value;
                RaisePropertyChanged(() => MenuContent);
            }
        }

        public Color TextColor
        {
            get { return m_TextColor; }
            set
            {
                if (value.Equals(m_TextColor))
                {
                    return;
                }
                m_TextColor = value;
                RaisePropertyChanged(() => TextColor);
            }
        }
    }
}