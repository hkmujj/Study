using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;
using Motor.ATP._300H.Subsys.Constant;

namespace Motor.ATP._300H.Subsys.Control.UserAction.ActionResponser
{
    public class F1DataDriverIDActionResponser : DriverActionResponserBase
    {
        public F1DataDriverIDActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F2)
        {
        }

        public override void ResponseMouseUp()
        {
            if (ATP.VersionManager.MainVersion >= VersionMarker.V1)
            {
                InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_司机号查看));
            }
            else if (ATP.VersionManager.MainVersion >= VersionMarker.V0)
            {
                InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_司机号));
            }
        }
    }
}