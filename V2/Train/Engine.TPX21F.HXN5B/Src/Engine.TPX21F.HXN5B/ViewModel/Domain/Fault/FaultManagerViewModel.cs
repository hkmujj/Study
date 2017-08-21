using System;
using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Controller.Domain.Fault;
using Engine.TPX21F.HXN5B.Model.Domain.Fault;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TPX21F.HXN5B.ViewModel.Domain.Fault
{
    [Export]
    public class FaultManagerViewModel :NotificationObject
    {
        [ImportingConstructor]
        public FaultManagerViewModel(FaultManagerController controller, FaultManagerModel model, Lazy<DomainViewModel> parent)
        {
            Controller = controller;
            Model = model;
            Parent = parent;
        }

        public FaultManagerController Controller { get; private set; }

        public FaultManagerModel Model { get; private set; }

        public Lazy<DomainViewModel> Parent { get; private set; }
    }
}