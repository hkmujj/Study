using ATP200C.FunctionButton;
using ATP200C.MainView.FunctionButton;

namespace ATP200C.PublicComponents
{
    public interface IButtonEventHandler
    {
        void Handle(FunctionButtonView context, ButtonInfo buttonInfo);

    }
}