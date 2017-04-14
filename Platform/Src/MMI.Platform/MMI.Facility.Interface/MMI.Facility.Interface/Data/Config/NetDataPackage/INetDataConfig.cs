namespace MMI.Facility.Interface.Data.Config.NetDataPackage
{
    /// <summary>
    /// 
    /// </summary>
    public interface INetDataConfig
    {
        /// <summary>
        /// 
        /// </summary>
        INetDataPackageConfig NetInputDataPackage { get; }

        /// <summary>
        /// 
        /// </summary>
        INetDataPackageConfig NetOutputDataPackage { get; }
    }
}
