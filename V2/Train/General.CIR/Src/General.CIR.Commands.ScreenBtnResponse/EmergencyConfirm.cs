using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class EmergencyConfirm : BtnResponseBase
	{
		public override void ClickUp()
		{
			ViewModel.Controller.NavigatorToKey(BtnItemKeys.报警紧急运行);
		}

		public override void ClickDown()
		{
		}
	}
}
