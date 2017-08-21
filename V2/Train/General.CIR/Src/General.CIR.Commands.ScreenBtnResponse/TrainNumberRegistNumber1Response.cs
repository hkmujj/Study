namespace General.CIR.Commands.ScreenBtnResponse
{
	public class TrainNumberRegistNumber1Response : BtnResponseBase
	{
		public override void ClickUp()
		{
			string trainNumber = ViewModel.SettingViewModel.TrainNumber;
			ViewModel.SettingViewModel.TrainNumber = trainNumber + "1";
		}

		public override void ClickDown()
		{
		}
	}
}
