using System.ComponentModel.Composition;
using Subway.WuHanLine6.Resource.Keys;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class StationSelectConfirm : BtnResponseBase
    {
        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            ViewModel.Model.StationModel.Controller.StaionName.Execute(ViewModel.Model.StationModel.SelectInfo?.Name);
            ViewModel.Controller.Navigator(StateInterfaceKeys.车站设定);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class StationSelectBakc : BtnResponseBase
    {
        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            ViewModel.Controller.Navigator(StateInterfaceKeys.车站设定);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class StationSettingConfirm : BtnResponseBase
    {
        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            ViewModel.Model.StationModel.Controller.Confirm.Execute(null);
        }
    }
}