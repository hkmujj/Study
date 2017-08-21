using System.ComponentModel.Composition;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class PasswordChangeActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ��Ӧ����
        /// </summary>
        public override void ResponseClick()
        {
            var pd = ViewModel.Value.SecondLevelViewModel.InputPasswordViewModel;
            pd.Controller.ModifyPassword();
        }
    }
}