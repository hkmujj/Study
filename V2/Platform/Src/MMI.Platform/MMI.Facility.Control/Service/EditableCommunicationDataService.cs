using MMI.Facility.DataType.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.NetDataPackage;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.Control.Service
{
    class EditableCommunicationDataService : ICommunicationDataService
    {

        public EditableCommunicationDataService(IConfig config, INetProjectDataConfig netProjectDataConfig,
            CommunicationDataEntity readDataEntity, CommunicationDataEntity writeDataEntity)
        {
            var read = new EditableCommunicationDataReadService(config, netProjectDataConfig, readDataEntity);
            ReadService = read;
            WriteService = new EditableCommunicationDataWriteService(config, netProjectDataConfig, writeDataEntity);
            WritableReadService = read;
        }

        public void Dispose()
        {
        }

        public IWritableCommunicationDataReadService WritableReadService { get; private set; }

        public ICommunicationDataReadService ReadService { get; private set; }

        public ICommunicationDataWriteService WriteService { get; private set; }

    }
}
