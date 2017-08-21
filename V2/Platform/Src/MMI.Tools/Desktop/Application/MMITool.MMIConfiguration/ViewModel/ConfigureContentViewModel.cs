using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMITool.Addin.MMIConfiguration.Controller;
using MMITool.Addin.MMIConfiguration.Model;

namespace MMITool.Addin.MMIConfiguration.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConfigureContentViewModel : NotificationObject
    {
        private ConfigureContentController m_Controller;
        private ConfigureContentModel m_Model;

        [ImportingConstructor]
        public ConfigureContentViewModel(ConfigureContentController contentController, ConfigureContentModel contentModel)
        {
            Model = contentModel;
            Controller = contentController;
        }

        public ConfigureContentController Controller
        {
            private set
            {
                if (Equals(value, m_Controller))
                {
                    return;
                }
                m_Controller = value;
                m_Controller.ViewModel = this;
                RaisePropertyChanged(() => Controller);
            }
            get { return m_Controller; }
        }

        public ConfigureContentModel Model
        {
            private set
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
    }
}