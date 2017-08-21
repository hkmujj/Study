using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.SystemSeting;

namespace Engine._6A.ViewModel.Common.SystemSetting
{
    [Export(typeof(IVersionInfoViewModel))]
    public class VersionInfoViewModel : ViewModelBase, IVersionInfoViewModel
    {
        private string m_SleepSunSystem;
        private string m_VideoSunSystem;
        private string m_RunTwoSunSystem;
        private string m_RunOneSunSystem;
        private string m_ColumnSunSystem;
        private string m_InsulationSubSystem;
        private string m_FireSubSystem;
        private string m_BrakeSubSystem;

        public VersionInfoViewModel()
        {

        }

        public string BrakeSubSystem
        {
            get { return m_BrakeSubSystem; }
            set
            {
                if (value == m_BrakeSubSystem)
                {
                    return;
                }
                m_BrakeSubSystem = value;
                RaisePropertyChanged(() => BrakeSubSystem);
            }
        }

        public string FireSubSystem
        {
            get { return m_FireSubSystem; }
            set
            {
                if (value == m_FireSubSystem)
                {
                    return;
                }
                m_FireSubSystem = value;
                RaisePropertyChanged(() => FireSubSystem);
            }
        }

        public string InsulationSubSystem
        {
            get { return m_InsulationSubSystem; }
            set
            {
                if (value == m_InsulationSubSystem)
                {
                    return;
                }
                m_InsulationSubSystem = value;
                RaisePropertyChanged(() => InsulationSubSystem);
            }
        }

        public string ColumnSunSystem
        {
            get { return m_ColumnSunSystem; }
            set
            {
                if (value == m_ColumnSunSystem)
                {
                    return;
                }
                m_ColumnSunSystem = value;
                RaisePropertyChanged(() => ColumnSunSystem);
            }
        }

        public string RunOneSunSystem
        {
            get { return m_RunOneSunSystem; }
            set
            {
                if (value == m_RunOneSunSystem)
                {
                    return;
                }
                m_RunOneSunSystem = value;
                RaisePropertyChanged(() => RunOneSunSystem);
            }
        }

        public string RunTwoSunSystem
        {
            get { return m_RunTwoSunSystem; }
            set
            {
                if (value == m_RunTwoSunSystem)
                {
                    return;
                }
                m_RunTwoSunSystem = value;
                RaisePropertyChanged(() => RunTwoSunSystem);
            }
        }

        public string VideoSunSystem
        {
            get { return m_VideoSunSystem; }
            set
            {
                if (value == m_VideoSunSystem)
                {
                    return;
                }
                m_VideoSunSystem = value;
                RaisePropertyChanged(() => VideoSunSystem);
            }
        }

        public string SleepSunSystem
        {
            get { return m_SleepSunSystem; }
            set
            {
                if (value == m_SleepSunSystem)
                {
                    return;
                }
                m_SleepSunSystem = value;
                RaisePropertyChanged(() => SleepSunSystem);
            }
        }
    }
}