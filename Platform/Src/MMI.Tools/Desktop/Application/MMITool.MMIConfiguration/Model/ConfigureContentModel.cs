using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;

namespace MMITool.Addin.MMIConfiguration.Model
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConfigureContentModel: NotificationObject
    {
        private ConfigFileModel m_CurrentConfigFileModel;

        public ConfigFileModel CurrentConfigFileModel
        {
            set
            {
                if (Equals(value, m_CurrentConfigFileModel))
                {
                    return;
                }
                m_CurrentConfigFileModel = value;
                RaisePropertyChanged(() => CurrentConfigFileModel);
            }
            get { return m_CurrentConfigFileModel; }
        }
    }
}