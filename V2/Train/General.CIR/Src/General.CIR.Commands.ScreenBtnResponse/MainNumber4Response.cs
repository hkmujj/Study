using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class MainNumber4Response : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.Controller.NavigatorToKey(BtnItemKeys.主界面电话号码输入);
			ViewModel.CalllViewModel.InitText("4");
		}

		public override void ClickDown()
		{
		}
	}
}
