using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// 制动自检界面返回按钮
    /// </summary>
    [Export]
    public class BrakeAutoCheckReturnResponse : BtnResponseBase
    {
        /// <summary>
        /// 按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            Navigator(StateInterfaceKeys.维修菜单);
            ViewModel.Model.BrakeAutoCheckModel.IsAutoChecking = false;
            ViewModel.Model.BrakeAutoCheckModel.Controller.AutoCheckEnd.Execute();
        }
    }
}