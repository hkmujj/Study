using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Controller.Domain;
using Engine.TAX2.SS7C.Model.Domain;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TAX2.SS7C.ViewModel.Domain
{
    [Export]
    public class OtherViewModel : NotificationObject
    {
        [ImportingConstructor]
        public OtherViewModel(OtherModel model, OtherController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public OtherModel Model { private set; get; }

        public OtherController Controller { private set; get; }

    }
}