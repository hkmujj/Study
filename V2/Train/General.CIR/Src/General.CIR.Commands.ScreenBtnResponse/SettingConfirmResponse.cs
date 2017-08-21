using System.Windows;
using Microsoft.Practices.Prism.Commands;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class SettingConfirmResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			CustomCommandBase expr_15 = ViewModel.SettingViewModel.SelectItem.Response;
			if (expr_15 != null)
			{
				DelegateCommand expr_20 = expr_15.Command;
				if (expr_20 != null)
				{
					expr_20.Execute();
				}
			}
			ViewModel.SettingViewModel.TripsVisibility = Visibility.Visible;
		}

		public override void ClickDown()
		{
		}
	}
}
