using System.ComponentModel.Composition;

namespace Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.StateProvider
{
    [Export]
    public class EmptyStateProvider: OrdinaryStateProvider
    {
        public EmptyStateProvider()
        {
            Visible = false;
        }
    }
}