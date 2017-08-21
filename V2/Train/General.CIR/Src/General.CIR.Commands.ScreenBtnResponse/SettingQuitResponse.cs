using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class SettingQuitResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			Response(BtnItemKeys.主界面GSMR);
		}

		public override void ClickDown()
		{
		}
	}
}
