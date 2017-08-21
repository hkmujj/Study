using System.Threading;
using System.Threading.Tasks;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class MasterGSMRF6Call : BtnResponseBase
    {
        public override void ClickUp()
        {
            Response(BtnItemKeys.主界面信息区域正在呼叫);
            ViewModel.MainContentViewModel.MasterInfoViewModel.CallName = string.Format("{0}次运转车长", ViewModel.MainContentViewModel.TitleViewModel.TrainNumber);
            ViewModel.MainContentViewModel.ButtonViewModel.IsCalling = true;
            Task.Factory.StartNew((() =>
           {
               Thread.Sleep(1000);
               bool isCalling = ViewModel.MainContentViewModel.ButtonViewModel.IsCalling;
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
