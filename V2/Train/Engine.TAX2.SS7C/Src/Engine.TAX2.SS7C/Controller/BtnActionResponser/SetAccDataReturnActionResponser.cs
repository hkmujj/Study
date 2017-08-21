using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Resources.Keys;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class SetAccDataReturnActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            if (!ViewModel.Value.SecondLevelViewModel.SetAccDataViewModel.Model.HasAnyModified)
            {
                NavigateBack();
            }
            else
            {
                NavigateTo(StateKeys.Root_二级显示_参数设置_置累计参数确认取消修改);
                ViewModel.Value.SecondLevelViewModel.SetAccDataViewModel.Model.IsSureModify = true;
            }
        }
    }
}