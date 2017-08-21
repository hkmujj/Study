namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class RegionEViewModel : ViewModelBase
    {
        public RegionEViewModel()
        {
            Logo = "SIEMENS";
        }

        private string m_Logo;

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
    }
}