using General.CIR.ViewModels;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class RunRegionBottomResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			LineSelectViewModel lineSelectViewModel = ViewModel.LineSelectViewModel;
			int num = lineSelectViewModel.Bureaus.IndexOf(lineSelectViewModel.SelectItems);
			lineSelectViewModel.SelectItems = ((num + 1 < lineSelectViewModel.Bureaus.Count - 1) ? lineSelectViewModel.Bureaus[num + 1] : lineSelectViewModel.Bureaus[lineSelectViewModel.Bureaus.Count - 1]);
		}

		public override void ClickDown()
		{
		}
	}
}
