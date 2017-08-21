using MMI.Facility.DataType.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.Control.Service
{
    class EditableCommunicationDataReadService : CommunicatonDataReadServiceBase
    {
        public EditableCommunicationDataReadService(IConfig config, INetProjectDataConfig projectDataConfig, CommunicationDataEntity communicationDataEntity)
            : base(config, projectDataConfig, communicationDataEntity)
        {
        }
    }
}
