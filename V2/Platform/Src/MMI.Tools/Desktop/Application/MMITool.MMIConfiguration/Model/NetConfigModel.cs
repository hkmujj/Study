using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Config;
using MMI.Facility.DataType.Config.Net;

namespace MMITool.Addin.MMIConfiguration.Model
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NetConfigModel : NotificationObject
    {
        private NetConfig m_NetConfig;
        private List<string> m_LocIpNameCollection = new List<string>();

        public List<string> LocIpNameCollection
        {
            get { return m_LocIpNameCollection; }
            set
            {
                if (Equals(value, m_LocIpNameCollection))
                {
                    return;
                }
                m_LocIpNameCollection = value;
                RaisePropertyChanged(() => LocIpNameCollection);
            }
        }

        public NetConfig NetConfig
        {
            get { return m_NetConfig; }
            set
            {
                if (Equals(value, m_NetConfig))
                {
                    return;
                }
                m_NetConfig = value;
                RaisePropertyChanged(() => NetConfig);
            }
        }
    }
}
