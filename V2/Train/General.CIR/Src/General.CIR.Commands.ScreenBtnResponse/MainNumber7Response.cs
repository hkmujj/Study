using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class MainNumber7Response : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.Controller.NavigatorToKey(BtnItemKeys.主界面电话号码输入);
			ViewModel.CalllViewModel.InitText("7");
		}

		public override void ClickDown()
		{
		}
	}
}
