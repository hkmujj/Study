using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.SystemSeting;

namespace Engine._6A.ViewModel.Common.SystemSetting
{
    [Export(typeof(IPlateformInfoViewModel))]
    public class PlateformInfoViewModel : ViewModelBase, IPlateformInfoViewModel
    {
        private string m_TelNum;
        private string m_Technology;
        private string m_Plateform;
        private string m_CpuVersion;

        public string CPUVersion
        {
            get { return m_CpuVersion; }
            set
            {
                if (value == m_CpuVersion)
                {
                    return;
                }
                m_CpuVersion = value;
                RaisePropertyChanged(() => CPUVersion);
            }
        }

        public string Plateform
        {
            get { return m_Plateform; }
            set
            {
                if (value == m_Plateform)
                {
                    return;
                }
                m_Plateform = value;
                RaisePropertyChanged(() => Plateform);
            }
        }

        public string Technology
        {
            get { return m_Technology; }
            set
            {
                if (value == m_Technology)
                {
                    return;
                }
                m_Technology = value;
                RaisePropertyChanged(() => Technology);
            }
        }

        public string TelNum
        {
            get { return m_TelNum; }
            set
            {
                if (value == m_TelNum)
                {
                    return;
                }
                m_TelNum = value;
                RaisePropertyChanged(() => TelNum);
            }
        }
    }
}