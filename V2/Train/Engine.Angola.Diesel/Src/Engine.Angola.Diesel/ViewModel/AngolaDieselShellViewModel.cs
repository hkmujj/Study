using System.ComponentModel.Composition;
using Engine.Angola.Diesel.Controller;
using Engine.Angola.Diesel.Model;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.Angola.Diesel.ViewModel
{

    [Export]
    public class AngolaDieselShellViewModel : NotificationObject
    {
        public AngolaDieselShellController Controller { private set; get; }

        public AngolaDieselShellModel Model { private set; get; }

        [ImportingConstructor]
        public AngolaDieselShellViewModel(AngolaDieselShellController controller, AngolaDieselShellModel model)
        {
            Controller = controller;
            Model = model;
            controller.ViewModel = this;
        }
    }
}