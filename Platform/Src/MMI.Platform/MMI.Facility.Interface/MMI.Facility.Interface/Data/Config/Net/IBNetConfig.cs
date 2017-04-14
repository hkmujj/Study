using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.Interface.Data.Config.Net
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBNetConfig
    {
        /// <summary>
        /// 监听端口
        /// </summary>
        BNetPortDefine ListerPort { get; }

        /// <summary>
        /// 教员IP
        /// </summary>
        string TeachIP {  get; }

        /// <summary>
        /// 教员端口
        /// </summary>
        BNetPortDefine TeachPort { get; }

        /// <summary>
        /// 主控IP
        /// </summary>
        string CpuIP {  get; }

        /// <summary>
        /// 主控端口
        /// </summary>
        BNetPortDefine CpuPort { get; }

        /// <summary>
        /// 本机IP地址网卡号
        /// </summary>
        int LocIpNum {  get; }

        /// <summary>
        /// 教员类型
        /// </summary>
        BNetTeachType TeachType { get; }

        /// <summary>
        /// 本机系统ID
        /// </summary>
        int SystemID {  get; }
    }
}
