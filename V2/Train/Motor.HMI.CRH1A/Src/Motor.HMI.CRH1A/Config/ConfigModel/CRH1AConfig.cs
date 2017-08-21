using System.ComponentModel;
using System.Xml.Serialization;
using Motor.HMI.CRH1A.Annotations;

namespace Motor.HMI.CRH1A.Config.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot]
    public class CRH1AConfig: INotifyPropertyChanged
    {
        private ProjectType m_Type;
        private CRH1ADebug m_DebugConfig;

        /// <summary>
        /// 当前类型
        /// </summary>
        [XmlElement]
        public ProjectType Type
        {
            set
            {
                if (value == m_Type)
                    return;

                m_Type = value;
                RaisePropertyChanged("Type");
            }
            get { return m_Type; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlElement]
        public CRH1ADebug DebugConfig
        {
            set
            {
                if (Equals(value, m_DebugConfig))
                    return;

                m_DebugConfig = value;
                RaisePropertyChanged("DebugConfig");
            }
            get { return m_DebugConfig; }
        }

        [XmlElement]
        public AdaptConfig AdaptConfig { get; set; }

        //[XmlElement]
        //public StationConfig StationConfig { set; get; }

        //[XmlElement]
        //public SystemMenuConfig SystemMenuConfig { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
