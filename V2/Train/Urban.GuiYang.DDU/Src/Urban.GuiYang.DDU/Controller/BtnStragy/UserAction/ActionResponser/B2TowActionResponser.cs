using System.ComponentModel.Composition;
using Urban.GuiYang.DDU.View.Contents.Contents;

namespace Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.ActionResponser
{
    [Export]
    public class B2TowActionResponser : OrdinaryActionResponser
    {
        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            RequestNavigateToContentContent(typeof (TowContentView));
        }
    }
}