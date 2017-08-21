using System;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200H.Subsys.Control
{
    public class ATP200HDriverInterfaceController : DriverInterfaceController
    {
        public ATP200HDriverInterfaceController(ATPDomain atp, IDriverInterfaceFactory factory,
            IDriverInterfaceView interfaceView)
            : base(atp)
        {

            DriverInterfaceFactory = factory;
            DriverInterfaceView = interfaceView;

        }

        protected override void OnInfomationBegin(IInfomationItem infomationItem)
        {
            if (infomationItem.Content.IsOnlyTextInfo())
            {
                return;
            }
            switch (infomationItem.Content.ResponseType)
            {
                case InfomationResponseType.System:
                    break;
                case InfomationResponseType.Vigilant:
                    break;
                case InfomationResponseType.Relase:
                    break;
                case InfomationResponseType.Ok:
                    UpdateDriverInterface(infomationItem.Content.ShowType.HasPopupView()
                        ? DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认有弹出框)
                        : DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认无弹出框));
                    break;
                case InfomationResponseType.OkCancel:
                    UpdateDriverInterface(infomationItem.Content.ShowType.HasPopupView()
                        ? DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消有弹出框)
                        : DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消无弹出框));
                    break;
                case InfomationResponseType.OkDelteCancel:
                    UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认删除取消));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}