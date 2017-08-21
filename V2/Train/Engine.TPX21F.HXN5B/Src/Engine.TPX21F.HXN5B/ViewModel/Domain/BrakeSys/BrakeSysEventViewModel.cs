using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Controller.Domain.BrakeSys.Events;
using Engine.TPX21F.HXN5B.Model.Domain.BrakeSys.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.ViewModel.Domain.BrakeSys
{
    [Export]
    public class BrakeSysEventViewModel : NotificationObject
    {
        [ImportingConstructor]
        public BrakeSysEventViewModel(BrakeSysEventModel model, BrakeSysEventController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public BrakeSysEventModel Model { get; private set; }

        public BrakeSysEventController Controller { get; private set; }
    }
}