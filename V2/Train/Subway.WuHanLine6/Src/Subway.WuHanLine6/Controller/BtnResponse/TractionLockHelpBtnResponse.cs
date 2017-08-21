using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// 牵引封锁界面帮助按钮响应
    /// </summary>
    [Export]
    public class TractionLockHelpBtnResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.牵引封锁帮助);
        }
    }
}