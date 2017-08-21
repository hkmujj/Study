using General.CIR.ViewModels;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class RecverRightResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			TitleViewModel expr_10 = ViewModel.MainContentViewModel.TitleViewModel;
			double reciverSound = expr_10.ReciverSound;
			expr_10.ReciverSound = reciverSound + 1.0;
		}

		public override void ClickDown()
		{
		}
	}
}
