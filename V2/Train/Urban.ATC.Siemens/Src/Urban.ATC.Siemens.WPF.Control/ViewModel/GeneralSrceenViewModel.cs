namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class GeneralSrceenViewModel : ViewModelBase
    {
        private string m_ChinaTitle;
        private string m_EnglishTitle;

        public string ChinaTitle
        {
            get { return m_ChinaTitle; }
            set
            {
                if (value == m_ChinaTitle)
                {
                    return;
                }
                m_ChinaTitle = value;
                RaisePropertyChanged(() => ChinaTitle);
            }
        }

        public string EnglishTitle
        {
            get { return m_EnglishTitle; }
            set
            {
                if (value == m_EnglishTitle)
                {
                    return;
                }
                m_EnglishTitle = value;
                RaisePropertyChanged(() => EnglishTitle);
            }
        }

        public string TripNumber { get; set; }
        public string CrewNumber { get; set; }
        public string DestinationNumber { get; set; }
    }
}