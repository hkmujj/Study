using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Controller.Domain;
using Urban.GuiYang.DDU.Model.Train;

namespace Urban.GuiYang.DDU.ViewModel
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