using System.Diagnostics;
using General.CIR.CIRData;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class TrainNumberRegistConfirmResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			CIRPacket cIRPacket = default(CIRPacket);
			cIRPacket.Init();
			cIRPacket.SetHeadInfo(3, 1, 3, 17);
			TrainNumRegistRequest trainNumRegistRequest = new TrainNumRegistRequest
			{
				Regist = 1,
				Order = 0,
				TrainNum = ViewModel.SettingViewModel.TrainNumber
			};
			cIRPacket.SetData(CIRCommAgent.StructToBytes(trainNumRegistRequest));
			Debug.WriteLine("请求注册车次号pack.GetPackLen()：{0}", new object[]
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
