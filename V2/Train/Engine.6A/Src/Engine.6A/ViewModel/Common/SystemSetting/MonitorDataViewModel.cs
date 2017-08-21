using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo;

namespace Engine._6A.ViewModel.Common.SystemSetting
{
    [Export(typeof(IMonitorDataViewModel))]
    public class MonitorDataViewModel : ViewModelBase, IMonitorDataViewModel
    {
        private string m_InstallationStatus;
        private string m_PassengerComplement;
        private string m_RememberLong;
        private string m_TotalWeight;
        private string m_TotallNum;
        private string m_WorkingCondition;
        private string m_KilometerPost;
        private string m_Speed;
        private string m_FitDriverID;
        private string m_DriverID;
        private string m_UseCarID;
        private string m_TrainID;
        private string m_ErrorFlag;
        private string m_CirculationNum;

        public string CirculationNum
        {
            get { return m_CirculationNum; }
            set
            {
                if (value == m_CirculationNum)
                {
                    return;
                }
                m_CirculationNum = value;
                RaisePropertyChanged(() => CirculationNum);
            }
        }

        public string ErrorFlag
        {
            get { return m_ErrorFlag; }
            set
            {
                if (value == m_ErrorFlag)
                {
                    return;
                }
                m_ErrorFlag = value;
                RaisePropertyChanged(() => ErrorFlag);
            }
        }

        public string TrainID
        {
            get { return m_TrainID; }
            set
            {
                if (value == m_TrainID)
                {
                    return;
                }
                m_TrainID = value;
                RaisePropertyChanged(() => TrainID);
            }
        }

        public string UseCarID
        {
            get { return m_UseCarID; }
            set
            {
                if (value == m_UseCarID)
                {
                    return;
                }
                m_UseCarID = value;
                RaisePropertyChanged(() => UseCarID);
            }
        }

        public string DriverID
        {
            get { return m_DriverID; }
            set
            {
                if (value == m_DriverID)
                {
                    return;
                }
                m_DriverID = value;
                RaisePropertyChanged(() => DriverID);
            }
        }

        public string FitDriverID
        {
            get { return m_FitDriverID; }
            set
            {
                if (value == m_FitDriverID)
                {
                    return;
                }
                m_FitDriverID = value;
                RaisePropertyChanged(() => FitDriverID);
            }
        }

        public string Speed
        {
            get { return m_Speed; }
            set
            {
                if (value == m_Speed)
                {
                    return;
                }
                m_Speed = value;
                RaisePropertyChanged(() => Speed);
            }
        }

        public string KilometerPost
        {
            get { return m_KilometerPost; }
            set
            {
                if (value == m_KilometerPost)
                {
                    return;
                }
                m_KilometerPost = value;
                RaisePropertyChanged(() => KilometerPost);
            }
        }

        public string WorkingCondition
        {
            get { return m_WorkingCondition; }
            set
            {
                if (value == m_WorkingCondition)
                {
                    return;
                }
                m_WorkingCondition = value;
                RaisePropertyChanged(() => WorkingCondition);
            }
        }

        public string TotallNum
        {
            get { return m_TotallNum; }
            set
            {
                if (value == m_TotallNum)
                {
                    return;
                }
                m_TotallNum = value;
                RaisePropertyChanged(() => TotallNum);
            }
        }

        public string TotalWeight
        {
            get { return m_TotalWeight; }
            set
            {
                if (value == m_TotalWeight)
                {
                    return;
                }
                m_TotalWeight = value;
                RaisePropertyChanged(() => TotalWeight);
            }
        }

        public string RememberLong
        {
            get { return m_RememberLong; }
            set
            {
                if (value == m_RememberLong)
                {
                    return;
                }
                m_RememberLong = value;
                RaisePropertyChanged(() => RememberLong);
            }
        }

        public string PassengerComplement
        {
            get { return m_PassengerComplement; }
            set
            {
                if (value == m_PassengerComplement)
                {
                    return;
                }
                m_PassengerComplement = value;
                RaisePropertyChanged(() => PassengerComplement);
            }
        }

        public string InstallationStatus
        {
            get { return m_InstallationStatus; }
            set
            {
                if (value == m_InstallationStatus)
                {
                    return;
                }
                m_InstallationStatus = value;
                RaisePropertyChanged(() => InstallationStatus);
            }
        }
    }
}