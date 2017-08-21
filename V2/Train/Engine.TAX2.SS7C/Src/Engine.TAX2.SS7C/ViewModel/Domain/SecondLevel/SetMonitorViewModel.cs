using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller.Domain.SecondLevel;
using Engine.TAX2.SS7C.Model.Domain.SecondLevel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain.SecondLevel
{
    [Export]
    public class SetMonitorViewModel : NotificationObject
    {
        [ImportingConstructor]
        public SetMonitorViewModel(SetMonitorModel model, SetMonitorController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public SetMonitorController Controller { get; private set; }

        public SetMonitorModel Model { get; private set; }
    }
}