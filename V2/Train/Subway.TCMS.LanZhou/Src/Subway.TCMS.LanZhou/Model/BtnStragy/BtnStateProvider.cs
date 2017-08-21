using Microsoft.Practices.Prism.ViewModel;

namespace Subway.TCMS.LanZhou.Model.BtnStragy
{
    public class BtnStateProvider : NotificationObject
    {
        private string m_Content;
        private bool m_IsEnabled;
        private bool m_IsSelected;

        public BtnStateProvider()
        {
            IsEnabled = true;
        }

        public string Content
        {
            set
            {
                if (value == m_Content)
                {
                    return;
                }

                m_Content = value;
                RaisePropertyChanged(() => Content);
            }
            get { return m_Content; }
        }

        public bool IsEnabled
        {
            get { return m_IsEnabled; }
            set
            {
                if (value == m_IsEnabled)
                {
                    return;
                }
                m_IsEnabled = value;
                RaisePropertyChanged(() => IsEnabled);
            }
        }

        public bool IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                if (value == m_IsSelected)
                {
                    return;
                }
                m_IsSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }
 
    }
}