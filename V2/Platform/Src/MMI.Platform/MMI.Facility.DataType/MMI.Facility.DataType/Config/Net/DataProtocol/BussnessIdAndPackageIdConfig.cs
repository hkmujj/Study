using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Practices.Prism;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.DataType.Config.Net.DataProtocol
{
    public class BussnessIdAndPackageIdConfig : IBussnessIdAndPackageIdConfig
    {
        public List<NetProjectDataConfig> ProjectDataConfigCollection { set; get; }

        /// <summary>
        /// ProjectDataConfigCollection
        /// </summary>
        [XmlIgnore]
        List<INetProjectDataConfig> IBussnessIdAndPackageIdConfig.ProjectDataConfigCollection
        {
            get { return ProjectDataConfigCollection.Cast<INetProjectDataConfig>().ToList(); }
        }
    }
}