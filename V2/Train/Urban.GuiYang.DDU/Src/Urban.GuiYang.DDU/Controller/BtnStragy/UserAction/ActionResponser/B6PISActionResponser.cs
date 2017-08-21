using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Urban.GuiYang.DDU.Constant;
using Urban.GuiYang.DDU.View.Contents.PIS;

namespace Urban.GuiYang.DDU.Controller.BtnStragy.UserAction.ActionResponser
{
    [Export]
    public class B6PISActionResponser : OrdinaryActionResponser
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            RequestNavigateToContent(typeof(PISContentLayoutView));
            RegionManager.RequestNavigate(RegionNames.ContentContentContentPISContent, typeof(AutoModelView).FullName);
        }
    }
}