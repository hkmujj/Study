using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Controller.Domain.PIS;
using Urban.GuiYang.DDU.Model.PIS.EmergBroadcast;

namespace Urban.GuiYang.DDU.ViewModel
{
    [Export]
    public class EmergBroadcastViewModel : NotificationObject
    {
        [ImportingConstructor]
        public EmergBroadcastViewModel(EmergBroadcastModel model, EmergBroadcastController controller)
        {
            Model = model;
            Controller = controller;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public PISViewModel Parent { get; set; }

        public EmergBroadcastModel Model { get; private set; }

        public EmergBroadcastController Controller { get; private set; }
    }
}