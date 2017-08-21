using System.ComponentModel.Composition;
using Urban.GuiYang.DDU.View.Contents.Contents;

namespace Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.ActionResponser
{
    [Export]
    public class B4AssistActionResponser : OrdinaryActionResponser
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            RequestNavigateToContentContent(typeof(AssistContentView));
        }
    }
}