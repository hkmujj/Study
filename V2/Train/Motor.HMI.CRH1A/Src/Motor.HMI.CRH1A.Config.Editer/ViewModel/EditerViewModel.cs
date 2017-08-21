using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using Motor.HMI.CRH1A.Config.ConfigModel;
using Motor.HMI.CRH1A.Config.Editer.Annotations;
using Motor.HMI.CRH1A.Config.Editer.Model;
using Motor.HMI.CRH1A.Util;

namespace Motor.HMI.CRH1A.Config.Editer.ViewModel
{
    public class EditerViewModel : INotifyPropertyChanged
    {
        private CRH1AConfig m_CRH1AConfig;
        private List<ProjectType> m_ProjectTypeCollection;
        private EditerConfig m_EditerConfig;

        public CRH1AConfig CRH1AConfig
        {
            set { m_CRH1AConfig = value; RaisePropertyChanged("CRH1AConfig");}
            get { return m_CRH1AConfig; }
        }

        public List<ProjectType> ProjectTypeCollection
        {
            set
            {
                m_ProjectTypeCollection = value;
                RaisePropertyChanged("ProjectTypeCollection");
            }
            get { return m_ProjectTypeCollection; }
        }

        public EditerViewModel()
        {
            var types = Enum.GetValues(typeof (ProjectType)).Cast<ProjectType>();
            var count = types.Count() - 1;
            ProjectTypeCollection = types.Take(count).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Initalize()
        {
            m_EditerConfig = DataSerialization.DeserializeFromXmlFile<EditerConfig>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\EditerConfig.xml"));

            CRH1AConfig = DataSerialization.DeserializeFromXmlFile<CRH1AConfig>(m_EditerConfig.AbsolutePath);
        }

        public void Save()
        {
            if (m_EditerConfig != null)
            {
                DataSerialization.SerializeToXmlFile(CRH1AConfig, m_EditerConfig.AbsolutePath);
                MessageBox.Show("保存配置成功");
            }
        }
    }
}