using General.CIR.Resource;

namespace General.CIR.Commands.SettingItemResponse
{
	public class OutLibResponse : CustomCommandBase
	{
		protected override void CommandAction()
		{
			ViewModel.Controller.NavigatorToKey(BtnItemKeys.设置界面出入库检测);
		}
	}
}
