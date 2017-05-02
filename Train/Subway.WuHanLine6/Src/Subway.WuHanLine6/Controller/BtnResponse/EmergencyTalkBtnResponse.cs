using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    ///     乘客报警按钮响应
    /// </summary>
    [Export]
    public class EmergencyTalkBtnResponse : BtnResponseBase
    {
        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.乘客报警);
        }
    }
}