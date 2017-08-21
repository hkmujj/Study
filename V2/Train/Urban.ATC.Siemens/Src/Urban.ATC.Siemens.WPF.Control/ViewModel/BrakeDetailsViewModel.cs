using Urban.ATC.Siemens.WPF.Interface.ViewStates;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class BrakeDetailsViewModel : ViewModelBase
    {
        public BrakeDetailsViewModel()
        {
            Type = BrakeDetailsType.Initial;
        }

        public BrakeDetailsType Type
        {
            get { return m_Type; }
            set
            {
                if (value == m_Type)
                {
                    return;
                }
                m_Type = value;
                RaisePropertyChanged(() => Type);
            }
        }

        private BrakeDetailsType m_Type;
    }
}