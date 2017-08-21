using General.CIR.Resource;

namespace General.CIR.Commands.SettingItemResponse
{
	public class MainInstanceResponse : CustomCommandBase
	{
		protected override void CommandAction()
		{
			ViewModel.Controller.NavigatorToKey(BtnItemKeys.设置界面维护界面密码输入);
		}
	}
}
