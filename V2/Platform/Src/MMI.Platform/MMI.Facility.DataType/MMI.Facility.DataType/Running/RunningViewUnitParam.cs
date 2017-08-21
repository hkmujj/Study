using System.Collections.Generic;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Running;

namespace MMI.Facility.DataType.Running
{
    public class RunningViewUnitParam : IRunningViewUnitParam
    {
        public RunningViewUnitParam()
        {
            Objects = new List<IObjectBase>();
        }

        public IAppViewConfigUnit ViewConfig { get; set; }

        public List<IObjectBase> Objects { get; set; }
    }
}
