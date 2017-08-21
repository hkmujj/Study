using General.CIR.Extentions;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class SettingColNumber2Response : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.MainContentViewModel.ColumnEndViewModel.SettingID = ViewModel.MainContentViewModel.ColumnEndViewModel.SettingID.GetColumnNumber("2", 6);
		}

		public override void ClickDown()
		{
		}
	}
}
