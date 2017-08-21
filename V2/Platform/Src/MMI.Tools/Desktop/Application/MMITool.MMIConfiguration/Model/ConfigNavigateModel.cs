using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace MMITool.Addin.MMIConfiguration.Model
{
    [Export]
    public class ConfigNavigateModel : NotificationObject
    {
        private bool m_IsSeniorConfigModel;
        private ObservableCollection<ConfigFileModel> m_ConfigFileCollection;

        public bool IsSeniorConfigModel
        {
            set
            {
                if (value.Equals(m_IsSeniorConfigModel))
                {
                    return;
                }
                m_IsSeniorConfigModel = value;
                RaisePropertyChanged(() => IsSeniorConfigModel);
            }
            get { return m_IsSeniorConfigModel; }
        }

        public ObservableCollection<ConfigFileModel> ConfigConfigFileCollection
        {
            get { return m_ConfigFileCollection; }
            private set
            {
                if (Equals(value, m_ConfigFileCollection))
                {
                    return;
                }
                m_ConfigFileCollection = value;
                RaisePropertyChanged(() => ConfigConfigFileCollection);
            }
        }

        public ConfigNavigateModel()
        {
            IsSeniorConfigModel = false;
            ConfigConfigFileCollection = new ObservableCollection<ConfigFileModel>();
        }
    }
}