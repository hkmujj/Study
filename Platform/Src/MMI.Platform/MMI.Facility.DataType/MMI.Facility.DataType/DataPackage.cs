using MMI.Facility.Interface.Addins;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Running;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.DataType
{
    public class DataPackage : IDataPackage
    {
        public IConfig Config { get; set; }

        public IRunningParam RunningParam { get; set; }

        public IAddInLoader AddInLoader { get; set; }

        public ICommunicationDataFacadeService CommunicationDataFacadeService { get; set; }

        public IObjectService ObjectService { get; set; }

        public IServiceManager ServiceManager { get; set; }
    }
}
