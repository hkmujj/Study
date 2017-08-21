using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// 紧急广播按钮响应
    /// </summary>
    [Export]
    public class EnmergencyBordercastBtnResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.紧急广播);
        }
    }
}