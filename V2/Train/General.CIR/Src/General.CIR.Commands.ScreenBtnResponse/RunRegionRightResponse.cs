using General.CIR.ViewModels;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class RunRegionRightResponse : BtnResponseBase
	{
		private const int Flag = 9;

		public override void ClickUp()
		{
			LineSelectViewModel lineSelectViewModel = ViewModel.LineSelectViewModel;
			int num = lineSelectViewModel.Bureaus.IndexOf(lineSelectViewModel.SelectItems);
			lineSelectViewModel.SelectItems = ((num + 9 < lineSelectViewModel.Bureaus.Count) ? lineSelectViewModel.Bureaus[num + 9] : lineSelectViewModel.Bureaus[lineSelectViewModel.Bureaus.Count - 1]);
		}

		public override void ClickDown()
		{
		}
	}
}
