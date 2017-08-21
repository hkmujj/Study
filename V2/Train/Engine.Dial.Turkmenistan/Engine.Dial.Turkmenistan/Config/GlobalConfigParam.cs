using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;

namespace Engine.Dial.Turkmenistan.Config
{
    public class GlobalConfigParam
    {
        static GlobalConfigParam()
        {
            Instance = new GlobalConfigParam();
        }

        public IProjectIndexDescriptionConfig IndexConfig { get; internal set; }

        public static GlobalConfigParam Instance { get; private set; }
        public SubsystemInitParam InitParam { get; set; }
    }
}