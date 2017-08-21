using ATP200C.FunctionButton;
using ATP200C.MainView;
using ATP200C.MainView.FunctionButton;
using ATP200C.PublicComponents;

namespace ATP200C.ButtonEventHandler
{
    public class TextInfoDownPageEventHandler : IButtonEventHandler
    {
        public void Handle(FunctionButtonView context, ButtonInfo buttonInfo)
        {
            if (TextInfoManager.Instance.CanGoForward)
            {
                TextInfoManager.Instance.GoForward();
            }

        }
    }
}