using System;
using System.Threading;
using System.Threading.Tasks;
using General.CIR.Resource;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class MasterGSMRF4Call : BtnResponseBase
    {
        //[CompilerGenerated]
        //[Serializable]
        //private sealed class <>c
        //{
        //	public static readonly MasterGSMRF4Call.<>c <>9 = new MasterGSMRF4Call.<>c();

        //	public static Action<object> <>9__0_0;

        //	internal void <ClickUp>b__0_0(object state)
        //	{
        //		Thread.Sleep(1000);
        //		bool isCalling = ViewModel.MainContentViewModel.ButtonViewModel.IsCalling;
        //		if (isCalling)
        //		{
        //			Response(BtnItemKeys.主界面信息区域呼叫接通);
        //		}
        //	}
        //}

        public override void ClickUp()
        {
            Response(BtnItemKeys.主界面信息区域正在呼叫);
            ViewModel.MainContentViewModel.MasterInfoViewModel.CallName = "车站值班员";
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
