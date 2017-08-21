using ATP200C.FunctionButton;
using ATP200C.MainView;
using ATP200C.MainView.FunctionButton;
using ATP200C.PopupViews;
using ATP200C.PublicComponents;

namespace ATP200C.ButtonEventHandler
{
    public class InputTrainLineButtonEventHandler : IButtonEventHandler
    {
        public void Handle(FunctionButtonView context, ButtonInfo buttonInfo)
        {

            GlobalParam.Instance.TrainLineViewItem = new InputTrainLineViewItem();

            PopupViewCanve.Instance.PopupViewItem = GlobalParam.Instance.TrainLineViewItem;
        }

    }
}