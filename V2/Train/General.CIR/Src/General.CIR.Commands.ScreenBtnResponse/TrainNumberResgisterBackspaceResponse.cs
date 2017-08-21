namespace General.CIR.Commands.ScreenBtnResponse
{
	public class TrainNumberResgisterBackspaceResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			string trainNumber = ViewModel.SettingViewModel.TrainNumber;
			bool flag = trainNumber == null || trainNumber.Length <= 1;
			if (flag)
			{
				ViewModel.SettingViewModel.TrainNumber = string.Empty;
			}
			else
			{
				ViewModel.SettingViewModel.TrainNumber = trainNumber.Substring(0, trainNumber.Length - 1);
			}
		}

		public override void ClickDown()
		{
		}
	}
}
