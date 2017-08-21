using Microsoft.Practices.Prism.ViewModel;
using Motor.HMI.CRH380BG.Controller.Domain;
using Motor.HMI.CRH380BG.Model.Domain.Fault;
using System.ComponentModel.Composition;

namespace Motor.HMI.CRH380BG.ViewModel.Domain
{
    [Export]
    public class FaultViewModel : NotificationObject
    {
        [ImportingConstructor]
        public FaultViewModel(FaultController controller, FaultModel model)
        {
            
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public FaultController Controller { get; private set; }

        public FaultModel Model { get; private set; }

        public DomainViewModel Parent { get; internal set; } 
    }
}
