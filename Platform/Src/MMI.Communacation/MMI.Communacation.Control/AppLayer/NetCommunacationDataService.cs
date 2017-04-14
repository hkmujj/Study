using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.DataType.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.NetDataPackage;
using MMI.Facility.Interface.Service;

namespace MMI.Communacation.Control.AppLayer
{
    public class NetCommunicationDataService : ICommunicationDataService
    {

        public NetCommunicationDataService(IConfig config, ICommunacationDataSender communacationDataSender,
            INetProjectDataConfig netProjectDataConfig, CommunicationDataEntity readDataEntity, CommunicationDataEntity writeDataEntity)
        {
            var read = new NetCommunicationDataReadService(config, netProjectDataConfig, readDataEntity);
            ReadService = read;
            WriteService = new NetCommunicationDataWriteService(config, communacationDataSender, netProjectDataConfig,
                writeDataEntity, netProjectDataConfig.ProjectType.ToString());
            WritableReadService = new ReadonlyNetCommunicationDataReadServiceWrapper(read);
        }

        public IWritableCommunicationDataReadService WritableReadService { get; private set; }

        public ICommunicationDataReadService ReadService { get; private set; }

        public ICommunicationDataWriteService WriteService { get; private set; }


        public void Dispose()
        {
            WriteService.Dispose();
            ReadService.Dispose();
        }
    }
}
