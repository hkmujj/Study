namespace General.CIR.Commands.ScreenBtnResponse
{
	public class SpeakerRightResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.MainContentViewModel.TitleViewModel.SpeakerSound += 1.0;
		}

		public override void ClickDown()
		{
		}
	}
}
