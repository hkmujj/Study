using System.ComponentModel.Composition;
using CommonUtil.Util;
using General.CIR.CIRData;
using General.CIR.Events;
using General.CIR.ViewModels;

namespace General.CIR.Controller.ViewModelController
{
	[Export]
	public class TitleController : ControllerBase<TitleViewModel>
	{
		public TitleController()
		{
			EventAggregator.GetEvent<NetWorkDataEvent>().Subscribe(NetWorkResponse);
		}

		private void NetWorkResponse(NetWorkEventArgs obj)
		{
			bool flag = obj.BusinessType == BusinessType.MMI;
			if (flag)
			{
				bool flag2 = obj.Data.Cbcmd == 89;
				if (flag2)
				{
					TrainIDRegistResponse trainIDRegistResponse = (TrainIDRegistResponse)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(TrainIDRegistResponse), 0);
					bool isRegist = trainIDRegistResponse.IsRegist;
					if (isRegist)
					{
						ViewModel.EngineNumberIsRegister = trainIDRegistResponse.IsSucceed;
						ViewModel.EngineNumber = trainIDRegistResponse.TrainID;
						AppLog.Info(string.Format("机车号{0}注册结果{1}", trainIDRegistResponse.TrainID, trainIDRegistResponse.IsSucceed));
					}
					else
					{
						ViewModel.EngineNumberIsRegister = !trainIDRegistResponse.IsSucceed;
						AppLog.Info(string.Format("机车号{0}注销结果{1}", trainIDRegistResponse.TrainID, trainIDRegistResponse.IsSucceed));
					}
				}
				else
				{
					bool flag3 = obj.Data.Cbcmd == 88;
					if (flag3)
					{
						TrainNumRegistResponse trainNumRegistResponse = (TrainNumRegistResponse)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(TrainNumRegistResponse), 0);
						bool isRegist2 = trainNumRegistResponse.IsRegist;
						if (isRegist2)
						{
							ViewModel.TrainNumber = trainNumRegistResponse.TrainNum.Trim(new char[1]);
							ViewModel.TrainNumberIsRegister = trainNumRegistResponse.IsSucceed;
							AppLog.Info(string.Format("车次号{0}注册结果{1}", trainNumRegistResponse.TrainNum, trainNumRegistResponse.IsSucceed));
						}
						else
						{
							ViewModel.TrainNumberIsRegister = !trainNumRegistResponse.IsSucceed;
							AppLog.Info(string.Format("车次号{0}注销结果{1}", trainNumRegistResponse.TrainNum, trainNumRegistResponse.IsSucceed));
						}
					}
				}
			}
		}
	}
}
