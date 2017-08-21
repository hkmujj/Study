using System;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class ColumnEndRemoveBtnResponse : BtnResponseBase
	{
		private static DateTime m_DownTime;

		public override void ClickUp()
		{
			bool flag = (DateTime.Now - m_DownTime).TotalSeconds > 3.0;
			if (flag)
			{
			}
		}

		public override void ClickDown()
		{
			m_DownTime = DateTime.Now;
		}
	}
}
