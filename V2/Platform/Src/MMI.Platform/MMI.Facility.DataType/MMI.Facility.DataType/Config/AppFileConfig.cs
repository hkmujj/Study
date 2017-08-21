using System.ComponentModel;
using System.Xml.Serialization;
using CommonUtil.Annotations;
using MMI.Facility.Interface.Data.Config;

namespace MMI.Facility.DataType.Config
{
    [XmlRoot]
    public class AppFileConfig : IAppFileConfig, INotifyPropertyChanged
    {
        private string m_ViewConfigFile;
        private string m_ObjectConfigFile;
        private string m_LogicConfigFile;

        public string ViewConfigFile
        {
            get { return m_ViewConfigFile; }
            set
            {
                if (value == m_ViewConfigFile)
                {
                    return;
                }
                m_ViewConfigFile = value;
                OnPropertyChanged("ViewConfigFile");
            }
        }

        public string ObjectConfigFile
        {
            get { return m_ObjectConfigFile; }
            set
            {
                if (value == m_ObjectConfigFile)
                {
                    return;
                }
                m_ObjectConfigFile = value;
                OnPropertyChanged("ObjectConfigFile");
            }
        }

        public string LogicConfigFile
        {
            get { return m_LogicConfigFile; }
            set
            {
                if (value == m_LogicConfigFile)
                {
                    return;
                }
                m_LogicConfigFile = value;
                OnPropertyChanged("LogicConfigFile");
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