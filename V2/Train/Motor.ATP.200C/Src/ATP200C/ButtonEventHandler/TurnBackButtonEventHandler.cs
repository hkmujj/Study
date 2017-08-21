using ATP200C.FunctionButton;
using ATP200C.MainView;
using ATP200C.MainView.FunctionButton;
using ATP200C.PublicComponents;

namespace ATP200C.ButtonEventHandler
{
    public class TurnBackButtonEventHandler : IButtonEventHandler
    {
        public void Handle(FunctionButtonView context, ButtonInfo buttonInfo)
        {
            PopupViewCanve.Instance.PopupViewItem = null;
        }
    }
}