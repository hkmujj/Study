using System.ComponentModel;
using System.Xml.Serialization;
using ES.Facility.PublicModule.IO;
using MMI.Facility.DataType.Properties;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.Net;

namespace MMI.Facility.DataType.Config.Net.Channel
{
    [XmlRoot]
    public class BNetConfig :INotifyPropertyChanged, IBNetConfig
    {
        /// <summary>
        /// 监听端口
        /// </summary>
        private BNetPortDefine m_ListerPort;
        /// <summary>
        /// 教员IP
        /// </summary>
        private string m_TeachIP;
        /// <summary>
        /// 教员端口
        /// </summary>
        private BNetPortDefine m_TeachPort;
        /// <summary>
        /// 主控IP
        /// </summary>
        private string m_CpuIP;
        /// <summary>
        /// 主控端口
        /// </summary>
        private BNetPortDefine m_CpuPort;
        /// <summary>
        /// 本地IP号
        /// </summary>
        private int m_LocIpNum;
        /// <summary>
        /// 系统ID
        /// </summary>
        private int m_SystemID;

        private BNetTeachType m_TeachType;

        [IniField(Section = "FmsConfig", KeyName = "ListerPort", DefaultValue = "0")]
        public BNetPortDefine ListerPort
        {
            set
            {
                if (value == m_ListerPort)
                {
                    return;
                }
                m_ListerPort = value;
                OnPropertyChanged("ListerPort");
            }
            get { return m_ListerPort; }
        }

        [IniField(Section = "FmsConfig", KeyName = "TeachIP", DefaultValue = "127.0.0.1")]
        public string TeachIP
        {
            set
            {
                if (value == m_TeachIP)
                {
                    return;
                }
                m_TeachIP = value;
                OnPropertyChanged("TeachIP");
            }
            get { return m_TeachIP; }
        }

        [IniField(Section = "FmsConfig", KeyName = "TeachPort", DefaultValue = "0")]
        public BNetPortDefine TeachPort
        {
            set
            {
                if (value == m_TeachPort)
                {
                    return;
                }
                m_TeachPort = value;
                OnPropertyChanged("TeachPort");
            }
            get { return m_TeachPort; }
        }

        [IniField(Section = "FmsConfig", KeyName = "CpuIP", DefaultValue = "127.0.0.1")]
        public string CpuIP
        {
            set
            {
                if (value == m_CpuIP)
                {
                    return;
                }
                m_CpuIP = value;
                OnPropertyChanged("CpuIP");
            }
            get { return m_CpuIP; }
        }

        [IniField(Section = "FmsConfig", KeyName = "CpuPort", DefaultValue = "0")]
        public BNetPortDefine CpuPort
        {
            set
            {
                if (value == m_CpuPort)
                {
                    return;
                }
                m_CpuPort = value;
                OnPropertyChanged("CpuPort");
            }
            get { return m_CpuPort; }
        }

        [IniField(Section = "FmsConfig", KeyName = "LocIpNum", DefaultValue = "0")]
        public int LocIpNum
        {
            set
            {
                if (value == m_LocIpNum)
                {
                    return;
                }
                m_LocIpNum = value;
                OnPropertyChanged("LocIpNum");
            }
            get { return m_LocIpNum; }
        }

        /// <summary>
        /// 教员类型
        /// </summary>
        [XmlElement]
        public BNetTeachType TeachType
        {
            get { return m_TeachType; }
            set
            {
                if (value == m_TeachType)
                {
                    return;
                }
                m_TeachType = value;
                OnPropertyChanged("TeachType");
            }
        }

        [IniField(Section = "FmsConfig", KeyName = "UinitiyNum", DefaultValue = "0")]
        public int SystemID
        {
            set
            {
                if (value == m_SystemID)
                {
                    return;
                }
                m_SystemID = value;
                OnPropertyChanged("SystemID");
            }
            get { return m_SystemID; }
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