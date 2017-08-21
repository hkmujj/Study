using General.CIR.ViewModels;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class MasterGSMRLeftResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			bool isSpeaker = ViewModel.MainContentViewModel.TitleViewModel.IsSpeaker;
			if (isSpeaker)
			{
				bool flag = ViewModel.MainContentViewModel.TitleViewModel.SpeakerSound > 0.0;
				if (flag)
				{
					TitleViewModel expr_4D = ViewModel.MainContentViewModel.TitleViewModel;
					double num = expr_4D.SpeakerSound;
					expr_4D.SpeakerSound = num - 1.0;
				}
			}
			else
			{
				bool flag2 = ViewModel.MainContentViewModel.TitleViewModel.ReciverSound > 0.0;
				if (flag2)
				{
					TitleViewModel expr_9D = ViewModel.MainContentViewModel.TitleViewModel;
					double num = expr_9D.ReciverSound;
					expr_9D.ReciverSound = num - 1.0;
				}
			}
		}

		public override void ClickDown()
		{
		}
	}
}
