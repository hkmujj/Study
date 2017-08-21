using System.Diagnostics;
using System.Threading;
using General.CIR.CIRData;
using General.CIR.Events;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class CallBtnResponse : BtnResponseBase
	{
		public CallBtnResponse()
		{
			EventAggregator.GetEvent<NetWorkDataEvent>().Subscribe(CallBack);
		}

		private void CallBack(NetWorkEventArgs obj)
		{
			bool flag = obj.BusinessType == BusinessType.MMI;
			if (flag)
			{
				bool flag2 = obj.Data.Cbcmd == 91;
				if (flag2)
				{
					CallConfirmResponeInitiativeGSMR callConfirmResponeInitiativeGSMR = (CallConfirmResponeInitiativeGSMR)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(CallConfirmResponeInitiativeGSMR), 0);
					Debug.WriteLine("呼叫成功：{0}，进入通话状态：{1}", new object[]
					{
						callConfirmResponeInitiativeGSMR.IsSucceed,
						callConfirmResponeInitiativeGSMR.IsOnThePhone
					});
					bool isSucceed = callConfirmResponeInitiativeGSMR.IsSucceed;
					if (isSucceed)
					{
						Thread.Sleep(1000);
						Response(BtnItemKeys.主界面信息区域呼叫接通);
					}
				}
			}
		}

		public override void ClickUp()
		{
			Response(BtnItemKeys.主界面信息区域正在呼叫);
			ViewModel.MainContentViewModel.MasterInfoViewModel.CallName = ViewModel.CalllViewModel.CallNumber.Text;
			ViewModel.MainContentViewModel.ButtonViewModel.IsCalling = true;
			CIRPacket cIRPacket = default(CIRPacket);
			cIRPacket.Init();
			cIRPacket.SetHeadInfo(3, 1, 3, 19);
			cIRPacket.SetData(CIRCommAgent.StructToBytes(new CallRequestInitiativeGSMR
			{
				callType = 3,
				CallNumber = ViewModel.CalllViewModel.CallNumber.Text
			}));
			Debug.WriteLine("GSM-R主动呼叫Mpack.GetPackLen()：{0}", new object[]
			{
				cIRPacket.GetPackLen()
			});
			CIRCommAgent.SendCIRData(CIRCommAgent.StructToBytes(cIRPacket), cIRPacket.GetDataLen(), false, 0);
		}

		public override void ClickDown()
		{
		}
	}
}
