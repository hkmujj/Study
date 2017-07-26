using System;
using MMI.Facility.Interface;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300S.Subsys.Control
{
    public class ATP300SDriverInterfaceController : DriverInterfaceController
    {
        public ATP300SDriverInterfaceController(ATPDomain atp, IDriverInterfaceFactory factory,
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