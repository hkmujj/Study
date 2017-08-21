using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Controller.Domain;
using Engine.TPX21F.HXN5B.Model.Domain.Other;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.ViewModel.Domain
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