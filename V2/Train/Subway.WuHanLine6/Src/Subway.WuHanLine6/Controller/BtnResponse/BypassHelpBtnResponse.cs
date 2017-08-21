using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// 旁路界面帮助按钮导航
    /// </summary>
    [Export]
    public class BypassHelpBtnResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.旁路界面帮助);
        }
    }
}