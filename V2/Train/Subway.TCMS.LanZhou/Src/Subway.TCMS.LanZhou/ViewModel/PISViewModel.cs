using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Controller.Domain;
using Subway.TCMS.LanZhou.Model.Domain;

namespace Subway.TCMS.LanZhou.ViewModel
{
    [Export]
    public class PISViewModel : NotificationObject
    {
        [ImportingConstructor]
        public PISViewModel(PISModel model, PISController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public PISModel Model { get; private set; }

        public PISController Controller { get; private set; }
    }
}