using System;
using System.ComponentModel.Composition;
using Urban.GuiYang.DDU.Controller.Domain.Train.CarPart;
using Urban.GuiYang.DDU.View.Contents.Contents;

namespace Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.ActionResponser
{
    [Export]
    public class B3BrakeActionResponser : OrdinaryActionResponser
    {
        [ImportingConstructor]
        public B3BrakeActionResponser(Lazy<BrakeController> brakeController)
        {
            BrakeController = brakeController;
        }

        public Lazy<BrakeController> BrakeController { get; private set; }

        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            RequestNavigateToContentContent(typeof(BrakePage1ContentView));

            BrakeController.Value.GotoFirstView();
        }
    }
}