using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller.Domain.SecondLevel;
using Engine.TAX2.SS7C.Model.Domain.SecondLevel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain.SecondLevel
{
    [Export]
    public class SetAccDataViewModel : NotificationObject
    {
        [ImportingConstructor]
        public SetAccDataViewModel(SetAccDataModel model, SetAccDataController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public SetAccDataModel Model { get; private set; }

        public SetAccDataController Controller { get; private set; }
    }
}