using System.ComponentModel.Composition;
using System.Threading;
using General.CIR.Contance;
using General.CIR.Events;
using General.CIR.ViewModels;

namespace General.CIR.Controller.ViewModelController
{
	[Export]
	public class MasterInfoController : ControllerBase<MasterInfoViewModel>
	{
		public MasterInfoController()
		{
			EventAggregator.GetEvent<CallEvent>().Subscribe(CallAction);
		}

		private void CallAction(CallEventArgs args)
		{
			ViewModel.CallName = args.Name;
			bool isCall = args.IsCall;
			if (isCall)
			{
				EventAggregator.GetEvent<NavigatorEvent>().Publish(ViewNames.CallingView);
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					Thread.Sleep(1000);
					base.EventAggregator.GetEvent<NavigatorEvent>().Publish(ViewNames.InCommingCallView);
				});
			}
			else
			{
				ViewModel.CenterInfo = "通话结束";
				EventAggregator.GetEvent<NavigatorEvent>().Publish(ViewNames.CnterInfoView);
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					Thread.Sleep(2000);
					base.EventAggregator.GetEvent<NavigatorEvent>().Publish(ViewNames.MasterInfoNull);
				});
			}
		}

		public override void Initialize()
		{
			ViewModel.CallName = string.Empty;
		}
	}
}
