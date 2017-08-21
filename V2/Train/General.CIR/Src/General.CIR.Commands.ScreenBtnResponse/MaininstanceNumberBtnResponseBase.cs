namespace General.CIR.Commands.ScreenBtnResponse
{
	public class MaininstanceNumberBtnResponseBase : BtnResponseBase
	{
		protected string InputChar
		{
			get;
			set;
		}

		public override void ClickUp()
		{
			ViewModel.SettingViewModel.MaininstanceText.InputString(InputChar);
		}

		public override void ClickDown()
		{
		}
	}
}
