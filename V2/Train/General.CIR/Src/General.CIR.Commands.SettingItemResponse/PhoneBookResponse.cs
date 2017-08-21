using General.CIR.Resource;

namespace General.CIR.Commands.SettingItemResponse
{
	public class PhoneBookResponse : CustomCommandBase
	{
		protected override void CommandAction()
		{
			ViewModel.Controller.NavigatorToKey(BtnItemKeys.设置界面查询通讯录);
		}
	}
}
