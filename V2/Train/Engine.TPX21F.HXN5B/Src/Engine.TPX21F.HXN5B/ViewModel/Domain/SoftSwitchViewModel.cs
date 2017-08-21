using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Controller.Domain.SoftSwitch;
using Engine.TPX21F.HXN5B.Model.Domain.SoftSwitch;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.ViewModel.Domain
{
    [Export]
    public class SoftSwitchViewModel : NotificationObject
    {
        [ImportingConstructor]
        public SoftSwitchViewModel(SoftSwitchController controller, SoftSwitchModel model)
        {
            Controller = controller;
            controller.ViewModel = this;
            Model = model;
            controller.Initalize();
        }

        public SoftSwitchController Controller { private set; get; }

        public SoftSwitchModel Model { private set; get; }
    }
}