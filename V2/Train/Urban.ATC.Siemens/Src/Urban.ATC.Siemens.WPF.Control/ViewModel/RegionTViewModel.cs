namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class RegionTViewModel : ViewModelBase
    {
        private string m_TripNumber;
        private double m_DestinationNumber;
        private double m_CrewNumber;

        public RegionTViewModel()
        {
            TripNumber = string.Empty;
            DestinationNumber = 0;
            CrewNumber = 0;
        }

        public string TripNumber
        {
            get { return m_TripNumber; }
            set
            {
                if (value.Equals(m_TripNumber))
                {
                    return;
                }
                m_TripNumber = value;
                RaisePropertyChanged(() => TripNumber);
            }
        }

        public double DestinationNumber
        {
            get { return m_DestinationNumber; }
            set
            {
                if (value.Equals(m_DestinationNumber))
                {
                    return;
                }
                m_DestinationNumber = value;
                RaisePropertyChanged(() => DestinationNumber);
            }
        }

        public double CrewNumber
        {
            get { return m_CrewNumber; }
            set
            {
                if (value.Equals(m_CrewNumber))
                {
                    return;
                }
                m_CrewNumber = value;
                RaisePropertyChanged(() => CrewNumber);
            }
        }
    }
}