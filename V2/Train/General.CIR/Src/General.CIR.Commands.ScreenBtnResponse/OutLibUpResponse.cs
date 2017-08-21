using General.CIR.Extentions;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class OutLibUpResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.SettingViewModel.OutLibViewModel.SelectItem = ViewModel.SettingViewModel.OutLibViewModel.AllOutLibItems.GetLastItem(ViewModel.SettingViewModel.OutLibViewModel.SelectItem, true);
		}

		public override void ClickDown()
		{
		}
	}
}
