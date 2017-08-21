using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Model.ViewSource;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class SelectPassworkKeybordRightActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            var pm = ViewModel.Value.SecondLevelViewModel.InputPasswordViewModel.Model;
            var idx = KeybordStrings.Instance.Numbers.IndexOf(pm.SelectedKeybordValue);
            idx = (idx + 1)%KeybordStrings.Instance.Numbers.Count;
            pm.SelectedKeybordValue = KeybordStrings.Instance.Numbers[idx];
        }
    }
}