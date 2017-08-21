using General.CIR.Resource;

namespace General.CIR.Commands.SettingItemResponse
{
	public class SpeakerResponse : CustomCommandBase
	{
		protected override void CommandAction()
		{
			ViewModel.Controller.NavigatorToKey(BtnItemKeys.设置界面扬声器音量);
			ViewModel.SettingViewModel.Trips = InFoResource.音量调节提示信息;
		}
	}
}
