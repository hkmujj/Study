using System;
using System.Collections.Generic;
using General.CIR.Extentions;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class TrainNumberRegistNumber3Response : BtnResponseBase
	{
		private readonly IList<string> m_Values;

		private string m_LastStr;

		private DateTime m_LastTime;

		public TrainNumberRegistNumber3Response()
		{
			m_Values = new List<string>
			{
				"3",
				"D",
				"E",
				"F"
			};
		}

		public override void ClickUp()
		{
			TimeSpan timeSpan = DateTime.Now - m_LastTime;
			m_LastTime = DateTime.Now;
			bool flag = timeSpan.TotalSeconds > 1.0;
			if (flag)
			{
				m_LastStr = m_Values[0];
				ViewModel.SettingViewModel.TrainNumber = ViewModel.SettingViewModel.TrainNumber + m_LastStr;
			}
			else
			{
				m_LastStr = m_Values.GetCurrentString(m_LastStr);
				ViewModel.SettingViewModel.TrainNumber = ViewModel.SettingViewModel.TrainNumber.GetCurrentNumber(m_Values, m_LastStr);
			}
		}

		public override void ClickDown()
		{
		}
	}
}
