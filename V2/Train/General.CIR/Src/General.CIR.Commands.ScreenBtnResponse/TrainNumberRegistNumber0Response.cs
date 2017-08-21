namespace General.CIR.Commands.ScreenBtnResponse
{
	public class TrainNumberRegistNumber0Response : BtnResponseBase
	{
		public override void ClickUp()
		{
			string trainNumber = ViewModel.SettingViewModel.TrainNumber;
			ViewModel.SettingViewModel.TrainNumber = trainNumber + "0";
		}

		public override void ClickDown()
		{
		}
	}
}
