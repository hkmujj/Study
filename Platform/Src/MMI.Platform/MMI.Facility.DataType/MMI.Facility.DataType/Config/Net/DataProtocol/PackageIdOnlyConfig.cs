using System.Xml.Serialization;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.DataType.Config.Net.DataProtocol
{
    public class PackageIdOnlyConfig: IPackageIdOnlyConfig
    {
        public NetDataConfig NetDataConfig { set; get; }

        /// <summary>
        ///  ˝æ›≈‰÷√
        /// </summary>
        [XmlIgnore]
        INetDataConfig IPackageIdOnlyConfig.NetDataConfig
        {
            get { return NetDataConfig; }
        }
    }
}