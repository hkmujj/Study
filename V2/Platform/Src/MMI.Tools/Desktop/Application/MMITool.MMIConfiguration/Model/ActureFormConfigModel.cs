using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.DataType.Config.Form;

namespace MMITool.Addin.MMIConfiguration.Model
{
    [Export]
    public class ActureFormConfigModel : NotificationObject
    {
        private ActureFormConfig m_ActureFormConfig;

        public string TargetConfigPath { get; set; }

        public string TargetConfigFile{ get; set; }


        public ActureFormConfig ActureFormConfig
        {
            set
            {
                if (Equals(value, m_ActureFormConfig))
                {
                    return;
                }
                m_ActureFormConfig = value;
                RaisePropertyChanged(() => ActureFormConfig);
            }
            get { return m_ActureFormConfig; }
        }
    }
}