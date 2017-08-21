using System;
using System.Threading;
using System.Threading.Tasks;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class HangUpResponse : BtnResponseBase
    {
        public override void ClickUp()
        {
            ViewModel.MainContentViewModel.MasterInfoViewModel.CenterInfo = "通话结束";
            ViewModel.MainContentViewModel.ButtonViewModel.IsCalling = false;
            Response(BtnItemKeys.主界面信息区域中心信息);
            Task.Factory.StartNew((() =>
           {
               Thread.Sleep(TimeSpan.FromSeconds(2));
               var flag = !ViewModel.MainContentViewModel.ButtonViewModel.IsCalling;
               if (flag)
               {
                   Response(BtnItemKeys.主界面信息区域空白区域);
                   Response(BtnItemKeys.主界面GSMR);
               }
           }));
        }

        public override void ClickDown()
        {
        }
    }
}
