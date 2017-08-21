using System.ComponentModel.Composition;
using Urban.GuiYang.DDU.Controller.Domain;

namespace Urban.GuiYang.DDU.ViewModel
{
    [Export]
    public class DebuggerViewModel 
    {
        [ImportingConstructor]
        public DebuggerViewModel(DebuggerController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
            
        }

        public DebuggerController Controller { get; private set; }

        public DomainViewModel DomainViewModel { get; set; }

        public void Initalize()
        {
            Controller.Initalize();
        }

    }
}