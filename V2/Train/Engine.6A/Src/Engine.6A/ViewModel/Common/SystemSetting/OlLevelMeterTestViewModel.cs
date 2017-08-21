using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo;

namespace Engine._6A.ViewModel.Common.SystemSetting
{
    [Export(typeof(IOlLevelMeterTestViewModel))]
    public class OlLevelMeterTestViewModel : ViewModelBase, IOlLevelMeterTestViewModel
    {
        private string m_ErrorFlag;
        private string m_CirculationNum;
        private string m_OilMass;

        public string OilMass
        {
            get { return m_OilMass; }
            set
            {
                if (value == m_OilMass)
                {
                    return;
                }
                m_OilMass = value;
                RaisePropertyChanged(() => OilMass);
            }
        }

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
    }
}