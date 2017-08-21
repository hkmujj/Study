using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Model.ViewSource;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class SelectPassworkKeybordLeftActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            var pm = ViewModel.Value.SecondLevelViewModel.InputPasswordViewModel.Model;
            var idx = KeybordStrings.Instance.Numbers.IndexOf(pm.SelectedKeybordValue);
            idx = ( idx + KeybordStrings.Instance.Numbers.Count -1 ) % KeybordStrings.Instance.Numbers.Count;
            pm.SelectedKeybordValue = KeybordStrings.Instance.Numbers[idx];
        }
    }
}