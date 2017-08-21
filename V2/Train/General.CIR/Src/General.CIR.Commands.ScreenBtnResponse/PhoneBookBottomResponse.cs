using General.CIR.ViewModels;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class PhoneBookBottomResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			PhoneBookViewModel phoneBookViewModel = ViewModel.PhoneBookViewModel;
			int num = phoneBookViewModel.AllLujus.IndexOf(phoneBookViewModel.SelectItems);
			phoneBookViewModel.SelectItems = ((num + 1 < phoneBookViewModel.AllLujus.Count - 1) ? phoneBookViewModel.AllLujus[num + 1] : phoneBookViewModel.AllLujus[phoneBookViewModel.AllLujus.Count - 1]);
		}

		public override void ClickDown()
		{
		}
	}
}
