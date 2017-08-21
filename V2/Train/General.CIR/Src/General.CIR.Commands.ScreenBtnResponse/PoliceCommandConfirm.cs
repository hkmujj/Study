using System.Threading;
using System.Threading.Tasks;
using CommonUtil.Util;
using General.CIR.CIRData;
using General.CIR.Controller;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class PoliceCommandConfirm : BtnResponseBase
    {
     

        public override void ClickUp()
        {
            CIRController controller = ViewModel.Controller;
            controller.NavigatorToKey(BtnItemKeys.报警发送报警命令);
            CIRPacket cIRPacket = default(CIRPacket);
            cIRPacket.Init();
            cIRPacket.SetHeadInfo(2, 19, 11, 2);
            StartStopLbjAlarmRequest startStopLbjAlarmRequest = new StartStopLbjAlarmRequest
            {
                StartStop = 1
            };
            cIRPacket.SetData(CIRCommAgent.StructToBytes(startStopLbjAlarmRequest));
            AppLog.Info("请求启动LBJ报警Mpack.GetPackLen()：{0}", new object[]
            {
                cIRPacket.GetPackLen()
            });
            CIRCommAgent.SendCIRData(CIRCommAgent.StructToBytes(cIRPacket), cIRPacket.GetDataLen(), false, 0);
            ViewModel.MainContentViewModel.PoliceViewModel.Trips = InFoResource.启动报警命令已发送;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                ViewModel.MainContentViewModel.PoliceViewModel.Trips = InFoResource.正在发送报警信息;
            });
        }

        public override void ClickDown()
        {
        }
    }
}
