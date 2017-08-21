using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Controller.Domain;
using Subway.TCMS.LanZhou.Model.Domain.Fault;

namespace Subway.TCMS.LanZhou.ViewModel
{
    [Export]
    public class FalutViewModel : NotificationObject
    {
        [ImportingConstructor]
        public FalutViewModel(FaultModel model, FaultController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public FaultModel Model { get; private set; }

        public FaultController Controller { get; private set; }
    }
}