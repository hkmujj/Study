using System.Collections.Generic;
using System.ComponentModel.Composition;
using MMITool.Addin.MMIConfiguration.Controller;
using MMITool.Addin.MMIConfiguration.Model;

namespace MMITool.Addin.MMIConfiguration.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ActureFormConfigViewModel : ConfigureContentEditerViewModeBase<ActureFormConfigController>
    {
        private string m_TargetConfigFile;
        private ActureFormConfigModel m_Model;

        public ConfigNavigateViewModel ConfigNavigateViewModel { private set; get; }

        public List<ActureFormConfigModel> ActureFormConfigModelCollection{ get; set; }

        public override string TargetConfigFile
        {
            get { return m_TargetConfigFile; }
            protected set
            {
                m_TargetConfigFile = value;
                Controller.UpdateCurrentConfigModel();
            }
        }

        public ActureFormConfigModel Model
        {
            set
            {
                if (Equals(value, m_Model))
                {
                    return;
                }

                m_Model = value;
                RaisePropertyChanged(() => Model);
            }
            get { return m_Model; }
        }

        [ImportingConstructor]
        public ActureFormConfigViewModel(ActureFormConfigController controller, ConfigNavigateViewModel configNavigateViewModel)
        {
            ConfigNavigateViewModel = configNavigateViewModel;
            Controller = controller;
            controller.ViewModel = this;
        }
    }
}