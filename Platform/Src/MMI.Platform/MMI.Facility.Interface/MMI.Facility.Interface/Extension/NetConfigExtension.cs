using System;
using System.Linq;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.Interface.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class NetConfigExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="projectDataConfig"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static INetDataConfig GetNetDataConfig(this INetConfig config, INetProjectDataConfig projectDataConfig)
        {
            INetDataConfig netDataConfig = null;

            switch (config.NetDataProtocolConfig.ProtocolType)
            {
                case NetDataProtocolType.PackageIdOnly:
                    netDataConfig = config.NetDataProtocolConfig.PackageIdOnlyConfig.NetDataConfig;
                    break;
                case NetDataProtocolType.BussnessIdAndPackageId:
                    netDataConfig =
                        config.NetDataProtocolConfig.BussnessIdAndPackageIdConfig.ProjectDataConfigCollection.First(
                            f => f.ProjectType == projectDataConfig.ProjectType).NetDataConfig;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return netDataConfig;

        }
    }
}