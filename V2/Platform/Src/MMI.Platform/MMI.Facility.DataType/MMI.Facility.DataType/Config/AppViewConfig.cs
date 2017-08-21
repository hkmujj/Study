using System.Collections.Generic;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.DataType.Config
{
    public class AppViewConfig : IAppViewConfig
    {
        public IAppConfig ParentConfig { get; set; }

        public Dictionary<int, IAppViewConfigUnit> AppViewConfigDic { get; private set; }

        public AppViewConfig()
        {
            AppViewConfigDic= new Dictionary<int, IAppViewConfigUnit>();
        }
    }
}
