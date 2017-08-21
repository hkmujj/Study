using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Motor.TCMS.CRH400BF.Controller;
using Motor.TCMS.CRH400BF.Model.Train;

namespace Motor.TCMS.CRH400BF.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
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
