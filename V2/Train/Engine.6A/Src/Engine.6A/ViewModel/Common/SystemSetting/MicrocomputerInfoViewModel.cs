using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo;

namespace Engine._6A.ViewModel.Common.SystemSetting
{
    [Export(typeof(IMicrocomputerInfoViewModel))]
    public class MicrocomputerInfoViewModel : ViewModelBase, IMicrocomputerInfoViewModel
    {
        private string m_ParkingCut;
        private string m_SmallFloodgate;
        private string m_BigFloodgate;
        private string m_MainFaultStatus;
        private string m_Pantograph;
        private string m_HandleLevel;
        private string m_OccupiedEnd;
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

        public string OccupiedEnd
        {
            get { return m_OccupiedEnd; }
            set
            {
                if (value == m_OccupiedEnd)
                {
                    return;
                }
                m_OccupiedEnd = value;
                RaisePropertyChanged(() => OccupiedEnd);
            }
        }

        public string HandleLevel
        {
            get { return m_HandleLevel; }
            set
            {
                if (value == m_HandleLevel)
                {
                    return;
                }
                m_HandleLevel = value;
                RaisePropertyChanged(() => HandleLevel);
            }
        }

        public string Pantograph
        {
            get { return m_Pantograph; }
            set
            {
                if (value == m_Pantograph)
                {
                    return;
                }
                m_Pantograph = value;
                RaisePropertyChanged(() => Pantograph);
            }
        }

        public string MainFaultStatus
        {
            get { return m_MainFaultStatus; }
            set
            {
                if (value == m_MainFaultStatus)
                {
                    return;
                }
                m_MainFaultStatus = value;
                RaisePropertyChanged(() => MainFaultStatus);
            }
        }

        public string BigFloodgate
        {
            get { return m_BigFloodgate; }
            set
            {
                if (value == m_BigFloodgate)
                {
                    return;
                }
                m_BigFloodgate = value;
                RaisePropertyChanged(() => BigFloodgate);
            }
        }

        public string SmallFloodgate
        {
            get { return m_SmallFloodgate; }
            set
            {
                if (value == m_SmallFloodgate)
                {
                    return;
                }
                m_SmallFloodgate = value;
                RaisePropertyChanged(() => SmallFloodgate);
            }
        }

        public string ParkingCut
        {
            get { return m_ParkingCut; }
            set
            {
                if (value == m_ParkingCut)
                {
                    return;
                }
                m_ParkingCut = value;
                RaisePropertyChanged(() => ParkingCut);
            }
        }
    }
}