using General.CIR.Resource;

namespace General.CIR.Commands.SettingItemResponse
{
	public class RunRegionResponse : CustomCommandBase
	{
		protected override void CommandAction()
		{
			ViewModel.Controller.NavigatorToKey(BtnItemKeys.设置界面运行区段);
		}
	}
}
