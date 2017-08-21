using CRH2MMI.Common.Global;

namespace CRH2MMI.CarInfo
{
    internal class CarInfoResource
    {
        private readonly CarInfo m_CarInfo;

        public CarInfoConfig CarInfoConfig { private set; get; }

        public CarInfoResource(CarInfo carInfo)
        {
            m_CarInfo = carInfo;
            CarInfoConfig = GlobalInfo.CurrentCRH2Config.CarInfoConfig;
            CarInfoConfig.RefreshGridRelation();
        }
    }
}
