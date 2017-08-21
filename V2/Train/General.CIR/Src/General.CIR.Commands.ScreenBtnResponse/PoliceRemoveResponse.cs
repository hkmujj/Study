using System.Threading;
using System.Threading.Tasks;
using CommonUtil.Util;
using General.CIR.CIRData;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class PoliceRemoveResponse : BtnResponseBase
    {
        public override void ClickUp()
        {
            ViewModel.Controller.NavigatorToKey(BtnItemKeys.报警发送报警);
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                ViewModel.Controller.NavigatorToKey(BtnItemKeys.报警报警解除命令);
                Thread.Sleep(5000);
                CIRPacket cIRPacket = default(CIRPacket);
                cIRPacket.Init();
                cIRPacket.SetHeadInfo(2, 19, 11, 2);
                cIRPacket.SetData(CIRCommAgent.StructToBytes(new StartStopLbjAlarmRequest
                {
                    StartStop = 2
                }));
                AppLog.Info("请求解除LBJ报警Mpack.GetPackLen()：{0}", new object[]
                {
                            cIRPacket.GetPackLen()
                });
                CIRCommAgent.SendCIRData(CIRCommAgent.StructToBytes(cIRPacket), cIRPacket.GetDataLen(), false, 0);
            });
        }

        public override void ClickDown()
        {
        }
    }
}
