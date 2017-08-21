using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMITool.Addin.MMIConfiguration.Controller;
using MMITool.Addin.MMIConfiguration.Model;

namespace MMITool.Addin.MMIConfiguration.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SelectExePathViewModel : NotificationObject
    {
        private SelectExePathController m_Controller;
        private SelectExePathModel m_Model;

        [ImportingConstructor]
        public SelectExePathViewModel(SelectExePathController controller, SelectExePathModel model)
        {
            Model = model;
            Controller = controller;
            model.PropertyChanged += (sender, args) =>
            {
                controller.ParserConfigCommand.RaiseCanExecuteChanged();
                controller.StartSelectedExeCommand.RaiseCanExecuteChanged();
            };
        }

        public SelectExePathController Controller
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

        public SelectExePathModel Model
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
