using General.CIR.Extentions;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class SettingColNumber6Response : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.MainContentViewModel.ColumnEndViewModel.SettingID = ViewModel.MainContentViewModel.ColumnEndViewModel.SettingID.GetColumnNumber("6", 6);
		}

		public override void ClickDown()
		{
		}
	}
}
