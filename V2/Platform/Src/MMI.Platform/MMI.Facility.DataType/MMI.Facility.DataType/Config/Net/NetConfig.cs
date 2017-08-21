using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
using CommonUtil.Util;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Attributes;
using MMI.Facility.DataType.Config.Net.Channel;
using MMI.Facility.DataType.Config.Net.DataProtocol;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.DataType.Config.Net
{
    [XmlRoot]
    [ConfigureDescription("网络通信配置", FileName)]
    public class NetConfig : NotificationObject, INetConfig
    {
        private NetChannelConfig m_NetChannelConfig;
        private NetDataProtocolConfig m_NetDataProtocolConfig;

        [XmlIgnore]
        public const string FileName = "NetConfig.xml";

        public NetChannelConfig NetChannelConfig
        {
            set
            {
                if (Equals(value, m_NetChannelConfig))
                    return;

                m_NetChannelConfig = value;
                RaisePropertyChanged(() => NetChannelConfig);
            }
            get { return m_NetChannelConfig; }
        }

        public NetDataProtocolConfig NetDataProtocolConfig
        {
            set
            {
                if (Equals(value, m_NetDataProtocolConfig))
                    return;

                m_NetDataProtocolConfig = value;
                RaisePropertyChanged(() => NetDataProtocolConfig);
            }
            get { return m_NetDataProtocolConfig; }
        }

        public NetConfig()
        {
            NetChannelConfig = new NetChannelConfig();
            NetDataProtocolConfig = new NetDataProtocolConfig();
        }

        /// <summary>
        /// 网络通道配置
        /// </summary>
        [XmlIgnore]
        INetChannelConfig INetConfig.NetChannelConfig
        {
            get { return NetChannelConfig; }
        }

        /// <summary>
        /// 网络数据协议配置
        /// </summary>
        [XmlIgnore]
        INetDataProtocolConfig INetConfig.NetDataProtocolConfig
        {
            get { return NetDataProtocolConfig; }
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private void Create()
        {
            var data = new NetConfig()
            {
                NetChannelConfig = new NetChannelConfig()
                {
                    BNetConfig =
                        new BNetConfig() {CpuIP = "10.2.2.92", TeachIP = "10.2.2.92",},
                    NetType = NetType.None,
                },
                NetDataProtocolConfig = new NetDataProtocolConfig()
                {
                    PackageIdOnlyConfig = new PackageIdOnlyConfig()
                    {
                        NetDataConfig = new NetDataConfig()
                        {
                            NetInputDataPackage = new NetDataPackageConfig(),
                            NetOutputDataPackage = new NetDataPackageConfig()
                        }
                    },
                    ProtocolType = NetDataProtocolType.PackageIdOnly,
                    BussnessIdAndPackageIdConfig = new BussnessIdAndPackageIdConfig()
                    {
                        ProjectDataConfigCollection = new List<NetProjectDataConfig>()
                        {
                            new NetProjectDataConfig()
                            {
                                ProjectType = ProjectType.Signal,
                                NetDataConfig = new NetDataConfig()
                                {
                                    NetInputDataPackage = new NetDataPackageConfig(),
                                    NetOutputDataPackage = new NetDataPackageConfig()
                                }
                            },
                            new NetProjectDataConfig()
                            {
                                ProjectType = ProjectType.Train,
                                NetDataConfig = new NetDataConfig()
                                {
                                    NetInputDataPackage = new NetDataPackageConfig(),
                                    NetOutputDataPackage = new NetDataPackageConfig()
                                }
                            }
                        }
                    }
                }
            };

            DataSerialization.SerializeToXmlFile(data, "D:\\a.xml");
        }
    }
}