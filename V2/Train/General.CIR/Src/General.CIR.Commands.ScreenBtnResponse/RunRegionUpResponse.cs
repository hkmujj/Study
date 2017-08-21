using General.CIR.ViewModels;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class RunRegionUpResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			LineSelectViewModel lineSelectViewModel = ViewModel.LineSelectViewModel;
			int num = lineSelectViewModel.Bureaus.IndexOf(lineSelectViewModel.SelectItems);
			lineSelectViewModel.SelectItems = ((num - 1 > 0) ? lineSelectViewModel.Bureaus[num - 1] : lineSelectViewModel.Bureaus[0]);
		}

		public override void ClickDown()
		{
		}
	}
}
