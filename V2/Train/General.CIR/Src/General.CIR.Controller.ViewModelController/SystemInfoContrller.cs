using System.ComponentModel.Composition;
using General.CIR.CIRData;
using General.CIR.Events;
using General.CIR.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace General.CIR.Controller.ViewModelController
{
	[Export]
	public class SystemInfoContrller : ControllerBase<SystemInfosViewModel>
	{
		private CIRViewModel m_CIRViewModel;

		public SystemInfoContrller()
		{
			EventAggregator.GetEvent<NetWorkDataEvent>().Subscribe(NetWorkResponse);
		}

		private void NetWorkResponse(NetWorkEventArgs obj)
		{
			bool flag = m_CIRViewModel == null;
			if (flag)
			{
				m_CIRViewModel = ServiceLocator.Current.GetInstance<CIRViewModel>();
			}
			bool flag2 = obj.BusinessType == BusinessType.MMI && obj.Data.Cbcmd == 85;
			if (flag2)
			{
				SysInfo sysInfo = (SysInfo)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(SysInfo), 0);
				ViewModel.LineIsAuto = sysInfo.IsHandWorkMode;
				ViewModel.LineName = sysInfo.LineName.Split(new char[]
				{
					';'
				})[0];
				ViewModel.KMMark = sysInfo.KMMark;
				ViewModel.IsSupplyOrder = (sysInfo.supplyOrder == 0);
				ViewModel.IsShuntTrain = sysInfo.IsShuntTrain;
				ViewModel.SignalModel = ((sysInfo.singalMode == 101) ? "GSM-R" : "450M");
				TitleViewModel instance = ServiceLocator.Current.GetInstance<TitleViewModel>();
				instance.EngineNumberIsRegister = sysInfo.IsRegistTrainIDSucceed;
				instance.TrainNumberIsRegister = sysInfo.IsRegistTrainNumSucceed;
			}
		}
	}
}
