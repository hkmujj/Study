using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.DataMonitor;

namespace Engine._6A.ViewModel.Common
{
    [Export(typeof(ISleepViewModel))]
    public class SleepViewModel : ViewModelBase, ISleepViewModel
    {
        private string m_DriverState;
        private string m_BoardInfo;
        private string m_TwoVideoCheck;
        private string m_OneVideoCheck;
        private string m_TwoDriverOpen;
        private string m_OneDriverOpen;

        public string OneDriverOpen
        {
            get { return m_OneDriverOpen; }
            set
            {
                if (value == m_OneDriverOpen)
                {
                    return;
                }
                m_OneDriverOpen = value;
                RaisePropertyChanged(() => OneDriverOpen);
            }
        }

        public string TwoDriverOpen
        {
            get { return m_TwoDriverOpen; }
            set
            {
                if (value == m_TwoDriverOpen)
                {
                    return;
                }
                m_TwoDriverOpen = value;
                RaisePropertyChanged(() => TwoDriverOpen);
            }
        }

        public string OneVideoCheck
        {
            get { return m_OneVideoCheck; }
            set
            {
                if (value == m_OneVideoCheck)
                {
                    return;
                }
                m_OneVideoCheck = value;
                RaisePropertyChanged(() => OneVideoCheck);
            }
        }

        public string TwoVideoCheck
        {
            get { return m_TwoVideoCheck; }
            set
            {
                if (value == m_TwoVideoCheck)
                {
                    return;
                }
                m_TwoVideoCheck = value;
                RaisePropertyChanged(() => TwoVideoCheck);
            }
        }

        public string BoardInfo
        {
            get { return m_BoardInfo; }
            set
            {
                if (value == m_BoardInfo)
                {
                    return;
                }
                m_BoardInfo = value;
                RaisePropertyChanged(() => BoardInfo);
            }
        }

        public string DriverState
        {
            get { return m_DriverState; }
            set
            {
                if (value == m_DriverState)
                {
                    return;
                }
                m_DriverState = value;
                RaisePropertyChanged(() => DriverState);
            }
        }
    }
}