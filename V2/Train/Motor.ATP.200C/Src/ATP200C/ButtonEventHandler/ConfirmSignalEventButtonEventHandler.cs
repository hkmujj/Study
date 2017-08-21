using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATP200C.FunctionButton;
using ATP200C.MainView;
using ATP200C.MainView.FunctionButton;
using ATP200C.PublicComponents;

namespace ATP200C.ButtonEventHandler
{
    public class ConfirmSignalEventButtonEventHandler : IButtonEventHandler
    {
        public void Handle(FunctionButtonView context, ButtonInfo buttonInfo)
        {
            OnConfirmed(context);
        }

        public static Action<ATPBaseClass> Confirmed;

        private static void OnConfirmed(ATPBaseClass obj)
        {
            var handler = Confirmed;
            if (handler != null)
            {
                handler(obj);
            }
        }
    }
}
