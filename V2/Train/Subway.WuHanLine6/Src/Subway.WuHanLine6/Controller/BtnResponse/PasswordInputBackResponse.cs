using System.ComponentModel.Composition;

namespace Subway.WuHanLine6.Controller.BtnResponse
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class PasswordInputBackResponse : BtnResponseBase
    {
        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            ViewModel.Controller.Navigator(ViewModel.Model.PasswordModel.BackView);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Export]
    public class PasswordInputConfirmResponse : BtnResponseBase
    {
        /// <summary>
        ///     按钮按下操作
        /// </summary>
        public override void ButtonClick()
        {
            if (ViewModel.Model.PasswordModel.Controller.CanConfirm())
            {
                ViewModel.Controller.Navigator(ViewModel.Model.PasswordModel.ConfirmView);
                ViewModel.Model.PasswordModel.DisplayPassword = string.Empty;
            }
        }
    }
}