using System.ComponentModel;
using System.Xml.Serialization;
using MMI.Facility.DataType.Properties;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.DataType.Config
{
    [XmlRoot]
    public class SubsystemConfig : INotifyPropertyChanged, ISubsystemConfig
    {
        private SubsystemType m_SubsystemType;
        private bool m_NeedLoad;
        private string m_Name;
        private string m_Dll;
        private string m_EntryClass;
        private ProjectType m_ProjectType;
        private int m_ShareIndex;
        private string m_ShareName;

        [XmlAttribute]
        public SubsystemType SubsystemType
        {
            get { return m_SubsystemType; }
            set
            {
                if (value == m_SubsystemType)
                {
                    return;
                }
                m_SubsystemType = value;
                OnPropertyChanged("SubsystemType");
            }
        }

        [XmlAttribute]
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

        [XmlAttribute]
        public bool NeedLoad
        {
            get { return m_NeedLoad; }
            set
            {
                if (value.Equals(m_NeedLoad))
                {
                    return;
                }
                m_NeedLoad = value;
                OnPropertyChanged("NeedLoad");
            }
        }

        [XmlAttribute]
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (value == m_Name)
                {
                    return;
                }
                m_Name = value;
                OnPropertyChanged("Name");
            }
        }

        [XmlAttribute]
        public string Dll
        {
            get { return m_Dll; }
            set
            {
                if (value == m_Dll)
                {
                    return;
                }
                m_Dll = value;
                OnPropertyChanged("Dll");
            }
        }

        [XmlAttribute]
        public string EntryClass
        {
            get { return m_EntryClass; }
            set
            {
                if (value == m_EntryClass)
                {
                    return;
                }
                m_EntryClass = value;
                OnPropertyChanged("EntryClass");
            }
        }

        /// <summary>
        /// PTT共享内存名称
        /// </summary>
        [XmlAttribute]
        public string ShareName
        {
            get { return m_ShareName; }
            set
            {
                if (m_ShareName == value)
                {
                    return;
                }
                m_ShareName = value;
                OnPropertyChanged("ShareName");
            }
        }

        /// <summary>
        /// PTT共享内存Index
        /// </summary>
        [XmlAttribute]
        public int ShareIndex
        {
            get { return m_ShareIndex; }
            set
            {
                if (m_ShareIndex == value)
                {
                    return;
                }
                m_ShareIndex = value;
                OnPropertyChanged("ShareIndex");
            }
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