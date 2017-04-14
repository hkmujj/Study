using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using CommonUtil.Annotations;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.DataType.Config.Net.DataProtocol
{
    [DebuggerDisplay("ProjectType={ProjectType}")]
    public class NetProjectDataConfig : INotifyPropertyChanged, INetProjectDataConfig
    {
        private ProjectType m_ProjectType;
        private NetDataConfig m_NetDataConfig;

        public ProjectType ProjectType
        {
            get { return m_ProjectType; }
            set
            {
                if (value == m_ProjectType)
                {
                    return;
                }
                m_ProjectType = value;
                OnPropertyChanged("ProjectType");
            }
        }

        public NetDataConfig NetDataConfig
        {
            get { return m_NetDataConfig; }
            set
            {
                if (Equals(value, m_NetDataConfig))
                {
                    return;
                }
                m_NetDataConfig = value;
                OnPropertyChanged("NetDataConfig");
            }
        }


        [XmlIgnore]
        INetDataConfig INetProjectDataConfig.NetDataConfig
        {
            get { return NetDataConfig; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}