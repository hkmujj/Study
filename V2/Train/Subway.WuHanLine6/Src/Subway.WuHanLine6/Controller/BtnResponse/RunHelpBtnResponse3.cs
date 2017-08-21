using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// 运行帮助界面3
    /// </summary>
    [Export]
    public class RunHelpBtnResponse3 : BtnResponseBase
    {
        /// <summary>
        /// 按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.运行界面帮助3);
        }
    }
}