using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Adapter;
using Urban.GuiYang.DDU.Controller.Domain.PIS;
using Urban.GuiYang.DDU.Model.PIS;

namespace Urban.GuiYang.DDU.ViewModel
{
    [Export]
    public class PISViewModel : NotificationObject
    {
        [ImportingConstructor]
        public PISViewModel(PISModel model, PISController controller, EmergBroadcastViewModel emergBroadcastViewModel)
        {
            Model = model;
            Controller = controller;
            EmergBroadcastViewModel = emergBroadcastViewModel;
            emergBroadcastViewModel.Parent = this;
            controller.ViewModel = this;
            controller.Initalize();
        }

        public DomainViewModel Parent { get; set; }

        public ISendInterface SendInterface { get { return Parent.SendInterface; } }   

        public EmergBroadcastViewModel EmergBroadcastViewModel { get; private set; }

        public PISModel Model { get; private set; }

        public PISController Controller { get; private set; }
    }
}