using General.CIR.ViewModels;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class PhoneBookRegionUpResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			PhoneBookViewModel phoneBookViewModel = ViewModel.PhoneBookViewModel;
			int num = phoneBookViewModel.AllLujus.IndexOf(phoneBookViewModel.SelectItems);
			phoneBookViewModel.SelectItems = ((num - 1 > 0) ? phoneBookViewModel.AllLujus[num - 1] : phoneBookViewModel.AllLujus[0]);
		}

		public override void ClickDown()
		{
		}
	}
}
