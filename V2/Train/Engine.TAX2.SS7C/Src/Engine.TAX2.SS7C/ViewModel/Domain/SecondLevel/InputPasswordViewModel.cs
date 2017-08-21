using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller.Domain.SecondLevel;
using Engine.TAX2.SS7C.Model.Domain.SecondLevel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain.SecondLevel
{
    [Export]
    public class InputPasswordViewModel: NotificationObject
    {
        [ImportingConstructor]
        public InputPasswordViewModel(InputPasswordModel model, InputPasswordController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public InputPasswordController Controller { get; private set; }

        public InputPasswordModel Model { get; private set; }
    }
}