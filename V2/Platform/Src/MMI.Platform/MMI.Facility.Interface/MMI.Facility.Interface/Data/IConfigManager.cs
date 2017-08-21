using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigManager
    {
        /// <summary>
        /// 
        /// </summary>
        IConfig Config { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseDirectory"></param>
        /// <param name="needInitalize"></param>
        void LoadConfig(string baseDirectory, bool needInitalize = true);

    }
}
