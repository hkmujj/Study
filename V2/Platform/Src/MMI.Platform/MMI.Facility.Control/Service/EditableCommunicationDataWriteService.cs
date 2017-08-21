using MMI.Facility.DataType.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.Control.Service
{
    internal class EditableCommunicationDataWriteService : CommunicatonDataWriteServiceBase
    {
        public EditableCommunicationDataWriteService(IConfig config, INetProjectDataConfig projectDataConfig,
            CommunicationDataEntity communicationDataEntity) : base(config, projectDataConfig, communicationDataEntity)
        {
        }
    }
}
