using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// 乘客报警帮助按钮响应
    /// </summary>
    [Export]
    public class EmergencyTalkHelpBtnResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.乘客报警帮助);
        }
    }
}