namespace General.CIR.Commands.ScreenBtnResponse
{
	public class CallInputBtnResponse : BtnResponseBase
	{
		protected string InputChar
		{
			get;
			set;
		}

		public override void ClickUp()
		{
			ViewModel.CalllViewModel.CallNumber.InputString(InputChar);
		}

		public override void ClickDown()
		{
		}
	}
}
