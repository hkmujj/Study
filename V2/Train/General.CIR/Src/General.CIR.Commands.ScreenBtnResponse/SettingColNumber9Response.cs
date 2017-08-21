using General.CIR.Extentions;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class SettingColNumber9Response : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.MainContentViewModel.ColumnEndViewModel.SettingID = ViewModel.MainContentViewModel.ColumnEndViewModel.SettingID.GetColumnNumber("9", 6);
		}

		public override void ClickDown()
		{
		}
	}
}
