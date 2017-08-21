using System.Xml.Serialization;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data.Config.Net;

namespace MMI.Facility.DataType.Config.Net.DataProtocol
{
    [XmlRoot]
    public class NetDataProtocolConfig :NotificationObject, INetDataProtocolConfig
    {
        private NetDataProtocolType m_ProtocolType;
        private PackageIdOnlyConfig m_PackageIdOnlyConfig;
        private BussnessIdAndPackageIdConfig m_BussnessIdAndPackageIdConfig;

        /// <summary>
        /// Э������
        /// </summary>
        public NetDataProtocolType ProtocolType
        {
            set
            {
                if (value == m_ProtocolType)
                    return;

                m_ProtocolType = value;
                RaisePropertyChanged(() => ProtocolType);
            }
            get { return m_ProtocolType; }
        }

        public PackageIdOnlyConfig PackageIdOnlyConfig
        {
            set
            {
                if (Equals(value, m_PackageIdOnlyConfig))
                    return;

                m_PackageIdOnlyConfig = value;
                RaisePropertyChanged(() => PackageIdOnlyConfig);
            }
            get { return m_PackageIdOnlyConfig; }
        }

        public BussnessIdAndPackageIdConfig BussnessIdAndPackageIdConfig
        {
            set
            {
                if (Equals(value, m_BussnessIdAndPackageIdConfig))
                    return;

                m_BussnessIdAndPackageIdConfig = value;
                RaisePropertyChanged(() => BussnessIdAndPackageIdConfig);
            }
            get { return m_BussnessIdAndPackageIdConfig; }
        }

        /// <summary>
        /// ֻ�а��ŵ�����
        /// </summary>
        [XmlIgnore]
        IPackageIdOnlyConfig INetDataProtocolConfig.PackageIdOnlyConfig
        {
            get { return PackageIdOnlyConfig; }
        }

        /// <summary>
        /// ҵ��id�Ͱ��ŵ�����
        /// </summary>
        [XmlIgnore]
        IBussnessIdAndPackageIdConfig INetDataProtocolConfig.BussnessIdAndPackageIdConfig
        {
            get { return BussnessIdAndPackageIdConfig; }
        }
    }
}