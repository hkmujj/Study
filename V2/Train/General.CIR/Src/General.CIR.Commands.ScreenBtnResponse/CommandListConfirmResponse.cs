using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class CommandListConfirmResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.DispatchCommandViewModel.DisplayUnit = ViewModel.DispatchCommandViewModel.SelectUnit;
			Response(BtnItemKeys.调度命令详细界面);
		}

		public override void ClickDown()
		{
		}
	}
}
