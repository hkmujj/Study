using General.CIR.Resource;

namespace General.CIR.Commands.SettingItemResponse
{
	public class ScreenResponse : CustomCommandBase
	{
		protected override void CommandAction()
		{
			ViewModel.Controller.NavigatorToKey(BtnItemKeys.设置界面屏幕亮度);
			ViewModel.SettingViewModel.Trips = InFoResource.亮度调节提示信息;
		}
	}
}
