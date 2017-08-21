using ATP200C.FunctionButton;
using ATP200C.MainView;
using ATP200C.MainView.FunctionButton;
using ATP200C.PublicComponents;

namespace ATP200C.ButtonEventHandler
{
    public class InputCmdButtonEventHandler : IButtonEventHandler
    {
        public void Handle(FunctionButtonView context, ButtonInfo buttonInfo)
        {
            PopupViewCanve.Instance.ActionCommand(context, UserActionCommandHelper.GetActionCommand(buttonInfo.FunButton.BtnContent));
        }
    }
}