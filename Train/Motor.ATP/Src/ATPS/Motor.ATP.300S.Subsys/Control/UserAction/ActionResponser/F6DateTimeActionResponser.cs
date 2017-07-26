using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class F6DateTimeActionResponser : DriverActionResponserBase
    {
        public F6DateTimeActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F6)
        {
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_日期时间));
        }
    }
}