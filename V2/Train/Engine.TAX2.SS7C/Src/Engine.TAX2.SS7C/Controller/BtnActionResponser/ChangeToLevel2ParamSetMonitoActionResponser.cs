using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Resources.Keys;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToLevel2ParamSetMonitoActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_二级显示_参数设置_置监控信号);
            ViewModel.Value.SecondLevelViewModel.SetMonitorViewModel.Controller.Reset();
        }
    }
}