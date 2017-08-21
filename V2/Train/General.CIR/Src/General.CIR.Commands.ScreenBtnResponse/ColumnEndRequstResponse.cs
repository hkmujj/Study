using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using General.CIR.CIRData;
using General.CIR.Events;
using General.CIR.Views.ColumnEnd;

namespace General.CIR.Commands.ScreenBtnResponse
{
    public class ColumnEndRequstResponse : BtnResponseBase
    {

        private bool m_IsDown = false;

        public override void ClickUp()
        {
            bool isDown = m_IsDown;
            if (!isDown)
            {
                //ViewModel.Controller.NavigatorTo(typeof(ColumnEndTwo).FullName);
                Task.Factory.StartNew(delegate (object s)
                {
                    Thread.Sleep(1000);
                    base.EventAggregator.GetEvent<NavigatorEvent>().Publish(typeof(ColumnEndOne).FullName);
                }, null);
            }
        }

        public override void ClickDown()
        {
            m_IsDown = true;
            ViewModel.MainContentViewModel.ColumnEndViewModel.IsRequst = true;
            Task.Factory.StartNew(new Action(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                ViewModel.MainContentViewModel.ColumnEndViewModel.IsRequst = false;
            }));
            CIRPacket cIRPacket = default(CIRPacket);
            cIRPacket.Init();
            cIRPacket.SetHeadInfo(3, 5, 4, 37);
            Debug.WriteLine("确认连接列尾pack1.GetPackLen()：{0}", new object[]
            {
                cIRPacket.GetPackLen()
            });
            CIRCommAgent.SendCIRData(CIRCommAgent.StructToBytes(cIRPacket), cIRPacket.GetDataLen(), false, 0);
        }
    }
}
