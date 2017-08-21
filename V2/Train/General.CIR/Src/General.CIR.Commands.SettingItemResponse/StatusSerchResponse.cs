using General.CIR.Resource;

namespace General.CIR.Commands.SettingItemResponse
{
	public class StatusSerchResponse : CustomCommandBase
	{
		protected override void CommandAction()
		{
			ViewModel.Controller.NavigatorToKey(BtnItemKeys.设置界面状态查询);
		}
	}
}
