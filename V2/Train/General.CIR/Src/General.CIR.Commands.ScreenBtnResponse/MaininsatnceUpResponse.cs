using General.CIR.Extentions;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class MaininsatnceUpResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.MaininstanceViewModel.SelectItem = ViewModel.MaininstanceViewModel.DisplayItems.GetLastItem(ViewModel.MaininstanceViewModel.SelectItem, true);
		}

		public override void ClickDown()
		{
		}
	}
}
