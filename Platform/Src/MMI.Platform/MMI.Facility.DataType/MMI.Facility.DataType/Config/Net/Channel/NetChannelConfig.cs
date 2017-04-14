using System.ComponentModel;
using System.Xml.Serialization;
using CommonUtil.Util;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Properties;
using MMI.Facility.Interface.Data.Config.Net;

namespace MMI.Facility.DataType.Config.Net.Channel
{
    public class NetChannelConfig : NotificationObject, INetChannelConfig
    {
        private ANetConfig m_ANetConfig;
        private BNetConfig m_BNetConfig;
        private CNetConfig m_CNetConfig;
        private PTT2DNetConfig m_PTT2DNetConfig;
        private NetType m_NetType;

        public NetType NetType
        {
            get { return m_NetType; }
            set
            {
                if (value == m_NetType)
                {
                    return;
                }

                m_NetType = value;
                RaisePropertyChanged(() => NetType);
            }
        }


        [XmlIgnore]
        IANetConfig INetChannelConfig.ANetConfig
        {
            get { return ANetConfig; }
        }

        [XmlIgnore]
        IBNetConfig INetChannelConfig.BNetConfig
        {
            get { return BNetConfig; }
        }

        [XmlIgnore]
        ICNetConfig INetChannelConfig.CNetConfig
        {
            get { return CNetConfig; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        IPTT2DNetConfig INetChannelConfig.PTT2DNetConfig
        {
            get { return PTT2DNetConfig; }
        }

        [XmlElement]
        public ANetConfig ANetConfig
        {
            get { return m_ANetConfig; }
            set
            {
                if (Equals(value, m_ANetConfig))
                {
                    return;
                }

                m_ANetConfig = value;
                OnPropertyChanged("ANetConfig");
            }
        }

        [XmlElement]
        public BNetConfig BNetConfig
        {
            get { return m_BNetConfig; }
            set
            {
                if (Equals(value, m_BNetConfig))
                {
                    return;
                }

                m_BNetConfig = value;
                OnPropertyChanged("BNetConfig");
            }
        }

        [XmlElement]
        public CNetConfig CNetConfig
        {
            get { return m_CNetConfig; }
            set
            {
                if (Equals(value, m_CNetConfig))
                {
                    return;
                }

                m_CNetConfig = value;
                OnPropertyChanged("CNetConfig");
            }
        }

        [XmlElement]
        public PTT2DNetConfig PTT2DNetConfig
        {
            set
            {
                if (Equals(value, m_PTT2DNetConfig))
                {
                    return;
                }

                m_PTT2DNetConfig = value;
                OnPropertyChanged("PTT2DNetConfig");
            }
            get { return m_PTT2DNetConfig; }
        }

        private static void Test()
        {
            var d = new NetChannelConfig()
            {
                BNetConfig =
                    new BNetConfig() { CpuIP = "10.2.2.92", TeachIP = "10.2.2.92", },
                NetType = NetType.None,
            };
            DataSerialization.SerializeToXmlFile(d, "D:\\a.xml");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}