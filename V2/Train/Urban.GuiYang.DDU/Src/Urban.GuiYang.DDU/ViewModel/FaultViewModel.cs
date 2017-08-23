using System.ComponentModel.Composition;
using Urban.GuiYang.DDU.Controller.Domain;
using Urban.GuiYang.DDU.Model.Train;

namespace Urban.GuiYang.DDU.ViewModel
{
    [Export]
    public class FaultViewModel
    {
        [Import]
        public FaultModel Model { get; private set; }
        [Import]
        public FaultController Controller { get; private set; }
    }
}
