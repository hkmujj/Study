using General.CIR.Models;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class MaininsatncePasswordConfirResposne : BtnResponseBase
	{
		public override void ClickUp()
		{
			bool flag = ViewModel.SettingViewModel.MaininstanceText.Text.Equals(GlobalParams.Instance.SystemConfig.Password);
			if (flag)
			{
				ViewModel.Controller.NavigatorToKey(BtnItemKeys.设置界面维护界面);
			}
		}

		public override void ClickDown()
		{
		}
	}
}
