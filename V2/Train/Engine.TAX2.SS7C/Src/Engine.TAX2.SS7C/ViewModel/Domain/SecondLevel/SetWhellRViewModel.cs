using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller.Domain.SecondLevel;
using Engine.TAX2.SS7C.Model.Domain.SecondLevel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain.SecondLevel
{
    [Export]
    public class SetWhellRViewModel : NotificationObject
    {
        private SetWhellRModel m_Model;

        [ImportingConstructor]
        public SetWhellRViewModel(SetWhellRModel model, SetWhellRController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public SetWhellRModel Model
        {
            get { return m_Model; }
            private set
            {
                if (Equals(value, m_Model))
                {
                    return;
                }

                m_Model = value;
                RaisePropertyChanged(() => Model);
            }
        }

        public SetWhellRController Controller { get; private set; } 
    }
}