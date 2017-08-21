using System.ComponentModel.Composition;
using General.CIR.Events;
using General.CIR.ViewModels;

namespace General.CIR.Controller.ViewModelController
{
	[Export]
	public class ButtonController : ControllerBase<ButtonViewModel>
	{
		public ButtonController()
		{
			EventAggregator.GetEvent<CallEvent>().Subscribe(delegate(CallEventArgs args)
			{
				base.ViewModel.IsCalling = args.IsCall;
			});
		}

		public override void Initialize()
		{
			ViewModel.F1 = "调度";
			ViewModel.F4 = "车站";
			ViewModel.F6 = "车长";
			ViewModel.F7 = "邻站\r\n组呼";
			ViewModel.F8 = "站内\r\n组呼";
		}

		public void UpdateStates()
		{
			bool isCalling = ViewModel.IsCalling;
			if (isCalling)
			{
			}
		}
	}
}
