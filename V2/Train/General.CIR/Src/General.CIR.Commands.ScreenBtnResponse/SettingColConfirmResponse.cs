using System.Diagnostics;
using General.CIR.CIRData;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class SettingColConfirmResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			CIRPacket cIRPacket = default(CIRPacket);
			cIRPacket.Init();
			cIRPacket.SetHeadInfo(3, 5, 4, 37);
			TailInfo tailInfo = new TailInfo
			{
				TrainID = ViewModel.MainContentViewModel.TitleViewModel.EngineNumber,
				TailID = ViewModel.MainContentViewModel.ColumnEndViewModel.ID
			};
			cIRPacket.SetData(CIRCommAgent.StructToBytes(tailInfo));
			Debug.WriteLine("请求连接列尾Mpack.GetPackLen()：{0}", new object[]
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
