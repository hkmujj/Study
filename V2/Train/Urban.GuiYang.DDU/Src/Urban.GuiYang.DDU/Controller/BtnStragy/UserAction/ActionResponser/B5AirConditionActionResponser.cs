using System;
using System.ComponentModel.Composition;
using Urban.GuiYang.DDU.Controller.Domain.Train.CarPart;
using Urban.GuiYang.DDU.View.Contents.Contents;

namespace Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.ActionResponser
{
    [Export]
    public class B5AirConditionActionResponser : OrdinaryActionResponser
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        [ImportingConstructor]
        public B5AirConditionActionResponser(Lazy<AirConditionController> airConditionController)
        {
            AirConditionController = airConditionController;
        }

        public Lazy<AirConditionController> AirConditionController { get; private set; }

        public override void ResponseClick()
        {
            RequestNavigateToContentContent(typeof(AirConditionPage1ContentView));
            AirConditionController.Value.GotoFirstView();
        }
    }
}