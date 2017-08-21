using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Config;

namespace MMITool.Addin.MMIConfiguration.Model
{
    [Export]
    public class AppConfigModel: NotificationObject
    {
        private AppConfig m_AppConfig;

        public AppConfig AppConfig
        {
            set
            {
                if (Equals(value, m_AppConfig))
                {
                    return;
                }
                m_AppConfig = value;
                RaisePropertyChanged(() => AppConfig);
            }
            get { return m_AppConfig; }
        }
    }
}