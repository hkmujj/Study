using System;
using System.Diagnostics;
using System.Text;
using General.CIR.CIRData;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class CommandInfoConfirmResponse : BtnResponseBase
	{
		public override void ClickUp()
		{
			CIRPacket cIRPacket = default(CIRPacket);
			cIRPacket.Init();
			cIRPacket.SetHeadInfo(1, 35, 6, 81);
			DispatchInfoSignConfirm dispatchInfoSignConfirm = default(DispatchInfoSignConfirm);
			dispatchInfoSignConfirm.Init();
			dispatchInfoSignConfirm.InfoName = 130;
			dispatchInfoSignConfirm.cmdForm = ViewModel.DispatchCommandViewModel.DisplayUnit.Info.cmdForm;
			ViewModel.DispatchCommandViewModel.DisplayUnit.Info.cmdNum.CopyTo(dispatchInfoSignConfirm.cmdNum, 0);
			dispatchInfoSignConfirm.TrainID = ViewModel.MainContentViewModel.TitleViewModel.TrainNumber;
			dispatchInfoSignConfirm.TrainNum = ViewModel.MainContentViewModel.TitleViewModel.EngineNumber;
			ViewModel.DispatchCommandViewModel.DisplayUnit.IsSign = true;
			cIRPacket.SetData(CIRCommAgent.StructToBytes(dispatchInfoSignConfirm));
			Debug.WriteLine("手动签收收到调度命令pack2.GetPackLen()：{0}", new object[]
			{
				cIRPacket.GetPackLen()
			});
			CIRCommAgent.SendCIRData(CIRCommAgent.StructToBytes(cIRPacket), cIRPacket.GetDataLen(), false, 0);
			ViewModel.DispatchCommandViewModel.DisplayUnit.Content = GetContent(ViewModel.DispatchCommandViewModel.DisplayUnit.Content);
		}

		private static string GetContent(string str)
		{
			StringBuilder stringBuilder = new StringBuilder(str);
			stringBuilder.AppendLine(string.Format("签收时间：{0}", DateTime.Now.ToString("hh时mm分ss秒")));
			stringBuilder.AppendLine(string.Format("签收地点：公里标{0:F1}", ViewModel.MainContentViewModel.SystemInfosViewModel.KMMark));
			return stringBuilder.ToString();
		}

		public override void ClickDown()
		{
		}
	}
}
