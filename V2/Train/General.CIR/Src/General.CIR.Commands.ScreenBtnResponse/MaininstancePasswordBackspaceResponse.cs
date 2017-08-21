namespace General.CIR.Commands.ScreenBtnResponse
{
	public class MaininstancePasswordBackspaceResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
		}

		public override void ClickDown()
		{
			ViewModel.SettingViewModel.MaininstanceText.Delete();
		}
	}
}
