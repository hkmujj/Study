using General.CIR.ViewModels;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class PhoneBookLeftResponse : BtnResponseBase
	{
		private const int Flag = 9;

		public override void ClickUp()
		{
			PhoneBookViewModel phoneBookViewModel = ViewModel.PhoneBookViewModel;
			int num = phoneBookViewModel.AllLujus.IndexOf(phoneBookViewModel.SelectItems);
			phoneBookViewModel.SelectItems = ((num - 9 >= 0) ? phoneBookViewModel.AllLujus[num - 9] : phoneBookViewModel.AllLujus[0]);
		}

		public override void ClickDown()
		{
		}
	}
}
