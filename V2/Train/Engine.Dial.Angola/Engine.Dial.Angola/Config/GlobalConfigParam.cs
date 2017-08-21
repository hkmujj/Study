using MMI.Facility.Interface.Data.Config;

namespace Engine.Dial.Angola.Config
{
    public class GlobalConfigParam
    {
        static GlobalConfigParam()
        {
            Instance = new GlobalConfigParam();
        }

        public IProjectIndexDescriptionConfig IndexConfig { get; internal set; }

        public static GlobalConfigParam Instance { get; private set; }
    }
}