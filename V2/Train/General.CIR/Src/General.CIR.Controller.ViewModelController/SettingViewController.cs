using System.ComponentModel.Composition;
using General.CIR.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace General.CIR.Controller.ViewModelController
{
	[Export]
	public class SettingViewController : ControllerBase<SettingViewModel>
	{
		protected TitleViewModel TitleViewModel
		{
			get;
			private set;
		}

		public override void Initialize()
		{
		}

		public void UpdateTrips()
		{
			bool flag = TitleViewModel == null;
			if (flag)
			{
				TitleViewModel = ServiceLocator.Current.GetInstance<TitleViewModel>();
			}
		}
	}
}
