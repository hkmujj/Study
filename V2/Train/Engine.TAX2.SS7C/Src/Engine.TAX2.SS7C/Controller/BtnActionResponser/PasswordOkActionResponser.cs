using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Resources.Keys;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class PasswordOkActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            var pd = ViewModel.Value.SecondLevelViewModel.InputPasswordViewModel;
            pd.Controller.AddPasswordChar(pd.Model.SelectedKeybordValue);

            if (pd.Controller.VerifyPassword())
            {
                NavigateTo(StateKeys.Root_二级显示);
            }
        }
    }
}