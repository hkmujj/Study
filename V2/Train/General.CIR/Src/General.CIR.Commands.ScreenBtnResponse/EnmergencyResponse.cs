using System;
using System.Threading;
using System.Threading.Tasks;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class EnmergencyResponse : BtnResponseBase
    {
        public override void ClickUp()
        {
            Response(BtnItemKeys.主界面信息区域正在呼叫);
            ViewModel.MainContentViewModel.MasterInfoViewModel.CallName = "紧急";
            ViewModel.MainContentViewModel.ButtonViewModel.IsCalling = true;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                var isClling = ViewModel.MainContentViewModel.ButtonViewModel.IsCalling;
                if (isClling)
                {
                    Response(BtnItemKeys.主界面信息区域呼叫接通);
                }
            });
        }

        public override void ClickDown()
        {
        }
    }
}
