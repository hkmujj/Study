using System.ComponentModel.Composition;
using Urban.GuiYang.DDU.View.Contents.Contents;

namespace Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.ActionResponser
{
    [Export]
    public class B1MainViewActionResponser : OrdinaryActionResponser
    {
        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            RequestNavigateToContentContent(typeof(MainViewContentView));
        }
    }
}