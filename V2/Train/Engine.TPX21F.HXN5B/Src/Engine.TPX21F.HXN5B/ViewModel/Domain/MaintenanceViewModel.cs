using System;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Controller.Domain.Maintenance;
using Engine.TPX21F.HXN5B.Model.Domain.Maintenance;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.ViewModel.Domain
{
    [Export]
    public class MaintenanceViewModel : NotificationObject
    {
        [ImportingConstructor]
        public MaintenanceViewModel(MaintenanceModel model, MaintenanceController controller, Lazy<DomainViewModel> parent)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            Parent = parent;
            controller.Initalize();
        }

        public MaintenanceController Controller { get; private set; }

        public MaintenanceModel Model { get; private set; }

        public Lazy<DomainViewModel> Parent { get; private set; }

    }
}
