using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.Control.Data
{
    internal interface IAppConfigLoader
    {
        IAppConfig LoadAppConfig(IConfig rootConfig, string baseDirectory, ISubsystemConfig subsystemConfig);
    }
}