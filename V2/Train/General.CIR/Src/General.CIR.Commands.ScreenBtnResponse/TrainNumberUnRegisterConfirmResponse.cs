using System.Diagnostics;
using General.CIR.CIRData;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class TrainNumberUnRegisterConfirmResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			CIRPacket cIRPacket = default(CIRPacket);
			cIRPacket.Init();
			cIRPacket.SetHeadInfo(3, 1, 3, 17);
			cIRPacket.SetData(CIRCommAgent.StructToBytes(new TrainNumRegistRequest
			{
				Regist = 0,
				Order = 0,
				TrainNum = ViewModel.MainContentViewModel.TitleViewModel.TrainNumber
			}));
			Debug.WriteLine("请求注销车次号pack.GetPackLen()：{0}", new object[]
			{
				cIRPacket.GetPackLen()
			});
			CIRCommAgent.SendCIRData(CIRCommAgent.StructToBytes(cIRPacket), cIRPacket.GetDataLen(), false, 0);
			ViewModel.Controller.NavigatorToKey(BtnItemKeys.主界面GSMR);
		}

		public override void ClickDown()
		{
		}
	}
}
