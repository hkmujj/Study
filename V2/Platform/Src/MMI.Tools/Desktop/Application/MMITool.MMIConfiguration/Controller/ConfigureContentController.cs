using System.ComponentModel.Composition;
using System.Windows.Input;
using MMI.Facility.WPFInfrastructure.Interfaces;
using MMITool.Addin.MMIConfiguration.Model;
using MMITool.Addin.MMIConfiguration.ViewModel;

namespace MMITool.Addin.MMIConfiguration.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConfigureContentController : ControllerBase<ConfigureContentViewModel>
    {
        private ICommand m_OkCommand;
        private ICommand m_CancelCommand;

        public void Select(ConfigFileModel configFileModel)
        {
            ViewModel.Model.CurrentConfigFileModel = configFileModel;
        }

        public ICommand OkCommand
        {
            set
            {
                if (Equals(value, m_OkCommand))
                {
                    return;
                }
                m_OkCommand = value;
                RaisePropertyChanged(() => OkCommand);
            }
            get { return m_OkCommand; }
        }

        public ICommand CancelCommand
        {
            set
            {
                if (Equals(value, m_CancelCommand))
                {
                    return;
                }
                m_CancelCommand = value;
                RaisePropertyChanged(() => CancelCommand);
            }
            get { return m_CancelCommand; }
        }
    }
}