using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Config;

namespace MMITool.Addin.MMIConfiguration.Model
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SystemConfigModel : NotificationObject
    {
        private SystemConfig m_SystemConfig;

        public SystemConfig SystemConfig
        {
            set
            {
                if (Equals(value, m_SystemConfig))
                {
                    return;
                }
                m_SystemConfig = value;
                RaisePropertyChanged(() => SystemConfig);
            }
            get { return m_SystemConfig; }
        }
    }
}