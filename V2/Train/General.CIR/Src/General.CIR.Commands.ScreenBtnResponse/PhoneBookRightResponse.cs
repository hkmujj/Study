using General.CIR.ViewModels;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class PhoneBookRightResponse : BtnResponseBase
	{
		private const int Flag = 9;

		public override void ClickUp()
		{
			PhoneBookViewModel phoneBookViewModel = ViewModel.PhoneBookViewModel;
			int num = phoneBookViewModel.AllLujus.IndexOf(phoneBookViewModel.SelectItems);
			phoneBookViewModel.SelectItems = ((num + 9 < phoneBookViewModel.AllLujus.Count) ? phoneBookViewModel.AllLujus[num + 9] : phoneBookViewModel.AllLujus[phoneBookViewModel.AllLujus.Count - 1]);
		}

		public override void ClickDown()
		{
		}
	}
}
