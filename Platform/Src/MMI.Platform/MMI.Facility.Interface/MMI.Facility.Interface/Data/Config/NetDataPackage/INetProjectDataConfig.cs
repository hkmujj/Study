namespace MMI.Facility.Interface.Data.Config.NetDataPackage
{
    /// <summary>
    /// 
    /// </summary>
    public interface INetProjectDataConfig
    {
        /// <summary>
        /// 
        /// </summary>
        ProjectType ProjectType { get; }

        /// <summary>
        /// 
        /// </summary>
        INetDataConfig NetDataConfig { get; }
    }
}