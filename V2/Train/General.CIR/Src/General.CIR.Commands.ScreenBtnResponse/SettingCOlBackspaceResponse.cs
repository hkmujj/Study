namespace General.CIR.Commands.ScreenBtnResponse
{
	public class SettingCOlBackspaceResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			string settingID = ViewModel.MainContentViewModel.ColumnEndViewModel.SettingID;
			bool flag = string.IsNullOrEmpty(settingID) || settingID.Length == 1;
			if (flag)
			{
				ViewModel.MainContentViewModel.ColumnEndViewModel.SettingID = string.Empty;
			}
			else
			{
				ViewModel.MainContentViewModel.ColumnEndViewModel.SettingID = settingID.Substring(0, settingID.Length - 1);
			}
		}

		public override void ClickDown()
		{
		}
	}
}
