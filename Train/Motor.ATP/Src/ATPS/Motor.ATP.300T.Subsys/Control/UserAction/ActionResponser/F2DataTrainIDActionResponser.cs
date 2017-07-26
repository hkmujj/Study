using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F2DataTrainIDActionResponser : DriverActionResponserBase
    {
        public F2DataTrainIDActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F2)
        {
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_车次号));
        }
    }
}