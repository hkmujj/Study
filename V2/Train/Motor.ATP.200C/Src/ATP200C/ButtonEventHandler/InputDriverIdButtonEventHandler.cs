using ATP200C.FunctionButton;
using ATP200C.MainView;
using ATP200C.MainView.FunctionButton;
using ATP200C.PopupViews;
using ATP200C.PublicComponents;

namespace ATP200C.ButtonEventHandler
{
    public class InputDriverIdButtonEventHandler : IButtonEventHandler
    {
        public void Handle(FunctionButtonView context, ButtonInfo buttonInfo)
        {
            if (GlobalParam.Instance.DriverIdViewItem==null)
            {
                GlobalParam.Instance.DriverIdViewItem=new InputDriverIDViewItem();
            }
            PopupViewCanve.Instance.PopupViewItem = GlobalParam.Instance.DriverIdViewItem;
        }

    }
}