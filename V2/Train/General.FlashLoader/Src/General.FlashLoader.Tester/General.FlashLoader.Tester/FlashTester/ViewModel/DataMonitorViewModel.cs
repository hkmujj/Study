using System.ComponentModel;

namespace Yunda.FlashTester.ViewModel
{
    public class DataMonitorViewModel : INotifyPropertyChanged
    {
        private float m_ActureSpeed;
        private float m_SettingSpeed;

        public float ActureSpeed
        {
            set
            {
                m_ActureSpeed = value;
                RaisePropertyChanged("ActureSpeed");
            }
            get { return m_ActureSpeed; }
        }

        public float SettingSpeed
        {
            set { m_SettingSpeed = value; RaisePropertyChanged("SettingSpeed"); }
            get { return m_SettingSpeed; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}