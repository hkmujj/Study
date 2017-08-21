using General.CIR.ViewModels;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class RunRegionLeftResponse : BtnResponseBase
	{
		private const int Flag = 9;

		public override void ClickUp()
		{
			LineSelectViewModel lineSelectViewModel = ViewModel.LineSelectViewModel;
			int num = lineSelectViewModel.Bureaus.IndexOf(lineSelectViewModel.SelectItems);
			lineSelectViewModel.SelectItems = ((num - 9 >= 0) ? lineSelectViewModel.Bureaus[num - 9] : lineSelectViewModel.Bureaus[0]);
		}

		public override void ClickDown()
		{
		}
	}
}
