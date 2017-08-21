using Microsoft.Practices.Prism.ViewModel;
using System.Drawing;

namespace Urban.ATC.Siemens.Model.ViewModel
{
    public class RegionTViewModel : NotificationObject
    {
        private string m_TripNumber;
        private string m_DestinationNumber;
        private string m_CrewNumber;
        private Color m_TextColor;

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

        public string TripNumber
        {
            get { return m_TripNumber; }
            set
            {
                if (value == m_TripNumber)
                {
                    return;
                }
                m_TripNumber = value;
                RaisePropertyChanged(() => TripNumber);
            }
        }

        public string DestinationNumber
        {
            get { return m_DestinationNumber; }
            set
            {
                if (value == m_DestinationNumber)
                {
                    return;
                }
                m_DestinationNumber = value;
                RaisePropertyChanged(() => DestinationNumber);
            }
        }

        public string CrewNumber
        {
            get { return m_CrewNumber; }
            set
            {
                if (value == m_CrewNumber)
                {
                    return;
                }
                m_CrewNumber = value;
                RaisePropertyChanged(() => CrewNumber);
            }
        }
    }
}