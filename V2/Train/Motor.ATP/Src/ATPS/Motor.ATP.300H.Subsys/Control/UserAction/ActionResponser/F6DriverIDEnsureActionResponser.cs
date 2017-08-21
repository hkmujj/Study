using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300H.Subsys.Control.UserAction.ActionResponser
{
    public class F6DriverIDEnsureActionResponser : DriverActionResponserBase
    {
        public F6DriverIDEnsureActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F6)
        {
        }
    }
}