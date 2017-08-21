using General.CIR.Resource;

namespace General.CIR.Commands.SettingItemResponse
{
	public class ColumnInputResponse : CustomCommandBase
	{
		protected override void CommandAction()
		{
			ViewModel.Controller.NavigatorToKey(BtnItemKeys.设置界面列尾输入);
		}
	}
}
