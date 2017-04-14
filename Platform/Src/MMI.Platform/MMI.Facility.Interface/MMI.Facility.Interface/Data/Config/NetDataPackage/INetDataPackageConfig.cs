namespace MMI.Facility.Interface.Data.Config.NetDataPackage
{
    /// <summary>
    /// 
    /// </summary>
    public interface INetDataPackageConfig
    {
        /// <summary>
        /// 数据包数
        /// </summary>
        int PackageCount { get; }

        /// <summary>
        /// float值的起始byte位置
        /// </summary>
        int FloatStartByte { get; }

        /// <summary>
        /// float值的byte个数
        /// </summary>
        int FloatCountOfByte { get; }

        /// <summary>
        /// Bool值的起始byte位置
        /// </summary>
        int BoolStartByte { get; }

        /// <summary>
        /// Bool值的byte个数
        /// </summary>
        int BoolCountOfByte { get; }

        /// <summary>
        /// float值的映射起始地址
        /// </summary>
        int FloatMappedStartIndex { get; }

        /// <summary>
        /// float值的映射起始地址
        /// </summary>
        int BoolMappedStartIndex { get; }

        /// <summary>
        /// 获得float的总个数
        /// </summary>
        /// <returns></returns>
        int GetTotalFloatCount();

        /// <summary>
        /// 获得 bool 的总个数
        /// </summary>
        /// <returns></returns>
        int GetTotalBoolCount();
    }
}