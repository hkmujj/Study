using Subway.CBTC.BeiJiaoKong.Models.RegionA;

namespace Subway.CBTC.BeiJiaoKong.ViewModel
{
    public class BrakeDetailsViewModel : ViewModelBase
    {
        public BrakeDetailsViewModel()
        {
            Type = BrakeDetailsType.Normal;
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