using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Constant;
using Engine.TAX2.SS7C.Resources.Keys;
using Engine.TAX2.SS7C.View.Contents;
using Microsoft.Practices.Prism.Regions;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToModifyTimeActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_时间设置);
            RegionManager.RequestNavigate(RegionNames.ContentModifyTime, typeof(ModifyTimeView).FullName);
            ViewModel.Value.OtherViewModel.Controller.ModifyTimeController.ResetSettingTime();
        }
    }
}