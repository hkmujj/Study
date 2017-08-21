using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Adapter;
using Urban.GuiYang.DDU.Controller.Domain;
using Urban.GuiYang.DDU.Model;

namespace Urban.GuiYang.DDU.ViewModel
{
    [Export]
    public class DomainViewModel : NotificationObject
    {
        [ImportingConstructor]
        public DomainViewModel(DomainController controller, DomainModel model, TrainViewModel trainViewModel, PISViewModel pisViewModel, ISendInterface sendInterface, DebuggerViewModel debuggerViewModel)
        {
            Controller = controller;
            controller.ViewModel = this;
            Model = model;
            TrainViewModel = trainViewModel;
            PISViewModel = pisViewModel;
            pisViewModel.Parent = this;
            SendInterface = sendInterface;
            DebuggerViewModel = debuggerViewModel;

            controller.Initalize();

            debuggerViewModel.DomainViewModel = this;
            
        }

        public ISendInterface SendInterface { get; private set; }

        public DomainController Controller { private set; get; }

        public DomainModel Model { private set; get; }

        public TrainViewModel TrainViewModel { get; private set; }

        public PISViewModel PISViewModel { get; private set; }

        public DebuggerViewModel DebuggerViewModel { get; private set; }
    }
}