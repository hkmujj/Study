using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Subway.TCMS.LanZhou.Controller.Domain;
using Subway.TCMS.LanZhou.Model.Domain;

namespace Subway.TCMS.LanZhou.ViewModel
{
    [Export]
    public class TrainViewModel : NotificationObject
    {
        [ImportingConstructor]
        public TrainViewModel(TrainModel model, TrainController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public TrainModel Model { get; private set; }

        public TrainController Controller { get; private set; }

    }
}