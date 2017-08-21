using System;
using System.Threading;
using System.Threading.Tasks;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class MasterGSMRF1CallResponse : BtnResponseBase
    {

        public override void ClickUp()
        {
            Response(BtnItemKeys.主界面信息区域正在呼叫);
            ViewModel.MainContentViewModel.MasterInfoViewModel.CallName = "调度员";
            ViewModel.MainContentViewModel.ButtonViewModel.IsCalling = true;
            Task.Factory.StartNew((() =>
           {
               Thread.Sleep(TimeSpan.FromSeconds(1));
               var isCalling = ViewModel.MainContentViewModel.ButtonViewModel.IsCalling;
               if (isCalling)
               {
                   Response(BtnItemKeys.主界面信息区域呼叫接通);
               }
           }));
        }

        public override void ClickDown()
        {
        }
    }
}
