using General.CIR.Resource;

namespace General.CIR.Commands.SettingItemResponse
{
	public class TrainNumberResponse : CustomCommandBase
	{
		protected override void CommandAction()
		{
			bool trainNumberIsRegister = ViewModel.MainContentViewModel.TitleViewModel.TrainNumberIsRegister;
			if (trainNumberIsRegister)
			{
				ViewModel.Controller.NavigatorToKey(BtnItemKeys.设置界面车次号注销);
				ViewModel.SettingViewModel.Trips = string.Format("当前车次号:{0},是否注销？", ViewModel.MainContentViewModel.TitleViewModel.TrainNumber);
			}
			else
			{
				ViewModel.Controller.NavigatorToKey(BtnItemKeys.设置界面车次号注册);
				ViewModel.SettingViewModel.Trips = "【确认】确认,【回格】删除，【退出】返回";
			}
		}
	}
}
