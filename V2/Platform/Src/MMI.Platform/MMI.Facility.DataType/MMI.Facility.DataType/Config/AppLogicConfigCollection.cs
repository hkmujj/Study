using System.Collections.Generic;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.DataType.Config
{
    public class AppLogicConfigCollection : IAppLogicConfigCollection
    {
        public AppLogicConfigCollection()
        {
            AppLogicConfigDic = new Dictionary<int, IAppLogicConfig>();
        }

        public IAppConfig ParentConfig { get; set; }

        public Dictionary<int, IAppLogicConfig> AppLogicConfigDic { get; private set; }
    }
}
