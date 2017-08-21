using System.Collections.Generic;
using MMI.Facility.DataType.Config.Net.DataProtocol;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.DataType.Config.Net.Channel
{
    public class DNetConfig : IDNetConfig
    {
        public List<NetProjectDataConfig> ProjectDataConfigCollection { get; set; }

        IEnumerable<INetProjectDataConfig> IDNetConfig.ProjectDataConfigCollection
        {
            get { return ProjectDataConfigCollection; }
        }
    }
}