using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class CallInputBackspaceResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			bool flag = string.IsNullOrEmpty(ViewModel.CalllViewModel.CallNumber.Text);
			if (flag)
			{
				ViewModel.Controller.NavigatorToKey(BtnItemKeys.主界面信息区域空白区域);
			}
			else
			{
				ViewModel.CalllViewModel.CallNumber.Delete();
			}
		}

		public override void ClickDown()
		{
		}
	}
}
