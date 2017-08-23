using System.ComponentModel.Composition;
using Urban.GuiYang.DDU.View.Contents;
using Urban.GuiYang.DDU.View.Contents.PIS;

namespace Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.ActionResponser
{
    [Export]
    public class B8EmergBroadcastReturnActionResponser : OrdinaryActionResponser
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            Domain.SendInterface.ClearEmergBroadcast();

            RequestNavigateToContent(typeof(PISContentLayoutView));
        }
    }
    [Export]
    public class B8FaultReturnActionResponser : OrdinaryActionResponser
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {

            RequestNavigateToContent(typeof(ContentShell1));
        }
    }
}