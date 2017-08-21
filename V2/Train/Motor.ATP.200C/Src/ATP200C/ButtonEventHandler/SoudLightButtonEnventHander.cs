using ATP200C.FunctionButton;
using ATP200C.MainView.FunctionButton;
using ATP200C.PublicComponents;

namespace ATP200C.ButtonEventHandler
{
    public class SoudLightButtonEnventHander : IButtonEventHandler
    {
        public void Handle(FunctionButtonView context, ButtonInfo buttonInfo)
        {
            if (buttonInfo.FunButton.BtnContent.Equals("大"))
            {
                if (context.SoudValue < 100)
                {
                    context.SoudValue += 10;
                }

            }
            else if (buttonInfo.FunButton.BtnContent.Equals("小"))
            {
                if (context.SoudValue > 0)
                {
                    context.SoudValue -= 10;
                }
            }
        }
    }
}