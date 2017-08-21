using System.ComponentModel;
using System.Xml.Serialization;
using CommonUtil.Util;
using MMI.Facility.DataType.Attributes;
using MMI.Facility.DataType.Properties;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.DataType.Config.Net.DataProtocol
{
    [XmlRoot]
    [ConfigureDescription("网络数据映射关系配置", FileName)]
    public class NetDataConfig : INetDataConfig, INotifyPropertyChanged
    {
        private NetDataPackageConfig m_NetInputDataPackage;
        private NetDataPackageConfig m_NetOutputDataPackage;
        public const string FileName = "NetDataConfig.xml";

        public NetDataPackageConfig NetInputDataPackage
        {
            get { return m_NetInputDataPackage; }
            set
            {
                if (Equals(value, m_NetInputDataPackage))
                {
                    return;
                }
                m_NetInputDataPackage = value;
                OnPropertyChanged("NetInputDataPackage");
            }
        }

        public NetDataPackageConfig NetOutputDataPackage
        {
            get { return m_NetOutputDataPackage; }
            set
            {
                if (Equals(value, m_NetOutputDataPackage))
                {
                    return;
                }
                m_NetOutputDataPackage = value;
                OnPropertyChanged("NetOutputDataPackage");
            }
        }

        [XmlIgnore]
        INetDataPackageConfig INetDataConfig.NetInputDataPackage
        {
            get { return NetInputDataPackage; }
        }

        [XmlIgnore]
        INetDataPackageConfig INetDataConfig.NetOutputDataPackage
        {
            get { return NetOutputDataPackage; }
        }

        private static void Test()
        {
            var d = new NetDataConfig()
                    {
                        NetInputDataPackage = new NetDataPackageConfig(),
                        NetOutputDataPackage = new NetDataPackageConfig()
                    };
            DataSerialization.SerializeToXmlFile(d, "D:\\a.xml");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}