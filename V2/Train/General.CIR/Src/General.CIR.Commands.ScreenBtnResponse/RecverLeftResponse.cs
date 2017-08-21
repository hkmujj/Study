namespace General.CIR.Commands.ScreenBtnResponse
{
	public class RecverLeftResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.MainContentViewModel.TitleViewModel.ReciverSound -= 1.0;
		}

		public override void ClickDown()
		{
		}
	}
}
