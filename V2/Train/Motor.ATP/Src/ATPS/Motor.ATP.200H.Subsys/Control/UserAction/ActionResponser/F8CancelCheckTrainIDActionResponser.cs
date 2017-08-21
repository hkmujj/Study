using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F8CancelCheckTrainIDActionResponser : DriverActionResponserBase
    {
        public F8CancelCheckTrainIDActionResponser(IDriverSelectableItem item) : base(item, UserActionType.F8)
        {
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_车次号));
        }
    }
}