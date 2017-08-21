using General.CIR.Extentions;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class MaininstanceBottonResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.MaininstanceViewModel.SelectItem = ViewModel.MaininstanceViewModel.DisplayItems.GetNextItem(ViewModel.MaininstanceViewModel.SelectItem, true);
		}

		public override void ClickDown()
		{
		}
	}
}
