using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class MasterGSMRSettingResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			Response(BtnItemKeys.设置界面);
			ViewModel.SettingViewModel.Trips = InFoResource.设置主界面提示信息;
		}

		public override void ClickDown()
		{
		}
	}
}
