namespace General.CIR.Commands.ScreenBtnResponse
{
	public class SerchConfirmResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.DispatchCommandViewModel.SelectSerchItem.Response.Command.Execute();
		}

		public override void ClickDown()
		{
		}
	}
}
