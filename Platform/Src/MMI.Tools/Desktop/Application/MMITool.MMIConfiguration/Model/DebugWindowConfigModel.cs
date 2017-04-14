using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Config;

namespace MMITool.Addin.MMIConfiguration.Model
{
    [Export]
    public class DebugWindowConfigModel : NotificationObject
    {
        private DebugWindowConfig m_DebugWindowConfig;

        public DebugWindowConfig DebugWindowConfig
        {
            set
            {
                if (Equals(value, m_DebugWindowConfig))
                {
                    return;
                }
                m_DebugWindowConfig = value;
                RaisePropertyChanged(() => DebugWindowConfig);
            }
            get { return m_DebugWindowConfig; }
        }
    }
}