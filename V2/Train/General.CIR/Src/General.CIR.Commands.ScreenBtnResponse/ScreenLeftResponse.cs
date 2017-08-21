using General.CIR.ViewModels;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class ScreenLeftResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			SettingViewModel expr_0B = ViewModel.SettingViewModel;
			double screen = expr_0B.Screen;
			expr_0B.Screen = screen - 1.0;
		}

		public override void ClickDown()
		{
		}
	}
}
